// Decompiled with JetBrains decompiler
// Type: EmbroideryData.Map.ProductGroupMap
// Assembly: EmbroideryData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85FC7BD1-C71A-46CF-AFDC-ECA6425FE3FE
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Release\netcoreapp3.1\PubTmp\Out\EmbroideryData.dll

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EmbroideryData.Map
{
  public class ProductGroupMap : IEntityTypeConfiguration<ProductGroup>
  {
    public void Configure(EntityTypeBuilder<ProductGroup> entityBuilder)
    {
      entityBuilder.HasKey((Expression<Func<ProductGroup, object>>) (t => (object) t.Id));
      entityBuilder.HasIndex((Expression<Func<ProductGroup, object>>) (t => t.GroupCode)).IsUnique(true);
      entityBuilder.Property<string>((Expression<Func<ProductGroup, string>>) (t => t.GroupCode)).HasMaxLength(20).IsRequired(true);
      entityBuilder.Property<string>((Expression<Func<ProductGroup, string>>) (t => t.Name)).HasMaxLength(200).IsRequired(true);
      entityBuilder.HasMany<Category>((Expression<Func<ProductGroup, IEnumerable<Category>>>) (t => t.Categories)).WithOne((Expression<Func<Category, ProductGroup>>) (t => t.Group)).HasForeignKey((Expression<Func<Category, object>>) (t => (object) t.GroupId));
    }
  }
}
