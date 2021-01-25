// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.UserController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Infrastructure;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Metron.Entities;

namespace DIMS.Controllers
{
  public class UserController : BaseController
  {
    private IUserService _service;
    private IUnitOfWork _uow;

    public UserController(IUnitOfWork uow, IUserService service, IUserService userservice)
      : base(uow, userservice)
    {
      this._uow = uow;
      this._service = service;
    }

    [CustomAuthorize(Roles = "Admin")]
    public ActionResult Index()
    {
      this.GetPermissionforUser();
      CustomPrincipal user = this.User;
      if (!user.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      return (ActionResult) this.View((object) new UserViewModel()
      {
        DepartmentList = this._uow.Repository<MASDepartment>().GetEntitiesBySql(string.Format(Queries.LoadUserDept, (object) 0)),
        SuperAdminId = user.UserId
      });
    }

    [CustomAuthorize(Roles = "Admin")]
    [HttpGet]
    public ActionResult Create()
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      UserViewModel userViewModel = new UserViewModel();
      return (ActionResult) this.View((object) this._service.BindSaveUserModel());
    }

    [HttpPost]
    public ActionResult Create(UserViewModel model)
    {
      if (!this.User.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      if (this.ModelState.IsValid)
      {
        int num = this._service.SaveUser(model);
        model = this._service.BindSaveUserModel();
        return (ActionResult) this.RedirectToAction("Edit", new RouteValueDictionary((object) new
        {
          controller = "User",
          action = "Edit",
          id = num
        }));
      }
      foreach (ModelState modelState in (IEnumerable<ModelState>) this.ViewData.ModelState.Values)
      {
        using (IEnumerator<ModelError> enumerator = modelState.Errors.GetEnumerator())
        {
          if (enumerator.MoveNext())
          {
            ModelError current = enumerator.Current;
            return (ActionResult) this.RedirectToAction("ErrorWrite", new RouteValueDictionary((object) new
            {
              controller = "Error",
              action = "ErrorWrite",
              message = (current.ErrorMessage + "-" + (object) current.Exception)
            }));
          }
        }
      }
      return (ActionResult) this.View("../Error/AccessDenied");
    }

    [CustomAuthorize(Roles = "Admin")]
    [HttpGet]
    public ActionResult Edit(int id)
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      if (id <= 0)
        return (ActionResult) this.View("../Error/AccessDenied");
      UserViewModel userViewModel = new UserViewModel();
      return (ActionResult) this.View("../User/Edit", (object) this._service.BindEditUserModel(id));
    }

    [CustomAuthorize(Roles = "Admin")]
    [HttpPost]
    public ActionResult Edit(UserViewModel model)
    {
      try
      {
        if (!this.User.Departments.Contains(17))
          return (ActionResult) this.View("../Error/AccessDenied");
        if (this.ModelState.IsValid)
        {
          if (model.UserId > 0)
            this._service.UpdateUser(model);
        }
        else
        {
          foreach (ModelState modelState in (IEnumerable<ModelState>) this.ViewData.ModelState.Values)
          {
            using (IEnumerator<ModelError> enumerator = modelState.Errors.GetEnumerator())
            {
              if (enumerator.MoveNext())
              {
                ModelError current = enumerator.Current;
                return (ActionResult) this.RedirectToAction("ErrorWrite", new RouteValueDictionary((object) new
                {
                  controller = "Error",
                  action = "ErrorWrite",
                  message = (current.ErrorMessage + "-" + (object) current.Exception)
                }));
              }
            }
          }
        }
        return (ActionResult) this.RedirectToAction("Index", new RouteValueDictionary((object) new
        {
          controller = "User",
          action = "Index"
        }));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public ActionResult EditPassword(int userId)
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      return (ActionResult) this.View((object) new UserViewModel()
      {
        UserId = userId,
        UserName = this._uow.Repository<User>().Get(userId).UserName
      });
    }

    public JsonResult IsUserNameExists(string username)
    {
      return this.Json((object) this._service.CheckUserName(username), JsonRequestBehavior.AllowGet);
    }

    public JsonResult GetUserDetailsById(int? DeptId)
    {
      IEnumerable<UserViewModel> userViewModels = (IEnumerable<UserViewModel>) new List<UserViewModel>();
      string rootUrl = this.User.GetRootUrl();
      IEnumerable<UserViewModel> entitiesBySql;
      if (DeptId.HasValue)
      {
        int? nullable = DeptId;
        int num = 0;
        if ((nullable.GetValueOrDefault() == num ? (!nullable.HasValue ? 1 : 0) : 1) != 0)
        {
          entitiesBySql = this._uow.Repository<UserViewModel>().GetEntitiesBySql(string.Format(Queries.GetUsersWithDepartment, (object) rootUrl, (object) DeptId));
          goto label_4;
        }
      }
      entitiesBySql = this._uow.Repository<UserViewModel>().GetEntitiesBySql(string.Format(Queries.GetUsersWithoutDepartment, (object) rootUrl));
label_4:
      return this.Json((object) entitiesBySql);
    }
  }
}
