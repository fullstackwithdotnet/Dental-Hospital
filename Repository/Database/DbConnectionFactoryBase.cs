// Decompiled with JetBrains decompiler
// Type: Repository.Database.DbConnectionFactoryBase
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using Repository.Database.Base;
using System.Data;
using System.Threading.Tasks;

namespace Repository.Database
{
  public abstract class DbConnectionFactoryBase : IConnectionFactory
  {
    public abstract string ConnectionString { get; }

    public abstract IDbConnection GetOpenConnection(bool withTransaction);

    public abstract Task<IDbConnection> GetOpenConnectionAsync(bool withTransaction);

    public abstract IDbTransaction GetTransaction();
  }
}
