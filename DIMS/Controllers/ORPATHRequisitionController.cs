// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.ORPATHRequisitionController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Infrastructure;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace DIMS.Controllers
{
  public class ORPATHRequisitionController : Controller
  {
    private IORPATHRequisitionService _Service;
    private IMASCodeService _Dropdownservice;

    public ORPATHRequisitionController(IORPATHRequisitionService Service, IMASCodeService Dropdownservice)
    {
      this._Service = Service;
      this._Dropdownservice = Dropdownservice;
    }

    public ActionResult Index(long id)
    {
      ORPATHRequisitionViewModel requisitionViewModel = new ORPATHRequisitionViewModel();
      return (ActionResult) this.View("../ORPATHRequisition/Index", (object) this._Service.BindIndex(id));
    }

    [HttpGet]
    public ActionResult Create(long id)
    {
      ORPATHRequisitionViewModel requisitionViewModel = new ORPATHRequisitionViewModel();
      return (ActionResult) this.View("../ORPATHRequisition/Create", (object) this._Service.BindRequisitionPatientModel(id));
    }

    [HttpPost]
    public ActionResult Create(ORPATHRequisitionViewModel model)
    {
      CustomPrincipal user = this.User as CustomPrincipal;
      int num = 0;
      if (this.ModelState.IsValid)
      {
        model.CreatedBy = user.Identity.Name;
        num = this._Service.SaveRequisition(model);
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
      return (ActionResult) this.RedirectToAction("Edit", (object) new
      {
        allotId = model.studentAllotmentViewModel.AllotId,
        Id = num
      });
    }

    [HttpGet]
    public ActionResult Edit(long allotId, int Id)
    {
      try
      {
        ORPATHRequisitionViewModel requisitionViewModel = new ORPATHRequisitionViewModel();
        return (ActionResult) this.View("../ORPATHRequisition/Edit", (object) this._Service.BindEditRequisitionModel(allotId, Id));
      }
      catch
      {
        return (ActionResult) this.View("../Shared/Error");
      }
    }

    [HttpPost]
    public ActionResult Edit(ORPATHRequisitionViewModel model)
    {
      try
      {
        if (this.ModelState.IsValid)
        {
          this._Service.UpdateRequisition(model);
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
        return (ActionResult) this.RedirectToAction(nameof (Edit), (object) new
        {
          allotId = model.studentAllotmentViewModel.AllotId,
          Id = model.RequisitionId
        });
      }
      catch (Exception ex)
      {
        return (ActionResult) this.RedirectToAction("ErrorWrite", new RouteValueDictionary((object) new
        {
          controller = "Error",
          action = "ErrorWrite",
          message = ex.ToString()
        }));
      }
    }

    public ActionResult ORPATHCaserecordReport(int RequisitionId)
    {
      ORPATHRequisitionViewModel requisitionViewModel = new ORPATHRequisitionViewModel();
      return (ActionResult) this.View("../Reports/ORPATHCaserecordReport", (object) this._Service.BindORPATHPatientReport(RequisitionId));
    }
  }
}
