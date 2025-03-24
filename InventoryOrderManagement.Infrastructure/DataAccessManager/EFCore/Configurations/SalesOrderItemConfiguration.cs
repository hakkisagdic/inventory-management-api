﻿using InventoryOrderManagement.Core;
using InventoryOrderManagement.Core.Common;
using InventoryOrderManagement.Infrastructure.DataAccessManager.EFCore.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccessManager.EFCore.Configurations;

public class SalesOrderItemConfiguration : BaseEntityConfiguration<SalesOrderItem>
{
    public override void Configure(EntityTypeBuilder<SalesOrderItem> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.SalesOrderId).HasMaxLength(Constants.IdConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.ProductId).HasMaxLength(Constants.IdConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.Summary).HasMaxLength(Constants.DescriptionConsts.MaxLength).IsRequired(false);
        builder.Property(x => x.UnitPrice).IsRequired(false);
        builder.Property(x => x.Quantity).IsRequired(false);
        builder.Property(x => x.Total).IsRequired(false);

    }
}

