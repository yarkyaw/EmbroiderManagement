﻿// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.DataSourceLoadOptions
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmbroiderManagement
{
  [ModelBinder(BinderType = typeof (DataSourceLoadOptionsBinder))]
  public class DataSourceLoadOptions : DataSourceLoadOptionsBase
  {
  }
}