// Decompiled with JetBrains decompiler
// Type: EmbroideryData.Map.GroupViewMap
// Assembly: EmbroideryData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85FC7BD1-C71A-46CF-AFDC-ECA6425FE3FE
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Release\netcoreapp3.1\PubTmp\Out\EmbroideryData.dll

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq.Expressions;

namespace EmbroideryData.Map
{
  public class GroupViewMap : IEntityTypeConfiguration<GroupView>
  {
    public void Configure(EntityTypeBuilder<GroupView> entityBuilder)
    {
      entityBuilder.HasNoKey();
      entityBuilder.ToView<GroupView>("GroupView");
      entityBuilder.Property<int>((Expression<Func<GroupView, int>>) (v => v.Id)).HasColumnName<int>("Id");
      entityBuilder.Property<string>((Expression<Func<GroupView, string>>) (v => v.Name)).HasColumnName<string>("Name");
      entityBuilder.Property<string>((Expression<Func<GroupView, string>>) (v => v.GroupCode)).HasColumnName<string>("GroupCode");
      entityBuilder.Property<int>((Expression<Func<GroupView, int>>) (v => v.CategoryCount)).HasColumnName<int>("CategoryCount");
    }
  }
}
