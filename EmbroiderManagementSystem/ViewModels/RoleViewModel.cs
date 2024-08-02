// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.ViewModels.RoleViewModel
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using System.ComponentModel.DataAnnotations;

namespace EmbroiderManagementSystem.ViewModels
{
  public class RoleViewModel
  {
    public string Id { get; set; }

    [Required(ErrorMessage = "Role name is required")]
    [StringLength(200, ErrorMessage = "Role name must be between 2 and 200 characters", MinimumLength = 2)]
    public string Name { get; set; }

    public string Description { get; set; }

    public int UsersCount { get; set; }

    public PermissionViewModel[] Permissions { get; set; }
  }
}
