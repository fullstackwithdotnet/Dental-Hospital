// Decompiled with JetBrains decompiler
// Type: Repository.Dapper.Base.GenericAddCommand`1
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
  public class GenericAddCommand<TEntity> : ICommand<int> where TEntity : EntityBase
  {
    private Dictionary<string, object> _dict;
    private readonly TEntity _entity;
    private readonly bool _saveZero;

    public GenericAddCommand(TEntity entity, bool savezeroval)
    {
      this._entity = entity;
      this._saveZero = savezeroval;
    }

    public GenericAddCommand(TEntity entity, Dictionary<string, object> dict)
    {
      this._entity = entity;
      this._dict = dict;
    }

    public int Execute(IDbConnection connection, IDbTransaction transaction = null)
    {
      string str1 = string.Empty;
      string str2 = string.Empty;
      if (this._dict == null || this._dict.Count == 0)
        this._dict = Utils.GetPropertiesAndValues((object) this._entity, false, this._saveZero);
      foreach (KeyValuePair<string, object> keyValuePair in this._dict)
      {
        str1 = str1 + keyValuePair.Key + ",";
        str2 = str2 + keyValuePair.Value + ",";
      }
      string sql = string.Format("INSERT INTO [{0}] ({1}) VALUES ({2})", (object) typeof (TEntity).Name, (object) str1.Remove(str1.Length - 1), (object) str2.Remove(str2.Length - 1));
      if (transaction != null)
        connection.ExecuteScalar<int>(sql, (object) CommandType.Text, transaction, new int?(), new CommandType?());
      else
        connection.ExecuteScalar<int>(sql, (object) CommandType.Text, (IDbTransaction) null, new int?(), new CommandType?());
      if (transaction != null)
        return connection.QueryFirst<int>("SELECT CAST(SCOPE_IDENTITY() as int)", (object) CommandType.Text, transaction, new int?(), new CommandType?());
      return connection.QueryFirst<int>("SELECT CAST(SCOPE_IDENTITY() as int)", (object) CommandType.Text, (IDbTransaction) null, new int?(), new CommandType?());
    }
  }
}
