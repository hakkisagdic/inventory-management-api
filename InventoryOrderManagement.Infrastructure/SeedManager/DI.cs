using Infrastructure.SeedManager.Systems;
using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Contexts;
using InventoryOrderManagement.Infrastructure.SeedManager.Systems;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InventoryOrderManagement.Infrastructure.SeedManager;

public static class DI
{
    public static IServiceCollection RegisterSystemSeedManager(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<CompanySeeder>();
        services.AddScoped<SystemWarehouseSeeder>();
        
        return services;
    }

    public static IHost SeedSystemData(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        
        var context = serviceProvider.GetRequiredService<DataContext>();
        
        // Warehouse kayıtları olup olmadığına bakmaksızın verileri seed et
        var systemwarehouseSeeder = serviceProvider.GetRequiredService<SystemWarehouseSeeder>();
        systemwarehouseSeeder.GenerateDataAsync().Wait();
        
        var companyseeder = serviceProvider.GetRequiredService<CompanySeeder>();
        companyseeder.GenerateDataAsync().Wait();
        
        return host;
    }
}