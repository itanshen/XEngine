using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XEngine.Web.Utility.BreadCrumbSiteMap
{
    public static class MvcSiteMapExtensions
    {
        public static MvcHtmlString PopulateBreadcrumb(this HtmlHelper helper, string url)
        {
            return MvcSiteMapHelper.PopulateBreadcrumb(url);
        }
    }
}