using InventoryOrderManagement.Core.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryOrderManagement.Infrastructure.SecurityManager.AspNetIdentity;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName)
            .HasMaxLength(Constants.NameConsts.MaxLength)
            .IsRequired(false);

        builder.Property(u => u.LastName)
            .HasMaxLength(Constants.NameConsts.MaxLength)
            .IsRequired(false);

        builder.Property(u => u.ProfilePictureName)
            .HasMaxLength(Constants.NameConsts.MaxLength)
            .IsRequired(false);

        builder.Property(u => u.IsDeleted)
            .IsRequired(false);

        builder.Property(u => u.CreatedAt)
            .IsRequired(false);

        builder.Property(u => u.CreatedById)
            .HasMaxLength(Constants.UserIdConsts.MaxLength)
            .IsRequired(false);

        builder.Property(u => u.UpdatedAt)
            .IsRequired(false);

        builder.Property(u => u.UpdatedById)
            .HasMaxLength(Constants.UserIdConsts.MaxLength)
            .IsRequired(false);

        builder.HasIndex(u => u.UserName);
        builder.HasIndex(u => u.Email);
        builder.HasIndex(u => u.FirstName);
        builder.HasIndex(u => u.LastName);
    }
}