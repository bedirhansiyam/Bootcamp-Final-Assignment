using Serilog;
using Serilog.Enrichers.Sensitive;
using Serilog.Filters;
using WebApi.Schema;

namespace WebApi.Service.Extensions;

public static class LoggerExtension
{
    public static void AddLoggerExtension(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        Log.Information("Application is starting...");
    }
}
