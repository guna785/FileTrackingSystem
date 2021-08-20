using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.Models
{
    public class InvoiceDocumentModel
    {
        public string invoiceId { get; set; }
        public string amount { get; set; }
        public string paidAmount { get; set; }
        public string balanceAmount { get; set; }
        public string[] payIds { get; set; }
        public string jobId { get; set; }
        public string jobType { get; set; }
        public string tax { get; set; }
        public string totalAmount { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string companyGST { get; set; }
        public string companyEmail { get; set; }
        public string companyPhone { get; set; }
        public string companyWeb { get; set; }
        public string clientName { get; set; }
        public string clientType { get; set; }
        public string clientAddress { get; set; }
        public string clientGST { get; set; }
        public string clientPhone { get; set; }
        public string clientEmail { get; set; }
        public DateTime invoiceDate { get; set; }
        public bool isOtherState { get; set; }
        public string notes { get; set; }
    }
}
