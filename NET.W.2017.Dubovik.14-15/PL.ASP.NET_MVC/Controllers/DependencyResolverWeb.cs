using DependencyResolver;
using NET.W._2017.Dubovik._14_15.AccountService;
using Ninject;

namespace PL.ASP.NET_MVC.Controllers
{
    public class DependencyResolverWeb
    {
        static  DependencyResolverWeb()
        {
            var ninjectKernel = new StandardKernel();
            AccountDependencyResolver.Configure(ninjectKernel);

            AccountService = ninjectKernel.Get<IAccountService>();
        }

        public static IAccountService AccountService { get; }
    }
}