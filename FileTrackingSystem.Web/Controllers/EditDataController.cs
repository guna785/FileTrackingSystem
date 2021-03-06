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
    public class EditDataController : Controller
    {
        private readonly IEdit _edit;
        private readonly ILogger<EditDataController> _logger;
        private readonly IUrlHelper _helper;
        public EditDataController(IEdit edit, ILogger<EditDataController> logger, IUrlHelper helper)
        {
            _edit = edit;
            _logger = logger;
            _helper = helper;
        }
        [HttpPost]
        public async Task<IActionResult> EditCompany([FromBody] CompanySchema schema)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res = await _edit.EditCompany(schema, HttpContext.User.Identity.Name);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $" Company {schema.Name} is Sucessfully Edited" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
        [HttpPost]
        public async Task<IActionResult> EditBranch([FromBody] BranchSchema schema)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res = await _edit.EditBranch(schema, HttpContext.User.Identity.Name);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $" Branch {schema.Name} is Sucessfully Edited" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
        [HttpPost]
        public async Task<IActionResult> EditAdmin([FromBody] UserSchema schema)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res = await _edit.EditUser(schema, HttpContext.User.Identity.Name);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $" Admin {schema.userName} is Sucessfully Edited" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
        [HttpPost]
        public async Task<IActionResult> EditRole([FromBody] RoleSchema schema)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res = await _edit.EditRole(schema, HttpContext.User.Identity.Name);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $" Role {schema.Name} is Sucessfully Edited" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
        [HttpPost]
        public async Task<IActionResult> EditEmployee([FromBody] EmployeeSchema schema)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res = await _edit.EditEmployee(schema, HttpContext.User.Identity.Name);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $" Admin {schema.userName} is Sucessfully Edited" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
        [HttpPost]
        public async Task<IActionResult> EditClient([FromBody] ClientSchema schema)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res = await _edit.EditClient(schema, HttpContext.User.Identity.Name);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $" Client {schema.name} is Sucessfully Edited" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
        [HttpPost]
        public async Task<IActionResult> JobStatusUpdate([FromBody] ChangeJobStatusSchema schema)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res = await _edit.ChangeJobStatus(schema, HttpContext.User.Identity.Name);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $" Job status  is Sucessfully Changed" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
        [HttpPost]
        public async Task<IActionResult> EditDocument([FromBody] DocumentSchema schema)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res = await _edit.EditDocument(schema, HttpContext.User.Identity.Name);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $" Document {schema.Name} is Sucessfully Edited" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
        [HttpPost]
        public async Task<IActionResult> EditJobType([FromBody] JobTypeSchema schema)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res = await _edit.EditJobType(schema, HttpContext.User.Identity.Name);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $" Job Type {schema.Name} is Sucessfully Edited" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
    }
}
