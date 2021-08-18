using FileTrackingSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public string invId { get; set; }
        public string jobId { get; set; }
        public string clientId { get; set; }
        public string companyId { get; set; }
        public int Amount { get; set; }
        public double Tax { get; set; }
        public double TotalAmount { get; set; }
        public double PaidAmount { get; set; }
        public double balanceAmount { get; set; }
        public IncoiceStatus status { get; set; }
        public DateTime createdAt { get; set; }
    }
}
