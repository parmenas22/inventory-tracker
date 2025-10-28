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
        string? GetCurrentUser();
        Task<string?> GetNameFromUserId(Guid userId);
    }
}