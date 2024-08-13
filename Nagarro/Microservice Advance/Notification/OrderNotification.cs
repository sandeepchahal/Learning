namespace Notification;

public class OrderNotification
{
    public int OrderId { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderedItems> Products { get; set; }
}

public class OrderedItems
{
    public int ProductId { get; set; }
    public int ProductDetailId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}