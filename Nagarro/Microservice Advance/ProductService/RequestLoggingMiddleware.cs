namespace ProductService;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        // Log request details
        logger.LogInformation("Request: {Method} {Url}", context.Request.Method, context.Request.Path);
        
        // Log headers (including token if present)
        foreach (var header in context.Request.Headers)
        {
            logger.LogInformation("Header: {Key} = {Value}", header.Key, header.Value);
        }
        await next(context);
    }
}
