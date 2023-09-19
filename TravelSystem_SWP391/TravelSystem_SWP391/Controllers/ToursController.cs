using Microsoft.AspNetCore.Mvc;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Controllers
{
    public class ToursController : Controller
    {
        public IActionResult tours()
        {
            DAO dal = new DAO();
            List<Tour> listtours = dal.GetTours();
            ViewBag.ListTours = listtours;
            return View();
        }
    }
}