// Decompiled with JetBrains decompiler
// Type: EmbroideryRepo.Interfaces.IPagedList`1
// Assembly: EmbroideryRepo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 647504C0-1B35-4864-B572-F8603CBAB0B5
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroideryRepo\bin\Debug\netcoreapp3.1\EmbroideryRepo.dll

using System.Collections.Generic;

namespace EmbroideryRepo.Interfaces
{
  public interface IPagedList<out T>
  {
    int PageIndex { get; }

    int PageSize { get; }

    int TotalCount { get; }

    int TotalPages { get; }

    IEnumerable<T> Items { get; }

    bool HasPreviousPage { get; }

    bool HasNextPage { get; }
  }
}
