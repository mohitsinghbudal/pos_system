using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using POS.DataLayer.Data;
using POS.DataLayer.Models;
using POS.Interface.DTO;
using POS.Interface.interfaces;

namespace POS.DataLayer.DataAccess.userDAL
{
    public class UserDll : IUserDll
    {
        private readonly POSDbContext _context;
        public UserDll(POSDbContext context)
        {
            _context = context;
        }
        public async Task<string?> test()
        {
            await Task.Delay(1000);
            return "test successful";
        }
        public async Task<bool> UserExists(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email);
        }
        public async Task<string> GetPassword(string email)
        {
            var password = await _context.Users
                .Where(u => u.Email == email)
                .Select(u => u.PasswordHash)
                .FirstOrDefaultAsync();
            return password;
        }

        
        public async Task<bool> SignUp( SignupDTO dto)
        {
            var Params = new[]{
                new SqlParameter("@Email", dto.Email),
                new SqlParameter("@Name", dto.Name),
                new SqlParameter("@Password", dto.Password),
                new SqlParameter("@PhoneNo", dto.PhoneNo)
                
            };


        var ResultList = await _context.Database
                    .SqlQueryRaw<int>("EXEC dbo.pos_signup @Email,@Name,@Password ,@PhoneNo", Params)
                    .ToListAsync();

        int newUserId = ResultList.FirstOrDefault();

        return newUserId > 0;

        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(
            string token)
        {
            return await _context.RefreshTokens
                .Include(rt => rt.User)
                .ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task AddRefreshTokenAsync(
            RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
        }

        public Task UpdateRefreshTokenAsync(
            RefreshToken refreshToken)
        {
            _context.RefreshTokens.Update(refreshToken);

            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}
