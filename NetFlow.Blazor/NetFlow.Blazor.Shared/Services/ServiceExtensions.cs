using DevExpress.Blazor;
using DevExpress.Blazor.Internal;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlow.Blazor.Shared.Services
{
    public static class ServiceExtensions
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddDevExpressBlazor();
            services.AddScoped<SearchManager>();
            services.AddScoped<ModuleLoader>();
            services.AddScoped<ThemeManager>();
            services.AddScoped<ClipboardManager>();
            services.AddScoped<SizeModeManager>();
            services.AddCascadingValue("NotificationCount", sp => 4);
            services.AddScoped(sp => new CascadingValueSource<SizeMode>("ParentSizeMode", SizeMode.Medium, false));
            services.AddCascadingValue(sp => sp.GetRequiredService<CascadingValueSource<SizeMode>>());
        }
    }
}
