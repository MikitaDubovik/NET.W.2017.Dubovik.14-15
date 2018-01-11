using DependencyResolver;
using Ninject.Modules;

namespace PL.ASP.NET_MVC.Infrastructure
{
    public class DependencyModule : NinjectModule
    {
        public override void Load() =>
            BankDependencyResolver.Configure(this.Kernel);
    }
}