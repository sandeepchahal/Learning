using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lumel.Shared;
using Microsoft.EntityFrameworkCore;

namespace Lumel.Data.Entities;

[Index(nameof(Name))]
[Index(nameof(Email))]
public class Customer:BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Address { get; set; }
    public GenderType? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public virtual List<Order>? Orders { get; set; }
}