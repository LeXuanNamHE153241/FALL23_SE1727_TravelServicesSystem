using System.Net;
using System.Net.Mail;
using System.Security.Principal;
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
        public List<Vehicle> GetVehicle()
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

        public static bool SendEmail(string fromEmail, string toEmail, string subject, string body, string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(smtpServer);
                smtpClient.Port = smtpPort;
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true; // Enable SSL for secure communication with the SMTP server

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromEmail);
                mailMessage.To.Add(toEmail);
                mailMessage.Subject = subject;
                mailMessage.Body = body;

                smtpClient.Send(mailMessage);
                return true; // Email sent successfully
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false; // Email sending failed
            }
        }

    }
}

