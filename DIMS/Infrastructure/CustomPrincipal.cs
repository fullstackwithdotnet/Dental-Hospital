// Decompiled with JetBrains decompiler
// Type: DIMS.Infrastructure.CustomPrincipal
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Enums;
using System.Collections.Generic;
using System.Security.Principal;

namespace DIMS.Infrastructure
{
  public class CustomPrincipal : ICustomPrincipal, IPrincipal
  {
    private List<string> _roles;
    private List<string> _permissions;
    private List<int> _departments;
    private List<DeptPermissions> _deptpermissions;

    public int UserId { get; set; }

    public IIdentity Identity { get; private set; }

    public CustomPrincipal(string username)
    {
      this.Identity = (IIdentity) new GenericIdentity(username);
      this._roles = new List<string>();
      this._permissions = new List<string>();
      this._departments = new List<int>();
      this._deptpermissions = new List<DeptPermissions>();
    }

    public List<string> UserRights(Department department)
    {
      List<string> stringList = new List<string>();
      foreach (DeptPermissions deptPermissions in this.DeptPermission)
      {
        if ((Department) deptPermissions.DeptId == department)
          stringList.Add(deptPermissions.PermissionName);
      }
      return stringList;
    }

    public string GetRootUrl()
    {
      return "";
    }

    public List<string> Roles
    {
      get
      {
        return this._roles;
      }
      set
      {
        this._roles = value;
      }
    }

    public List<string> Permissions
    {
      get
      {
        return this._permissions;
      }
      set
      {
        this._permissions = value;
      }
    }

    public List<int> Departments
    {
      get
      {
        return this._departments;
      }
      set
      {
        this._departments = value;
      }
    }

    public List<DeptPermissions> DeptPermission
    {
      get
      {
        return this._deptpermissions;
      }
      set
      {
        this._deptpermissions = value;
      }
    }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName
    {
      get
      {
        return this.FirstName + " " + this.LastName;
      }
    }

    public bool IsInRole(string role)
    {
      foreach (string role1 in this.Roles)
      {
        if (role.Contains(role1))
          return true;
      }
      return false;
    }

    public bool CanAdd
    {
      get
      {
        return this.Permissions.Contains(PermissionsEnum.Add.ToString());
      }
    }

    public bool CanEdit
    {
      get
      {
        return this.Permissions.Contains(PermissionsEnum.Edit.ToString());
      }
    }

    public bool CanDelete
    {
      get
      {
        return false;
      }
    }

    public bool CanView
    {
      get
      {
        return this.Permissions.Contains(PermissionsEnum.View.ToString());
      }
    }
  }
}
