using Microsoft.AspNetCore.Mvc;

namespace TravelSystem_SWP391.Controllers
{
    public class VehicleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewListVehicle()
        {
            return View();
        }
    }
}
