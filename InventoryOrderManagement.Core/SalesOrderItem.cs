using InventoryOrderManagement.Core.Common;

namespace InventoryOrderManagement.Core;

public class SalesOrderItem : BaseEntity
{
    public string? SalesOrderId { get; set; }
    public SalesOrder? SalesOrder { get; set; }
    public string? ProductId { get; set; }
    public Product? Product { get; set; }
    public string? Summary { get; set; }
    public double? UnitPrice { get; set; }
    public double? Quantity { get; set; }
    public double? Total { get; set; }
}