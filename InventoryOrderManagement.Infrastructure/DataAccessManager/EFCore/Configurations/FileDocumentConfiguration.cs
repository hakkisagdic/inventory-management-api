using InventoryOrderManagement.Core;
using InventoryOrderManagement.Core.Common;
using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccessManager.EFCore.Configurations;

public class FileDocumentConfiguration : BaseEntityConfiguration<FileDocument>
{
    public override void Configure(EntityTypeBuilder<FileDocument> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.Description).HasMaxLength(Constants.DescriptionConsts.MaxLength).IsRequired(false);
        builder.Property(e => e.OriginalName).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
        builder.Property(e => e.GeneratedName).HasMaxLength(Constants.NameConsts.MaxLength).IsRequired(false);
        builder.Property(e => e.Extension).HasMaxLength(Constants.CodeConsts.MaxLength).IsRequired(false);
        builder.Property(e => e.FileSize).IsRequired(false);

        builder.HasIndex(e => e.OriginalName);
        builder.HasIndex(e => e.GeneratedName);
    }
}
