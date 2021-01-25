// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.DesignationController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Infrastructure;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Metron.Entities;

namespace DIMS.Controllers
{
  public class DesignationController : BaseController
  {
    private IUnitOfWork _uow;
    private IMASDesignationService _service;

    public DesignationController(IUnitOfWork uow, IMASDesignationService service, IUserService userservice)
      : base(uow, userservice)
    {
      this._uow = uow;
      this._service = service;
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult Index()
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      IOrderedEnumerable<MASDesignation> orderedEnumerable = this._uow.Repository<MASDesignation>().GetAll().OrderBy<MASDesignation, string>((Func<MASDesignation, string>) (A => A.DesigName));
      List<DesignationViewModal> designationViewModalList = new List<DesignationViewModal>();
      foreach (MASDesignation masDesignation in (IEnumerable<MASDesignation>) orderedEnumerable)
        designationViewModalList.Add(new DesignationViewModal()
        {
          DesigId = masDesignation.DesigId,
          DesigName = masDesignation.DesigName
        });
      return (ActionResult) this.View((object) designationViewModalList);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Details(int id)
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      MASDesignation masDesignation = this._service.Get(id);
      return (ActionResult) this.View(nameof (Details), (object) new DesignationViewModal()
      {
        DesigId = masDesignation.DesigId,
        DesigName = masDesignation.DesigName
      });
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Create()
    {
      this.GetPermissionforUser();
      if (this.User.Departments.Contains(17))
        return (ActionResult) this.View((object) new DesignationViewModal());
      return (ActionResult) this.View("../Error/AccessDenied");
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult Create(DesignationViewModal Desigmodal)
    {
      if (!this.User.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      if (this.ModelState.IsValid)
      {
        if (this._service.CheckDesigName(Desigmodal.DesigName))
          this.TempData["Message"] = (object) "Designation Name already Exist";
        else
          this._service.Add(new MASDesignation()
          {
            DesigName = Desigmodal.DesigName
          });
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
      return (ActionResult) this.RedirectToAction(nameof (Create));
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Edit(int id)
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      MASDesignation masDesignation = this._service.Get(id);
      return (ActionResult) this.View(nameof (Edit), (object) new DesignationViewModal()
      {
        DesigId = masDesignation.DesigId,
        DesigName = masDesignation.DesigName
      });
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult Edit(DesignationViewModal Desigmodal)
    {
      if (!this.User.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      if (this.ModelState.IsValid)
      {
        this._service.Update(new MASDesignation()
        {
          DesigName = Desigmodal.DesigName,
          DesigId = Desigmodal.DesigId
        });
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
      return (ActionResult) this.RedirectToAction("Index");
    }
  }
}
