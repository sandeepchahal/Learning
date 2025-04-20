using CsvHelper.Configuration;
using Lumel.Dto;

namespace Lumel.Service;

public sealed class CsvRowMap : ClassMap<CsvRow>
{
    public CsvRowMap()
    {
        Map(m => m.OrderId).Name("Order ID");
        Map(m => m.ProductId).Name("Product ID");
        Map(m => m.CustomerId).Name("Customer ID");
        Map(m => m.ProductName).Name("Product Name");
        Map(m => m.Category).Name("Category");
        Map(m => m.Region).Name("Region");
        Map(m => m.DateOfSale).Name("Date of Sale");
        Map(m => m.QuantitySold).Name("Quantity Sold");
        Map(m => m.UnitPrice).Name("Unit Price");
        Map(m => m.Discount).Name("Discount");
        Map(m => m.ShippingCost).Name("Shipping Cost");
        Map(m => m.PaymentMethod).Name("Payment Method");
        Map(m => m.CustomerName).Name("Customer Name");
        Map(m => m.CustomerEmail).Name("Customer Email");
        Map(m => m.CustomerAddress).Name("Customer Address");
    }
}