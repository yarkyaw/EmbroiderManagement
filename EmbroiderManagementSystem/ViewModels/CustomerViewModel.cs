// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.ViewModels.CustomerViewModel
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using System.Collections.Generic;

namespace EmbroiderManagementSystem.ViewModels
{
  public class CustomerViewModel
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string Gender { get; set; }

    public ICollection<OrderViewModel> Orders { get; set; }
  }
}
