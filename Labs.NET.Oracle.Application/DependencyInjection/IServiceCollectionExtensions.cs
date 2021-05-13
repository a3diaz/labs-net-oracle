using Labs.NET.Oracle.Application.Services;
using Labs.NET.Oracle.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labs.NET.Oracle.Application.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDefaultServices(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            services.Add(ServiceDescriptor.Describe(typeof(IPhoneManager), typeof(PhoneManager), lifetime));
            services.Add(ServiceDescriptor.Describe(typeof(IPersonManager), typeof(PersonManager), lifetime));

            return services;
        }
    }
}
