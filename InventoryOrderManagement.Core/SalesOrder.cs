using InventoryOrderManagement.Core.Common;

namespace InventoryOrderManagement.Core;

public class SalesOrder : BaseEntity
{
    public string? Number { get; set; }
    public DateTime? OrderDate { get; set; }
    public DeliveryOrderStatus OrderStatus { get; set; }
    public string? Description { get; set; }
    public string? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public string TaxId { get; set; }
    public Tax? Tax { get; set; }
    public double? BeforeTaxAmount { get; set; }
    public double? TaxAmount { get; set; }
    public double? AfterTaxAmount { get; set; }
    public ICollection<SalesOrderItem> SalesOrderItemList { get; set; } = new List<SalesOrderItem>();
}