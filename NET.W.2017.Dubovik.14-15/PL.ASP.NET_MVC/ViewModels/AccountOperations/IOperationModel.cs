using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.ASP.NET_MVC.Models
{
    public interface IOperationModel
    {
        string Id { get; set; }

        string Sum { get; set; }
    }
}