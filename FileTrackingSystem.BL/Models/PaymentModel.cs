using FileTrackingSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public string payId { get; set; }
        public string invoiceId { get; set; }
        public int Amount { get; set; }
        public string jobId { get; set; }
        public string clientId { get; set; }
        public string companyId { get; set; }
        public int ApplicationUserId { get; set; }
        public PaymentStatus status { get; set; }
        public DateTime createdAt { get; set; }
    }
}
