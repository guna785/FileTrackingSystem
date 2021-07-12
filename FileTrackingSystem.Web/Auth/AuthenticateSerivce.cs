using FileTrackingSystem.BL.Contract;
using FileTrackingSystem.BL.Models;
using FileTrackingSystem.Models.Enums;
using FileTrackingSystem.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileTrackingSystem.Web.Auth
{
    public interface IAuthenticateSerivce
    {
        Task<AuthenticatedModel> Auth(LoginViewModel login);
        Task Inialize(HttpContext httpContext);
    }
    public class AuthenticateSerivce : IAuthenticateSerivce
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AuthenticateSerivce> _logger;
        private readonly IUrlHelper _helper;
        private readonly IIdentityUserService _service;
        public AuthenticateSerivce(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AuthenticateSerivce> logger, IUrlHelper helper,
            IIdentityUserService service)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _helper = helper;
            _service = service;
        }
        public async Task<AuthenticatedModel> Auth(LoginViewModel login)
        {
            _logger.LogInformation("Authentication Begans ...");
            var res = await _signInManager.PasswordSignInAsync(login.uname, login.password, login.RememberMe, lockoutOnFailure: true);
            var Am = new AuthenticatedModel();
            Am.sign = res;
            if (res.Succeeded)
            {
                _logger.LogInformation("User {0} is Authenticated Successfully and authentication status {1}", login.uname, res);
                var usr = _userManager.Users.Where(x => x.UserName.Equals(login.uname)).FirstOrDefault();
                Am.name = usr.Name;
                Am.ID = usr.Id.ToString();
                Am.uname = usr.UserName;
                Am.role = usr.userType ==UserType.SuperAdmin ?"SuperAdmin"  : usr.userType==UserType.Admin ? "Admin" : "Worker";
            }
            else
            {
                _logger.LogInformation("User {0} Not Authorised and authentication status {1}", login.uname, res);
            }
            return Am;
        }

        public async Task Inialize(HttpContext httpContext)
        {
            var usr = _userManager.Users.Where(x => x.UserName == "admin").FirstOrDefault();
            if (usr == null)
            {
                var appUser = new ApplicationUser()
                {
                    userType = UserType.SuperAdmin,
                    Email = "guna@b2lsolutions.in",
                    Name = "SuperAdmin",
                    UserName = "root",

                };
                var res = await _userManager.CreateAsync(appUser, "Ashok@1991");
                if (res.Succeeded)
                {
                    await _service.ConfirmEmailGenerate(appUser, httpContext);
                }
            }

        }


    }
}
