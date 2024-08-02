// Decompiled with JetBrains decompiler
// Type: EmbroideryService.Interface.IEmbroiderServiceItemHistoryService
// Assembly: EmbroideryService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B47DA04D-694D-42C7-AAB8-A117E695B3FB
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroideryService\bin\Debug\netcoreapp3.1\EmbroideryService.dll

using EmbroiderData;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmbroideryService.Interface
{
  public interface IEmbroiderServiceItemHistoryService
  {
    Task<EmbroiderServiceItemHistory> SaveAsync(
      EmbroiderServiceItemHistory entity);

    Task<EmbroiderServiceItemHistory> GetById(int id);

    Task<List<EmbroiderServiceItemHistory>> GetByCriteriaAsync(
      Expression<Func<EmbroiderServiceItemHistory, bool>> criteria);
  }
}
