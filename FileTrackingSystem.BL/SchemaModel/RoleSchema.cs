using FileTrackingSystem.Schema.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.SchemaModel
{
    public class RoleSchema
    {
        [GSchema("Id", "ID", "hidden", false)]
        public int Id { get; set; } = 0;
        [GSchema("Name", "Name", "string", true, getHtmlClass = "col-md-12")]
        public string Name { get; set; }
        [GSchema("Discription", "Discription", "string", true, getHtmlClass = "col-md-12")]
        public string Discription { get; set; }
        [GSchema("Permission", "Permissions", "string", true, getEnumVal ="permission", getHtmlClass = "col-md-12",getfieldHtmlClass = "select2 selectMultiple")]
        public dynamic Permission { get; set; }
    }
}
