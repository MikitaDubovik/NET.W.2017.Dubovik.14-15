using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BLL.Interface.Accounts;

namespace PL.ASP.NET_MVC.Models
{
    public class AccountWeb
    {
        [Display(Name = "Account type")]
        [Required(ErrorMessage = "Field must be selected", AllowEmptyStrings = false)]
        public AccountsLevel Type { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        public string OwnerFirstName { get; set; }

        [Display(Name = "Second name")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        public string OwnerSecondName { get; set; }

        [Display(Name = "Initial sum")]
        [Required(ErrorMessage = "Field must not be empty", AllowEmptyStrings = false)]
        [Range(typeof(decimal), "100", "50000", ErrorMessage = "The initial sum must be at least 100 and not more than 50,000")]
        public string Sum { get; set; }
    }
}