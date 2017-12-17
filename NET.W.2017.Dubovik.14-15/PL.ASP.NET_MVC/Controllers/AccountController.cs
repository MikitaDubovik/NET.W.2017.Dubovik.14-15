using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DependencyResolver;
using NET.W._2017.Dubovik._14_15.AccountService;
using Ninject;

namespace PL.ASP.NET_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IKernel NinjectKernel;
        private readonly IAccountService iAccountService;

        private AccountController()
        {
            iAccountService = DependencyResolverWeb.AccountService;
        }

    // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}