using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Controllers
{
    public class AdminController : Controller
    {
        AdminDAO adminDAO = new AdminDAO();

        // Get
        public IActionResult ViewListUser()
        {
            List<User> listUser = adminDAO.GetListUser();
            ViewBag.search = null;
            ViewBag.ListUser = listUser;
            return View();
        }
        public IActionResult ViewListFeedback()
        {
            List<Feedback> listFeedback = adminDAO.GetListFeedBack();
            ViewBag.search = null;
            ViewBag.ListFeedback = listFeedback;
            return View();
        }
        public IActionResult ViewListTour()
        {
            List<Tour> listTour = adminDAO.GetListTour();
            ViewBag.search = null;
            ViewBag.ListTour = listTour;
            return View();
        }
        public IActionResult CreateTour()
        {
            ViewBag.ListTour = adminDAO.GetListTour();
            ViewBag.ListVehicle = adminDAO.GetListVehicle();
            ViewBag.ListRestaurant = adminDAO.GetListRestaurant();
            ViewBag.ListStaff = adminDAO.GetListStaff();
            return View();
        }
        public IActionResult EditTour(int tourId)
        {
            Tour tour = adminDAO.GetTourById(tourId);
            if (tour == null)
            {
                return RedirectToAction("EditTour", "Admin", new { mess = 1 });
            }

            ViewBag.TourId = tourId;
            ViewBag.Tour = adminDAO.GetTourById(tourId);
            ViewBag.ListTour = adminDAO.GetListTour();
            ViewBag.ListVehicle = adminDAO.GetListVehicle();
            ViewBag.ListRestaurant = adminDAO.GetListRestaurant();
            ViewBag.ListStaff = adminDAO.GetListStaff();
            return View(tour);
        }
        public IActionResult DeleteTour(int tourId)
        {
            Tour tour = adminDAO.GetTourById(tourId);
            if (tour == null)
            {
                return RedirectToAction("EditTour", "Admin", new { mess = 1 });
            }
            ViewBag.TourId = tourId;
            ViewBag.Tour = adminDAO.GetTourById(tourId);
            return View();
        }
        public IActionResult TourDetail(int tourId)
        {
            Tour tour = adminDAO.GetTourById(tourId);
            if (tour == null)
            {
                return RedirectToAction("EditTour", "Admin", new { mess = 1 });
            }
            ViewBag.TourId = tourId;
            ViewBag.Tour = adminDAO.GetTourById(tourId);
            return View();
        }

        // Post
        [HttpPost]
        public IActionResult AddTour()
        {
            string NameTour = HttpContext.Request.Form["name"];
            //double? PriceTour = double.TryParse(HttpContext.Request.Form["price"], out double price) ? (double?)price : null;
            //string IMGTour = HttpContext.Request.Form["file"];
            //string Description = HttpContext.Request.Form["description"];
            //string CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //int? VehicleId = int.TryParse(HttpContext.Request.Form["vehicleId"], out int vehicleId) ? (int?)vehicleId : null;
            //int? HotelId = int.TryParse(HttpContext.Request.Form["hotelId"], out int hotelId) ? (int?)hotelId : null;
            //string AirPlane = HttpContext.Request.Form["airPlane"];
            //string Duration = HttpContext.Request.Form["duration"];
            //bool? Status = bool.TryParse(HttpContext.Request.Form["status"], out bool status) ? (bool?)status : null;
            //int? StaffId = int.TryParse(HttpContext.Request.Form["staffId"], out int staffId) ? (int?)staffId : null;
            //int? RestaurantId = int.TryParse(HttpContext.Request.Form["restaurantId"], out int restaurantId) ? (int?)restaurantId : null;
            //string Itinerary = HttpContext.Request.Form["itinerary"];
            //string Exclusions = HttpContext.Request.Form["exclusions"];
            //int? GroupSize = int.TryParse(HttpContext.Request.Form["groupSize"], out int groupSize) ? (int?)groupSize : null;
            //string Guide = HttpContext.Request.Form["guide"];
            //int? Rating = int.TryParse(HttpContext.Request.Form["rating"], out int rating) ? (int?)rating : null;

            Tour tour = new Tour()
            {
                Name = NameTour
            };

            //Tour tour = new Tour()
            //{
            //    Name = NameTour,
            //    Price = PriceTour,
            //    Image = IMGTour,
            //    Description = Description,
            //    CreateDate = CreateDate,
            //    VehicleId = VehicleId,
            //    HotelId = HotelId,
            //    AirPlane = AirPlane,
            //    Duration = Duration,
            //    Status = Status,
            //    StaffId = StaffId,
            //    RestaurantId = RestaurantId,
            //    Itinerary = Itinerary,
            //    Exclusions = Exclusions,
            //    GroupSize = GroupSize,
            //    Guide = Guide,
            //    Rating = Rating
            //};
            adminDAO.AddTour(tour);
            return RedirectToAction("ViewListTour", "Admin");

        }

        public IActionResult UpdateTour()
        {
            int? TourId = int.TryParse(HttpContext.Request.Form["tourId"], out int tourId) ? (int?)tourId : null;
            if (TourId == null)
            {
                return RedirectToAction("EditTour", "Admin", new { mess = 1 });
            }

            Tour tour = adminDAO.GetTourById((int)TourId);
            if (tour == null)
            {
                return RedirectToAction("EditTour", "Admin", new { mess = 1 });
            }


            string NameTour = HttpContext.Request.Form["name"];
            double? PriceTour = double.TryParse(HttpContext.Request.Form["price"], out double price) ? (double?)price : null;
            string IMGTour = HttpContext.Request.Form["file"];
            string Description = HttpContext.Request.Form["description"];
            string CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int? VehicleId = int.TryParse(HttpContext.Request.Form["vehicleId"], out int vehicleId) ? (int?)vehicleId : null;
            int? HotelId = int.TryParse(HttpContext.Request.Form["hotelId"], out int hotelId) ? (int?)hotelId : null;
            string AirPlane = HttpContext.Request.Form["airPlane"];
            string Duration = HttpContext.Request.Form["duration"];
            bool? Status = bool.TryParse(HttpContext.Request.Form["status"], out bool status) ? (bool?)status : null;
            int? StaffId = int.TryParse(HttpContext.Request.Form["staffId"], out int staffId) ? (int?)staffId : null;
            int? RestaurantId = int.TryParse(HttpContext.Request.Form["restaurantId"], out int restaurantId) ? (int?)restaurantId : null;
            string Itinerary = HttpContext.Request.Form["itinerary"];
            string Exclusions = HttpContext.Request.Form["exclusions"];
            int? GroupSize = int.TryParse(HttpContext.Request.Form["groupSize"], out int groupSize) ? (int?)groupSize : null;
            string Guide = HttpContext.Request.Form["guide"];
            int? Rating = int.TryParse(HttpContext.Request.Form["rating"], out int rating) ? (int?)rating : null;

            if (NameTour != null)
            {
                tour.Name = NameTour;
            }
            if (PriceTour != null)
            {
                tour.Price = PriceTour.Value;
            }
            if (!string.IsNullOrEmpty(IMGTour))
            {
                tour.Image = IMGTour;
            }
            if (!string.IsNullOrEmpty(Description))
            {
                tour.Description = Description;
            }
            if (VehicleId != null)
            {
                tour.VehicleId = VehicleId.Value;
            }
            if (HotelId != null)
            {
                tour.HotelId = HotelId.Value;
            }
            if (!string.IsNullOrEmpty(AirPlane))
            {
                tour.AirPlane = AirPlane;
            }
            if (!string.IsNullOrEmpty(Duration))
            {
                tour.Duration = Duration;
            }
            if (Status != null)
            {
                tour.Status = Status.Value;
            }
            if (StaffId != null)
            {
                tour.StaffId = StaffId.Value;
            }
            if (RestaurantId != null)
            {
                tour.RestaurantId = RestaurantId.Value;
            }
            if (!string.IsNullOrEmpty(Itinerary))
            {
                tour.Itinerary = Itinerary;
            }
            if (!string.IsNullOrEmpty(Exclusions))
            {
                tour.Exclusions = Exclusions;
            }
            if (GroupSize != null)
            {
                tour.GroupSize = GroupSize.Value;
            }
            if (!string.IsNullOrEmpty(Guide))
            {
                tour.Guide = Guide;
            }
            if (Rating != null)
            {
                tour.Rating = Rating.Value;
            }

            if (adminDAO.EditTour(tour))
            {
                return RedirectToAction("ViewListTour", "Admin");
            }
            else
            {
                return RedirectToAction("EditTour", "Admin", new { mess = 1, tourId = TourId });
            }


        }

        public IActionResult RemoveTour(int tourId)
        {
            Tour tour = adminDAO.GetTourById(tourId);
            if (tour == null)
            {
                return RedirectToAction("ViewListTour", "Admin", new { mess = 1 });
            }


            if (adminDAO.DeleteTour(tour))
            {
                return RedirectToAction("ViewListTour", "Admin");
            }
            else
            {
                return RedirectToAction("ViewListTour", "Admin", new { mess = 1 });
            }
        }

    }
}
