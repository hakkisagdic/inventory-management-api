using InventoryOrderManagement.Core;
using InventoryOrderManagement.Core.Common;
using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccessManager.EFCore.Configurations;

public class SalesReturnConfiguration : BaseEntityConfiguration<SalesReturn>
{
    public override void Configure(EntityTypeBuilder<SalesReturn> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Number).HasMaxLength(Constants.CodeConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.ReturnDate).IsRequired(false);
        builder.Property(x => x.Status).IsRequired(false);
        builder.Property(x => x.Description).HasMaxLength(Constants.DescriptionConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.DeliveryOrderId).HasMaxLength(Constants.IdConsts.MaxLength).IsRequired(false);

        builder.HasIndex(e => e.Number);
    }
}

