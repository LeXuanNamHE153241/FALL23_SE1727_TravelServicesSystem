namespace TravelSystem_SWP391
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllersWithViews();
			var app = builder.Build();
			app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=index}/{id?}"

	);
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			//app.MapRazorPages();
			app.Run();
		}
	}
}