using Microsoft.AspNetCore.Mvc;
using PetInsurance.Shared;
using PetInsurance.Shared.Models;
using PetInsurance.Shared.Request;
using PetInsurance.Shared.Response;

namespace PetInsurance.Web.Controllers
{
    /// <summary>
    /// Handles requests related to insurance quotes.
    /// </summary>
    public class QuoteController : Controller
    {
        private readonly IHttpClientFactory _factory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<QuoteController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuoteController"/> class.
        /// </summary>
        public QuoteController(IHttpClientFactory factory, IConfiguration configuration, ILogger<QuoteController> logger)
        {
            _factory = factory;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Displays the quote request form.
        /// </summary>
        /// <returns>A view displaying the quote request form.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            var apiUrl = _configuration["ApiSettings:PublicBaseUrl"];
            if (string.IsNullOrEmpty(apiUrl))
            {
                apiUrl = "http://localhost:5000";
            }

            ViewBag.ApiUrl = apiUrl;
            return View(new QuoteRequest
            {
                Pet = new Pet() { Breed = "Other" },
                Owner = new Owner(),
                CoverLevel = "Standard"
            });
        }

        /// <summary>
        /// Processes the submitted quote request and calculates the premium.
        /// </summary>
        /// <param name="request">The quote request.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(QuoteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", request);
            }

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
                    return View("Index", request);
                }

                var loginData = await loginResponse.Content.ReadFromJsonAsync<LoginTokenResponse>();
                if (loginData == null || string.IsNullOrWhiteSpace(loginData.Token))
                {
                    ViewBag.Error = "Token not received from login response";
                    return View("Index", request);
                }

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginData.Token);

                var premiumResponse = await client.PostAsJsonAsync("api/quote", new
                {
                    Age = request.Pet.Age,
                    Breed = request.Pet.Breed,
                    CoverLevel = request.CoverLevel
                });

                if (!premiumResponse.IsSuccessStatusCode)
                {
                    var errorContent = await premiumResponse.Content.ReadAsStringAsync();
                    ViewBag.Error = $"Failed to get quote: {errorContent}";
                    return View("Index", request);
                }

                var premium = await premiumResponse.Content.ReadFromJsonAsync<QuoteResponse>();
                if (premium == null)
                {
                    ViewBag.Error = "Failed to parse quote response";
                    return View("Index", request);
                }

                var policy = new Policy
                {
                    Request = new QuoteRequest
                    {
                        Pet = new Pet
                        {
                            Name = request.Pet.Name,
                            Breed = request.Pet.Breed,
                            Age = request.Pet.Age
                        },
                        Owner = new Owner
                        {
                            FirstName = request.Owner.FirstName,
                            LastName = request.Owner.LastName
                        },
                        CoverLevel = request.CoverLevel
                    },
                    Premium = premium.Premium
                };

                var createPolicyResponse = await client.PostAsJsonAsync("api/policy/create", policy);
                if (!createPolicyResponse.IsSuccessStatusCode)
                {
                    var errorContent = await createPolicyResponse.Content.ReadAsStringAsync();
                    ViewBag.Error = $"Failed to create policy: {errorContent}";
                    return View("Index", request);
                }

                _logger.LogInformation("Policy created successfully for pet: {PetName}", request.Pet.Name);
                return RedirectToAction("Index", "Policies");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during policy submission");
                ViewBag.Error = $"An error occurred: {ex.Message}";
                return View("Index", request);
            }
        }
    }
}