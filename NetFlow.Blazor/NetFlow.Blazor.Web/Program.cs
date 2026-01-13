using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using NetFlow.Blazor.Shared;
using NetFlow.Blazor.Web.Client;
using NetFlow.Blazor.Web.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddDevExpressBlazor();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(NetFlow.Blazor.Shared._Imports).Assembly,
        typeof(NetFlow.Blazor.Web.Client._Imports).Assembly);

app.Run();
