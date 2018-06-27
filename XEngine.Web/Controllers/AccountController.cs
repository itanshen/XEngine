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
using XEngine.Web.Utility;
using XEngine.Web.Utility.Filter;
using PagedList;

namespace XEngine.Web.Controllers
{
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
            string userName = fc["UserName"];
            string password = fc["Password"];
            string encryptPwd = EncryptDecrypt.EncryptString(password);
            bool rememberMe = fc["RememberMe"] == null ? false : true;
            string returnUrl = Convert.ToString(TempData["ReturnUrl"]);

            SysUser user = unitOfWork.SysUserRepository.Get(filter: u => u.UserName == userName
                && (u.Password == password || u.Password == encryptPwd)).FirstOrDefault();
            unitOfWork.Dispose();

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, rememberMe);
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

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(SysUser user)
        {
            user.Password = EncryptDecrypt.EncryptString(user.Password);
            user.ModifiedDate = DateTime.Now;
            unitOfWork.SysUserRepository.Insert(user);
            unitOfWork.Save();
            return View();
        }

    }
}