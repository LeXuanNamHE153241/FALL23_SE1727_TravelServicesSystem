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
        public List<Booking> GetListBooking()
        {
            List<Booking> listbooking = new List<Booking>();
            try
            {
                listbooking = context.Bookings.ToList();
                return listbooking;
            }
            catch
            {
                return listbooking;
            }
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

        public Boolean EditVehicle(Vehicle vehicle, string TypeVehicle, string PriceVehicle , string Rate, string Description)
        {
            try
            {
                Vehicle a = context.Vehicles.Where(x => x.Name == vehicle.Name.Trim()).FirstOrDefault();
                a.Type = TypeVehicle;
                a.Price = Convert.ToDouble(PriceVehicle);
                
                a.Rate = Convert.ToInt32(Rate);
                a.Description = Description;
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

        public void UpdateStaff(staff staffff)
        {
            staff? checkExist = context.staff.AsNoTracking().FirstOrDefault(stf => stf.Id == staffff.Id);
            if (checkExist != null)
            {
                try
                {
                    context.Entry(staffff).State = EntityState.Modified;
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





    }
}

