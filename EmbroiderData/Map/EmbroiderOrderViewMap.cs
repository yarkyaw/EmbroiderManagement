// Decompiled with JetBrains decompiler
// Type: EmbroideryData.Map.EmbroiderOrderViewMap
// Assembly: EmbroideryData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85FC7BD1-C71A-46CF-AFDC-ECA6425FE3FE
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Release\netcoreapp3.1\PubTmp\Out\EmbroideryData.dll

using EmbroiderData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq.Expressions;

namespace EmbroideryData.Map
{
  public class EmbroiderOrderViewMap : IEntityTypeConfiguration<EmbroiderOrderView>
  {
    public void Configure(
      EntityTypeBuilder<EmbroiderOrderView> entityBuilder)
    {
      entityBuilder.HasNoKey();
      entityBuilder.ToView<EmbroiderOrderView>("EmbroiderOrderView");
      entityBuilder.Property<int>((Expression<Func<EmbroiderOrderView, int>>) (v => v.Id)).HasColumnName<int>("Id");
      entityBuilder.Property<string>((Expression<Func<EmbroiderOrderView, string>>) (v => v.OrderNo)).HasColumnName<string>("OrderNo");
      entityBuilder.Property<DateTime>((Expression<Func<EmbroiderOrderView, DateTime>>) (v => v.OrderDate)).HasColumnName<DateTime>("OrderDate");
      entityBuilder.Property<Status>((Expression<Func<EmbroiderOrderView, Status>>) (v => v.OrderStatus)).HasColumnName<Status>("OrderStatus");
      entityBuilder.Property<OrderType>((Expression<Func<EmbroiderOrderView, OrderType>>) (v => v.OrderType)).HasColumnName<OrderType>("OrderType");
      entityBuilder.Property<string>((Expression<Func<EmbroiderOrderView, string>>) (v => v.CategoryName)).HasColumnName<string>("CategoryName");
      entityBuilder.Property<string>((Expression<Func<EmbroiderOrderView, string>>) (v => v.ProductWeightName)).HasColumnName<string>("ProductWeightName");
    }
  }
}
