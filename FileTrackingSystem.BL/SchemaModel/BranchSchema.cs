using FileTrackingSystem.Schema.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.SchemaModel
{
    public class BranchSchema
    {
        [GSchema("Id", "ID", "hidden", false)]
        public int Id { get; set; } = 0;
        [GSchema("Name", "Name", "string", true, getHtmlClass = "col-md-6")]
        public string Name { get; set; }
        [GSchema("CompanyId", "Select Company", "string", true,getEnumVal ="company", getHtmlClass = "col-md-6",getfieldHtmlClass = "select2")]
        public string CompanyId { get; set; }
    }
}
