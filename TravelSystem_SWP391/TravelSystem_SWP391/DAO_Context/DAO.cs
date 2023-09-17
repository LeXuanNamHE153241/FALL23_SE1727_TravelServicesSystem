using System.Security.Principal;
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
	}
}