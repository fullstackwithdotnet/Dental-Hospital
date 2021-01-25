// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.RegistrationController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Infrastructure;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Metron.Entities;

namespace DIMS.Controllers
{
  public class RegistrationController : BaseController
  {
    private IUnitOfWork _uow;
    private IOPDPatientRegistrationService _service;

    public RegistrationController(IUnitOfWork uow, IOPDPatientRegistrationService service, IUserService userservice)
      : base(uow, userservice)
    {
      this._uow = uow;
      this._service = service;
      this.TempData["Message"] = (object) null;
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult Index()
    {
      try
      {
        this.GetPermissionforUser();
        if (!this.User.Departments.Contains(15))
          return (ActionResult) this.View("../Error/AccessDenied");
        IEnumerable<OPDPatientRegistration> all = this._uow.Repository<OPDPatientRegistration>().GetAll(string.Format("CONVERT(char(10),RegDate,126)  = '{0}'", (object) DateTime.Now.ToString("yyyy-MM-dd")));
        List<PatientSearchViewModal> patientSearchViewModalList = new List<PatientSearchViewModal>();
        foreach (OPDPatientRegistration patientRegistration in all)
          patientSearchViewModalList.Add(new PatientSearchViewModal()
          {
            PatientId = (long) patientRegistration.PatientId,
            OPNo = patientRegistration.OpNo,
            Name = patientRegistration.PatientName,
            Age = patientRegistration.Age,
            RegDate = patientRegistration.RegDate
          });
        return (ActionResult) this.View((object) patientSearchViewModalList);
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

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Create()
    {
      try
      {
        this.GetPermissionforUser();
        if (!this.User.Departments.Contains(15))
          return (ActionResult) this.View("../Error/AccessDenied");
        return (ActionResult) this.View((object) this._service.BindPatientModel(new OPDPatientRegistrationViewModel()
        {
          RegDateDisplay = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt")
        }));
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

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult Create(OPDPatientRegistrationViewModel model)
    {
      CustomPrincipal user = this.User;
      if (!user.Departments.Contains(15))
        return (ActionResult) this.View("../Error/AccessDenied");
      int num = 0;
      if (this.ModelState.IsValid)
      {
        model.CreatedBy = user.Identity.Name;
        model.PatientName = model.PatientName.ToUpper();
        num = this._service.SavePatient(model);
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
      if (num <= 0)
        return (ActionResult) this.RedirectToAction(nameof (Create));
      this.TempData["Message"] = (object) "Saved Successfully ";
      return (ActionResult) this.RedirectToAction("Edit", (object) new
      {
        Id = num
      });
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Edit(int Id)
    {
      try
      {
        this.GetPermissionforUser();
        if (!this.User.Departments.Contains(15))
          return (ActionResult) this.View("../Error/AccessDenied");
        OPDPatientRegistrationViewModel registrationViewModel = new OPDPatientRegistrationViewModel();
        return (ActionResult) this.View(nameof (Edit), (object) this._service.BindEditPatientModel(Id));
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

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult Edit(OPDPatientRegistrationViewModel model)
    {
      CustomPrincipal user = this.User;
      if (!user.Departments.Contains(15))
        return (ActionResult) this.View("../Error/AccessDenied");
      if (this.ModelState.IsValid)
      {
        model.ModifiedBy = user.Identity.Name;
        model.PatientName = model.PatientName.ToUpper();
        this._service.UpdatePatient(model);
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
      this.TempData["Message"] = (object) "Updated Successfully ";
      return (ActionResult) this.RedirectToAction(nameof (Edit), (object) new
      {
        Id = model.PatientId
      });
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Details(int Id)
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(15))
        return (ActionResult) this.View("../Error/AccessDenied");
      OPDPatientRegistrationViewModel registrationViewModel = new OPDPatientRegistrationViewModel();
      return (ActionResult) this.View("OPRegistrationReport", (object) this._service.DisplayPatient(Id));
    }

    public JsonResult GetStates(int id)
    {
      return this.Json((object) new SelectList((IEnumerable) this._service.GetStatesById(id), "Value", "Text"));
    }

    public JsonResult GetCities(int id)
    {
      return this.Json((object) new SelectList((IEnumerable) this._service.GetCitiesById(id), "Value", "Text"));
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult Search()
    {
      try
      {
        this.GetPermissionforUser();
        if (!this.User.Departments.Contains(15))
          return (ActionResult) this.View("../Error/AccessDenied");
        return (ActionResult) this.View("../Registration/_OPDSearchOptions", (object) new OPDSearchViewModel()
        {
          From_Date = DateTime.Now,
          To_Date = DateTime.Now,
          userid = this.User.UserId
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

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult SearchDetails()
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(15))
        return (ActionResult) this.View("../Error/AccessDenied");
      return (ActionResult) this.View("../Registration/_OPDSearchDetails", (object) new OPDSearchViewModel()
      {
        From_Date = DateTime.Now,
        To_Date = DateTime.Now
      });
    }

    public JsonResult GetOPDSearchList(OPDSearchViewModel model)
    {
      string From_Date = model.From_Date.ToString("yyyy-MM-dd");
      string To_Date = model.To_Date.ToString("yyyy-MM-dd");
      string rootUrl = this.User.GetRootUrl();
      model.SearchDetails = this._service.opdSearchDetails(From_Date, To_Date, rootUrl);
      return this.Json((object) model.SearchDetails);
    }

    public JsonResult GetOPDSearchDetList(OPDSearchViewModel model)
    {
      string From_Date = model.From_Date.ToString("yyyy-MM-dd");
      string To_Date = model.To_Date.ToString("yyyy-MM-dd");
      string rootUrl = this.User.GetRootUrl();
      string opNo = model.OPNo;
      string patientName = model.PatientName;
      model.SearchDetails = this._service.opdSearchDetailsList(From_Date, To_Date, rootUrl, opNo, patientName);
      return this.Json((object) model.SearchDetails);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult ViewReferralStatus(int PatientId = 0)
    {
      IEnumerable<ReferralStatusViewModel> referralStatusViewModels = (IEnumerable<ReferralStatusViewModel>) new List<ReferralStatusViewModel>();
      return (ActionResult) this.PartialView("../ReferralStatus/ViewReferral", (object) this._uow.Repository<ReferralStatusViewModel>().GetEntitiesBySql(string.Format(Queries.ReferralListWithLink, (object) PatientId)).ToList<ReferralStatusViewModel>());
    }

    public ActionResult BillingReport(int BillId, int DeptId)
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(19))
        return (ActionResult) this.View("../Error/AccessDenied");
      BillingViewModal billingViewModal = new BillingViewModal();
      if (BillId > 0)
        billingViewModal = this._service.BindReport(BillId, DeptId);
      return (ActionResult) this.View("../Reports/BillingReport", (object) billingViewModal);
    }

    public FileResult Download(string fileName)
    {
      if (fileName == null)
        return (FileResult) null;
      string fileName1 = Path.Combine(this.Server.MapPath("~/Content/Upload/"), fileName);
      return (FileResult) this.File(fileName1, MimeMapping.GetMimeMapping(fileName1), fileName);
    }
  }
}
