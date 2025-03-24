namespace InventoryOrderManagement.Infrastructure.SecurityManager.AspNetIdentity;

public class TokenDto
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public string CreatedById { get; set; } = string.Empty;
} 