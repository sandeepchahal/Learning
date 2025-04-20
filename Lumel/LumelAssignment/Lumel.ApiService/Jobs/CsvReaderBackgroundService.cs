using Lumel.Service;

namespace Lumel.ApiService.Jobs;

public class CsvReaderBackgroundService:BackgroundService
{
    private readonly ILogger<CsvReaderBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _interval;
    public CsvReaderBackgroundService(ILogger<CsvReaderBackgroundService> logger,
        IServiceProvider serviceProvider,
        IConfiguration configuration)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _interval = TimeSpan.FromMinutes(configuration.GetValue<int>("CsvProcessor:IntervalMinutes"));
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("CsvBackgroundService started at {dateTime} ", DateTime.Now);
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var csvService = scope.ServiceProvider.GetRequiredService<ICsvService>();
                await csvService.ProcessFile();
            }
            await Task.Delay(_interval, stoppingToken);
        }
    }
}