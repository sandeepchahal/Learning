using Lumel.ApiService.Jobs;
using Lumel.Service;

namespace Lumel.ApiService;

public static class DbServiceConfiguration
{
    public static void RegisterDbServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICustomerService, CustomerService>();
        serviceCollection.AddScoped<IProductService, ProductService>();
        serviceCollection.AddScoped<IOrderService, OrderService>();
        serviceCollection.AddScoped<ICsvService, CsvService>();
    }

    public static void RegisterBackgroundService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHostedService<CsvReaderBackgroundService>();
    }
}