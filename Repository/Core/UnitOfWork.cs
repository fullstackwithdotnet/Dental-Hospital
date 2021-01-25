// Decompiled with JetBrains decompiler
// Type: Repository.Core.UnitOfWork
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using Repository.Base;
using System;
using System.Collections;

namespace Repository.Core
{
  public class UnitOfWork : IUnitOfWork, IDisposable
  {
    private IDbContext _context;
    private bool disposedValue;

    public UnitOfWork(IDbContext dbContext)
    {
      this._context = dbContext;
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase
    {
      Hashtable hashtable = new Hashtable();
      string name = typeof (TEntity).Name;
      lock (name)
      {
        if (!hashtable.ContainsKey((object) name))
        {
          object instance = Activator.CreateInstance(typeof (RepositoryBase<>).MakeGenericType(typeof (TEntity)), new object[1]
          {
            (object) this._context
          });
          hashtable.Add((object) name, instance);
        }
      }
      return (IRepository<TEntity>) hashtable[(object) name];
    }

    public void Save()
    {
      this._context.SaveChanges();
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposedValue)
        return;
      int num = disposing ? 1 : 0;
      this.disposedValue = true;
    }

    void IDisposable.Dispose()
    {
      this.Dispose(true);
    }
  }
}
