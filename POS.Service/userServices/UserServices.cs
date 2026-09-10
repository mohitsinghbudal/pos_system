using Microsoft.Extensions.Configuration;
using POS.Interface.DTO;
using POS.Interface.interfaces;
using System.Security.Cryptography;

namespace POS.Service.userServices
{
    public class UserServices : IUserService
    {
        private readonly IUserDll _userDll;
        private readonly IJwtService _jwt;
        private readonly IJwtDll _jwtDll;
        private readonly IConfiguration _config;


        public UserServices(IUserDll userDll, IJwtService jwt, IJwtDll jwtDll, IConfiguration config)
        {
            _userDll = userDll ;
            _jwt = jwt;
            _jwtDll = jwtDll;
            _config = config;
        }

        public async Task<string?> test()
        {
            return await _userDll.test();
        }
        public async Task<bool> SignUp(SignupDTO dto)
        {
            dto.Email = dto.Email.Trim().ToLower();
            dto.PhoneNo = dto.PhoneNo.Trim();

            
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password,workFactor:10);

            var user = new SignupDTO

            {
                Name = dto.Name,
                Email = dto.Email,
                Password = hashedPassword,
                PhoneNo = dto.PhoneNo
            };

            return await _userDll.SignUp(user);
        }
        public async Task<LoginResDTO?> Login(LoginReqDTO dto)
        {
            dto.Email.Trim().ToLower();

            // Check if user exists
            var user = await _userDll.GetUserAsync(dto.Email);
            //if not return 
            if (user == null) throw new Exception("Invalid email or password");

            if(!user.IsActive)
                throw new Exception("User is not active");

            if (string.IsNullOrEmpty(user.PasswordHash))
            {
                throw new Exception("Password hash was not retrieved from database.");
            }

            //if exists, then hash password and compare with the hashed password in the database
            var isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);


            if(!isPasswordValid) throw new Exception("Invalid email or password");

            //generate access token
            

            var accessToken = _jwt.GenerateAccessToken(user);


            var refreshTokenValue = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64)
);

           


            await _jwtDll.AddRefreshTokenAsync(refreshTokenValue, user.Id);
            await _jwtDll.SaveChangesAsync();
            return new LoginResDTO
            {
                AccessToken = accessToken,
                RefreshToken =  refreshTokenValue
            };
        }

        public async Task<RefreshTokenResponseDto?> Refresh(RefreshTokenRequestDto dto)
        {
            //find refresh token in database
            var oldToken = await _jwtDll
                .GetRefreshTokenAsync(dto.RefreshToken);

            if (oldToken == null)
                throw new Exception("Invalid refresh token");

            if (oldToken.ExpiresAt <= DateTime.UtcNow)
                throw new Exception("Refresh token expired");

            if (oldToken.RevokedAt != null)
                throw new Exception("Refresh token has been revoked");

            if (oldToken.UserId <= 0)
                throw new Exception("User not found");

            


            var user = await _userDll.GetUserByIdAsync(oldToken.UserId);

            if(user == null) throw new Exception("User not found");

            if (!user.IsActive)
            {
                throw new Exception("User is not active");
            }
            // Generate new access token
            var accessToken = _jwt.GenerateAccessToken(user);

            // Generate new refresh token
            var newRefreshTokenValue = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64)
            );

            // Revoke old token
            oldToken.IsRevoked = true;
            oldToken.RevokedAt = DateTime.UtcNow;
            oldToken.ReplacedByToken = newRefreshTokenValue;


            await _jwtDll.UpdateRefreshToken(oldToken);

            

            await _jwtDll.AddRefreshTokenAsync(newRefreshTokenValue, oldToken.UserId);

            await _jwtDll.SaveChangesAsync();

            return new RefreshTokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshTokenValue,
                AccessTokenExpiresAt= DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:AccessTokenMinutes"])),
                RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:RefreshTokenDays"]))
            };
        }

        public async Task<bool> Logout(
    RefreshTokenRequestDto dto)
        {

            var refreshToken = await _jwtDll
                .GetRefreshTokenAsync(dto.RefreshToken);

            if (refreshToken == null)
                return false;

            if (refreshToken.RevokedAt != null)
                return false;

            refreshToken.IsRevoked = true;
            refreshToken.RevokedAt = DateTime.UtcNow;

            
            await _jwtDll.UpdateRefreshToken(refreshToken);
            await _jwtDll.SaveChangesAsync();

            return true;
        }
    }
}
