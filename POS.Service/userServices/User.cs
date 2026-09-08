using POS.Interface.DTO;
using POS.Interface.interfaces;
namespace POS.Service.userServices
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
        public async Task<bool> signUp(SignupDTO dto)
        {
            return await _userDll.signUp(dto);
        }
        public async Task<bool> Login(LoginDTO dto)
        {
            return await _userDll.Login(dto);
        }
    }
}
