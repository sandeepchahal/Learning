using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Lumel.Data;

public class LumelDbContextFactory:IDesignTimeDbContextFactory<LumelDbContext>
{
    public LumelDbContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        var basePath = Path.Combine(Directory.GetCurrentDirectory());

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .Build();
        
        var builder = new DbContextOptionsBuilder<LumelDbContext>();
        var connectionString = configuration.GetConnectionString("AppDb");
        builder.UseSqlServer(connectionString);
        return new LumelDbContext(builder.Options);
    }
}