using FileTrackingSystem.BL.Contract;
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
        public GetControl(IGenericDbService<Client> client, IGenericDbService<JobType> jbType)
        {
            _client = client;
            _jbType = jbType;
        }
        public IQueryable<Client> GetAllClients()
        {
            return _client.AsQueryable();
        }

        public IQueryable<JobType> GetJobTypes()
        {
            return _jbType.AsQueryable();
        }
    }
}
