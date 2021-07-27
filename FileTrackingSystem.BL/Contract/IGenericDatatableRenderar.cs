using FileTrackingSystem.BL.DataTableModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.Contract
{
    public interface IGenericDatatableRenderar
    {
        dynamic CompanyJson(DtParameters parameters);
        dynamic AdminJson(DtParameters parameters);
        dynamic EmployeeJson(DtParameters parameters);
        dynamic RoleJson(DtParameters parameters);
        dynamic ClientJson(DtParameters parameters);
        dynamic JobJson(DtParameters parameters);
        dynamic JobTypeJson(DtParameters parameters);
        dynamic DocumentJson(DtParameters parameters);
        dynamic InvoiceJson(DtParameters parameters);
        dynamic PaymentJson(DtParameters parameters);

    }
}
