using Microsoft.AspNetCore.Mvc;

namespace SimpleFrontEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public ConfigurationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("SimpleCrudApiClient");
        }

        [HttpGet("{durationInMinutes}")]
        public async Task<IActionResult> Get(int durationInMinutes)
        
        {
            var response = await _httpClient.GetAsync($"/api/Configuration/{durationInMinutes}");

            if (response.IsSuccessStatusCode)
            {
                return Ok($"CPU stress test for {durationInMinutes} minute(s) complete.");
            }

            return BadRequest();            
        }
    }
}
