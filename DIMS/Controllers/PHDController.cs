// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.PHDController
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
    public class PHDController : BaseController
    {
        private IUnitOfWork _uow;
        private IPHDCasesheetService _service;
        private IOPDPatientRegistrationService _opdService;

        public PHDController(IUnitOfWork uow, IPHDCasesheetService service, IUserService userservice,
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
                if (user.DeptPermission.Where<DeptPermissions>(x => x.DeptId.Equals(8))
                        .Where<DeptPermissions>(
                            x => x.PermissionName.Equals(permission))
                        .ToList<DeptPermissions>().Count <= 0)
                    return View("../Error/AccessDenied");
                return View("../PHD/Index", new DeptHomeViewModel()
                {
                    From_Date = DateTime.Now,
                    To_Date = DateTime.Now,
                    DeptId = 8,
                    ControllerName = "PHD"
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
        public ActionResult TreatmentIndex()
        {
            try
            {
                GetPermissionforUser();
                DeptHomeViewModel deptHomeViewModel = new DeptHomeViewModel();
                CustomPrincipal user = User;
                List<string> stringList = new List<string>();
                string permission = Convert.ToString(PermissionsEnum.CancelAllotment);
                List<DeptPermissions> deptPermission = user.DeptPermission;
                deptHomeViewModel.AccessYNo = deptPermission
                                                  .Where<DeptPermissions
                                                  >(x => x.DeptId.Equals(8))
                                                  .Where<DeptPermissions>(x =>
                                                      x.PermissionName.Equals(permission)).ToList<DeptPermissions>()
                                                  .Count > 0;
                if (!user.Departments.Contains(8))
                    return View("../Error/AccessDenied");
                deptHomeViewModel.From_Date = DateTime.Now;
                deptHomeViewModel.To_Date = DateTime.Now;
                deptHomeViewModel.DeptId = 8;
                deptHomeViewModel.ControllerName = "PHD";
                return View("../PHD/TreatmentIndex", deptHomeViewModel);
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
        public ActionResult Operations(long allotId, int patientId)
        {
            try
            {
                GetPermissionforUser();
                if (!User.Departments.Contains(8))
                    return View("../Error/AccessDenied");
                PHDViewModel phdViewModel = new PHDViewModel();
                return View("../PHD/PHDPatientTreatmentList",
                    _service.BindTreatmentList(allotId));
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
        public ActionResult Create(long allotId, int patientId)
        {
            try
            {
                GetPermissionforUser();
                if (!User.Departments.Contains(8))
                    return View("../Error/AccessDenied");
                PHDViewModel phdViewModel = new PHDViewModel();
                return View("../PHD/Create", _service.BindPHDPatientModel(allotId));
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
        public ActionResult Create(PHDViewModel model)
        {
            try
            {
                CustomPrincipal user = User;
                if (!user.Departments.Contains(8))
                    return View("../Error/AccessDenied");
                int num = 0;
                if (ModelState.IsValid)
                {
                    model.CreatedBy = user.Identity.Name;
                    num = _service.SavePHDPatient(model);
                    model.PHDId = num;
                    if (!model.Approval1)
                        model.ApprovalType = 1;
                    _service.UpdateAllotment(model);
                    _service.UpdateReferralStatus(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        _service.SavefollowUp(model);
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
                                        message = (current.ErrorMessage + "-" + (object) current.Exception)
                                    }));
                            }
                        }
                    }
                }

                return RedirectToAction("EditOnly", new RouteValueDictionary(new
                {
                    controller = "PHD",
                    action = "EditOnly",
                    allotId = model.studentAllotmentViewModel.AllotId,
                    PHDId = num
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
        public ActionResult Edit(long allotId, int PHDId)
        {
            try
            {
                GetPermissionforUser();
                if (!User.Departments.Contains(8))
                    return View("../Error/AccessDenied");
                PHDViewModel phdViewModel = new PHDViewModel();
                return View("../PHD/Edit",
                    _service.BindEditPHDPatientModel(allotId, PHDId));
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
        public ActionResult Edit(PHDViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int phdId = model.PHDId;
                if (!User.Departments.Contains(8))
                    return View("../Error/AccessDenied");
                if (ModelState.IsValid)
                {
                    _service.UpdatePHDPatient(model);
                    _service.UpdateAllotment(model);
                    _service.UpdateReferralStatus(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        _service.SavefollowUp(model);
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
                                        message = (current.ErrorMessage + "-" + (object) current.Exception)
                                    }));
                            }
                        }
                    }
                }

                TempData["Message"] = "Updated Successfully ";
                return RedirectToAction(nameof(Edit), new RouteValueDictionary(new
                {
                    controller = "PHD",
                    action = nameof(Edit),
                    allotId = allotId,
                    PHDId = phdId
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
        public ActionResult EditOnly(long allotId, int PHDId)
        {
            try
            {
                GetPermissionforUser();
                CustomPrincipal user = User;
                if (!user.Departments.Contains(8))
                    return View("../Error/AccessDenied");
                PHDViewModel phdViewModel1 = new PHDViewModel();
                PHDViewModel phdViewModel2 = _service.BindEditPHDPatientModel(allotId, PHDId);
                IEnumerable<DeptPermissions> source =
                    user.DeptPermission.Where<DeptPermissions>(x => x.DeptId.Equals(8));
                phdViewModel2.EditCaseSheetAccess =
                    source.Where<DeptPermissions>(x =>
                            x.PermissionName.Equals(Convert.ToString(PermissionsEnum.EditFreezeCase)))
                        .ToList<DeptPermissions>().Count > 0;
                return View("../PHD/Edit", phdViewModel2);
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
        public ActionResult EditOnly(PHDViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int phdId = model.PHDId;
                CustomPrincipal user = User;
                if (!user.Departments.Contains(8))
                    return View("../Error/AccessDenied");
                if (ModelState.IsValid)
                {
                    model.ModifiedBy = user.Identity.Name;
                    _service.UpdatePHDPatient(model);
                    _service.UpdateAllotment(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        _service.SavefollowUp(model);
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
                                        message = (current.ErrorMessage + "-" + (object) current.Exception)
                                    }));
                            }
                        }
                    }
                }

                TempData["Message"] = "Updated Successfully ";
                return RedirectToAction("Edit", new RouteValueDictionary(new
                {
                    controller = "PHD",
                    action = nameof(EditOnly),
                    allotId = allotId,
                    PHDId = phdId
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

        public ActionResult Allotment(int PatientId = 0, long ReferredId = 0, int CourseType = 0)
        {
            StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
            return PartialView("../StudentAllotment/_StudentAllotment",
                _service.DisplayAllotment(PatientId, ReferredId, CourseType));
        }

        [CustomAuthorize(Roles = "Admin, HOD,Staff, Student")]
        [HttpGet]
        public ActionResult Search()
        {
            try
            {
                GetPermissionforUser();
                CustomPrincipal user = User;
                List<string> stringList = new List<string>();
                Convert.ToString(PermissionsEnum.Allotment);
                if (user.DeptPermission.Where<DeptPermissions>(x => x.DeptId.Equals(8))
                        .ToList<DeptPermissions>().Count <= 0)
                    return View("../Error/AccessDenied");
                return View("../PHD/_PHDSearchOptions", new PHDSearchViewModel()
                {
                    From_Date = DateTime.Now,
                    To_Date = DateTime.Now
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

        public JsonResult GetPHDSearchList(PHDSearchViewModel model)
        {
            string From_Date = model.From_Date.ToString("yyyy-MM-dd");
            string To_Date = model.To_Date.ToString("yyyy-MM-dd");
            string rootUrl = User.GetRootUrl();
            model.SearchDetails = _service.phdSearchDetails(From_Date, To_Date, rootUrl);
            return Json(model.SearchDetails);
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
                if (user.DeptPermission.Where<DeptPermissions>(x => x.DeptId.Equals(8))
                        .Where<DeptPermissions>(
                            x => x.PermissionName.Equals(permission))
                        .ToList<DeptPermissions>().Count <= 0)
                    return View("../Error/AccessDenied");
                return View("../PHD/_PHDApprovalQueue",
                    new CasesheetApprovalListViewModel()
                    {
                        From_Date = DateTime.Now,
                        To_Date = DateTime.Now
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

        public JsonResult GetPHDApprovalList(CasesheetApprovalListViewModel model)
        {
            string From_Date = model.From_Date.ToString("yyyy-MM-dd");
            string To_Date = model.To_Date.ToString("yyyy-MM-dd");
            string rootUrl = User.GetRootUrl();
            model.SearchDetails = _service.phdCasesheetApprovalList(From_Date, To_Date, rootUrl);
            return Json(model.SearchDetails);
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpGet]
        public ActionResult Approval(long allotId, int CaserecordId)
        {
            try
            {
                GetPermissionforUser();
                if (!User.Departments.Contains(8))
                    return View("../Error/AccessDenied");
                PHDViewModel phdViewModel1 = new PHDViewModel();
                PHDViewModel phdViewModel2 = _service.BindEditPHDPatientModel(allotId, CaserecordId);
                phdViewModel2.FileUploadlist = _uow.Repository<FileUploadViewModel>()
                    .GetEntitiesBySql(string.Format(Queries.FileUploadList, phdViewModel2.PatientId,
                        8));
                phdViewModel2.approvalViewModal = new ApprovalViewModal()
                {
                    CaserecordId = phdViewModel2.PHDId,
                    DeptId = 8,
                    DoctorId = phdViewModel2.studentAllotmentViewModel.DoctorId,
                    PatientId = phdViewModel2.PatientId,
                    ReferredTreatmentId = 0
                };
                if (!phdViewModel2.Approval1)
                    phdViewModel2.ApprovalType = 1;
                else if (phdViewModel2.Approval1 && !phdViewModel2.Approval2)
                    phdViewModel2.ApprovalType = 2;
                else if (phdViewModel2.Approval1 && phdViewModel2.Approval2 && !phdViewModel2.Approval3)
                    phdViewModel2.ApprovalType = 3;
                phdViewModel2.approvalViewModal.ApprovalTypeId = phdViewModel2.ApprovalType;
                return View("../PHD/Approval", phdViewModel2);
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
        public ActionResult Approval(PHDViewModel model)
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
                _service.ProcedureApproval(model);
                PHDCasesheet entity = new PHDCasesheet();
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

                entity.PHDDate = model.PHDDate;
                entity.PHDId = model.PHDId;
                _uow.Repository<PHDCasesheet>().Update(entity, false);
                TempData["Message"] = "Approved Successfully ";
                return RedirectToAction("ApprovalQueue", new RouteValueDictionary(new
                {
                    controller = "PHD",
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

        public ActionResult Report(int Id)
        {
            try
            {
                PHDViewModel phdViewModel = new PHDViewModel();
                return View("../Reports/PHDCaserecordReport",
                    _service.BindPHDPatientReport(Id));
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
                                                           x => x.DeptId.Equals(8))
                                                       .Where<DeptPermissions>(x =>
                                                           x.PermissionName.Equals(permission))
                                                       .ToList<DeptPermissions>().Count > 0;
                followupSearchViewModel.From_Date = DateTime.Now;
                followupSearchViewModel.To_Date = DateTime.Now;
                followupSearchViewModel.DeptId = 8;
                followupSearchViewModel.DeptCode = Department.PHD.ToString();
                followupSearchViewModel.DeptName = DeparmentNameDto.StringBusinessUnits(Department.PHD);
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
        [HttpPost]
        public ActionResult UpdateSendForApproval(PHDViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int phdId = model.PHDId;
                CustomPrincipal user = User;
                if (!user.Departments.Contains(8))
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
                    _service.UpdatePHDPatient(model);
                    _service.UpdateAllotment(model);
                    _service.SendApproval(allotId);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        _service.SavefollowUp(model);
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
                                        message = (current.ErrorMessage + "-" + (object) current.Exception)
                                    }));
                            }
                        }
                    }
                }

                TempData["Message"] = "Updated Successfully & Sent for Approval";
                return RedirectToAction("EditOnly", new RouteValueDictionary(new
                {
                    controller = "PHD",
                    action = "EditOnly",
                    allotId = allotId,
                    PHDId = phdId
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
        public ActionResult InvestigationApproval(PHDViewModel model)
        {
            CustomPrincipal user = User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int phdId = model.PHDId;
            if (model.BillingLabRadQueueDetails != null)
            {
                foreach (BillingQueueServiceViewModel labRadQueueDetail in model.BillingLabRadQueueDetails)
                {
                    if (labRadQueueDetail != null && labRadQueueDetail.BillQueueId != 0 && labRadQueueDetail.IsApproved)
                        _uow.Repository<BillQueueDetails>().GetEntitiesBySql(
                            string.Format(Queries.BillQueueApprovalUpdate, "Y", user.Identity.Name,
                                labRadQueueDetail.BillQueueId));
                }
            }

            return RedirectToAction("Approval", new RouteValueDictionary(new
            {
                controller = "PHD",
                action = "Approval",
                allotId = allotId,
                CaserecordId = phdId
            }));
        }

        [HttpPost]
        public ActionResult ReferralApproval(PHDViewModel model)
        {
            CustomPrincipal user = User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int phdId = model.PHDId;
            if (model.ApprovedepartmentReferral != null)
            {
                foreach (ReferralStatusViewModel referralStatusViewModel in model.ApprovedepartmentReferral)
                {
                    if (referralStatusViewModel != null && referralStatusViewModel.ReferredId != 0L &&
                        referralStatusViewModel.IsApproved)
                        _uow.Repository<ReferralStatus>().GetEntitiesBySql(
                            string.Format(Queries.ReferralApprovalUpdate, "Y", user.Identity.Name,
                                referralStatusViewModel.ReferredId));
                }
            }

            return RedirectToAction("Approval", new RouteValueDictionary(new
            {
                controller = "PHD",
                action = "Approval",
                allotId = allotId,
                CaserecordId = phdId
            }));
        }

        [HttpPost]
        public ActionResult TreatmentApproval(PHDViewModel model)
        {
            CustomPrincipal user = User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int phdId = model.PHDId;
            if (model.BillingQueueDetails != null)
            {
                foreach (BillingQueueServiceViewModel billingQueueDetail in model.BillingQueueDetails)
                {
                    if (billingQueueDetail != null && billingQueueDetail.BillQueueId != 0 &&
                        billingQueueDetail.IsApproved)
                        _uow.Repository<BillQueueDetails>().GetEntitiesBySql(
                            string.Format(Queries.BillQueueApprovalUpdate, "Y", user.Identity.Name,
                                billingQueueDetail.BillQueueId));
                }
            }

            return RedirectToAction("Approval", new RouteValueDictionary(new
            {
                controller = "PHD",
                action = "Approval",
                allotId = allotId,
                CaserecordId = phdId
            }));
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
                        DepartmentId = (int) Department.PHD,
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
                                    message = (current.ErrorMessage + "-" + (object) current.Exception)
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
                                    message = (current.ErrorMessage + "-" + (object) current.Exception)
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
