using InventoryOrderManagement.Core.Common;

namespace InventoryOrderManagement.Core;

public class CustomerContact : BaseEntity
{
    public string? Name { get; set; }
    public string? JobTitle { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Description { get; set; }
    public Customer? Customer { get; set; }
    public Guid? CustomerId { get; set; }
}