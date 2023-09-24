using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks.Dataflow;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Controllers
{
    public class ToursController : Controller
    {
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
        public IActionResult BookTours()
        {
            List<Tour> listtours = dal.GetAllTours();
            ViewBag.search = null;
            ViewBag.ListTours = listtours;
            return View();
        }
    }
}