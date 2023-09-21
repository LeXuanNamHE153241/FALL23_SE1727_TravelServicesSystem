﻿using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;
using System.Net;
using System.Net.Mail;
using static System.Collections.Specialized.BitVector32;

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
                //HttpContext.Session.SetString("Image", account.Image.ToString());
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
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "Home");

        
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
        public IActionResult Register(int mess)
        {
            if (mess == 1)
            {
                ViewBag.mess2 = "Please your imfomation again!!!";
            }
            else if (mess == 2)
            {
                ViewBag.mess2 = "PassWord is not same Confirm-PassWord !!!";
            }
            else
            {
                ViewBag.mess2 = "";
            }
            return View();
        }
        public IActionResult RegisterAccess()
        {
			traveltestContext context = new traveltestContext();
			DAO dal = new DAO();
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

            Random r = new Random();
            string OTP = r.Next(100000,999999).ToString();


            //sendemail

            string fromEmail = "namlxhe153241@fpt.edu.vn";
            string toEmail = Username;
            string subject = "Hello"+Username;
            string body = "Tạo Tài Khoản Travel  Thành Công !!! " +
                "Mã OTP Của Bạn Là: "+OTP;
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "namlxhe153241@fpt.edu.vn";
            string smtpPassword = "esot ywmu zsji tlqf";

            bool result = DAO.SendEmail(fromEmail, toEmail, subject, body, smtpServer, smtpPort, smtpUsername, smtpPassword);



            //Check Email
            if (dal.IsEmailValid(Username)==true&& Pass == Cf_Pass && dal.IsPhoneNumberValidVietnam(PhoneNumber)==true&&dal.IsValidFirstnameorLastname(FirstName)==true&&dal.IsValidFirstnameorLastname(LastName) == true&&result==true)
			{
                User user = new User()
                {
                    Email = HttpContext.Request.Form["username"],
                    Password = HttpContext.Request.Form["pass"],
                    FirstName = HttpContext.Request.Form["FirstName"],
                    LastName = HttpContext.Request.Form["LastName"],
                    PhoneNumber = HttpContext.Request.Form["PhoneNumber"],
                    RoleId = 1,
                    Action = "on",
                    Gender = HttpContext.Request.Form["Gender"]
                };
				context.Add(user);
				context.SaveChanges();
                return RedirectToAction("Login", "Login");
            }
			else
			{
                return RedirectToAction("Register", "Login", new {mess =1 });
            }

        }
    }
}