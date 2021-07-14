using FileTrackingSystem.BL.Contract;
using FileTrackingSystem.BL.Models;
using FileTrackingSystem.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.Helper
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<IdentityUserService> _logger;
        private readonly IUrlHelper _helper;
        public IdentityUserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<IdentityUserService> logger, IUrlHelper helper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _helper = helper;
        }
        public async Task<bool> ResetPasswordUrl(string email, HttpContext httpContext)
        {
            var usr = await _userManager.FindByEmailAsync(email);
            if (usr == null)
                return false;
            var token = await _userManager.GeneratePasswordResetTokenAsync(usr);
            var confirmationLink = "";
            try
            {
                confirmationLink = _helper.Action("ResetPassword", "Login", new { token }, httpContext.Request.Scheme);
                EmailHelper emailHelper = new EmailHelper();
                bool emailResponse = await emailHelper.SendEmail(usr.Email, confirmationLink);
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;

        }
        public async Task<bool> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userManager.
                 FindByNameAsync(model.UserName);

            var result = await _userManager.ResetPasswordAsync
                      (user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ConfirmEmailGenerate(ApplicationUser appUser, HttpContext httpContext)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            var confirmationLink = "";
            try
            {
                confirmationLink = _helper.Action("ConfirmEmail", "Login", new { token, email = appUser.Email }, httpContext.Request.Scheme);
            }
            catch (Exception ex)
            {

            }

            EmailHelper emailHelper = new EmailHelper();
            bool emailResponse = await emailHelper.SendEmail(appUser.Email, confirmationLink);
            return emailResponse;
        }

        public async Task<bool> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return false;

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded ? true : false;
        }
    }
}
