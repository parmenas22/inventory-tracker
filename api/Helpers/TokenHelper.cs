using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Helpers
{
    public static class TokenHelper
    {
        public static string GenerateToken(string firstName, string lastName, string email, List<string> roles, IConfiguration configuration)
        {
            var issuer = configuration["Jwt:Issuer"];
            var secretKey = configuration["Jwt:Key"];
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("FirstName", firstName),
                new Claim("LastName",lastName)
            };


            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: issuer, claims: claims, expires: DateTime.Now.AddHours(24), signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}