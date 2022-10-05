using System.Web;
using System.Web.Mvc;

namespace Claro.SIACU.Web.WebApplication.IFI
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
