using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XEngine.Web.DAL;
using XEngine.Web.Models;
using XEngine.Web.Repositories;
using PagedList;

namespace XEngine.Web.Controllers
{
    public class UserController : Controller
    {
        //private ISysUserRepository userRepository = new SysUserRepository(new XEngineContext());

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
                users = users.Where(u => u.Name.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(u => u.Name).ToList();
                    break;
                default:
                    users = users.OrderBy(u => u.Name).ToList();
                    break;
            }
            int pageSize = 2;
            int pageNumber = page ?? 1;

            return View(users.ToPagedList(pageNumber, pageSize));
        }

    }
}