using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DependencyResolver;
using Ninject.Modules;

namespace PL.ASP.NET_MVC.DependencyResolver
{
    public class DependencyModule : NinjectModule
    {
        public override void Load() =>
            BankDependencyResolver.Configure(this.Kernel);
    }
}