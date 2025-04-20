using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lumel.Data.Entities;

[Index(nameof(Category))]
[Index(nameof(Name))]
public class Product: BaseEntity
{
    [Key]
    public required string Id { get; set; }

    [StringLength(100)]
    public required string Name { get; set; }
    [StringLength(100)]
    public required string Category { get; set; } // we should be storing id as foreign key which reference the category table
    [StringLength(1000)]
    public string? Description { get; set; }
    public virtual List<Order>? Orders { get; set; }
}