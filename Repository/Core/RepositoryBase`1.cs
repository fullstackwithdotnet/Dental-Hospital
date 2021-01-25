// Decompiled with JetBrains decompiler
// Type: Repository.Core.RepositoryBase`1
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using Repository.Base;
using System.Collections.Generic;

namespace Repository.Core
{
  public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : EntityBase
  {
    private IDbContext _context;
    private readonly Dictionary<object, object> _filters;

    public RepositoryBase(IDbContext context)
    {
      this._context = context;
      this._filters = new Dictionary<object, object>();
    }

    public IEnumerable<TEntity> GetAll()
    {
      return this._context.GetAll<TEntity>();
    }

    public IEnumerable<TEntity> GetAll(string whereClause)
    {
      return this._context.GetAll<TEntity>(whereClause);
    }

    public void Update(TEntity entity, bool savezeroValue = false)
    {
      this._context.Update<TEntity>(entity, savezeroValue);
    }

    public TEntity Get(int id)
    {
      return this._context.Get<TEntity>(id);
    }

    public int Add(TEntity entity, bool savezeroValue = false)
    {
      return this._context.Add<TEntity>(entity, savezeroValue);
    }

    public void Delete(TEntity entity)
    {
      this._context.Delete<TEntity>(entity);
    }

    public void Delete(int id)
    {
      this._context.Delete<TEntity>(id);
    }

    public int Count()
    {
      return this._context.Count<TEntity>();
    }

    public int Count(string whereClause)
    {
      return this._context.Count<TEntity>(whereClause);
    }

    public IEnumerable<TEntity> GetEntitiesBySql(string sql)
    {
      return this._context.GetEntitiesBySql<TEntity>(sql);
    }

    public IEnumerable<TEntity> GetALL(out int totalCount, string orderBy = null, int? page = null, int? pageSize = null)
    {
      totalCount = 0;
      return this._context.GetAll<TEntity>();
    }

    public long Max(string columnName)
    {
      return this._context.Max<TEntity>(columnName);
    }

    public long Max(string columnName, string whereClause)
    {
      return this._context.Max<TEntity>(columnName, whereClause);
    }
  }
}
