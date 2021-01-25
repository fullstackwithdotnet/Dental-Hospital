// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.ReferralStatusController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Enums;
using DIMS.Infrastructure;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Metron.Entities;

namespace DIMS.Controllers
{
  public class ReferralStatusController : Controller
  {
    private IUnitOfWork _uow;
    private IOPDPatientRegistrationService _OPDservice;

    public ReferralStatusController(IUnitOfWork uow, IOPDPatientRegistrationService OPDservice)
    {
      this._uow = uow;
      this._OPDservice = OPDservice;
    }

    public ActionResult Index()
    {
      return (ActionResult) this.View();
    }

    [HttpGet]
    public ActionResult Create(int id)
    {
      ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel();
      if (!string.IsNullOrEmpty(id.ToString()))
      {
        OPDPatientRegistration patientRegistration = this._OPDservice.Get(id);
        referralStatusViewModel.patientInformationViewModel = new PatientInformationViewModel()
        {
          PatientId = patientRegistration.PatientId,
          OpNo = patientRegistration.OpNo,
          PatientName = patientRegistration.PatientName,
          Phone = patientRegistration.Phone,
          AgeGender = patientRegistration.Age.ToString() + "/" + (object) (Gender) patientRegistration.GenderId,
          Address = patientRegistration.Address
        };
        referralStatusViewModel.PatientId = patientRegistration.PatientId;
      }
      return (ActionResult) this.View("../ReferralStatus/Create", (object) referralStatusViewModel);
    }

    [HttpPost]
    public ActionResult Create(ReferralStatusViewModel departmentReferredStatusViewModel)
    {
      return (ActionResult) this.RedirectToAction("Edit");
    }

    public JsonResult GetReferralListSearch(DeptHomeViewModel model)
    {
      string str1 = model.From_Date.ToString("yyyy-MM-dd");
      string str2 = model.To_Date.ToString("yyyy-MM-dd");
      int deptId = model.DeptId;
      model.deptReferralListViewModel = (IEnumerable<DeptReferralListViewModel>) this._uow.Repository<DeptReferralListViewModel>().GetEntitiesBySql(string.Format(Queries.ReferralHomeWithDate, (object) deptId, (object) str1, (object) str2)).ToList<DeptReferralListViewModel>();
      return this.Json((object) model.deptReferralListViewModel);
    }

    public JsonResult GetOMFSReferralListSearch(DeptHomeViewModel model)
    {
      string str1 = model.From_Date.ToString("yyyy-MM-dd");
      string str2 = model.To_Date.ToString("yyyy-MM-dd");
      int deptId = model.DeptId;
      int num1 = 1;
      int num2 = 2;
      string rootUrl = (this.User as CustomPrincipal).GetRootUrl();
      model.deptReferralListViewModel = (IEnumerable<DeptReferralListViewModel>) this._uow.Repository<DeptReferralListViewModel>().GetEntitiesBySql(string.Format(Queries.ReferralHomeWithDateOMFS, (object) deptId, (object) str1, (object) str2, (object) num1, (object) num2, (object) rootUrl)).ToList<DeptReferralListViewModel>();
      return this.Json((object) model.deptReferralListViewModel);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult EditReferralStatus(int ReferredId = 0)
    {
      ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel();
      return (ActionResult) this.PartialView("../ReferralStatus/_ViewReferredReason", (object) this._uow.Repository<ReferralStatusViewModel>().GetEntitiesBySql(string.Format(Queries.GetReferredReasonbyId, (object) ReferredId)).FirstOrDefault<ReferralStatusViewModel>());
    }

    [HttpPost]
    public void UpdateRefferedReason(ReferralStatusViewModel model)
    {
      try
      {
        if (!this.ModelState.IsValid)
          return;
        this._uow.Repository<ReferralStatusViewModel>().GetEntitiesBySql(string.Format(Queries.UpdateReferredReasonbyId, (object) model.ReferredReason, (object) model.Priority, (object) model.ReferredId));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
