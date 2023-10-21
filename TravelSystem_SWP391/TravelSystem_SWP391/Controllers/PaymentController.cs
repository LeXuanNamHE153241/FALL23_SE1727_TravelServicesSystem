using Microsoft.AspNetCore.Mvc;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TravelSystem_SWP391.Controllers
{
    public class PaymentController : Controller
    {
        traveltestContext context = new traveltestContext();
        DAO dal = new DAO();
        PayDAO pay = new PayDAO();
        public IActionResult Pay(int id)
        {
            Vehicle v = new Vehicle();
            v = dal.getVihecle(id);
            ViewBag.Vehicle = v;
            string p = pay.payment1(1);
            ViewBag.P = p;
        
            
            return View();
        }
        public IActionResult PayRes()
        {   
           
           return View();
        }

     

       

    }

}
