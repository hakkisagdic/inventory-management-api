using InventoryOrderManagement.Core;
using InventoryOrderManagement.Core.Common;
using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccessManager.EFCore.Configurations;

public class VendorContactConfiguration : BaseEntityConfiguration<VendorContact>
{
    public override void Configure(EntityTypeBuilder<VendorContact> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.Description).HasMaxLength(Constants.DescriptionConsts.MaxLength).IsRequired(false);

        builder.HasIndex(e => e.Name);
    }
}

