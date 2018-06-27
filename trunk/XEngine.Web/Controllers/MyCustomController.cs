using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using XEngine.Web.Utility;

namespace XEngine.Web.Controllers
{
    public class MyCustomController : Controller
    {
        //public void Execute(RequestContext requestContext)
        //{
        //    requestContext.HttpContext.Response.Write("Hello World.");
        //}
        public ActionResult NativeOutput()
        {
            return Redirect("~/Account/Login");
        }
        public CustomRedirectResult CustomOutput()
        {
            return new CustomRedirectResult { Url = "~/Account/Login" };
        }
        public HttpStatusCodeResult StatusCode()
        {
            return new HttpStatusCodeResult(404, "URL cannot beserviced");
        }
        public HttpStatusCodeResult NotFoundStatusCode()
        {
            return HttpNotFound();
        }
        public HttpStatusCodeResult UnauthorizedStatusCode()
        {
            return new HttpUnauthorizedResult();
        }
    }
}