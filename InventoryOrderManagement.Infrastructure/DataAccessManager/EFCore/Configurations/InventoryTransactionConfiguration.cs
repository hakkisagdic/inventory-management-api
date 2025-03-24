using InventoryOrderManagement.Core;
using InventoryOrderManagement.Core.Common;
using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccessManager.EFCore.Configurations;

public class InventoryTransactionConfiguration : BaseEntityConfiguration<InventoryTransaction>
{
    public override void Configure(EntityTypeBuilder<InventoryTransaction> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.ModuleId).HasMaxLength(Constants.IdConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.ModuleName).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.ModuleCode).HasMaxLength(Constants.CodeConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.MovementDate).IsRequired(false);
        builder.Property(x => x.Status).IsRequired(false);
        builder.Property(x => x.Number).HasMaxLength(Constants.CodeConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.WarehouseId).HasMaxLength(Constants.IdConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.ProductId).HasMaxLength(Constants.IdConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.Movement).IsRequired(false);
        builder.Property(x => x.WarehouseFromId).HasMaxLength(Constants.IdConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.WarehouseToId).HasMaxLength(Constants.IdConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.QtySCSys).IsRequired(false);
        builder.Property(x => x.QtySCCount).IsRequired(false);
        builder.Property(x => x.QtySCDelta).IsRequired(false);

        builder.HasIndex(e => e.Number);
        builder.HasIndex(e => e.ModuleName);
        builder.HasIndex(e => e.ModuleCode);
        
        // İlişkileri açıkça tanımlama
        builder.HasOne(x => x.Warehouse)
            .WithMany()
            .HasForeignKey(x => x.WarehouseId)
            .IsRequired(false);
            
        builder.HasOne(x => x.WarehouseFrom)
            .WithMany()
            .HasForeignKey(x => x.WarehouseFromId)
            .IsRequired(false);
            
        builder.HasOne(x => x.WarehouseTo)
            .WithMany()
            .HasForeignKey(x => x.WarehouseToId)
            .IsRequired(false);
    }
}