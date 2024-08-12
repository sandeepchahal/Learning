namespace ProductDetailService;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("Authorization", out var header))
        {
            var token = header.ToString();
            Console.WriteLine($"Bearer Token: {token}");
        }
        await next(context);
    }
}
