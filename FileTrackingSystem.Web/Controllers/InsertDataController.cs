using FileTrackingSystem.BL.Contract;
using FileTrackingSystem.BL.SchemaModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileTrackingSystem.Web.Controllers
{
    public class InsertDataController : Controller
    {
        private readonly IInsert _insert;
        private readonly ILogger<InsertDataController> _logger;
        public InsertDataController(IInsert insert, ILogger<InsertDataController> logger)
        {
            _insert = insert;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany([FromBody] CompanySchema schema)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res =await _insert.InsertCompany(schema, HttpContext.User.Identity.Name);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $"New Company {schema.Name} is Sucessfully Added" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
        [HttpPost]
        public async Task<IActionResult> AddBranch([FromBody] BranchSchema schema)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res = await _insert.InsertBranch(schema, HttpContext.User.Identity.Name);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $"New Branch {schema.Name} is Sucessfully Added" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }

    }
}
