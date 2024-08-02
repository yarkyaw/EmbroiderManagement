
using EmbroideryData;
using EmbroideryRepo.Extension;
using EmbroideryRepo.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace EmbroideryRepo
{
  public class Repository<T> : IRepository<T>, IDisposable, IAsyncRepository<T>
    where T : class
  {
    private readonly EmbroideryContext context;
    private DbSet<T> entities;
    private bool _disposed;
    private string errorMessage = string.Empty;

    public Repository(EmbroideryContext context)
    {
      this.context = context;
      this.entities = context.Set<T>();
    }

    public IEnumerable<T> ListAll() => (IEnumerable<T>) this.entities.AsQueryable();

    public virtual T GetById(Guid id) => this.entities.Find((object) id);

    public T GetByCode(string code) => this.entities.Find((object) code);

    public IQueryable<T> GetQueryable() => this.entities.AsQueryable();

    public T Add(T entity)
    {
      if ((object) entity == null)
        throw new ArgumentNullException(nameof (entity));
      this.entities.Add(entity);
      this.SaveChange();
      return entity;
    }

    public void AddList(IEnumerable<T> entitities)
    {
      if (entitities == null)
        throw new ArgumentNullException(nameof (entitities));
      foreach (T entitity in entitities)
        this.entities.Add(entitity);
      this.SaveChange();
    }

    public void Update(T entity)
    {
      if ((object) entity == null)
        throw new ArgumentNullException(nameof (entity));
      this.context.Entry<T>(entity).State = EntityState.Modified;
      this.SaveChange();
    }

    public void UpdateList(IEnumerable<T> entitities)
    {
      if (entitities == null)
        throw new ArgumentNullException(nameof (entitities));
      foreach (T entitity in entitities)
        this.context.Entry<T>(entitity).State = EntityState.Modified;
      this.SaveChange();
    }

    public async Task UpdateListAsync(IEnumerable<T> entitities)
    {
      if (entitities == null)
        throw new ArgumentNullException(nameof (entitities));
      foreach (T entitity in entitities)
      {
        T entity = entitity;
        this.context.Entry<T>(entity).State = EntityState.Modified;
        entity = default (T);
      }
      int num = await this.context.SaveChangesAsync();
    }

    public void Delete(T entity)
    {
      if ((object) entity == null)
        throw new ArgumentNullException(nameof (entity));
      this.entities.Remove(entity);
      this.SaveChange();
    }

    private void SaveChange() => this.context.SaveChanges();

    private async Task SaveChangeAsync()
    {
      int num = await this.context.SaveChangesAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
      T async = await this.entities.FindAsync((object) id);
      return async;
    }

    public virtual async Task<T> GetByIdAsync(Guid id)
    {
      T async = await this.entities.FindAsync((object) id);
      return async;
    }

    public virtual async Task<T> GetByIdsAsync(string firstId, string secondId)
    {
      T async = await this.entities.FindAsync((object) firstId, (object) secondId);
      return async;
    }

    public async Task<List<T>> ListAllAsync()
    {
      List<T> listAsync = await this.entities.ToListAsync<T>();
      return listAsync;
    }

    public async Task<IQueryable<T>> GetQuearableAsync()
    {
      int num = await Task.FromResult<int>(0);
      return this.entities.AsQueryable();
    }

    public async Task<T> AddAsync(T entity)
    {
      this.entities.Add(entity);
      int num = await this.context.SaveChangesAsync();
      return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
      if ((object) entity == null)
        throw new ArgumentNullException(nameof (entity));
      this.context.Entry<T>(entity).State = EntityState.Modified;
      int num = await this.context.SaveChangesAsync();
      return entity;
    }

    public async Task DeleteAsync(T entity)
    {
      this.entities.Remove(entity);
      int num = await this.context.SaveChangesAsync();
    }

    public void ExecQueryUpdate(string query)
    {
    }

    public async Task AddListAsync(IEnumerable<T> entitities)
    {
      if (entitities == null)
        throw new ArgumentNullException(nameof (entitities));
      foreach (T entitity in entitities)
      {
        T entity = entitity;
        this.entities.Add(entity);
        entity = default (T);
      }
      await this.SaveChangeAsync();
    }

    public async Task<List<T>> GetByCriteriaAsync(
      Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
      Expression<Func<T, bool>> whereClause,
      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
      bool noTrack,
      CancellationToken cancellationToken)
    {
      cancellationToken.ThrowIfCancellationRequested();
      this.ThrowIfDisposed();
      IQueryable<T> queryCount = (IQueryable<T>) this.entities;
      if (whereClause != null)
        queryCount = queryCount.Where<T>(whereClause);
      IQueryable<T> query = this.PrepareQueryInternal(noTrack, whereClause, include, orderBy);
      List<T> items = await query.ToListAsync<T>(cancellationToken);
      List<T> objList = items;
      queryCount = (IQueryable<T>) null;
      query = (IQueryable<T>) null;
      items = (List<T>) null;
      return objList;
    }

    public virtual async Task<IPagedList<T>> GetPagedAsync(
      int pageSize,
      int pageIndex,
      Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
      Expression<Func<T, bool>> whereClause = null,
      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      cancellationToken.ThrowIfCancellationRequested();
      this.ThrowIfDisposed();
      IQueryable<T> queryCount = (IQueryable<T>) this.entities;
      if (whereClause != null)
        queryCount = queryCount.Where<T>(whereClause);
      int totalItemCount = await queryCount.CountAsync<T>(cancellationToken);
      if (totalItemCount == 0)
        return (IPagedList<T>) new PagedList<T>((IEnumerable<T>) new List<T>(), 0, pageSize, totalItemCount);
      IQueryable<T> query = this.PrepareQueryInternal(whereClause: whereClause, include: include, orderBy: orderBy);
      List<T> items = await query.Skip<T>(pageIndex * pageSize).Take<T>(pageSize).ToListAsync<T>(cancellationToken);
      return (IPagedList<T>) new PagedList<T>((IEnumerable<T>) items, pageIndex, pageSize, totalItemCount);
    }

    public virtual async Task<IPagedList<TResult>> GetPagedWithSelectorAsync<TResult>(
      Expression<Func<T, TResult>> selector,
      int pageSize,
      int pageIndex,
      Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
      Expression<Func<T, bool>> whereClause = null,
      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      cancellationToken.ThrowIfCancellationRequested();
      this.ThrowIfDisposed();
      IQueryable<T> queryCount = (IQueryable<T>) this.entities;
      if (whereClause != null)
        queryCount = queryCount.Where<T>(whereClause);
      int totalItemCount = await queryCount.CountAsync<T>(cancellationToken);
      if (totalItemCount == 0)
        return (IPagedList<TResult>) new PagedList<TResult>((IEnumerable<TResult>) new List<TResult>(), 0, pageSize, totalItemCount);
      IQueryable<T> query = this.PrepareQueryInternal(whereClause: whereClause, include: include, orderBy: orderBy);
      List<TResult> items = await query.Select<T, TResult>(selector).Skip<TResult>(pageIndex * pageSize).Take<TResult>(pageSize).ToListAsync<TResult>(cancellationToken);
      return (IPagedList<TResult>) new PagedList<TResult>((IEnumerable<TResult>) items, pageIndex, pageSize, totalItemCount);
    }

    public virtual async Task<IQueryable<TResult>> GetWithSelectorAsync<TResult>(
      Expression<Func<T, TResult>> selector,
      Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
      Expression<Func<T, bool>> whereClause = null,
      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      cancellationToken.ThrowIfCancellationRequested();
      this.ThrowIfDisposed();
      IQueryable<T> queryCount = (IQueryable<T>) this.entities;
      if (whereClause != null)
        queryCount = queryCount.Where<T>(whereClause);
      int totalItemCount = await queryCount.CountAsync<T>(cancellationToken);
      if (totalItemCount == 0)
        return new List<TResult>().AsQueryable<TResult>();
      IQueryable<T> query = this.PrepareQueryInternal(whereClause: whereClause, include: include, orderBy: orderBy);
      IQueryable<TResult> items = query.Select<T, TResult>(selector).AsQueryable<TResult>();
      int num = await Task.FromResult<int>(0);
      return items;
    }

    public virtual Task<IPagedList<T>> GetSequentialPagedAsync<TSeqProperty>(
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
      where TSeqProperty : IComparable, IComparable<TSeqProperty>, IConvertible, IEquatable<TSeqProperty>
    {
      return this.GetSequentialPagedWithSelectorAsync<T, TSeqProperty>((Expression<Func<T, T>>) null, pageSize, seqPropExpr, include, bottomMostSeqId, topMostSeqId, returnNewlyAddedItems, recent, whereClause, disableTracking, cancellationToken);
    }

    public virtual async Task<IPagedList<TResult>> GetSequentialPagedWithSelectorAsync<TResult, TSeqProperty>(
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
      where TSeqProperty : IComparable, IComparable<TSeqProperty>, IConvertible, IEquatable<TSeqProperty>
    {
      cancellationToken.ThrowIfCancellationRequested();
      this.ThrowIfDisposed();
      int totalItemCount = await this.entities.CountAsync<T>(cancellationToken);
      if (totalItemCount == 0)
        return (IPagedList<TResult>) new PagedList<TResult>((IEnumerable<TResult>) new List<TResult>(), 0, pageSize, totalItemCount);
      TSeqProperty defaultSeqlId = default (TSeqProperty);
      if (pageSize < 0)
        pageSize = 0;
      IQueryable<T> query = this.PrepareQueryInternal(disableTracking, whereClause);
      IQueryable<T> queryWithInclude = this.PrepareQueryInternal(disableTracking, whereClause, include);
      int calculatedPageIndex = 0;
      ParameterExpression entityParameter = seqPropExpr.Parameters[0];
      if (recent)
      {
        if (topMostSeqId.Equals(defaultSeqlId) && bottomMostSeqId.Equals(defaultSeqlId))
        {
          queryWithInclude = (IQueryable<T>) queryWithInclude.OrderByDescending<T, TSeqProperty>(seqPropExpr);
          if (pageSize > 0)
            queryWithInclude = queryWithInclude.Take<T>(pageSize);
          IList<TResult> resultList1;
          if (selector != null)
          {
            List<TResult> resultList2 = await queryWithInclude.Select<T, TResult>(selector).ToListAsync<TResult>(cancellationToken);
            resultList1 = (IList<TResult>) resultList2;
            resultList2 = (List<TResult>) null;
          }
          else
          {
            List<T> objList = await queryWithInclude.ToListAsync<T>(cancellationToken);
            resultList1 = objList as IList<TResult>;
            objList = (List<T>) null;
          }
          IList<TResult> items = resultList1;
          resultList1 = (IList<TResult>) null;
          return (IPagedList<TResult>) new PagedList<TResult>((IEnumerable<TResult>) items, calculatedPageIndex, pageSize, totalItemCount);
        }
        if (returnNewlyAddedItems)
        {
          if (topMostSeqId.Equals(defaultSeqlId))
            return (IPagedList<TResult>) new PagedList<TResult>((IEnumerable<TResult>) new List<TResult>(), calculatedPageIndex, pageSize, totalItemCount);
          Expression<Func<T, bool>> upNewItemsExp = Expression.Lambda<Func<T, bool>>((Expression) Expression.GreaterThan(seqPropExpr.Body, (Expression) Expression.Constant((object) (TSeqProperty) topMostSeqId)), entityParameter);
          int newlyAddedItemCount = await query.CountAsync<T>(upNewItemsExp, cancellationToken);
          if (newlyAddedItemCount == 0)
            return (IPagedList<TResult>) new PagedList<TResult>((IEnumerable<TResult>) new List<TResult>(), calculatedPageIndex, pageSize, totalItemCount);
          queryWithInclude = (IQueryable<T>) queryWithInclude.Where<T>(upNewItemsExp).OrderBy<T, TSeqProperty>(seqPropExpr);
          if (pageSize > 0)
            queryWithInclude = queryWithInclude.Take<T>(pageSize);
          IList<TResult> resultList1;
          if (selector != null)
          {
            List<TResult> resultList2 = await queryWithInclude.Select<T, TResult>(selector).ToListAsync<TResult>(cancellationToken);
            resultList1 = (IList<TResult>) resultList2;
            resultList2 = (List<TResult>) null;
          }
          else
          {
            List<T> objList = await queryWithInclude.ToListAsync<T>(cancellationToken);
            resultList1 = objList as IList<TResult>;
            objList = (List<T>) null;
          }
          IList<TResult> newItems = resultList1;
          resultList1 = (IList<TResult>) null;
          newItems = (IList<TResult>) newItems.Reverse<TResult>().ToList<TResult>();
          if (newItems.Any<TResult>() && pageSize > 0 && newlyAddedItemCount >= pageSize)
          {
            calculatedPageIndex = newlyAddedItemCount / pageSize;
            --calculatedPageIndex;
            if (newlyAddedItemCount % pageSize > 0)
              ++calculatedPageIndex;
            if (calculatedPageIndex < 0)
              calculatedPageIndex = 0;
          }
          return (IPagedList<TResult>) new PagedList<TResult>((IEnumerable<TResult>) newItems, calculatedPageIndex, pageSize, totalItemCount);
        }
        Expression<Func<T, bool>> olderExp = Expression.Lambda<Func<T, bool>>((Expression) Expression.LessThan(seqPropExpr.Body, (Expression) Expression.Constant((object) (TSeqProperty) bottomMostSeqId)), entityParameter);
        queryWithInclude = (IQueryable<T>) queryWithInclude.Where<T>(olderExp).OrderByDescending<T, TSeqProperty>(seqPropExpr);
        if (pageSize > 0)
          queryWithInclude = queryWithInclude.Take<T>(pageSize);
        IList<TResult> resultList3;
        if (selector != null)
        {
          List<TResult> resultList1 = await queryWithInclude.Select<T, TResult>(selector).ToListAsync<TResult>(cancellationToken);
          resultList3 = (IList<TResult>) resultList1;
          resultList1 = (List<TResult>) null;
        }
        else
        {
          List<T> objList = await queryWithInclude.ToListAsync<T>(cancellationToken);
          resultList3 = objList as IList<TResult>;
          objList = (List<T>) null;
        }
        IList<TResult> pagedOlderItems = resultList3;
        resultList3 = (IList<TResult>) null;
        if (pagedOlderItems.Any<TResult>() && pageSize > 0)
        {
          int totalDownCount = await query.Where<T>(olderExp).OrderByDescending<T, TSeqProperty>(seqPropExpr).CountAsync<T>(cancellationToken);
          int totalTopCount = totalItemCount - totalDownCount;
          calculatedPageIndex = totalTopCount / pageSize;
        }
        return (IPagedList<TResult>) new PagedList<TResult>((IEnumerable<TResult>) pagedOlderItems, calculatedPageIndex, pageSize, totalItemCount);
      }
      if (bottomMostSeqId.Equals(defaultSeqlId))
      {
        queryWithInclude = (IQueryable<T>) queryWithInclude.OrderBy<T, TSeqProperty>(seqPropExpr);
        if (pageSize > 0)
          queryWithInclude = queryWithInclude.Take<T>(pageSize);
        IList<TResult> resultList1;
        if (selector != null)
        {
          List<TResult> resultList2 = await queryWithInclude.Select<T, TResult>(selector).ToListAsync<TResult>(cancellationToken);
          resultList1 = (IList<TResult>) resultList2;
          resultList2 = (List<TResult>) null;
        }
        else
        {
          List<T> objList = await queryWithInclude.ToListAsync<T>(cancellationToken);
          resultList1 = objList as IList<TResult>;
          objList = (List<T>) null;
        }
        IList<TResult> items = resultList1;
        resultList1 = (IList<TResult>) null;
        return (IPagedList<TResult>) new PagedList<TResult>((IEnumerable<TResult>) items, calculatedPageIndex, pageSize, totalItemCount);
      }
      Expression<Func<T, bool>> newerExp = Expression.Lambda<Func<T, bool>>((Expression) Expression.GreaterThan(seqPropExpr.Body, (Expression) Expression.Constant((object) (TSeqProperty) bottomMostSeqId)), entityParameter);
      queryWithInclude = (IQueryable<T>) queryWithInclude.Where<T>(newerExp).OrderBy<T, TSeqProperty>(seqPropExpr);
      if (pageSize > 0)
        queryWithInclude = queryWithInclude.Take<T>(pageSize);
      IList<TResult> resultList4;
      if (selector != null)
      {
        List<TResult> resultList1 = await queryWithInclude.Select<T, TResult>(selector).ToListAsync<TResult>(cancellationToken);
        resultList4 = (IList<TResult>) resultList1;
        resultList1 = (List<TResult>) null;
      }
      else
      {
        List<T> objList = await queryWithInclude.ToListAsync<T>(cancellationToken);
        resultList4 = objList as IList<TResult>;
        objList = (List<T>) null;
      }
      IList<TResult> pagedNewerItems = resultList4;
      resultList4 = (IList<TResult>) null;
      if (pagedNewerItems.Any<TResult>() && pageSize > 0)
      {
        int totalDownCount = await query.Where<T>(newerExp).OrderBy<T, TSeqProperty>(seqPropExpr).CountAsync<T>(cancellationToken);
        int totalTopCount = totalItemCount - totalDownCount;
        calculatedPageIndex = totalTopCount / pageSize;
      }
      return (IPagedList<TResult>) new PagedList<TResult>((IEnumerable<TResult>) pagedNewerItems, calculatedPageIndex, pageSize, totalItemCount);
    }

    public IList<T> RunStorePorcedureWithParam(
      string spName,
      string paramName,
      object paramValue)
    {
      IList<T> list = (IList<T>) null;
      this.context.LoadStoredProc(spName).WithSqlParam(paramName, paramValue).ExecuteStoredProc((Action<EFExtensions.SprocResults>) (handler => list = handler.ReadToList<T>()));
      return list;
    }

    public IList<T> RunStorePorcedureWithParams(string spName, SqlParameter[] parameters)
    {
      IList<T> list = (IList<T>) null;
      this.context.LoadStoredProc(spName).WithSqlParams(parameters).ExecuteStoredProc((Action<EFExtensions.SprocResults>) (handler => list = handler.ReadToList<T>()));
      return list;
    }

    private IQueryable<T> PrepareQueryInternal(
      bool disableTracking = true,
      Expression<Func<T, bool>> whereClause = null,
      Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
    {
      IQueryable<T> source = (IQueryable<T>) this.entities;
      if (disableTracking)
        source = source.AsNoTracking<T>();
      if (include != null)
        source = (IQueryable<T>) include(source);
      if (whereClause != null)
        source = source.Where<T>(whereClause);
      if (orderBy != null)
        source = (IQueryable<T>) orderBy(source);
      return source;
    }

    protected virtual void ThrowIfDisposed()
    {
      if (this._disposed)
        throw new ObjectDisposedException(this.GetType().Name);
    }

    public virtual void Dispose() => this._disposed = true;

    public Task<T> GetByIdAsync(string id) => throw new NotImplementedException();

    public T GetById(string id) => throw new NotImplementedException();

    public Task<IEnumerable<T>> ExecuteStoredProcedure(
      string spQuery,
      object[] parameters)
    {
      throw new NotImplementedException();
    }
  }
}
