namespace ProductDetailService.Models;

public class ProductDetail
{
    public int ProductDetailId { get; set; }
    public int ProductId { get; set; }
    public string Size { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Design { get; set; } = string.Empty;
}

public static class PredefinedProductDetail
{
    public static IEnumerable<ProductDetail> GetProductDetails => new[]
    {
        new ProductDetail()
        {
            
        }
    };
}