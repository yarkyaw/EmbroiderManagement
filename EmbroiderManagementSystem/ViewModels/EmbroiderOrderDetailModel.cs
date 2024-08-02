// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.ViewModels.EmbroiderOrderDetailModel
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using EmbroiderData;
using EmbroideryData;

namespace EmbroiderManagementSystem.ViewModels
{
  public class EmbroiderOrderDetailModel
  {
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int SubCategoryId { get; set; }

    public string Description { get; set; }

    public int Quantity { get; set; }

    public int Ratio { get; set; }

    public MaterialType MaterialType { get; set; }

    public SubCategoryModel SubCategory { get; set; }
  }
}
