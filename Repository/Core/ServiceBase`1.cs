// Decompiled with JetBrains decompiler
// Type: Repository.Core.ServiceBase`1
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using Repository.Base;
using System;
using System.Collections.Generic;

namespace Repository.Core
{
  public abstract class ServiceBase<TEntity> : IService<TEntity> where TEntity : EntityBase
  {
    private IUnitOfWork _uow;

    public ServiceBase(IUnitOfWork uow)
    {
      this._uow = uow;
    }

    public int Add(TEntity entity)
    {
      return this._uow.Repository<TEntity>().Add(entity, false);
    }

    public int Count()
    {
      return this._uow.Repository<TEntity>().Count();
    }

    public int Count(string whereClause)
    {
      return this._uow.Repository<TEntity>().Count(whereClause);
    }

    public void Delete(TEntity entity)
    {
      this._uow.Repository<TEntity>().Delete(entity);
    }

    public void Delete(int Id)
    {
      TEntity entity = this._uow.Repository<TEntity>().Get(Id);
      if ((object) entity == null)
        return;
      this._uow.Repository<TEntity>().Delete(entity);
    }

    public TEntity Get(int id)
    {
      return this._uow.Repository<TEntity>().Get(id);
    }

    public IEnumerable<TEntity> GetAll()
    {
      return this._uow.Repository<TEntity>().GetAll();
    }

    public IEnumerable<TEntity> GetAll(string whereClause)
    {
      return this._uow.Repository<TEntity>().GetAll(whereClause);
    }

    public IEnumerable<TEntity> GetEntitiesBySql(string sql)
    {
      return this._uow.Repository<TEntity>().GetEntitiesBySql(sql);
    }

    public int SaveChanges(bool acceptAllChangesOnSuccess)
    {
      throw new NotImplementedException();
    }

    public void Update(TEntity entity)
    {
      this._uow.Repository<TEntity>().Update(entity, false);
    }

    public long Max(string columnName)
    {
      return this._uow.Repository<TEntity>().Max(columnName);
    }

    public long Max(string columnName, string whereClause)
    {
      return this._uow.Repository<TEntity>().Max(columnName, whereClause);
    }
  }
}
