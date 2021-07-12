using FileTrackingSystem.Models.BaseClass;
using FileTrackingSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Models.Models
{
    public class Notifications:CommonModel
    {
        public int jobId { get; set; }
        public int ApplicationUserId { get; set; }
        public int ClientId { get; set; }
        public string Message { get; set; }
        public NotificationStatus status { get; set; }
    }
}
