using FileTrackingSystem.DAL.Context;
using FileTrackingSystem.DAL.Contract;
using FileTrackingSystem.DAL.Repository;
using FileTrackingSystem.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.DAL
{
    public static class StartUpExtentions
    {
        public static IServiceCollection AddDbContextService(this IServiceCollection services)
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path);
            var root = config.Build();
            var conStr = root.GetSection("DataBaseConnections:connStr").Value;
            var lockoutOptions = new LockoutOptions()
            {
                AllowedForNewUsers = true,
                DefaultLockoutTimeSpan = TimeSpan.FromHours(10),
                MaxFailedAccessAttempts = 3
            };
            services.AddDefaultIdentity<ApplicationUser>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
                opt.Lockout = lockoutOptions;
                opt.SignIn.RequireConfirmedAccount = true;
                opt.SignIn.RequireConfirmedEmail = true;
                opt.User.RequireUniqueEmail = true;
            }).AddRoles<ApplicationRole>()
                  .AddEntityFrameworkStores<IdentityContext>()
                  .AddDefaultTokenProviders();
            services.AddDbContext<IdentityContext>(options => options.UseMySQL(conStr));
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(conStr));
            services.AddScoped(typeof(IGenericDbService<>), typeof(GenericDbService<>));

            return services;
        }
    }
}
