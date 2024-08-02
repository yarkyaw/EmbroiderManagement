// Decompiled with JetBrains decompiler
// Type: EmbroideryData.Map.CategoryViewMap
// Assembly: EmbroideryData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85FC7BD1-C71A-46CF-AFDC-ECA6425FE3FE
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Release\netcoreapp3.1\PubTmp\Out\EmbroideryData.dll

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq.Expressions;

namespace EmbroideryData.Map
{
  public class CategoryViewMap : IEntityTypeConfiguration<CategoryView>
  {
    public void Configure(EntityTypeBuilder<CategoryView> entityBuilder)
    {
      entityBuilder.HasNoKey();
      entityBuilder.ToView<CategoryView>("CategoryView");
      entityBuilder.Property<int>((Expression<Func<CategoryView, int>>) (v => v.Id)).HasColumnName<int>("Id");
      entityBuilder.Property<string>((Expression<Func<CategoryView, string>>) (v => v.Name)).HasColumnName<string>("Name");
      entityBuilder.Property<string>((Expression<Func<CategoryView, string>>) (v => v.GroupCode)).HasColumnName<string>("GroupCode");
      entityBuilder.Property<string>((Expression<Func<CategoryView, string>>) (v => v.GroupName)).HasColumnName<string>("GroupName");
      entityBuilder.Property<int>((Expression<Func<CategoryView, int>>) (v => v.GroupId)).HasColumnName<int>("GroupId");
      entityBuilder.Property<string>((Expression<Func<CategoryView, string>>) (v => v.CategoryCode)).HasColumnName<string>("CategoryCode");
    }
  }
}
