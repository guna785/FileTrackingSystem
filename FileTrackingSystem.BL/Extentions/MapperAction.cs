using FileTrackingSystem.BL.SchemaModel;
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
       
    }

}
