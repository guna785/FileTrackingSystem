using FileTrackingSystem.BL.Models;
using FileTrackingSystem.Models.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.Contract
{
    public interface IIdentityUserService
    {
        Task<bool> ResetPasswordUrl(string email, HttpContext httpContext);
        Task<bool> ResetPassword(ResetPasswordViewModel model);
        Task<bool> ConfirmEmailGenerate(ApplicationUser appUser, HttpContext httpContext);
        Task<bool> ConfirmEmail(string token, string email);
    }
}
