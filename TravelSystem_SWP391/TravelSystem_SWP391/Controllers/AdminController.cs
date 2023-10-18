using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Controllers
{
    public class AdminController : Controller
    {
        AdminDAO adminDAO = new AdminDAO();
        public IActionResult Manager()
        {
            List<User> listUser = adminDAO.GetListUser();
            ViewBag.search = null;
            ViewBag.ListUser = listUser;
            return View();
        }
        public IActionResult ListFeedback()
        {
            List<Feedback> listFeedback = adminDAO.GetListFeedBack();
            ViewBag.search = null;
            ViewBag.ListFeedback = listFeedback;
            return View();
        }

        public IActionResult AddTour()
        {
            string NameTour = "";
            NameTour = HttpContext.Request.Form["name"];
            string PriceTour = "";
            PriceTour = HttpContext.Request.Form["price"];
            string IMGTour = "";
            IMGTour = HttpContext.Request.Form["file"];
            string Description = "";
            Description = HttpContext.Request.Form["description"];

            Tour tour = new Tour()
            {
                Name = HttpContext.Request.Form["name"],
                Price = double.Parse(HttpContext.Request.Form["price"]),
                Image = HttpContext.Request.Form["file"],
                Description = HttpContext.Request.Form["description"]
            };
            adminDAO.AddTour(tour);
            return RedirectToAction("ViewListTour", "Admin");

        }
        public IActionResult EditTourAccess(int tourId)
        {


            return RedirectToAction("EditTour", "Admin", new { tourId = tourId });
        }
        public IActionResult EditTour(int tourId)
        {

            ViewBag.TourId = tourId;

            Tour tour = adminDAO.GetTourById(tourId);
            ViewBag.Tour = tour;
            return View();
        }
        public IActionResult EditTourRequest()
        {
            string NameTour = "";
            NameTour = HttpContext.Request.Form["name"];
            string PriceTour = "";
            PriceTour = HttpContext.Request.Form["price"];
            string Description = "";
            Description = HttpContext.Request.Form["description"];
            Tour v = adminDAO.GetTourByName(NameTour);
            if (adminDAO.EditTour(v, PriceTour, Description))
            {



                return RedirectToAction("ViewListTour", "Admin");
            }
            else
            {
                return RedirectToAction("EditTour", "Admin", new { mess = 1 });
            }


        }

        public IActionResult DeleteTour(int id)
        {

            Tour tour = adminDAO.GetTourById(id);
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
