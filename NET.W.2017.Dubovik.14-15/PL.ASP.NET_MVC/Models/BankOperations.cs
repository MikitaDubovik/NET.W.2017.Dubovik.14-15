using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Accounts;

namespace PL.ASP.NET_MVC.Models
{
    public class BankOperations
    {
        [Display(Name = "Account type")]
        public string Type { get; set; }

        [HiddenInput]
        [Display(Name = "Account ID")]
        public string Id { get; set; }

        [Display(Name = "Initial sum")]
        public decimal Sum { get; set; }
    }
}