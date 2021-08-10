using FileTrackingSystem.BL.Contract;
using FileTrackingSystem.BL.Extentions;
using FileTrackingSystem.BL.SchemaModel;
using FileTrackingSystem.DAL.Contract;
using FileTrackingSystem.Models.Enums;
using FileTrackingSystem.Models.Models;
using Microsoft.AspNetCore.Http;
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
        private readonly IGenericDbService<JobType> _jbType;
        private readonly IGenericDbService<DocumentRequired> _docReq;
        private readonly IGenericDbService<Document> _doc;
        private readonly IGenericDbService<Client> _client;
        private readonly IGenericDbService<Branch> _branch;
        private readonly IGenericDbService<Log> _log;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<ApplicationRole> _role;
        private readonly ILogger<InsertControl> _logger;
        private readonly IIdentityUserService _service;
        public InsertControl(IGenericDbService<Company> company, UserManager<ApplicationUser> user, ILogger<InsertControl> logger,
            IGenericDbService<Log> log, IGenericDbService<Branch> branch, RoleManager<ApplicationRole> role, IIdentityUserService service,
            IGenericDbService<Client> client, IGenericDbService<JobType> jbType, IGenericDbService<DocumentRequired> docReq,
            IGenericDbService<Document> doc)
        {
            _user = user;
            _company = company;
            _branch = branch;
            _logger = logger;
            _log = log;
            _jbType = jbType;
            _role = role;
            _doc = doc;
            _service = service;
            _client = client;
            _docReq = docReq;
        }

        public async Task<bool> InsertBranch(BranchSchema model, string user)
        {
            try
            {
                _logger.LogInformation("Branch Addition Starts ....");
                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                var cmp = _company.AsQueryable().Where(x => x.Name == model.CompanyId).FirstOrDefault();
                model.CompanyId = cmp.Id.ToString();
                _branch.Create(model.toBranch());
                _log.Create(MapperAction.CreateLog("Insert Branch", $"Branch {model.Name} is Added successfully to Company {cmp.Name} by {user}", user, LogType.Event));
                _logger.LogInformation("Branch Data Addition Done ....");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public async Task<bool> InsertClient(ClientSchema model, HttpContext context)
        {
            try
            {
                _logger.LogInformation("Client Data Addition Starts ....");
                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                var usr = await _user.FindByNameAsync(context.User.Identity.Name);
                _client.Create(model.toClient(usr.Id));
                _log.Create(MapperAction.CreateLog("Insert Clinet", $"Client {model.name} is Added successfully by {usr.UserName}", usr.UserName, LogType.Event));
                _logger.LogInformation("Client Data Addition Done ....");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public async Task<bool> InsertCompany(CompanySchema model, string user)
        {
            try
            {
                _logger.LogInformation("Compay Data Addition Starts ....");
                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                var usr = await _user.FindByNameAsync(user);
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

        public async Task<bool> InsertDocument(DocumentSchema model, HttpContext context)
        {
            try
            {
                _logger.LogInformation("Document Data Addition Starts ....");
                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                _doc.Create(model.toDocument());

                _logger.LogInformation("Document Data Addition Done ....");
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public async Task<bool> InsertEmployee(EmployeeSchema model, HttpContext context)
        {
            try
            {
                _logger.LogInformation("Employee Data Addition Starts ....");
                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                var brnch = _branch.AsQueryable().Where(x => x.Name == model.branchId).FirstOrDefault();
                model.branchId = brnch.Id.ToString();
                var res = await _user.CreateAsync(model.toEmployee(brnch.Id), model.password);

                if (res.Succeeded)
                {
                    var rls = model.Roles.Split(',');
                    var usr = await _user.FindByNameAsync(model.userName);
                    foreach (var r in rls)
                    {
                        //var rol =await _role.FindByNameAsync(r);
                        await _user.AddToRoleAsync(usr, r);
                    }
                    await _service.ConfirmEmailGenerate(usr, context);
                    _log.Create(MapperAction.CreateLog("Insert EMployee", $"Employee {model.userName} is Added successfully by {context.User.Identity.Name}", context.User.Identity.Name, LogType.Event));

                    _logger.LogInformation("Employee Data Addition Done ....");
                    return true;
                }
                _logger.LogError("Error : " + Newtonsoft.Json.JsonConvert.SerializeObject(res.Errors.ToList()));
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public async Task<bool> InsertJobType(JobTypeSchema model, HttpContext context)
        {
            try
            {
                _logger.LogInformation("Job Type Data Addition Starts ....");
                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                var usr = await _user.FindByNameAsync(context.User.Identity.Name);
                _jbType.Create(model.toJobType(usr.Id));
                var docreq = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(model.documentRequired);
                var jbtyp = _jbType.FindByCondition(x => x.Name == model.Name).FirstOrDefault();
                foreach (var d in docreq)
                {
                    var dc = _doc.FindByCondition(x => x.Name == d).FirstOrDefault();

                    _docReq.Create(new DocumentRequired()
                    {
                        Name = d,
                        createdAt = DateTime.Now,
                        docId = dc.Id,
                        jobTypeId = jbtyp.Id
                    });
                }
                _log.Create(MapperAction.CreateLog("Insert Job Type", $"Job Type {model.Name} is Added successfully by {usr.UserName}", usr.UserName, LogType.Event));
                _logger.LogInformation("Job Type Data Addition Done ....");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public async Task<bool> InsertRole(RoleSchema model, string user)
        {
            try
            {
                _logger.LogInformation("Role Data Addition Starts ....");
                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                var res = await _role.CreateAsync(model.toRole());
                if (res.Succeeded)
                {
                    _logger.LogInformation("Role Data Addition Done ....");
                    return true;
                }
                _logger.LogError("Error : " + Newtonsoft.Json.JsonConvert.SerializeObject(res.Errors.ToList()));
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public async Task<bool> InsertUser(UserSchema model, HttpContext context)
        {
            try
            {
                _logger.LogInformation("Admin User Data Addition Starts ....");
                _logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(model));
                var brnch = _branch.AsQueryable().Where(x => x.Name == model.branchId).FirstOrDefault();
                model.branchId = brnch.Id.ToString();
                var res = await _user.CreateAsync(model.toUser(brnch.CompanyId), model.password);
                if (res.Succeeded)
                {
                    var usr = await _user.FindByNameAsync(model.userName);
                    await _service.ConfirmEmailGenerate(usr, context);
                    _log.Create(MapperAction.CreateLog("Insert Admin", $"Admin {model.userName} is Added successfully by {context.User.Identity.Name}", context.User.Identity.Name, LogType.Event));

                    _logger.LogInformation("Admin User Data Addition Done ....");
                    return true;
                }
                _logger.LogError("Error : " + Newtonsoft.Json.JsonConvert.SerializeObject(res.Errors.ToList()));
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }
    }
}
