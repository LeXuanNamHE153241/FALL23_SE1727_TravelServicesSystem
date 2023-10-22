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

using TravelSystem_SWP391.Services;

namespace TravelSystem_SWP391.Controllers
{
    public class PaymentController : Controller
    {
        traveltestContext context = new traveltestContext();
        DAO dal = new DAO();
        
        private readonly IVnPayService _vnPayService;

        public PaymentController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
           
        }

        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Json(response);
        }
        public IActionResult Pay(int id)
        {
            String FirstName = HttpContext.Session.GetString("username");
            Booking booking = dal.GetListBookingByID(id);
            ViewBag.Booking = booking;

            Vehicle v = new Vehicle();
            v = dal.getVihecle(id);
            ViewBag.Vehicle = v;
            
        
            
            return View();
        }
        public IActionResult PayRes()
        {   
           
           return View();
        }

     

       

    }

}
