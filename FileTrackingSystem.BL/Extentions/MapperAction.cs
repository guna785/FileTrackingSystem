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
        public static Company toCompany(this CompanySchema schema)
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
        public static Branch toBranch(this BranchSchema schema)
        {

            return new Branch()
            {
                CompanyId = Convert.ToInt32(schema.CompanyId),
                createdAt = DateTime.Now,
                Name = schema.Name
            };
        }
        public static ApplicationUser toUser(this UserSchema schema, int Id)
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
                CompanyId = Id,
                branchId = Convert.ToInt32(schema.branchId),
                userType = schema.userType,
                UserName = schema.userName

            };
        }
        public static ApplicationRole toRole(this RoleSchema role)
        {
            return new ApplicationRole()
            {
                createdAt = DateTime.Now,
                description = role.Discription,
                Name = role.Name,
                permissions = role.Permission
            };
        }
        public static JobType toJobType(this JobTypeSchema jbtype, int id)
        {
            return new JobType()
            {
                createdAt = DateTime.Now,
                createdBy = id,
                Name = jbtype.Name,
                status = jbtype.status
            };
        }
        public static Document toDocument(this DocumentSchema doc)
        {
            return new Document()
            {
                createdAt = DateTime.Now,
                Name = doc.Name,
                status = doc.status,
                docType = doc.docType,
                Remarks = "none"
            };
        }
        public static ApplicationUser toEmployee(this EmployeeSchema schema, int Id)
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
                CompanyId = Id,
                branchId = Convert.ToInt32(schema.branchId),
                userType = schema.userType,
                UserName = schema.userName

            };
        }
        public static Client toClient(this ClientSchema schema, int user)
        {

            return new Client()
            {
                Address = schema.Address,
                clientType = schema.clientType,
                ContactPersonName = schema.ContactPersonName,
                createdAt = DateTime.Now,
                createdBy = user,
                Dob = schema.Dob,
                Email = schema.Email,
                fatherName = schema.fatherName,
                Gender = schema.Gender,
                GSTNo = schema.GSTNo,
                idProof = Convert.FromBase64String(schema.photo),
                idProoftype = schema.idProoftype,
                name = schema.name,
                Pan = schema.Pan,
                Phone = schema.Phone,
                Remarks = schema.Remarks
            };
        }
        public static Log CreateLog(string name, string message, string user, LogType log)
        {
            return new Log()
            {
                createdAt = DateTime.Now,
                eventName = name,
                message = message,
                userName = user,
                logType = log
            };
        }

    }

}
