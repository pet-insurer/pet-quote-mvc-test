using Microsoft.AspNetCore.Mvc;
using PetInsurance.Shared.Models;
using PetInsurance.Shared.Response;

namespace PetInsurance.Web.Controllers
{
    /// <summary>
    /// Handles requests related to insurance policies.
    /// </summary>
    public class PoliciesController : Controller
    {
        private readonly IHttpClientFactory _factory;
        private readonly ILogger<PoliciesController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PoliciesController"/> class.
        /// </summary>
        /// <param name="policyStore">The policy store service.</param>
        public PoliciesController(IHttpClientFactory factory, ILogger<PoliciesController> logger)
        {
            _factory = factory;
            _logger = logger;
        }

        /// <summary>
        /// Displays a list of all issued policies.
        /// </summary>
        /// <returns>A view displaying the list of policies.</returns>
        public async Task<IActionResult> Index()
        {
            List<Policy> policies = new();
            try
            {
                using var client = _factory.CreateClient("MyClient");

                var loginResponse = await client.PostAsJsonAsync("api/auth/login", new
                {
                    Username = "test",
                    Password = "pass"
                });

                if (!loginResponse.IsSuccessStatusCode)
                {
                    var errorContent = await loginResponse.Content.ReadAsStringAsync();
                    ViewBag.Error = $"Login failed: {errorContent}";
                    return View(policies);
                }

                var loginData = await loginResponse.Content.ReadFromJsonAsync<LoginTokenResponse>();
                if (loginData == null || string.IsNullOrWhiteSpace(loginData.Token))
                {
                    ViewBag.Error = "Token not received from login response";
                    return View(policies);
                }

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginData.Token);

                var policiesResponse = await client.GetAsync("api/policies");
                if (!policiesResponse.IsSuccessStatusCode)
                {
                    var errorContent = await policiesResponse.Content.ReadAsStringAsync();
                    ViewBag.Error = $"Failed to fetch policies: {errorContent}";
                    return View(policies);
                }

                policies = await policiesResponse.Content.ReadFromJsonAsync<List<Policy>>() ?? new List<Policy>();
                _logger.LogInformation("Retrieved {Count} policies", policies.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching policies");
                ViewBag.Error = $"An error occurred: {ex.Message}";
            }

            return View(policies);
        }
    }
}