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
            await Task.Delay(1);
            return "test successful";
        }
        public async Task<bool> UserExists(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email);
        }
        public async Task<UserDTO> GetUserAsync(string email)
        {
            var user = await _context.Users
                .Where(u => u.Email == email)
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    PasswordHash = u.PasswordHash,
                    PhoneNo = u.PhoneNo,
                    CreatedAt = u.CreatedAt,
                    IsActive = u.IsActive,
                    RoleId = u.RoleId
                })
                .FirstOrDefaultAsync();
            return user;
        }
        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
                .Where(u => u.Id == id)
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    PhoneNo = u.PhoneNo,
                    CreatedAt = u.CreatedAt,
                    IsActive = u.IsActive,
                    RoleId = u.RoleId
                })
                .FirstOrDefaultAsync();
            return user;

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

            if (newUserId == -1)
            {
                throw new InvalidOperationException("Email already registered");
            }

            return newUserId > 0;

        }


    }
}
