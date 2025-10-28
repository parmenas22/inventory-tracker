using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.Database;
using api.DTOs;
using api.DTOs.Auth;
using api.Enums;
using api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace api.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public AuthService(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
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
                    Token = TokenHelper.GenerateToken(user.FirstName, user.LastName, user.Email, userRoles, _configuration)
                };

                return ApiResponse<LoginResponseDto>.Success(HttpStatusCode.OK, response, "Login successful");
            }
            catch (System.Exception ex)
            {
                //TODO: improve error handling
                return ApiResponse<LoginResponseDto>.Fail(HttpStatusCode.InternalServerError, "An unexpected error occured on login", ex, ErrorType.LOGIN);
            }
        }
    }
}