// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.BaseController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Infrastructure;
using DIMS.Services.Abstract;
using Repository.Base;
using System.Security.Principal;
using System.Web.Mvc;

namespace DIMS.Controllers
{
  public class BaseController : Controller
  {
    private IUnitOfWork _uow;
    private IUserService _service;
    private IUnitOfWork uow;

    public BaseController(IUnitOfWork uow, IUserService service)
    {
      this._uow = uow;
      this._service = service;
    }

    public BaseController(IUnitOfWork uow)
    {
      this._uow = uow;
    }

    protected void GetPermissionforUser()
    {
      CustomPrincipal user = this.HttpContext.User as CustomPrincipal;
      foreach (DeptPermissions deptPermissions in this._service.GetDepartmentsPermissionForUser(user.UserId))
        user.DeptPermission.Add(deptPermissions);
      this.HttpContext.User = (IPrincipal) user;
    }

    protected virtual CustomPrincipal User
    {
      get
      {
        return this.HttpContext.User as CustomPrincipal;
      }
    }
  }
}
