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
using System.Security.Principal;
using System.Runtime.Serialization;

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

        public IActionResult CreatePaymentUrl(PaymentInformationModel model,int id)
        {
            String Amount = "";
            Amount = HttpContext.Request.Form["Amount"];
            if(Amount == "500000")
            {
                var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

                return Redirect(url);
            }
            else
            {
                return RedirectToAction("Pay", "Payment", new {id = id , mess =1});
            }
           
           
        }

        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Json(response);
        }
        public IActionResult Pay(int id,string mess)
        {
            String FirstName = HttpContext.Session.GetString("username");
            Booking booking = dal.GetListBookingByID(id);
            ViewBag.Booking = booking;
            HttpContext.Session.SetInt32("ID", id);
            Vehicle v = new Vehicle();
            v = dal.getVihecle(id);
            ViewBag.Vehicle = v;
            ViewBag.ID=id;
            if (mess == "1")
            {
                ViewBag.mess = "Số Tiền Cần Cọc Là 500.000 VND, bạn vui lòng kiểm tra lại!!!";
            }
            else
            {
                ViewBag.mess = null;
            }
            
            return View();
        }
        public IActionResult PayRes(long vnp_Amount,string vnp_OrderInfo,string vnp_BankCode,
            string vnp_TransactionNo,string vnp_ResponseCode,string vnp_PayDate,string vnp_SecureHash,string vnp_TxnRef)
        {
            
            ViewBag.vnp_Amount=vnp_Amount/100;
            ViewBag.vnp_OrderInfo=vnp_OrderInfo;
            ViewBag.vnp_BankCode = vnp_BankCode;
            ViewBag.vnp_TransactionNo = vnp_TransactionNo;
            ViewBag.vnp_ResponseCode = vnp_ResponseCode;
           
            ViewBag.vnp_SecureHash = vnp_SecureHash;
            ViewBag.vnp_TxnRef = vnp_TxnRef;

           return View();
        }
        public IActionResult RequestPay()
        {
            string cf_status = HttpContext.Request.Form["cf_status"];
            if (HttpContext.Session.GetInt32("ID")== null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                int id = (int)HttpContext.Session.GetInt32("ID");
                Booking booking = dal.GetListBookingByID(id);
                if (cf_status == "Kết Quả : Thành Công")
                {
                    dal.EditMessBooking(booking, "Thanh Toán Thành Công");
                }
                else if (cf_status == "Kết Quả :Không Thành Công")
                {
                    dal.EditMessBooking(booking, "Thanh Toán Không Thành Công");
                }
                else
                {
                    dal.EditMessBooking(booking, "Chưa Thanh Toán");
                }


                return RedirectToAction("ViewListBookingVehicleInTourist", "Booking");
            }
           
        }





    }

}
