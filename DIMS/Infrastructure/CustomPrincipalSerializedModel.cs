// Decompiled with JetBrains decompiler
// Type: DIMS.Infrastructure.CustomPrincipalSerializedModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using System.Collections.Generic;

namespace DIMS.Infrastructure
{
  public class CustomPrincipalSerializedModel
  {
    public int UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public IEnumerable<string> Roles { get; set; }

    public IEnumerable<string> Permissions { get; set; }

    public IEnumerable<int> Departments { get; set; }

    public IEnumerable<DeptPermissions> DeptPermission { get; set; }

    public bool CanAdd { get; set; }

    public bool CanEdit { get; set; }

    public bool CanDelete { get; set; }

    public bool CanView { get; set; }
  }
}
