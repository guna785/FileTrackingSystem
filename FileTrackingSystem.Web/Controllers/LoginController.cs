using FileTrackingSystem.BL.Contract;
using FileTrackingSystem.BL.Models;
using FileTrackingSystem.Models.Models;
using FileTrackingSystem.Web.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public LoginController(IAuthenticateSerivce authenticate, IIdentityUserService service, ILogger<LoginController> logger,
            SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _authenticate = authenticate;
            _userManager = userManager;
            _service = service;
            _logger = logger;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            await _authenticate.Inialize(HttpContext);
            ViewBag.err = "";
            return View(model);
        }
      
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel _user, string returnUrl)
        {
            var usr = await _authenticate.Auth(_user);
            LoginViewModel model = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
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
                return View(model);
            }
            else
            {
                ViewBag.err = "User Name / Password Error";
                return View(model);
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
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var res = await _service.ConfirmEmail(token, email);

            ViewBag.user = res ? email : "";
            return View(res ? "ConfirmEmail" : "Error");
        }
        [AllowAnonymous]
        public IActionResult signout()
        {
            HttpContext.SignOutAsync(
        CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("", "Login");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallBack", "Login", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallBack(string returnUrl, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            LoginViewModel model = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error From External Provider : {remoteError}");
                return View("Index", model);
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading External Login Information");
                return View("Index", model);
            }

            var res = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (res.Succeeded)
            {
               
            }
            else
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                if (email != null)
                {
                    var usr = await _userManager.FindByEmailAsync(email);
                    if (usr == null)
                    {
                        usr = new ApplicationUser()
                        {
                            UserName = email,
                            Email = email
                        };
                        await _userManager.CreateAsync(usr);
                    }
                    await _userManager.AddLoginAsync(usr, info);
                    await _signInManager.SignInAsync(usr, isPersistent: false);
                }
            }

            return View("Index", model);

        }
    }
}
