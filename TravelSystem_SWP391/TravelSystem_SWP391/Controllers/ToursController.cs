using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks.Dataflow;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;
using X.PagedList;

namespace TravelSystem_SWP391.Controllers
{
    public class ToursController : Controller
    {
        traveltestContext context = new traveltestContext();
        DAO dal = new DAO();
        public IActionResult tours()
        {
            String RoleID = HttpContext.Session.GetString("RoleID");
            ViewBag.RoleID = RoleID;
            List<Tour> listtours = dal.GetAllTours();
            ViewBag.search = null;
            ViewBag.ListTours = listtours;
            List<Country> listct = dal.GetAllCountry();
            ViewBag.ListCountry = listct;
            return View();
        }
        [HttpPost]
        public ActionResult SearchTours()
        {
            traveltestContext traveltestContext = new traveltestContext();
            string country = "";
            country = HttpContext.Request.Form["country"];
            var tourrs = (from p in traveltestContext.Tours
                          where p.Guide.Contains(country)
                          orderby p.Price, p.Duration descending
                          select new
                          {
                              Name = p.Name,
                              Duration = p.Duration,
                              Price = p.Price,
                              Image = p.Image,
                              Rating = p.Rating,
                          }).ToList();
            ViewBag.Name = country;
            ViewBag.search = tourrs;
            return View();
        }

        public IActionResult BookTours(int id)
        {
            Tour newTour = dal.GetTourById(id);
            List<Hotel> listHotelBooking = dal.GetListHotelByName(newTour.Name);
            List<Restaurant> listResBooking = dal.GetListRestaurantByName(newTour.Name);
            List<Vehicle> listVehBooking = dal.GetListVehicleForBooking();
            List<staff> listStaffBooking = dal.GetStaffsIsWorking();


            ViewBag.NewTour = newTour;
            ViewBag.ListHotelTour = listHotelBooking;
            ViewBag.ListRestaurantTour = listResBooking;
            ViewBag.ListVehicleTour = listVehBooking;
            ViewBag.ListStaffTour = listStaffBooking;
            return View();
        }

        [HttpPost]
        public IActionResult AddBooking(Booking inforBook)
        {
            Booking newBook = new Booking();
            newBook.Email = HttpContext.Session.GetString("Email");
            newBook.Name = HttpContext.Session.GetString("FirstName") + HttpContext.Session.GetString("LastName");
            newBook.Phone = HttpContext.Session.GetString("Phone");
            newBook.TourId = inforBook.TourId;
            newBook.HotelId = inforBook.HotelId;
            newBook.RestaurantId = inforBook.RestaurantId;
            newBook.VehicleId = inforBook.VehicleId;
            newBook.NumPeople = inforBook.NumPeople;
            newBook.Message = inforBook.Message;
            newBook.StartDate = inforBook.StartDate;
            newBook.EndDate = inforBook.EndDate;
            dal.AddBooking(newBook);
            return RedirectToAction("tours", "Tours");
        }



        public IActionResult ViewDetailsAccess(string name)
        {
            return RedirectToAction("ViewDetails", "Tours", new { name = name });
        }
        public IActionResult ViewDetails(string name)
        {
            String RoleID = HttpContext.Session.GetString("RoleID");
            ViewBag.RoleID = RoleID;
            ViewBag.Name = name;
            Tour t = context.Tours.FirstOrDefault(t => t.Name == name);
            ViewBag.tours = t;
            List<Schedule> c = context.Schedules.Include(t=>t.Tour).Where(t=>t.TourId == t.Tour.Id && t.Tour.Name == name).ToList();
            ViewBag.schedules = c;
            return View();
        }
        
    }
}