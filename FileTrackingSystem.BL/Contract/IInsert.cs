using FileTrackingSystem.BL.SchemaModel;
using Microsoft.AspNetCore.Http;
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
        Task<bool> InsertBranch(BranchSchema model, string user);
        Task<bool> InsertUser(UserSchema model, HttpContext context);
        Task<bool> InsertRole(RoleSchema model, string user);
        Task<bool> InsertEmployee(EmployeeSchema model, HttpContext context);
        Task<bool> InsertClient(ClientSchema model, HttpContext context);
        Task<bool> InsertJobType(JobTypeSchema model, HttpContext context);
        Task<bool> InsertDocument(DocumentSchema model, HttpContext context);

    }
}
