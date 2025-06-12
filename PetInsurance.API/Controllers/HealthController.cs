using Microsoft.AspNetCore.Mvc;

namespace PetInsurance.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Health check endpoint to verify API is running.
        /// </summary>
        /// <returns>HTTP 200 with status message.</returns>
        [HttpGet("health")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetHealthStatus()
        {
            return Ok(new { status = "OK" });
        }
    }
}