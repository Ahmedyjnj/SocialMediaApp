using SocialMediaApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace SocialMediaApp.Security
{
    public class Roles
    {
        private readonly IHttpContextAccessor accessor;

        public Roles(IHttpContextAccessor accessor) // this is a constructor to initialize the accessor to use session out of controller
        {
            this.accessor = accessor;
        }


        public bool IsAdmin() // this a method to check if the user is admin or not 
        {

            var role = accessor.HttpContext.Session.GetString("UserRole");

            return role == "admin";
           
        }


       












    }
}
