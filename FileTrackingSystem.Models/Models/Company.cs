using FileTrackingSystem.Models.BaseClass;
using FileTrackingSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Models.Models
{
    public class Company:CommonModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public string Contact { get; set; }

        public string Pan { get; set; }
        public string TIN { get; set; }
        public string GST { get; set; }

        public string HSN { get; set; }
        public string BankName { get; set; }
        public string BankAccNo { get; set; }

        public string BankBranch { get; set; }
        public string IFSC { get; set; }
        public StatusType status { get; set; }
        public virtual IList<Invoice> Invoices { get; set; }
        public virtual IList<Payment> Payments { get; set; }
    }
}
