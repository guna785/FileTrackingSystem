using FileTrackingSystem.BL.Contract;
using FileTrackingSystem.BL.DataTableModel;
using FileTrackingSystem.BL.DataTableViewModel;
using FileTrackingSystem.BL.Extentions;
using FileTrackingSystem.BL.Models;
using FileTrackingSystem.DAL.Contract;
using FileTrackingSystem.Models.Enums;
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
        private readonly IGenericDbService<Document> _document;
        private readonly IGenericDbService<Payment> _payment;
        private readonly IGenericDbService<Invoice> _invoice;
        private readonly IGenericDbService<JobType> _jbType;
        private readonly IGenericDbService<Job> _job;
        private readonly IGenericDbService<Company> _company;
        private readonly IGenericDbService<Branch> _branch;
        private readonly IGenericDbService<Client> _client;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<ApplicationRole> _role;
        public GenericDatatableRenderar(IGenericDbService<Company> company, UserManager<ApplicationUser> user, RoleManager<ApplicationRole> role,
            IGenericDbService<Client> client, IGenericDbService<Job> job, IGenericDbService<JobType> jbType, IGenericDbService<Invoice> invoice,
            IGenericDbService<Payment> payment, IGenericDbService<Document> document, IGenericDbService<Branch> branch)
        {
            _user = user;
            _company = company;
            _role = role;
            _client = client;
            _jbType = jbType;
            _invoice = invoice;
            _job = job;
            _payment = payment;
            _document = document;
            _branch = branch;
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
                                           r.Address != null && r.Address.ToUpper().Contains(searchBy.ToUpper())
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
        public dynamic AdminJson(DtParameters parameters)
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

            var branch = _branch.AsQueryable();
            var company = _company.AsQueryable();
            var result = _user.Users.Where(x => x.userType == UserType.Admin);
            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.PhoneNumber != null && r.PhoneNumber.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Email != null && r.Email.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.UserName != null && r.UserName.ToUpper().Contains(searchBy.ToUpper())
                                          );
                branch = branch.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()));
                company = company.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()));
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = _user.Users.Where(x => x.userType == UserType.Admin);

            var totalResultsCount = cntdb.Count();

            return new
            {
                draw = parameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(parameters.Start)
                    .Take(parameters.Length)
                    .AsEnumerable().Select(x => new AdminView()
                    {
                        gender = x.gender,
                        Email = x.Email,
                        address = x.address,
                        createdAt = x.createdAt,
                        branchId = branch.Where(y => y.Id == x.branchId).FirstOrDefault().Name,
                        CompanyId = company.Where(y => y.Id == x.CompanyId).FirstOrDefault().Name,
                        idNumber = x.idNumber,
                        Name = x.Name,
                        idType = x.idType,
                        img = x.img,
                        PhoneNumber = x.PhoneNumber,
                        status = x.status,
                        UserName = x.UserName,
                        userType = x.userType,
                        Id = x.Id

                    })
                    .ToList()
            };
        }

        public dynamic EmployeeJson(DtParameters parameters)
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
            var branch = _branch.AsQueryable();
            var company = _company.AsQueryable();
            var result = _user.Users.Where(x => x.userType == UserType.User);
            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.PhoneNumber != null && r.PhoneNumber.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Email != null && r.Email.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.UserName != null && r.UserName.ToUpper().Contains(searchBy.ToUpper())
                                          );
                branch = branch.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()));
                company = company.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()));
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = _user.Users.Where(x => x.userType == UserType.User);

            var totalResultsCount = cntdb.Count();

            return new
            {
                draw = parameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(parameters.Start)
                    .Take(parameters.Length)
                     .AsEnumerable().Select(x => new AdminView()
                     {
                         gender = x.gender,
                         Email = x.Email,
                         address = x.address,
                         createdAt = x.createdAt,
                         branchId = branch.Where(y => y.Id == x.branchId).FirstOrDefault().Name,
                         CompanyId = company.Where(y => y.Id == x.CompanyId).FirstOrDefault().Name,
                         idNumber = x.idNumber,
                         Name = x.Name,
                         idType = x.idType,
                         img = x.img,
                         PhoneNumber = x.PhoneNumber,
                         status = x.status,
                         UserName = x.UserName,
                         userType = x.userType,
                         Id = x.Id

                     })
                    .ToList()
            };
        }

        public dynamic RoleJson(DtParameters parameters)
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
            var result = _role.Roles;
            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.description != null && r.description.ToUpper().Contains(searchBy.ToUpper())
                                          );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = _role.Roles;

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

        public dynamic ClientJson(DtParameters parameters)
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
            var result = _client.AsQueryable();
            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.name != null && r.name.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Phone != null && r.Phone.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Email != null && r.Email.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Pan != null && r.Pan.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Address != null && r.Pan.ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.clientType.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.Gender.ToString().ToUpper().Contains(searchBy.ToUpper()) ||
                                           r.ContactPersonName != null && r.ContactPersonName.ToUpper().Contains(searchBy.ToUpper())
                                        );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = _client.AsQueryable();

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

        public dynamic JobJson(DtParameters parameters, string catagory)
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
            var result =string.IsNullOrWhiteSpace(catagory)? _job.AsQueryable():catagory=="CompletedJobs"?_job.AsQueryable().Where(x=>x.status==JobStatus.JobCompleted) 
                            :catagory=="PendingJobs"?_job.AsQueryable().Where(x=>x.status!=JobStatus.JobCompleted)
                            :_job.AsQueryable().Where(x=>x.status==JobStatus.Cancelled);
            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.JbId != null && r.JbId.ToUpper().Contains(searchBy.ToUpper())

                                        );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = string.IsNullOrWhiteSpace(catagory) ? _job.AsQueryable() : catagory == "CompletedJobs" ? _job.AsQueryable().Where(x => x.status == JobStatus.JobCompleted)
                            : catagory == "PendingJobs" ? _job.AsQueryable().Where(x => x.status != JobStatus.JobCompleted)
                            : _job.AsQueryable().Where(x => x.status == JobStatus.Cancelled);

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

        public dynamic JobTypeJson(DtParameters parameters)
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
            var result = _jbType.AsQueryable();
            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper())

                                        );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = _jbType.AsQueryable();

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

        public dynamic DocumentJson(DtParameters parameters)
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
            var result = _document.AsQueryable();
            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper())

                                        );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = _document.AsQueryable();

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

        public dynamic InvoiceJson(DtParameters parameters)
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
            var result = (from i in _invoice.AsQueryable()
                          join j in _job.AsQueryable() on i.jobId equals j.Id
                          join c in _client.AsQueryable() on i.clientId equals c.Id
                          join cmp in _company.AsQueryable() on i.companyId equals cmp.Id
                          //where a.Status == "Pending"
                          select new InvoiceModel()
                          {
                              Amount = i.Amount,
                              balanceAmount = i.balanceAmount,
                              Id = i.Id,
                              clientId = c.Pan,
                              companyId = i.companyId.ToString(),
                              invId = i.invId,
                              jobId = j.JbId,
                              PaidAmount = i.PaidAmount,
                              status = i.status,
                              Tax = i.Tax,
                              TotalAmount = i.TotalAmount,
                              createdAt = i.createdAt
                          });
            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.invId != null && r.invId.ToUpper().Contains(searchBy.ToUpper())

                                        );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = _invoice.AsQueryable();

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

        public dynamic PaymentJson(DtParameters parameters)
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
            var result = (from p in _payment.AsQueryable()
                          join i in _invoice.AsQueryable() on p.invoiceId equals i.Id
                          join j in _job.AsQueryable() on p.jobId equals j.Id
                          join c in _client.AsQueryable() on p.clientId equals c.Id
                          join cmp in _company.AsQueryable() on p.companyId equals cmp.Id
                          //where a.Status == "Pending"
                          select new PaymentModel()
                          {
                              Amount = i.Amount,
                              Id = i.Id,
                              clientId = c.Pan,
                              companyId = i.companyId.ToString(),
                              invoiceId = i.invId,
                              jobId = j.JbId,
                              status = p.status,
                              createdAt = i.createdAt,
                               payId=p.payId,
                              ApplicationUserId = p.ApplicationUserId
                          }); ;
            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.payId != null && r.payId.ToUpper().Contains(searchBy.ToUpper())

                                        );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = _payment.AsQueryable();

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

        public dynamic BranchJson(DtParameters parameters)
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
            //var result = _branch.AsQueryable();
            var result = (from b in _branch.AsQueryable()
                          join c in _company.AsQueryable() on b.CompanyId equals c.Id
                          select new BranchView() { Id = b.Id, companyId = c.Name, Name = b.Name, createdAt = b.createdAt });
            if (!string.IsNullOrEmpty(searchBy))
            {
                result = result.Where(r => r.Name != null && r.Name.ToUpper().Contains(searchBy.ToUpper())

                                        );
            }

            result = orderAscendingDirection ? result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Asc) : result.AsQueryable().OrderByDynamic(orderCriteria, DtOrderDir.Desc);

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            var filteredResultsCount = result.Count();
            var cntdb = _branch.AsQueryable();

            var totalResultsCount = cntdb.Count();

            return new
            {
                draw = parameters.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result
                    .Skip(parameters.Start)
                    .Take(parameters.Length)
                    //.AsEnumerable().Select(x => new BranchView()
                    //{
                    //    Name = x.Name,
                    //    companyId = _company.FindById(x.CompanyId).Name,
                    //    createdAt = x.createdAt,
                    //    Id = x.Id
                    //})
                    .ToList()
            };
        }


    }
}
