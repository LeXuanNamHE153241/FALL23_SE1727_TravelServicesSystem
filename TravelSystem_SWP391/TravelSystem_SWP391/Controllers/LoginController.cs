﻿using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;
using System.Net;
using System.Net.Mail;
using static System.Collections.Specialized.BitVector32;
using static System.Net.WebRequestMethods;
using System.Reflection;

namespace TravelSystem_SWP391.Controllers
{
    public class LoginController : Controller
	{
		DAO dal = new DAO();
        SendEmail send = new SendEmail();
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
                HttpContext.Session.SetString("Email", account.Email.ToString());
                HttpContext.Session.SetString("FirstName",account.FirstName.ToString());
				HttpContext.Session.SetString("LastName", account.LastName.ToString());
                HttpContext.Session.SetString("RoleID", account.RoleId.ToString());
                HttpContext.Session.SetString("Phone", account.PhoneNumber.ToString());
                HttpContext.Session.SetString("username", account.Email.ToString());
                HttpContext.Session.SetString("pass", account.Password.ToString());
                //HttpContext.Session.SetString("Image", account.Image.ToString());

                if (account.Description != null)
                {
                    HttpContext.Session.SetString("descr", account.Description.ToString());
                }
                else
                {
                    HttpContext.Session.SetString("descr", "");
                }

                List<Vehicle> listvehicle = dal.GetListVehicle();
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
            else if (mess == 3)
            {
                ViewBag.mess1 = "This email has been registered !!!";
            }
            else if (mess == 4)
            {
                ViewBag.mess1 = "Account registration successful !!!";
            }
            else
			{
				ViewBag.mess1 = "";
			}
			return View();
		}
        public IActionResult Register(int mess,string email)
        {
            ViewBag.Email = email;
            if (mess == 1)
            {
                ViewBag.mess2 = "Please your imfomation again!!!";
            }
            else if (mess == 2)
            {
                ViewBag.mess2 = "This email has been registered !!!";
            }
            else
            {
                ViewBag.mess2 = "";
            }
            if (ViewBag.Email == null )
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        public IActionResult CheckEmailRegister(int mess)
        {
            if (mess == 1)
            {
                ViewBag.mess1 = "Tài Khoản Email không đúng hoặc không tồn tại vui lòng kiểm tra lại !!!";
            }

            return View();
        }
        //CheckEmailRegisterAccess
        public IActionResult CheckEmailRegisterAccess()
        {
            traveltestContext context = new traveltestContext();
            DAO dal = new DAO();
            String Email = "";
            Email = HttpContext.Request.Form["email"];


            Random r = new Random();
            string OTP = r.Next(100000, 999999).ToString();


            //sendemail

            string fromEmail = "namlxhe153241@fpt.edu.vn";
            string toEmail = Email;
            string subject = "Hello " + Email;

            string body =
                "Mã OTP đăng ký tài khoản tại Travel Systerm  Của Bạn Là: " + OTP;
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "namlxhe153241@fpt.edu.vn";
            string smtpPassword = "esot ywmu zsji tlqf";

            bool result = SendEmail.theSendEmailForGotPassWord(fromEmail, toEmail, subject, body, smtpServer, smtpPort, smtpUsername, smtpPassword, Email);

            //Check Email
            if (dal.IsEmailValid(Email) && result == true)
            {

                HttpContext.Session.SetString("Email", Email.ToString());
                HttpContext.Session.SetString("OTP", OTP.ToString());


                return RedirectToAction("ConfilmOTPregister", "Login");
            }
            else
            {
                return RedirectToAction("CheckEmailRegister", "Login", new { mess = 1 });
            }

        }

        public IActionResult ConfilmOTPregister(string messcf)
        {
            String OTP = HttpContext.Session.GetString("OTP");
            String Email = HttpContext.Session.GetString("Email");
            ViewBag.messcf = messcf;
            ViewBag.Email = Email;
            ViewBag.OTP = OTP;
            
            return View();
        }
        public IActionResult ConfilmOTPregisterAccess(string email, string otp)
        {

            String OTP = "";
            OTP = HttpContext.Request.Form["otpcf"];
            ViewBag.OTP = otp;
            if (OTP == otp)
            {


                return RedirectToAction("Register", "Login", new { email = email });
            }
            else
            {
                return RedirectToAction("ConfilmOTPregister", "Login", new { messcf = 1 });
            }
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
            
            
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "namlxhe153241@fpt.edu.vn";
            string smtpPassword = "esot ywmu zsji tlqf";

            

            //Check Email
            if (dal.IsEmailValid(Username) ==true&& Pass == Cf_Pass && dal.IsPhoneNumberValidVietnam(PhoneNumber)==true&&dal.IsValidFirstnameorLastname(FirstName)==true&&dal.IsValidFirstnameorLastname(LastName) == true)
			{

                User usercheck = context.Users.Where(x => x.Email == Username).FirstOrDefault();
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
                if (usercheck != null)
                {
                    string body = "Tạo Tài Khoản Không Thành Công , Email này đã được đăng ký bạn vui lòng kiểm tra lại !!!";
                    bool result = SendEmail.theSendEmail(fromEmail, toEmail, subject, body, smtpServer, smtpPort, smtpUsername, smtpPassword, Username, Pass, Cf_Pass, FirstName, LastName, PhoneNumber);
                    return RedirectToAction("Login", "Login", new { mess = 3 });
                }
                else
                {
                    string body = "Bạn Đã Đăng Ký Thành Công Tài Khoản Tại Travel Systerm  !!!";
                    bool result = SendEmail.theSendEmail(fromEmail, toEmail, subject, body, smtpServer, smtpPort, smtpUsername, smtpPassword, Username, Pass, Cf_Pass, FirstName, LastName, PhoneNumber);
                    context.Add(user);
                    context.SaveChanges();
                    return RedirectToAction("Login", "Login", new { mess = 4 });
                }
				
            }
			else
			{
                return RedirectToAction("Register", "Login", new {mess =1 });
            }
        
        }



        public IActionResult ForgotPassWord(int mess)
        {
            return View();
        }


        public IActionResult ForgotPassWordAccess()
        {
            traveltestContext context = new traveltestContext();
            DAO dal = new DAO();
            String Email = "";
            Email = HttpContext.Request.Form["email"];
            

            Random r = new Random();
            string OTP = r.Next(100000, 999999).ToString();


            //sendemail

            string fromEmail = "namlxhe153241@fpt.edu.vn";
            string toEmail = Email;
            string subject = "Hello " + Email;

            string body = 
                "Mã OTP Của Bạn Là: " + OTP;
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "namlxhe153241@fpt.edu.vn";
            string smtpPassword = "esot ywmu zsji tlqf";

            bool result = SendEmail.theSendEmailForGotPassWord(fromEmail, toEmail, subject, body, smtpServer, smtpPort, smtpUsername, smtpPassword, Email);

            //Check Email
            if (dal.IsEmailValid(Email) && result == true)
            {
               
                HttpContext.Session.SetString("Email", Email.ToString());
                HttpContext.Session.SetString("OTP", OTP.ToString());
                

                return RedirectToAction("ConfilmOTP", "Login");
            }
            else
            {
                return RedirectToAction("Register", "Login", new { mess = 1 });
            }

        }
        
        public IActionResult ConfilmOTP(string messcf)
        {
            String OTP = HttpContext.Session.GetString("OTP");
            String Email = HttpContext.Session.GetString("Email");
            ViewBag.messcf = messcf;
            ViewBag.Email = Email;
            ViewBag.OTP = OTP;
            return View();
        }
        public IActionResult ConfilmOTPAccess(string email,string otp)
        {
            String OTP = "";
            OTP = HttpContext.Request.Form["otpcf"];
            ViewBag.OTP = otp;
            if (OTP==otp)
            {


                return RedirectToAction("ChangePassWord", "Login",new {email =email});
            }
            else
            {
                return RedirectToAction("ConfilmOTP", "Login", new {messcf=1});
            }
        }
        public IActionResult ChangePassWord(string email)
        {
            if( email != null)
            {
                HttpContext.Session.SetString("Email", email);
            }
            
            ViewBag.Email = email;
            if(email == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        public IActionResult ChangePassWordAccess()
        {
            traveltestContext context = new traveltestContext();
            DAO dal = new DAO();
            String Email = "";
            Email = HttpContext.Request.Form["email"];
            String Pass = "";
            Pass = HttpContext.Request.Form["pass"];
            String Cf_Pass = "";
            Cf_Pass = HttpContext.Request.Form["Confirm-Password"];

            User users = dal.getUser(HttpContext.Session.GetString("Email"));
            if ( Pass == Cf_Pass &&dal.ChangePass(users,Pass) )
            {
               
                
               
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return RedirectToAction("ChangePassWord", "Login", new { email = Email });
            }

           
        }

    }
}





































































