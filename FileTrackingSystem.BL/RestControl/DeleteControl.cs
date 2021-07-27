using FileTrackingSystem.BL.Contract;
using FileTrackingSystem.BL.Extentions;
using FileTrackingSystem.DAL.Contract;
using FileTrackingSystem.Models.Enums;
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
        private readonly IGenericDbService<Branch> _branch;
        private readonly IGenericDbService<Log> _log;
        private readonly UserManager<ApplicationUser> _user;
        private readonly ILogger<DeleteControl> _logger;
        public DeleteControl(IGenericDbService<Company> company, UserManager<ApplicationUser> user, ILogger<DeleteControl> logger,
            IGenericDbService<Log> log, IGenericDbService<Branch> branch)
        {
            _user = user;
            _company = company;
            _logger = logger;
            _log = log;
            _branch = branch;
        }

        public async Task<bool> DeleteBranch(int id, string user)
        {
            try
            {
                _logger.LogInformation("Branch Data Deeltion Starts ....");
                _logger.LogInformation("Branch Data Deletion Starts ....");
                _logger.LogInformation($"Checks Branch Id {id} Exists ");
                var brnch = _branch.FindById(id);
                _branch.Delete(brnch);
                _log.Create(MapperAction.CreateLog("Edit Branch", $"Branch {brnch.Name} is Updated successfully by {user}", user, LogType.Event));
                _logger.LogInformation("Branch Data Edition Done ....");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public async Task<bool> DeleteComapny(int id, string user)
        {
            try
            {
                _logger.LogInformation("Compay Data Deeltion Starts ....");
                _logger.LogInformation("Compay Data Deletion Starts ....");
                _logger.LogInformation($"Checks Company Id {id} Exists ");
                var cmp = _company.FindById(id);
                _company.Delete(cmp);
                _log.Create(MapperAction.CreateLog("Edit Company", $"Company {cmp.Name} is Updated successfully by {user}", user, LogType.Event));
                _logger.LogInformation("Compay Data Edition Done ....");
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public async Task<bool> DeletetUser(int id, string user)
        {
            var adm = _user.Users.Where(x => x.userType == FileTrackingSystem.Models.Enums.UserType.Admin).FirstOrDefault();
            await _user.DeleteAsync(adm);
            return true;
        }
    }
}
