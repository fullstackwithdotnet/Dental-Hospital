﻿// Decompiled with JetBrains decompiler
// Type: Repository.Database.Base.IQueryAsync`1
// Assembly: Repository, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BA205E5F-5C91-4BA4-949F-EF7055589DA9
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\Repository.dll

using System.Data;
using System.Threading.Tasks;

namespace Repository.Database.Base
{
  public interface IQueryAsync<T>
  {
    Task<T> ExecuteAsync(IDbConnection connection);
  }
}
