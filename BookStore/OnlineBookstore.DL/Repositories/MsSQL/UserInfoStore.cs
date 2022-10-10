using System.Data.SqlClient;
using BookStore.Models.Models.User;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace OnlineBookstore.DL.Repositories.MsSQL
{
    public class UserInfoStore : IUserPasswordStore<UserInfo>, IUserRoleStore<UserInfo>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserInfoStore> _logger;
        private readonly IPasswordHasher<UserInfo> _passwordHasher;
        public UserInfoStore(IConfiguration configuration, ILogger<UserInfoStore> logger, IPasswordHasher<UserInfo> passwordHasher)
        {
            _configuration = configuration;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public void Dispose()
        {

        }

        public Task<string?> GetUserIdAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserId.ToString());
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
            user.UserName = normalizedName;
            return Task.FromResult(user.UserName);
        }

        public async Task<IdentityResult> CreateAsync(UserInfo user, CancellationToken cancellationToken)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    user.Password = _passwordHasher.HashPassword(user, user.Password);
                    var result = await conn.ExecuteAsync(
                        $"INSERT INTO UserInfo (DisplayName,UserName,Email,Password,CreatedDate)" +
                        $" VALUES(@DisplayName,@UserName,@Email,@Password,@CreatedDate)"
                        , user);


                    //return result > 0 ? IdentityResult.Success : IdentityResult.Failed(result);
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
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync(cancellationToken);

                    var userReturn = await conn.ExecuteAsync("UPDATE UserInfo SET Password = @passwordHash WHERE UserId=@UserId",
                        new { user.UserId, passwordHash });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error {e.Message}");
            }
        }

        public async Task<string> GetPasswordHashAsync(UserInfo user, CancellationToken cancellationToken)
        {

            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync(cancellationToken);

                var userReturn = await conn.QueryFirstOrDefaultAsync<string>("SELECT Password FROM UserInfo WITH (NOLOCK)" +
                    "WHERE UserId=@UserId",
                    new { user.UserId });
                return userReturn;
            }
        }

        public async Task<bool> HasPasswordAsync(UserInfo user, CancellationToken cancellationToken)
        {
            return !string.IsNullOrEmpty(await GetPasswordHashAsync(user, cancellationToken));
        }




        public Task AddToRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(UserInfo user, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync();

                var result = await conn.QueryAsync<string>(
                    "SELECT r.RoleName as Name FROM Roles r WHERE r.Id IN (SELECT ur.Id FROM UserRoles ur WHERE ur.UserId = @UserId)", new { user.UserId });
                return result.ToList();
            }
        }

        public Task<bool> IsInRoleAsync(UserInfo user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserInfo>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
