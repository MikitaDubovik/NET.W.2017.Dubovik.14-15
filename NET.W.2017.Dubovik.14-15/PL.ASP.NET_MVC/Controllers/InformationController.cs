using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.ASP.NET_MVC.Controllers
{
    public class InformationController : Controller
    {
        // GET: Information
        /// <summary>
        /// Shows contacts for communication
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact() => View();

        /// <summary>
        /// Shows information about this project
        /// </summary>
        /// <returns></returns>
        public ActionResult About() => View();
    }
}