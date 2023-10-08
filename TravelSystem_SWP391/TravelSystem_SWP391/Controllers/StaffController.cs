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

        [HttpPut]
        public IActionResult Update(staff stafff)
        {
            try
            {
                dal.UpdateStaff(stafff);
                return Ok();
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
