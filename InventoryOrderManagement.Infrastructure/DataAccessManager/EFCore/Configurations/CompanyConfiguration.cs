using InventoryOrderManagement.Core;
using InventoryOrderManagement.Core.Common;
using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Configurations;

public class CompanyConfiguration : BaseEntityConfiguration<Company>
{
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);
        
        builder.Property(x => x.Name).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.Description).HasMaxLength(Constants.DescriptionConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.Currency).HasMaxLength(Constants.CodeConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.Street).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.City).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.State).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.ZipCode).HasMaxLength(Constants.CodeConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.Country).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.PhoneNumber).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.Email).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.Website).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
    }
}