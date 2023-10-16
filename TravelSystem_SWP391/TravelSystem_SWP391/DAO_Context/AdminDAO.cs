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
                Tour a = context.Tours.Where(x => x.Id == tour.Id).FirstOrDefault();
                context.Tours.Remove(a);
                context.SaveChanges();
                return true;
            }
            catch
            {
            }
            return false;
        }
        public Boolean EditTour(Tour tour, string PriceTour, string Description)
        {
            try
            {
                Tour a = GetTourByName(tour.Name.Trim());

                a.Price = Convert.ToDouble(PriceTour);
                a.Description = Description;

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

