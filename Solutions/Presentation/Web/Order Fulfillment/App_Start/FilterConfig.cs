using System.Web;
using System.Web.Mvc;

namespace RepZio.Ofa.Presentation.Web.OrderFulfillment.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
