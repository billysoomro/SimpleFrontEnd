using Microsoft.AspNetCore.Mvc;
using SimpleFrontEnd.Models;
using System.Text.Json;

namespace SimpleFrontEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AWSRegionAndAZController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public AWSRegionAndAZController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("SimpleCrudApiClient");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _httpClient.GetAsync("/api/AWSRegionAndAZ");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var awsRegionAndAZInfo = JsonSerializer.Deserialize<AWSRegionAndAZInfo>(content);

                return Ok(awsRegionAndAZInfo);
            }

            return BadRequest();
        }
    }
}
