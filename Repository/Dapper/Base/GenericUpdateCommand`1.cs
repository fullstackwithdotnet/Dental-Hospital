// Decompiled with JetBrains decompiler
// Type: Repository.Dapper.Base.GenericUpdateCommand`1
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using Dapper;
using Repository.Base;
using Repository.Database.Base;
using System;
using System.Collections.Generic;
using System.Data;

namespace Repository.Dapper.Base
{
  public class GenericUpdateCommand<TEntity> : ICommand where TEntity : EntityBase
  {
    private string _columnName;
    private Dictionary<string, object> _dict;
    private TEntity _entity;
    private int _id;
    private readonly bool _saveZero;

    public GenericUpdateCommand(TEntity entity, bool savezeroval)
    {
      this._entity = entity;
      this._saveZero = savezeroval;
    }

    public GenericUpdateCommand(int id, Dictionary<string, object> dict)
    {
      this._dict = dict;
      this._id = id;
    }

    public void Execute(IDbConnection connection, IDbTransaction transaction = null)
    {
      if (this._dict == null && (object) this._entity == null)
        throw new Exception("Entity and Dictionary both cannot be null");
      if (this._dict == null || this._dict.Count == 0)
        this._dict = Utils.GetPropertiesAndValues((object) this._entity, true, this._saveZero);
      string columns = string.Empty;
      string values = string.Empty;
      Utils.GetColumnValueStrings(this._dict, out columns, out values);
      this._columnName = (object) this._entity != null ? Utils.GetPKColumnName((object) this._entity, out this._id) : Utils.GetPKColumnName(typeof (TEntity));
      string sql = string.Format("UPDATE [{0}] SET {1} WHERE {2} = {3}", (object) typeof (TEntity).Name, (object) this.GetUpdateString(this._dict), (object) this._columnName, (object) this._id);
      if (transaction != null)
        connection.ExecuteScalar(sql, (object) CommandType.Text, transaction, new int?(), new CommandType?());
      else
        connection.ExecuteScalar(sql, (object) CommandType.Text, transaction, new int?(), new CommandType?());
    }

    private string GetUpdateString(Dictionary<string, object> dict)
    {
      string empty = string.Empty;
      foreach (KeyValuePair<string, object> keyValuePair in dict)
        empty += string.Format("{0} = {1},", (object) keyValuePair.Key, keyValuePair.Value);
      return empty.Remove(empty.Length - 1);
    }
  }
}
