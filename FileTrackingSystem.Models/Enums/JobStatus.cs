using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Models.Enums
{
    public enum JobStatus
    {
        New,
        DocumentPending,
        DocumentCollected,
        JobCompletedBillPending,
        JobCompleted,
        Cancelled
    }
}
