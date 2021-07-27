﻿using FileTrackingSystem.BL.Contract;
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
        public EditDataController(IEdit edit, ILogger<EditDataController> logger)
        {
            _edit = edit;
            _logger = logger;
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
    }
}
