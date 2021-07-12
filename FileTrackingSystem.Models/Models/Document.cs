using FileTrackingSystem.Models.BaseClass;
using FileTrackingSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Models.Models
{
    public class Document:CommonModel
    {
        public string Name { get; set; }
        public StatusType  status { get; set; }
        public string Remarks { get; set; }
        public DocType docType { get; set; }
    }
}
