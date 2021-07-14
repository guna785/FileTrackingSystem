using FileTrackingSystem.BL.Contract;
using FileTrackingSystem.BL.DataTableModel;
using FileTrackingSystem.BL.Extentions;
using FileTrackingSystem.DAL.Contract;
using FileTrackingSystem.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.GenericDatatablesFN
{
    public class GenericDatatableRenderar : IGenericDatatableRenderar
    {
        
        private readonly IGenericDbService<Company> _company;
        private readonly UserManager<ApplicationUser> _user;
        public GenericDatatableRenderar(IGenericDbService<Company> company, UserManager<ApplicationUser> user)
        {            
            _user = user;
            _company = company;
        }

        public dynamic CompanyJson(DtParameters parameters)
        {
            var searchBy = parameters.Search?.Value;
            var orderCriteria = string.Empty;
            var orderAscendingDirection = true;

            if (parameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = "createdAt";
                orderAscendingDirection = parameters.Order[0].Dir.ToString().ToLower() != "asc";
            }
            else
            {
                // if we have an empty search then just order the results by Id ascending
                orderCriteria = "createdAt";
                orderAscendingDirection = false;
            }
            var result = _company.AsQueryable();
            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()) || 
                                           r.Address!=null && r.Address.ToUpper().Contains(searchBy.ToUpper()) 
                                          );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = _company.AsQueryable();

            var totalResultsCount = cntdb.Count();

            return new
            {
                draw = parameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(parameters.Start)
                    .Take(parameters.Length)
                    .ToList()
            };
        }

    }
}
