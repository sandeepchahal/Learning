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
        foreach (var orderItem in dto)
        {
            var order = await GetByIdAsync(orderItem.OrderId);
            if (order == null)
            {
                orderList.Add(
                    new Order()
                    {
                        Id = orderItem.OrderId,
                        ProductId = orderItem.ProductId,
                        CustomerId = orderItem.CustomerId,
                        Discount = Math.Round(orderItem.Discount, 2, MidpointRounding.AwayFromZero),
                        Quantity = orderItem.Quantity,
                        Region = orderItem.Region,
                        PaymentMethod = orderItem.PaymentMethod,
                        ShippingCost = Math.Round(orderItem.ShippingCost, 2, MidpointRounding.AwayFromZero),
                        UnitPrice = Math.Round(orderItem.UnitPrice, 2, MidpointRounding.AwayFromZero),
                        DateOfSale = orderItem.DateOfSale,
                        CreatedBy = "System",
                        CreatedOn = DateTime.Now,
                    });
            }
            else
            {
                order.ProductId = orderItem.ProductId;
                order.CustomerId = orderItem.CustomerId;
                order.Discount = Math.Round(orderItem.Discount, 2, MidpointRounding.AwayFromZero);
                order.Quantity = orderItem.Quantity;
                order.Region = orderItem.Region;
                order.PaymentMethod = orderItem.PaymentMethod;
                order.ShippingCost = Math.Round(orderItem.ShippingCost, 2, MidpointRounding.AwayFromZero);
                order.UnitPrice = Math.Round(orderItem.UnitPrice, 2, MidpointRounding.AwayFromZero);
                order.DateOfSale = orderItem.DateOfSale;
                order.ModifiedBy = "System";
                order.ModifiedOn = DateTime.Now;
            }
        }
        if (orderList.Count != 0)
        {
            dbContext.Orders.AddRange(orderList);
        }
        await dbContext.SaveChangesAsync();
    }

    public async Task<Order?> GetByIdAsync(string orderId)
    {
        return await dbContext.Orders.FindAsync(orderId);
    }

    public async Task<IEnumerable<Order>?> GetByProductIdAsync(string productId)
    {
        return await dbContext.Orders.AsNoTracking()
            .Where(orderItem => orderItem.ProductId == productId).ToListAsync();
    }

    public async Task<IEnumerable<Order>?> GetByCustomerIdAsync(string customerId)
    {
        return await dbContext.Orders.AsNoTracking()
            .Where(orderItem => orderItem.CustomerId == customerId).ToListAsync();

    }
}