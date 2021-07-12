﻿using FileTrackingSystem.BL.Contract;
using FileTrackingSystem.BL.Helper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileTrackingSystem.BL
{
    public static class BLExtensions
    {
        public static IServiceCollection AddBLExtensions(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IGenericDatatableRenderar), typeof(GenericDatatableRenderar));
            //services.AddScoped(typeof(IInsert), typeof(InsertControl));
            //services.AddScoped(typeof(IEdit), typeof(EditControl));
            services.AddScoped(typeof(IIdentityUserService), typeof(IdentityUserService));
            //services.AddScoped<EditBuilder>();
            return services;
        }
    }
}
