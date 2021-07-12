using FileTrackingSystem.Models.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Models.Models
{
    public class StateCode:CommonModel
    {
        public int Code { get; set; }
        public string Name { get; set; }
    }
}
