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
        public IActionResult Employee()
        {
            return View();
        }
        private string schema="";
        public async Task<IActionResult> PopUpModelShow(string ID)
        {
            if (ID.Contains("AddCompany"))
            {
                schema = await _sgenerator.GenerateSchema<AddCompanySchema>("");
                ViewBag.modalTitle = "AddCompany";
            }
            else if (ID.Contains("EditCompany"))
            {
                var objId = ID.Split('-')[1];
                var data = await _builder.ReturnObjectData<EditCompanySchema>(objId == null ? 0 : Convert.ToInt32(objId));
                ViewBag.val = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                schema = await _sgenerator.GenerateSchema<EditCompanySchema>("");
                ViewBag.modalTitle = "EditCompany";
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
