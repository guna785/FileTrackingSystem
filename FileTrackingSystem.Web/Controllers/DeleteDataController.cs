using FileTrackingSystem.BL.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileTrackingSystem.Web.Controllers
{
    public class DeleteDataController : Controller
    {
        private readonly IDelete _delete;
        private readonly ILogger<EditDataController> _logger;
        public DeleteDataController(IDelete delete, ILogger<EditDataController> logger)
        {
            _delete = delete;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCompany ([FromBody] int Id)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model state is Invalid");
                return BadRequest("Invalid Request");
            }
            var res =await _delete.DeleteComapny(Id, HttpContext.User.Identity.Name);
            if (res)
            {
                _logger.LogInformation("Resquest Completed Successfully");
                var result = new { status = $" Company {Id} is Sucessfully Deleted" };
                return Ok(result);
            }
            return BadRequest("Invalid Request");
        }
    }
}
