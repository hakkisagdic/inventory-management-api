using InventoryOrderManagement.Core.Common;

namespace InventoryOrderManagement.Core;

public class SalesReturn : BaseEntity
{
    public string? Number { get; set; }
    public DateTime? ReturnDate { get; set; }
    public SalesReturnStatus? Status { get; set; }
    public string? Description { get; set; }
    public string? DeliveryOrderId { get; set; }
    public DeliveryOrder? DeliveryOrder { get; set; }
}