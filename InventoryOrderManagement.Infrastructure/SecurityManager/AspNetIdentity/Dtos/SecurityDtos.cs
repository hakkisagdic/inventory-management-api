using InventoryOrderManagement.Infrastructure.SecurityManager.NavigationMenu;

namespace InventoryOrderManagement.Infrastructure.SecurityManager.AspNetIdentity.Dtos;

public class LoginResultDto
{
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
    public DateTime TokenExpireDate { get; set; }
    public DateTime RefreshTokenExpireDate { get; set; }
    public bool Success { get; set; }
    public required string Message { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string CompanyName { get; set; }
    public required string AccessToken { get; set; }
    public List<MenuNavigationTreeNodeDto> MenuNavigation { get; set; } = new();
    public List<string> Roles { get; set; } = new();
    public string Avatar { get; set; } = string.Empty;
}

public class LogoutResultDto
{
    public bool Success { get; set; }
    public required string Message { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string CompanyName { get; set; }
    public List<string> UserClaims { get; set; } = new();
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}

public class RegisterResultDto
{
    public bool Success { get; set; }
    public required string Message { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string CompanyName { get; set; }
}

public class UpdateUserResultDto
{
    public bool Success { get; set; }
    public required string Message { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsDeleted { get; set; }
}

public class DeleteUserResultDto
{
    public bool Success { get; set; }
    public required string Message { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsDeleted { get; set; }
}

public class GetRoleListResultDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string NormalizedName { get; set; }
}

public class GetUserListResultDto
{
    public required string Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public required string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public bool LockoutEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public int AccessFailedCount { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? CreatedAt { get; set; }
}

public class CreateUserResultDto
{
    public bool Success { get; set; }
    public required string Message { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsDeleted { get; set; }
}

public class RefreshTokenResultDto
{
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
    public DateTime TokenExpireDate { get; set; }
    public DateTime RefreshTokenExpireDate { get; set; }
    public bool Success { get; set; }
    public required string Message { get; set; }
    public required string UserId { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string CompanyName { get; set; }
    public required string AccessToken { get; set; }
    public List<MenuNavigationTreeNodeDto> MenuNavigation { get; set; } = new();
    public List<string> Roles { get; set; } = new();
    public string Avatar { get; set; } = string.Empty;
}

public class GetMyProfileListResultDto
{
    public required string Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public required string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public bool LockoutEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public int AccessFailedCount { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string CompanyName { get; set; }
}