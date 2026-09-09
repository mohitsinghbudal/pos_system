using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace POS.Service.Jwt
{
   
        public class JwtService : IJwtService
        {
            private readonly IConfiguration _configuration;

            public JwtService(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public string GenerateAccessToken(User user)
            {
                var claims = new List<Claim>
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    user.Id.ToString()
                ),

                new Claim(
                    JwtRegisteredClaimNames.Email,
                    user.Email
                ),

                new Claim(
                    ClaimTypes.Name,
                    user.Name
                ),

                new Claim(
                    ClaimTypes.Role,
                    user.Role.RoleName
                )
            };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        _configuration["Jwt:Key"]!
                    )
                );

                var credentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                );

                var expires = GetAccessTokenExpiration();

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: expires,
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler()
                    .WriteToken(token);
            }

            public string GenerateRefreshToken()
            {
                var randomBytes = RandomNumberGenerator.GetBytes(64);

                return Convert.ToBase64String(randomBytes);
            }

            public DateTime GetAccessTokenExpiration()
            {
                var minutes = int.Parse(
                    _configuration["Jwt:AccessTokenMinutes"]!
                );

                return DateTime.UtcNow.AddMinutes(minutes);
            }

            public DateTime GetRefreshTokenExpiration()
            {
                var days = int.Parse(
                    _configuration["Jwt:RefreshTokenDays"]!
                );

                return DateTime.UtcNow.AddDays(days);
            }
        }
    }
}

