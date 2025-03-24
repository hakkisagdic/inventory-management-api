using InventoryOrderManagement.Infrastructure.SecurityManager.AspNetIdentity;
using InventoryOrderManagement.Infrastructure.SecurityManager.AspNetIdentity.Dtos;
using InventoryOrderManagement.Presentation.Common.Base;
using InventoryOrderManagement.Presentation.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryOrderManagement.Presentation.Controllers;

[Route("api/[controller]")]
public class SecurityController : BaseApiController
{
    private readonly SecurityService _securityService;

    public SecurityController(ISender sender, SecurityService securityService) : base(sender)
    {
        _securityService = securityService;
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiSuccessResult<RegisterResultDto>>> RegisterAsync(
        [FromBody] RegisterRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _securityService.RegisterAsync(
            request.Email,
            request.Password,
            request.ConfirmPassword,
            request.FirstName,
            request.LastName,
            request.CompanyName,
            cancellationToken);

        return Ok(new ApiSuccessResult<RegisterResultDto>
        {
            Code = StatusCodes.Status200OK,
            Message = "Kullanıcı başarıyla kaydedildi",
            Content = result
        });
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiSuccessResult<LoginResultDto>>> LoginAsync(
        [FromBody] LoginRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await _securityService.LoginAsync(
            request.Email,
            request.Password,
            cancellationToken);

        // Token'ı cookie'ye kaydet
        Response.Cookies.Append("access_token", result.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddMinutes(60) // Expire time aynı JWT ayarlarıyla eşleşmeli
        });

        return Ok(new ApiSuccessResult<LoginResultDto>
        {
            Code = StatusCodes.Status200OK,
            Message = "Giriş başarılı",
            Content = result
        });
    }

    [HttpPost("Logout")]
    [Authorize]
    public async Task<ActionResult<ApiSuccessResult<LogoutResultDto>>> LogoutAsync(
        CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(new ApiErrorResult
            {
                Code = StatusCodes.Status400BadRequest,
                Message = "Kullanıcı kimliği bulunamadı",
            });
        }
        
        var result = await _securityService.LogoutAsync(userId, cancellationToken);
        
        // Cookie temizle
        Response.Cookies.Delete("access_token");

        return Ok(new ApiSuccessResult<LogoutResultDto>
        {
            Code = StatusCodes.Status200OK,
            Message = "Çıkış başarılı",
            Content = result
        });
    }
}

public class RegisterRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
}

public class LoginRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
} 