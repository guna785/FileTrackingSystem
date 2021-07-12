using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.Models
{
    public class ResetPasswordView
    {
        [Required]
        public string email { get; set; }
    }
}
