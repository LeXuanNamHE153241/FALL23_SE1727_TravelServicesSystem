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
    public class TouristDAO
    {
        traveltestContext context = new traveltestContext();
        public Tour getTour(int ID)
        {
            try
            {
                Tour tour = context.Tours.Where(x => x.Id == ID).FirstOrDefault();
                if (tour != null)
                {
                    return tour;
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
    }
}
