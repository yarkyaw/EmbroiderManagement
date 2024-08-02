using EmbroideryRepo.Interfaces;
using System.Collections.Generic;

namespace EmbroideryRepo
{
  public class PagedList<T> : IPagedList<T>
  {
    public PagedList(IEnumerable<T> items, int pageIndex, int pageSize, int totalCount)
    {
      this.Items = items;
      pageIndex = pageIndex < 0 ? 0 : pageIndex;
      pageSize = pageSize < 0 ? 0 : pageSize;
      totalCount = totalCount < 0 ? 0 : totalCount;
      this.TotalCount = totalCount;
      if (pageSize == 0 && totalCount > 0)
        pageSize = totalCount;
      if (pageSize > 0)
      {
        this.TotalPages = this.TotalCount / pageSize;
        if (this.TotalCount % pageSize > 0)
          this.TotalPages = this.TotalPages + 1;
      }
      this.PageSize = pageSize;
      this.PageIndex = pageIndex;
    }

    public int PageIndex { get; }

    public int PageSize { get; }

    public int TotalCount { get; }

    public int TotalPages { get; }

    public IEnumerable<T> Items { get; }

    public bool HasPreviousPage => this.PageIndex > 0;

    public bool HasNextPage => this.PageIndex + 1 < this.TotalPages;
  }
}
