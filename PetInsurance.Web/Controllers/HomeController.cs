using Microsoft.AspNetCore.Mvc;

namespace PetInsurance.Web.Controllers
{
    /// <summary>
    /// Handles requests to the home page.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Displays the home page.
        /// </summary>
        /// <returns>The home page view.</returns>
        public IActionResult Index()
        {
            // Return the view associated with this action
            return View();
        }
    }
}
