// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.ViewModels.PermissionViewModel
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using EmbroideryData;

namespace EmbroiderManagementSystem.ViewModels
{
  public class PermissionViewModel
  {
    public string Name { get; set; }

    public string Value { get; set; }

    public string GroupName { get; set; }

    public string Description { get; set; }

    public static explicit operator PermissionViewModel(
      ApplicationPermission permission)
    {
      return new PermissionViewModel()
      {
        Name = permission.Name,
        Value = permission.Value,
        GroupName = permission.GroupName,
        Description = permission.Description
      };
    }
  }
}
