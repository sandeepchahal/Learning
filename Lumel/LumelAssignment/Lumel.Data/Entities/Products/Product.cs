using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lumel.Data.Entities;

[Index(nameof(Category))]
[Index(nameof(Name))]
public class Product: BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required string Name { get; set; }
    public required string Category { get; set; } // we should be storing id as foreign key which reference the category table
    public string? Description { get; set; }
    public virtual List<Order>? Orders { get; set; }
}