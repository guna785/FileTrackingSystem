using FileTrackingSystem.Models.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Models.Models
{
    public class Branch:CommonModel
    {
        public string Name { get; set; }
        public int CompanyId { get; set; }
    }
}
