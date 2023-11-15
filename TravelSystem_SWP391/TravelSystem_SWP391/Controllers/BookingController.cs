using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TravelSystem_SWP391.DAO_Context;
using TravelSystem_SWP391.Models;
using X.PagedList;
using static System.Net.WebRequestMethods;

namespace TravelSystem_SWP391.Controllers
{
    public class BookingController : Controller
    {
        traveltestContext context = new traveltestContext();
        DAO dal = new DAO();
        public IActionResult ViewListBooking(int page)
        {
            String statuslogin = HttpContext.Session.GetString("username");
            page = page < 1 ? 1 : page;
            List<Booking> listbooking = dal.GetListBooking();
            int pagesize = 8;
            var booking = context.Bookings.ToPagedList(page, pagesize);
            ViewBag.Booking = booking;
            if (statuslogin == null)
            {
                return RedirectToAction("index", "Home");
            }
            return View(booking);
        }
        public IActionResult ViewHistoryListBooking(int page)
        {
            String FirstName = HttpContext.Session.GetString("username");
            List<Booking> listbooking = dal.GetListBookingByEmail(FirstName);
            ViewBag.Booking = listbooking;
           
            return View();
        }
        public IActionResult ViewListBookingVehicleInTourist()
        {
            String FirstName = HttpContext.Session.GetString("username");
            List<Booking> listbooking = dal.GetListHistoryBookingByEmail(FirstName);
           
            ViewBag.Booking = listbooking;
            return View(listbooking);


            
        }
        public IActionResult RequestAccept(int id,string email)
        {

            string fromEmail = "namlxhe153241@fpt.edu.vn";
            string toEmail = email;
            string subject = "Hello " + email;

            string body = "Booking Thành Công ."
                +"Cảm Ơn Bạn Đã Tin Tưởng Và Đặt Tour,"
                +"Bạn Vui Lòng Đăng Nhập Vào Hệ Thống Và Kiểm Tra Lại Thông Tin Trước Chuyến Đi."
                +"Travel Systerm Xin Cảm Ơn .";
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "namlxhe153241@fpt.edu.vn";
            string smtpPassword = "esot ywmu zsji tlqf";
            bool result = SendEmail.theSendEmailBooking(fromEmail, toEmail, subject, body, smtpServer, smtpPort, smtpUsername, smtpPassword);
            var booking = dal.GetListBookingByID(id);
            string stt = "Acceptance";
            dal.EditMessBooking(booking, stt);
            return RedirectToAction("ViewListBooking", "Booking");
        }
        public IActionResult RequestUnaccept(int id,string email)
        {
          
            string fromEmail = "namlxhe153241@fpt.edu.vn";
            string toEmail = email;
            string subject = "Hello " + email;

            string body = "Vì Một Số Lý Do Nào Đó Từ Phía Của Chúng Tôi Không Thể Xếp Lịch Cho Bạn Chúng Tôi Rất Xin Lỗi Vì Sự Bất Tiện Này Hân Hạnh Được Phục Vụ Bạn Lần Sau " ;
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUsername = "namlxhe153241@fpt.edu.vn";
            string smtpPassword = "esot ywmu zsji tlqf";
            bool result = SendEmail.theSendEmailBooking(fromEmail, toEmail, subject, body, smtpServer, smtpPort, smtpUsername, smtpPassword);
            var booking = dal.GetListBookingByID(id);
            string stt = "Unacceptance";
            dal.EditMessBooking(booking, stt);
            return RedirectToAction("ViewListBooking", "Booking");
        }
    }
}
