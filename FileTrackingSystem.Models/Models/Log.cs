using FileTrackingSystem.Models.BaseClass;
using FileTrackingSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Models.Models
{
    public class Log : CommonModel
    {
        public LogType logType { get; set; }
        public string eventName { get; set; }
        public string userName { get; set; }
        public string message { get; set; }
    }
}
