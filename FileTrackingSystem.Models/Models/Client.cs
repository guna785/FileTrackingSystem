using FileTrackingSystem.Models.BaseClass;
using FileTrackingSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Models.Models
{
    public class Client : CommonModel
    {
        public string name { get; set; }
        public string fatherName { get; set; }
        public string Pan { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public ClientType clientType { get; set; }
        public string Address { get; set; }
        public byte[] idProof { get; set; }
        public IdProofType idProoftype { get; set; }
        public int createdBy { get; set; }
        public DateTime Dob { get; set; }
        public string Remarks { get; set; }
        public string Email { get; set; }
        public string GSTNo { get; set; }
        public string ContactPersonName { get; set; }
        public virtual IList<Job> Jobs { get; set; }
        public virtual IList<Invoice> Invoices { get; set; }
        public virtual IList<Payment> Payments { get; set; }
    }
}
