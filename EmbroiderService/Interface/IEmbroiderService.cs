using EmbroiderData.DTO;
using EmbroideryData;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EmbroideryService.Interface
{
    #region Interfaces

    public interface IEmbroiderService
    {
        #region Methods

        Task DeleteAsync(Embroider entity);

        Task<List<Embroider>> GetAsyncWithInclude(
      Func<IQueryable<Embroider>, IIncludableQueryable<Embroider, object>> include,
      Expression<Func<Embroider, bool>> criteria,
      Func<IQueryable<Embroider>, IOrderedQueryable<Embroider>> orderBy = null,
      bool noTrack = true,
      CancellationToken cancellationToken = default(CancellationToken));

        Task<List<Embroider>> GetByCriteriaAsync(
      Expression<Func<Embroider, bool>> criteria);

        Task<Embroider> GetById(int id);

        Task<IQueryable<EmbroiderDTO>> GetEmbroiderDTOQueryable();

        Task<IQueryable<Embroider>> GetEmbroiderQueryable();

        Task<List<Embroider>> GetListAllAsync();

        Task<Embroider> SaveAsync(Embroider entity);

        Task UpdateAsync(Embroider entity);

        #endregion
    }

    #endregion
}
