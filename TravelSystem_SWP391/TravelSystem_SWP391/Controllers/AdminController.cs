using Microsoft.AspNetCore.Mvc;

namespace TravelSystem_SWP391.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Manager()
        {
            return View();
        }
    }
}
