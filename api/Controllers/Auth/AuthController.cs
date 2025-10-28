using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Auth;
using api.Helpers;
using api.Services.Auth;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto requestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<LoginResponseDto>.Fail(System.Net.HttpStatusCode.BadRequest, "Invalid request payload"));
            }

            var result = await _authService.Login(requestDto);
            if (!result.Succeeded)
            {
                return StatusCode((int)result.StatusCode, result);
            }

            return Ok(result);
        }
    }
}