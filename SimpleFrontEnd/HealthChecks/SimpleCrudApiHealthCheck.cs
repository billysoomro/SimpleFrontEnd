using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SimpleFrontEnd.HealthChecks
{
    public class SimpleCrudApiHealthCheck : IHealthCheck
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public SimpleCrudApiHealthCheck(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("SimpleCrudApiClient");
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        
        {
            var response = await _httpClient.GetAsync("/api/Canary", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return new HealthCheckResult(HealthStatus.Healthy, "Able to communicate to the SimpleCrudApi");                
            }

            return new HealthCheckResult(HealthStatus.Unhealthy, "Unable to communicate to the SimpleCrudApi");
        }
    }
}
