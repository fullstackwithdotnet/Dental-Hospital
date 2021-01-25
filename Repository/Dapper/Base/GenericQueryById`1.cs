// Decompiled with JetBrains decompiler
// Type: Repository.Dapper.Base.GenericQueryById`1
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using Dapper;
using Repository.Base;
using Repository.Core;
using Repository.Database.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Repository.Dapper.Base
{
  public class GenericQueryById<TEntity> : IQuery<TEntity> where TEntity : EntityBase
  {
    private List<string> _dict;
    private string _columnName;
    private int _id;

    public GenericQueryById(int id)
    {
      this._id = id;
    }

    public GenericQueryById(int id, string columnName)
    {
      this._columnName = columnName;
      this._id = id;
    }

    public GenericQueryById(int id, List<string> dict)
    {
      this._dict = dict;
      this._id = id;
    }

    public GenericQueryById(int id, List<string> dict, string columnName)
    {
      this._columnName = columnName;
      this._dict = dict;
      this._id = id;
    }

    public TEntity Execute(IDbConnection connection, IDbTransaction transaction)
    {
      string empty = string.Empty;
      string str1;
      if (this._dict == null || this._dict.Count == 0)
      {
        str1 = string.Format("SELECT * FROM [{0}] ", (object) typeof (TEntity).Name);
      }
      else
      {
        string str2 = string.Empty;
        this.GetProperties();
        foreach (string str3 in this._dict)
          str2 = str2 + str3 + ",";
        str1 = string.Format("SELECT {0} FROM [{1}]", (object) typeof (TEntity).Name, (object) str2.Remove(str2.Length - 1));
      }
      this.GetPKColumnName();
      if (string.IsNullOrEmpty(this._columnName))
        throw new Exception("Could not find Primary Key column for table " + (object) typeof (TEntity));
      string sql = str1 + string.Format("WHERE {0} = {1}", (object) this._columnName, (object) this._id);
      if (transaction != null)
        return connection.QueryFirst<TEntity>(sql, (object) CommandType.Text, transaction, new int?(), new CommandType?());
      return connection.QueryFirst<TEntity>(sql, (object) CommandType.Text, (IDbTransaction) null, new int?(), new CommandType?());
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

    private void GetPKColumnName()
    {
      foreach (PropertyInfo property in typeof (TEntity).GetProperties())
      {
        if (property.CustomAttributes.Any<CustomAttributeData>((Func<CustomAttributeData, bool>) (a => a.AttributeType == typeof (PrimaryKeyAttribute))))
          this._columnName = property.Name;
      }
    }
  }
}
