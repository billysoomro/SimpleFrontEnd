using Microsoft.AspNetCore.Mvc;

namespace SimpleFrontEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CanaryController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("The SimpleFrontEnd is running.");
        }
    }
}
