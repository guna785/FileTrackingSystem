using FileTrackingSystem.BL.SchemaModel;
using FileTrackingSystem.Models.Enums;
using FileTrackingSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.Extentions
{
    public static class MapperAction
    {
        public static Company toCompany(this AddCompanySchema schema)
        {
            return new Company()
            {
                Id = schema.Id,
                Address = schema.Address,
                createdAt = DateTime.Now,
                Email = schema.Email,
                Name = schema.Name,
                Contact = schema.Contact,
                status = schema.status,
                Pan = schema.Pan,
                GST = schema.GST,
                TIN = schema.TIN,
                BankAccNo = schema.BankAccNo,
                BankBranch = schema.BankBranch,
                BankName = schema.BankName,
                HSN = schema.HSN,
                IFSC = schema.IFSC,
                Web = schema.Web
            };
        }
        public static ApplicationUser toUser(this AddUserSchema schema)
        {
            return new ApplicationUser()
            {
                Id = schema.Id,
                createdAt = DateTime.Now,
                Email = schema.email,
                Name = schema.Name,
                PhoneNumber = schema.phone,
                status = StatusType.Active,
                gender = schema.gender,
                CompanyId = Convert.ToInt32(schema.CompanyId),
                userType = schema.userType,
                UserName = schema.userName

            };
        }
        public static ApplicationUser toEmployee(this AddEmployeeSchema schema)
        {
            return new ApplicationUser()
            {
                Id = schema.Id,
                createdAt = DateTime.Now,
                Email = schema.email,
                Name = schema.Name,
                PhoneNumber = schema.phone,
                status = StatusType.Active,
                gender = schema.gender,
                CompanyId = Convert.ToInt32(schema.CompanyId),
                userType = schema.userType,
                UserName = schema.userName

            };
        }

    }

}
