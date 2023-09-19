using Microsoft.AspNetCore.Mvc;
using TravelSystem_SWP391.DAO_Context;

namespace TravelSystem_SWP391.Controllers
{
	public class HomeController : Controller
	{
        DAO dal = new DAO();
        public IActionResult tours()
		{

			return View();
		}
		public IActionResult index()
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
            return View();
		}
		public IActionResult blog()
		{
			
			return View();
		}
		public IActionResult contacts()
		{
			return View();
		}
		public IActionResult about()
		{
			return View();
		}
		public IActionResult gallery()
		{
			return View();
		}
	}
}
