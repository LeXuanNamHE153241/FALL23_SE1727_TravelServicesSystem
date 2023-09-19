using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Controllers
{
    public class LoginController : Controller
	{
		DAO dal = new DAO();

		[HttpPost]
		public ActionResult LoginAccess()
		{
			String Username = "";
			Username = HttpContext.Request.Form["username"];
			String Pass = "";
			Pass = HttpContext.Request.Form["pass"];
			User account = new User();
			Vehicle vehicle = new Vehicle();
			account = dal.Login(Username, Pass);
			if (account != null)
			{
				HttpContext.Session.SetString("FirstName",account.FirstName.ToString());
				HttpContext.Session.SetString("LastName", account.LastName.ToString());
                HttpContext.Session.SetString("RoleID", account.RoleId.ToString());
                HttpContext.Session.SetString("Phone", account.PhoneNumber.ToString());
                HttpContext.Session.SetString("Image", account.Image.ToString());
                List<Vehicle> listvehicle = dal.GetVehicle();
				ViewBag.ListVehicle = listvehicle;
                //HttpContext.Session.SetString("accID", account.Image.ToString());
                //HttpContext.Session.SetString("chucvu", account.RoleId.ToString());
                return RedirectToAction("index", "Home");
			}
			else
			{
				return RedirectToAction("Login", "Login", new { mess = 1 });
			}
		}
		public IActionResult Login(int mess)
		{
			 
			if (mess == 1)
			{
				ViewBag.mess1 = "Thông tin tài khoản không tồn tại , kiểm tra lại thông tin đăng nhập";
			}
			else if (mess ==2)
			{
				ViewBag.mess1 = "Vui lòng đăng nhập trước khi thao tác";
			}
			else
			{
				ViewBag.mess1 = "";
			}
			return View();
		}
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult RegisterAccess()
        {
            String Username = "";
            Username = HttpContext.Request.Form["username"];
            String Pass = "";
            Pass = HttpContext.Request.Form["pass"];
			String Cf_Pass = "";
			Cf_Pass = HttpContext.Request.Form["Confirm-Password"];

            String FirstName = "";
            FirstName = HttpContext.Request.Form["FirstName"];
            String LastName = "";
            LastName = HttpContext.Request.Form["LastName"];
            String PhoneNumber = "";
            PhoneNumber = HttpContext.Request.Form["PhoneNumber"];
            String Gender = "";
            Gender = HttpContext.Request.Form["Gender"];
            return RedirectToAction("Login", "Login");
        }
    }
}