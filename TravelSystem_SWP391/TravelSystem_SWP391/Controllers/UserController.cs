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


        public IActionResult ViewDetailsUsers()
        {
            string email = HttpContext.Session.GetString("username");
            string pass = HttpContext.Session.GetString("pass");
            ViewBag.pass = pass;
            User u = context.Users.FirstOrDefault(u => u.Email == email);
            ViewBag.users = u;
            return View();
        }
        public IActionResult EditDetailsUsers()
        {

            string email = HttpContext.Session.GetString("username");

            User u = context.Users.FirstOrDefault(u => u.Email == email);
            ViewBag.users = u;
            return View();
        }
        public IActionResult EditDetailsUsersRequest()
        {
            string FirstName = "";
            FirstName = HttpContext.Request.Form["firstname"];
            string LastName = "";
            LastName = HttpContext.Request.Form["lastname"];
            string PhoneNumber = "";
            PhoneNumber = HttpContext.Request.Form["phonenumber"];
            string Description = "";
            Description = HttpContext.Request.Form["description"];
            string Gender = "";
            Gender = HttpContext.Request.Form["gender"];
            string email = HttpContext.Session.GetString("username");
            User u = context.Users.FirstOrDefault(u => u.Email == email);
            if (dal.EditUser(u, FirstName, LastName, PhoneNumber, Description, Gender))
            {



                return RedirectToAction("ViewDetailsUsers", "User" );
            }
            else
            {
                return RedirectToAction("ViewDetailsUsers", "User");
            }
        }
    }
}
