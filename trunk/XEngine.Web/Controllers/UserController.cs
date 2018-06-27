using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XEngine.Web.DAL;
using XEngine.Web.Models;
using XEngine.Web.Repositories;
using PagedList;
using XEngine.Web.Utility;
using XEngine.Web.Utility.Filter;
using System.Net;
using XEngine.Web.ViewModels;
using System.Text;
using System.Data.SqlClient;

namespace XEngine.Web.Controllers
{
    [CustomAuthorize]
    public class UserController : Controller
    {
        //private ISysUserRepository userRepository = new SysUserRepository(new XEngineContext());
        private XEngineContext db = new XEngineContext();
        ////
        //// GET: /User/
        //public ActionResult Index()
        //{
        //    var users = userRepository.GetUsers();
        //    return View(users);
        //}

        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /User/
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            //var users = unitOfWork.SysUserRepository.Get(filter: u => u.Name.Contains("s"), orderBy: q => q.OrderBy(u => u.Name));
            var users = unitOfWork.SysUserRepository.Get();
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.UserName.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(u => u.UserName).ToList();
                    break;
                default:
                    users = users.OrderBy(u => u.UserName).ToList();
                    break;
            }
            int pageSize = 2;
            int pageNumber = page ?? 1;

            return View(users.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SysUser user)
        {
            if (ModelState.IsValid)
            {
                user.Password = EncryptDecrypt.EncryptString(user.Password);
                user.ModifiedDate = DateTime.Now;
                unitOfWork.SysUserRepository.Insert(user);
                unitOfWork.Save();
                unitOfWork.Dispose();
                return RedirectToAction("Index");
            }
            return View();
        }


        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "删除出错，请重试。";
            }
            SysUser user = unitOfWork.SysUserRepository.GetByID(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);

        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                unitOfWork.SysUserRepository.Delete(id);
                unitOfWork.Save();
                unitOfWork.Dispose();
            }
            catch (Exception)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SysUser user = unitOfWork.SysUserRepository.GetByID(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            //绑定IsActive的值
            List<SelectListItem> selectIsActive = new List<SelectListItem> { 
                new SelectListItem{Text="是",Value="Y"},
                new SelectListItem{Text="否",Value="N"}
            };
            ViewData["selectIsActive"] = new SelectList(selectIsActive, "Value", "Text", user.IsActive);
            BindAssignedRoleData(user);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,UserName,Email,Password,CName,Description,IsActive")] SysUser user, FormCollection fc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.ModifiedDate = DateTime.Now;
                    user.IsActive = fc["selectIsActive"];
                    if (!string.IsNullOrEmpty(user.Password))
                    {
                        user.Password = EncryptDecrypt.DecryptString(user.Password);
                    }
                    unitOfWork.SysUserRepository.Update(user);
                    unitOfWork.Save();
                    unitOfWork.Dispose();
                }
                string rolelist = fc["selectedUsers"];
                UpdateUserRoles(user.ID.ToString(), rolelist);
            }
            catch (Exception)
            {

                throw;
            }
            return Redirect("~/User/Edit/" + user.ID);
        }

        private void BindAssignedRoleData(SysUser user)
        {
            var allRoles = unitOfWork.SysRoleRepository.Get();
            var userRoleIDs = new HashSet<int>(user.SysUserRoles.Select(u => u.SysRoleID));
            var viewModel = new List<AssignedRoleData>();
            foreach (var role in allRoles)
            {
                viewModel.Add(new AssignedRoleData
                {
                    RoleID = role.ID,
                    RoleName = role.Name,
                    Assigned = userRoleIDs.Contains(role.ID)
                });
            }
            ViewBag.Roles = viewModel;
        }
        /// <summary>
        /// 根据用户id, 及该用户下的角色列表 更新 用户的角色
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="memberList">角色列表id</param>
        private void UpdateUserRoles(string userId, string memberList)
        {
            //根据userId删除该用户下的所有角色，插入该用户下的下新的角色列表
            string[] roleids = { };
            if (memberList != null)
            {
                roleids = memberList.Split(new char[] { ',' });
            }
            StringBuilder sb = new StringBuilder();
            if (userId != null)
            {
                sb.Append("DELETE FROM [dbo].[SysUserRole] WHERE [SysUserID]=@userId AND 1=1;");
            }
            for (int i = 0; i < roleids.Length; i++)
            {
                sb.Append("INSERT INTO [dbo].[SysUserRole] ([SysUserID],[SysRoleID],[ModifiedDate]) VALUES (@userId," + roleids[i] + ",GETDATE());");
            }

            string sql = sb.ToString();
            SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("@userId",userId)
            };
            db.Database.ExecuteSqlCommand(sql, paras);
        }
    }
}