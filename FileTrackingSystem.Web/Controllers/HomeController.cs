using FileTrackingSystem.Schema.Generator;
using FileTrackingSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FileTrackingSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GSgenerator _sgenerator;
       // private readonly EditBuilder _builder;
        public HomeController(ILogger<HomeController> logger, GSgenerator sgenerator/*, EditBuilder builder*/)
        {
            _logger = logger;
            _sgenerator = sgenerator;
           // _builder = builder;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Company()
        {
            return View();
        }
        public IActionResult Admins()
        {
            return View();
        }
        public IActionResult Employee()
        {
            return View();
        }
        private string schema;
        public async Task<IActionResult> PopUpModelShow(string Id)
        {
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
