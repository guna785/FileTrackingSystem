using FileTrackingSystem.BL.SchemaBuilder;
using FileTrackingSystem.BL.SchemaModel;
using FileTrackingSystem.Schema.Generator;
using FileTrackingSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FileTrackingSystem.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GSgenerator _sgenerator;
        private readonly EditBuilder _builder;
        public HomeController(ILogger<HomeController> logger, GSgenerator sgenerator, EditBuilder builder)
        {
            _logger = logger;
            _sgenerator = sgenerator;
            _builder = builder;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Company()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Admins()
        {
            return View();
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Branch()
        {
            return View();
        }
        public IActionResult Employee()
        {
            return View();
        }
        public IActionResult Role()
        {
            return View();
        }
        private string schema="";
        public async Task<IActionResult> PopUpModelShow(string ID)
        {
            if (ID.Contains("AddCompany"))
            {
                schema = await _sgenerator.GenerateSchema<CompanySchema>("");
                ViewBag.modalTitle = "AddCompany";
            }
            else if (ID.Contains("EditCompany"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<CompanySchema>(objId == null ? 0 : Convert.ToInt32(objId));
                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                schema = await _sgenerator.GenerateSchema<CompanySchema>("");
                ViewBag.modalTitle = "EditCompany";
            }
           else if (ID.Contains("AddBranch"))
            {
                schema = await _sgenerator.GenerateSchema<BranchSchema>("");
                ViewBag.modalTitle = "AddBranch";
            }
            else if (ID.Contains("EditBranch"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<BranchSchema>(objId == null ? 0 : Convert.ToInt32(objId));
                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                schema = await _sgenerator.GenerateSchema<BranchSchema>("");
                ViewBag.modalTitle = "EditBranch";
            }
            else if (ID.Contains("AddAdmin"))
            {
                schema = await _sgenerator.GenerateSchema<UserSchema>("");
                ViewBag.modalTitle = "AddAdmin";
            }
            else if (ID.Contains("EditAdmin"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<UserSchema>(objId == null ? 0 : Convert.ToInt32(objId));
                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                schema = await _sgenerator.GenerateSchema<UserSchema>("Edit");
                ViewBag.modalTitle = "EditAdmin";
            }
            else if (ID.Contains("AddRole"))
            {
                schema = await _sgenerator.GenerateSchema<RoleSchema>("");
                ViewBag.modalTitle = "AddRole";
            }
            else if (ID.Contains("EditRole"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<RoleSchema>(objId == null ? 0 : Convert.ToInt32(objId));
                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                schema = await _sgenerator.GenerateSchema<RoleSchema>("Edit");
                ViewBag.modalTitle = "EditRole";
            }
            else if (ID.Contains("AddEmployee"))
            {
                schema = await _sgenerator.GenerateSchema<EmployeeSchema>("");
                ViewBag.modalTitle = "AddEmployee";
            }
            else if (ID.Contains("EditEmployee"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<EmployeeSchema>(objId == null ? 0 : Convert.ToInt32(objId));
                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                schema = await _sgenerator.GenerateSchema<EmployeeSchema>("Edit");
                ViewBag.modalTitle = "EditEmployee";
            }
            ViewBag.schema = schema;
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
