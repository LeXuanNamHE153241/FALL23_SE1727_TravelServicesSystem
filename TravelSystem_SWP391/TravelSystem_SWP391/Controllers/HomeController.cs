using Microsoft.AspNetCore.Mvc;

namespace TravelSystem_SWP391.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}
