using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CAMUS
{
    public class ExpireAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            DateTime dt = System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location);

            var nowTime = DateTime.Now;
            var remindTime = dt.AddDays(57);
            var expireTime = dt.AddDays(60);

            if ((remindTime - nowTime).Days == 0 && remindTime.CompareTo(expireTime) < 0)
            {
                httpContext.Session["remind"] = "网站访问权限还有3天到期，请联系技术。";
                return true;
            }

            if (nowTime.CompareTo(expireTime) > 0)
            {
                httpContext.Session["expire"] = "网站已不能访问，请联系技术。";
                return false;
            }


            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["expire"] != null)
            {
                ContentResult result = new ContentResult();
                result.Content = HttpContext.Current.Session["expire"].ToString();
                filterContext.Result = result;
            }
        }
    }
}