using FileTrackingSystem.BL.Contract;
using FileTrackingSystem.BL.Extentions;
using FileTrackingSystem.BL.SchemaModel;
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
    public class InsertControl : IInsert
    {
        private readonly IGenericDbService<Company> _company;
        private readonly IGenericDbService<Log> _log;
        private readonly UserManager<ApplicationUser> _user;
        private readonly ILogger<InsertControl> _logger;
        public InsertControl(IGenericDbService<Company> company, UserManager<ApplicationUser> user, ILogger<InsertControl> logger,
            IGenericDbService<Log> log)
        {
            _user = user;
            _company = company;
            _logger = logger;
            _log = log;
        }
        public async Task<bool> InsertCompany(CompanySchema model, string user)
        {
            try
            {
                _logger.LogInformation("Compay Data Addition Starts ....");
                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                var usr =await _user.FindByNameAsync(user);
                _company.Create(model.toCompany());
                _log.Create(MapperAction.CreateLog("Insert Company", $"Comapany {model.Name} is Added successfully by {usr.UserName}", usr.UserName, LogType.Event));
                _logger.LogInformation("Compay Data Addition Done ....");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());               
                return false;
            }
        }

        public async Task<bool> InsertEmployee(EmployeeSchema model, string user)
        {
            try
            {
                _logger.LogInformation("Employee Data Addition Starts ....");
                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                var res = await _user.CreateAsync(model.toEmployee(), model.password);
                if (res.Succeeded)
                {
                    _logger.LogInformation("Employee Data Addition Done ....");
                    return true;
                }
                _logger.LogError("Error : " + Newtonsoft.Json.JsonConvert.SerializeObject(res.Errors.ToList()));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public async Task<bool> InsertUser(UserSchema model, string user)
        {
            try
            {
                _logger.LogInformation("Admin User Data Addition Starts ....");
                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                var res = await _user.CreateAsync(model.toUser(), model.password);
                if (res.Succeeded)
                {
                    _logger.LogInformation("Admin User Data Addition Done ....");
                    return true;
                }
                _logger.LogError("Error : " + Newtonsoft.Json.JsonConvert.SerializeObject(res.Errors.ToList()));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }
    }
}
