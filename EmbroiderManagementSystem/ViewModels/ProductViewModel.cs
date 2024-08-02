// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.ViewModels.ProductViewModel
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using System;

namespace EmbroiderManagementSystem.ViewModels
{
  public class ProductViewModel
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Icon { get; set; }

    public Decimal BuyingPrice { get; set; }

    public Decimal SellingPrice { get; set; }

    public int UnitsInStock { get; set; }

    public bool IsActive { get; set; }

    public bool IsDiscontinued { get; set; }

    public string ProductCategoryName { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
  }
}
