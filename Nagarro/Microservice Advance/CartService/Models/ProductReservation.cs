namespace CartService.Models;

public abstract class ProductReservation
{
    public int ProductId { get; set; }
    public int ProductDetailId { get; set; }
    public int Quantity {get;set;}

    public int UserId { get; set; }
}

public class CartReservation : ProductReservation
{
    public DateTime ExpirationTime { get;} = DateTime.Now.AddMinutes(15);
}