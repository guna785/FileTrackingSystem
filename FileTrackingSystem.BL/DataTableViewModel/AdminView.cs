using FileTrackingSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.DataTableViewModel
{
    public class AdminView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string address { get; set; }
        public IdProofType idType { get; set; }
        public string idNumber { get; set; }
        public string img { get; set; }
        public StatusType status { get; set; }
        public Gender gender { get; set; }
        public UserType userType { get; set; }
        public DateTime createdAt { get; set; }
        public string CompanyId { get; set; }
        public string branchId { get; set; }
    }
}
