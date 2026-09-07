using POS.Interface.DTO;
namespace POS.Interface.interfaces
{
    public interface IUserService
    {
        Task<string?> test();
        Task<bool> signUp(userSignupDTO dto);
    }
    public interface IUserDll
    {
        Task<string?> test();
        Task<bool> signUp(userSignupDTO dto);
        
    }



}
