using EmbroiderData;
using EmbroideryData;
using EmbroideryRepo.Interfaces;
using EmbroideryService.Interface;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmbroideryService
{
  public class EmbroiderServiceItemHistoryService : IEmbroiderServiceItemHistoryService
  {
    private readonly IAsyncRepository<EmbroiderServiceItemHistory> _repoAsync;

    public EmbroiderServiceItemHistoryService(
      IAsyncRepository<EmbroiderServiceItemHistory> repoAsync)
    {
      this._repoAsync = repoAsync;
    }

    public async Task<List<EmbroiderServiceItemHistory>> GetByCriteriaAsync(
      Expression<Func<EmbroiderServiceItemHistory, bool>> criteria)
    {
      List<EmbroiderServiceItemHistory> byCriteriaAsync = await this._repoAsync.GetByCriteriaAsync((Func<IQueryable<EmbroiderServiceItemHistory>, IIncludableQueryable<EmbroiderServiceItemHistory, object>>) null, criteria);
      return byCriteriaAsync;
    }

    public async Task<EmbroiderServiceItemHistory> GetById(int id)
    {
      EmbroiderServiceItemHistory byIdAsync = await this._repoAsync.GetByIdAsync(id);
      return byIdAsync;
    }

    public async Task<EmbroiderServiceItemHistory> SaveAsync(
      EmbroiderServiceItemHistory entity)
    {
      EmbroiderServiceItemHistory serviceItemHistory = await this._repoAsync.AddAsync(entity);
      return serviceItemHistory;
    }
  }
}
