using CartService.Implementation;
namespace CartService;

public class ProductReservationWatchService(IServiceScopeFactory serviceScopeFactory, ILogger<ProductReservationWatchService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var itemsToRemove = new List<(int userId, int productId, int productDetailId)>();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceScopeFactory.CreateScope();
                var cartService = scope.ServiceProvider.GetRequiredService<ICartService>();
                var result = cartService.GetAllItems();
                logger.LogInformation($"Items count- {result.Count}");
                itemsToRemove.Clear();
                foreach (var dict in result)
                {
                    foreach (var item in dict.Value)
                    {
                        logger.LogInformation($"Current Time - {DateTime.Now}");
                        logger.LogInformation($"{item.ExpirationTime}");

                        if (DateTime.Now > item.ExpirationTime)
                        {
                            itemsToRemove.Add((dict.Key, item.ProductId, item.ProductDetailId));
                        }

                    }
                }
                logger.LogInformation($"Items To Delete count- {itemsToRemove.Count}");
                foreach (var (userId, productId, productDetailId) in itemsToRemove)
                {
                    var (removed, message) = await cartService.RemoveItem(userId, productId, productDetailId);

                    logger.LogInformation(
                        removed
                            ? $"Removed expired item: UserId={userId}, ProductId={productId}, ProductDetailId={productDetailId}"
                            : $"Failed to remove expired item: UserId={userId}, ProductId={productId}, ProductDetailId={productDetailId}. Reason: {message}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occurred while cleaning up expired cart items: {ex.Message}");
            }
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Run every 1 minute
        }
    }
}