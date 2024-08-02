// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.ViewModels.CustomerViewModelValidator
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using FluentValidation;
using System;
using System.Linq.Expressions;

namespace EmbroiderManagementSystem.ViewModels
{
  public class CustomerViewModelValidator : AbstractValidator<CustomerViewModel>
  {
    public CustomerViewModelValidator()
    {
      this.RuleFor<string>((Expression<Func<CustomerViewModel, string>>) (register => register.Name)).NotEmpty<CustomerViewModel, string>().WithMessage<CustomerViewModel, string>("Customer name cannot be empty");
      this.RuleFor<string>((Expression<Func<CustomerViewModel, string>>) (register => register.Gender)).NotEmpty<CustomerViewModel, string>().WithMessage<CustomerViewModel, string>("Gender cannot be empty");
    }
  }
}
