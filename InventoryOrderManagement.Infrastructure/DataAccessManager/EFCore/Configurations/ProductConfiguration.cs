using InventoryOrderManagement.Core;
using InventoryOrderManagement.Core.Common;
using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccessManager.EFCore.Configurations;

public class ProductConfiguration : BaseEntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.Number).HasMaxLength(Constants.CodeConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.Description).HasMaxLength(Constants.DescriptionConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.UnitPrice).IsRequired(false);
        builder.Property(x => x.Physical).IsRequired(false);
        builder.Property(x => x.UnitMeasureId).HasMaxLength(Constants.IdConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.ProductGroupId).HasMaxLength(Constants.IdConsts.MaxLength).IsRequired(false);

        builder.HasIndex(e => e.Name);
        builder.HasIndex(e => e.Number);
    }
}

