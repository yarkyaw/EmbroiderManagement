// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.ViewModels.UserViewModel
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using EmbroiderManagementSystem.Helpers;

namespace EmbroiderManagementSystem.ViewModels
{
  public class UserViewModel : UserBaseViewModel
  {
    public bool IsLockedOut { get; set; }

    [MinimumCount(1, true, false, ErrorMessage = "Roles cannot be empty")]
    public string[] Roles { get; set; }
  }
}
