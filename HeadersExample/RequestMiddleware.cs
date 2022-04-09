using Microsoft.Extensions.Primitives;

namespace HeadersExample;

public class RequestMiddleware
{
    private readonly RequestDelegate _next;

    public RequestMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.Any(x => x.Key == "Device-Id"))
        {
            Console.WriteLine($"Device id: {context.Request.Headers["Device-Id"]}");
        }
        else
        {
            Console.WriteLine("Adding device id to header");
            context.Request.Headers.Add("Device-Id", StringValues.Empty);
        }

        await _next(context);
    }
}

public static class RequestMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomRequestHeaders(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestMiddleware>();
    }
}