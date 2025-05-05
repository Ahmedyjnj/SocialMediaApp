using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;

namespace SocialMediaApp.Controllers
{
    public class UserController : Controller
    {
        private readonly SocialMedia1Context context;

        public UserController(SocialMedia1Context context)
        {
            this.context = context;
        }

        public IActionResult ShowPage(int id)
        {
            var user = context.Users.Include(u=>u.Posts).FirstOrDefault(u=>u.UserId==id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


    }
}
