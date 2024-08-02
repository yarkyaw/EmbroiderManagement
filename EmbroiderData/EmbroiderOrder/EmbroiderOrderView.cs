// Decompiled with JetBrains decompiler
// Type: EmbroideryData.EmbroiderOrderView
// Assembly: EmbroideryData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85FC7BD1-C71A-46CF-AFDC-ECA6425FE3FE
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Release\netcoreapp3.1\PubTmp\Out\EmbroideryData.dll

using EmbroiderData;
using System;

namespace EmbroideryData
{
  public class EmbroiderOrderView
  {
    public int Id { get; set; }

    public string OrderNo { get; set; }

    public DateTime OrderDate { get; set; }

    public string ProductWeightName { get; set; }

    public Status OrderStatus { get; set; }

    public OrderType OrderType { get; set; }

    public string CategoryName { get; set; }
  }
}
