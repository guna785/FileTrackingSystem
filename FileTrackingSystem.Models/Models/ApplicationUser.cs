using FileTrackingSystem.Models.Contract;
using FileTrackingSystem.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Models.Models
{
    public class ApplicationUser : IdentityUser<int>, ICommonModel
    {
        public string Name { get; set; }
        public string address { get; set; }
        public IdProofType idType { get; set; }
        public string idNumber { get; set; }
        public string img { get; set; }
        public StatusType status { get; set; }
        public Gender gender { get; set; }
        public UserType userType { get; set; }
        public DateTime createdAt { get; set; }
        public int CompanyId { get; set; }
        public int branchId { get; set; }

    }
}
