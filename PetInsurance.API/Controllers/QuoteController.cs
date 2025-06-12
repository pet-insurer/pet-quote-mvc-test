using Microsoft.AspNetCore.Mvc;
using PetInsurance.API.Interface;
using PetInsurance.Shared.Request;

namespace PetInsurance.Web.Controllers.Api
{
    /// <summary>
    /// Provides endpoints for calculating insurance quotes.
    /// </summary>
    [ApiController]
    [Route("api")]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteCalculator _calculator;

        public QuoteController(IQuoteCalculator calculator)
        {
            _calculator = calculator;
        }

        /// <summary>
        /// Calculates the insurance premium based on the provided quote request.
        /// </summary>
        /// <param name="request">The quote premium request.</param>
        [HttpPost("quote")]
        public IActionResult Post([FromBody] QuotePremiumRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Breed))
                ModelState.AddModelError("Pet.Breed", "Pet breed is required.");

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            // Calculate the premium
            var premium = _calculator.CalculatePremium(request);

            // Return the calculated premium
            return Ok(new { premium });
        }
    }
}