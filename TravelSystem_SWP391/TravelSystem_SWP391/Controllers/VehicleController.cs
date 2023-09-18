using Microsoft.AspNetCore.Mvc;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Controllers
{
    public class VehicleController : Controller
    {
        DAO dal = new DAO();
        public IActionResult ViewListVehicle()
        {
            List<Vehicle> listvehicle = dal.GetVehicle();
            ViewBag.ListVehicle = listvehicle;
            return View();
        }
    }
}
