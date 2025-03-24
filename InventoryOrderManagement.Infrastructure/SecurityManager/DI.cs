using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Contexts;
using InventoryOrderManagement.Infrastructure.SecurityManager.AspNetIdentity;
using InventoryOrderManagement.Infrastructure.SecurityManager.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryOrderManagement.Infrastructure.SecurityManager;

public static class DI
{
    public static IServiceCollection RegisterSecurityServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Identity settings
        services.Configure<IdentitySettings>(configuration.GetSection("Identity"));
        
        // Identity services
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<DataContext>()
        .AddDefaultTokenProviders();

        // JWT Authentication
        services.RegisterToken(configuration);

        // Register Security Service
        services.AddScoped<SecurityService>();

        return services;
    }
} 