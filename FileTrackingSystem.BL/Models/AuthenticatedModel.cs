using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.Models
{
    public class AuthenticatedModel
    {
        public string ID { get; set; }
        public string uname { get; set; }
        public string name { get; set; }
        public SignInResult sign { get; set; }
        public string role { get; set; }
    }
}
