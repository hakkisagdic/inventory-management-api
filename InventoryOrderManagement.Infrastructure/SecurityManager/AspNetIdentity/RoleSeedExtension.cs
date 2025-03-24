using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;

namespace InventoryOrderManagement.Infrastructure.SecurityManager.AspNetIdentity;

public static class RoleSeedExtension
{
    public static IHost SeedRoles(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        
        // Rolleri oluştur
        CreateRolesAsync(roleManager).Wait();
        
        return host;
    }
    
    public static WebApplication SeedRoles(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        
        // Rolleri oluştur
        CreateRolesAsync(roleManager).Wait();
        
        return app;
    }
    
    private static async Task CreateRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        // Tüm rolleri kontrol et ve yoksa oluştur
        foreach (var roleName in RoleHelper.AllRoles)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                // Rol mevcut değilse oluştur
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
} 