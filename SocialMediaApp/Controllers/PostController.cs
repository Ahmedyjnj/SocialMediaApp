using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Controllers.FilterationRole;
using SocialMediaApp.Models;

namespace SocialMediaApp.Controllers
{
    [UserCheckFilter]
    public class PostController : Controller
    {
        private readonly SocialMedia1Context context;

        public PostController(SocialMedia1Context app)
        {
            this.context = app;
        }


        public IActionResult Index()
        {
            var posts = context.Posts.Include(p => p.User).OrderByDescending(p => p.CreatedAt).ToList(); // جلب كل المنشورات
            return View(posts);
        }







        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Post post, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                post.UserId = int.Parse(HttpContext.Session.GetString("UserId")); // جلب معرف المستخدم من الجلسة


                if (imageFile != null && imageFile.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/posts", fileName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    // احفظ المسار النسبي في قاعدة البيانات
                    post.ImagePath = "/images/posts/" + fileName;
                }

                context.Posts.Add(post);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = HttpContext.Session.GetString("UserId"); //this is the current user

            var post = context.Posts.Include(p => p.User).FirstOrDefault(p => p.PostId == id); //this is the post we need to edit 

            if (int.Parse(userId)!=post.UserId)
            {
                //ViewBag.ErrorMessage = "You are not authorized to edit this post.";

                TempData["ErrorMessage"] = "You are not authorized to edit this post.";
                return RedirectToAction("Index");

            }
          

            return View(post);
        }

        [HttpPost]
        public IActionResult Edit(Post post, IFormFile imageFile)
        {
            
            if (ModelState.IsValid)
            {
                post.UserId = int.Parse(HttpContext.Session.GetString("UserId"));




                if (imageFile != null && imageFile.Length > 0)
                {
                    // إنشاء اسم فريد
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/posts", fileName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    // احفظ المسار النسبي للصورة
                    post.ImagePath = "/images/posts/" + fileName;

                }




                context.Posts.Update(post);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            var post = context.Posts.Include(p => p.User).FirstOrDefault(p => p.PostId == id);

            if (post == null)
            {
                return NotFound();
            }

            var userId = HttpContext.Session.GetString("UserId");

            if (int.Parse(userId) != post.UserId)
            {
                TempData["ErrorMessage"] = "You are not authorized to delete this post.";
                return RedirectToAction("Index");
            }

            return View(post);
        }



        [HttpPost, ActionName("Delete")]
       
        public IActionResult DeleteConfirmed(int id)
        {
            var post = context.Posts.Find(id);

            if (post == null)
            {
                return NotFound();
            }

            context.Posts.Remove(post);
            context.SaveChanges();
            TempData["SuccessMessage"] = "Post deleted successfully.";

            return RedirectToAction("Index");
        }

    }
}
