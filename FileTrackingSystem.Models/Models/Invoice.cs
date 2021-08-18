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
        public string invId { get; set; }
        public int jobId { get; set; }
        public int clientId { get; set; }
        public int companyId { get; set; }
        public int Amount { get; set; }
        public double Tax { get; set; }
        public double TotalAmount { get; set; }
        public double PaidAmount { get; set; }
        public double balanceAmount { get; set; }
        public IncoiceStatus status{ get; set; }

        public virtual IList<Payment> Payments { get; set; }
    }
}
