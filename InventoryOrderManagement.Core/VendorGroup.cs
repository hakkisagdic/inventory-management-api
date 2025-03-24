using InventoryOrderManagement.Core.Common;

namespace InventoryOrderManagement.Core;

public class VendorGroup : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}