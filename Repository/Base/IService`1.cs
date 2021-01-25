// Decompiled with JetBrains decompiler
// Type: Repository.Base.IService`1
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using System.Collections.Generic;

namespace Repository.Base
{
  public interface IService<TEntity> where TEntity : EntityBase
  {
    long Max(string columnName);

    long Max(string columnName, string whereClause);

    int Add(TEntity entity);

    void Update(TEntity entity);

    IEnumerable<TEntity> GetAll();

    IEnumerable<TEntity> GetAll(string whereClause);

    TEntity Get(int id);

    void Delete(TEntity entity);

    void Delete(int id);

    int Count();

    int Count(string whereClause);

    int SaveChanges(bool acceptAllChangesOnSuccess);

    IEnumerable<TEntity> GetEntitiesBySql(string sql);
  }
}
