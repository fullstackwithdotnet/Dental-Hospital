// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.OPDRevisitRegistrationController
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
  public class OPDRevisitRegistrationController : BaseController
  {
    private IUnitOfWork _uow;
    private IOPDRevisitRegistrationService _service;

    public OPDRevisitRegistrationController(IUnitOfWork uow, IOPDRevisitRegistrationService service, IUserService userservice)
      : base(uow, userservice)
    {
      this._uow = uow;
      this._service = service;
      this.TempData["Message"] = (object) null;
    }

    public ActionResult Index(int Id, string DeptCode = null, string flag = null)
    {
      try
      {
        this.GetPermissionforUser();
        if (!this.User.Departments.Contains(15))
          return (ActionResult) this.View("../Error/AccessDenied");
        OPDRevisitRegistrationViewModel model = new OPDRevisitRegistrationViewModel();
        if (this.ModelState.IsValid)
        {
          model.FollowupId = Id;
          model.flag = flag;
          if (model.flag == "Ignore")
            this._uow.Repository<FollowUp>().GetEntitiesBySql(string.Format("update Followup set Status='" + "Ignored" + "',IgnoreReason='" + "" + "' where FollowupId=" + (object) model.FollowupId + " "));
          else
            this._service.RevisitSaveFromFollowup(model);
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
        if (!string.IsNullOrEmpty(DeptCode))
          return (ActionResult) this.RedirectToAction("DeptAppointmentSearch", DeptCode);
        return (ActionResult) this.RedirectToAction("Search");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public ActionResult RevisitFollowUpList(OPDRevisitRegistrationViewModel OPRmodel)
    {
      try
      {
        this.GetPermissionforUser();
        if (!this.User.Departments.Contains(15))
          return (ActionResult) this.View("../Error/AccessDenied");
        OPRmodel.RevisitFollowUpList = (IEnumerable<FollowupViewModal>) this._service.RevisitFollowUpList(OPRmodel.PatientId).ToList<FollowupViewModal>();
        return (ActionResult) this.View();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    [HttpGet]
    public ActionResult Create(int Id)
    {
      try
      {
        this.GetPermissionforUser();
        if (!this.User.Departments.Contains(15))
          return (ActionResult) this.View("../Error/AccessDenied");
        OPDRevisitRegistrationViewModel registrationViewModel = new OPDRevisitRegistrationViewModel();
        if (this.ModelState.IsValid)
        {
          registrationViewModel.PatientId = Id;
          registrationViewModel = this._service.BindPatientModel(registrationViewModel.PatientId);
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
        return (ActionResult) this.View((object) registrationViewModel);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    [HttpPost]
    public ActionResult Create(OPDRevisitRegistrationViewModel model)
    {
      try
      {
        this.GetPermissionforUser();
        if (!this.User.Departments.Contains(15))
          return (ActionResult) this.View("../Error/AccessDenied");
        int num = 0;
        int patientId = model.PatientId;
        if (this.ModelState.IsValid)
        {
          num = this._service.RevisitSave(model);
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
        return (ActionResult) this.RedirectToAction("Edit", new RouteValueDictionary((object) new
        {
          controller = "OPDRevisitRegistration",
          action = "Edit",
          patientId = patientId,
          RevisitId = num
        }));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    [HttpGet]
    public ActionResult Edit(int PatientId, int RevisitId)
    {
      try
      {
        this.GetPermissionforUser();
        if (!this.User.Departments.Contains(15))
          return (ActionResult) this.View("../Error/AccessDenied");
        OPDRevisitRegistrationViewModel registrationViewModel = new OPDRevisitRegistrationViewModel();
        return (ActionResult) this.View("../OPDRevisitRegistration/Edit", (object) this._service.EditBindOPDRViewmodel(PatientId, RevisitId));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    [HttpPost]
    public ActionResult Edit(OPDRevisitRegistrationViewModel Model)
    {
      try
      {
        this.GetPermissionforUser();
        int num = 0;
        if (!this.User.Departments.Contains(15))
          return (ActionResult) this.View("../Error/AccessDenied");
        int patientId = Model.PatientId;
        if (this.ModelState.IsValid)
        {
          num = this._service.UpdateRevisit(Model);
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
        this.TempData["Message"] = (object) "Updated Successfully";
        return (ActionResult) this.RedirectToAction(nameof (Edit), new RouteValueDictionary((object) new
        {
          controller = "OPDRevisitRegistration",
          action = nameof (Edit),
          PatientId = patientId,
          RevisitId = num
        }));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult Search()
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(15))
        return (ActionResult) this.View("../Error/AccessDenied");
      return (ActionResult) this.View("../OPDRevisitRegistration/FollowupList", (object) new OPDFollowupSearchViewModel()
      {
        From_Date = DateTime.Now,
        To_Date = DateTime.Now
      });
    }

    public JsonResult GetOPDFollowupSearchList(OPDFollowupSearchViewModel model)
    {
      string From_Date = model.From_Date.ToString("yyyy-MM-dd");
      string To_Date = model.To_Date.ToString("yyyy-MM-dd");
      string rootUrl = this.User.GetRootUrl();
      model.FollowupSearchDetails = this._service.opdFollowupSearchDetails(From_Date, To_Date, rootUrl);
      return this.Json((object) model.FollowupSearchDetails);
    }

    public JsonResult GetOPDFollowupByDeptSearchList(OPDFollowupSearchViewModel model)
    {
      string From_Date = model.From_Date.ToString("yyyy-MM-dd");
      string To_Date = model.To_Date.ToString("yyyy-MM-dd");
      int deptId = model.DeptId;
      string rootUrl = this.User.GetRootUrl();
      model.FollowupSearchDetails = this._service.opdFollowupSearchDetailsbyDeptId(From_Date, To_Date, deptId, rootUrl);
      return this.Json((object) model.FollowupSearchDetails);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult RevisitSearch()
    {
      try
      {
        this.GetPermissionforUser();
        if (!this.User.Departments.Contains(15))
          return (ActionResult) this.View("../Error/AccessDenied");
        return (ActionResult) this.View("../OPDRevisitRegistration/_RevisitPatientSearch", (object) new OPDRevisitSearchViewModel()
        {
          From_Date = DateTime.Now,
          To_Date = DateTime.Now
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public JsonResult GetOPDRevisitSearchList(OPDRevisitSearchViewModel model)
    {
      string From_Date = model.From_Date.ToString("yyyy-MM-dd");
      string To_Date = model.To_Date.ToString("yyyy-MM-dd");
      string rootUrl = this.User.GetRootUrl();
      model.RevistSearchDetails = this._service.opdRevisitSearchDetails(From_Date, To_Date, rootUrl);
      return this.Json((object) model.RevistSearchDetails);
    }

    public ActionResult RescheduleCreate(int FollowupId, int DeptId)
    {
      FollowupViewModal followupViewModal = new FollowupViewModal();
      return (ActionResult) this.PartialView("../OPDRevisitRegistration/_RescheduleCreate", (object) this._service.DisplayRescheduleDetails(FollowupId, DeptId));
    }

    [HttpPost]
    public ActionResult RescheduleCreate(FollowupViewModal followup)
    {
      if (this.ModelState.IsValid)
      {
        CustomPrincipal user = this.User;
        this._uow.Repository<FollowUp>().GetEntitiesBySql(string.Format(Queries.UpdateReschedulebyFollowUpId, (object) followup.FollowupDate, (object) followup.FollowupTime, (object) followup.FollowupReason, (object) followup.FollowupId));
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
      return (ActionResult) null;
    }
  }
}
