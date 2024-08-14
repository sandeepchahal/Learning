using CheckoutService.Models;

namespace CheckoutService.Coordinator;

public interface ICheckOutCoordinator
{
    Task<bool> ExecuteCheckOut(int userId,string authorizationToken,IEnumerable<CartItem> cartItems);
}