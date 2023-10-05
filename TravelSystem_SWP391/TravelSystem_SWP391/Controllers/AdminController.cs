using Microsoft.AspNetCore.Mvc;
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
    }
}
