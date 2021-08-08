using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.Schema.Generator
{
    public static class GschemaExtentions
    {
        public static IServiceCollection AddGschemaExtentions(this IServiceCollection services)
        {
            services.AddScoped<getEnumList>();
           // services.AddScoped<GSgenerator>();
            services.AddScoped<SchemaGenerator>();
            return services;
        }
    }
}
