namespace Lumel.Dto;

public class OrderDto
{
    public required int OrderId { get; set; }
    public required int ProductId { get; set; }
    public required int CustomerId { get; set; }
    public required string Region { get; set; } 
    public required DateTime DateOfSale { get; set; }
    public required int Quantity { get; set; }
    public required decimal UnitPrice { get; set; }
    public required decimal Discount { get; set; }
    public required decimal ShippingCost { get; set; }
    public required string PaymentMethod { get; set; } 
}