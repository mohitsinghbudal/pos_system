using POS.Interface.DTO;
namespace POS.Interface.interfaces
{
    public interface IUserService
    {
        Task<string?> test();
        Task<bool> signUp(SignupDTO dto);
        Task<bool> Login(LoginDTO dto);

    }
    public interface IUserDll
    {
        Task<string?> test();
        Task<bool> signUp(SignupDTO dto);
        Task<bool> Login(LoginDTO dto);
    }



}
