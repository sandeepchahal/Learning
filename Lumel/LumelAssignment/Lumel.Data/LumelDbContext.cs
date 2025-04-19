using Lumel.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lumel.Data;

public class LumelDbContext:DbContext
{
    public LumelDbContext(DbContextOptions<LumelDbContext> options):base(options)
    {
        
    }
    
    #region DbSets

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<MarketingCampaign> MarketingCampaigns { get; set; }
    #endregion
    
}