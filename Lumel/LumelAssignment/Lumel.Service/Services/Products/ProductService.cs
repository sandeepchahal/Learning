using Lumel.Data;
using Lumel.Data.Entities;
using Lumel.Dto;
using Microsoft.EntityFrameworkCore;

namespace Lumel.Service;

public class ProductService(LumelDbContext dbContext):IProductService
{
    public async Task AddOrUpdateAsync(List<ProductDto> dto)
    {
        var products = new List<Product>();
        var tasks = dto.Select(async col =>
        {
            var product = await GetByIdAsync(col.Id);
            if (product == null)
            {
                return new Product()
                {
                    Id = col.Id,
                    Name = col.Name,
                    Category = col.Category,
                    Description = col.Description,
                    CreatedBy = "System",
                    CreatedOn = DateTime.Now
                };
            }
            product.Description = col.Description;
            product.ModifiedBy = "System";
            product.ModifiedOn = DateTime.Now;
            return null;
        }).Where(col=>col!= null).ToList();
        
        var result =  await Task.WhenAll(tasks);
        products.AddRange(result.Where(col=>col!= null)!);
        if (products.Count != 0)
        {
            dbContext.Products.AddRange(products);
        }
        await dbContext.SaveChangesAsync();
    }

    public async Task<Product?> GetByIdAsync(int productId)
    {
        return await dbContext.Products.FindAsync(productId);
    }
}