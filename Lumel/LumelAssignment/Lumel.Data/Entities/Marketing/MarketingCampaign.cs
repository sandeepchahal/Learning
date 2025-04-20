using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumel.Data.Entities;

public class MarketingCampaign:BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [StringLength(100)]
    public required string Name { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime  EndDate { get; set; }
}