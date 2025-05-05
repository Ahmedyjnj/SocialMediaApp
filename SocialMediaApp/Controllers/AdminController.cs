using Microsoft.AspNetCore.Mvc;
using SocialMediaApp.Models;

namespace SocialMediaApp.Controllers
{
    [AdminCheckFilter]
    public class AdminController : Controller
    {
        private readonly SocialMedia1Context context;

        public AdminController(SocialMedia1Context context)
        {
            this.context = context;
        }


        public IActionResult Index()
        {
            return View(context.Users.ToList());
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user, IFormFile photoFile)
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

               
                // معالجة رفع الصورة
                if (photoFile != null && photoFile.Length > 1)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profiles");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        photoFile.CopyTo(stream);
                    }

                    // حفظ المسار النسبي للصورة في قاعدة البيانات
                    user.ProfilePhoto = "/Images/users/" + uniqueFileName;
                }


                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }



        [HttpPost]
        public IActionResult Edit(User user, IFormFile photoFile)
        {
            if (ModelState.IsValid)
            {

                if (photoFile != null && photoFile.Length > 1)
                {
                    // إنشاء اسم فريد للصورة
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "users");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);

                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        photoFile.CopyTo(stream);
                    }

                    // حفظ مسار الصورة في الداتا بيز
                    user.ProfilePhoto = "/Images/users/" + uniqueFileName;
                }


                context.Users.Update(user);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }



        
        public IActionResult Details(int id)
        {
            var user = context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }





        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = context.Users.Find(id);

            return View(user);
        }



        [HttpPost,ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            var user = context.Users.Find(id);
                context.Users.Remove(user);
                context.SaveChanges();
                
            return RedirectToAction("Index");
          
            
        }














    }
}
