using Lumel.Data;
using Lumel.Data.Entities;
using Lumel.Dto;
using Microsoft.EntityFrameworkCore;

namespace Lumel.Service;

public class OrderService(LumelDbContext dbContext):IOrderService
{
    public async Task AddAsync(List<OrderDto> dto)
    {
        var orderList = new List<Order>();
        var orders = dto.Select(async col =>
        {
            var order = await GetByIdAsync(col.OrderId);
            if (order == null)
            {
                return new Order()
                {
                    Id = col.OrderId,
                    ProductId = col.ProductId,
                    CustomerId = col.CustomerId,
                    Discount = col.Discount,
                    Quantity = col.Quantity,
                    Region = col.Region,
                    PaymentMethod = col.PaymentMethod,
                    ShippingCost = col.ShippingCost,
                    UnitPrice = col.UnitPrice,
                    DateOfSale = col.DateOfSale,
                    CreatedBy = "System",
                    CreatedOn = DateTime.Now,
                };
            }

            order.ProductId = col.ProductId;
            order.CustomerId = col.CustomerId;
            order.Discount = col.Discount;
            order.Quantity = col.Quantity;
            order.Region = col.Region;
            order.PaymentMethod = col.PaymentMethod;
            order.ShippingCost = col.ShippingCost;
            order.UnitPrice = col.UnitPrice;
            order.DateOfSale = col.DateOfSale;
            order.ModifiedBy = "System";
            order.ModifiedOn = DateTime.Now;
            return null;
        }).Where(c=>c!= null);

        var result = await Task.WhenAll(orders);
        orderList.AddRange(result.Where(c=>c!=null)!);
        if (orderList.Count != 0)
        {
            dbContext.Orders.AddRange(orderList);
        }
        await dbContext.SaveChangesAsync();
    }

    public async Task<Order?> GetByIdAsync(int orderId)
    {
        return await dbContext.Orders.FindAsync(orderId);
    }

    public async Task<IEnumerable<Order>?> GetByProductIdAsync(int productId)
    {
        return await dbContext.Orders.AsNoTracking()
            .Where(col => col.ProductId == productId).ToListAsync();
    }

    public async Task<IEnumerable<Order>?> GetByCustomerIdAsync(int customerId)
    {
        return await dbContext.Orders.AsNoTracking()
            .Where(col => col.CustomerId == customerId).ToListAsync();

    }
}