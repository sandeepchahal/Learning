using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var ocelotConfigFileName = environment == "Production" ? "ocelot.json" : $"ocelot.{environment}.json";
builder.Configuration.AddJsonFile(ocelotConfigFileName, reloadOnChange:true, optional:false);
builder.Services.AddOcelot(builder.Configuration).AddConsul();
var app = builder.Build();
app.UseOcelot().Wait();
app.Run();