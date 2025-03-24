namespace InventoryOrderManagement.Core.Common;

public interface IHasIsDeleted
{
    bool IsDeleted { get; set; }
}