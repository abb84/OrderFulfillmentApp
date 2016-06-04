
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RepZio.Ofa.Presentation.Web.OrderFulfillment.App_Start;

namespace RepZio.Ofa.Presentation.Web.OrderFulfillment
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
