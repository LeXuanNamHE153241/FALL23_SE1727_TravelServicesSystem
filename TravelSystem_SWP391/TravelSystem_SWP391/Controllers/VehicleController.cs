using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Controllers
{
    public class VehicleController : Controller
    {
        traveltestContext context = new traveltestContext();
        DAO dal = new DAO();
        public IActionResult ViewListVehicle( int mess)
        {
            String FirstName = HttpContext.Session.GetString("FirstName");

            String LastName = HttpContext.Session.GetString("LastName");

            String RoleID = HttpContext.Session.GetString("RoleID");

            String Phone = HttpContext.Session.GetString("Phone");

            String Image = HttpContext.Session.GetString("Image");
            ViewBag.FirstName = FirstName;
            ViewBag.LastName = LastName;
            ViewBag.RoleID = RoleID;
            ViewBag.Phone = Phone;
            ViewBag.Image = Image;

            List<Vehicle> listvehicle = dal.GetListVehicle();
            ViewBag.search = null;
            ViewBag.ListVehicle = listvehicle;
            if (mess == 1)
            {
                ViewBag.mess = "1";
            }
            
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
        public IActionResult editvehiclerequest()
        {
            string NameVehicle = "";
            NameVehicle = HttpContext.Request.Form["name"];
            string TypeVehicle = "";
            TypeVehicle = HttpContext.Request.Form["type"];
            string PriceVehicle = "";
            PriceVehicle = HttpContext.Request.Form["price"];
            
            string Rate = "";
            Rate = HttpContext.Request.Form["rate"];
            string Description = "";
            Description = HttpContext.Request.Form["description"];
            Vehicle v = context.Vehicles.FirstOrDefault(v => v.Name == NameVehicle);
            if ( dal.EditVehicle(v,TypeVehicle,PriceVehicle,Rate,Description))
            {



                return RedirectToAction("ViewListVehicle", "Vehicle");
            }
            else
            {
                return RedirectToAction("editvehicle", "Vehicle", new { mess = 1});
            }

            
        }
        public IActionResult deletevehicle(int id)
        {
            
            Vehicle v = context.Vehicles.FirstOrDefault(v => v.Id == id);
            if (dal.DeleteVehicle(v))
            {



                return RedirectToAction("ViewListVehicle", "Vehicle");
            }
            else
            {
                return RedirectToAction("ViewListVehicle", "Vehicle", new { mess = 1 });
            }
        }
        public ActionResult BookVehicle(int id)
        {
            traveltestContext context = new traveltestContext();
            
            String Email = HttpContext.Session.GetString("username");
            
            User u = new User();
            u = dal.getUser(Email);
            Vehicle v = new Vehicle();
            v = dal.getVihecle(id);
            List<staff> staff = dal.GetListStaffByRole("Driver Staff");
            ViewBag.UserBook = u;
            ViewBag.ListStaff = staff;
            ViewBag.Vehicle = v;



            return View();
        }
        public ActionResult BookVehicleAccess()
        {
            traveltestContext context = new traveltestContext();
            String Email = HttpContext.Request.Form["Email"];

            String NameUser = HttpContext.Request.Form["NameUser"];

            String Phone = HttpContext.Request.Form["Phone"];

            String NameVehicle = HttpContext.Request.Form["NameVehicle"];

            String IdVehicle = HttpContext.Request.Form["IdVehicle"];
            String RoleID = HttpContext.Session.GetString("RoleID");
            Booking booking = new Booking()
            {
                Name = NameUser,

                Email = Email,

                Phone = Phone,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                NumPeople= 5,
                Message = "no",
                VehicleId = int.Parse(IdVehicle)
            };
            context.Add(booking);
            context.SaveChanges();

            if (RoleID == "1")
            {
                return RedirectToAction("ViewListVehicle", "Vehicle", new {mess = 1 });
            }
            else
            {
                return RedirectToAction("ViewListBooking", "Booking");
            } 
                
            
           


            
        }


    }
}
