using FileTrackingSystem.BL.Contract;
using FileTrackingSystem.BL.DataTableModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileTrackingSystem.Web.Controllers
{
    public class ViewDataController : Controller
    {
        private readonly IGenericDatatableRenderar _renderar;
        public ViewDataController(IGenericDatatableRenderar renderar)
        {
            _renderar = renderar;
        }
        [HttpPost]
        public IActionResult LoadCompany([FromBody] DtParameters parameters)
        {
            return Ok(_renderar.CompanyJson(parameters));
        }
        [HttpPost]
        public IActionResult LoadBranch([FromBody] DtParameters parameters)
        {
            return Ok(_renderar.BranchJson(parameters));
        }
        [HttpPost]
        public IActionResult LoadAdmins([FromBody] DtParameters parameters)
        {
            return Ok(_renderar.AdminJson(parameters));
        }
        [HttpPost]
        public IActionResult LoadRoles([FromBody] DtParameters parameters)
        {
            return Ok(_renderar.RoleJson(parameters));
        }
        [HttpPost]
        public IActionResult LoadEmployes([FromBody] DtParameters parameters)
        {
            return Ok(_renderar.EmployeeJson(parameters));
        }
        [HttpPost]
        public IActionResult LoadClients([FromBody] DtParameters parameters)
        {
            return Ok(_renderar.ClientJson(parameters));
        }

    }
}
