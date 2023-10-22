using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using TravelSystem_SWP391.Models;



namespace TravelSystem_SWP391.DAO_Context
{
    public class AdminDAO
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
        public Boolean DeleteTour(Tour tour)
        {
            try
            {
                context.Tours.Remove(tour);
                context.SaveChanges();
                return true;
            }
            catch
            {
            }
            return false;
        }
        public Boolean EditTour(Tour tour)
        {
            try
            {

                context.Attach(tour).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch
            {
            }
            return false;
        }
        public Tour GetTourByName(string name)
        {
            return context.Tours.FirstOrDefault(v => v.Name == name);
        }
        public Tour GetTourById(int id)
        {
            return context.Tours.FirstOrDefault(v => v.Id == id);
        }
        public void AddTour(Tour tour)
        {
            context.Add(tour);
            context.SaveChanges();
        }
        public List<staff> GetListStaff()
        {
            List<staff> listStaff = new List<staff>();
            try
            {
                listStaff = context.staff.ToList();
                return listStaff;
            }
            catch
            {
                return listStaff;
            }
        }
        public List<Restaurant> GetListRestaurant()
        {
            List<Restaurant> listRestaurant = new List<Restaurant>();
            try
            {
                listRestaurant = context.Restaurants.ToList();
                return listRestaurant;
            }
            catch
            {
                return listRestaurant;
            }
        }
        public List<Vehicle> GetListVehicle()
        {
            List<Vehicle> listVehicle = new List<Vehicle>();
            try
            {
                listVehicle = context.Vehicles.ToList();
                return listVehicle;
            }
            catch
            {
                return listVehicle;
            }
        }
        public List<Tour> GetListTour()
        {
            List<Tour> listTour = new List<Tour>();
            try
            {
                listTour = context.Tours.ToList();
                return listTour;
            }
            catch
            {
                return listTour;
            }
        }

        public List<User> GetListUser()
        {
            List<User> listUser = new List<User>();
            try
            {
                listUser = context.Users.ToList();
                return listUser;
            }
            catch
            {
                return listUser;
            }
        }
        public List<Feedback> GetListFeedBack()
        {
            List<Feedback> listFeedback = new List<Feedback>();
            try
            {
                listFeedback = context.Feedbacks.ToList();
                return listFeedback;
            }
            catch
            {
                return listFeedback;
            }
        }





    }
}

