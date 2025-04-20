using Lumel.Shared;

namespace Lumel.Dto;

public class CustomerDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Address { get; set; }
    public GenderType? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }   
}