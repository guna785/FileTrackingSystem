using FileTrackingSystem.BL.Contract;
using FileTrackingSystem.BL.Models;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace FileTrackingSystem.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SchemaGenerator _schema;
        private readonly EditBuilder _builder;
        private readonly IGet _get;
        private readonly IInsert _insert;
        private readonly IGeneratePdf _generatePdf;
        public HomeController(ILogger<HomeController> logger, EditBuilder builder, SchemaGenerator schema, IGet get,
             IInsert insert, IGeneratePdf generatePdf)
        {
            _logger = logger;
            _builder = builder;
            _schema = schema;
            _get = get;
            _insert = insert;
            _generatePdf = generatePdf;
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
        public IActionResult Client()
        {
            return View();
        }
        public IActionResult Role()
        {
            return View();
        }
        public IActionResult JobType()
        {
            return View();
        }
        public IActionResult Document()
        {
            return View();
        }
        public IActionResult Job()
        {
            return View();
        }       
        public IActionResult CompletedJob()
        {
            return View();
        }
        public IActionResult PendingJob()
        {
            return View();
        }
        public IActionResult CancelledJob()
        {
            return View();
        }
        public IActionResult Invoice()
        {
            return View();
        }
        public IActionResult Payment()
        {
            return View();
        }
        public async Task<IActionResult> PrintInvoice(int Id)
        {
            var res = _get.GetInvoiceDocument(Id);
            return await _generatePdf.GetPdf("Views/Home/InvoiceDocumentView.cshtml", res);
        }
        public async Task<IActionResult> DownloadInvoice(int Id)
        {
            var res = _get.GetInvoiceDocument(Id);
            var pdf = await _generatePdf.GetByteArray("Views/Home/InvoiceDocumentView.cshtml", res);
            var contentType = "APPLICATION/octet-stream";
            return File(pdf, contentType, "Invoice.pdf");
        }
        public IActionResult EventLog()
        {
            return View();
        }
        public IActionResult JobLog()
        {
            return View();
        }
        public IActionResult CreateNeJob()
        {
            ViewBag.jobtype = _get.GetJobTypes();
            return View();
        }
        [HttpPost]
        public IActionResult GetClients([FromBody]string Id)
        {
            var client = _get.GetAllClients().Where(x => x.Pan.ToUpper().Contains(Id.ToUpper()) || x.name.ToUpper().Contains(Id.ToUpper())).Select(x=> string.Concat( x.Pan , "  -  " , x.name) ).ToList();
            return Ok(client);
        }
        private string schema="";
        public async Task<IActionResult> PopUpModelShow(string ID)
        {
            if (ID.Contains("AddCompany"))
            {
                schema = await _schema.Generate<CompanySchema>(HttpContext);
               // schema = await _sgenerator.GenerateSchema<CompanySchema>("");
                ViewBag.modalTitle = "AddCompany";
            }
            else if (ID.Contains("EditCompany"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<CompanySchema>(objId == null ? 0 : Convert.ToInt32(objId));
                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                schema = await _schema.Generate<CompanySchema>(HttpContext);
                ViewBag.modalTitle = "EditCompany";
            }
           else if (ID.Contains("AddBranch"))
            {
                schema = await _schema.Generate<BranchSchema>(HttpContext);
                ViewBag.modalTitle = "AddBranch";
            }
            else if (ID.Contains("EditBranch"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<BranchSchema>(objId == null ? 0 : Convert.ToInt32(objId));
                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                schema = await _schema.Generate<BranchSchema>(HttpContext);
                ViewBag.modalTitle = "EditBranch";
            }
            else if (ID.Contains("AddAdmin"))
            {
                schema = await _schema.Generate<UserSchema>(HttpContext);
                ViewBag.modalTitle = "AddAdmin";
            }
            else if (ID.Contains("EditAdmin"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<UserSchema>(objId == null ? 0 : Convert.ToInt32(objId));
                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                schema = await _schema.Generate<UserSchema>(HttpContext);
                ViewBag.modalTitle = "EditAdmin";
            }
            else if (ID.Contains("AddRole"))
            {
                schema = await _schema.Generate<RoleSchema>(HttpContext);
                ViewBag.modalTitle = "AddRole";
            }
            else if (ID.Contains("EditRole"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<RoleSchema>(objId == null ? 0 : Convert.ToInt32(objId));
                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                schema = await _schema.Generate<RoleSchema>(HttpContext);
                ViewBag.modalTitle = "EditRole";
            }
            else if (ID.Contains("AddEmployee"))
            {
                schema = await _schema.Generate<EmployeeSchema>(HttpContext);
                ViewBag.modalTitle = "AddEmployee";
            }
            else if (ID.Contains("EditEmployee"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<EmployeeSchema>(objId == null ? 0 : Convert.ToInt32(objId));
                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                schema = await _schema.Generate<EmployeeSchema>(HttpContext);
                ViewBag.modalTitle = "EditEmployee";
            }
            else if (ID.Contains("AddClient"))
            {
                schema = await _schema.Generate<ClientSchema>(HttpContext);
                ViewBag.modalTitle = "AddClient";
            }
            else if (ID.Contains("EditClient"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<ClientSchema>(objId == null ? 0 : Convert.ToInt32(objId));
                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                schema = await _schema.Generate<ClientSchema>(HttpContext);
                ViewBag.modalTitle = "EditClient";
            }
            else if (ID.Contains("AddJobType"))
            {
                schema = await _schema.Generate<JobTypeSchema>(HttpContext);
                ViewBag.modalTitle = "AddJobType";
            }
            else if (ID.Contains("EditJobType"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<JobTypeSchema>(objId == null ? 0 : Convert.ToInt32(objId));
                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                schema = await _schema.Generate<JobTypeSchema>(HttpContext);
                ViewBag.modalTitle = "EditJobType";
            }
            else if (ID.Contains("AddDocument"))
            {
                schema = await _schema.Generate<DocumentSchema>(HttpContext);
                ViewBag.modalTitle = "AddDocument";
            }
            else if (ID.Contains("EditDocument"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<DocumentSchema>(objId == null ? 0 : Convert.ToInt32(objId));
                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                schema = await _schema.Generate<DocumentSchema>(HttpContext);
                ViewBag.modalTitle = "EditDocument";
            }
            else if (ID.Contains("ChangeJobStatus"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<ChangeJobStatusSchema>(objId == null ? 0 : Convert.ToInt32(objId));
                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                schema = await _schema.Generate<ChangeJobStatusSchema>(HttpContext);
                ViewBag.modalTitle = "ChangeJobStatus";
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
