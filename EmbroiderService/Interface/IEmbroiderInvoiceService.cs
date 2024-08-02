// Decompiled with JetBrains decompiler
// Type: EmbroideryService.Interface.IEmbroiderInvoiceService
// Assembly: EmbroideryService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B47DA04D-694D-42C7-AAB8-A117E695B3FB
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroideryService\bin\Debug\netcoreapp3.1\EmbroideryService.dll

using EmbroiderData.DTO;
using EmbroideryData;
using EmbroideryData.DTO;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EmbroideryService.Interface
{
  public interface IEmbroiderInvoiceService
  {
    Task<EmbroiderInvoice> SaveAsync(EmbroiderInvoice entity);

    Task<EmbroiderInvoice> GetById(int id);

    Task UpdateAsync(EmbroiderInvoice entity);

    Task DeleteAsync(EmbroiderInvoice entity);

    Task<List<EmbroiderInvoice>> GetListAllAsync();

    Task<List<EmbroiderInvoice>> GetAsyncWithInclude(
      Func<IQueryable<EmbroiderInvoice>, IIncludableQueryable<EmbroiderInvoice, object>> include,
      Expression<Func<EmbroiderInvoice, bool>> criteria,
      Func<IQueryable<EmbroiderInvoice>, IOrderedQueryable<EmbroiderInvoice>> orderBy = null,
      bool noTrack = true,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<List<EmbroiderInvoice>> GetByCriteriaAsync(
      Expression<Func<EmbroiderInvoice, bool>> criteria);

    Task<IQueryable<EmbroiderInvoiceDTO>> GetDTOQueryable();

    Task<List<EmbroiderInvoiceDetail>> GetEmbroiderInvoiceDetailByCriteriaAsync(
      Expression<Func<EmbroiderInvoiceDetail, bool>> criteria);

    Task UpdateEmbroiderInvoiceDetails(IList<EmbroiderInvoiceDetail> entities);
  }
}
