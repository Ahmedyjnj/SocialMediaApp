using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Models;

namespace SocialMediaApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SocialMedia1Context context;

        public AccountController(SocialMedia1Context context)
        {
            this.context = context;
        }




        [HttpGet]
        public IActionResult Login()
        {
            
            return View();

        }



        [HttpPost]
        public IActionResult Login(User user)
        {
            //with email and password go in 

            var userFromDb = context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);

            if (userFromDb != null)
            {


                HttpContext.Session.SetString("UserName", userFromDb.UserName);
                HttpContext.Session.SetString("UserRole", userFromDb.Role);
                HttpContext.Session.SetString("UserId", userFromDb.UserId.ToString());

                

                if (userFromDb.Role == "Admin")
                {


                    // Redirect to the admin dashboard
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    // Redirect to the user dashboard
                    return RedirectToAction("Index", "Post");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(user);

            }


        }




        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // تمسح كل البيانات اللي في الجلسة
            return RedirectToAction("Login", "Account");
        }












        [HttpGet]
        public IActionResult Registeration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registeration(User user)
        {
            if (ModelState.IsValid)
            {
                if (context.Users.Any(u => u.UserName == user.UserName)) //this will sure that the name is unique
                {
                    ModelState.AddModelError("UserName", "name is already taken.");
                    return View(user);
                }
                if (context.Users.Any(u => u.Email == user.Email)) //this will sure that the email is unique
                {
                    ModelState.AddModelError("Email", "Email is already taken.");
                    return View(user);
                }
              
                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }












        public IActionResult Index()
        {
            return View();
        }
    }
}
