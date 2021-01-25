// Decompiled with JetBrains decompiler
// Type: Repository.Base.IRepository`1
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using System.Collections.Generic;

namespace Repository.Base
{
  public interface IRepository<TEntity> where TEntity : EntityBase
  {
    long Max(string columnName);

    long Max(string columnName, string whereClause);

    int Count();

    IEnumerable<TEntity> GetAll(string whereClause);

    int Count(string whereClause);

    IEnumerable<TEntity> GetAll();

    void Update(TEntity entity, bool savezeroValue = false);

    TEntity Get(int id);

    int Add(TEntity entity, bool savezeroValue = false);

    void Delete(TEntity entity);

    IEnumerable<TEntity> GetALL(out int totalCount, string orderBy = null, int? page = null, int? pageSize = null);

    IEnumerable<TEntity> GetEntitiesBySql(string sql);
  }
}
