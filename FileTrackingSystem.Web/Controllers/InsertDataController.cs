using FileTrackingSystem.BL.Contract;
using FileTrackingSystem.BL.Models;
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

        private readonly IUrlHelper _helper;
        public InsertDataController(IInsert insert, ILogger<InsertDataController> logger, IUrlHelper helper)
        {
            _insert = insert;
            _logger = logger;
            _helper = helper;
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
        [HttpPost]
        public async Task<IActionResult> AddAdmin([FromBody] UserSchema schema)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res = await _insert.InsertUser(schema, HttpContext);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $"New Admin {schema.userName} is Sucessfully Added" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] RoleSchema schema)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res = await _insert.InsertRole(schema, HttpContext.User.Identity.Name);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $"New Role {schema.Name} is Sucessfully Added" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeSchema schema)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res = await _insert.InsertEmployee(schema, HttpContext);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $"New Employee {schema.userName} is Sucessfully Added" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] ClientSchema schema)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res = await _insert.InsertClient(schema, HttpContext);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $"New Client {schema.name} is Sucessfully Added" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
        [HttpPost]
        public async Task<IActionResult> AddDocument([FromBody] DocumentSchema schema)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res = await _insert.InsertDocument(schema, HttpContext);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $"New Document {schema.Name} is Sucessfully Added" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }

        [HttpPost]
        public async Task<IActionResult> AddJobType([FromBody] JobTypeSchema schema)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res = await _insert.InsertJobType(schema, HttpContext);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $"New Job Type {schema.Name} is Sucessfully Added" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
        [HttpPost]
        public async Task<IActionResult> AddJob([FromBody] JobPostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res = await _insert.InsertJob(model, HttpContext);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $"New Job  is Sucessfully Added" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }

    }
}
