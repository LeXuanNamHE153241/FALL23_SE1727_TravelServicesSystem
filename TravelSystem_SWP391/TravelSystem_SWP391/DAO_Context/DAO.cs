using Microsoft.Data.SqlClient;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Principal;
using System.Text;

using System.Net.Mail;

using System.Text.RegularExpressions;
using TravelSystem_SWP391.Models;



namespace TravelSystem_SWP391.DAO_Context
{
    public class DAO
    {
        traveltestContext context = new traveltestContext();

        public User Login(string userName, string pass)
        {
            try
            {
                User account = context.Users.Where(x => x.Email.Trim().ToLower().Equals(userName.Trim().ToLower()) == true && x.Password.Trim().ToLower().Equals(pass.Trim().ToLower()) == true).FirstOrDefault();
                if (account != null)
                {
                    return account;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }
        public List<Vehicle> GetListVehicle()
        {
            List<Vehicle> listvehicle = new List<Vehicle>();
            try
            {
                listvehicle = context.Vehicles.ToList();
                return listvehicle;
            }
            catch
            {
                return listvehicle;
            }
        }
        public List<Hotel> GetListHotel()
        {
            List<Hotel> listhotel = new List<Hotel>();
            try
            {
                listhotel = context.Hotels.ToList();
                return listhotel;
            }
            catch
            {
                return listhotel;
            }
        }
        public List<Restaurant> GetListRes()
        {
            List<Restaurant> listres = new List<Restaurant >();
            try
            {
                listres = context.Restaurants.ToList();
                return listres;
            }
            catch
            {
                return listres;
            }
        }
        public List<Booking> GetListBooking()
        {
            List<Booking> listbooking = new List<Booking>();
            try
            {
                listbooking = context.Bookings.Include(s => s.Hotel).Include(s => s.Vehicle).Include(s => s.Restaurant).Include(s => s.Tour).Where(s => s.VehicleId == s.Vehicle.Id && s.HotelId == s.Hotel.Id).ToList();
                return listbooking;
            }
            catch
            {
                return listbooking;
            }
        }
        public List<Booking> GetListBookingByEmail(string email)
        {
            List<Booking> listbookingbyrole = new List<Booking>();

            try
            {
                listbookingbyrole = context.Bookings.Include(s => s.Hotel).Include(s => s.Vehicle).Include(s => s.Restaurant).Include(s => s.Tour).Where(s => s.Email == email && s.VehicleId == s.Vehicle.Id && s.HotelId == s.Hotel.Id && DateTime.Compare(s.EndDate,DateTime.Now)<=0).ToList();
                return listbookingbyrole;
            }
            catch
            {
                return listbookingbyrole;
            }
        }
        public List<Booking> GetListHistoryBookingByEmail(string email)
        {
            List<Booking> listbookingbyrole = new List<Booking>();
            List<Vehicle> listvehicle = new List<Vehicle>();
            
            try
            {
              
                    listbookingbyrole = context.Bookings.Include(s => s.Hotel).Include(s => s.Vehicle).Include(s =>s.Restaurant).Include(s => s.Tour).Where(s => s.Email == email && s.VehicleId == s.Vehicle.Id && s.HotelId == s.Hotel.Id && DateTime.Compare(s.EndDate, DateTime.Now) > 0).ToList();
               


                return listbookingbyrole;
            }
            catch
            {
                return listbookingbyrole;
            }
        }
        public Booking GetListBookingByID(int ID)
        {
            try
            {
                Booking Booking = context.Bookings.Where(x => x.Id == ID).FirstOrDefault();
                if (Booking != null)
                {
                    return Booking;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }
        public Boolean EditMessBooking(Booking booking,string sttThanhToan)
        {
            try
            {
                Booking a = context.Bookings.Where(x => x.Id == booking.Id).FirstOrDefault();
                a.Message = sttThanhToan;
                context.SaveChanges();
                return true;
            }
            catch
            {
            }
            return false;
        }

        public List<staff> GetListStaffByRole(string role)
        {
            List<staff> liststaffbyrole = new List<staff>();
            
            try
            {
                liststaffbyrole = context.staff.Where(s => s.RoleStaff==role).ToList();
                return liststaffbyrole;
            }
            catch
            {
                return liststaffbyrole;
            }
        }


        public List<Tour> GetTours()
		{
			return context.Tours.ToList();
		}


        public List<Tour> GetAllTours()
        {
            List<Tour> listtours = new List<Tour>();
            try
            {
                listtours = context.Tours.ToList();
                return listtours;
            }
            catch
            {
                return listtours;
            }
        }
        public List<Country> GetAllCountry()
        {
            List<Country> listct = new List<Country>();
            try
            {
                listct = context.Countries.ToList();
                return listct;
            }
            catch
            {
                return listct;
            }
        }


        public bool IsEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool IsPhoneNumberValidVietnam(string phoneNumber)
        {
            // Define a regular expression pattern for Vietnamese phone numbers
            // This pattern assumes the country code is +84 and follows the format 10 digits after that.
            string pattern = @"^0[0-9]{9}$";

            // Use Regex.IsMatch to check if the phone number matches the pattern
            return Regex.IsMatch(phoneNumber, pattern);
        }
        public bool IsEmailValid(string emailAddress)
        {
            // Define a regular expression pattern for a basic email address format
            // This is a simplified pattern and may not cover all edge cases
            string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            // Use Regex.IsMatch to check if the email address matches the pattern
            return Regex.IsMatch(emailAddress, pattern);
        }
        public Boolean ChangePass(User account, string newPass)
        {
            try
            {
                User a = context.Users.Where(x => x.Email == account.Email.Trim() && x.Password == account.Password.Trim()).FirstOrDefault();
                a.Password = newPass;
                context.SaveChanges();
                return true;
            }
            catch
            {
            }
            return false;
        }
        public Hotel getHotel(int ID)
        {
            try
            {
                Hotel hotel = context.Hotels.Where(x => x.Id == ID).FirstOrDefault();
                if (hotel != null)
                {
                    return hotel;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }

        public User getUser(string Email)
        {
            try
            {
                User users = context.Users.Where(x => x.Email == Email).FirstOrDefault();
                if (users != null)
                {
                    return users;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }
        public Vehicle getVihecle(int ID)
        {
            try
            {
                Vehicle vehicle = context.Vehicles.Where(x => x.Id == ID).FirstOrDefault();
                if (vehicle != null)
                {
                    return vehicle;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }


        public bool IsValidFirstnameorLastname(string name)
        {
            // You can add your validation rules here.
            // Example: Check if the firstname is not empty and contains only letters.
            return !string.IsNullOrWhiteSpace(name) && IsAlphaOnly(name);
        }

        public bool IsAlphaOnly(string input)
        {
            // Check if the input string contains only letters.
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }
        public void ForgotPassWord(staff staffff)
        {
            try
            {
                context.staff.Add(staffff);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }




        public static Vehicle SearchVehiclesByName(string VehicleName, List<Vehicle> listvehicle)
        {

            foreach (Vehicle rs in listvehicle)
                if (VehicleName == rs.Name)
                    return rs; // <- The product found

            return null; // <- There's no such a product in the list
        }

        public Boolean EditUser(User user, string FirstName, string LastName, string PhoneNumber, string Description, string Gender )
        {
            try
            {
                User u = context.Users.Where(x => x.Email == user.Email.Trim()).FirstOrDefault();
                u.LastName = LastName;
                u.FirstName = FirstName;
                u.PhoneNumber = PhoneNumber;
                u.Description = Description;
                u.Gender = Gender;
                context.SaveChanges();
                return true;
            }
            catch
            {
            }
            return false;
        }
        public Boolean EditVehicle(Vehicle vehicle,string NameVehicle, string TypeVehicle, string PriceVehicle , string Rate, string Description)
        {
            try
            {
                Vehicle a = context.Vehicles.Where(x => x.Name == vehicle.Name.Trim()).FirstOrDefault();

                a.Type = TypeVehicle;
                a.Price = Convert.ToDouble(PriceVehicle);
                a.Name = NameVehicle;
                a.Rate = Convert.ToInt32(Rate);
                a.Description = Description;
                if (a.Price>0&&a.Rate>0&&a.Rate<=5)
                {
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
                
                
            }
            catch
            {
            }
            return false;
        }

        public Boolean EditRestaurant(Restaurant restaurant, string AddressRestaurant,string Phone, string PriceRestaurant, string Rate, string Description)
        {
            try
            {
                Restaurant r = context.Restaurants.Where(x => x.Name == restaurant.Name.Trim()).FirstOrDefault();
                r.Address = AddressRestaurant;
                r.Price = PriceRestaurant;
                r.Phone = Phone;
                r.Rate = Convert.ToInt32(Rate);
                r.Description = Description;
                context.SaveChanges();
                return true;
            }
            catch
            {
            }
            return false;
        }
        public Boolean EditHotel(Hotel hotel, string AddressHotel, string City, string Country, string Phone, string Rating, string Review, string RoomTypes, string Amenities)
        {
            try
            {
                Hotel h = context.Hotels.Where(x => x.Name == hotel.Name.Trim()).FirstOrDefault();
                h.Address = AddressHotel;
                h.City = City;
                h.Country = Country;
                h.Phone = Phone;
                h.Rating = Convert.ToDouble(Rating);
                h.Review = Review;
                h.RoomTypes = RoomTypes;
                h.Amenities = Amenities;
                context.SaveChanges();
                return true;
            }
            catch
            {
            }
            return false;
        }

        public Boolean DeleteVehicle(Vehicle vehicle)
        {
            try
            {
                Vehicle a = context.Vehicles.Where(x => x.Id == vehicle.Id).FirstOrDefault();
                context.Vehicles.Remove(a);
                context.SaveChanges();
                return true;
            }
            catch
            {
            }
            return false;
        }
        public Boolean DeleteRestaurant(Restaurant restaurant)
        {
            try
            {
                Restaurant a = context.Restaurants.Where(x => x.Id == restaurant.Id).FirstOrDefault();
                context.Restaurants.Remove(a);
                context.SaveChanges();
                return true;
            }
            catch
            {
            }
            return false;
        }
        public Boolean DeleteHotel(Hotel hotel)
        {
            try
            {
                Hotel a = context.Hotels.Where(x => x.Id == hotel.Id).FirstOrDefault();
                context.Hotels.Remove(a);
                context.SaveChanges();
                return true;
            }
            catch
            {
            }
            return false;
        }




        public static Tour SearchToursByName(string ToursName, List<Tour> listtours)

        {
            foreach (Tour tour in listtours)
                if (ToursName == tour.Name)
                    return tour;

            return null;
        }

        public List<staff> GetStaffs()
        {
            List<staff> staffs = context.staff.Include(p => p.EmailUserNavigation).ToList();
            if (staffs.Count > 0)
            {
                return staffs;
            }
            else
            {
                return null;
            }
        }

        public void AddStaff(staff staffff)
        {
            try
            {
                context.staff.Add(staffff);
                Save();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddUserStaff(User user)
        {
            try
            {
                context.Users.Add(user);
                Save();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateStaff(User staffff)
        {
            User? checkExist = GetUserByEmail(staffff.Email);
            if (checkExist != null)
            {
                try
                {
                    checkExist.FirstName = staffff.FirstName;
                    checkExist.LastName = staffff.LastName;
                    checkExist.PhoneNumber = staffff.PhoneNumber;
                    checkExist.Description = staffff.Description;

                    Save();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Exception("Staff doesn't exist!");
            }
        }

        //Khong thuc su xoa Staff. Chi can thay roleId = 100 la xong
        public void RemoveStaff(string staffEmail)
        {
            User? checkExist = context.Users.FirstOrDefault(stf => stf.Email == staffEmail);
            if (checkExist != null)
            {
                try
                {
                    checkExist.RoleId = 100;
                    Save();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Exception("Staff doesn't exist!");
            }
        }

        public staff SearchStaff(string staffEmail)
        {
            var staffs = context.staff.Include(p => p.EmailUserNavigation).FirstOrDefault(stf => stf.EmailUser.ToLower().Contains(staffEmail.ToLower()));
            return staffs;

        }

        public void Save()
        {
            context.SaveChanges();
        }

        public User GetUserByEmail(string userEmail)
        {
            var users = context.Users.FirstOrDefault(us => us.Email.ToLower().Equals(userEmail.ToLower()));
            return users;
        }

        public Hotel GetHotel(int id)
        {
            var hotel = context.Hotels.FirstOrDefault(us => us.Id.Equals(id));
            return hotel;
        }

        public Restaurant GetRestaurant(int id)
        {
            var res = context.Restaurants.FirstOrDefault(us => us.Id.Equals(id));
            return res;
        }

        public Vehicle GetVehicle(int id)
        {
            var ve = context.Vehicles.FirstOrDefault(us => us.Id.Equals(id));
            return ve;
        }

        public Tour GetTour(int id)
        {
            var to = context.Tours.FirstOrDefault(us => us.Id.Equals(id));
            return to;
        }

        public FeedbackDetail DetailFeedback(string emailUser, string feedbackName, string feedbackMessage, string feedbackSubject, string feedbackResponce)
        {
            FeedbackDetail fbdetail = new FeedbackDetail();

            Feedback fb = new Feedback();
            fb.Email = emailUser;
            fb.Name = feedbackName;
            fb.Message = feedbackMessage;
            fb.Subject = feedbackSubject;
            fb.Response = feedbackResponce;

            if (feedbackSubject == "Hotel")
            {
                Hotel ht = GetHotel(1);
                fbdetail.Hotel = ht;
            }
            else if (feedbackSubject == "Restaurant")
            {
                Restaurant re = GetRestaurant(1);
                fbdetail.Restaurant = re;
            }
            else if (feedbackSubject == "Vehicle")
            {
                Vehicle v = GetVehicle(1);
                fbdetail.Vehicle = v;
            }
            else if (feedbackSubject == "Tour")
            {
                Tour t = GetTour(1);
                fbdetail.Tour = t;
            }

            fbdetail.User = GetUserByEmail(emailUser);
            fbdetail.Feedback = fb;
            return fbdetail;
        }

        public void UpdateFeedback(Feedback fb)
        {
            Feedback checkExist = context.Feedbacks.Where
            (e =>
                e.Email == fb.Email &&
                e.Name == fb.Name &&
                e.Subject == fb.Subject &&
                e.Message == fb.Message
            ).FirstOrDefault();

            if (checkExist != null)
            {
                try
                {
                    checkExist.Response = fb.Response;
                    Save();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                throw new Exception("fb doesn't exist!");
            }
        }

        public Tour GetTourById(int id)
        {
            var tour = context.Tours.FirstOrDefault(stf => stf.Id.Equals(id));
            return tour;
        }

        public List<Hotel> GetListHotelByName(string nameTour)
        {
            List<Hotel> hotels = context.Hotels.Where(x => x.City.ToLower().Equals(nameTour.ToLower())).ToList();
            return hotels;
        }

        public List<Restaurant> GetListRestaurantByName(string nameTour)
        {
            List<Restaurant> restaurants = context.Restaurants.Where(x => x.Address.ToLower().Contains(nameTour.ToLower())).ToList();
            return restaurants;
        }

        public List<Vehicle> GetListVehicleForBooking()
        {
            List<Vehicle> vehs = context.Vehicles.ToList();
            return vehs;
        }

        public List<staff> GetStaffsIsWorking()
        {
            List<staff> staffs = context.staff.Include(p => p.EmailUserNavigation).Where(x => x.EmailUserNavigation.RoleId.Equals(4)).ToList();
            if (staffs.Count > 0)
            {
                return staffs;
            }
            else
            {
                return null;
            }
        }

        public void AddBooking(Booking newTour)
        {
            try
            {
                context.Bookings.Add(newTour);
                Save();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}

