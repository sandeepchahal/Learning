using Lumel.Dto;
using Lumel.Service;

namespace Lumel.ApiService.Jobs;

public class CsvReaderBackgroundService:BackgroundService
{
    private readonly ILogger<CsvReaderBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _interval;
    private readonly string _csvPath;

    public CsvReaderBackgroundService(ILogger<CsvReaderBackgroundService> logger,
        IServiceProvider serviceProvider,
        IConfiguration configuration)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;

        _interval = TimeSpan.FromMinutes(configuration.GetValue<int>("CsvProcessor:IntervalMinutes"));
        _csvPath = configuration.GetValue<string>("CsvProcessor:CsvFilePath")!;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("CsvBackgroundService started at {dateTime} ", DateTime.Now);
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var csvService = scope.ServiceProvider.GetRequiredService<ICsvService>();
                try
                {
                    if (File.Exists(_csvPath))
                    {
                        var lines = await File.ReadAllLinesAsync(_csvPath, stoppingToken);
                       
                        var customerDtos = new List<CustomerDto>();
                        var productDtos = new List<ProductDto>();
                        var orderDtos = new List<OrderDto>();
                        
                        foreach (var line in lines.Skip(1)) 
                        {
                            var values = line.Split(',');
                            int.TryParse(values[0], out int orderId);
                            int.TryParse(values[1], out int productId);
                            int.TryParse(values[2], out int customerId);
                            
                            string pName = values[3];
                            string category = values[4];
                            string region = values[5];
                            
                            DateTime.TryParse(values[6], out DateTime dateOfSale);
                            int.TryParse(values[7], out int quantitySold);
                            decimal.TryParse(values[8], out decimal unitPrice);
                            decimal.TryParse(values[9], out decimal discount);
                            decimal.TryParse(values[10], out decimal shippingCost);

                            string paymentMethod = values[11];
                            string customerName = values[12];
                            string email = values[13];
                            string address = values[14];

                            
                            var customerDto = new CustomerDto
                            {
                                Id = customerId,
                                Email = email,
                                Address = address,
                                Name = customerName,
                                DateOfBirth = null,
                                Gender = null
                            };
                            var productDto = new ProductDto
                            {
                                Id = productId,
                                Category = category,
                                Name = pName,
                                Description = null
                            };
                            var orderDto = new OrderDto
                            {
                               OrderId = orderId,
                               ProductId = productId,
                               CustomerId = customerId,
                               Discount = discount,
                               Quantity = quantitySold,
                               Region = region,
                               PaymentMethod = paymentMethod,
                               ShippingCost = shippingCost,
                               UnitPrice = unitPrice,
                               DateOfSale = dateOfSale
                            };
                            customerDtos.Add(customerDto);
                            productDtos.Add(productDto);
                            orderDtos.Add(orderDto);
                        }

                        await csvService.AddOrUpdateAsync(
                            new CsvDto()
                            {
                                CustomerDto = customerDtos,
                                ProductDto = productDtos,
                                OrderDto = orderDtos
                            });
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing CSV.");
                }
            }
            await Task.Delay(_interval, stoppingToken);
        }
        _logger.LogInformation("CSV Background Service is stopped at {dateTime}", DateTime.Now);
    }
}