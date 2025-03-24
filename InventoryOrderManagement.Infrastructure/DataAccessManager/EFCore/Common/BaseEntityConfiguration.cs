using InventoryOrderManagement.Core.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Common;

public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasMaxLength(Constants.IdConsts.MaxLength).IsRequired(true);
        
        builder.Property(x => x.IsDeleted).HasDefaultValue(false).IsRequired(true);
        
        builder.Property(x => x.CreatedAtUtc).IsRequired(false);
        builder.Property(x => x.CreatedById).HasMaxLength(Constants.UserIdConsts.MaxLength).IsRequired(false);
        
        builder.Property(x => x.UpdatedAtUtc).IsRequired(false);
        builder.Property(x => x.UpdatedById).HasMaxLength(Constants.UserIdConsts.MaxLength).IsRequired(false);
    }
}