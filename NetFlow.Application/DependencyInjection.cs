using Microsoft.Extensions.DependencyInjection;
using NetFlow.Application.Firms;
using NetFlow.Application.GuaranteeCommissions;
using NetFlow.Application.Guarantees;
using NetFlow.Application.Modules;
using NetFlow.Application.Netsis.AccountingVouchers;
using NetFlow.Application.Netsis.Customers;
using NetFlow.Application.Netsis.Orders;
using NetFlow.Application.Netsis.Products;
using NetFlow.Application.Netsis.Shipments;
using NetFlow.Application.Netsis.Warehouses;
using NetFlow.Application.Roles;
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
            //NETSİS
            services.AddScoped<CustomerService>();
            services.AddScoped<OrderService>();
            services.AddScoped<ProductService>();
            services.AddScoped<ShipmentService>();
            services.AddScoped<WarehouseService>();
            services.AddScoped<AccountingVoucherService>();

            //NETFLOW
            services.AddScoped<FirmWriteService>();
            services.AddScoped<RoleWriteService>();
            services.AddScoped<ModuleWriteService>();
            services.AddScoped<UserWriteService>();
            services.AddScoped<GuaranteeWriteService>();
            services.AddScoped<GuaranteeCommissionWriteService>();
            return services;
        }
    }
}
