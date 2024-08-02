// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.ViewModels.EmbroiderModel
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using System;

namespace EmbroiderManagementSystem.ViewModels
{
  public class EmbroiderModel
  {
    public int Id { get; set; }

    public string EmbroiderCode { get; set; }

    public string Name { get; set; }

    public decimal Balance { get; set; }

    public decimal OpeningBalance { get; set; }

    public string Address { get; set; }

    public string Phone { get; set; }

    public EmbroiderOrderModel Order {get;set;}
  }
}
