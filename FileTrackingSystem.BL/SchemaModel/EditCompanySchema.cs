using FileTrackingSystem.Models.Enums;
using FileTrackingSystem.Schema.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.SchemaModel
{
    public class EditCompanySchema
    {
        [GSchema("Id", "ID", "hidden", true)]
        public int Id { get; set; }
        [GSchema("Name", "Name", "string", true, getHtmlClass = "col-md-6")]
        public string Name { get; set; }
        [GSchema("Address", "Address", "string", true, getHtmlClass = "col-md-6")]
        public string Address { get; set; }
        [GSchema("Email", "Email", "email", true, getHtmlClass = "col-md-6")]
        public string Email { get; set; }
        [GSchema("Web", "Web Site", "string", true, getHtmlClass = "col-md-6")]
        public string Web { get; set; }
        [GSchema("Contact", "Contact No", "number", true, getHtmlClass = "col-md-6")]
        public string Contact { get; set; }
        [GSchema("Pan", "PAN", "string", true, getHtmlClass = "col-md-6")]
        public string Pan { get; set; }
        [GSchema("TIN", "TIN No", "string", true, getHtmlClass = "col-md-6")]
        public string TIN { get; set; }
        [GSchema("GST", "GST No", "string", true, getHtmlClass = "col-md-6")]
        public string GST { get; set; }
        [GSchema("HSN", "HSN", "string", true, getHtmlClass = "col-md-6")]
        public string HSN { get; set; }
        [GSchema("BankName", "Bank Name", "string", true, getHtmlClass = "col-md-6")]
        public string BankName { get; set; }
        [GSchema("BankAccNo", "Bank Account No", "string", true, getHtmlClass = "col-md-6")]
        public string BankAccNo { get; set; }
        [GSchema("BankBranch", "Banck Branch", "string", true, getHtmlClass = "col-md-6")]
        public string BankBranch { get; set; }
        [GSchema("IFSC", "IFSC Code", "string", true, getHtmlClass = "col-md-6")]
        public string IFSC { get; set; }
        [GSchema("status", "Status", "string", true, getEnumVal = "status", getHtmlClass = "col-md-6")]
        public StatusType status { get; set; }
    }
}
