using FileTrackingSystem.BL.SchemaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.Contract
{
    public interface IInsert
    {
        Task<bool> InsertCompany(AddCompanySchema model, string user);
        Task<bool> InsertUser(AddUserSchema model, string user);
        Task<bool> InsertEmployee(AddEmployeeSchema model, string user);
    }
}
