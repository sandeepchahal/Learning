namespace Lumel.Dto;

public class ProductDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Category { get; set; } 
    public string? Description { get; set; }
}