// Decompiled with JetBrains decompiler
// Type: Repository.Dapper.Base.GenericQuery`1
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using Dapper;
using Repository.Base;
using Repository.Database.Base;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Repository.Dapper.Base
{
  public class GenericQuery<TEntity> : IQuery<IEnumerable<TEntity>> where TEntity : EntityBase
  {
    private List<string> _dict;
    private string _where;

    public GenericQuery(string where)
    {
      this._where = where;
    }

    public GenericQuery()
    {
    }

    public GenericQuery(List<string> dict, string where = null)
    {
      this._dict = dict;
    }

    public IEnumerable<TEntity> Execute(IDbConnection connection, IDbTransaction transaction)
    {
      string empty = string.Empty;
      string sql;
      if (this._dict == null || this._dict.Count == 0)
      {
        sql = string.Format("SELECT * FROM [{0}] ", (object) typeof (TEntity).Name);
      }
      else
      {
        string str1 = string.Empty;
        this.GetProperties();
        foreach (string str2 in this._dict)
          str1 = str1 + str2 + ",";
        sql = string.Format("SELECT {0} FROM [{1}]", (object) typeof (TEntity).Name, (object) str1.Remove(str1.Length - 1));
      }
      if (!string.IsNullOrEmpty(this._where))
        sql = sql + "WHERE " + this._where;
      if (transaction != null)
        return connection.Query<TEntity>(sql, (object) CommandType.Text, transaction, true, new int?(), new CommandType?());
      return connection.Query<TEntity>(sql, (object) CommandType.Text, (IDbTransaction) null, true, new int?(), new CommandType?());
    }

    public void GetProperties()
    {
      this._dict = new List<string>();
      foreach (PropertyInfo property in typeof (TEntity).GetType().GetProperties())
      {
        string empty = string.Empty;
        this._dict.Add(property.Name);
      }
    }
  }
}
