using Microsoft.AspNetCore.Mvc;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Controllers
{
    public class HotelController : Controller
    {
        traveltestContext context = new traveltestContext();
        DAO dal = new DAO();
        public IActionResult ViewListHotel()
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
            List<Hotel> listhotel = dal.GetListHotel();
            ViewBag.search = null;
            ViewBag.ListHotel = listhotel;
            return View();
        }
        [HttpPost]
        public ActionResult SearchHotel()
        {
            traveltestContext context = new traveltestContext();
            String NameHotel = "";

            NameHotel = HttpContext.Request.Form["namehotel"];
            var data = (from p in context.Hotels
                        where p.Name.Contains(NameHotel)
                        orderby p.Rating descending
                        select new
                        {
                            Name = p.Name,
                            City = p.City,
                            Address = p.Address,
                            Country = p.Country,
                            Image = p.Image,
                            Rating = p.Rating,
                            Phone = p.Phone,
                            Review = p.Review,
                            RoomTypes = p.RoomTypes,
                            Amenities = p.Amenities

                        }).ToList();
            ViewBag.Name = NameHotel;
            ViewBag.search = data;


            return View();
        }
        public IActionResult additemhotel()
        {
            List<Hotel> listhotel = dal.GetListHotel();
            ViewBag.search = null;
            ViewBag.ListHotel = listhotel;
            return View();
        }
        public IActionResult AddHotelAccess()
        {
            string NameHotel = "";
            NameHotel = HttpContext.Request.Form["name"];
            string Address = "";
            Address = HttpContext.Request.Form["address"];
            string City = "";
            City = HttpContext.Request.Form["city"];
            string Country = "";
            Country = HttpContext.Request.Form["country"];
            string Phone = "";
            Phone = HttpContext.Request.Form["phone"];
            string Rating = "";
            Rating = HttpContext.Request.Form["rating"];
            string IMGHotel = "";
            IMGHotel = HttpContext.Request.Form["file"];
            string Review = "";
            Review = HttpContext.Request.Form["review"];
            string RoomTypes = "";
            RoomTypes = HttpContext.Request.Form["roomtypes"];
            string Amenities = "";
            Amenities = HttpContext.Request.Form["amenities"];

            Hotel hotel = new Hotel()
            {
                Name = HttpContext.Request.Form["name"],
                Address = HttpContext.Request.Form["address"],
                City = HttpContext.Request.Form["city"],
                Country = HttpContext.Request.Form["country"],
                Phone = HttpContext.Request.Form["phone"],
                Rating = double.Parse(HttpContext.Request.Form["rating"]),
                Image = HttpContext.Request.Form["file"],
                Review = HttpContext.Request.Form["review"],
                RoomTypes = HttpContext.Request.Form["roomtypes"],
                Amenities = HttpContext.Request.Form["amenities"]
            };
            context.Add(hotel);
            context.SaveChanges();
            return RedirectToAction("ViewListHotel", "Hotel");
        }
        public IActionResult edithotelaccess(string name)
        {


            return RedirectToAction("edithotel", "Hotel", new { name = name });
        }
        public IActionResult edithotel(string name)
        {
            ViewBag.Name = name;
            Hotel h = context.Hotels.FirstOrDefault(v => v.Name == name);
            ViewBag.Hotel = h;
            return View();
        }
        public IActionResult edithotelrequest()
        {
            string NameHotel = "";
            NameHotel = HttpContext.Request.Form["name"];
            string AddressHotel = "";
            AddressHotel = HttpContext.Request.Form["address"];
            string City = "";
            City = HttpContext.Request.Form["city"];
            string Country = "";
            Country = HttpContext.Request.Form["country"];
            string Phone = "";
            Phone = HttpContext.Request.Form["phone"];
            string Rating = "";
            Rating = HttpContext.Request.Form["rating"];
            string Review = "";
            Review = HttpContext.Request.Form["review"];
            string RoomTypes = "";
            RoomTypes = HttpContext.Request.Form["roomtypes"];
            string Amenities = "";
            Amenities = HttpContext.Request.Form["amenities"];

            Hotel h = context.Hotels.FirstOrDefault(v => v.Name == NameHotel);
            if (dal.EditHotel(h, AddressHotel, City, Country, Phone, Rating, Review, RoomTypes, Amenities))
            {



                return RedirectToAction("ViewListHotel", "Hotel");
            }
            else
            {
                return RedirectToAction("edithotel", "Hotel", new { mess = 1 });
            }


        }
        public IActionResult deletehotel(int id)
        {

            Hotel r = context.Hotels.FirstOrDefault(v => v.Id == id);
            if (dal.DeleteHotel(r))
            {



                return RedirectToAction("ViewListHotel", "Hotel");
            }
            else
            {
                return RedirectToAction("ViewListHotel", "Hotel", new { mess = 1 });
            }
        }
    }
}
