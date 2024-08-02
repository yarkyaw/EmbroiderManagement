using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EmbroideryRepo.Interfaces
{
  public interface IAsyncRepository<T> : IDisposable where T : class
  {
    Task<T> GetByIdAsync(int id);

    Task<T> GetByIdAsync(string id);

    Task<T> GetByIdsAsync(string firstId, string secondId);

    Task<List<T>> ListAllAsync();

    Task<T> AddAsync(T entity);

    Task AddListAsync(IEnumerable<T> entity);

    Task<T> UpdateAsync(T entity);

    Task DeleteAsync(T entity);

    Task<IQueryable<T>> GetQuearableAsync();

    Task<IEnumerable<T>> ExecuteStoredProcedure(string spQuery, object[] parameters);

    Task UpdateListAsync(IEnumerable<T> entitities);

    Task<List<T>> GetByCriteriaAsync(
      Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
      Expression<Func<T, bool>> whereClause = null,
      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
      bool noTrack = true,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<IPagedList<T>> GetPagedAsync(
      int pageSize,
      int pageIndex,
      Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
      Expression<Func<T, bool>> whereClause = null,
      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<IPagedList<TResult>> GetPagedWithSelectorAsync<TResult>(
      Expression<Func<T, TResult>> selector,
      int pageSize,
      int pageIndex,
      Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
      Expression<Func<T, bool>> whereClause = null,
      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<IQueryable<TResult>> GetWithSelectorAsync<TResult>(
      Expression<Func<T, TResult>> selector,
      Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
      Expression<Func<T, bool>> whereClause = null,
      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<IPagedList<T>> GetSequentialPagedAsync<TSeqProperty>(
      int pageSize,
      Expression<Func<T, TSeqProperty>> seqPropExpr,
      Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
      TSeqProperty bottomMostSeqId =  default(TSeqProperty),
      TSeqProperty topMostSeqId =  default(TSeqProperty),
      bool returnNewlyAddedItems = false,
      bool recent = true,
      Expression<Func<T, bool>> whereClause = null,
      bool disableTracking = true,
      CancellationToken cancellationToken = default (CancellationToken))
      where TSeqProperty : IComparable, IComparable<TSeqProperty>, IConvertible, IEquatable<TSeqProperty>;

    Task<IPagedList<TResult>> GetSequentialPagedWithSelectorAsync<TResult, TSeqProperty>(
      Expression<Func<T, TResult>> selector,
      int pageSize,
      Expression<Func<T, TSeqProperty>> seqPropExpr,
      Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
      TSeqProperty bottomMostSeqId = default(TSeqProperty),
      TSeqProperty topMostSeqId = default(TSeqProperty),
      bool returnNewlyAddedItems = false,
      bool recent = true,
      Expression<Func<T, bool>> whereClause = null,
      bool disableTracking = true,
      CancellationToken cancellationToken = default (CancellationToken))
      where TResult : class
      where TSeqProperty : IComparable, IComparable<TSeqProperty>, IConvertible, IEquatable<TSeqProperty>;
  }
}
