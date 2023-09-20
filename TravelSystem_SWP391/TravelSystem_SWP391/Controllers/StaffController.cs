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

    }

}
