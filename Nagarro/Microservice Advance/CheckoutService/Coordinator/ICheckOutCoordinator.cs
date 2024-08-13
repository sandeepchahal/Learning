using CheckoutService.Models;

namespace CheckoutService.Coordinator;

public interface ICheckOutCoordinator
{
    Task<bool> ExecuteCheckOut(IEnumerable<CartItem> cartItems);
}