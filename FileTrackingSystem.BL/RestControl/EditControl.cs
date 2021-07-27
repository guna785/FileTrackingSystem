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
    public class EditControl : IEdit
    {
        private readonly IGenericDbService<Company> _company;
        private readonly UserManager<ApplicationUser> _user;
        private readonly ILogger<EditControl> _logger;
        private readonly IGenericDbService<Log> _log;
        public EditControl(IGenericDbService<Company> company, UserManager<ApplicationUser> user, ILogger<EditControl> logger,
            IGenericDbService<Log> log)
        {
            _user = user;
            _company = company;
            _logger = logger;
            _log = log;
        }

        public async Task<bool> EditCompany(CompanySchema model, string user)
        {
            try
            {
                _logger.LogInformation("Compay Data Edition Starts ....");
                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                _logger.LogInformation("Compay Data Addition Starts ....");
                _logger.LogInformation($"Checks Company Name {model.Name} Exists ");
                var cmp = _company.FindById(model.Id);
                if (cmp != null)
                {
                    cmp.Name = model.Name;
                    cmp.Pan = model.Pan;
                    cmp.Address = model.Address;
                    cmp.BankAccNo = model.BankAccNo;
                    cmp.BankBranch = model.BankBranch;
                    cmp.BankName = model.BankName;
                    cmp.Contact = model.Contact;
                    cmp.Email = model.Email;
                    cmp.GST = model.GST;
                    cmp.HSN = model.HSN;
                    cmp.IFSC = model.IFSC;
                    cmp.status = model.status;
                    cmp.TIN = model.TIN;
                    cmp.Web = model.Web;

                    _company.Create(cmp);
                    _log.Create(MapperAction.CreateLog("Edit Company", $"Company {model.Name} is Updated successfully by {user}", user, LogType.Event));
                    _logger.LogInformation("Compay Data Edition Done ....");
                    return true;
                }
                else
                {
                    _logger.LogError($"Compay {model.Name} not Exoists....");
                    return false;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
           
        }

        public Task<bool> EditUser(UserSchema model, string user)
        {
            throw new NotImplementedException();
        }
    }
}
