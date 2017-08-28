using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CAMUS
{
    public class LoginAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session != null)
            {
                var session = httpContext.Session["user"];

                return session != null;
            }


            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            RedirectResult redirect = new RedirectResult("~/account");
            filterContext.Result = redirect;
        }
    }
}