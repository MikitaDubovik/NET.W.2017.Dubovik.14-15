using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Accounts;

namespace PL.ASP.NET_MVC.Models
{
    public class MoneyOperations : IOperationModel
    {
        [Display(Name = "Account type")]
        public string Type { get; set; }

        [HiddenInput]
        [Display(Name = "Account ID")]
        public string Id { get; set; }

        [Display(Name = "Quantity of monetary units")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [Range(typeof(decimal), "100", "20000", ErrorMessage = "The initial sum must be at least 100 and not more than 50,000")]
        public string Sum { get; set; }
    }
}