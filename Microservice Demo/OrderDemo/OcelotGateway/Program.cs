using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((context, config) =>
{
    config.AddJsonFile($"ocelot.{context.HostingEnvironment.EnvironmentName}.json", true, true);
});

builder.Services.AddOcelot().AddConsul();

var app = builder.Build();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Ocelot is up & running");
    });
});

app.UseOcelot().Wait();
app.Run();
