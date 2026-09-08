using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using POS.Interface.DTO;
using POS.Interface.interfaces;
using POS.DataLayer.Data;

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
        public async Task<bool> signUp( SignupDTO dto)
        {
            var Params = new[]{
                new SqlParameter("@Email", dto.Email),
                new SqlParameter("@Name", dto.Name),
                new SqlParameter("@PhoneNo", dto.PhoneNo),
                new SqlParameter("@Password", dto.Password)
            };


        var ResultList = await _context.Database
                    .SqlQueryRaw<int>("EXEC dbo.pos_signup @Email,@Name, @Password, @PhoneNo", Params)
                    .ToListAsync();

        int newUserId = ResultList.FirstOrDefault();

        return newUserId > 0;

        }
        public async Task<bool> Login(LoginDTO dto)
        {
            return true;
        }
    }
}
