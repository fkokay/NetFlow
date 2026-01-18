using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using NetFlow.Blazor.Shared.Security;
using NetFlow.Blazor.Shared.Services;
using NetFlow.Blazor.Web.Components;
using NetFlow.Blazor.Web.Security;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpContextAccessor();


builder.Services.AddScoped<IPlatformContext, WebPlatformContext>();

// Cookie Auth
builder.Services.AddAuthorizationCore();
builder.Services
    .AddAuthentication("Blazor")
    .AddScheme<AuthenticationSchemeOptions, BlazorAuthHandler>("Blazor", _ => { });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<CustomAuthStateProvider>(provider =>
    (CustomAuthStateProvider)provider.GetRequiredService<AuthenticationStateProvider>()
);


builder.Services.AddScoped<ILoginService>(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    var http = factory.CreateClient("ApiClient");

    return new WebLoginService(
        http,
        sp.GetRequiredService<AuthenticationStateProvider>());
});

builder.Services.AddScoped<IApiClientFactory, ApiClientFactory>();

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7071/");
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
     .AddAdditionalAssemblies(typeof(NetFlow.Blazor.Shared._Imports).Assembly);

app.Run();
