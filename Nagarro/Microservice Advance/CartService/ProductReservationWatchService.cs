using CartService.Implementation;
namespace CartService;

public class ProductReservationWatchService(IServiceScopeFactory serviceScopeFactory):BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceScopeFactory.CreateScope();
                var cartService = scope.ServiceProvider.GetRequiredService<ICartService>();

                var result = cartService.GetAllItems();
                foreach (var item in result)
                {
                    if (DateTime.Now <= item.ExpirationTime) continue;
                    var (removed, message) =
                        await cartService.RemoveItem(item.UserId, item.ProductId, item.ProductDetailId);
                    Console.WriteLine(
                        removed
                            ? $"Removed expired item: UserId={item.UserId}, ProductId={item.ProductId}, ProductDetailId={item.ProductDetailId}"
                            : $"Failed to remove expired item: UserId={item.UserId}, ProductId={item.ProductId}, ProductDetailId={item.ProductDetailId}. Reason: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while cleaning up expired cart items: {ex.Message}");
            }
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Run every 5 minutes
        }
    }
}