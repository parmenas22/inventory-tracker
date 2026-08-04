using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Database;
using api.DTOs;
using api.DTOs.Auth;
using api.Enums;
using api.Helpers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(AppDbContext dbContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetCurrentUser()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId");
        }

        public async Task<string?> GetNameFromUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return null;
            }

            var user = await _dbContext.Users.AsNoTracking().Where(u => u.UserId == userId && !u.IsDeleted).Select(u => new { u.FirstName, u.LastName }).FirstOrDefaultAsync();

            if (user is null)
            {
                return null;
            }

            return $"{user.FirstName} {user.LastName}";
        }

        public async Task<ApiResponse<ForgotPasswordResponseDto>> ForgotPassword(ForgotPasswordRequestDto forgotPasswordRequestDto)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == forgotPasswordRequestDto.Email.ToLower() && !u.IsDeleted);
                if (user is null)
                {
                    return ApiResponse<ForgotPasswordResponseDto>.Success(System.Net.HttpStatusCode.OK, new ForgotPasswordResponseDto(), "If the email exists, a reset email has been sent.");
                }

                user.ResetToken = Guid.NewGuid().ToString("N");
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();

                var response = new ForgotPasswordResponseDto
                {
                    ResetToken = user.ResetToken
                };

                //send reset email

                return ApiResponse<ForgotPasswordResponseDto>.Success(System.Net.HttpStatusCode.OK, response, "Password reset token generated successfully.");
            }
            catch (System.Exception ex)
            {
                return ApiResponse<ForgotPasswordResponseDto>.Fail(System.Net.HttpStatusCode.InternalServerError, "An unexpected error occurred while generating password reset token.", ex, errorType: ErrorType.LOGIN);
            }
        }

        public async Task<ApiResponse> ResetPassword(ResetPasswordRequestDto resetPasswordRequestDto)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.ResetToken == resetPasswordRequestDto.ResetToken && !u.IsDeleted);
                if (user is null)
                {
                    return ApiResponse.Fail(System.Net.HttpStatusCode.BadRequest, "Invalid or expired reset token.");
                }

                user.Password = PasswordHelper.HashPassword(resetPasswordRequestDto.NewPassword);
                user.ResetToken = null;
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();

                return ApiResponse.Success(System.Net.HttpStatusCode.OK, "Password has been reset successfully.");
            }
            catch (System.Exception ex)
            {
                return ApiResponse.Fail(System.Net.HttpStatusCode.InternalServerError, "An unexpected error occurred while resetting password.", ex, errorType: ErrorType.LOGIN);
            }
        }

        public async Task<ApiResponse> Register(RegisterRequestDto registerRequestDto)
        {
            try
            {
                var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == registerRequestDto.Email.ToLower() && !u.IsDeleted);
                if (existingUser is not null)
                {
                    return ApiResponse.Fail(HttpStatusCode.BadRequest, "Email is already registered.");
                }

                var user = new User
                {
                    FirstName = registerRequestDto.FirstName,
                    LastName = registerRequestDto.LastName,
                    Email = registerRequestDto.Email.ToLower(),
                    Password = PasswordHelper.HashPassword(registerRequestDto.Password),
                    CreatedBy = SeedConstants.SystemUserId,
                    UpdatedBy = SeedConstants.SystemUserId,
                };

                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                var userRole = new UserRole
                {
                    UserId = user.UserId,
                    RoleId = SeedConstants.UserRoleId,
                    CreatedBy = SeedConstants.SystemUserId,
                    UpdatedBy = SeedConstants.SystemUserId,
                };

                await _dbContext.UserRoles.AddAsync(userRole);
                await _dbContext.SaveChangesAsync();

                return ApiResponse.Success(HttpStatusCode.OK, "Registration successful. Proceed to login.");
            }
            catch (System.Exception ex)
            {
                return ApiResponse.Fail(HttpStatusCode.InternalServerError, "An unexpected error occurred during registration.", ex, errorType: ErrorType.LOGIN);
            }
        }

        public async Task<ApiResponse<LoginResponseDto>> Login(LoginRequestDto loginRequestDto)
        {
            try
            {
                //verify if user exists
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginRequestDto.Email.ToLower() && !u.IsDeleted);
                if (user is null)
                {
                    return ApiResponse<LoginResponseDto>.Fail(HttpStatusCode.BadRequest, "User with this email does not exist");
                }
                //verify password
                if (!PasswordHelper.VerifyPassword(user.Password, loginRequestDto.Password))
                {
                    return ApiResponse<LoginResponseDto>.Fail(HttpStatusCode.BadRequest, "Username or password is incorrect");
                }

                //Get user roles
                List<string> userRoles = await _dbContext.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.User.Email == loginRequestDto.Email && !ur.IsDeleted).Select(ur => ur.Role.Name).ToListAsync();

                var response = new LoginResponseDto
                {
                    Token = TokenHelper.GenerateToken(user.FirstName, user.LastName, user.Email, user.UserId.ToString(), userRoles, _configuration)
                };

                return ApiResponse<LoginResponseDto>.Success(HttpStatusCode.OK, response, "Login successful");
            }
            catch (System.Exception ex)
            {
                return ApiResponse<LoginResponseDto>.Fail(HttpStatusCode.InternalServerError, "An unexpected error occured on login", ex, errorType: ErrorType.LOGIN);
            }
        }
    }
}