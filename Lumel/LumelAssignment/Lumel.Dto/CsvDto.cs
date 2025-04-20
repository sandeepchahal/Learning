namespace Lumel.Dto;

public class CsvDto
{
    public required List<CustomerDto> CustomerDto { get; set; }
    public required List<ProductDto> ProductDto { get; set; }
    public required List<OrderDto> OrderDto { get; set; }
}