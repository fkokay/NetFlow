using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NetFlow.Blazor.Shared;
using NetFlow.Blazor.Web.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddDevExpressBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ITokenStore, WebTokenStore>();

builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();
builder.Services.AddAuthorizationCore();

builder.Services.AddTransient<JwtHandler>();

builder.Services.AddHttpClient("api", c =>
{
    c.BaseAddress = new Uri("https://localhost:7071");
})
.AddHttpMessageHandler<JwtHandler>();

await builder.Build().RunAsync();
