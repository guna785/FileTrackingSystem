using FileTrackingSystem.BL.Contract;
using FileTrackingSystem.BL.Extentions;
using FileTrackingSystem.BL.SchemaModel;
using FileTrackingSystem.DAL.Contract;
using FileTrackingSystem.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.RestControl
{
    public class InsertControl : IInsert
    {
        private readonly IGenericDbService<Company> _company;
        private readonly UserManager<ApplicationUser> _user;
        public InsertControl(IGenericDbService<Company> company, UserManager<ApplicationUser> user)
        {
            _user = user;
            _company = company;
        }       
        public async Task<bool> InsertCompany(AddCompanySchema model, string user)
        {
            try
            {
                _company.Create(model.toCompany());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
