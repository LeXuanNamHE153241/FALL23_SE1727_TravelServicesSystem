using Microsoft.AspNetCore.Mvc;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;
using X.PagedList;

namespace TravelSystem_SWP391.Controllers
{
    public class HotelController : Controller
    {
        traveltestContext context = new traveltestContext();
        DAO dal = new DAO();
        public IActionResult ViewListHotel(int page)
        {
            page = page < 1 ? 1 : page;
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
            int pagesize = 4;
            List<Hotel> listhotel = dal.GetListHotel();
            var bookinghotel = context.Hotels.ToPagedList(page, pagesize);
            ViewBag.search = null;
            ViewBag.ListHotel = bookinghotel;
            return View(bookinghotel);
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
        public ActionResult BookHotel(int id)
        {
            traveltestContext context = new traveltestContext();

            String Email = HttpContext.Session.GetString("username");

            User u = new User();
            u = dal.getUser(Email);
            Hotel h = new Hotel();
            h = dal.getHotel(id);
            List<staff> staff = dal.GetListStaffByRole("Hotel Staff");
            ViewBag.UserBook = u;
            ViewBag.ListStaff = staff;
            ViewBag.Hotel = h;

            String statuslogin = HttpContext.Session.GetString("FirstName");
            if (statuslogin == null)
            {
                return RedirectToAction("index", "Home");
            }

            return View();
        }
        public ActionResult BookHotelAccess()
        {
            traveltestContext context = new traveltestContext();
            String Email = HttpContext.Request.Form["Email"];

            String NameUser = HttpContext.Request.Form["NameUser"];

            String Phone = HttpContext.Request.Form["Phone"];

            String NameHotel = HttpContext.Request.Form["NameHotel"];
            String startdate = HttpContext.Request.Form["stdate"];
            String enddate = HttpContext.Request.Form["edate"];
            String IdHotel = HttpContext.Request.Form["IdHotel"];
            String RoleID = HttpContext.Session.GetString("RoleID");
            Booking booking = new Booking()
            {
                Name = NameUser,

                Email = Email,

                Phone = Phone,
                StartDate = DateTime.Parse(startdate),
                EndDate = DateTime.Parse(enddate),
                NumPeople = 5,
                Message = "",
                HotelId = int.Parse(IdHotel)
            };
            context.Add(booking);
            context.SaveChanges();

            if (RoleID == "1")
            {
                return RedirectToAction("ViewListBookingVehicleInTourist", "Booking", new { mess = 1 });
            }
            else
            {
                return RedirectToAction("ViewListBooking", "Booking");
            }






        }
    }
}
