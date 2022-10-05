namespace BookStore.Models.Models.Healthchecks
{
    public class HealthCheckResponse
    { 
        public string Status { get;init; }
        public IEnumerable<IndividualHealthCheckResponses> HealhtChecks { get; init; }
        public TimeSpan HealthCheckDuration { get; init; }
    }
}
