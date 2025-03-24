using InventoryOrderManagement.Core.Common;

namespace InventoryOrderManagement.Core;

public class UnitMeasure : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}