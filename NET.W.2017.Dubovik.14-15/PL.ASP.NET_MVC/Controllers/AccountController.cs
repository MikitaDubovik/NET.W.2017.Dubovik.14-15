using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DependencyResolver;
using NET.W._2017.Dubovik._14_15.AccountService;
using Ninject;
using PL.ASP.NET_MVC.Models;

namespace PL.ASP.NET_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

       public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

    // GET: Account
        public ActionResult Index()
        {
            return this.View();
        }



        public ActionResult OpenAccount() => this.View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(View = "AccountServiceError")]
        public async Task<ActionResult> OpenAccount(AccountWeb account)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            await Task.Run(() => this.accountService.OpenAccount(
                account.Type,
                account.OwnerFirstName,
                account.OwnerSecondName,
                account.Sum));

            this.TempData["isAccountOpened"] = true;
            return this.RedirectToAction(nameof(this.AccountSuccessfullyOpened));
        }

        [HttpGet]
        public ActionResult AccountSuccessfullyOpened()
        {
            var isAccountOpened = TempData["isAccountOpened"] as bool?;
            if (!isAccountOpened.HasValue || !isAccountOpened.Value)
            {
                return this.RedirectToAction(nameof(this.OpenAccount));
            }

            this.TempData["isAccountOpened"] = false;
            return this.View();
        }

        public ActionResult Deposite()
        {
            var accounts = this.accountService.GetAccounts().Select(a => new BankOperations()
            {
                Id = a.Id,
                Sum = a.CurrentSum
            });

            //TO DO 
            foreach (var account in accounts)
            {
                account.Type = this.accountService.GetTypeOfAccount(account.Id);
            }
            return this.View(accounts);
        }

        public ActionResult Withdraw() => this.View();

        public ActionResult Transfer() => this.View();

        public ActionResult Close() => this.View();
    }
}