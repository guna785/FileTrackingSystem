using FileTrackingSystem.BL.Contract;
using FileTrackingSystem.DAL.Contract;
using FileTrackingSystem.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.RestControl
{
    public class DeleteControl : IDelete
    {
        private readonly IGenericDbService<Company> _company;
        private readonly UserManager<ApplicationUser> _user;
        private readonly ILogger<DeleteControl> _logger;
        public DeleteControl(IGenericDbService<Company> company, UserManager<ApplicationUser> user, ILogger<DeleteControl> logger)
        {
            _user = user;
            _company = company;
            _logger = logger;
        }
        public async Task<bool> DeleteComapny(int id, string user)
        {
            var cmp = _company.FindById(id);
            _company.Delete(cmp);
            return true;
        }

        public async Task<bool> DeletetUser(int id, string user)
        {
            var adm = _user.Users.Where(x => x.userType == FileTrackingSystem.Models.Enums.UserType.Admin).FirstOrDefault();
            await _user.DeleteAsync(adm);
            return true;
        }
    }
}
