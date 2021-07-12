using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Models.Contract
{
    public interface ICommonModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime createdAt { get; set; }
    }
}
