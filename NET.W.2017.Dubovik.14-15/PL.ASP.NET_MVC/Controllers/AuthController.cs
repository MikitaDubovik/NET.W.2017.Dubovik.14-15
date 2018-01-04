using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using PL.ASP.NET_MVC.Infrastructure;
using PL.ASP.NET_MVC.Providers;
using PL.ASP.NET_MVC.ViewModels;


namespace PL.ASP.NET_MVC.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        public AuthController(/*IUserRepository repository*/)
        {
           // this.repository = repository;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
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
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Account");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
            }

            return View(viewModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //// TO DO async
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (viewModel.Captcha != (string) Session[CaptchaImage.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Incorrect input.");
                return View(viewModel);
            }

            var anyUser = true;//repository.GetAllUsers().Any(u => u.Email.Contains(viewModel.Email));

            if (anyUser)
            {
                ModelState.AddModelError("", "User with this address already registered.");
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                var membershipUser = ((CustomMembershipProvider) Membership.Provider)
                    .CreateUser(viewModel.Email, viewModel.Password);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, false);
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Error registration.");
                }
            }

            return View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult Captcha()
        {
            Session[CaptchaImage.CaptchaValueKey] =
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
            return PartialView("_LoginPartial");
        }
    }
}