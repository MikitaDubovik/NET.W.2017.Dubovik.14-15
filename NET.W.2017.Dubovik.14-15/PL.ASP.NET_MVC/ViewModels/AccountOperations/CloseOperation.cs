using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PL.ASP.NET_MVC.ViewModels.AccountOperations
{
    public class CloseOperation : IOperationModel
    {
        [HiddenInput]
        [Display(Name = "Account ID")]
        public string Id { get; set; }

        [HiddenInput]
        [Display(Name = "Quantity of monetary units")]
        public string Sum { get; set; }
    }
}