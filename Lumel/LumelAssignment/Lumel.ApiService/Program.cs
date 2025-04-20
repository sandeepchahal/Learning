using Lumel.ApiService;
using Lumel.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LumelDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LumelDb")));

builder.Services.RegisterDbServices();
builder.Services.RegisterBackgroundService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers(); 
app.Run();