using InventoryOrderManagement.Core.Common;

namespace InventoryOrderManagement.Core;

public class Warehouse : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? SystemWarehouse { get; set; } = false;
}