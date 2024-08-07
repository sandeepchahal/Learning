namespace ProductService.Models;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public static class PredefinedProduct
{
    public static List<Product> Products =
    [
        new Product()
        {
            ProductId = 1,
            Name = "Product-1",
            Description = "This is product 1"
        },

        new Product()
        {
            ProductId = 2,
            Name = "Product-2",
            Description = "This is product 2"
        },

        new Product()
        {
            ProductId = 3,
            Name = "Product-3",
            Description = "This is product 3"
        }
    ];
}
// Product Details
public class ProductDetailDto
{
    public int ProductDetailId { get; set; }
    public int ProductId { get; set; }
    public string Size { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Design { get; set; } = string.Empty;
}