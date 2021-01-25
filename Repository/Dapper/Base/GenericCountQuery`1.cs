// Decompiled with JetBrains decompiler
// Type: Repository.Dapper.Base.GenericCountQuery`1
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
  public class GenericCountQuery<TEntity> : IQuery<int> where TEntity : EntityBase
  {
    private List<string> _dict;
    private string _where;

    public GenericCountQuery(string where)
    {
      this._where = where;
    }

    public GenericCountQuery()
    {
    }

    public int Execute(IDbConnection connection, IDbTransaction transaction)
    {
      string empty = string.Empty;
      string sql = string.Format("SELECT count(*) FROM [{0}] ", (object) typeof (TEntity).Name);
      if (!string.IsNullOrEmpty(this._where))
        sql = sql + "WHERE " + this._where;
      if (transaction != null)
        return connection.QueryFirst<int>(sql, (object) CommandType.Text, transaction, new int?(), new CommandType?());
      return connection.QueryFirst<int>(sql, (object) CommandType.Text, (IDbTransaction) null, new int?(), new CommandType?());
    }
  }
}
