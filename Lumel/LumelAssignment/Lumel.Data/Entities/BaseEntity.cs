namespace Lumel.Data.Entities;

public abstract class BaseEntity
{
    public required string CreatedBy { get; set; }
    public required DateTime CreatedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
}