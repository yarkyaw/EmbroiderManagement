// Decompiled with JetBrains decompiler
// Type: EmbroideryData.Map.SubCategoryViewMap
// Assembly: EmbroideryData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85FC7BD1-C71A-46CF-AFDC-ECA6425FE3FE
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Release\netcoreapp3.1\PubTmp\Out\EmbroideryData.dll

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq.Expressions;

namespace EmbroideryData.Map
{
  public class SubCategoryViewMap : IEntityTypeConfiguration<SubCategoryView>
  {
    public void Configure(EntityTypeBuilder<SubCategoryView> entityBuilder)
    {
      entityBuilder.HasNoKey();
      entityBuilder.ToView<SubCategoryView>("SubCategoryView");
      entityBuilder.Property<int>((Expression<Func<SubCategoryView, int>>) (v => v.Id)).HasColumnName<int>("Id");
      entityBuilder.Property<string>((Expression<Func<SubCategoryView, string>>) (v => v.Name)).HasColumnName<string>("Name");
      entityBuilder.Property<string>((Expression<Func<SubCategoryView, string>>) (v => v.SubCategoryCode)).HasColumnName<string>("SubCategoryCode");
      entityBuilder.Property<string>((Expression<Func<SubCategoryView, string>>) (v => v.GroupCode)).HasColumnName<string>("GroupCode");
      entityBuilder.Property<string>((Expression<Func<SubCategoryView, string>>) (v => v.GroupName)).HasColumnName<string>("GroupName");
      entityBuilder.Property<int>((Expression<Func<SubCategoryView, int>>) (v => v.GroupId)).HasColumnName<int>("GroupId");
      entityBuilder.Property<string>((Expression<Func<SubCategoryView, string>>) (v => v.CategoryCode)).HasColumnName<string>("CategoryCode");
      entityBuilder.Property<int>((Expression<Func<SubCategoryView, int>>) (v => v.CategoryId)).HasColumnName<int>("CategoryId");
      entityBuilder.Property<string>((Expression<Func<SubCategoryView, string>>) (v => v.CategoryName)).HasColumnName<string>("CategoryName");
    }
  }
}
