using System.Data.SqlClient;
using BookStore.Models.Models.User;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OnlineBookstore.DL.Repositories.MsSQL;

namespace OnlineBookstore.DL.Repositories.InMemoryRepositories
{
    public class UserRoleStore : IRoleStore<UserRole>
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserRoleStore> _logger;
        public UserRoleStore(IConfiguration configuration, ILogger<UserRoleStore> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public void Dispose()
        {

        }

        public async Task<IdentityResult> CreateAsync(UserRole role, CancellationToken cancellationToken)
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);

                    var query = @"INSERT INTO UserRoles ([Id], [RoleId],[UserId]) VALUES (@Id,@RoleId,@UserId) ";
                    var result = await conn.ExecuteAsync(query, role);
                    return IdentityResult.Success;
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error message: {e.Message}  Error in:{nameof(UserInfoStore.CreateAsync)}");
                    return IdentityResult.Failed();
                }
            }
        }

        public Task<IdentityResult> UpdateAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(UserRole role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(UserRole role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<UserRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<UserRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {

            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);
                    var result = await conn.QueryFirstOrDefaultAsync<UserRole>("" +
                    "SELECT ur.Id,ur.RoleId,ur.UserId,r.RoleName AS Name FROM UserRoles ur WITH(NOLOCK) INNER JOIN Roles r  ON RoleId = r.Id " +
                    "WHERE r.RoleName =@normalizedRoleName",
                        new { Name = @normalizedRoleName });
                    return result;
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error message: {e.Message} \n Error in:{nameof(UserInfoStore.FindByNameAsync)}");
                    return null;
                }
            }
        }
    }
}
