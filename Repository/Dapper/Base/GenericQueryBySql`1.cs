// Decompiled with JetBrains decompiler
// Type: Repository.Dapper.Base.GenericQueryBySql`1
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using Dapper;
using Repository.Base;
using Repository.Database.Base;
using System.Collections.Generic;
using System.Data;

namespace Repository.Dapper.Base
{
  public class GenericQueryBySql<TEntity> : IQuery<IEnumerable<TEntity>> where TEntity : EntityBase
  {
    private string _query;

    public GenericQueryBySql(string query)
    {
      this._query = query;
    }

    public IEnumerable<TEntity> Execute(IDbConnection connection, IDbTransaction transaction)
    {
      if (transaction != null)
        return connection.Query<TEntity>(this._query, (object) CommandType.Text, transaction, true, new int?(), new CommandType?());
      return connection.Query<TEntity>(this._query, (object) CommandType.Text, (IDbTransaction) null, true, new int?(), new CommandType?());
    }
  }
}
