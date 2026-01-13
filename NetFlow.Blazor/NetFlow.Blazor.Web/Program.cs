using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using NetFlow.Blazor.Shared.Security;
using NetFlow.Blazor.Web.Components;
using NetFlow.Blazor.Web.Security;

var builder = WebApplication.CreateBuilder(args);

// =============================
// Razor Components (InteractiveServer)
// =============================
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// =============================
// Authorization for Blazor
// =============================
builder.Services.AddAuthorizationCore();

// =============================
// LocalStorage (Browser JWT storage)
// =============================
builder.Services.AddBlazoredLocalStorage();

// =============================
// JWT Auth Services
// =============================
builder.Services.AddScoped<ITokenStore, WebTokenStore>();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();

// =============================
// HttpClient → API with JWT
// =============================
builder.Services.AddTransient<JwtHandler>();

builder.Services.AddHttpClient("api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7071/");
})
.AddHttpMessageHandler<JwtHandler>();

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("api"));

// =============================
// App
// =============================
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
