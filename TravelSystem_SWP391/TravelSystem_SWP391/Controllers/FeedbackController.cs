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
        DAO dao = new DAO();

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

        [HttpGet]
        public ActionResult List()
        {
            List<Feedback> fbs = dal.GetListFeedBack();
            return View(fbs);
        }

        [HttpGet]
        public ActionResult DetailFeedback(string userEmail, string feedbackName, string feedbackMessage, string feedbackSubject, string feedbackResponce)
        {
            FeedbackDetail fbs = dao.DetailFeedback(userEmail, feedbackName, feedbackMessage, feedbackSubject, feedbackResponce);
            return View(fbs);
        }

        [HttpPost]
        public IActionResult AddResponse(Feedback feedback)
        {
            Feedback newFeedback = new Feedback()
            {
                Email = feedback.Email,
                Name = feedback.Name,
                Subject = feedback.Subject,
                Message = feedback.Message,
                Response = feedback.Response,
            };
            dao.UpdateFeedback(newFeedback);
            return RedirectToAction("List");
        }

    }
}
