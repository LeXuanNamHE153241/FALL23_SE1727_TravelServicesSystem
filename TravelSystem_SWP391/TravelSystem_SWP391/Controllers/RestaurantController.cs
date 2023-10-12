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
    }
}
