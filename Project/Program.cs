using DAL.Context;
using Project.ServicesRegister;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text.Json;
using Project.API;

var builder = WebApplication.CreateBuilder(args);
var CurrentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProjectContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API, ENV : " + CurrentEnvironment,
        Version = "v1"
    });
});

builder.Services.InjectServices();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Store.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(600);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
var _env = app.Environment.EnvironmentName;
if (_env == "Local" || _env == "Development")
{
    app.UseSwagger();
    app.UseSwaggerUI(q => q.SwaggerEndpoint("v1/swagger.json", _env));
}

app.UseStatusCodePages();

app.UseCors("AllowAll");
app.UseCors(x => x
     .SetIsOriginAllowed(origin => true)
     .AllowAnyMethod()
     .AllowAnyHeader()
     .AllowCredentials());

app.UseMiddleware<CustomExceptionHandlerMiddleware>();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllers();

app.Run();
