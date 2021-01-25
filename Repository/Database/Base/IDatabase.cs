// Decompiled with JetBrains decompiler
// Type: Repository.Database.Base.IDatabase
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using System.Threading.Tasks;

namespace Repository.Database.Base
{
  public interface IDatabase
  {
    T Execute<T>(IQuery<T> query);

    void Execute(ICommand command);

    T Execute<T>(ICommand<T> command);

    Task<T> ExecuteAsync<T>(IQueryAsync<T> query);

    Task ExecuteAsync(ICommandAsync command);

    Task<T> ExecuteAsync<T>(ICommandAsync<T> command);

    void Commit();
  }
}
