using BLL.Interface.IDGenerator;
using DAL;
using DAL.Interface;
using NET.W._2017.Dubovik._14_15.AccountService;
using Ninject;

namespace DependencyResolver
{
    public class AccountDependencyResolver
    {
        public static void Configure(IKernel kernel)
        {
            kernel.Bind<IRepository>().To<BinaryFileRepository>()
                .WithConstructorArgument(@"accounts.bin");

            kernel.Bind<IIdGenerator>().To<IdGenerator>();

            var accountRepository = kernel.Get<IRepository>();
            var accountIdGenerator = kernel.Get<IIdGenerator>();
            kernel.Bind<IAccountService>().To<AccountService>()
                .WithConstructorArgument("accountRepository", accountRepository)
                .WithConstructorArgument("accountIdGenerator", accountIdGenerator);
        }
    }
}
