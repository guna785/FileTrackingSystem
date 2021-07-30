using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Models.Enums
{
    public enum RolePermissions
    {
        AddNewJob,
        DocumentCollection,
        AssignJob,
        ReAssignJob,
        ProcessJob,
        CompleteJOb,
        CloseJob,
        AddClient,
        EditClient,
        EditJobDetails,
        AddNewDocumentType,
        AddNewJobType,
        GenerateInvoice,
        CollectPayment,
    }
}
