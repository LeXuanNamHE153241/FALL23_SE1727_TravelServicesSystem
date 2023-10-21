using Microsoft.AspNetCore.Mvc;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Controllers
{
    public class BookingController : Controller
    {
        traveltestContext context = new traveltestContext();
        DAO dal = new DAO();
        public IActionResult ViewListBooking()
        {
            List<Booking> listbooking = dal.GetListBooking();
            ViewBag.Booking = listbooking;
            return View();
        }
        public IActionResult ViewListBookingVehicleInTourist()
        {
            String FirstName = HttpContext.Session.GetString("username");
            List<Booking> listbooking = dal.GetListBookingByEmail(FirstName);
            ViewBag.Booking = listbooking;
            return View();
        }
    }
}
