using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XEngine.Web.DAL;
using XEngine.Web.Models;
using PagedList;
using XEngine.Web.Utility.Filter;
using System.Data;

namespace XEngine.Web.Controllers
{
    //[CustomAuthorize]
    public class HomeController : Controller
    {
        private XEngineContext db = new XEngineContext();
        private IWeapon weapon;
        public HomeController(IWeapon weaponParam)
        {
            weapon = weaponParam;
        }

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

            ////1. 创建一个Ninject的内核实例
            //IKernel ninjectKernel = new StandardKernel();
            ////2. 配置Ninject内核,指明接口需绑定的类
            //ninjectKernel.Bind<IWeapon>().To<Sword>();
            ////3. 根据上一步的配置创建一个对象
            //var weapon = ninjectKernel.Get<IWeapon>();

            var warrior1 = new Samurai(weapon);
            object Hit = warrior1.Hit("the evildoers");

            return View(Hit);
        }

        [HttpPost]
        public string AjaxTest(string userName, string email)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            IEnumerable<SysUser> users = unitOfWork.SysUserRepository.Get(filter: o => o.UserName.Contains(userName));

            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<object> listReturn = new List<object>();
            foreach (var item in users)
            {
                listReturn.Add(new
                {
                    ID = item.ID,
                    UserName = item.UserName,
                    Email = item.Email,
                    ModifiedDate = item.ModifiedDate.ToString()
                });
            }
            List<object> listA = new List<object>();
            foreach (var item in users)
            {
                listA.Add(new
                {
                    a = GetObjectPropertyValue(item, "ID")
                });
            }
            List<object> listTable = new List<object>();
            DataTable dt = GetCreateDataTable();
            foreach (DataRow item in dt.Rows)
            {
                listTable.Add(new
                {
                    a = item["a"],
                    b = item["B"],
                    c = item["C"]
                });
            }
            string result = js.Serialize(new
            {
                list = listReturn,
                property = listA,
                propertys = GetPropertiesList<SysUser>(users.ToList(), "ID"),
                dataTable = listTable,
                err = "null"
            });

            return result;
            //return js.Serialize(listReturn);
        }

        public static string GetObjectPropertyValue<T>(T t, string propertyname)
        {
            string str = "";
            Type type = typeof(T);

            System.Reflection.PropertyInfo property = type.GetProperty(propertyname);

            if (property == null) return string.Empty;

            object o = property.GetValue(t, null);

            if (o == null) return string.Empty;

            str = o.ToString();
            return str;
        }
        public List<string> GetPropertiesList<T>(List<T> listData, string comboText)
        {
            List<string> list = new List<string>();
            foreach (var item in listData)
            {
                Type type = typeof(T);
                System.Reflection.PropertyInfo property = type.GetProperty(comboText);
                if (property == null)
                {
                    list.Add(string.Empty);
                }
                else
                {
                    object o = property.GetValue(item, null);
                    if (o == null)
                    {
                        list.Add(string.Empty);
                    }
                    else
                    {
                        list.Add(o.ToString());
                    }
                }

            }
            return list;
        }
        protected DataTable GetCreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("A", typeof(int));
            dt.Columns.Add("B", typeof(string));
            dt.Columns.Add("C", typeof(string));

            for (var i = 0; i < 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i;
                dr[1] = "B" + i;
                dr[2] = "B" + i;
                dt.Rows.Add(dr);
            }

            return dt;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}