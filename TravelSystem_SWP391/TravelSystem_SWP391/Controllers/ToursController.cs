using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks.Dataflow;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Controllers
{
    public class ToursController : Controller
    {
        traveltestContext context = new traveltestContext();
        DAO dal = new DAO();
        public IActionResult tours()
        {
            List<Tour> listtours = dal.GetAllTours();
            ViewBag.search = null;
            ViewBag.ListTours = listtours;
            return View();
        }
        [HttpPost]
        public ActionResult SearchTours()
        {
            traveltestContext traveltestContext = new traveltestContext();
            string ToursName = "";
            ToursName = HttpContext.Request.Form["toursname"];
            var tourrs = (from p in traveltestContext.Tours
                          where p.Name.Contains(ToursName)
                          orderby p.Price, p.Duration descending
                          select new
                          {
                              Name = p.Name,
                              Duration = p.Duration,
                              Price = p.Price,
                              Image = p.Image,
                              Rating = p.Rating,
                          }).ToList();
            ViewBag.Name = ToursName;
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

            ViewBag.Name = name;

            Tour t = context.Tours.FirstOrDefault(t => t.Name == name);
            ViewBag.tours = t;
            return View();
        }
    }
}