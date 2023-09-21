using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
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
            ViewBag.search = null;
            ViewBag.ListVehicle = listvehicle;
            return View();
        }
        [HttpPost]
        public ActionResult SearchVehicle()
        {
            traveltestContext context = new traveltestContext();
            String NameVehicle = "";
            
            NameVehicle = HttpContext.Request.Form["namevehicle"];
            var data = (from p in context.Vehicles
                        where p.Name.Contains(NameVehicle)
                        orderby p.Price, p.Rate descending
                        select new
                        {
                            Name = p.Name,
                            Description= p.Description,
                            Price = p.Price,
                            Image = p.Image,
                            Rate = p.Rate,
                            Type = p.Type
                           
                        }).ToList();
            ViewBag.Name = NameVehicle;
            ViewBag.search = data;


            return View();
        }

        public IActionResult additem()
        {
            List<Vehicle> listvehicle = dal.GetVehicle();
            ViewBag.search = null;
            ViewBag.ListVehicle = listvehicle;
            return View();
        }
    }
}
