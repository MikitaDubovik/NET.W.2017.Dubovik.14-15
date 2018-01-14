using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PL.ASP.NET_MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            System.Web.Mvc.DependencyResolver.SetResolver(new Infrastructure.NinjectDependencyResolver());

            ModelValidatorProviders.Providers.Clear();
        }

        protected void Application_EndRequest()
        {
        }
    }
}
