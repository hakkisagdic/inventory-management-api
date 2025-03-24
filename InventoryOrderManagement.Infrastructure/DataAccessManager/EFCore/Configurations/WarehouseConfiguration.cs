using InventoryOrderManagement.Core;
using InventoryOrderManagement.Core.Common;
using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Configurations;

public class WarehouseConfiguration : BaseEntityConfiguration<Warehouse>
{
    public override void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.Description).HasMaxLength(Constants.DescriptionConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.SystemWarehouse).IsRequired(false);

        builder.HasIndex(e => e.Name);
    }
}