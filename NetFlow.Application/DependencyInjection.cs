using Microsoft.Extensions.DependencyInjection;
using NetFlow.Application.Firms;
using NetFlow.Application.Modules;
using NetFlow.Application.Roles;
using NetFlow.Application.Shipping;
using NetFlow.Application.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddScoped<ApproveOrderHandler>();
            //services.AddScoped<CreateOrderHandler>();
            services.AddScoped<ShipmentService>();
            services.AddScoped<FirmWriteService>();
            services.AddScoped<RoleWriteService>();
            services.AddScoped<ModuleWriteService>();
            services.AddScoped<UserWriteService>();
            return services;
        }
    }
}
