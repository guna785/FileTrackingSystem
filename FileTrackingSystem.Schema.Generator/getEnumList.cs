using FileTrackingSystem.DAL.Contract;
using FileTrackingSystem.Models.Enums;
using FileTrackingSystem.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Schema.Generator
{
    public class getEnumList
    {
        private readonly IGenericDbService<Company> _company;
        private readonly IGenericDbService<Branch> _branch;
        private readonly RoleManager<ApplicationRole> _role;
        public getEnumList(IGenericDbService<Company> company, IGenericDbService<Branch> branch, RoleManager<ApplicationRole> role)
        {
            _company = company;
            _branch = branch;
            _role = role;
        }
        public async Task<dynamic> getEnumRecords(string val, string zone = "")
        {


            if (val.Equals("status"))
            {
                return Enum.GetNames(typeof(StatusType));
            }
            else if (val.Equals("gender"))
            {
                return Enum.GetNames(typeof(Gender));
            }
            else if (val.Equals("usertype-admin") )
            {
                return Enum.GetNames(typeof(UserType)).Where(x => x.Equals("Admin")).ToList();
            }     
            else if (val.Equals("usertype-User"))
            {
                return Enum.GetNames(typeof(UserType)).Where(x => x.Equals("User")).ToList();
            }
            else if (val.Equals("company"))
            {
                return _company.AsQueryable().Select(x => x.Name).ToList();
            }
            else if (val.Equals("branch"))
            {
                return _branch.AsQueryable().Select(x => x.Name).ToList();
            }
            else if (val.Equals("role"))
            {
                return _role.Roles.Select(x=>x.Name).ToList();
            }
            else if (val.Equals("permission"))
            {
                return Enum.GetNames(typeof(RolePermissions));
            }
            else if (val.Equals("clientType"))
            {
                return Enum.GetNames(typeof(ClientType));
            }
            else if (val.Equals("idProoftype"))
            {
                return Enum.GetNames(typeof(IdProofType));
            }
            else if (val.Contains("month"))
            {
                var ls = new List<string>(CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames);
                return ls;
            }
            else if (val.Contains("year"))
            {
                var ls = new List<string>();
                int y = 2017;
                for (int i = 0; i <= DateTime.Now.Year; i++)
                {
                    ls.Add(y.ToString());
                    y++;
                }
                return ls;
            }

            return null;
        }

        public async Task<string> getVlidationMessage(string val)
        {
            var msg = new
            {
                required = val + " is Required Property",
                pattern = "Correct format of " + val

            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(msg);
        }
    }
}
