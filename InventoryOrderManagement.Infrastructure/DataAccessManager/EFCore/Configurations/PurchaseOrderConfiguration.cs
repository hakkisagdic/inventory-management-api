using InventoryOrderManagement.Core;
using InventoryOrderManagement.Core.Common;
using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccessManager.EFCore.Configurations;

public class PurchaseOrderConfiguration : BaseEntityConfiguration<PurchaseOrder>
{
    public override void Configure(EntityTypeBuilder<PurchaseOrder> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Number).HasMaxLength(Constants.CodeConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.OrderDate).IsRequired(false);
        builder.Property(x => x.OrderStatus).IsRequired(false);
        builder.Property(x => x.Description).HasMaxLength(Constants.DescriptionConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.VendorId).HasMaxLength(Constants.IdConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.TaxId).HasMaxLength(Constants.IdConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.BeforeTaxAmount).IsRequired(false);
        builder.Property(x => x.TaxAmount).IsRequired(false);
        builder.Property(x => x.AfterTaxAmount).IsRequired(false);

        builder.HasIndex(e => e.Number);
    }
}

