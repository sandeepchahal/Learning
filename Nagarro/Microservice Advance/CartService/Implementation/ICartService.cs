using CartService.Models;

namespace CartService.Implementation;

public interface ICartService
{
    Task<(bool,string?)> AddToCart(int userId,ProductReservation productReservation);
    List<CartReservation> GetCartItems(int userId);
    List<CartReservation> GetAllItems();
   (bool,string?) RemoveItem(int userId,int productId, int productDetailId);
   (bool,string?) AddQuantity(int userId,int productId, int productDetailId, int quantity);
   (bool,string?) DeleteQuantity(int userId,int productId, int productDetailId, int quantity);

}