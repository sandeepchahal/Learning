using System.Configuration;
using AccountManagementService;
using Consul;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Account Management API", Version = "v1" });
});
builder.Services.AddSingleton<IHostedService, ConsulRegistrationService>();
var consulHost = builder.Configuration.GetValue<string>("ConsulConfiguration:Host");

if (string.IsNullOrEmpty(consulHost))
    throw new ConfigurationErrorsException($"Consul Configuration Host is not found in the configuration");

builder.Services.AddSingleton<IConsulClient>(_ => new ConsulClient(config =>
{
    config.Address = new Uri(consulHost);
}));

builder.Services.AddHttpClient("CustomerService", client =>
{
    client.BaseAddress = new Uri("http://localhost:5079/customer/"); 
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Account Management API v1"));
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();