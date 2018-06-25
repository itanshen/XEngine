using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XEngine.Web.DAL;
using XEngine.Web.Extensions;

namespace XEngine.Web.Controllers
{
    //[CustomAuthorize]
    public class HomeController : Controller
    {
        private XEngineContext db = new XEngineContext();

        public ActionResult Index()
        {
            var user = db.SysRoles.Count();
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}