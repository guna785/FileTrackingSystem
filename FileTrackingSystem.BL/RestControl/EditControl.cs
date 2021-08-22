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
        private readonly IGenericDbService<JobType> _jbType;
        private readonly IGenericDbService<DocumentRequired> _docReq;
        private readonly IGenericDbService<Document> _doc;
        private readonly IGenericDbService<Client> _client;
        private readonly IGenericDbService<Branch> _branch;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<ApplicationRole> _role;
        private readonly IGenericDbService<Job> _job;
        private readonly IGenericDbService<Invoice> _invoice;
        private readonly IGenericDbService<Payment> _payment;
        private readonly ILogger<EditControl> _logger;
        private readonly IGenericDbService<Log> _log;
        public EditControl(IGenericDbService<Company> company, UserManager<ApplicationUser> user, ILogger<EditControl> logger,
            IGenericDbService<Log> log, IGenericDbService<Branch> branch, RoleManager<ApplicationRole> role,
            IGenericDbService<JobType> jbType, IGenericDbService<Client> client, IGenericDbService<DocumentRequired> docReq,
            IGenericDbService<Document> doc ,IGenericDbService<Job> job, IGenericDbService<Invoice> invoice,
            IGenericDbService<Payment> payment)
        {
            _user = user;
            _company = company;
            _client = client;
            _jbType = jbType;
            _logger = logger;
            _log = log;
            _branch = branch;
            _role = role;
            _doc = doc;
            _docReq = docReq;
            _job = job;
            _invoice = invoice;
            _payment = payment;
        }

        public async Task<bool> ChangeJobStatus(ChangeJobStatusSchema model, string user)
        {
            try
            {
                _logger.LogInformation("Job Data Edition Starts ....");
                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                _logger.LogInformation("JOb Data Addition Starts ....");
                _logger.LogInformation($"Checks Job Id for {model.Id} Exists ");
                var jb = _job.AsQueryable().Where(x => x.Id == model.Id).FirstOrDefault();
                if (jb != null)
                {
                    var usr = await _user.FindByNameAsync(model.UserId);
                    jb.ApplicationUserId = usr.Id;
                    jb.status = model.status;
                    jb.remarks = model.comment;
                    _job.Update(jb);
                    _log.Create(MapperAction.CreateLog("Change Job Status", $"Job Status {jb.JbId} is Updated successfully  to {jb.status} by {user}", user, LogType.Event));
                    _logger.LogInformation("Job Data Edition Done ....");
                    return true;
                }
                _logger.LogError($"Job {model.Id} not Exoists....");
                return false;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
           
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

        public Task<bool> EditClient(ClientSchema model, string user)
        {
            throw new NotImplementedException();
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
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }

        }

        public async Task<bool> EditDocument(DocumentSchema model, string user)
        {
            try
            {
                _logger.LogInformation("Document Data Edition Starts ....");
                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                _logger.LogInformation("Document Data Addition Starts ....");
                _logger.LogInformation($"Checks Document Name {model.Name} Exists ");
                var dc = _doc.FindById(model.Id);
                if (dc != null)
                {
                    dc.Name = model.Name;
                    dc.status = model.status;
                    dc.docType = model.docType;
                    _doc.Update(dc);

                    _log.Create(MapperAction.CreateLog("Edit Document", $"Document {model.Name} is Updated successfully by {user}", user, LogType.Event));
                    _logger.LogInformation("Document Data Edition Done ....");
                    return true;

                }
                else
                {
                    _logger.LogError($"Document {model.Name} not Exoists....");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public async Task<bool> EditEmployee(EmployeeSchema model, string user)
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
                        usr.PasswordHash = _user.PasswordHasher.HashPassword(usr, model.password);
                    }

                    await _user.UpdateAsync(usr);
                    var rls = model.Roles.Split(',');
                    foreach (var r in rls)
                    {
                        await _user.RemoveFromRoleAsync(usr, r);
                        await _user.AddToRoleAsync(usr, r);
                        //var rol =await _role.FindByNameAsync(r);
                    }

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

        public async Task<bool> EditJobType(JobTypeSchema model, string user)
        {
            try
            {
                _logger.LogInformation("Job Type Data Edition Starts ....");
                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                _logger.LogInformation("Job Type Data Addition Starts ....");
                _logger.LogInformation($"Checks Job Type Id for {model.Name} Exists ");
                var jbType = _jbType.FindById(model.Id);
                if (jbType != null)
                {
                    jbType.Name = model.Name;
                    jbType.Id = model.Id;
                    jbType.Remarks = "none";
                    jbType.status = model.status;

                    _jbType.Update(jbType);
                    var docReq = _docReq.AsQueryable().Where(x => x.jobTypeId == jbType.Id);
                    foreach (var d in docReq)
                    {
                        _docReq.Delete(d);
                    }
                    var docreq = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(model.documentRequired);
                    foreach (string d in docreq)
                    {
                        var dc = _doc.FindByCondition(x => x.Name == d).FirstOrDefault();
                        _docReq.Create(new DocumentRequired()
                        {
                            Name = d,
                            createdAt = DateTime.Now,
                            docId = dc.Id,
                            jobTypeId = jbType.Id
                        });
                    }
                    _log.Create(MapperAction.CreateLog("Edit Job Type", $"Job Type {model.Name} is Updated successfully by {user}", user, LogType.Event));
                    _logger.LogInformation("Job Type Data Edition Done ....");
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

                    var res = await _role.UpdateAsync(rol);
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
                        usr.PasswordHash = _user.PasswordHasher.HashPassword(usr, model.password);
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
