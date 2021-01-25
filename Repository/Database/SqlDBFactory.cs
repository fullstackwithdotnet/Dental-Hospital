// Decompiled with JetBrains decompiler
// Type: Repository.Database.SqlDBFactory
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using Repository.Database.Base;

namespace Repository.Database
{
  public class SqlDBFactory : DatabaseFactoryBase
  {
    public override IDatabase Create(IConnectionFactory connectionFactory)
    {
      return (IDatabase) new Repository.Database.Database(connectionFactory);
    }

    public override IDatabase Create(IConnectionFactory connectionFactory, bool withTransaction)
    {
      return (IDatabase) new Repository.Database.Database(connectionFactory, withTransaction);
    }
  }
}
