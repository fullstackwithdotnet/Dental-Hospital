// Decompiled with JetBrains decompiler
// Type: Repository.Dapper.Base.GenericMaxQuery`1
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using Dapper;
using Repository.Base;
using Repository.Database.Base;
using System.Data;

namespace Repository.Dapper.Base
{
  public class GenericMaxQuery<TEntity> : IQuery<long> where TEntity : EntityBase
  {
    private string _where;
    private string _column;

    public GenericMaxQuery(string columneName, string where)
    {
      this._column = columneName;
      this._where = where;
    }

    public GenericMaxQuery(string columneName)
    {
      this._column = columneName;
    }

    public long Execute(IDbConnection connection, IDbTransaction transaction)
    {
      string empty = string.Empty;
      string sql = string.Format("SELECT MAX({0}) FROM [{1}] ", (object) this._column, (object) typeof (TEntity).Name);
      if (!string.IsNullOrEmpty(this._where))
        sql = sql + "WHERE " + this._where;
      if (transaction != null)
        return connection.QueryFirst<long>(sql, (object) CommandType.Text, transaction, new int?(), new CommandType?());
      return connection.QueryFirst<long>(sql, (object) CommandType.Text, (IDbTransaction) null, new int?(), new CommandType?());
    }
  }
}
