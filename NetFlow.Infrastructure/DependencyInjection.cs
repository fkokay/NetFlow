using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetFlow.Application.Auth;
using NetFlow.Domain.Common;
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
                var firm = sp.GetRequiredService<ICurrentFirmProvider>();
                return new CachedFileSqlProvider(sqlPath, firm);
            });
            
            services.AddDbContext<NetFlowDbContext>(options => options.UseSqlServer(config.GetConnectionString("MakroFlow")));

            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
