// Decompiled with JetBrains decompiler
// Type: Repository.Database.Database
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using Repository.Database.Base;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Repository.Database
{
  public class Database : IDatabase
  {
    private readonly IConnectionFactory connectionFactory;
    private bool _withTransaction;

    public Database(IConnectionFactory connectionFactory)
    {
      this.connectionFactory = connectionFactory;
    }

    public Database(IConnectionFactory connectionFactory, bool withTransaction)
      : this(connectionFactory)
    {
      this._withTransaction = withTransaction;
    }

    public T Execute<T>(IQuery<T> query)
    {
      try
      {
        IDbConnection openConnection = this.connectionFactory.GetOpenConnection(this._withTransaction);
        T obj = query.Execute(openConnection, this.connectionFactory.GetTransaction());
        this.CloseAndDisposeConnection(this._withTransaction ? CommitOrRollback.DontClose : CommitOrRollback.Close);
        return obj;
      }
      catch (Exception ex)
      {
        this.CloseAndDisposeConnection(CommitOrRollback.Rollback);
        throw ex;
      }
    }

    public async Task<T> ExecuteAsync<T>(IQueryAsync<T> query)
    {
      T obj1;
      try
      {
        T obj2 = await query.ExecuteAsync(this.connectionFactory.GetOpenConnection(this._withTransaction));
        this.CloseAndDisposeConnection(this._withTransaction ? CommitOrRollback.DontClose : CommitOrRollback.Close);
        obj1 = obj2;
      }
      catch (Exception ex)
      {
        this.CloseAndDisposeConnection(CommitOrRollback.Rollback);
        throw ex;
      }
      return obj1;
    }

    public void Execute(ICommand command)
    {
      try
      {
        IDbConnection openConnection = this.connectionFactory.GetOpenConnection(this._withTransaction);
        command.Execute(openConnection, this.connectionFactory.GetTransaction());
        this.CloseAndDisposeConnection(this._withTransaction ? CommitOrRollback.DontClose : CommitOrRollback.Close);
      }
      catch (Exception ex)
      {
        this.CloseAndDisposeConnection(CommitOrRollback.Rollback);
        throw ex;
      }
    }

    public T Execute<T>(ICommand<T> command)
    {
      try
      {
        IDbConnection openConnection = this.connectionFactory.GetOpenConnection(this._withTransaction);
        T obj = command.Execute(openConnection, this.connectionFactory.GetTransaction());
        this.CloseAndDisposeConnection(this._withTransaction ? CommitOrRollback.DontClose : CommitOrRollback.Close);
        return obj;
      }
      catch (Exception ex)
      {
        this.CloseAndDisposeConnection(CommitOrRollback.Rollback);
        throw ex;
      }
    }

    public async Task ExecuteAsync(ICommandAsync command)
    {
      try
      {
        await command.ExecuteAsync(this.connectionFactory.GetOpenConnection(this._withTransaction));
        this.CloseAndDisposeConnection(this._withTransaction ? CommitOrRollback.DontClose : CommitOrRollback.Close);
      }
      catch (Exception ex)
      {
        this.CloseAndDisposeConnection(CommitOrRollback.Rollback);
        throw ex;
      }
    }

    public async Task<T> ExecuteAsync<T>(ICommandAsync<T> command)
    {
      T obj1;
      try
      {
        T obj2 = await command.ExecuteAsync(this.connectionFactory.GetOpenConnection(this._withTransaction));
        this.CloseAndDisposeConnection(this._withTransaction ? CommitOrRollback.DontClose : CommitOrRollback.Close);
        obj1 = obj2;
      }
      catch (Exception ex)
      {
        this.CloseAndDisposeConnection(CommitOrRollback.Rollback);
        throw ex;
      }
      return obj1;
    }

    public void Commit()
    {
      if (!this._withTransaction)
        return;
      this.CloseAndDisposeConnection(CommitOrRollback.Commit);
    }

    private void CloseAndDisposeConnection(CommitOrRollback cor)
    {
      IDbTransaction transaction = this.connectionFactory.GetTransaction();
      IDbConnection openConnection = this.connectionFactory.GetOpenConnection(this._withTransaction);
      switch (cor)
      {
        case CommitOrRollback.Commit:
          transaction?.Commit();
          openConnection.Close();
          openConnection.Dispose();
          break;
        case CommitOrRollback.Rollback:
          transaction?.Rollback();
          openConnection.Close();
          openConnection.Dispose();
          break;
        case CommitOrRollback.Close:
          openConnection.Close();
          openConnection.Dispose();
          break;
      }
    }
  }
}
