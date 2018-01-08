using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using NET.W._2017.Dubovik._14_15.AccountService;
using PL.ASP.NET_MVC.Models;

namespace PL.ASP.NET_MVC.Controllers
{
    ////[Authorize]
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
            return this.ActiveOperation();
        }

        public ActionResult Deposite()
        {
            currentOperation = "Deposite";
            return this.ActiveOperation();
        }

        public ActionResult Close()
        {
            currentOperation = "Close";
            return this.ActiveOperation();
        }

        [HttpGet]
        public ActionResult StartOperation(string id = "")
        {
            currentId = id;
            ViewBag.OperationName = currentOperation;
            ViewBag.Id = id;
            if (currentOperation.Equals("Close"))
            {
                return this.View("CloseOperation");
            }

            return this.View("MoneyOperation");
        }

        [HttpPost]
        public ActionResult EndMoneyOperation(MoneyOperations bank)
        {
            return this.End(bank);
        }

        [HttpPost]
        public ActionResult EndCloseOperation(CloseOperation bank)
        {
            return this.End(bank);
        }

        public ActionResult Transfer() => this.View();

        [HttpPost]
        private ActionResult End(IOperationModel bank)
        {
            if (!ModelState.IsValid)
            {
                this.TempData["isError"] = true;
                if (Request.IsAjaxRequest())
                {
                    return this.PartialView("_NotValidData");
                }
                else
                {
                    return this.View("NotValidData");
                }
            }

            switch (currentOperation)
            {
                case "Withdraw":
                    this.accountService.WithdrawMoney(currentId, Convert.ToDecimal(bank.Sum));
                    break;
                case "Deposite":
                    this.accountService.DepositMoney(currentId, Convert.ToDecimal(bank.Sum));
                    break;
                case "Close":
                    this.accountService.CloseAccount(currentId);
                    break;
                    default:
                        break;
            }

            if (Request.IsAjaxRequest())
            {
                return this.PartialView("_OperationSuccessfullyComplete");
            }

            return this.View("OperationSuccessfullyComplete");
        }

        private ActionResult ActiveOperation()
        {
            var accounts = this.accountService.GetAccounts().Select(a => new MoneyOperations()
            {
                Id = a.Id,
                Sum = a.CurrentSum.ToString(),
                Type = this.GetTypeOfAccount(a.Id)
            });

            ViewBag.OperationName = currentOperation;
            return this.View("ViewTable", accounts);
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
    }
}