using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks.Dataflow;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Controllers
{
    public class ToursController : Controller
    {
        public IActionResult tours()
        {
            DAO dal = new DAO();
            List<Tour> listtours = dal.GetAllTours();
            ViewBag.ListTours = listtours;
            return View();
        }

        public ActionResult search(string searchBy, string searchValue)
        {
            try
            {
                DAO dal = new DAO();
                List<Tour> listtours = dal.GetAllTours();
                if(listtours.Count==0)
                {
                    TempData["InfoMessage"] = "Currently products not available in the Database.";
                }
                else
                {
                    if(string.IsNullOrEmpty(searchValue)) 
                    {
                        TempData["InfoMessage"] = "Please provide search value.";
                        return View(listtours);
                    }
                    else
                    {
                        if (searchBy.ToLower() == "tourname")
                        {
                            ViewBag.ListTours = listtours.Where(p => p.Name.ToLower().Contains(searchValue.ToLower()));
                            return View();
                        }
                        if (searchBy.ToLower() == "price")
                        {
                            ViewBag.ListTours = listtours.Where(p => p.Price == double.Parse(searchValue));
                            return View();
                        }
                        if (searchBy.ToLower() == "duration")
                        {
                            ViewBag.ListTours = listtours.Where(p => p.Duration.ToLower().Contains(searchValue.ToLower()));
                            return View();
                        }
                    }
                }
                return View(listtours);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}