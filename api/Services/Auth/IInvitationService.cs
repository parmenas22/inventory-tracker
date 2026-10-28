using api.DTOs.Auth;
using api.Helpers;

namespace api.Services.Auth
{
    public interface IInvitationService
    {
        Task<ApiResponse<List<InvitationRoleDto>>> GetRolesAsync();
        Task<ApiResponse<UserAccessOverviewDto>> GetAdminOverviewAsync(CancellationToken cancellationToken);
        Task<ApiResponse> CreateInvitationAsync(InviteUserRequestDto request, Guid invitedBy, CancellationToken cancellationToken);
        Task<ApiResponse> AcceptInvitationAsync(AcceptInvitationRequestDto request, CancellationToken cancellationToken);
    }
}
