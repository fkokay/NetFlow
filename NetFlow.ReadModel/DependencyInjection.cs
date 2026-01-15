using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetFlow.ReadModel.Assets;
using NetFlow.ReadModel.Firms;
using NetFlow.ReadModel.Guarantees;
using NetFlow.ReadModel.Roles;
using NetFlow.ReadModel.TenderDevices;
using NetFlow.ReadModel.TenderDocuments;
using NetFlow.ReadModel.TenderExternalQuality;
using NetFlow.ReadModel.TenderOpex;
using NetFlow.ReadModel.TenderReaktif;
using NetFlow.ReadModel.Tenders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.ReadModel
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddReadModel(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(new ReadModelOptions
            {
                ConnectionString = config.GetConnectionString("MakroFlow")
                    ?? throw new InvalidOperationException("MakroFlow connection string missing.")
            });

            services.AddScoped<FirmReadService>();
            services.AddScoped<TenderReadService>();
            services.AddScoped<TenderAuthorityReadService>();
            services.AddScoped<TenderCapexReadService>();
            services.AddScoped<TenderDeviceReadService>();
            services.AddScoped<TenderOpexReadService>();
            services.AddScoped<TenderReaktifReadService>();
            services.AddScoped<TenderRequiredDocumentReadService>();
            services.AddScoped<TenderExternalQualityReadService>();
            services.AddScoped<GuaranteeReadService>();
            services.AddScoped<RoleReadService>();
            services.AddScoped<AssetReadService>();

            return services;
        }
    }

    public sealed class ReadModelOptions
    {
        public required string ConnectionString { get; init; }
    }
}
