//using AirlinesLibrary.Repos;
using AirlinesMvcApp.Filters;
using EFAirlinesLibrary.Repos;

namespace AirlinesMvcApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(config => config.Filters.Add(typeof(LogActionFilterAttribute)));
            builder.Services.AddControllersWithViews(config => config.Filters.Add(typeof(CustomExceptionFilter)));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}