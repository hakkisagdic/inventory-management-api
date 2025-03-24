using InventoryOrderManagement.Core.Common;

namespace InventoryOrderManagement.Core;

public class ProductGroup : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}