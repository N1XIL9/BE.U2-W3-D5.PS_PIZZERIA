using System.Web;
using System.Web.Mvc;

namespace BE.U2_W3_D5.PS_PIZZERIA
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
