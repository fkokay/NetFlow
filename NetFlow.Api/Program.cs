using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using NetFlow.Api.Auth;
using NetFlow.Api.Middlewares;
using NetFlow.Application;
using NetFlow.Application.Auth;
using NetFlow.Domain.Common;
using NetFlow.Domain.Identity;
using NetFlow.Infrastructure;
using NetFlow.Infrastructure.Auth;
using NetFlow.Netsis;
using NetFlow.ReadModel;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "NetFlow API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header. Örnek: Bearer {token}"
    });

    c.AddSecurityRequirement(doc =>
    {
        var scheme = new OpenApiSecuritySchemeReference("Bearer", doc);

        var req = new OpenApiSecurityRequirement();
        req.Add(scheme, new List<string>());

        return req;
    });
});
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration, builder.Environment.ContentRootPath)
    .AddReadModel(builder.Configuration)
    .AddNetsis();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<DomainExceptionMiddleware>();
builder.Services.AddHealthChecks();

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt")
);

builder.Services.AddScoped<ITokenService, JwtTokenService>();
builder.Services.AddScoped<CurrentUser>();
builder.Services.AddScoped<ICurrentUser>(sp => sp.GetRequiredService<CurrentUser>());
var jwt = builder.Configuration.GetSection("Jwt").Get<JwtOptions>()!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SigningKey))
        };
    }).AddCookie("Cookies");


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<DomainExceptionMiddleware>();
app.UseMiddleware<CurrentUserMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MakroFlow API");
        c.RoutePrefix = "swagger";
    });
}

app.MapHealthChecks("/health");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();