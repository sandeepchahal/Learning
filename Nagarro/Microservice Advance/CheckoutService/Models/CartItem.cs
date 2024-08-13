namespace CheckoutService.Models;

public class CartItem
{
    public int ProductId { get; set; }
    public int ProductDetailId { get; set; }
    public int Quantity { get; set; }
}

public class CheckOutRequestItem
{
    public List<CartItem> CartItems { get; set; } = [];
}
