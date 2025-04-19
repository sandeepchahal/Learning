using Lumel.Data.Entities;
using Lumel.Dto;

namespace Lumel.Service;

public interface IOrderService
{
    Task AddAsync(List<OrderDto> dto);
    Task<Order?> GetByIdAsync(int orderId);
    Task<IEnumerable<Order>?> GetByProductIdAsync(int productId);
    Task<IEnumerable<Order>?> GetByCustomerIdAsync(int customerId);
}