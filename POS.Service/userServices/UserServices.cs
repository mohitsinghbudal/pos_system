using POS.Interface.DTO;
using POS.Interface.interfaces;
using BCrypt.Net;
namespace POS.Service.userServices
#nullable disable 
{
    public class UserServices : IUserService
    {
        private readonly IUserDll _userDll;

        public UserServices(IUserDll userDll)
        {
            _userDll = userDll ;
        }

        public async Task<string?> test()
        {
            return await _userDll.test();
        }
        public async Task<bool> SignUp(SignupDTO dto)
        {
            var check = await _userDll.UserExists(dto.Email);

            if (check) throw new Exception("User already exists");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password,workFactor:12);

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

            var get_password = await _userDll.GetPassword(dto.Email);
            
            if(get_password == null) throw new Exception("User does not exist");

            var pw_check = BCrypt.Net.BCrypt.Verify(dto.Password, get_password);

            if(!pw_check) throw new Exception("Invalid email or password");

            var accessToken = GenerateJwt(user);

            var refreshToken = Guid.NewGuid().ToString();

            _context.RefreshTokens.Add(new RefreshToken
            {
                accessToken = refreshToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(30),
                IsRevoked = false
            });
            await _context.SaveChangesAsync();
        }
    }
}
