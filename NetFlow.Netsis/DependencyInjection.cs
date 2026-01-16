using Microsoft.Extensions.DependencyInjection;
using NetFlow.Application.Netsis.Shipments;
using NetFlow.Netsis.Connection;
using NetFlow.Netsis.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Netsis
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNetsis(this IServiceCollection services)
        {
            services.AddScoped<NetsisConnectionFactory>();
            services.AddScoped<IShipmentReadRepository, NetsisShipmentReadRepository>();
            return services;
        }
    }
}
