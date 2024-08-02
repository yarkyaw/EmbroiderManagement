// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.DataSourceLoadOptionsBinder
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmbroiderManagement
{
  public class DataSourceLoadOptionsBinder : IModelBinder
  {
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
      DataSourceLoadOptions sourceLoadOptions = new DataSourceLoadOptions();
      DataSourceLoadOptionsParser.Parse((DataSourceLoadOptionsBase) sourceLoadOptions, (Func<string, string>) (key => bindingContext.ValueProvider.GetValue(key).FirstOrDefault<string>()));
      bindingContext.Result = ModelBindingResult.Success((object) sourceLoadOptions);
      return Task.CompletedTask;
    }
  }
}
