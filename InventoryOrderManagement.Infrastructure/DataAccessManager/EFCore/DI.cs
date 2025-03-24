using InventoryOrderManagement.Application.Common.CQS.Commands;
using InventoryOrderManagement.Application.Common.CQS.Queries;
using InventoryOrderManagement.Application.Common.Repositories;
using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Contexts;
using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore;

public static class DI
{
    public static IServiceCollection RegisterDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var databaseProvider = configuration["DatabaseProvider"];

        // Eğer databaseProvider belirtilmemişse, varsayılan olarak SQLite'ı kullan
        if (string.IsNullOrEmpty(databaseProvider))
        {
            databaseProvider = "Sqlite";
        }

        switch (databaseProvider)
        {
            case "Sqlite":
                services.AddDbContext<DataContext>(options =>
                    options.UseSqlite(connectionString)
                        .LogTo(Log.Information, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                );
                services.AddDbContext<CommandContext>(options =>
                    options.UseSqlite(connectionString)
                        .LogTo(Log.Information, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                );
                services.AddDbContext<Contexts.QueryContext>(options =>
                    options.UseSqlite(connectionString)
                        .LogTo(Log.Information, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                );
                break;
            /*
            case "InMemory":
            case "MySql":
            case "PostgreSql":
            */
            case "SqlServer":
                services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(connectionString)
                        .LogTo(Log.Information, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                );
                services.AddDbContext<CommandContext>(options =>
                    options.UseSqlServer(connectionString)
                        .LogTo(Log.Information, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                );
                services.AddDbContext<Contexts.QueryContext>(options =>
                    options.UseSqlServer(connectionString)
                        .LogTo(Log.Information, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                );
                break;
            default:
                services.AddDbContext<DataContext>(options =>
                    options.UseSqlite(connectionString)
                        .LogTo(Log.Information, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                );
                services.AddDbContext<CommandContext>(options =>
                    options.UseSqlite(connectionString)
                        .LogTo(Log.Information, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                );
                services.AddDbContext<Contexts.QueryContext>(options =>
                    options.UseSqlite(connectionString)
                        .LogTo(Log.Information, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                );
                break;
        }
        
        services.AddScoped<ICommandContext, CommandContext>();
        services.AddScoped<IQueryContext, Contexts.QueryContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
        services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
        
        return services;
    }
    
    public static IHost CreateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var dataContext = serviceProvider.GetRequiredService<DataContext>();
        dataContext.Database.EnsureCreated();

        return host;
    }
}