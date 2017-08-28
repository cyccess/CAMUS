using CAMUS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CAMUS.Controllers
{
    [ExpireAuthorize]
    public class HomeController : Controller
    {
        private CAMUSContext db = new CAMUSContext();

        public ActionResult Index()
        {
            var list = db.Banner.Where(b => !b.IsDelete).ToList();

            return View(list);
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Distribution()
        {

            return View();
        }

        public ActionResult BrandInformation(string id = "BrandInformation")
        {
            ViewBag.Title = "品牌信息";

            return View(id);
        }

        public ActionResult News(int newsId = 0, string id = "detail")
        {
            CAMUSContext db = new CAMUSContext();
            if (newsId != 0)
            {
                var detail = db.News.Find(newsId);
                return View(id, detail);
            }
            var news = db.News.Where(n => !n.IsDelete).ToList();
            return View(news);
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult JoinUs()
        {
            return View();
        }
    }
}