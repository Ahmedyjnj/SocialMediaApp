using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SocialMediaApp.Controllers.FilterationRole
{
    public class UserCheckFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = context.HttpContext.Session.GetString("UserRole");


            if (role == null || ( role.ToUpper() != "USER" && role.ToUpper() !="ADMIN"))
            {
                // Redirect to home if not admin
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
            base.OnActionExecuting(context);// if it is admin it go and pass the constrain filter  // this will call the base class method to continue the action execution
        }




    }
}
