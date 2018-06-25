﻿using System.Web;
using System.Web.Mvc;

namespace XEngine.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            filters.Add(new XEngine.Web.Extensions.CustomAuthorizeAttribute());

        }
    }
}
