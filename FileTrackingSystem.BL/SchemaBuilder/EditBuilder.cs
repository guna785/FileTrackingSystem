using FileTrackingSystem.BL.SchemaModel;
using FileTrackingSystem.DAL.Contract;
using FileTrackingSystem.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.SchemaBuilder
{
    public class EditBuilder
    {
        private readonly IGenericDbService<Client> _client;
        private readonly IGenericDbService<Company> _company;
        private readonly IGenericDbService<Branch> _branch;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<ApplicationRole> _role;
        public EditBuilder(IGenericDbService<Company> company,
            UserManager<ApplicationUser> user, IGenericDbService<Branch> branch,
            RoleManager<ApplicationRole> role, IGenericDbService<Client> client)
        {
            _branch = branch;
            _company = company;
            _user = user;
            _role = role;
            _client = client;
        }
        public async Task<T> ReturnObjectData<T>(int id)
        {
            var obj = typeof(T).Name;
            if (obj.Equals("CompanySchema"))
            {
                var obdata = _company.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
                return (T)Convert.ChangeType(new CompanySchema()
                {
                    Id = obdata.Id,
                    Address = obdata.Address,
                    Email = obdata.Email,
                    GST = obdata.GST,
                    HSN = obdata.HSN,
                    Pan = obdata.Pan,
                    Web = obdata.Web,
                    TIN = obdata.TIN,
                    IFSC = obdata.IFSC,
                    Name = obdata.Name,
                    BankAccNo = obdata.BankAccNo,
                    BankBranch = obdata.BankBranch,
                    BankName = obdata.BankName,
                    Contact = obdata.Contact
                }, typeof(T));
            }
            else if (obj.Equals("BranchSchema"))
            {
                var obdata = _branch.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
                return (T)Convert.ChangeType(new BranchSchema()
                {
                    Id = obdata.Id,
                    CompanyId = _company.FindById(obdata.CompanyId).Name,
                    Name = obdata.Name,
                }, typeof(T));
            }
            else if (obj.Equals("UserSchema"))
            {
                var obdata = _user.Users.Where(x => x.Id == id).FirstOrDefault();
                return (T)Convert.ChangeType(new UserSchema()
                {
                    Id = obdata.Id,
                    branchId = _branch.FindById(obdata.branchId).Name,
                    email = obdata.Email,
                    gender = obdata.gender,
                    phone = obdata.PhoneNumber,
                    userName = obdata.UserName,
                    Name = obdata.Name,
                }, typeof(T));
            }
            else if (obj.Equals("RoleSchema"))
            {
                var obdata = _role.Roles.Where(x => x.Id == id).FirstOrDefault();
                return (T)Convert.ChangeType(new RoleSchema()
                {
                    Id = obdata.Id,
                    Discription = obdata.description,
                    Name = obdata.Name,
                    Permission = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(obdata.permissions)
                }, typeof(T));
            }
            else if (obj.Equals("EmployeeSchema"))
            {
                var obdata = _user.Users.Where(x => x.Id == id).FirstOrDefault();
                return (T)Convert.ChangeType(new EmployeeSchema()
                {
                    Id = obdata.Id,
                    branchId = _branch.FindById(obdata.branchId).Name,
                    Name = obdata.Name,
                    email = obdata.Email,
                    gender = obdata.gender,
                    phone = obdata.PhoneNumber,
                    userName = obdata.UserName,
                    userType = obdata.userType
                }, typeof(T));
            }
            else if (obj.Equals("ClientSchema"))
            {
                var obdata = _client.FindById(id);
                return (T)Convert.ChangeType(new ClientSchema()
                {
                    Address = obdata.Address,
                    clientType = obdata.clientType,
                    ContactPersonName = obdata.ContactPersonName,
                    Dob = obdata.Dob,
                    Email = obdata.Email,
                    fatherName = obdata.fatherName,
                    Gender = obdata.Gender,
                    GSTNo = obdata.GSTNo,
                    Id = obdata.Id,
                    idProoftype = obdata.idProoftype,
                    name = obdata.name,
                    Pan = obdata.Pan,
                    Phone = obdata.Phone,
                    Remarks = obdata.Remarks
                }, typeof(T));

            }

            return (T)Convert.ChangeType(null, typeof(T));
        }
    }
}
