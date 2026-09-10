using POS.Interface.DTO;

namespace POS.Interface.interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(UserDTO dto);
    }
    public interface IJwtDll
    {
        Task<RefreshTokenDto?> GetRefreshTokenAsync(string token);

        Task<bool> AddRefreshTokenAsync(string refreshToken, int userId);

        Task<bool> UpdateRefreshToken(RefreshTokenDto refreshToken);

        Task SaveChangesAsync();
    }
}
