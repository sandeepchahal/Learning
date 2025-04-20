using System.Diagnostics;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Lumel.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Lumel.Service;

public class CsvService :ICsvService
{
    private readonly IProductService _productService;
    private readonly ICustomerService _customerService;
    private readonly IOrderService _orderService;
    private readonly ILogger<CsvService> _logger;
    private readonly string _csvPath;
    public CsvService(IProductService productService, ICustomerService customerService,
        IOrderService orderService, ILogger<CsvService> logger,
        IConfiguration configuration)
    {
        _productService = productService;
        _customerService = customerService;
        _orderService = orderService;
        _logger = logger;
        _csvPath = configuration.GetValue<string>("CsvProcessor:CsvFilePath")!;

    } 
    public async Task ProcessFile(bool isBackgroundExecutor = true)
    {
        try
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            if (File.Exists(_csvPath))
            {
                using var reader = new StreamReader(_csvPath);
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ",",
                    HeaderValidated = null,
                    MissingFieldFound = null
                });

                csv.Context
                    .RegisterClassMap<CsvRowMap>();
                var records = csv.GetRecords<CsvRow>().ToList();
                var customerDic = new Dictionary<string, CustomerDto>();
                var productDic = new Dictionary<string, ProductDto>();
                var orderDic = new Dictionary<string, OrderDto>();

                foreach (var row in records)
                {
                    var customerDto = new CustomerDto
                    {
                        Id = row.CustomerId,
                        Email = row.CustomerEmail,
                        Address = row.CustomerAddress,
                        Name = row.CustomerName,
                        DateOfBirth = null,
                        Gender = null
                    };
                    var productDto = new ProductDto
                    {
                        Id = row.ProductId,
                        Category = row.Category,
                        Name = row.ProductName,
                        Description = null
                    };
                    var orderDto = new OrderDto
                    {
                        OrderId = row.OrderId,
                        ProductId = row.ProductId,
                        CustomerId = row.CustomerId,
                        Discount = row.Discount,
                        Quantity = row.QuantitySold,
                        Region = row.Region,
                        PaymentMethod = row.PaymentMethod,
                        ShippingCost = row.ShippingCost,
                        UnitPrice = row.UnitPrice,
                        DateOfSale = row.DateOfSale
                    };
                    customerDic[row.CustomerId] = customerDto;
                    productDic[row.ProductId] = productDto;
                    orderDic[row.OrderId] = orderDto;
                }

                await AddOrUpdateAsync(
                    new CsvDto()
                    {
                        CustomerDto = customerDic.Values.ToList(),
                        ProductDto = productDic.Values.ToList(),
                        OrderDto = orderDic.Values.ToList()
                    }, isBackgroundExecutor);
            }
            _logger.LogInformation("CSV Background Service is stopped at {dateTime}", DateTime.Now);
            stopwatch.Stop();
            _logger.LogInformation($"Total time taken {stopwatch.ElapsedMilliseconds / 1000.0} seconds");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing CSV.");
        }
    }
    
    
    #region Private
    private async Task AddOrUpdateAsync(CsvDto csvDto,bool isBackgroundExecutor)
    {
        await _customerService.AddOrUpdateAsync(csvDto.CustomerDto,isBackgroundExecutor);
        await _productService.AddOrUpdateAsync(csvDto.ProductDto);
        await _orderService.AddAsync(csvDto.OrderDto);
    }
    #endregion
}