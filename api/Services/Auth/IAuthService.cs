using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Auth;
using api.Helpers;

namespace api.Services.Auth
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginResponseDto>> Login(LoginRequestDto loginRequestDto);
        Task<ApiResponse> Register(RegisterRequestDto registerRequestDto);
        Task<ApiResponse<ForgotPasswordResponseDto>> ForgotPassword(ForgotPasswordRequestDto forgotPasswordRequestDto);
        Task<ApiResponse> ResetPassword(ResetPasswordRequestDto resetPasswordRequestDto);
        string? GetCurrentUser();
        Task<string?> GetNameFromUserId(Guid userId);
        Task<ApiResponse<RefreshTokenResponseDTO>> RefreshAsync(string refreshToken);
        Task<ApiResponse> RevokeRefreshTokensForFamily(Guid familyId);
        Task<ApiResponse> RevokeRefreshTokensForUser(Guid userId);
        Task<ApiResponse> LogoutAsync();
    }
}