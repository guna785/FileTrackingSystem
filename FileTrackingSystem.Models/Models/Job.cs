using FileTrackingSystem.Models.BaseClass;
using FileTrackingSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Models.Models
{
    public class Job:CommonModel
    {
        public int jobTypeId { get; set; }
        public ClientType clientType { get; set; }
        public int clientId { get; set; }
        public int ApplicationUserId { get; set; }
        public DateTime AllotedTime { get; set; }
        public JobStatus status { get; set; }
        public DateTime CompletedTime { get; set; }
        public int createdBy { get; set; }
        public virtual IList<SubmittedDocument> SubmittedDocuments { get; set; }
        public virtual IList<PendingDocument> PendingDocuments { get; set; }
        public virtual Invoice invoice { get; set; }
        public virtual IList<Payment> Payments { get; set; }


    }
}
