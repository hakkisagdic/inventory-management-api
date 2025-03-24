namespace InventoryOrderManagement.Core.Common;

public class BaseEntity : IHasIsDeleted
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime? CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public Guid? CreatedById { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
    public Guid? UpdatedById { get; set; }
    public bool IsDeleted { get; set; }
}