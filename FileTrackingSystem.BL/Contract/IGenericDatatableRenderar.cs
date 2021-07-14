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

    }
}
