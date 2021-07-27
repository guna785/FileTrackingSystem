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
        Task<bool> InsertCompany(CompanySchema model, string user);
        Task<bool> InsertUser(UserSchema model, string user);
        Task<bool> InsertEmployee(EmployeeSchema model, string user);
        
    }
}
