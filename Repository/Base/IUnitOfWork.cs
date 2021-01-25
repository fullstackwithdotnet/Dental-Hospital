// Decompiled with JetBrains decompiler
// Type: Repository.Base.IUnitOfWork
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using System;

namespace Repository.Base
{
  public interface IUnitOfWork : IDisposable
  {
    void Save();

    IRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase;
  }
}
