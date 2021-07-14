using FileTrackingSystem.BL.Contract;
using FileTrackingSystem.BL.Models;
using FileTrackingSystem.Web.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FileTrackingSystem.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IAuthenticateSerivce _authenticate;
        private readonly IIdentityUserService _service;
        public LoginController(IAuthenticateSerivce authenticate, IIdentityUserService service, ILogger<LoginController> logger)
        {
            _authenticate = authenticate;
            _service = service;
            _logger = logger;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            _authenticate.Inialize(HttpContext);
            ViewBag.err = "";
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel _user, string returnUrl)
        {
            var usr = await _authenticate.Auth(_user);
            if (usr.sign.Succeeded)
            {
                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, usr.uname));
                claims.Add(new Claim(ClaimTypes.Surname, usr.name));
                claims.Add(new Claim(ClaimTypes.Role, usr.role));
                var identity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.
        AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties();
                props.IsPersistent = _user.RememberMe;

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.
        AuthenticationScheme,
                    principal, props).Wait();

                return RedirectToAction("", "Home");
            }
            else if (usr.sign.IsLockedOut)
            {
                ViewBag.err = "User Locked Out for Multiple Wrong Attempts";
                return View();
            }
            else
            {
                ViewBag.err = "User Name / Password Error";
                return View();
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult ForgetPassword()
        {
            ViewBag.err = "";
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ResetPasswordView forget)
        {
            var res = await _service.ResetPasswordUrl(forget.email, HttpContext);
            ViewBag.err = res ? "Password reset url Sent to Email account : " + forget.email : "OOPS some Error Occured";
            return View();
        }
        [AllowAnonymous]
        public IActionResult ResetPassword(string token)
        {
            ViewBag.err = "";
            return View(new ResetPasswordViewModel() { Token = token });
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var res = await _service.ResetPassword(model);
            if (res)
            {
                ViewBag.Message = "Password reset successful!";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.err = "Error while resetting the password!";
                return View();
            }
        }
        [AllowAnonymous]
        public IActionResult signout()
        {
            HttpContext.SignOutAsync(
        CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("", "Login");
        }
    }
}
