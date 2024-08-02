// Decompiled with JetBrains decompiler
// Type: EmbroideryRepo.Interfaces.IRepository`1
// Assembly: EmbroideryRepo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 647504C0-1B35-4864-B572-F8603CBAB0B5
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroideryRepo\bin\Debug\netcoreapp3.1\EmbroideryRepo.dll

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace EmbroideryRepo.Interfaces
{
  public interface IRepository<T> : IDisposable where T : class
  {
    IEnumerable<T> ListAll();

    T GetById(string id);

    T GetByCode(string code);

    IQueryable<T> GetQueryable();

    T Add(T entity);

    void AddList(IEnumerable<T> entity);

    void Update(T entity);

    void UpdateList(IEnumerable<T> entity);

    void Delete(T entity);

    IList<T> RunStorePorcedureWithParams(string spName, SqlParameter[] parameters);
  }
}
