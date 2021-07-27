using FileTrackingSystem.Models.Enums;
using FileTrackingSystem.Schema.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.SchemaModel
{
    public class EmployeeSchema
    {
        [GSchema("Id", "ID", "hidden", false)]
        public int Id { get; set; }
        [GSchema("Name", "Name", "string", true, getHtmlClass = "col-md-6")]
        public string Name { get; set; }
        [GSchema("gender", "Gender", "string", true, getEnumVal = "gender", getHtmlClass = "col-md-6")]
        public Gender gender { get; set; }
        [GSchema("userType", "User Type", "string", true, getEnumVal = "usertype-User", getHtmlClass = "col-md-6")]
        public UserType userType { get; set; }
        [GSchema("CompanyId", "Company", "string", true, getEnumVal = "Company", getHtmlClass = "col-md-6")]
        public string CompanyId { get; set; }
        [GSchema("userName", "User Name", "string", true, getHtmlClass = "col-md-6")]
        public string userName { get; set; }
        [GSchema("password", "Password", "password", true, getHtmlClass = "col-md-6")]
        public string password { get; set; }
        [GSchema("email", "Email", "email", true, getHtmlClass = "col-md-6")]
        public string email { get; set; }
        [GSchema("phone", "Phone", "number", true, getHtmlClass = "col-md-6")]
        public string phone { get; set; }
    }
}
