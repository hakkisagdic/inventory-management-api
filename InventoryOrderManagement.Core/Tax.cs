using InventoryOrderManagement.Core.Common;

namespace InventoryOrderManagement.Core;

public class Tax : BaseEntity
{
    public string? Name { get; set; }
    public double? Percentage { get; set; }
    public string? Description { get; set; }
}