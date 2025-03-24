using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore;
using InventoryOrderManagement.Infrastructure.LogManager.Serilog;
using InventoryOrderManagement.Infrastructure.SecurityManager;
using InventoryOrderManagement.Infrastructure.SeedManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryOrderManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.RegisterDataAccess(configuration);
        services.RegisterSerilog(configuration);
        services.RegisterSystemSeedManager(configuration);
        services.RegisterSecurityServices(configuration);
        
        return services;
    }
}