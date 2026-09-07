using POS.Interface.DTO;
using POS.Interface.interfaces;
namespace POS.Service.userServices
{
    public class UserServices : IUserService
    {
        private readonly IUserDll _userDll;

        public UserServices(IUserDll userDll)
        {
            _userDll = userDll ?? throw new System.ArgumentNullException(nameof(userDll));
        }

        public async Task<string?> test()
        {
            return await _userDll.test();
        }
        public async Task<bool> signUp(userSignupDTO dto)
        {
            return await _userDll.signUp(dto);
        }

    }
}
