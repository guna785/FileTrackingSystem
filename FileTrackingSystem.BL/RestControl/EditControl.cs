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
        private readonly IGenericDbService<Branch> _branch;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<ApplicationRole> _role;
        private readonly ILogger<EditControl> _logger;
        private readonly IGenericDbService<Log> _log;
        public EditControl(IGenericDbService<Company> company, UserManager<ApplicationUser> user, ILogger<EditControl> logger,
            IGenericDbService<Log> log, IGenericDbService<Branch> branch,RoleManager<ApplicationRole> role)
        {
            _user = user;
            _company = company;
            _logger = logger;
            _log = log;
            _branch = branch;
            _role = role;
        }

        public async Task<bool> EditBranch(BranchSchema model, string user)
        {
            try
            {
                _logger.LogInformation("Branch Data Edition Starts ....");
                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                _logger.LogInformation("Branch Data Addition Starts ....");
                _logger.LogInformation($"Checks Branch Id for {model.Name} Exists ");
                var brnch = _branch.FindById(model.Id);
                var cmp = _company.AsQueryable().Where(x => x.Name == model.CompanyId).FirstOrDefault();
                if (brnch != null)
                {
                    brnch.Name = model.Name;
                    brnch.CompanyId = cmp.Id;
                    _branch.Update(brnch);
                    _log.Create(MapperAction.CreateLog("Edit Branch", $"Branch {model.Name} is Updated successfully for Company {model.CompanyId} by {user}", user, LogType.Event));
                    _logger.LogInformation("Branch Data Edition Done ....");
                    return true;
                }
                else
                {
                    _logger.LogError($"Branch {model.Name} not Exoists....");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
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

                    _company.Update(cmp);
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

        public Task<bool> EditEmployee(EmployeeSchema model, string user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditRole(RoleSchema model, string user)
        {
            try
            {
                _logger.LogInformation("Role Data Edition Starts ....");
                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                _logger.LogInformation("Role Data Addition Starts ....");
                _logger.LogInformation($"Checks Role Name {model.Name} Exists ");
                var rol = await _role.FindByIdAsync(model.Id.ToString());
                if (rol != null)
                {
                    rol.Name = model.Name;
                    rol.description = model.Discription;

                   var res= await _role.UpdateAsync(rol);
                    if (res.Succeeded)
                    {
                        _log.Create(MapperAction.CreateLog("Edit Role", $"Role {model.Name} is Updated successfully by {user}", user, LogType.Event));
                        _logger.LogInformation("Role Data Edition Done ....");
                        return true;
                    }
                    else
                    {
                        _logger.LogError("Error : " + Newtonsoft.Json.JsonConvert.SerializeObject(res.Errors.ToList()));
                        return false;
                    }
                   
                }
                else
                {
                    _logger.LogError($"Compay {model.Name} not Exoists....");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public async Task<bool> EditUser(UserSchema model, string user)
        {
            try
            {
                _logger.LogInformation("Admin Data Edition Starts ....");
                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                _logger.LogInformation("Admin Data Addition Starts ....");
                _logger.LogInformation($"Checks Admin Name {model.Name} Exists ");
                var usr = await _user.FindByIdAsync(model.Id.ToString());
                if (usr != null)
                {
                    var brnch = _branch.AsQueryable().Where(x => x.Name == model.branchId).FirstOrDefault();
                    usr.Name = model.Name;
                    usr.PhoneNumber = model.phone;
                    usr.Email = model.email;
                    usr.gender = model.gender;
                    usr.UserName = model.userName;
                    usr.branchId = brnch.Id;
                    usr.CompanyId = brnch.CompanyId;
                    if (!string.IsNullOrEmpty(model.password))
                    {
                        usr.PasswordHash= _user.PasswordHasher.HashPassword(usr, model.password);
                    }
                    
                    await _user.UpdateAsync(usr);
                    _log.Create(MapperAction.CreateLog("Edit Admin", $"Admin {model.Name} is Updated successfully by {user}", user, LogType.Event));
                    _logger.LogInformation("Admin Data Edition Done ....");
                    return true;
                }
                else
                {
                    _logger.LogError($"Compay {model.Name} not Exoists....");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }

        }
    }
}
