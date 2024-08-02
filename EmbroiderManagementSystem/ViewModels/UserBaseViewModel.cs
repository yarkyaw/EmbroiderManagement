// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.ViewModels.UserBaseViewModel
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using System.ComponentModel.DataAnnotations;

namespace EmbroiderManagementSystem.ViewModels
{
  public abstract class UserBaseViewModel
  {
    public string Id { get; set; }

    [Required(ErrorMessage = "Username is required")]
    [StringLength(200, ErrorMessage = "Username must be between 2 and 200 characters", MinimumLength = 2)]
    public string UserName { get; set; }

    public string FullName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [StringLength(200, ErrorMessage = "Email must be at most 200 characters")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    public string JobTitle { get; set; }

    public string PhoneNumber { get; set; }

    public string Configuration { get; set; }

    public bool IsEnabled { get; set; }
  }
}
