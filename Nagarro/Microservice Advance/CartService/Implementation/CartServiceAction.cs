using CartService.Models;

namespace CartService.Implementation;

public class CartServiceAction(IHttpClientFactory httpClientFactory):ICartService
{
    private readonly Dictionary<int, List<CartReservation>> _productReservation = new();
    private readonly HttpClient _client = httpClientFactory.CreateClient("ProductServiceClient");
    
    public async Task<(bool,string?)> AddToCart(int userId, ProductReservation productReservation)
    {
        try
        {
            var result = await TryReserveProductIfAvailable(productReservation.ProductId, productReservation.Quantity);
            if (!result.Item1)
                return result;

            var cart = new CartReservation()
            {
                ProductId = productReservation.ProductId,
                Quantity = productReservation.Quantity,
                UserId = userId,
                ProductDetailId = productReservation.ProductDetailId
            };
            if (!_productReservation.ContainsKey(productReservation.UserId))
            {
                _productReservation[productReservation.UserId] = [cart];

            }
            else
            {
                _productReservation[productReservation.UserId].Add(cart);
            }

            return (true, "Item(s) is added to the cart");
        }
        catch (Exception)
        {
            return (false, "An error has occurred while processing the request");
        }
    }

    public List<CartReservation> GetCartItems(int userId)
    {
        try
        {
            var items = _productReservation.GetValueOrDefault(userId);
            return items ?? [];
        }
        catch (Exception)
        {
            return [];
        }
    }

    public List<CartReservation> GetAllItems()
    {
        try
        {
            var result = new List<CartReservation>();

            foreach (var item in _productReservation)
            {
                result.AddRange(item.Value);
            }
            return result;
        }
        catch (Exception)
        {
            return [];
        }
    }
    public (bool,string?) RemoveItem(int userId,int productId, int productDetailId)
    {
        try
        {
            if (!_productReservation.TryGetValue(userId, out List<CartReservation>? value)) 
                return (false,$"No items in the cart for user id - {userId}");
            var result = value
                .Find(col => col.ProductId == productId 
                             && col.ProductDetailId == productDetailId);
            if (result == null) return (false,$"No items in the cart are available for product Id - {productId} ");
            value.Remove(result);
            return (true,$"item is removed from cart successfully");

        }
        catch (Exception)
        {
            return (false,$"An error has occurred while processing the request");
        }
    }

    public (bool,string?) AddQuantity(int userId,int productId, int productDetailId, int quantity)
    {
        try
        {
            if (!_productReservation.TryGetValue(userId, out List<CartReservation>? value)) 
                return (false,$"No items in the cart for user id - {userId}");
            var result = value
                .Find(col => col.ProductId == productId 
                             && col.ProductDetailId == productDetailId);
            if (result == null) return (false,$"No items in the cart are available for product Id - {productId} ");
            result.Quantity += quantity;
            return (true,$"item is updated successfully");
        }
        catch (Exception)
        {
            return (false,$"An error has occurred while processing the request");
        }
    }

    public (bool,string?) DeleteQuantity(int userId,int productId, int productDetailId, int quantity)
    {
        try
        {
            if (!_productReservation.TryGetValue(userId, out List<CartReservation>? value)) 
                return (false,$"No items in the cart for user id - {userId}");
            var result = value
                .Find(col => col.ProductId == productId 
                             && col.ProductDetailId == productDetailId);
            if (result == null) return (false,$"No items in the cart are available for product Id - {productId} ");
            result.Quantity -= quantity;
            return (true,$"item is updated successfully");
        }
        catch (Exception)
        {
            return (false,$"An error has occurred while processing the request");
        }
    }

    private async Task<(bool,string?)> TryReserveProductIfAvailable(int productId, int quantity)
    {
        try
        {
            var response = await _client.GetAsync($"reserve/{productId}/{quantity}");
            var responseResult = await response.Content.ReadAsStringAsync();
            return (response.IsSuccessStatusCode, responseResult);
        }
        catch (Exception)
        {
            return (false, string.Empty);
        }
    }
}