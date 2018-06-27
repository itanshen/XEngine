using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XEngine.Web.Utility;
using System.Data.Entity;
using XEngine.Web.DAL;

namespace XEngine.Web.Utility.Filter
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            string roles = GetXMLRoles.GetActionRoles(actionName, controllerName);
            if (!string.IsNullOrEmpty(roles))
            {
                this.AuthRoles = roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                this.AuthRoles = new string[] { };
            }

            base.OnAuthorization(filterContext);
        }
        private string[] AuthRoles { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("HttpContext");
            }
            if (AuthRoles == null || AuthRoles.Length == 0)
            {
                return true;
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }
            #region 确定当前用户角色是否属于指定的角色
            string query = "select Name from SysRole where id in(select SysRoleId from SysUserRole where SysUserId in (select id from SysUser where UserName=@userName))";
            string currentUser = httpContext.User.Identity.Name;
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@userName",currentUser)
            };

            using (XEngineContext db = new XEngineContext())
            {
                var userRoles = db.Database.SqlQuery<string>(query, para).ToList();
                for (int i = 0; i < AuthRoles.Length; i++)
                {
                    if (userRoles.Contains(AuthRoles[i]))
                    {
                        return true;
                    }
                }
            }
            #endregion

            return false;

            //return base.AuthorizeCore(httpContext);
        }

    }
}