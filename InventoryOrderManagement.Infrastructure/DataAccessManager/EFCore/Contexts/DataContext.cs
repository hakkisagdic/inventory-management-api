using InventoryOrderManagement.Application.Common.Repositories;
using InventoryOrderManagement.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InventoryOrderManagement.Infrastructure.SecurityManager.AspNetIdentity;

namespace InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Contexts;

public class DataContext : IdentityDbContext<ApplicationUser>, IEntityDbSet
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<Company> Company { get; set; }
    public DbSet<FileImage> FileImage { get; set; }
    public DbSet<FileDocument> FileDocument { get; set; }
    public DbSet<CustomerGroup> CustomerGroup { get; set; }
    public DbSet<CustomerCategory> CustomerCategory { get; set; }
    public DbSet<VendorGroup> VendorGroup { get; set; }
    public DbSet<VendorCategory> VendorCategory { get; set; }
    public DbSet<Warehouse> Warehouse { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Vendor> Vendor { get; set; }
    public DbSet<UnitMeasure> UnitMeasure { get; set; }
    public DbSet<ProductGroup> ProductGroup { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<CustomerContact> CustomerContact { get; set; }
    public DbSet<VendorContact> VendorContact { get; set; }
    public DbSet<Tax> Tax { get; set; }
    public DbSet<SalesOrder> SalesOrder { get; set; }
    public DbSet<SalesOrderItem> SalesOrderItem { get; set; }
    public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
    public DbSet<PurchaseOrderItem> PurchaseOrderItem { get; set; }
    public DbSet<InventoryTransaction> InventoryTransaction { get; set; }
    public DbSet<DeliveryOrder> DeliveryOrder { get; set; }
    public DbSet<GoodsReceive> GoodsReceive { get; set; }
    public DbSet<SalesReturn> SalesReturn { get; set; }
    public DbSet<PurchaseReturn> PurchaseReturn { get; set; }
    public DbSet<Token> Token { get; set; }
}