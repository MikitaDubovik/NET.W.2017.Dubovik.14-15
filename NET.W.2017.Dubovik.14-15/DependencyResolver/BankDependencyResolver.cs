using BLL.Interface;
using BLL.Interface.IDGenerator;
using DAL;
using DAL.Interface;
using NET.W._2017.Dubovik._14_15.AccountService;
using NET.W._2017.Dubovik._14_15.OwnerService;
using Ninject;
using ORM;

namespace DependencyResolver
{
    public class BankDependencyResolver
    {
        public static void Configure(IKernel kernel)
        {
            ////kernel.Bind<IRepository>().To<BinaryFileRepository>()
            ////    .WithConstructorArgument(@"accounts.bin");

            kernel.Bind<IIdGenerator>().To<IdGenerator>();

            kernel.Bind<IRepository>().To<SQLRepository>();

            var accountRepository = kernel.Get<IRepository>();
            var accountIdGenerator = kernel.Get<IIdGenerator>();
            kernel.Bind<IAccountService>().To<AccountService>()
                .WithConstructorArgument("accountRepository", accountRepository)
                .WithConstructorArgument("accountIdGenerator", accountIdGenerator);
            kernel.Bind<IOwnerService>().To<OwnerService>()
                .WithConstructorArgument("accountRepository", accountRepository);
        }
    }
}
