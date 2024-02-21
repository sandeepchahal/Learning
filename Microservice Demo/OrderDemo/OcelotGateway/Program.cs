using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOcelot(builder.Configuration).AddConsul();

var app = builder.Build();
app.UseOcelot().Wait();
app.Run();
