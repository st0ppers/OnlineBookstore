using System.Data.SqlClient;
using BookStore.Models.Models.User;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OnlineBookstore.DL.Interface;

namespace OnlineBookstore.DL.Repositories.MsSQL
{
    public class EmployeeSqlRepository : IEmployeeRepository
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<EmployeeSqlRepository> _logger;

        public EmployeeSqlRepository(IConfiguration configuration, ILogger<EmployeeSqlRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IEnumerable<Employee?>> GetEmployeeDetails()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM Employee WITH(NOLOCK)";
                    await conn.OpenAsync();
                    return  await conn.QueryAsync<Employee>(query);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetEmployeeDetails)}:{e.Message}");
            }
            return Enumerable.Empty<Employee>();
        }

        public async Task<Employee?> GetEmployeeDetails(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Employee>("SELECT * FROM Employee WITH (NOLOCK) WHERE [Id] = @Id", new { Id = id });
                }

            }
            catch (Exception e)
            {
                _logger.LogError($" {e.Message}");
                return null;
            }

        }

        public async Task AddEmployee(Employee employee)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    string query =
                        "INSERT INTO [Employee] (EmployeeID, NationalIDNumber, EmployeeName, LoginID, JobTitle, BirthDate, MaritalStatus, Gender, HireDate, VacationHours, SickLeaveHours, rowguid, ModifiedDate) VALUES(@EmployeeID, @NationalIDNumber, @EmployeeName, @LoginID, @JobTitle, @BirthDate, @MaritalStatus, @Gender, @HireDate, @VacationHours, @SickLeaveHours, @rowguid, @ModifiedDate)";
                    await conn.OpenAsync();
                    var result =await conn.ExecuteAsync(query, employee);

                }
            }
            catch (Exception e)
            {
                _logger.LogError($" {e.Message}");
                throw;
            }
        }

        public async Task UpdateEmployee(Employee employee)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.ExecuteAsync($"UPDATE Employee SET (NationalIDNumber=@NationalIDNumber,EmployeeName=@EmployeeName,LoginID=@LoginID,JobTitle=@JobTitle,BirthDate=@BirthDate,MaritalStatus=@MaritalStatus,Gender=@Gender,HireDate=@HireDate,VacationHours=@VacationHours,SickLeaveHours=@SickLeaveHours,rowguid=@rowguid,ModifiedDate=@ModifiedDate) WHERE Id=@Id"
                        ,
                        employee);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($" {e.Message}");
                throw;
            }
        }

        public async Task DeleteEmployee(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await GetEmployeeDetails(id);

                    (await conn.QueryAsync<Employee>($"DELETE FROM Employee WHERE EmployeeId=@EmployeeId",
                        new { EmployeeId = id })).SingleOrDefault();

                }
            }
            catch (Exception e)
            {
                _logger.LogError($" {e.Message}");
                throw;
            }
        }

        public async Task<bool> CheckEmployee(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    await conn.QueryFirstOrDefaultAsync<Employee>("SELECT * FROM Employee WITH (NOLOCK) WHERE [Id] = @Id", new { Id = id });
                    return true;
                }

            }
            catch (Exception e)
            {
                _logger.LogError($" {e.Message}");
                return false;
            }

        }
    }
}
