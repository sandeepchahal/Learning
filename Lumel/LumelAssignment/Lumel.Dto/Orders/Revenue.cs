namespace Lumel.Dto;

public class Revenue
{
    public decimal TotalRevenue { get; set; }
    public Dictionary<string,decimal> TotalRevenueByProduct { get; set; }
    public Dictionary<string,decimal> TotalRevenueByCategory{ get; set; }
    public Dictionary<string,decimal> TotalRevenueByRegion { get; set; }
    
}