using System.Net;
using System.Security.Cryptography;
using System.Text;
using api.Database;
using api.DTOs.Auth;
using api.Helpers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services.Auth
{
    public class InvitationService : IInvitationService
    {
        private static readonly TimeSpan InvitationLifetime = TimeSpan.FromDays(7);
        private readonly AppDbContext _dbContext;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly ILogger<InvitationService> _logger;

        public InvitationService(
            AppDbContext dbContext,
            IEmailSender emailSender,
            IConfiguration configuration,
            ILogger<InvitationService> logger)
        {
            _dbContext = dbContext;
            _emailSender = emailSender;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ApiResponse<List<InvitationRoleDto>>> GetRolesAsync()
        {
            var roles = await _dbContext.Roles
                .AsNoTracking()
                .Where(role => !role.IsDeleted)
                .OrderBy(role => role.Name)
                .Select(role => new InvitationRoleDto { RoleId = role.RoleId, Name = role.Name })
                .ToListAsync();

            return ApiResponse<List<InvitationRoleDto>>.Success(HttpStatusCode.OK, roles, "Roles loaded.");
        }

        public async Task<ApiResponse<UserAccessOverviewDto>> GetAdminOverviewAsync(
            CancellationToken cancellationToken)
        {
            var activeUsers = await _dbContext.Users
                .AsNoTracking()
                .Where(user => !user.IsDeleted)
                .OrderBy(user => user.LastName)
                .ThenBy(user => user.FirstName)
                .Select(user => new ActiveUserDto
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                    Roles = user.UserRoles
                        .Where(userRole => !userRole.IsDeleted && !userRole.Role.IsDeleted)
                        .Select(userRole => userRole.Role.Name)
                        .OrderBy(roleName => roleName)
                        .ToList()
                })
                .ToListAsync(cancellationToken);

            var pendingInvitations = await _dbContext.UserInvitations
                .AsNoTracking()
                .Where(invitation => !invitation.IsDeleted && invitation.AcceptedAt == null)
                .OrderByDescending(invitation => invitation.CreatedAt)
                .Select(invitation => new PendingInvitationDto
                {
                    UserInvitationId = invitation.UserInvitationId,
                    Email = invitation.Email,
                    Role = invitation.Role.Name,
                    CreatedAt = invitation.CreatedAt,
                    ExpiresAt = invitation.ExpiresAt
                })
                .ToListAsync(cancellationToken);

            var overview = new UserAccessOverviewDto
            {
                ActiveUsers = activeUsers,
                PendingInvitations = pendingInvitations
            };

            return ApiResponse<UserAccessOverviewDto>.Success(
                HttpStatusCode.OK,
                overview,
                "User access loaded.");
        }

        public async Task<ApiResponse> CreateInvitationAsync(
            InviteUserRequestDto request,
            Guid invitedBy,
            CancellationToken cancellationToken)
        {
            var email = request.Email.Trim().ToLowerInvariant();
            if (await _dbContext.Users.AnyAsync(user => user.Email.ToLower() == email && !user.IsDeleted, cancellationToken))
            {
                return ApiResponse.Fail(HttpStatusCode.Conflict, "A user with this email already exists.");
            }

            var role = await _dbContext.Roles
                .FirstOrDefaultAsync(candidate => candidate.RoleId == request.RoleId && !candidate.IsDeleted, cancellationToken);
            if (role is null)
            {
                return ApiResponse.Fail(HttpStatusCode.BadRequest, "The selected role is not available.");
            }

            var uiUrl = _configuration["UIUrl"];
            if (!Uri.TryCreate(uiUrl, UriKind.Absolute, out var baseUri) ||
                (baseUri.Scheme != Uri.UriSchemeHttp && baseUri.Scheme != Uri.UriSchemeHttps))
            {
                _logger.LogError("UIUrl must be configured as an absolute URL before invitations can be sent.");
                return ApiResponse.Fail(HttpStatusCode.InternalServerError, "Invitation email is not configured.");
            }

            var rawToken = TokenHelper.GenerateRawToken();
            var invitation = new UserInvitation
            {
                Email = email,
                RoleId = role.RoleId,
                TokenHash = HashToken(rawToken),
                ExpiresAt = DateTimeOffset.UtcNow.Add(InvitationLifetime),
                CreatedBy = invitedBy,
                UpdatedBy = invitedBy
            };

            var previousInvitations = await _dbContext.UserInvitations
                .Where(existing => existing.Email == email && !existing.IsDeleted && existing.AcceptedAt == null)
                .ToListAsync(cancellationToken);
            foreach (var previousInvitation in previousInvitations)
            {
                previousInvitation.IsDeleted = true;
                previousInvitation.UpdatedBy = invitedBy;
                previousInvitation.UpdatedAt = DateTime.UtcNow;
            }

            await _dbContext.UserInvitations.AddAsync(invitation, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var invitationUri = new Uri(baseUri, $"accept-invitation?token={Uri.EscapeDataString(rawToken)}");
            try
            {
                await _emailSender.SendInvitationAsync(email, invitationUri, role.Name, cancellationToken);
            }
            catch (Exception exception)
            {
                invitation.IsDeleted = true;
                invitation.UpdatedBy = invitedBy;
                invitation.UpdatedAt = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync(CancellationToken.None);
                _logger.LogError(exception, "Failed to send an invitation to {Email}.", email);
                return ApiResponse.Fail(HttpStatusCode.InternalServerError, "Unable to send the invitation email.");
            }

            return ApiResponse.Success(HttpStatusCode.OK, $"Invitation sent to {email}.");
        }

        public async Task<ApiResponse> AcceptInvitationAsync(
            AcceptInvitationRequestDto request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Token))
            {
                return ApiResponse.Fail(HttpStatusCode.BadRequest, "The invitation link is invalid or expired.");
            }

            var tokenHash = HashToken(request.Token);
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            var invitation = await _dbContext.UserInvitations
                .AsNoTracking()
                .FirstOrDefaultAsync(candidate => candidate.TokenHash == tokenHash, cancellationToken);
            if (invitation is null ||
                invitation.IsDeleted ||
                invitation.AcceptedAt.HasValue ||
                invitation.ExpiresAt <= DateTimeOffset.UtcNow)
            {
                return ApiResponse.Fail(HttpStatusCode.BadRequest, "The invitation link is invalid or expired.");
            }

            if (await _dbContext.Users.AnyAsync(user => user.Email.ToLower() == invitation.Email && !user.IsDeleted, cancellationToken))
            {
                return ApiResponse.Fail(HttpStatusCode.Conflict, "An account already exists for this invitation.");
            }

            var acceptedAt = DateTimeOffset.UtcNow;
            var claimed = await _dbContext.UserInvitations
                .Where(candidate =>
                    candidate.UserInvitationId == invitation.UserInvitationId &&
                    !candidate.IsDeleted &&
                    candidate.AcceptedAt == null &&
                    candidate.ExpiresAt > acceptedAt)
                .ExecuteUpdateAsync(
                    updates => updates.SetProperty(candidate => candidate.AcceptedAt, acceptedAt),
                    cancellationToken);
            if (claimed == 0)
            {
                return ApiResponse.Fail(HttpStatusCode.BadRequest, "The invitation link is invalid or expired.");
            }

            var user = new User
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                Email = invitation.Email,
                Password = PasswordHelper.HashPassword(request.Password),
                CreatedBy = invitation.CreatedBy,
                UpdatedBy = invitation.CreatedBy
            };
            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.UserRoles.AddAsync(new UserRole
            {
                User = user,
                RoleId = invitation.RoleId,
                CreatedBy = invitation.CreatedBy,
                UpdatedBy = invitation.CreatedBy
            }, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return ApiResponse.Success(HttpStatusCode.OK, "Your account has been created. You can now sign in.");
        }

        private static string HashToken(string token)
        {
            return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
        }
    }
}
