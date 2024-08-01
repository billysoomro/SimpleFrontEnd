using Microsoft.AspNetCore.Mvc;
using SimpleFrontEnd.Utilities;

namespace SimpleCrudApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AZController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                if (EnvironmentChecker.IsRunningInLambda())
                {
                    return Ok("This functionality isn't available while running in Lambda.");
                }

                var availabilityZone = await InstanceMetadataRetriever.GetAvailabilityZoneAsync();

                return Ok(availabilityZone);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
