using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CAMUS.Models;

namespace CAMUS.Controllers
{
    public class AccountController : Controller
    {
        private CAMUSContext db = new CAMUSContext();

        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Message = "用户名或密码不能为空";
                return View("Login");
            }

            password = PasswordToBase64(password);
            var user = db.User.SingleOrDefault(u => u.Name == username && u.Password == password);
            if (user != null)
            {
                Session["User"] = user;
                return RedirectToAction("News","Admin");
            }
            ViewBag.Message = "用户名或密码错误";
            return View("Login");
        }

        private string PasswordToBase64(string pwd)
        {
            if (string.IsNullOrEmpty(pwd)) return "";
            byte[] pwdByte = System.Text.Encoding.Default.GetBytes(pwd);
            return Convert.ToBase64String(pwdByte);
        }
    }
}
