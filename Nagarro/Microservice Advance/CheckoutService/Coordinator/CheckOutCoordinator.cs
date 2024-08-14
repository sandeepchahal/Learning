using CheckoutService.Enums;
using CheckoutService.Models;
using CheckoutService.ServiceImplementations;

namespace CheckoutService.Coordinator;

public class CheckOutCoordinator(IHttpClientFactory httpClientFactory,IMessageService messageService):ICheckOutCoordinator
{
    private static int _order = 0;
    private readonly HttpClient _productDetailClient = httpClientFactory.CreateClient("ProductDetailServiceClient");
    private readonly HttpClient _cartClient = httpClientFactory.CreateClient("CartServiceClient");
    
    public async Task<bool> ExecuteCheckOut(int userId, IEnumerable<CartItem> cartItems)
    {
        // step -1 deducted the quantity from product
        // step -2 mock payment and send notification 
        // step -3 delete items from cart
        // step -4 send notification order created or failed
        try
        {
            foreach (var item in cartItems)
            {
                if (await ReserveProduct(item)) continue;
                await CompensateQuantity(item);
                return false;
            }
            SendNotification(cartItems, OrderStatus.Created, userId);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #region  Product Service
    private async Task<bool> ReserveProduct(CartItem item)
    {
        var productInfo = new ProductQuantityContext() { ProductDetailId = item.ProductDetailId, Quantity = -item.Quantity };
        var response = await _productDetailClient.PostAsJsonAsync("reserve", productInfo);
        if (!response.IsSuccessStatusCode) return false;
        if (await ProcessPayment(item)) return true;
        await CompensateQuantity(item);
        return false;

    }
    private async Task<bool> CompensateQuantity(CartItem item)
    {
        var productInfo = new ProductQuantityContext() { ProductDetailId = item.ProductDetailId,
            Quantity = item.Quantity };
        var response = await _productDetailClient.PutAsJsonAsync("update-quantity", productInfo);
        return response.IsSuccessStatusCode; 
    }
    #endregion

    #region Payment
    
    private async Task<bool> ProcessPayment(CartItem item)
    {
        await Task.Delay(10000);
        if (await RemoveFromCart(item)) return true;
        await CompensatePayment(item);
        await CompensateQuantity(item);
        return false;
    }
    private async Task<bool> CompensatePayment(CartItem item)
    {
        await Task.Delay(10000);
        
        return true;
    }
    #endregion

    #region Cart
    private async Task<bool> RemoveFromCart(CartItem item)
    {
        var removeCartUrl = $"remove/{item.ProductId}/{item.ProductDetailId}";
        var response = await _cartClient.DeleteAsync(removeCartUrl);
        return response.IsSuccessStatusCode;

    }
    private async Task<bool> CompensateCart(CartItem cartItem)
    {
        var cartInfo = new CartItem() { ProductDetailId = cartItem.ProductDetailId, ProductId = cartItem.ProductId, Quantity = cartItem.Quantity};
        var response = await _cartClient.PostAsJsonAsync("add", cartInfo);
        return response.IsSuccessStatusCode; 
    }

    #endregion
    
    #region  notificaton

    private void SendNotification(IEnumerable<CartItem> item, OrderStatus orderStatus, int userId)
    {
        try
        {
            var orderNotification = new OrderNotification()
            {
                UserId = userId,
                OrderId = _order+1,
                Status = orderStatus,
                Products = item.ToList()
            };
            messageService.PublishMessage(OrderStatus.Created,orderNotification);
        }
        catch (Exception)
        {
            // log the error
        }
    }
    #endregion
}