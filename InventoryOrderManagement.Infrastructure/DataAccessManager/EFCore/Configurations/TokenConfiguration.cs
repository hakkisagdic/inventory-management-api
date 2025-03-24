using InventoryOrderManagement.Core.Common;
using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Common;
using InventoryOrderManagement.Infrastructure.SecurityManager.AspNetIdentity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccessManager.EFCore.Configurations;

public class TokenConfiguration : BaseEntityConfiguration<Token>
{
    public override void Configure(EntityTypeBuilder<Token> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.UserId).HasMaxLength(Constants.UserIdConsts.MaxLength).IsRequired(false);
        builder.Property(e => e.RefreshToken).HasMaxLength(Constants.LengthConsts.M).IsRequired(false);
    }
}
