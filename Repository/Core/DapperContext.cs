// Decompiled with JetBrains decompiler
// Type: Repository.Core.DapperContext
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using Repository.Base;
using Repository.Dapper.Base;
using Repository.Database;
using Repository.Database.Base;
using System.Collections.Generic;
using Unity.Attributes;

namespace Repository.Core
{
  public class DapperContext : IDbContext
  {
    private readonly IDatabase _database;

    public DapperContext(IConnectionFactory connectionFactory, bool withTransaction)
    {
      this._database = new SqlDBFactory().Create(connectionFactory, withTransaction);
    }

    [InjectionConstructor]
    public DapperContext(IConnectionFactory connectionFactory)
    {
      this._database = new SqlDBFactory().Create(connectionFactory);
    }

    public IEnumerable<TEntity> GetEntitiesBySql<TEntity>(string sql) where TEntity : EntityBase
    {
      return this._database.Execute<IEnumerable<TEntity>>((IQuery<IEnumerable<TEntity>>) new GenericQueryBySql<TEntity>(sql));
    }

    public int Add<TEntity>(TEntity entity, bool savezeroVal) where TEntity : EntityBase
    {
      return this._database.Execute<int>((ICommand<int>) new GenericAddCommand<TEntity>(entity, savezeroVal));
    }

    public int Count<TEntity>() where TEntity : EntityBase
    {
      return this._database.Execute<int>((IQuery<int>) new GenericCountQuery<TEntity>());
    }

    public int Count<TEntity>(string whereClause) where TEntity : EntityBase
    {
      return this._database.Execute<int>((IQuery<int>) new GenericCountQuery<TEntity>(whereClause));
    }

    public void Delete<TEntity>(TEntity entity) where TEntity : EntityBase
    {
      this._database.Execute((ICommand) new GenericDeleteCommand<TEntity>(entity));
    }

    public void Delete<TEntity>(int id) where TEntity : EntityBase
    {
      this._database.Execute((ICommand) new GenericDeleteCommand<TEntity>(id));
    }

    public TEntity Get<TEntity>(int id) where TEntity : EntityBase
    {
      return this._database.Execute<TEntity>((IQuery<TEntity>) new GenericQueryById<TEntity>(id));
    }

    public IEnumerable<TEntity> GetAll<TEntity>(out int totalCount, string orderBy = null, int? page = null, int? pageSize = null) where TEntity : EntityBase
    {
      totalCount = 0;
      return this._database.Execute<IEnumerable<TEntity>>((IQuery<IEnumerable<TEntity>>) new GenericQuery<TEntity>());
    }

    public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : EntityBase
    {
      return this._database.Execute<IEnumerable<TEntity>>((IQuery<IEnumerable<TEntity>>) new GenericQuery<TEntity>());
    }

    public IEnumerable<TEntity> GetAll<TEntity>(string whereClause) where TEntity : EntityBase
    {
      return this._database.Execute<IEnumerable<TEntity>>((IQuery<IEnumerable<TEntity>>) new GenericQuery<TEntity>(whereClause));
    }

    public int SaveChanges()
    {
      this._database.Commit();
      return 1;
    }

    public void Update<TEntity>(TEntity entity, bool savezeroVal) where TEntity : EntityBase
    {
      this._database.Execute((ICommand) new GenericUpdateCommand<TEntity>(entity, savezeroVal));
    }

    public long Max<TEntity>(string columnName) where TEntity : EntityBase
    {
      return this._database.Execute<long>((IQuery<long>) new GenericMaxQuery<TEntity>(columnName));
    }

    public long Max<TEntity>(string columnName, string whereClause) where TEntity : EntityBase
    {
      return this._database.Execute<long>((IQuery<long>) new GenericMaxQuery<TEntity>(columnName, whereClause));
    }
  }
}
