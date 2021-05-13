using Labs.NET.Oracle.Domain.Data;
using Labs.NET.Oracle.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labs.NET.Oracle.Infrastructure.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddOracleRepositories(this IServiceCollection services, string connectionString, 
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            services.AddDbContext<LabsContext>(options =>
            {
                options.UseOracle(connectionString);
            }, lifetime);

            services.Add(ServiceDescriptor.Describe(typeof(IUnitOfWork), typeof(UnitOfWork), lifetime));
            services.Add(ServiceDescriptor.Describe(typeof(IPersonRepository), typeof(PersonRepository), lifetime));
            services.Add(ServiceDescriptor.Describe(typeof(IPhoneRepository), typeof(PhoneRepository), lifetime));
            
            return services;
        }
    }
}
