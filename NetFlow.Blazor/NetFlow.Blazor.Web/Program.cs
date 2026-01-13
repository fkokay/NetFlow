using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using NetFlow.Blazor.Shared.Auth;
using NetFlow.Blazor.Shared.Services;
using NetFlow.Blazor.Web.Auth;
using NetFlow.Blazor.Web.Client.Auth;
using NetFlow.Blazor.Web.Components;
using NetFlow.Blazor.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped<IAppAuthState, ServerAppAuthState>();

builder.Services.AddAuthentication("Dummy")
    .AddScheme<AuthenticationSchemeOptions, DummyAuthHandler>("Dummy", _ => { });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = null;   // 🔥 otomatik global authorize kapalı
});

builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddDevExpressBlazor();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7071/") });

builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AllowAnonymous()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(NetFlow.Blazor.Shared._Imports).Assembly,
        typeof(NetFlow.Blazor.Web.Client._Imports).Assembly);

app.Run();
