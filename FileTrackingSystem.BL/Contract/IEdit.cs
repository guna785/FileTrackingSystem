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
        Task<bool> EditCompany(CompanySchema model, string user);
        Task<bool> EditBranch(BranchSchema model, string user);
        Task<bool> EditUser(UserSchema model, string user);
        Task<bool> EditRole(RoleSchema model, string user);
        Task<bool> EditEmployee(EmployeeSchema model, string user);
        Task<bool> EditClient(ClientSchema model, string user);
        Task<bool> EditJobType(JobTypeSchema model, string user);
        Task<bool> EditDocument(DocumentSchema model, string user);

    }
}
