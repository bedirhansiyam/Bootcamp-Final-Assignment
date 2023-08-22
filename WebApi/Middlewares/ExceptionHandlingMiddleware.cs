using Serilog;
using System.Net;
using System.Text.Json;

namespace WebApi.Service;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        Log.Information("LogErrorHandlerMiddleware.Invoke");
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            Log.Fatal(
                         $"Path={context.Request.Path} || " +
                         $"Method={context.Request.Method} || " +
                         $"Exception={ex.Message}"
                         );

            await HandleExceptionAsync(context);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(JsonSerializer.Serialize("Internal server error"));
    }
}
