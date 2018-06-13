using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using XEngine.Web.Models;
using System.Web.Security;
using XEngine.Web.DAL;

namespace XEngine.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Account/Login
        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            TempData["ReturnUrl"] = Convert.ToString(Request["ReturnUrl"]);
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string userName = fc["inputUserName"];
            string password = fc["inputPassword"];
            string encryptPwd = Utility.EncryptDecrypt.EncryptString(password);
            bool rememberMe = fc["ckbRememberMe"] == null ? false : true;
            string returnUrl = Convert.ToString(TempData["ReturnUrl"]);

            SysUser user = unitOfWork.SysUserRepository.Get(filter: u => u.Name == userName
                && (u.Password == password || u.Password == encryptPwd)).FirstOrDefault();
            unitOfWork.Dispose();

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(userName, rememberMe);
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("~/");
                }
            }
            else
            {
                ViewBag.LoginState = "用户名或密码错误，请重试";
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}