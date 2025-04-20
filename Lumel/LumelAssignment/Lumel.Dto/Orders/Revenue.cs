namespace Lumel.Dto;

public class Revenue
{
    public decimal TotalRevenue { get; set; }
    public List<RevenueDTO> TotalRevenueByProduct { get; set; }
    public List<RevenueDTO> TotalRevenueByCategory{ get; set; }
    public List<RevenueDTO> TotalRevenueByRegion { get; set; }
    
}


public class RevenueDTO
{
    public string Name { get; set; }
    public decimal Revenue { get; set; }
}