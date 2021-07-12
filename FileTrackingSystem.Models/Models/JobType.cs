using FileTrackingSystem.Models.BaseClass;
using FileTrackingSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Models.Models
{
    public class JobType:CommonModel
    {
        public string Name { get; set; }
        public StatusType status { get; set; }
        public int createdBy { get; set; }
        public string Remarks { get; set; }
        public virtual IList<DocumentRequired>   documentRequireds { get; set; }
        public virtual IList<Job> jobs { get; set; }
    }
}
