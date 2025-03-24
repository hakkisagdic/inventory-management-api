using InventoryOrderManagement.Core.Common;

namespace InventoryOrderManagement.Infrastructure.SecurityManager.AspNetIdentity;

public class Token : BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpireDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public string CreatedByIp { get; set; } = string.Empty;
    public string CreatedById { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
    public DateTime? RevokedDate { get; set; }
    public string RevokedByIp { get; set; } = string.Empty;
    public string ReplacedByToken { get; set; } = string.Empty;
    public bool IsExpired => DateTime.UtcNow >= ExpireDate;
    public bool IsActive => RevokedDate == null && !IsExpired;
} 