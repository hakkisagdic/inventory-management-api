using InventoryOrderManagement.Core.Common;

namespace InventoryOrderManagement.Core;

public class Company : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Currency { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
}