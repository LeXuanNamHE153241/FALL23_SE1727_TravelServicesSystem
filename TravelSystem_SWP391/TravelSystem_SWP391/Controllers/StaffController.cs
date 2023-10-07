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
            try
            {
                dal.AddStaff(stafff);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Delete(int staffId)
        {
            try
            {
                dal.RemoveStaff(staffId);
                return Ok();
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
