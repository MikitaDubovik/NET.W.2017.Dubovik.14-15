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
        private static string currentId;
        private static string currentOperation;
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
                decimal.Parse(account.Sum)));

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

        public ActionResult Withdraw()
        {
            currentOperation = "Withdraw";
            return ActiveOperation();
        }

        public ActionResult Deposite()
        {
            currentOperation = "Deposite";
            return ActiveOperation();
        }

        public ActionResult Close()
        {
            currentOperation = "Close";
            return ActiveOperation();
        }

        private ActionResult ActiveOperation()
        {
            var accounts = this.accountService.GetAccounts().Select(a => new MoneyOperations()
            {
                Id = a.Id,
                Sum = a.CurrentSum.ToString(),
                Type = GetTypeOfAccount(a.Id)
            });

            ViewBag.OperationName = currentOperation;
            return this.View("ViewTable", accounts);
        }

        [HttpGet]
        public ActionResult StartOperation(string id = "")
        {
            currentId = id;
            ViewBag.OperationName = currentOperation;
            ViewBag.Id = id;
            if (currentOperation.Equals("Close"))
            {
                return View("CloseOperation");
            }

            return View("MoneyOperation");
        }

        [HttpPost]
        public ActionResult EndMoneyOperation(MoneyOperations bank)
        {
            return End(bank);
        }

        [HttpPost]
        public ActionResult EndCloseOperation(CloseOperation bank)
        {
            return End(bank);
        }

        [HttpPost]
        public ActionResult End(IOperationModel bank)
        {
            if (!ModelState.IsValid)
            {
                TempData["isError"] = true;
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_NotValidData");
                }
                else
                {
                    return View("NotValidData");
                }
            }

            switch (currentOperation)
            {
                case "Withdraw":
                    accountService.WithdrawMoney(currentId, Convert.ToDecimal(bank.Sum));;
                    break;
                case "Deposite":
                    accountService.DepositMoney(currentId, Convert.ToDecimal(bank.Sum));
                    break;
                case "Close":
                    accountService.CloseAccount(currentId);
                    break;
                    default:
                        break;
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_OperationSuccessfullyComplete");
            }

            return View("OperationSuccessfullyComplete");
        }
        
        private string GetTypeOfAccount(string id)
        {
            string temp = this.accountService.GetTypeOfAccount(id);
            string[] tempArray = temp.Split('.');
            switch (tempArray.Last())
            {
                case "GoldAccount":
                    return "Gold Account";
                case "PlatinumAccount":
                    return "Platinum Account";
                default:
                    return "Base Account";
            }
        }

        public ActionResult Transfer() => this.View();
    }
}