using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface;
using PL.ASP.NET_MVC.Infrastructure;
using PL.ASP.NET_MVC.Providers;
using PL.ASP.NET_MVC.ViewModels;
using PL.ASP.NET_MVC.ViewModels.Authentication;

namespace PL.ASP.NET_MVC.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        private readonly IOwnerService ownerService;

        public AuthController(IOwnerService ownerService)
        {
            this.ownerService = ownerService;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        //// TO DO async
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogOnViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(viewModel.Email, viewModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, viewModel.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return this.Redirect(returnUrl);
                    }
                    else
                    {
                        return this.RedirectToAction("Index", "Account");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect login or password.");
                }
            }

            return this.View(viewModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return this.RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel viewModel)
        {
            if (viewModel.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Incorrect input.");
                return this.View(viewModel);
            }

            var anyUser = await Task.Run(() => this.ownerService.GetOwners().Any(u => u.Email.Contains(viewModel.Email)));

            if (anyUser)
            {
                ModelState.AddModelError(string.Empty, "User with this address already registered.");
                return this.View(viewModel);
            }

            if (ModelState.IsValid)
            {
                var membershipUser = await Task.Run(() => ((CustomMembershipProvider)Membership.Provider)
                    .CreateUser(viewModel.Email, viewModel.Password));

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, false);
                    return this.RedirectToAction("Index", "Account");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error registration.");
                }
            }

            return this.View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult Captcha()
        {
            this.Session[CaptchaImage.CaptchaValueKey] =
                new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString(CultureInfo.InvariantCulture);
            var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Helvetica");

            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            // Dispose of the CAPTCHA image object.
            ci.Dispose();
            return null;
        }

        [ChildActionOnly]
        public ActionResult LoginPartial()
        {
            return this.PartialView("_LoginPartial");
        }
    }
}