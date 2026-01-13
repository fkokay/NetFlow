using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NetFlow.Blazor.Shared.Services;
using NetFlow.Blazor.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddDevExpressBlazor();

await builder.Build().RunAsync();
