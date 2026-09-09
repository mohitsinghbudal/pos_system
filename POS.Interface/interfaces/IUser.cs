using POS.Interface.DTO;
namespace POS.Interface.interfaces
{
    public interface IUserService
    {
        Task<string?> test();
        Task<bool> SignUp(SignupDTO dto);
        Task<bool> Login(LoginDTO dto);

    }
    public interface IUserDll
    {
        Task<string?> test();
        Task<bool> SignUp(SignupDTO dto);
        Task<bool> UserExists(string email);
        Task<string> GetPassword(string email);
    }



}
