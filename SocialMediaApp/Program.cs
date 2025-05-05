using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;

namespace SocialMediaApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region services
            // this will be used to store session data in cash of browser to check account
            builder.Services.AddDistributedMemoryCache();

            // add session details like time out and cookie 
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(40); // „œ… ’·«ÕÌ… «·Ã·”…
                options.Cookie.HttpOnly = true; // Ã⁄· «·ﬂÊﬂÌ“ €Ì— ﬁ«»·… ··Ê’Ê· ⁄»— Ã«›« ”ﬂ—Ì» 
                options.Cookie.IsEssential = true; // «·ﬂÊﬂÌ“ √”«”Ì…
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<SocialMedia1Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddHttpContextAccessor();
            #endregion


            #region pipelines HTTP request


            var app = builder.Build();

            app.UseSession();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Post}/{action=Index}/{id?}");

            app.Run();
            #endregion


        }
    }
}


// HttpContext.Session.SetString("UserEmail", userFromDb.Email);
//HttpContext.Session.SetString("UserRole", userFromDb.Role);

//this in actions to store the session data

//this for get the session data
//var userEmail = HttpContext.Session.GetString("UserEmail");
//var userRole = HttpContext.Session.GetString("UserRole");
