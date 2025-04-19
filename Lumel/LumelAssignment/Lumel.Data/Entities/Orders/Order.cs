using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace Lumel.Data.Entities;

[Index(nameof(Region))]

[Index(nameof(DateOfSale))]
public class Order:BaseEntity
{
    [Key]
    public required int Id { get; set; }

    [ForeignKey(nameof(Product))]
    public required int ProductId { get; set; }
    
    [ForeignKey(nameof(Customer))]
    public required int CustomerId { get; set; }
    [StringLength(100)]
    public required string Region { get; set; } // it should be regionId as Forgein key
    public required DateTime DateOfSale { get; set; }
    public required int Quantity { get; set; }
    [Precision(18,2)]
    public required decimal UnitPrice { get; set; }
    public required decimal Discount { get; set; }
    [Precision(18,2)]
    public required decimal ShippingCost { get; set; }
    public required string PaymentMethod { get; set; } 
    
    public virtual Product? Product { get; set; }
    public virtual Customer? Customer { get; set; }
}