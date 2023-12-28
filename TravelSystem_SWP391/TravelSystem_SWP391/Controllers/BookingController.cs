using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
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
            int pagesize = 8;
            //List<Booking> listbooking = dal.GetListBooking().ToPagedList(page, pagesize);
           
            var booking = context.Bookings.Include(s => s.Hotel).Include(s => s.Vehicle).Include(s => s.Restaurant).Include(s => s.Tour).Where(s => s.VehicleId == s.Vehicle.Id && s.HotelId == s.Hotel.Id&& s.Tour.Inclusions.Contains(statuslogin) ==true).ToList().ToPagedList(page, pagesize);
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
           
            return View(listbooking);
        }
        public IActionResult ViewListBookingVehicleInTourist()
        {
            String FirstName = HttpContext.Session.GetString("username");
            List<Booking> listbooking = dal.GetListHistoryBookingByEmail(FirstName);
           
            ViewBag.Booking = listbooking;
            return View(listbooking);


            
        }
        public IActionResult RequestAccept(int id,string email,string page)
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
            return RedirectToAction("ViewListBooking", "Booking", new {page=page});
        }
        public IActionResult RequestUnaccept(int id,string email, string page)
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
            return RedirectToAction("ViewListBooking", "Booking",new { page = page });
        }



        public IActionResult ViewDetailsInTouris(int id,string nametour,string message,string deposit)
        {
            String RoleID = HttpContext.Session.GetString("RoleID");
            ViewBag.RoleID = RoleID;
            
            Booking b = context.Bookings.Include(s => s.Hotel).Include(s => s.Vehicle).Include(s => s.Restaurant).Include(s => s.Tour).FirstOrDefault(t => t.Id==id &&t.Tour.Name == nametour&&t.HotelId==t.Hotel.Id&&
            t.VehicleId==t.Vehicle.Id&&t.RestaurantId==t.Restaurant.Id);
            ViewBag.bookings = b;
            ViewBag.message = message;
            ViewBag.deposit = deposit;
           var c = context.Schedules.Include(t => t.Tour).Include(t=>t.Location).Where(t => t.TourId == t.Tour.Id&&t.Tour.Name==nametour&&t.LocationId==t.Location.Id).ToList();
            ViewBag.schedules = c;

            return View();

        }
    }
}
