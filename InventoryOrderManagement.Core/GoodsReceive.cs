using InventoryOrderManagement.Core.Common;

namespace InventoryOrderManagement.Core;

public class GoodsReceive : BaseEntity
{
    public string? Number { get; set; }
    public DateTime? ReveiveDate { get; set; }
    public GoodsReceiveStatus? Status { get; set; }
    public string? Description { get; set; }
    public string? PurchaseOrderId { get; set; }
    public PurchaseOrder? PurchaseOrder { get; set; }
}