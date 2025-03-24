using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InventoryOrderManagement.Infrastructure.SecurityManager.Tokens;

public static class DI
{
    public static IServiceCollection RegisterToken(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSectionName = "Jwt";
        services.Configure<TokenSettings>(configuration.GetSection(jwtSectionName));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var tokenSettings = configuration.GetSection(jwtSectionName).Get<TokenSettings>();

            if (tokenSettings?.Key.Length < 32)
            {
                throw new Exception("JWT key length must be at least 32 characters.");
            }

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = tokenSettings!.Issuer,
                ValidAudience = tokenSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key)),
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.HttpContext.Request.Cookies["access_token"];
                    if (!string.IsNullOrEmpty(accessToken)) context.Token = accessToken;
                    else
                    {
                        var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
                            context.Token = authorizationHeader.Substring("Bearer ".Length).Trim();
                    }
                    
                    return Task.CompletedTask;
                },
                
                OnChallenge = context =>
                {
                    if (context.AuthenticateFailure is SecurityTokenExpiredException)
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 498;
                        context.Response.ContentType = "application/json";
                        
                        var result = JsonSerializer.Serialize(
                            new
                            {
                                code = 498, 
                                message = "Token Expired",
                                error = new
                                {
                                    @ref = "https://datatracker.ietf.org/doc/html/rfc9110",
                                    exceptionType = "SecurityTokenExpiredException",
                                    innerException = "SecurityTokenExpiredException",
                                    source = "",
                                    stackTrace = ""
                                }
                            });
                        return context.Response.WriteAsync(result);
                    }
                    return Task.CompletedTask;
                }
            };
        });

        services.AddTransient<ITokenService, TokenService>();
        services.AddScoped<TokenSettings>();
        
        return services;
    }
}