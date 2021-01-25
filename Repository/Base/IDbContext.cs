// Decompiled with JetBrains decompiler
// Type: Repository.Base.IDbContext
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using System.Collections.Generic;

namespace Repository.Base
{
  public interface IDbContext
  {
    long Max<TEntity>(string columnName) where TEntity : EntityBase;

    long Max<TEntity>(string columnName, string whereClause) where TEntity : EntityBase;

    int Add<TEntity>(TEntity entity, bool savezeroVal) where TEntity : EntityBase;

    void Update<TEntity>(TEntity entity, bool savezeroVal) where TEntity : EntityBase;

    IEnumerable<TEntity> GetAll<TEntity>() where TEntity : EntityBase;

    IEnumerable<TEntity> GetAll<TEntity>(string whereClause) where TEntity : EntityBase;

    TEntity Get<TEntity>(int id) where TEntity : EntityBase;

    void Delete<TEntity>(TEntity entity) where TEntity : EntityBase;

    void Delete<TEntity>(int id) where TEntity : EntityBase;

    int Count<TEntity>() where TEntity : EntityBase;

    int Count<TEntity>(string whereClause) where TEntity : EntityBase;

    int SaveChanges();

    IEnumerable<TEntity> GetEntitiesBySql<TEntity>(string sql) where TEntity : EntityBase;
  }
}
