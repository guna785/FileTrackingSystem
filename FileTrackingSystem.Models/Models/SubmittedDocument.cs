using FileTrackingSystem.Models.BaseClass;
using FileTrackingSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Models.Models
{
    public class SubmittedDocument : CommonModel
    {
        public int documentId { get; set; }
        public int jobId { get; set; }
        public StatusType status { get; set; }
    }
    public class PendingDocument : CommonModel
    {
        public int documentId { get; set; }
        public int jobId { get; set; }
        public StatusType status { get; set; }
    }
}
