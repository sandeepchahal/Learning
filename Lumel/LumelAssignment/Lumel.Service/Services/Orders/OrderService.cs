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

    public async Task<Revenue> CalculateRevenue(DateTime startDate, DateTime endDate)
    {
        var orders = await dbContext.Orders.AsNoTracking()
            .Where(col => col.DateOfSale >= startDate && col.DateOfSale <= endDate).ToListAsync();
        
        decimal totalRevenue = orders.Sum(col => col.Quantity * col.UnitPrice * (1 - col.Discount));
        
        var revenueByProduct = orders.OrderBy(col=>col.ProductId)
            .GroupBy(col=>col.ProductId)
            .Select(col => new RevenueDTO()
            {
                Name = col.Key,
                Revenue = col.Sum(o => o.Quantity * o.UnitPrice * (1 - o.Discount))
            }).ToList();

        var revenueByRegion = orders.OrderBy(col => col.Region)
            .GroupBy(col => col.Region)
            .Select(col => new RevenueDTO()
            {
                Name = col.Key,
                Revenue = col.Sum(o => o.Quantity * o.UnitPrice * (1 - o.Discount))
            }).ToList();


        var groupByCategory =
            (from o in dbContext.Orders
                join p in dbContext.Products on o.ProductId equals p.Id
                where o.DateOfSale>=startDate && o.DateOfSale<=endDate
                group new { o, p } by p.Category
                into g
                orderby g.Key
                select new RevenueDTO()
                {
                    Name = g.Key,
                    Revenue = g.Sum(s=>s.o.Quantity * s.o.UnitPrice * (1 - s.o.Discount))
                }).ToList();
        
        return new Revenue()
        {
            TotalRevenue = totalRevenue,
            TotalRevenueByCategory = groupByCategory,
            TotalRevenueByProduct = revenueByProduct,
            TotalRevenueByRegion = revenueByRegion
        };
    }
}