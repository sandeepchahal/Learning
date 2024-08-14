namespace APIGateway;

public class MyLogger
{
    private readonly RequestDelegate _next;
    private readonly ILogger<MyLogger> _logger;

    public MyLogger(RequestDelegate next, ILogger<MyLogger> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        foreach (var header in context.Request.Headers)
        {
            _logger.LogInformation("Header: {Key} = {Value}", header.Key, header.Value);
        }
        await _next(context);
    }  
}
