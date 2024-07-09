using Microsoft.AspNetCore.Mvc;

namespace WebClient.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[Controller]/[Action]")]
    public class HomeController : Controller
    {
        // Dependency injection of services
        private readonly ILogger<HomeController> _logger;


        public HomeController( ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("Errors/Error404");
            }
            return View();
        }
    }
}