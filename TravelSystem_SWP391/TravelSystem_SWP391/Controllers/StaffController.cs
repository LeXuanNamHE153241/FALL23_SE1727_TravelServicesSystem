using Microsoft.AspNetCore.Mvc;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;

namespace TravelSystem_SWP391.Controllers
{
    public class StaffController : Controller
    {
        DAO dal = new DAO();

        [HttpGet]
        public ActionResult List()
        {
            List<staff> staffs = dal.GetStaffs();
            return View(staffs);
        }

        [HttpPost]
        public IActionResult Add(staff stafff)
        {
            string emailAll = stafff.EmailUserNavigation?.Email;
            User newUser = new User()
            {
                Email = emailAll,
                Password = stafff.EmailUserNavigation?.Password,
                FirstName = stafff.EmailUserNavigation?.FirstName,
                LastName = stafff.EmailUserNavigation?.LastName,
                PhoneNumber = stafff.EmailUserNavigation?.PhoneNumber,
                RoleId = 4,
                Action = "on",
                Description = stafff.EmailUserNavigation?.Description,
                Gender = stafff.EmailUserNavigation?.Gender,
            };
            staff newStaff = new staff()
            {
                RoleStaff = stafff.RoleStaff,
                EmailUserNavigation = newUser
            };

            dal.AddUserStaff(newUser);
            dal.AddStaff(newStaff);
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult ViewAddStaff()
        {
            return View();
        }

        public IActionResult Delete(string staffEmail)
        {
            try
            {
                dal.RemoveStaff(staffEmail);
                return RedirectToAction("List");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult EditInformationStaff()
        {
            String FirstName = HttpContext.Session.GetString("FirstName");

            String LastName = HttpContext.Session.GetString("LastName");

            String RoleID = HttpContext.Session.GetString("RoleID");

            String Phone = HttpContext.Session.GetString("Phone");

            String Image = HttpContext.Session.GetString("Image");
            String Email = HttpContext.Session.GetString("username");

            ViewBag.Email = Email;
            ViewBag.FirstName = FirstName;
            ViewBag.LastName = LastName;
            ViewBag.RoleID = RoleID;
            ViewBag.Phone = Phone;
            ViewBag.Image = Image;
            return View();
        }

        [HttpPost]
        public IActionResult UpdateStaff(string Email, string FirstName, string LastName, string Phone, string Description)
        {
            User newUser = new User()
            {
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = Phone,
                Description = Description,
            };

            try
            {
                dal.UpdateStaff(newUser);
                return RedirectToAction("List", "Feedback");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult SearchStaff(string namevehicle)
        {
            try
            {
                staff staffs = dal.SearchStaff(namevehicle);
                if (staffs != null)
                {
                    return View(staffs);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }

}
