using System.Configuration;
using CartService;
using CartService.Implementation;
using JWTConfiguration;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICartService, CartServiceAction>();

builder.Services.AddHttpClient("ProductServiceClient", (serviceProvider, client) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var baseUrl = configuration["Services:Product:BaseUrl"];
    if (baseUrl is null)
        throw new ConfigurationErrorsException("Base Url is not found");
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = JwtConfigurationProvider.GetTokenValidationParameters();
    });
builder.Services.AddHostedService<ProductReservationWatchService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.Run();