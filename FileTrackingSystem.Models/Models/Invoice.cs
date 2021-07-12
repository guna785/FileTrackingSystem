using FileTrackingSystem.Models.BaseClass;
using FileTrackingSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Models.Models
{
    public class Invoice:CommonModel
    {
        public int jobId { get; set; }
        public int MyProperty { get; set; }
        public int clientId { get; set; }
        public int companyId { get; set; }
        public int Amount { get; set; }
        public int Tax { get; set; }
        public int TotalAmount { get; set; }
        public int PaidAmount { get; set; }
        public int balanceAmount { get; set; }
        public IncoiceStatus status{ get; set; }

        public virtual IList<Payment> Payments { get; set; }
    }
}
