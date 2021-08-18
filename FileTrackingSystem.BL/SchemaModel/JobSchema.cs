using FileTrackingSystem.Models.Enums;
using FileTrackingSystem.Schema.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.SchemaModel
{
    public class JobSchema
    {
        [GSchema("Id", "ID", "hidden", false)]
        public int Id { get; set; } = 0;
        [GSchema("jobTypeId", "Job Type", "string", true, getEnumVal = "jobType", getHtmlClass = "col-md-6")]
        public int jobTypeId { get; set; }
        [GSchema("clientType", "Client Type", "string", true, getEnumVal = "clientType", getHtmlClass = "col-md-6")]
        public ClientType clientType { get; set; }
        [GSchema("clientId", "Select Client", "string", true, getEnumVal = "client", getHtmlClass = "col-md-6", getfieldHtmlClass = "select2")]
        public int clientId { get; set; }
        [GSchema("branchId", "Select Branch", "string", true, getEnumVal = "branch", getHtmlClass = "col-md-6", getfieldHtmlClass = "select2")]
        public string branchId { get; set; }
        [GSchema("status", "Job Status", "string", true, getEnumVal = "jobStatus", getHtmlClass = "col-md-6")]
        public JobStatus status { get; set; }
        [GSchema("paidAmount", "Amount Paid In Advance", "number", true,  getHtmlClass = "col-md-6")]
        public int paidAmount { get; set; }
    }
}
