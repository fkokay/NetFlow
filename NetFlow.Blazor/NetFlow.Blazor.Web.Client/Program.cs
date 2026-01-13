using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NetFlow.Blazor.Shared.Auth;
using NetFlow.Blazor.Shared.Services;
using NetFlow.Blazor.Web.Client.Auth;
using NetFlow.Blazor.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddBlazoredLocalStorage();

// Token store
builder.Services.AddScoped<ITokenStore, WebTokenStore>();

// Auth provider
builder.Services.AddScoped<IAppAuthState, JwtAppAuthState>();

// HttpClient → API
builder.Services.AddScoped<JwtHandler>();


builder.Services.AddSingleton<IFormFactor, FormFactor>();

builder.Services.AddScoped(sp =>
{
    var tokenStore = sp.GetRequiredService<ITokenStore>();
    var handler = new JwtHandler(tokenStore);

    return new HttpClient(handler)
    {
        BaseAddress = new Uri("https://localhost:7071/")
    };
});
await builder.Build().RunAsync();
