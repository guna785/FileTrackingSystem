using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.Models
{
    public class JobPostViewModel
    {
        public string client { get; set; }
        public int jobtype { get; set; }
        public int amount { get; set; }
        public double tax { get; set; }
        public double totalAmount { get; set; }
        public int advanceAdmount { get; set; }
    }
}
