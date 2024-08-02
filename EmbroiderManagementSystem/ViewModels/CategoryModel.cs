// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.ViewModels.CategoryModel
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

namespace EmbroiderManagementSystem.ViewModels
{
  public class CategoryModel
  {
    public int Id { get; set; }

    public string CategoryCode { get; set; }

    public int GroupId { get; set; }

    public string Name { get; set; }

    public ProductGroupModel Group { get; set; }
  }
}
