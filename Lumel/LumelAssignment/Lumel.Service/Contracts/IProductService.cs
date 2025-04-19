using Lumel.Data.Entities;
using Lumel.Dto;

namespace Lumel.Service;

public interface IProductService
{
    Task AddOrUpdateAsync(List<ProductDto> dto);
    Task<Product?> GetByIdAsync(int productId);
}