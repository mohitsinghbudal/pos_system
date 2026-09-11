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

        public async Task<RefreshTokenResponseDto?> Refresh(
    RefreshTokenRequestDto dto)
        {
            var oldToken = await _jwtDll.GetRefreshTokenAsync(dto.RefreshToken);

            if (oldToken == null)
                throw new Exception("Invalid or expired refresh token");

            var user = await _userDll.GetUserByIdAsync(oldToken.UserId);

            if (user == null)
                throw new Exception("User not found");

            if (!user.IsActive)
                throw new Exception("User is not active");

            var now = DateTime.UtcNow;

            var accessToken = _jwt.GenerateAccessToken(user);

            var newRefreshTokenValue = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));

            oldToken.IsRevoked = true;
            oldToken.RevokedAt = now;
            oldToken.ReplacedByToken = newRefreshTokenValue;

            await _jwtDll.UpdateRefreshToken(oldToken);

            await _jwtDll.AddRefreshTokenAsync(
                newRefreshTokenValue,
                oldToken.UserId);

            await _jwtDll.SaveChangesAsync();

            return new RefreshTokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshTokenValue,
                AccessTokenExpiresAt = now.AddMinutes(
                    int.Parse(_config["Jwt:AccessTokenMinutes"]!)),
                RefreshTokenExpiresAt = now.AddDays(
                    int.Parse(_config["Jwt:RefreshTokenDays"]!))
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
