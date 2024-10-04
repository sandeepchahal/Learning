using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

[ApiController]
[Route("api/product")]
public class ProductController(IMemoryCache memoryCache, ILogger<ProductController> logger) : ControllerBase
{
    public static List<Product> products =
    [
        new ()
        {
            Id =1,
            Name ="test",
            Description="test 1 description"
        },
        new ()
        {
            Id =2,
            Name ="test",
            Description="test 2 description"
        },new ()
        {
            Id =3,
            Name ="test",
            Description="test 3 description"
        },
    ];

    [HttpGet("get/{id}")]
    public IActionResult Get(int id)
    {
        if (memoryCache.TryGetValue(id, out Product? cachedResult))
        {
            logger.LogInformation("Getting from cache");
            return Ok(cachedResult);
        }
        var result = products.Find(col => col.Id == id);
        var cacheOptions = new MemoryCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromMinutes(2))
        .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
        logger.LogInformation("Saving into cache");

        memoryCache.Set(id, result, cacheOptions);
        return Ok(result);

    }
}



public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}