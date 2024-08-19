using APIGateway;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var ocelotConfigFileName = environment == "Production" ? "ocelot.json" : $"ocelot.{environment}.json";
builder.Configuration.AddJsonFile(ocelotConfigFileName, reloadOnChange: true, optional: false);
builder.Services.AddOcelot(builder.Configuration).AddConsul().AddPolly();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Authorization"));
});

var app = builder.Build();
app.UseOcelot().Wait();
app.UseMiddleware<TraceIdMiddleware>();
app.Run();