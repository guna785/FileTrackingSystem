using FileTrackingSystem.Models.BaseClass;
using FileTrackingSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Models.Models
{
    public class Payment:CommonModel
    {
        public int invoiceId { get; set; }
        public int Amount { get; set; }
        public int ApplicationUserId { get; set; }
        public PaymentStatus status { get; set; }
    }
}
