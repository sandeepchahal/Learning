using Lumel.Data;
using Lumel.Data.Entities;
using Lumel.Dto;

namespace Lumel.Service;

public class ProductService(LumelDbContext dbContext):IProductService
{
    public async Task AddOrUpdateAsync(List<ProductDto> dto)
    {
        var products = new List<Product>();
        foreach(var productItem in dto)
        {
            var product = await GetByIdAsync(productItem.Id);
            if (product == null)
            {
                products.Add( new Product()
                {
                    Id = productItem.Id,
                    Name = productItem.Name,
                    Category = productItem.Category,
                    Description = productItem.Description,
                    CreatedBy = "System",
                    CreatedOn = DateTime.Now
                });
            }
            else
            {
                product.Description = productItem.Description;
                product.ModifiedBy = "System";
                product.ModifiedOn = DateTime.Now;
            }
        }
        if (products.Count != 0)
        {
            dbContext.Products.AddRange(products);
        }
        await dbContext.SaveChangesAsync();
    }

    public async Task<Product?> GetByIdAsync(string productId)
    {
        return await dbContext.Products.FindAsync(productId);
    }
}