using System.Web;
using System.Web.Mvc;
using PointNet.Web.Core.ActionFilters;

namespace PointNet.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
