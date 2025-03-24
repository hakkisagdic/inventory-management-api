using InventoryOrderManagement.Core;
using InventoryOrderManagement.Core.Common;
using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccessManager.EFCore.Configurations;

public class CustomerContactConfiguration : BaseEntityConfiguration<CustomerContact>
{
    public override void Configure(EntityTypeBuilder<CustomerContact> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.PhoneNumber).HasMaxLength(Constants.CodeConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.JobTitle).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.PhoneNumber).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.Email).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.Description).HasMaxLength(Constants.DescriptionConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.CustomerId).HasMaxLength(Constants.IdConsts.MaxLength).IsRequired(false);

        builder.HasIndex(e => e.Name);
        builder.HasIndex(e => e.PhoneNumber);
    }
}

