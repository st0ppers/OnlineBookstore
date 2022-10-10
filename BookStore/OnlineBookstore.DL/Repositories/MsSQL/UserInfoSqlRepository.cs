using System.Data.SqlClient;
using BookStore.Models.Models.User;
using Dapper;
using Microsoft.Extensions.Configuration;
using OnlineBookstore.DL.Interface;

namespace OnlineBookstore.DL.Repositories.MsSQL
{
    public class UserInfoSqlRepository : IUserInfoRepository
    {
        private readonly IConfiguration _configuration;

        public UserInfoSqlRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<UserInfo?> GetUserInfoAsync(string email, string password)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstOrDefaultAsync<UserInfo?>("SELECT * FROM UserInfo WITH(NOLOCK) WHERE Email=@Email AND Password=@Password",
                        new { Email = email, Password = password });
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
