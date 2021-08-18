using FileTrackingSystem.Models.Enums;
using FileTrackingSystem.Schema.Generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.SchemaModel
{
    public class ClientSchema
    {
        [GSchema("Id", "ID", "hidden", false)]
        public int Id { get; set; } = 0;
        [GSchema("name", "Name", "string", true, getHtmlClass = "col-md-6")]
        public string name { get; set; }
        [GSchema("fatherName", "Father Name", "string", true, getHtmlClass = "col-md-6")]
        public string fatherName { get; set; }
        [GSchema("Pan", "PAN", "string", true, getHtmlClass = "col-md-6")]
        public string Pan { get; set; }
        [GSchema("Phone", "Phone No", "string", true, getHtmlClass = "col-md-6")]
        public string Phone { get; set; }
        [GSchema("Gender", "Gender", "string", true, getEnumVal = "gender", getHtmlClass = "col-md-6")]
        public Gender Gender { get; set; }
        [GSchema("clientType", "Client Type", "string", true, getEnumVal = "clientType", getHtmlClass = "col-md-6")]
        public ClientType clientType { get; set; }
        [GSchema("Address", "Address", "string", true, getHtmlClass = "col-md-6")]
        public string Address { get; set; }
        [GSchema("idProoftype", "Proof Type", "string", true, getEnumVal = "idProoftype", getHtmlClass = "col-md-6")]
        public IdProofType idProoftype { get; set; }
        [GSchema("idProofNo", "Id Proof No", "string", true, getHtmlClass = "col-md-6" ,getfieldHtmlClass = "accept-image")]
        public string  idProofNo { get; set; }
       
        [GSchema("Dob", "Date of Birth", "date", true, getHtmlClass = "col-md-6")]
        public DateTime Dob { get; set; }
        [GSchema("Remarks", "Remarks", "string", true, getHtmlClass = "col-md-6")]
        public string Remarks { get; set; }
        [GSchema("Email", "Email", "email", true, getHtmlClass = "col-md-6")]
        public string Email { get; set; }
        [GSchema("GSTNo", "GST No", "string", true, getHtmlClass = "col-md-6")]
        public string GSTNo { get; set; }
        [GSchema("ContactPersonName", "Contact Person Name", "string", true, getHtmlClass = "col-md-6")]
        public string ContactPersonName { get; set; }
    }
}
