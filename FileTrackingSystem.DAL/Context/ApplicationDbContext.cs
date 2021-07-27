using FileTrackingSystem.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<Document>().ToTable("Document");
            modelBuilder.Entity<DocumentRequired>().ToTable("DocumentRequired");
            modelBuilder.Entity<PendingDocument>().ToTable("PendingDocument");
            modelBuilder.Entity<SubmittedDocument>().ToTable("SubmittedDocument");
            modelBuilder.Entity<StateCode>().ToTable("StateCode");
            modelBuilder.Entity<Invoice>().ToTable("Invoice");
            modelBuilder.Entity<Payment>().ToTable("Payment");
            modelBuilder.Entity<Job>().ToTable("Job");
            modelBuilder.Entity<JobType>().ToTable("JobType");
            modelBuilder.Entity<Notifications>().ToTable("Notifications");
            modelBuilder.Entity<Branch>().ToTable("Branch");
            modelBuilder.Entity<Log>().ToTable("Log");

        }
    }
}
