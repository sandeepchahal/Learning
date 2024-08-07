using Microsoft.OpenApi.Models;
using UserService.ServiceRegistrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Authentication Service", Version = "v1" });
});

builder.Services.RegisterServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication API v1"));
}

app.UseHttpsRedirection();
app.MapControllers();
app.Urls.Add("http://0.0.0.0:5004");
app.Run();