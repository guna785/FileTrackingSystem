using FileTrackingSystem.Models.Contract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Models.Models
{
    public class ApplicationRole : IdentityRole<int>, ICommonModel
    {
        public int ApplicationUserId { get; set; }
        public string description { get; set; }
        public DateTime createdAt { get; set; }
    }
}
