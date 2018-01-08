using System;
using BLL.Interface.Accounts;
using DependencyResolver;
using NET.W._2017.Dubovik._14_15.AccountService;
using Ninject;

namespace ConsolePL
{
    public class Program
    {
        private static readonly IKernel NinjectKernel;

        static Program()
        {
            NinjectKernel = new StandardKernel();
            AccountDependencyResolver.Configure(NinjectKernel);
        }

        public static void Main(string[] args)
        {
            try
            {
                var accountService = NinjectKernel.Get<IAccountService>();
                Test(accountService);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        private static void Test(IAccountService accountService)
        {
            var idFirst = accountService.OpenAccount("Another", "One", 100);
            accountService.DepositMoney(idFirst, 322);
            accountService.WithdrawMoney(idFirst, 228);
            Console.WriteLine(accountService.GetAccountStatus(idFirst));

            Console.WriteLine();

            var idSecond = accountService.OpenAccount(AccountsLevel.Platinum, "Bites", "The Dust", 444);

            accountService.DepositMoney(idSecond, 10);
            accountService.WithdrawMoney(idSecond, 20);
            Console.WriteLine(accountService.GetAccountStatus(idSecond));

            Test(idFirst, idSecond, accountService);
        }

        private static void Test(string otherIdFirst, string otherIdSecond, IAccountService accountService)
        {
            foreach (var account in accountService.GetAccounts())
            {
                Console.WriteLine(account.OwnerFirstName);
            }

            var idFirst = otherIdFirst;
            var idSecond = otherIdSecond;
            Console.WriteLine("After searching");
            Console.WriteLine();
            Console.WriteLine(accountService.GetAccountStatus(idFirst));
            Console.WriteLine();
            Console.WriteLine(accountService.GetAccountStatus(idSecond));
        }
    }
}
