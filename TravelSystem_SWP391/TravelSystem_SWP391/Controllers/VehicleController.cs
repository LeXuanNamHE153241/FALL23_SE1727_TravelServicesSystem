using Microsoft.AspNetCore.Mvc;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Controllers
{
    public class VehicleController : Controller
    {
        traveltestContext context = new traveltestContext();
        DAO dal = new DAO();
        public IActionResult ViewListVehicle()
        {
            List<Vehicle> listvehicle = dal.GetListVehicle();
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
            List<Vehicle> listvehicle = dal.GetListVehicle();
            ViewBag.search = null;
            ViewBag.ListVehicle = listvehicle;
            return View();
        }
        public IActionResult AddVehicleAccess()
        {
            string NameVehicle = "";
            NameVehicle = HttpContext.Request.Form["name"];
            string TypeVehicle = "";
            TypeVehicle = HttpContext.Request.Form["type"];
            string PriceVehicle = "";
            PriceVehicle = HttpContext.Request.Form["price"];
            string IMGVehicle = "";
            IMGVehicle = HttpContext.Request.Form["file"];
            string Rate = "";
            Rate = HttpContext.Request.Form["rate"];
            string Description = "";
            Description = HttpContext.Request.Form["description"];
            
                Vehicle vehicle = new Vehicle()
                {
                    Name = HttpContext.Request.Form["name"],
                    Type = HttpContext.Request.Form["type"],
                    Price = double.Parse(HttpContext.Request.Form["price"]) ,
                    Image = HttpContext.Request.Form["file"],
                    Rate = int.Parse(HttpContext.Request.Form["rate"]),
                    
                    Description = HttpContext.Request.Form["description"]
                };
                context.Add(vehicle);
                context.SaveChanges();
                return RedirectToAction("ViewListVehicle", "Vehicle"); 







        }
        public IActionResult editvehicleaccess(string name)
        {

            
            return RedirectToAction("editvehicle", "Vehicle", new { name = name });
        }
        public IActionResult editvehicle(string name)
        {
            
            ViewBag.Name = name;
            
            Vehicle v = context.Vehicles.FirstOrDefault(v => v.Name == name);
            ViewBag.vehicle = v;
            return View();
        }


    }
}
