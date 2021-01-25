// Decompiled with JetBrains decompiler
// Type: Repository.Database.DatabaseFactoryBase
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using Repository.Database.Base;

namespace Repository.Database
{
  public abstract class DatabaseFactoryBase
  {
    public abstract IDatabase Create(IConnectionFactory connectionFactory);

    public abstract IDatabase Create(IConnectionFactory connectionFactory, bool withTransaction);
  }
}
