using System.Data.SqlClient;
using System.Text.RegularExpressions;
using BookStore.Models.Models.User;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace OnlineBookstore.DL.Repositories.MsSQL
{
    public class UserInfoStore : IUserPasswordStore<UserInfo>
    {
        private readonly IConfiguration _configuration;

        public UserInfoStore(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult((user.UserId).ToString());
        }

        public Task<string?> GetUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(UserInfo user, string userName, CancellationToken cancellationToken)
        {

            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(UserInfo user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    await conn.ExecuteAsync(
                        $"INSERT INTO UserInfo (UserId,DisplayName,UserName,Email,Passowrd,CreatedDate)" +
                        $" VALUES(@UserId,@DisplayName,@UserName,@Email,@Passowrd,@CreatedDate)"
                        , user);
                    return await Task.FromResult(IdentityResult.Success);
                }
            }
            catch (Exception e)
            {
                return await Task.FromResult(IdentityResult.Failed(new IdentityError()
                {
                    Description = "Error in create user info"
                }));
            }
        }

        public Task<IdentityResult> UpdateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserInfo> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UserInfo> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM UserInfo WHERE UserName=@UserName",
                        new { UserName = normalizedUserName });
                    return result;
                };
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task SetPasswordHashAsync(UserInfo user, string passwordHash, CancellationToken cancellationToken)
        {
            //implement
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var userReturn = await conn.QueryFirstOrDefaultAsync<UserInfo>("SELECT * FROM UserInfo WHERE UserId=@UserId",
                        new { UserId = user.UserId });
                    await conn.ExecuteAsync("UPDATE UserInfo SET Password=@Password WHERE UserId=@UserId",
                        new { UserId = userReturn.UserId });

                }
            }
            catch (Exception e)
            {

            }
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(UserInfo user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
