using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using POS.Interface.DTO;
using POS.Interface.interfaces;

namespace POS.DataLayer.DataAccess.userDAL
{
    public class UserDll : IUserDll
    {
        public async Task<string?> test()
        {
            await Task.Delay(1000);
            return "test successful";
        }
        public async Task<bool> signUp( SignupDTO dto)
        {
            var Params = new SqlParameter("@email", dto.Email);

            var result = await DbContext.Database
                .SqlQueryRaw<bool>("EXECUTE dbo.SignUp @email", Params) ;
            return result;
        }
        public async Task<bool> Login(LoginDTO dto)
        {
           
        }
    }
}
