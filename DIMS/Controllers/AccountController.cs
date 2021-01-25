// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.AccountController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using AutoMapper;
using DIMS.Infrastructure;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Newtonsoft.Json;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Metron.Entities;

namespace DIMS.Controllers
{
  public class AccountController : BaseController
  {
    private IUserService _service;
    private IUnitOfWork _uow;

    public AccountController(IUnitOfWork uow, IUserService service)
      : base(uow, service)
    {
      this._service = service;
      this._uow = uow;
    }

    public ActionResult Index()
    {
      return (ActionResult) this.View();
    }

    [HttpPost]
    public ActionResult Index(LoginViewModel model, string returnUrl = "")
    {
      if (this.ModelState.IsValid)
      {
        this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        this.Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1.0));
        this.Response.Cache.SetNoStore();
        foreach (string allKey in this.Request.Cookies.AllKeys)
          this.Response.Cookies[allKey].Expires = DateTime.Now.AddDays(-1.0);
        User userNameAndPassword = this._service.GetUserByUserNameAndPassword(model.Username, model.Password);
        if (userNameAndPassword != null)
        {
          Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<User, CustomPrincipal>()));
          CustomPrincipal customPrincipal = Mapper.Map<User, CustomPrincipal>(userNameAndPassword);
          foreach (Role role in this._service.GetRolesForUser(userNameAndPassword.UserId))
            customPrincipal.Roles.Add(role.RoleName);
          foreach (UserDepartments userDepartments in this._service.GetDepartmentsForUser(userNameAndPassword.UserId))
            customPrincipal.Departments.Add(userDepartments.DeptId);
          string userData = JsonConvert.SerializeObject((object) new CustomPrincipalSerializedModel()
          {
            UserId = customPrincipal.UserId,
            FirstName = customPrincipal.FirstName,
            LastName = userNameAndPassword.LastName,
            Roles = (IEnumerable<string>) customPrincipal.Roles,
            Departments = (IEnumerable<int>) customPrincipal.Departments
          });
          this.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(new FormsAuthenticationTicket(1, userNameAndPassword.UserName, DateTime.Now, DateTime.Now.AddMinutes(15.0), false, userData))));
          return (ActionResult) this.RedirectToAction(nameof (Index), "Home");
        }
      }
      this.ModelState.AddModelError("", "Incorrect username and/or password");
      return (ActionResult) this.View((object) model);
    }

    [AllowAnonymous]
    public ActionResult LogOut()
    {
      FormsAuthentication.SignOut();
      return (ActionResult) this.RedirectToAction("Index", "Account", (RouteValueDictionary) null);
    }
  }
}
