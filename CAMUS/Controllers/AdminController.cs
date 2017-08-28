using CAMUS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CAMUS.Controllers
{
    [LoginAuthorize]
    public class AdminController : Controller
    {
        private CAMUSContext db = new CAMUSContext();

        public ActionResult SetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SetPassword(string oldPwd, string newPwd)
        {
            var user = (User)Session["User"];

            string pwd = PasswordToBase64(oldPwd);
            var model = db.User.SingleOrDefault(u => u.Name == user.Name && u.Password == pwd);

            if (model == null)
            {
                ViewBag.Message = "旧密码错误";
                return View();
            }

            model.Password = PasswordToBase64(newPwd);
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            ViewBag.Message = "密码修改成功";

            return View();
        }


        private string PasswordToBase64(string pwd)
        {
            if (string.IsNullOrEmpty(pwd)) return "";
            byte[] pwdByte = System.Text.Encoding.Default.GetBytes(pwd);
            return Convert.ToBase64String(pwdByte);
        }

        public ActionResult Banner()
        {
            var list = db.Banner.Where(b => !b.IsDelete).ToList();
            return View(list);
        }

        [HttpPost]
        public ActionResult BannerDelete(int id = 0)
        {
            if (id > 0)
            {
                var entity = db.Banner.Find(id);
                entity.IsDelete = true;
                db.SaveChanges();
                return Content("Success");
            }
            return Content("Failure");
        }

        [HttpPost]
        public ActionResult BannerUpload()
        {
            HttpPostedFileBase file = Request.Files["image"];
            if (file != null)
            {
                var newFileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
                var fileName = Path.Combine(Request.MapPath("~/images"), newFileName);
                file.SaveAs(fileName);

                var banner = new Banner();
                banner.Path = "/images/" + newFileName;
                banner.CreateDate = DateTime.Now;
                banner.Order = 0;
                db.Banner.Add(banner);
                db.SaveChanges();

                return Json(new { code = 100, path = banner.Path, bannerId = banner.Id });
            }

            return Json(new { code = 1001, message = "上传错误！" });
        }

        [HttpPost]
        public ActionResult NewsImageUpload()
        {
            HttpPostedFileBase file = Request.Files["newsImage"];
            if (file != null)
            {
                var newFileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
                var fileName = Path.Combine(Request.MapPath("~/images"), newFileName);
                file.SaveAs(fileName);
                return Json(new { code = 100, path = "/images/" + newFileName });
            }

            return Json(new { code = 1001, message = "上传错误！" });
        }

        // GET: News 
        public ActionResult News()
        {
            return View(db.News.ToList());
        }

        // GET: News/Details/5
        public ActionResult NewsDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        public ActionResult NewsCreate()
        {
            return View();
        }

        // POST: News/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult NewsCreate(News news)
        {
            if (ModelState.IsValid)
            {
                news.CreateDate = DateTime.Now;
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("News");
            }

            return View(news);
        }

        // GET: News/Edit/5
        public ActionResult NewsEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult NewsEdit(News news)
        {
            if (ModelState.IsValid)
            {
                news.CreateDate = DateTime.Now;
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("News");
            }
            return View(news);
        }

        // GET: News/Delete/5
        public ActionResult NewsDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("NewsDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            news.IsDelete = true;
            db.SaveChanges();
            return RedirectToAction("News");
        }

        [HttpPost]
        public ActionResult NewsImgUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return Content("没有文件！", "text/plain");
            }

            var fileName = Path.Combine(Request.MapPath("~/images/news"), Path.GetFileName(file.FileName));
            try
            {
                file.SaveAs(fileName);
                //tm.AttachmentPath = fileName;//得到全部model信息
                string imgPath = "../images/news/" + Path.GetFileName(file.FileName);
                //return Content("上传成功！", "text/plain");
                return Json("");
            }
            catch
            {
                return Content("上传异常 ！", "text/plain");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}