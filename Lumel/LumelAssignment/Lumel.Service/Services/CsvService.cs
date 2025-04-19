using Lumel.Dto;

namespace Lumel.Service;

public class CsvService(IProductService productService, ICustomerService customerService, IOrderService orderService)
    :ICsvService
{
    public async Task AddOrUpdateAsync(CsvDto csvDto)
    {
        await customerService.AddOrUpdateAsync(csvDto.CustomerDto);
        await productService.AddOrUpdateAsync(csvDto.ProductDto);
        await orderService.AddAsync(csvDto.OrderDto);
    }
}