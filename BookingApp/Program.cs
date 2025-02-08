using BookingApp.BookingDbContext;
using BookingApp.Models;
using Microsoft.AspNetCore.Identity;

using BookingApp.Data;
using Microsoft.EntityFrameworkCore;
using BookingApp.Repository.Services;
using BookingApp.Repository.Implimentation;

namespace BookingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
           
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IRoomInterface, RoomService>();

            builder.Services.AddDbContext<RoomBookingContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddDbContext<BookingAppContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Login")));

            // Use AddDefaultIdentity (handles user accounts only)
            builder.Services.AddDefaultIdentity<BookingAppUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<BookingAppContext>();


            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Add Authentication and Authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
