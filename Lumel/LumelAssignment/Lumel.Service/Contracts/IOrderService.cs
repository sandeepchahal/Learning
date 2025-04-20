using Lumel.Data.Entities;
using Lumel.Dto;

namespace Lumel.Service;

public interface IOrderService
{
    Task AddAsync(List<OrderDto> dto);
    Task<Order?> GetByIdAsync(string orderId);
    Task<IEnumerable<Order>?> GetByProductIdAsync(string productId);
    Task<IEnumerable<Order>?> GetByCustomerIdAsync(string customerId);

    Task<Revenue> CalculateRevenue(DateTime startDate, DateTime endDate);
}