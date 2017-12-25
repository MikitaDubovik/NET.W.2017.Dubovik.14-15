using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DependencyResolver;
using Ninject.Modules;

namespace PL.ASP.NET_MVC.DependencyResolver
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load() =>
            AccountDependencyResolver.Configure(this.Kernel);
    }
}