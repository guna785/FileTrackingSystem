using FileTrackingSystem.BL.Models;
using FileTrackingSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.Contract
{
    public interface IGet
    {
        public IQueryable<Client> GetAllClients();
        public IQueryable<JobType> GetJobTypes();
        public InvoiceDocumentModel GetInvoiceDocument(int Id);
        public IQueryable<ApplicationUser> GetAllEmployees();
    }
}
