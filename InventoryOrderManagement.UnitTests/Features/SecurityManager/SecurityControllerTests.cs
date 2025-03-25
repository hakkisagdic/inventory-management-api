using System.Net.Http.Json;
using InventoryOrderManagement.Infrastructure.SecurityManager.AspNetIdentity.Dtos;
using InventoryOrderManagement.Presentation.Common.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Xunit;

namespace InventoryOrderManagement.UnitTests.Features.SecurityManager;

public class SecurityControllerTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer;
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    
    public SecurityControllerTests()
    {
        _dbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("inventory_test_db")
            .WithUsername("test_user")
            .WithPassword("test_password")
            .Build();

        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Test container bağlantı dizesini kullan
                    services.Configure<string>("ConnectionStrings:DefaultConnection", 
                        _dbContainer.GetConnectionString());
                });
            });

        _client = _factory.CreateClient();
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
        _factory.Dispose();
        _client.Dispose();
    }

    [Fact]
    public async Task Register_WithValidData_ShouldSucceed()
    {
        // Arrange
        var registerRequest = new RegisterRequestDto
        {
            Email = "test@example.com",
            Password = "Test123!",
            ConfirmPassword = "Test123!",
            FirstName = "Test",
            LastName = "User",
            CompanyName = "Test Company"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/Security/Register", registerRequest);
        var result = await response.Content.ReadFromJsonAsync<ApiSuccessResult<RegisterResultDto>>();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(result);
        Assert.Equal(200, result.Code);
        Assert.Equal("Kullanıcı başarıyla kaydedildi", result.Message);
        Assert.NotNull(result.Content);
        Assert.Equal(registerRequest.Email, result.Content.Email);
    }

    [Fact]
    public async Task Login_WithValidCredentials_ShouldSucceed()
    {
        // Arrange
        await Register_WithValidData_ShouldSucceed();
        
        var loginRequest = new LoginRequestDto
        {
            Email = "test@example.com",
            Password = "Test123!"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/Security/Login", loginRequest);
        var result = await response.Content.ReadFromJsonAsync<ApiSuccessResult<LoginResultDto>>();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(result);
        Assert.Equal(200, result.Code);
        Assert.Equal("Giriş başarılı", result.Message);
        Assert.NotNull(result.Content);
        Assert.NotNull(result.Content.AccessToken);
        Assert.NotNull(result.Content.RefreshToken);
    }

    [Fact]
    public async Task Logout_WithValidToken_ShouldSucceed()
    {
        // Arrange
        await Login_WithValidCredentials_ShouldSucceed();

        // Act
        var response = await _client.PostAsync("/api/Security/Logout", null);
        var result = await response.Content.ReadFromJsonAsync<ApiSuccessResult<LogoutResultDto>>();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(result);
        Assert.Equal(200, result.Code);
        Assert.Equal("Çıkış başarılı", result.Message);
    }
}