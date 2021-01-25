// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.OMFSController
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
using System.Web.Routing;
using Metron.Entities;

namespace DIMS.Controllers
{
  public class OMFSController : BaseController
  {
    private IUnitOfWork _uow;
    private IOMFSCasesheetService _service;
      private IOPDPatientRegistrationService _opdService;

      public OMFSController(IUnitOfWork uow, IOMFSCasesheetService service, IUserService userservice,
          IOPDPatientRegistrationService opdService)
          : base(uow, userservice)
      {
          _uow = uow;
          _service = service;
          _opdService = opdService;
          TempData["Message"] = null;
      }

      [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult Index()
    {
      try
      {
        GetPermissionforUser();
        CustomPrincipal user = User;
        List<string> stringList = new List<string>();
        string permission = Convert.ToString(PermissionsEnum.Allotment);
        if (user.DeptPermission.Where<DeptPermissions>(x => x.DeptId.Equals(2)).Where<DeptPermissions>(x => x.PermissionName.Equals(permission)).ToList<DeptPermissions>().Count <= 0)
          return View("../Error/AccessDenied");
        return View("../OMFS/Index", new DeptHomeViewModel()
        {
            From_Date = DateTime.Now,
            To_Date = DateTime.Now,
            DeptId = 2,
            ControllerName = "OMFS"
        });
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
        {
            controller = "Error",
            action = "ErrorWrite",
            message = ex.ToString()
        }));
      }
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult OPTreatmentIndex()
    {
      try
      {
        GetPermissionforUser();
        DeptHomeViewModel deptHomeViewModel = new DeptHomeViewModel();
        CustomPrincipal user = User;
        List<string> stringList = new List<string>();
        string permission = Convert.ToString(PermissionsEnum.CancelAllotment);
        List<DeptPermissions> deptPermission = user.DeptPermission;
        deptHomeViewModel.AccessYNo = deptPermission.Where<DeptPermissions>(x => x.DeptId.Equals(2)).Where<DeptPermissions>(x => x.PermissionName.Equals(permission)).ToList<DeptPermissions>().Count > 0;
        if (!user.Departments.Contains(2))
          return View("../Error/AccessDenied");
        deptHomeViewModel.From_Date = DateTime.Now;
        deptHomeViewModel.To_Date = DateTime.Now;
        deptHomeViewModel.DeptId = 2;
        deptHomeViewModel.ControllerName = "OMFS";
        return View("../OMFS/OPTreatmentIndex", deptHomeViewModel);
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
        {
            controller = "Error",
            action = "ErrorWrite",
            message = ex.ToString()
        }));
      }
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult IPScheduleApproval()
    {
      try
      {
        GetPermissionforUser();
        if (!User.Departments.Contains(2))
          return View("../Error/AccessDenied");
        return View("../OMFS/IPScheduleApproval", new DeptHomeViewModel()
        {
            From_Date = DateTime.Now,
            To_Date = DateTime.Now,
            DeptId = 2,
            ControllerName = "OMFS"
        });
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
        {
            controller = "Error",
            action = "ErrorWrite",
            message = ex.ToString()
        }));
      }
    }

    [HttpGet]
    public ActionResult OMFSTreatment(long allotId, int patientId)
    {
      try
      {
        OMFSTreatmentViewModel treatmentViewModel = new OMFSTreatmentViewModel();
        return View("../OMFS/OMFSTreatment", _service.BindTreatmentModel(allotId, patientId));
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
        {
            controller = "Error",
            action = "ErrorWrite",
            message = ex.ToString()
        }));
      }
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Create(long allotId, int patientId, int TreatmentId)
    {
      try
      {
        GetPermissionforUser();
        if (!User.Departments.Contains(2))
          return View("../Error/AccessDenied");
        OMFSOPViewModel omfsopViewModel = new OMFSOPViewModel();
        return View("../OMFS/OPCreate", _service.BindOmfsOpPatientModel(allotId, TreatmentId));
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
        {
            controller = "Error",
            action = "ErrorWrite",
            message = ex.ToString()
        }));
      }
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult OPCreate(OMFSOPViewModel model)
    {
      try
      {
        CustomPrincipal user = User;
        if (!user.Departments.Contains(2))
          return View("../Error/AccessDenied");
        int num = 0;
        if (ModelState.IsValid)
        {
          model.CreatedBy = user.Identity.Name;
          num = _service.SaveOMFSOPPatient(model);
          model.OMFSOpId = num;
          if (!model.Approval1)
            model.ApprovalType = 1;
          _service.OMFSOpUpdateAllotment(model);
          _service.OMFSOpUpdateReferralStatus(model);
          if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
            _service.OMFSOPSavefollowUp(model);
        }
        else
        {
          foreach (ModelState modelState in ViewData.ModelState.Values)
          {
            using (IEnumerator<ModelError> enumerator = modelState.Errors.GetEnumerator())
            {
              if (enumerator.MoveNext())
              {
                ModelError current = enumerator.Current;
                return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
                {
                    controller = "Error",
                    action = "ErrorWrite",
                    message = (current.ErrorMessage + "-" + (object) current.Exception)
                }));
              }
            }
          }
        }
        return RedirectToAction("OPEditOnly", new RouteValueDictionary(new
        {
            controller = "OMFS",
            action = "OPEditOnly",
            allotId = model.studentAllotmentViewModel.AllotId,
            omfsopId = num
        }));
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
        {
            controller = "Error",
            action = "ErrorWrite",
            message = ex.ToString()
        }));
      }
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult OPEdit(long allotId, int omfsopId)
    {
      try
      {
        GetPermissionforUser();
        if (!User.Departments.Contains(2))
          return View("../Error/AccessDenied");
        OMFSOPViewModel omfsopViewModel = new OMFSOPViewModel();
        return View("../OMFS/OPEdit", _service.BindEditOmfsOpModel(allotId, omfsopId));
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
        {
            controller = "Error",
            action = "ErrorWrite",
            message = ex.ToString()
        }));
      }
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult OPEdit(OMFSOPViewModel model)
    {
      try
      {
        long allotId = model.studentAllotmentViewModel.AllotId;
        int omfsOpId = model.OMFSOpId;
        if (!User.Departments.Contains(2))
          return View("../Error/AccessDenied");
        if (ModelState.IsValid)
        {
          _service.UpdateOMFSOPPatient(model);
          _service.OMFSOpUpdateAllotment(model);
          _service.OMFSOpUpdateReferralStatus(model);
          if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
            _service.OMFSOPSavefollowUp(model);
        }
        else
        {
          foreach (ModelState modelState in ViewData.ModelState.Values)
          {
            using (IEnumerator<ModelError> enumerator = modelState.Errors.GetEnumerator())
            {
              if (enumerator.MoveNext())
              {
                ModelError current = enumerator.Current;
                return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
                {
                    controller = "Error",
                    action = "ErrorWrite",
                    message = (current.ErrorMessage + "-" + (object) current.Exception)
                }));
              }
            }
          }
        }
        TempData["Message"] = "Updated Successfully ";
        return RedirectToAction("OPEditOnly", new RouteValueDictionary(new
        {
            controller = "OMFS",
            action = "OPEditOnly",
            allotId = allotId,
            omfsopId = omfsOpId
        }));
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
        {
            controller = "Error",
            action = "ErrorWrite",
            message = ex.ToString()
        }));
      }
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult OPEditOnly(long allotId, int omfsopId)
    {
      try
      {
        GetPermissionforUser();
        CustomPrincipal user = User;
        if (!user.Departments.Contains(2))
          return View("../Error/AccessDenied");
        OMFSOPViewModel omfsopViewModel1 = new OMFSOPViewModel();
        OMFSOPViewModel omfsopViewModel2 = _service.BindEditOmfsOpModel(allotId, omfsopId);
        IEnumerable<DeptPermissions> source = user.DeptPermission.Where<DeptPermissions>(x => x.DeptId.Equals(2));
        omfsopViewModel2.EditCaseSheetAccess = source.Where<DeptPermissions>(x => x.PermissionName.Equals(Convert.ToString(PermissionsEnum.EditFreezeCase))).ToList<DeptPermissions>().Count > 0;
        return View("../OMFS/OPEdit", omfsopViewModel2);
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
        {
            controller = "Error",
            action = "ErrorWrite",
            message = ex.ToString()
        }));
      }
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult OPEditOnly(OMFSOPViewModel model)
    {
      try
      {
        long allotId = model.studentAllotmentViewModel.AllotId;
        int omfsOpId = model.OMFSOpId;
        CustomPrincipal user = User;
        if (!user.Departments.Contains(2))
          return View("../Error/AccessDenied");
        if (ModelState.IsValid)
        {
          model.ModifiedBy = user.Identity.Name;
          _service.UpdateOMFSOPPatient(model);
          _service.OMFSOpUpdateAllotment(model);
          if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
            _service.OMFSOPSavefollowUp(model);
        }
        else
        {
          foreach (ModelState modelState in ViewData.ModelState.Values)
          {
            using (IEnumerator<ModelError> enumerator = modelState.Errors.GetEnumerator())
            {
              if (enumerator.MoveNext())
              {
                ModelError current = enumerator.Current;
                return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
                {
                    controller = "Error",
                    action = "ErrorWrite",
                    message = (current.ErrorMessage + "-" + (object) current.Exception)
                }));
              }
            }
          }
        }
        TempData["Message"] = "Updated Successfully ";
        return RedirectToAction(nameof (OPEditOnly), new RouteValueDictionary(new
        {
            controller = "OMFS",
            action = nameof (OPEditOnly),
            allotId = allotId,
            omfsopId = omfsOpId
        }));
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
        {
            controller = "Error",
            action = "ErrorWrite",
            message = ex.ToString()
        }));
      }
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Operations(long allotId, int patientId, int ScheduleId = 0)
    {
      GetPermissionforUser();
      if (!User.Departments.Contains(2))
        return View("../Error/AccessDenied");
      OMFSTreatmentViewModel treatmentViewModel = new OMFSTreatmentViewModel();
      return View("../OMFS/OMFSTreatment", _service.BindTreatmentModel(allotId, patientId));
    }

    public ActionResult OMFSSchedule(long allotId, int patientId, int ScheduleId)
    {
      if (ScheduleId > 0)
      {
        OMFSIPViewModel omfsipViewModel = new OMFSIPViewModel();
        OMFSIPCasesheet omfsipCasesheet1 = new OMFSIPCasesheet();
        OMFSIPCasesheet omfsipCasesheet2 = _uow.Repository<OMFSIPCasesheet>().GetAll("PatientId=" + patientId).OrderByDescending<OMFSIPCasesheet, int>(x => x.OMFSIpId).FirstOrDefault<OMFSIPCasesheet>();
        if (omfsipCasesheet2 == null)
          return RedirectToAction("OMFSIPCreate", new RouteValueDictionary(new
          {
              allotId = allotId,
              patientId = patientId,
              scheduleId = ScheduleId
          }));
        int omfsIpId = omfsipCasesheet2.OMFSIpId;
        return RedirectToAction("OMFSIPEdit", new RouteValueDictionary(new
        {
            allotId = allotId,
            OMFSIpId = omfsIpId
        }));
      }
      OMFSOPViewModel omfsopViewModel = new OMFSOPViewModel();
      OMFSOPCasesheet omfsopCasesheet1 = new OMFSOPCasesheet();
      OMFSOPCasesheet omfsopCasesheet2 = _uow.Repository<OMFSOPCasesheet>().GetAll("PatientId=" + patientId).OrderByDescending<OMFSOPCasesheet, int>(x => x.OMFSOpId).FirstOrDefault<OMFSOPCasesheet>();
      if (omfsopCasesheet2 == null)
        return RedirectToAction("Create", new RouteValueDictionary(new
        {
            allotId = allotId,
            patientId = patientId,
            TreatmentId = 6
        }));
      int omfsOpId = omfsopCasesheet2.OMFSOpId;
      return RedirectToAction("OPEdit", new RouteValueDictionary(new
      {
          allotId = allotId,
          omfsopId = omfsOpId
      }));
    }

    public ActionResult Allotment(int PatientId = 0, long ReferredId = 0, int CourseType = 0)
    {
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      return PartialView("../StudentAllotment/_StudentAllotment", _service.DisplayAllotment(PatientId, ReferredId, CourseType));
    }

    [CustomAuthorize(Roles = "Admin,HOD, Staff, Student")]
    public ActionResult OMFSPatientSearch()
    {
      try
      {
        GetPermissionforUser();
        CustomPrincipal user = User;
        List<string> stringList = new List<string>();
        Convert.ToString(PermissionsEnum.Allotment);
        if (user.DeptPermission.Where<DeptPermissions>(x => x.DeptId.Equals(2)).ToList<DeptPermissions>().Count <= 0)
          return View("../Error/AccessDenied");
        return View("../OMFS/_OMFSSearchOptions", new OMFSIPSearchViewModel()
        {
            From_Date = DateTime.Now,
            To_Date = DateTime.Now,
            DeptId = 2,
            ControllerName = "OMFS",
            TreatmentTypesList = _uow.Repository<TreatmentTypes>().GetEntitiesBySql(string.Format(Queries.TreatmentsTypes, 2)).ToList<TreatmentTypes>()
        });
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
        {
            controller = "Error",
            action = "ErrorWrite",
            message = ex.ToString()
        }));
      }
    }

    public JsonResult GetOMFSEditSearchList(OMFSIPSearchViewModel model)
    {
      string From_Date = model.From_Date.ToString("yyyy-MM-dd");
      string To_Date = model.To_Date.ToString("yyyy-MM-dd");
      string rootUrl = User.GetRootUrl();
      model.SearchDetails = _service.omfsOPSearchDetails(From_Date, To_Date, rootUrl).ToList<OMFSSearchDetails>();
      return Json(model.SearchDetails);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult UpdateSendForApproval(OMFSOPViewModel model)
    {
      try
      {
        long allotId = model.studentAllotmentViewModel.AllotId;
        int omfsOpId = model.OMFSOpId;
        CustomPrincipal user = User;
        if (!user.Departments.Contains(2))
          return View("../Error/AccessDenied");
        if (ModelState.IsValid)
        {
          if (!model.SendForApproval1)
            model.SendForApproval1 = true;
          else if (!model.SendForApproval2)
            model.SendForApproval2 = true;
          else if (!model.SendForApproval3)
            model.SendForApproval3 = true;
          model.ModifiedBy = user.Identity.Name;
          _service.UpdateOMFSOPPatient(model);
          _service.OMFSOpUpdateAllotment(model);
          _service.SendApproval(allotId);
          if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
            _service.OMFSOPSavefollowUp(model);
        }
        else
        {
          foreach (ModelState modelState in ViewData.ModelState.Values)
          {
            using (IEnumerator<ModelError> enumerator = modelState.Errors.GetEnumerator())
            {
              if (enumerator.MoveNext())
              {
                ModelError current = enumerator.Current;
                return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
                {
                    controller = "Error",
                    action = "ErrorWrite",
                    message = (current.ErrorMessage + "-" + (object) current.Exception)
                }));
              }
            }
          }
        }
        TempData["Message"] = "Updated Successfully & Sent for Approval";
        return RedirectToAction("OPEditOnly", new RouteValueDictionary(new
        {
            controller = "OMFS",
            action = "OPEditOnly",
            allotId = allotId,
            OMFSOpId = omfsOpId
        }));
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
        {
            controller = "Error",
            action = "ErrorWrite",
            message = ex.ToString()
        }));
      }
    }

    [HttpPost]
    public ActionResult InvestigationApproval(OMFSOPViewModel model)
    {
      CustomPrincipal user = User;
      long allotId = model.studentAllotmentViewModel.AllotId;
      int omfsOpId = model.OMFSOpId;
      if (model.BillingLabRadQueueDetails != null)
      {
        foreach (BillingQueueServiceViewModel labRadQueueDetail in model.BillingLabRadQueueDetails)
        {
          if (labRadQueueDetail != null && labRadQueueDetail.BillQueueId != 0 && labRadQueueDetail.IsApproved)
            _uow.Repository<BillQueueDetails>().GetEntitiesBySql(string.Format(Queries.BillQueueApprovalUpdate, "Y", user.Identity.Name, labRadQueueDetail.BillQueueId));
        }
      }
      return RedirectToAction("Approval", new RouteValueDictionary(new
      {
          controller = "OMFS",
          action = "Approval",
          allotId = allotId,
          CaserecordId = omfsOpId
      }));
    }

    [HttpPost]
    public ActionResult ReferralApproval(OMFSOPViewModel model)
    {
      CustomPrincipal user = User;
      long allotId = model.studentAllotmentViewModel.AllotId;
      int omfsOpId = model.OMFSOpId;
      if (model.ApprovedepartmentReferral != null)
      {
        foreach (ReferralStatusViewModel referralStatusViewModel in model.ApprovedepartmentReferral)
        {
          if (referralStatusViewModel != null && referralStatusViewModel.ReferredId != 0L && referralStatusViewModel.IsApproved)
            _uow.Repository<ReferralStatus>().GetEntitiesBySql(string.Format(Queries.ReferralApprovalUpdate, "Y", user.Identity.Name, referralStatusViewModel.ReferredId));
        }
      }
      return RedirectToAction("Approval", new RouteValueDictionary(new
      {
          controller = "OMFS",
          action = "Approval",
          allotId = allotId,
          CaserecordId = omfsOpId
      }));
    }

    [HttpPost]
    public ActionResult TreatmentApproval(OMFSOPViewModel model)
    {
      CustomPrincipal user = User;
      long allotId = model.studentAllotmentViewModel.AllotId;
      int omfsOpId = model.OMFSOpId;
      if (model.BillingQueueDetails != null)
      {
        foreach (BillingQueueServiceViewModel billingQueueDetail in model.BillingQueueDetails)
        {
          if (billingQueueDetail != null && billingQueueDetail.BillQueueId != 0 && billingQueueDetail.IsApproved)
            _uow.Repository<BillQueueDetails>().GetEntitiesBySql(string.Format(Queries.BillQueueApprovalUpdate, "Y", user.Identity.Name, billingQueueDetail.BillQueueId));
        }
      }
      return RedirectToAction("Approval", new RouteValueDictionary(new
      {
          controller = "OMFS",
          action = "Approval",
          allotId = allotId,
          CaserecordId = omfsOpId
      }));
    }

    [HttpPost]
    public JsonResult DeleteVitalItems(int VitalId)
    {
      _uow.Repository<OMFSIPDrugDetails>().GetEntitiesBySql(string.Format(Queries.DeleteVitalDetails, VitalId));
      return Json(true);
    }

    [HttpPost]
    public JsonResult DeleteDrugItems(int DrugDetId)
    {
      _uow.Repository<OMFSIPDrugDetails>().GetEntitiesBySql(string.Format(Queries.DeleteDrugDetails, DrugDetId));
      return Json(true);
    }

    [HttpPost]
    public JsonResult DeleteVitalItemsN(int VitalId)
    {
      _uow.Repository<OMFSIPDrugDetails>().GetEntitiesBySql(string.Format(Queries.DeleteVitalDetails, VitalId));
      return Json(true);
    }

    [HttpPost]
    public JsonResult DeleteDrugItemsN(int DrugDetId)
    {
      _uow.Repository<OMFSIPDrugDetails>().GetEntitiesBySql(string.Format(Queries.DeleteDrugDetails, DrugDetId));
      return Json(true);
    }

    public JsonResult DeleteConsumableItems(int ConsumableId)
    {
      _uow.Repository<OMFSIPOtConsumablesDetails>().GetEntitiesBySql(string.Format(Queries.DeleteConsumableDetails, ConsumableId));
      return Json(true);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult ApprovalQueue()
    {
      try
      {
        GetPermissionforUser();
        CustomPrincipal user = User;
        List<string> stringList = new List<string>();
        string permission = Convert.ToString(PermissionsEnum.ProcedureApproval);
        if (user.DeptPermission.Where<DeptPermissions>(x => x.DeptId.Equals(2)).Where<DeptPermissions>(x => x.PermissionName.Equals(permission)).ToList<DeptPermissions>().Count <= 0)
          return View("../Error/AccessDenied");
        return View("../OMFS/_OMFSApprovalQueue", new CasesheetApprovalListViewModel()
        {
            From_Date = DateTime.Now,
            To_Date = DateTime.Now,
            TreatmentTypeDetails = _uow.Repository<TreatmentTypes>().GetEntitiesBySql(string.Format(Queries.TreatmentsTypes, 2)).ToList<TreatmentTypes>()
        });
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
        {
            controller = "Error",
            action = "ErrorWrite",
            message = ex.ToString()
        }));
      }
    }

    public JsonResult GetOMFSApprovalList(CasesheetApprovalListViewModel model)
    {
      string From_Date = model.From_Date.ToString("yyyy-MM-dd");
      string To_Date = model.To_Date.ToString("yyyy-MM-dd");
      string rootUrl = User.GetRootUrl();
      model.SearchDetails = model.TreatmentId != 7 ? _service.omfsOpCasesheetApprovalList(From_Date, To_Date, rootUrl) : _service.omfsIpCasesheetApprovalList(From_Date, To_Date, rootUrl);
      return Json(model.SearchDetails);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Approval(long allotId, int CaserecordId)
    {
      try
      {
        GetPermissionforUser();
        if (!User.Departments.Contains(2))
          return View("../Error/AccessDenied");
        OMFSOPViewModel omfsopViewModel1 = new OMFSOPViewModel();
        OMFSOPViewModel omfsopViewModel2 = _service.BindEditOmfsOpModel(allotId, CaserecordId);
        omfsopViewModel2.FileUploadlist = _uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, omfsopViewModel2.PatientId, 2));
        omfsopViewModel2.approvalViewModal = new ApprovalViewModal()
        {
          CaserecordId = omfsopViewModel2.OMFSOpId,
          DeptId = 2,
          DoctorId = omfsopViewModel2.studentAllotmentViewModel.DoctorId,
          PatientId = omfsopViewModel2.PatientId,
          ReferredTreatmentId = 0
        };
        if (!omfsopViewModel2.Approval1)
          omfsopViewModel2.ApprovalType = 1;
        else if (omfsopViewModel2.Approval1 && !omfsopViewModel2.Approval2)
          omfsopViewModel2.ApprovalType = 2;
        else if (omfsopViewModel2.Approval1 && omfsopViewModel2.Approval2 && !omfsopViewModel2.Approval3)
          omfsopViewModel2.ApprovalType = 3;
        omfsopViewModel2.approvalViewModal.ApprovalTypeId = omfsopViewModel2.ApprovalType;
        return View("../OMFS/Approval", omfsopViewModel2);
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
        {
            controller = "Error",
            action = "ErrorWrite",
            message = ex.ToString()
        }));
      }
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult OpApproval(OMFSOPViewModel model)
    {
      try
      {
        CustomPrincipal user = User;
        if (!model.Approval1)
          model.ApprovalType = 1;
        else if (model.Approval1 && !model.Approval2)
          model.ApprovalType = 2;
        else if (model.Approval1 && model.Approval2 && !model.Approval3)
          model.ApprovalType = 3;
        model.CreatedBy = user.Identity.Name;
        _service.ProcedureApprovalOp(model);
        OMFSOPCasesheet entity = new OMFSOPCasesheet();
        if (3 == model.ApprovalType)
        {
          entity.SendForApproval1 = true;
          entity.SendForApproval2 = true;
          entity.SendForApproval3 = true;
          entity.Approval1 = true;
          entity.Approval2 = true;
          entity.Approval3 = true;
        }
        else if (2 == model.ApprovalType)
        {
          entity.SendForApproval1 = true;
          entity.SendForApproval2 = true;
          entity.Approval1 = true;
          entity.Approval2 = true;
          entity.Approval3 = false;
        }
        else
        {
          entity.SendForApproval1 = true;
          entity.Approval1 = true;
          entity.Approval2 = false;
          entity.Approval3 = false;
        }
        entity.OMFSOpDate = model.OMFSOpDate;
        entity.OMFSOpId = model.OMFSOpId;
        _uow.Repository<OMFSOPCasesheet>().Update(entity, false);
        TempData["Message"] = "Approved Successfully ";
        return RedirectToAction("ApprovalQueue", new RouteValueDictionary(new
        {
            controller = "OMFS",
            action = "ApprovalQueue"
        }));
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
        {
            controller = "Error",
            action = "ErrorWrite",
            message = ex.ToString()
        }));
      }
    }

    public ActionResult OPReport(int Id)
    {
      try
      {
        OMFSOPViewModel omfsopViewModel = new OMFSOPViewModel();
        return View("../Reports/OMFSOpCaserecordReport", _service.BindOMFSOPPatientReport(Id));
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
        {
            controller = "Error",
            action = "ErrorWrite",
            message = ex.ToString()
        }));
      }
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult DeptAppointmentSearch()
    {
        try
        {
            OPDFollowupSearchViewModel followupSearchViewModel = new OPDFollowupSearchViewModel();
            GetPermissionforUser();
            CustomPrincipal user = User;
            List<string> stringList = new List<string>();
            string permission = Convert.ToString(PermissionsEnum.AppReschedule);
            List<DeptPermissions> deptPermission = user.DeptPermission;
            followupSearchViewModel.AccessYN = deptPermission
                                                   .Where<DeptPermissions>(
                                                       x => x.DeptId.Equals(2))
                                                   .Where<DeptPermissions>(x =>
                                                       x.PermissionName.Equals(permission)).ToList<DeptPermissions>()
                                                   .Count > 0;
            followupSearchViewModel.From_Date = DateTime.Now;
            followupSearchViewModel.To_Date = DateTime.Now;
            followupSearchViewModel.DeptId = 2;
            followupSearchViewModel.DeptCode = Department.OMFS.ToString();
            followupSearchViewModel.DeptName = DeparmentNameDto.StringBusinessUnits(Department.OMFS);
            return View("../OPDRevisitRegistration/FollowupListByDept",
                followupSearchViewModel);
        }
        catch (Exception ex)
        {
            return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
            {
                controller = "Error",
                action = "ErrorWrite",
                message = ex.ToString()
            }));
        }
    }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpGet]
        public ActionResult CreateOpd()
        {
            try
            {
                GetPermissionforUser();
                if (!User.Departments.Contains(15))
                    return View("../Error/AccessDenied");
                return View(_opdService.BindPatientModel(
                    new OPDPatientRegistrationViewModel()
                    {
                        DepartmentId = (int)Department.OMFS,
                        RegDateDisplay = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt")
                    }));
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
                {
                    controller = "Error",
                    action = "ErrorWrite",
                    message = ex.ToString()
                }));
            }
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpPost]
        public ActionResult CreateOpd(OPDPatientRegistrationViewModel model)
        {
            CustomPrincipal user = User;
            if (!user.Departments.Contains(15))
                return View("../Error/AccessDenied");
            int num = 0;
            if (ModelState.IsValid)
            {
                model.CreatedBy = user.Identity.Name;
                model.PatientName = model.PatientName.ToUpper();
                num = _opdService.SavePatient(model);
            }
            else
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    using (IEnumerator<ModelError> enumerator = modelState.Errors.GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            ModelError current = enumerator.Current;
                            return RedirectToAction("ErrorWrite", new RouteValueDictionary(
                                new
                                {
                                    controller = "Error",
                                    action = "ErrorWrite",
                                    message = (current.ErrorMessage + "-" + (object)current.Exception)
                                }));
                        }
                    }
                }
            }

            if (num <= 0)
                return RedirectToAction(nameof(CreateOpd));
            TempData["Message"] = "Saved Successfully ";
            return RedirectToAction("EditOpd", new
            {
                Id = num
            });
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpGet]
        public ActionResult EditOpd(int Id)
        {
            try
            {
                GetPermissionforUser();
                if (!User.Departments.Contains(15))
                    return View("../Error/AccessDenied");
                OPDPatientRegistrationViewModel registrationViewModel = new OPDPatientRegistrationViewModel();
                return View(nameof(EditOpd), _opdService.BindEditPatientModel(Id));
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
                {
                    controller = "Error",
                    action = "ErrorWrite",
                    message = ex.ToString()
                }));
            }
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpPost]
        public ActionResult EditOpd(OPDPatientRegistrationViewModel model)
        {
            CustomPrincipal user = User;
            if (!user.Departments.Contains(15))
                return View("../Error/AccessDenied");
            if (ModelState.IsValid)
            {

                model.ModifiedBy = user.Identity.Name;
                model.PatientName = model.PatientName.ToUpper();
                _opdService.UpdatePatient(model);
            }
            else
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    using (IEnumerator<ModelError> enumerator = modelState.Errors.GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            ModelError current = enumerator.Current;
                            return RedirectToAction("ErrorWrite", new RouteValueDictionary(
                                new
                                {
                                    controller = "Error",
                                    action = "ErrorWrite",
                                    message = (current.ErrorMessage + "-" + (object)current.Exception)
                                }));
                        }
                    }
                }
            }

            TempData["Message"] = "Updated Successfully ";
            return RedirectToAction(nameof(EditOpd), new
            {
                Id = model.PatientId
            });
        }
    }
}
