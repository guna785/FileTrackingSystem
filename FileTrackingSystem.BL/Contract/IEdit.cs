using FileTrackingSystem.BL.SchemaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.Contract
{
    public interface IEdit
    {
        Task<bool> EditCompany(EditCompanySchema model, string user);
    }
}
