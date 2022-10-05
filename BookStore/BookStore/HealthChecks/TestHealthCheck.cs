using System.Linq.Expressions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BookStore.HealthChecks
{
    public class TestHealthCheck : IHealthCheck
    {

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {

            }
            catch (Exception e)
            {
                return HealthCheckResult.Unhealthy($"Test dead : {e.Message}");
            }
            return HealthCheckResult.Healthy("Test is healthy");
        }
    }
}
