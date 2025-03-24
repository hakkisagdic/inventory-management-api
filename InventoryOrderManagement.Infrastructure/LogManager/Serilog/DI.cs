using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;

namespace InventoryOrderManagement.Infrastructure.LogManager.Serilog;

public static class DI
{
    public static IServiceCollection RegisterSerilog(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();
        
        services.AddLogging(logger =>
        {
            logger.ClearProviders();
            logger.AddConsole();
            logger.AddProvider(new SerilogLoggerProvider(Log.Logger));
        });

        return services;
    }
}