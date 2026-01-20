using Microsoft.Extensions.DependencyInjection;
using NetFlow.Application.Netsis.AccountingVouchers;
using NetFlow.Application.Netsis.Customers;
using NetFlow.Application.Netsis.ExpenseAccountCodes;
using NetFlow.Application.Netsis.Orders;
using NetFlow.Application.Netsis.Products;
using NetFlow.Application.Netsis.Shipments;
using NetFlow.Application.Netsis.Warehouses;
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
            services.AddScoped<ICustomerReadRepository, NetsisCustomerReadRepository>();
            services.AddScoped<IProductReadRepository, NetsisProductReadRepository>();
            services.AddScoped<IShipmentReadRepository, NetsisShipmentReadRepository>();
            services.AddScoped<IWarehouseReadRepository, NetsisWarehouseReadRepository>();
            services.AddScoped<IOrderReadRepository, NetsisOrderReadRepository>();
            services.AddScoped<IAccountingVoucherReadRepository, NetsisAccountingVoucherReadRepository>();
            services.AddScoped<IExpenseAccountCodeReadRepository,NetsisExpenseAccountCodeRepository>();
            return services;
        }
    }
}
