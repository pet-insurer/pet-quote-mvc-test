using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetInsurance.API.Interface;
using PetInsurance.Shared.Models;

namespace PetInsurance.API.Controllers
{
    /// <summary>
    /// API endpoints for managing insurance policies.
    /// </summary>
    [ApiController]
    [Route("api")]
    [Authorize]
    public class PoliciesController : ControllerBase
    {
        private readonly IPolicyStoreService _policyStore;
        private readonly ILogger<PoliciesController> _logger;

        public PoliciesController(IPolicyStoreService policyStore, ILogger<PoliciesController> logger)
        {
            _policyStore = policyStore;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all issued policies.
        /// </summary>
        /// <returns>A list of issued policies.</returns>
        [HttpGet("policies")]
        public IActionResult GetAllPolicies()
        {
            try
            {
                var policies = _policyStore.GetAll();
                
                if (policies == null || !policies.Any())
                {
                    _logger.LogInformation("No policies found");
                    return Ok(new List<Policy>());
                }

                _logger.LogInformation("Retrieved {Count} policies", policies.Count);
                return Ok(policies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving policies");
                return StatusCode(500, new { message = "An error occurred while retrieving policies" });
            }
        }

        /// <summary>
        /// Saves a new policy to the store.
        /// </summary>
        /// <param name="policy">The policy to save.</param>
        [HttpPost("policy/create")]
        public IActionResult Create([FromBody] Policy policy)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid policy model state");
                    return BadRequest(ModelState);
                }

                if (policy == null || policy.Request == null)
                {
                    _logger.LogWarning("Policy request is null");
                    return BadRequest(new { message = "Policy request is required" });
                }

                _policyStore.Add(policy);
                _logger.LogInformation("Policy created successfully");
                return Ok(new { message = "Policy saved successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating policy");
                return StatusCode(500, new { message = "An error occurred while creating the policy" });
            }
        }
    }
}