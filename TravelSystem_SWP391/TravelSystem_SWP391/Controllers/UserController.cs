using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks.Dataflow;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Controllers
{
    public class UserController : Controller
    {
        traveltestContext context = new traveltestContext();
        DAO dal = new DAO();

       
        public IActionResult ViewDetailsUsers(string email)
        {

            ViewBag.Email = email;

            User u = context.Users.FirstOrDefault(u => u.Email == email);
            ViewBag.users = u;
            return View();
        }
    }
}
