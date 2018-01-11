﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using Ninject.Web.Mvc;

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

            //var registrations = new DependencyModule();
            //var kernel = new StandardKernel(registrations);
            //kernel.Unbind<ModelValidatorProvider>();
            //System.Web.Mvc.DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            System.Web.Mvc.DependencyResolver.SetResolver(new Infrastructure.NinjectDependencyResolver());
        }

        protected void Application_EndRequest()
        {
        }
    }
}
