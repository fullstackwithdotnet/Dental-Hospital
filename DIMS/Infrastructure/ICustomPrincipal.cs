// Decompiled with JetBrains decompiler
// Type: DIMS.Infrastructure.ICustomPrincipal
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using System.Security.Principal;

namespace DIMS.Infrastructure
{
  public interface ICustomPrincipal : IPrincipal
  {
    string FirstName { get; set; }

    string LastName { get; set; }
  }
}
