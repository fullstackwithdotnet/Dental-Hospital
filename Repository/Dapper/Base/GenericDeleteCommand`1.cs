// Decompiled with JetBrains decompiler
// Type: Repository.Dapper.Base.GenericDeleteCommand`1
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using Dapper;
using Repository.Base;
using Repository.Database.Base;
using System.Data;

namespace Repository.Dapper.Base
{
  public class GenericDeleteCommand<TEntity> : ICommand where TEntity : EntityBase
  {
    private TEntity _entity;
    private int _id;
    private string _where;

    public GenericDeleteCommand(TEntity entity)
    {
      this._entity = entity;
    }

    public GenericDeleteCommand(int id)
    {
      this._id = id;
    }

    public GenericDeleteCommand(string where)
    {
      this._where = where;
    }

    public void Execute(IDbConnection connection, IDbTransaction transaction = null)
    {
      string empty1 = string.Empty;
      string sql;
      if (string.IsNullOrEmpty(this._where))
      {
        string empty2 = string.Empty;
        sql = string.Format("DELETE FROM [{0}] WHERE {1} = {2}", (object) typeof (TEntity).Name, (object) this._entity == null ? (object) Utils.GetPKColumnName(typeof (TEntity)) : (object) Utils.GetPKColumnName((object) this._entity, out this._id), (object) this._id);
      }
      else
        sql = string.Format("DELETE FROM [{0}] WHERE {1}", (object) typeof (TEntity).Name, (object) this._where);
      if (transaction != null)
        connection.ExecuteScalar(sql, (object) CommandType.Text, transaction, new int?(), new CommandType?());
      else
        connection.ExecuteScalar(sql, (object) CommandType.Text, (IDbTransaction) null, new int?(), new CommandType?());
    }
  }
}
