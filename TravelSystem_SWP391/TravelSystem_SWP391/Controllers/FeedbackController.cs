using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Controllers
{
    public class FeedbackController : Controller
    {
        traveltestContext context = new traveltestContext();
        AdminDAO dal = new AdminDAO();


        public IActionResult AddFeedback()
        {
            List<Feedback> listfeedback = dal.GetListFeedBack();
            ViewBag.search = null;
            ViewBag.ListFeedback = listfeedback;
            return View();
        }
        public IActionResult AddFeedbackAccess()
        {
            string Email = "";
            Email = HttpContext.Request.Form["email"];
            string Name = "";
            Name = HttpContext.Request.Form["name"];
            string Subject = "";
            Subject = HttpContext.Request.Form["subject"];
            string Message = "";
            Message = HttpContext.Request.Form["message"];

            Feedback feedback = new Feedback()
            {
                Email = HttpContext.Request.Form["email"],
                Name = HttpContext.Request.Form["name"],
                Subject = HttpContext.Request.Form["subject"],
                Message = HttpContext.Request.Form["message"]
            };
            context.Add(feedback);
            context.SaveChanges();
            return RedirectToAction("AddFeedback", "Feedback");

        }
    }
}
