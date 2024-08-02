// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.ViewModels.UserEditViewModel
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using EmbroiderManagementSystem.Helpers;
using System.ComponentModel.DataAnnotations;

namespace EmbroiderManagementSystem.ViewModels
{
  public class UserEditViewModel : UserBaseViewModel
  {
    public string CurrentPassword { get; set; }

    [MinLength(6, ErrorMessage = "New Password must be at least 6 characters")]
    public string NewPassword { get; set; }

    [MinimumCount(1, true, false, ErrorMessage = "Roles cannot be empty")]
    public string[] Roles { get; set; }
  }
}
