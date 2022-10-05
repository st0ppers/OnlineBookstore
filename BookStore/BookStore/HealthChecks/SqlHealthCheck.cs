using System.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BookStore.HealthChecks
{
    public class SqlHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _configuration;

        public SqlHealthCheck(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    await conn.OpenAsync(cancellationToken);
                }
                catch (Exception e)
                {
                    return HealthCheckResult.Unhealthy(e.Message);
                }
            }
            return HealthCheckResult.Healthy("SQL Connection is OK");
        }
    }
}
