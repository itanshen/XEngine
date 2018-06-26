using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XEngine.Web.DAL;
using XEngine.Web.Models;

namespace XEngine.Web.Controllers
{
    //[CustomAuthorize]
    public class HomeController : Controller
    {
        private XEngineContext db = new XEngineContext();

        public ActionResult Index()
        {
            //var user = db.SysRoles.Count();

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

        public ActionResult Battle()
        {
            //Samurai model = new Samurai(new Sword());
            //ViewBag.Hit = model.Hit("Scott");

            //1. 创建一个Ninject的内核实例
            IKernel ninjectKernel = new StandardKernel();
            //2. 配置Ninject内核,指明接口需绑定的类
            ninjectKernel.Bind<IWeapon>().To<Sword>();
            //3. 根据上一步的配置创建一个对象
            var weapon = ninjectKernel.Get<IWeapon>();

            var warrior1 = new Samurai(weapon);
            ViewBag.Hit = warrior1.Hit("the evildoers");

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}