﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PL.ASP.NET_MVC.Models
{
    public class CountMoney
    {
        [Display(Name = "Amount of money")]
        [Range(typeof(decimal), "1", "20000", ErrorMessage = "The sum must be at least 1 and not more than 20.000")]
        public string Count { get; set; }
    }
}