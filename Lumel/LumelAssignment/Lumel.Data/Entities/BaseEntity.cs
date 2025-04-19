using System.ComponentModel.DataAnnotations;

namespace Lumel.Data.Entities;

public abstract class BaseEntity
{
    [StringLength(100)]
    public required string CreatedBy { get; set; }
    public required DateTime CreatedOn { get; set; }
    [StringLength(100)]
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
}