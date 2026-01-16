using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetFlow.Application.Auth;
using NetFlow.Application.Common.Interfaces;
using NetFlow.Domain.Common;
using NetFlow.Domain.Identity;
using NetFlow.Infrastructure.Common;
using NetFlow.Infrastructure.Identity;
using NetFlow.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config, string contentRootPath)
        {
            var sqlPath = Path.Combine(contentRootPath, "Sql");

            services.AddScoped<ISqlProvider>(sp =>
            {
                return new CachedFileSqlProvider(sqlPath);
            });
            
            services.AddScoped<INetFlowDbContext, NetFlowDbContext> ();
            services.AddDbContext<NetFlowDbContext>(options => options.UseSqlServer(config.GetConnectionString("MakroFlow")));

            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
