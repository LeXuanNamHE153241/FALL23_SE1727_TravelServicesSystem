﻿using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;
using X.PagedList;

namespace TravelSystem_SWP391.Controllers
{
    public class VehicleController : Controller
    {
        traveltestContext context = new traveltestContext();
        DAO dal = new DAO();
        public IActionResult ViewListVehicle( int mess,int page)
        {
            page = page <1 ? 1 : page;
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
            int pagesize = 4;
            List<Vehicle> listvehicle = dal.GetListVehicle();
            var vehicle = context.Vehicles.ToPagedList(page, pagesize);
            ViewBag.search = null;
            ViewBag.ListVehicle = vehicle;
            ViewBag.page=page;
            ViewBag.pagesize = pagesize;
            if (mess == 1)
            {
                ViewBag.mess = "1";
            }
            if(FirstName == null)
            {
                return RedirectToAction("index", "Home");
            }
            return View(vehicle);
        }
        [HttpPost]
        public ActionResult SearchVehicle()
        {
            traveltestContext context = new traveltestContext();
            String NameVehicle = "";
            String RoleID = HttpContext.Session.GetString("RoleID");
            NameVehicle = HttpContext.Request.Form["namevehicle"];
            ViewBag.RoleID = RoleID;
            var data = (from p in context.Vehicles
                        where p.Name.Contains(NameVehicle)
                        orderby p.Price, p.Rate descending
                        select new
                        {
                            Id=p.Id,
                            Name = p.Name,
                            Description= p.Description,
                            Price = p.Price,
                            Image = p.Image,
                            Rate = p.Rate,
                            Type = p.Type
                           
                        }).ToList();
            ViewBag.Name = NameVehicle;
            ViewBag.search = data;
            String statuslogin = HttpContext.Session.GetString("FirstName");
            if (statuslogin == null)
            {
                return RedirectToAction("index", "Home");
            }

            return View();
        }


       
        public IActionResult additem()
        {
            List<Vehicle> listvehicle = dal.GetListVehicle();
            ViewBag.search = null;
            ViewBag.ListVehicle = listvehicle;
            String statuslogin = HttpContext.Session.GetString("FirstName");
            if (statuslogin == null)
            {
                return RedirectToAction("index", "Home");
            }
            return View();
        }
        public IActionResult AddVehicleAccess()
        {
            string NameVehicle = "";
            NameVehicle = HttpContext.Request.Form["name"];
            string TypeVehicle = "";
            TypeVehicle = HttpContext.Request.Form["type"];
            string PriceVehicle = "";
            PriceVehicle = HttpContext.Request.Form["price"];
            string IMGVehicle = "";
            IMGVehicle = HttpContext.Request.Form["file"];
            string Rate = "";
            Rate = HttpContext.Request.Form["rate"];
            string Description = "";
            Description = HttpContext.Request.Form["description"];
            
                Vehicle vehicle = new Vehicle()
                {
                    Name = HttpContext.Request.Form["name"],
                    Type = HttpContext.Request.Form["type"],
                    Price = double.Parse(HttpContext.Request.Form["price"]) ,
                    Image = HttpContext.Request.Form["file"],
                    Rate = int.Parse(HttpContext.Request.Form["rate"]),
                    
                    Description = HttpContext.Request.Form["description"]
                };
                context.Add(vehicle);
                context.SaveChanges();
            String statuslogin = HttpContext.Session.GetString("FirstName");
            if (statuslogin == null)
            {
                return RedirectToAction("index", "Home");
            }
            return RedirectToAction("ViewListVehicle", "Vehicle"); 







        }
        public IActionResult editvehicleaccess(string name)
        {

            String statuslogin = HttpContext.Session.GetString("FirstName");
            if (statuslogin == null)
            {
                return RedirectToAction("index", "Home");
            }
            return RedirectToAction("editvehicle", "Vehicle", new { name = name });
        }
        public IActionResult editvehicle(string name,int mess)
        {
            
            ViewBag.Name = name;
            if (mess == 1)
            {
                ViewBag.mess = "1";
            }
            else
            {
                ViewBag.mess = "22";
            }
            Vehicle v = context.Vehicles.FirstOrDefault(v => v.Name == name);
            ViewBag.vehicle = v;
            String statuslogin = HttpContext.Session.GetString("FirstName");
            if (statuslogin == null)
            {
                return RedirectToAction("index", "Home");
            }
            return View();
        }
        public IActionResult editvehiclerequest()
        {
            string NameVehicle = "";
            NameVehicle = HttpContext.Request.Form["name"];
            string TypeVehicle = "";
            TypeVehicle = HttpContext.Request.Form["type"];
            string PriceVehicle = "";
            PriceVehicle = HttpContext.Request.Form["price"];
            
            string Rate = "";
            Rate = HttpContext.Request.Form["rate"];
            string Description = "";
            Description = HttpContext.Request.Form["description"];
            Vehicle v = context.Vehicles.FirstOrDefault(v => v.Name == NameVehicle);
            if ( dal.EditVehicle(v,NameVehicle,TypeVehicle,PriceVehicle,Rate,Description)==true)
            {



                return RedirectToAction("ViewListVehicle", "Vehicle");
            }
            else
            {
                return RedirectToAction("editvehicle", "Vehicle", new {name =NameVehicle, mess = 1});
            }

            
        }
        public IActionResult deletevehicle(int id)
        {
            
            Vehicle v = context.Vehicles.FirstOrDefault(v => v.Id == id);
            if (dal.DeleteVehicle(v))
            {



                return RedirectToAction("ViewListVehicle", "Vehicle");
            }
            else
            {
                return RedirectToAction("ViewListVehicle", "Vehicle", new { mess = 1 });
            }
        }
        public ActionResult BookVehicle(int id)
        {
            traveltestContext context = new traveltestContext();
            
            String Email = HttpContext.Session.GetString("username");
            
            User u = new User();
            u = dal.getUser(Email);
            Vehicle v = new Vehicle();
            v = dal.getVihecle(id);
            List<staff> staff = dal.GetListStaffByRole("Driver Staff");
            ViewBag.UserBook = u;
            ViewBag.ListStaff = staff;
            ViewBag.Vehicle = v;

            String statuslogin = HttpContext.Session.GetString("FirstName");
            if (statuslogin == null)
            {
                return RedirectToAction("index", "Home");
            }

            return View();
        }
        public ActionResult BookVehicleAccess()
        {
            traveltestContext context = new traveltestContext();
            String Email = HttpContext.Request.Form["Email"];

            String NameUser = HttpContext.Request.Form["NameUser"];

            String Phone = HttpContext.Request.Form["Phone"];

            String NameVehicle = HttpContext.Request.Form["NameVehicle"];
            String startdate = HttpContext.Request.Form["stdate"];
            String enddate = HttpContext.Request.Form["edate"];
            String IdVehicle = HttpContext.Request.Form["IdVehicle"];
            String RoleID = HttpContext.Session.GetString("RoleID");
            Booking booking = new Booking()
            {
                Name = NameUser,

                Email = Email,

                Phone = Phone,
                StartDate = DateTime.Parse(startdate),
                EndDate = DateTime.Parse(enddate),
                NumPeople = 5,
                Message = "",
                VehicleId = int.Parse(IdVehicle)
            };
            context.Add(booking);
            context.SaveChanges();

            if (RoleID == "1")
            {
                return RedirectToAction("ViewListBookingVehicleInTourist", "Booking", new {mess = 1 });
            }
            else
            {
                return RedirectToAction("ViewListBooking", "Booking");
            } 
                
            
           


            
        }


    }
}
