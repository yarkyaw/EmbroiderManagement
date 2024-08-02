// Decompiled with JetBrains decompiler
// Type: EmbroiderOrderyService.Interface.IEmbroiderOrderService
// Assembly: EmbroideryService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B47DA04D-694D-42C7-AAB8-A117E695B3FB
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroideryService\bin\Debug\netcoreapp3.1\EmbroideryService.dll

using EmbroideryData;
using EmbroideryData.DTO;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EmbroiderOrderyService.Interface
{
  public interface IEmbroiderOrderService
  {
    Task<EmbroiderOrder> SaveAsync(EmbroiderOrder entity);

    Task<EmbroiderOrder> GetById(int id);

    Task UpdateAsync(EmbroiderOrder entity);

    Task DeleteAsync(EmbroiderOrder entity);

    Task<List<EmbroiderOrder>> GetListAllAsync();

    Task<List<EmbroiderOrder>> GetAsyncWithInclude(
      Func<IQueryable<EmbroiderOrder>, IIncludableQueryable<EmbroiderOrder, object>> include,
      Expression<Func<EmbroiderOrder, bool>> criteria,
      Func<IQueryable<EmbroiderOrder>, IOrderedQueryable<EmbroiderOrder>> orderBy = null,
      bool noTrack = true,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<List<EmbroiderOrder>> GetByCriteriaAsync(
      Expression<Func<EmbroiderOrder, bool>> criteria);

    Task<IQueryable<EmbroiderOrderView>> GetEmbroiderOrderViewQueryable();

    Task<IQueryable<EmbroiderOrderDTO>> GetDTOQueryable();

    Task<List<EmbroiderOrderDetail>> GetEmbroiderOrderDetailByCriteriaAsync(
      Expression<Func<EmbroiderOrderDetail, bool>> criteria);

    Task UpdateEmbroiderOrderDetails(IList<EmbroiderOrderDetail> entities);
  }
}
