using FileTrackingSystem.DAL.Contract;
using FileTrackingSystem.Models.Enums;
using FileTrackingSystem.Models.Models;
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
        public getEnumList(IGenericDbService<Company> company)
        {
            _company = company;
        }
        public async Task<string> getEnumRecords(string val, string zone = "")
        {


            if (val.Equals("status"))
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(Enum.GetNames(typeof(StatusType)));
            }
            else if (val.Equals("gender"))
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(Enum.GetNames(typeof(Gender)));
            }
            else if (val.Equals("usertype"))
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(Enum.GetNames(typeof(UserType)).Where(x => !x.Equals("User")).ToList());
            }                     
            else if (val.Equals("company"))
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(_company.AsQueryable().Select(x => x.Name).ToList());
            }
            else if (val.Contains("month"))
            {
                var ls = new List<string>(CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames);
                return Newtonsoft.Json.JsonConvert.SerializeObject(ls);
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
                return Newtonsoft.Json.JsonConvert.SerializeObject(ls);
            }

            return "";
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
