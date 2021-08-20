using FileTrackingSystem.Models.Enums;
using FileTrackingSystem.Schema.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.SchemaModel
{
    public class ChangeJobStatusSchema
    {
        [GSchema("Id", "ID", "hidden", false)]
        public int Id { get; set; }
        [GSchema("status", "Job Status", "string", true, getEnumVal = "jobStatus", getHtmlClass = "col-md-12")]
        public JobStatus status { get; set; }
        [GSchema("UserId", "Assigned To", "string", true, getEnumVal = "employee", getHtmlClass = "col-md-12")]
        public string UserId { get; set; }
        [GSchema("comment", "Notes", "textarea", true, getHtmlClass = "col-md-12")]
        public string comment { get; set; }
    }
}
