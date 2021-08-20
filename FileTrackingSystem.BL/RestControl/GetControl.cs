using FileTrackingSystem.BL.Contract;
using FileTrackingSystem.BL.Models;
using FileTrackingSystem.DAL.Contract;
using FileTrackingSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL.RestControl
{
    public class GetControl : IGet
    {
        private readonly IGenericDbService<Client> _client;
        private readonly IGenericDbService<JobType> _jbType;
        private readonly IGenericDbService<Company> _company;
        private readonly IGenericDbService<Job> _job;
        private readonly IGenericDbService<Invoice> _inv;
        private readonly IGenericDbService<Payment> _pay;
        private readonly IGenericDbService<StateCode> _codes;
        public GetControl(IGenericDbService<Client> client, IGenericDbService<JobType> jbType, IGenericDbService<Company> company,
            IGenericDbService<Job> job, IGenericDbService<Invoice> inv, IGenericDbService<Payment> pay, IGenericDbService<StateCode> codes)
        {
            _client = client;
            _jbType = jbType;
            _company = company;
            _inv = inv;
            _pay = pay;
            _job = job;
            _codes = codes;
        }
        public IQueryable<Client> GetAllClients()
        {
            return _client.AsQueryable();
        }

        public InvoiceDocumentModel GetInvoiceDocument(int Id)
        {
            var res = (from j in _job.AsQueryable()  
                       join i in _inv.AsQueryable() on j.Id equals i.jobId 
                       join jt in _jbType.AsQueryable() on j.jobTypeId equals jt.Id
                       join cl in _client.AsQueryable() on i.clientId equals cl.Id
                       join c in _company.AsQueryable() on i.companyId equals c.Id
                       where i.Id == Id
                       select new InvoiceDocumentModel()
                       {
                           amount = i.Amount.ToString(),
                           balanceAmount = i.balanceAmount.ToString(),
                           clientAddress = cl.Address,
                           clientEmail = cl.Email,
                           clientGST = cl.GSTNo,
                           clientName = cl.name,
                           clientPhone = cl.Phone,
                           clientType = cl.clientType.ToString(),
                           companyAddress = c.Address,
                           companyEmail = c.Email,
                           companyGST = c.GST,
                           companyName = c.Name,
                           companyPhone = c.Contact,
                           companyWeb = c.Web,
                           invoiceDate = i.createdAt,
                           invoiceId = i.invId,
                           jobId = j.JbId,
                           jobType = jt.Name,
                           notes = "",
                           paidAmount = i.PaidAmount.ToString(),
                           tax = i.Tax.ToString(),
                           totalAmount = i.TotalAmount.ToString(),


                       }).FirstOrDefault();
            var py = _pay.AsQueryable().Where(x => x.invoiceId == Id).Select(x => x.payId).ToArray();

            res.payIds = py;
            var cod = res.companyGST.Substring(0, 2);
            var clCod = res.clientGST.Substring(0, 2);
            res.isOtherState = cod.Equals(clCod);
            return res;

        }

        public IQueryable<JobType> GetJobTypes()
        {
            return _jbType.AsQueryable();
        }
    }
}
