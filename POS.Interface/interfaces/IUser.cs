using POS.Interface.DTO;
namespace POS.Interface.interfaces
{
    public interface IUserService
    {
        Task<string?> test();
        Task<bool> SignUp(SignupDTO dto);
        Task<LoginResDTO?> Login(LoginReqDTO dto);
        Task<RefreshTokenResponseDto?> Refresh(RefreshTokenRequestDto dto);
        Task<bool> Logout(RefreshTokenRequestDto dto);

    }
    public interface IUserDll
    {
        Task<string?> test();
        Task<bool> SignUp(SignupDTO dto);
        Task<bool> UserExists(string email);
        Task<UserDTO?> GetUserAsync(string email);
        Task<UserDTO?> GetUserByIdAsync(int id);
       

   

        
    }



}
