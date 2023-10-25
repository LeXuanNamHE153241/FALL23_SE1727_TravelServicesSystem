using Microsoft.AspNetCore.Mvc;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Controllers
{
    public class RestaurantController : Controller
    {
        traveltestContext context = new traveltestContext();
        DAO dal = new DAO();
        public IActionResult ViewListRes()
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
            List<Restaurant> listres = dal.GetListRes();
            ViewBag.search = null;
            ViewBag.ListRestaurant = listres;
            return View();
        }
        [HttpPost]
        public ActionResult SearchRestaurant()
        {
            traveltestContext context = new traveltestContext();
            String NameRestaurant = "";

            NameRestaurant = HttpContext.Request.Form["namerestaurant"];
            var data = (from p in context.Restaurants
                        where p.Name.Contains(NameRestaurant)
                        orderby p.Price, p.Rate descending
                        select new
                        {
                            Name = p.Name,
                            Description = p.Description,
                            Price = p.Price,
                            Image = p.Image,
                            Rate = p.Rate,
                            Address = p.Address,
                            Phone = p.Phone,

                        }).ToList();
            ViewBag.Name = NameRestaurant;
            ViewBag.search = data;


            return View();
        }
        public IActionResult additemres()
        {
            List<Restaurant> listrestaurant = dal.GetListRes();
            ViewBag.search = null;
            ViewBag.ListVehicle = listrestaurant;
            return View();
        }
        public IActionResult AddRestaurantAccess()
        {
            string NameRestaurant = "";
            NameRestaurant = HttpContext.Request.Form["name"];
            string Address = "";
            Address = HttpContext.Request.Form["address"];
            string Phone = "";
            Phone = HttpContext.Request.Form["phone"];
            string PriceVehicle = "";
            PriceVehicle = HttpContext.Request.Form["price"];
            string IMGVehicle = "";
            IMGVehicle = HttpContext.Request.Form["file"];
            string Rate = "";
            Rate = HttpContext.Request.Form["rate"];
            string Description = "";
            Description = HttpContext.Request.Form["description"];

            Restaurant restaurant = new Restaurant()
            {
                Name = HttpContext.Request.Form["name"],
                Address = HttpContext.Request.Form["address"],
                Phone = HttpContext.Request.Form["phone"],
                Price = HttpContext.Request.Form["price"],
                Image = HttpContext.Request.Form["file"],
                Rate = int.Parse(HttpContext.Request.Form["rate"]),

                Description = HttpContext.Request.Form["description"]
            };
            context.Add(restaurant);
            context.SaveChanges();
            return RedirectToAction("ViewListRes", "Restaurant");







        }


        public IActionResult editrestaurantaccess(string name)
        {


            return RedirectToAction("editrestaurant", "Restaurant", new { name = name });
        }
        public IActionResult editrestaurant(string name)
        {

            ViewBag.Name = name;

            Restaurant r = context.Restaurants.FirstOrDefault(v => v.Name == name);
            ViewBag.Restaurant = r;
            return View();
        }
        public IActionResult editrestaurantrequest()
        {
            string NameRestaurant = "";
            NameRestaurant = HttpContext.Request.Form["name"];
            string AddressVehicle = "";
            AddressVehicle = HttpContext.Request.Form["address"];
            string PriceVehicle = "";
            PriceVehicle = HttpContext.Request.Form["price"];

            string Rate = "";
            Rate = HttpContext.Request.Form["rate"];

            string Phone = "";
            Phone = HttpContext.Request.Form["phone"];
            string Description = "";
            Description = HttpContext.Request.Form["description"];
            Restaurant r = context.Restaurants.FirstOrDefault(v => v.Name == NameRestaurant);
            if (dal.EditRestaurant(r, AddressVehicle, Phone, PriceVehicle, Rate, Description))
            {



                return RedirectToAction("ViewListRes", "Restaurant");
            }
            else
            {
                return RedirectToAction("editrestaurant", "Restaurant", new { mess = 1 });
            }


        }

        public IActionResult deleterestaurant(int id)
        {

            Restaurant r = context.Restaurants.FirstOrDefault(v => v.Id == id);
            if (dal.DeleteRestaurant(r))
            {



                return RedirectToAction("ViewListRes", "Restaurant");
            }
            else
            {
                return RedirectToAction("ViewListRes", "Restaurant", new { mess = 1 });
            }
        }

    }
}
