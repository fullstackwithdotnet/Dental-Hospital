// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.PERIOController
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
    public class PERIOController : BaseController
    {
        private IUnitOfWork _uow;
        private IPERIOCasesheetService _service;
        private IOPDPatientRegistrationService _opdService;

        public PERIOController(IUnitOfWork uow, IPERIOCasesheetService service, IUserService userservice,
            IOPDPatientRegistrationService opdService)
            : base(uow, userservice)
        {
            this._uow = uow;
            this._opdService = opdService;
            this._service = service;
            this.TempData["Message"] = (object) null;
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        public ActionResult Index()
        {
            try
            {
                this.GetPermissionforUser();
                CustomPrincipal user = this.User;
                List<string> stringList = new List<string>();
                string permission = Convert.ToString((object) PermissionsEnum.Allotment);
                if (user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(3)))
                        .Where<DeptPermissions>(
                            (Func<DeptPermissions, bool>) (x => x.PermissionName.Equals(permission)))
                        .ToList<DeptPermissions>().Count <= 0)
                    return (ActionResult) this.View("../Error/AccessDenied");
                return (ActionResult) this.View("../PERIO/Index", (object) new DeptHomeViewModel()
                {
                    From_Date = DateTime.Now,
                    To_Date = DateTime.Now,
                    DeptId = 3,
                    ControllerName = "PERIO"
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
        public ActionResult TreatmentIndex()
        {
            try
            {
                this.GetPermissionforUser();
                DeptHomeViewModel deptHomeViewModel = new DeptHomeViewModel();
                CustomPrincipal user = this.User;
                List<string> stringList = new List<string>();
                string permission = Convert.ToString((object) PermissionsEnum.CancelAllotment);
                List<DeptPermissions> deptPermission = user.DeptPermission;
                deptHomeViewModel.AccessYNo = deptPermission
                                                  .Where<DeptPermissions
                                                  >((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(3)))
                                                  .Where<DeptPermissions>((Func<DeptPermissions, bool>) (x =>
                                                      x.PermissionName.Equals(permission))).ToList<DeptPermissions>()
                                                  .Count > 0;
                if (!user.Departments.Contains(3))
                    return (ActionResult) this.View("../Error/AccessDenied");
                deptHomeViewModel.From_Date = DateTime.Now;
                deptHomeViewModel.To_Date = DateTime.Now;
                deptHomeViewModel.DeptId = 3;
                deptHomeViewModel.ControllerName = "PERIO";
                return (ActionResult) this.View("../PERIO/TreatmentIndex", (object) deptHomeViewModel);
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
        public ActionResult Operations(long allotId, int patientId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(3))
                    return (ActionResult) this.View("../Error/AccessDenied");
                PERIOViewModel perioViewModel = new PERIOViewModel();
                return (ActionResult) this.View("../PERIO/PerioPatientTreatmentList",
                    (object) this._service.BindTreatmentList(allotId));
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
        public ActionResult Create(long allotId, int patientId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(3))
                    return (ActionResult) this.View("../Error/AccessDenied");
                PERIOViewModel perioViewModel = new PERIOViewModel();
                return (ActionResult) this.View("../PERIO/Create",
                    (object) this._service.BindPerioPatientModel(allotId));
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
        public ActionResult Create(PERIOViewModel model)
        {
            try
            {
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(3))
                    return (ActionResult) this.View("../Error/AccessDenied");
                int num = 0;
                if (this.ModelState.IsValid)
                {
                    model.CreatedBy = user.Identity.Name;
                    num = this._service.SavePERIOPatient(model);
                    model.PerioId = num;
                    if (!model.Approval1)
                        model.ApprovalType = 1;
                    this._service.UpdateAllotment(model);
                    this._service.UpdateReferralStatus(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.SavefollowUp(model);
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
                                return (ActionResult) this.RedirectToAction("ErrorWrite", new RouteValueDictionary(
                                    (object) new
                                    {
                                        controller = "Error",
                                        action = "ErrorWrite",
                                        message = (current.ErrorMessage + "-" + (object) current.Exception)
                                    }));
                            }
                        }
                    }
                }

                return (ActionResult) this.RedirectToAction("EditOnly", new RouteValueDictionary((object) new
                {
                    controller = "PERIO",
                    action = "EditOnly",
                    allotId = model.studentAllotmentViewModel.AllotId,
                    PerioId = num
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
        [HttpGet]
        public ActionResult Edit(long allotId, int PerioId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(3))
                    return (ActionResult) this.View("../Error/AccessDenied");
                PERIOViewModel perioViewModel = new PERIOViewModel();
                return (ActionResult) this.View("../PERIO/Edit",
                    (object) this._service.BindEditPERIOPatientModel(allotId, PerioId));
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
        public ActionResult Edit(PERIOViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int perioId = model.PerioId;
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(3))
                    return (ActionResult) this.View("../Error/AccessDenied");
                if (this.ModelState.IsValid)
                {
                    model.ModifiedBy = user.Identity.Name;
                    this._service.UpdatePERIOPatient(model);
                    this._service.UpdateAllotment(model);
                    this._service.UpdateReferralStatus(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.SavefollowUp(model);
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
                                return (ActionResult) this.RedirectToAction("ErrorWrite", new RouteValueDictionary(
                                    (object) new
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
                return (ActionResult) this.RedirectToAction(nameof(Edit), new RouteValueDictionary((object) new
                {
                    controller = "PERIO",
                    action = nameof(Edit),
                    allotId = allotId,
                    PerioId = perioId
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
        [HttpGet]
        public ActionResult EditOnly(long allotId, int PerioId)
        {
            try
            {
                this.GetPermissionforUser();
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(3))
                    return (ActionResult) this.View("../Error/AccessDenied");
                PERIOViewModel perioViewModel1 = new PERIOViewModel();
                PERIOViewModel perioViewModel2 = this._service.BindEditPERIOPatientModel(allotId, PerioId);
                IEnumerable<DeptPermissions> source =
                    user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(3)));
                perioViewModel2.EditCaseSheetAccess =
                    source.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x =>
                            x.PermissionName.Equals(Convert.ToString((object) PermissionsEnum.EditFreezeCase))))
                        .ToList<DeptPermissions>().Count > 0;
                return (ActionResult) this.View("../PERIO/Edit", (object) perioViewModel2);
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
        public ActionResult EditOnly(PERIOViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int perioId = model.PerioId;
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(3))
                    return (ActionResult) this.View("../Error/AccessDenied");
                if (this.ModelState.IsValid)
                {
                    model.ModifiedBy = user.Identity.Name;
                    this._service.UpdatePERIOPatient(model);
                    this._service.UpdateAllotment(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.SavefollowUp(model);
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
                                return (ActionResult) this.RedirectToAction("ErrorWrite", new RouteValueDictionary(
                                    (object) new
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
                return (ActionResult) this.RedirectToAction("Edit", new RouteValueDictionary((object) new
                {
                    controller = "PERIO",
                    action = nameof(EditOnly),
                    allotId = allotId,
                    PerioId = perioId
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
        public ActionResult Allotment(int PatientId = 0, long ReferredId = 0, int CourseType = 0)
        {
            StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
            return (ActionResult) this.PartialView("../StudentAllotment/_StudentAllotment",
                (object) this._service.DisplayAllotment(PatientId, ReferredId, CourseType));
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff,Student")]
        public ActionResult Search()
        {
            try
            {
                this.GetPermissionforUser();
                CustomPrincipal user = this.User;
                List<string> stringList = new List<string>();
                if (user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(3)))
                        .ToList<DeptPermissions>().Count <= 0)
                    return (ActionResult) this.View("../Error/AccessDenied");
                return (ActionResult) this.View("../PERIO/_PerioSearchOptions", (object) new PERIOSearchViewModel()
                {
                    From_Date = DateTime.Now,
                    To_Date = DateTime.Now
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

        public JsonResult GetPERIOSearchList(PERIOSearchViewModel model)
        {
            string From_Date = model.From_Date.ToString("yyyy-MM-dd");
            string To_Date = model.To_Date.ToString("yyyy-MM-dd");
            string rootUrl = this.User.GetRootUrl();
            model.SearchDetails = this._service.perioSearchDetails(From_Date, To_Date, rootUrl);
            return this.Json((object) model.SearchDetails);
        }

        public ActionResult Report(int Id)
        {
            try
            {
                PERIOViewModel perioViewModel = new PERIOViewModel();
                return (ActionResult) this.View("../Reports/PerioCaserecordReport",
                    (object) this._service.BindPERIOPatientReport(Id));
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

        [CustomAuthorize(Roles = "Admin, HOD, Staff")]
        public ActionResult ApprovalQueue()
        {
            this.GetPermissionforUser();
            CustomPrincipal user = this.User;
            List<string> stringList = new List<string>();
            string permission = Convert.ToString((object) PermissionsEnum.ProcedureApproval);
            if (user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(3)))
                    .Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.PermissionName.Equals(permission)))
                    .ToList<DeptPermissions>().Count <= 0)
                return (ActionResult) this.View("../Error/AccessDenied");
            return (ActionResult) this.View("../PERIO/_PerioApprovalQueue",
                (object) new CasesheetApprovalListViewModel()
                {
                    From_Date = DateTime.Now,
                    To_Date = DateTime.Now
                });
        }

        public JsonResult GetPERIOApprovalList(CasesheetApprovalListViewModel model)
        {
            string From_Date = model.From_Date.ToString("yyyy-MM-dd");
            string To_Date = model.To_Date.ToString("yyyy-MM-dd");
            string rootUrl = this.User.GetRootUrl();
            model.SearchDetails = this._service.perioCasesheetApprovalList(From_Date, To_Date, rootUrl);
            return this.Json((object) model.SearchDetails);
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpGet]
        public ActionResult Approval(long allotId, int CaserecordId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(3))
                    return (ActionResult) this.View("../Error/AccessDenied");
                PERIOViewModel perioViewModel1 = new PERIOViewModel();
                PERIOViewModel perioViewModel2 = this._service.BindEditPERIOPatientModel(allotId, CaserecordId);
                perioViewModel2.FileUploadlist = this._uow.Repository<FileUploadViewModel>()
                    .GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) perioViewModel2.PatientId,
                        (object) 3));
                perioViewModel2.approvalViewModal = new ApprovalViewModal()
                {
                    CaserecordId = perioViewModel2.PerioId,
                    DeptId = 3,
                    DoctorId = perioViewModel2.studentAllotmentViewModel.DoctorId,
                    PatientId = (long) perioViewModel2.PatientId,
                    ReferredTreatmentId = 0
                };
                if (!perioViewModel2.Approval1)
                    perioViewModel2.ApprovalType = 1;
                else if (perioViewModel2.Approval1 && !perioViewModel2.Approval2)
                    perioViewModel2.ApprovalType = 2;
                else if (perioViewModel2.Approval1 && perioViewModel2.Approval2 && !perioViewModel2.Approval3)
                    perioViewModel2.ApprovalType = 3;
                perioViewModel2.approvalViewModal.ApprovalTypeId = perioViewModel2.ApprovalType;
                return (ActionResult) this.View("../PERIO/Approval", (object) perioViewModel2);
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
        public ActionResult Approval(PERIOViewModel model)
        {
            try
            {
                CustomPrincipal user = this.User;
                if (!model.Approval1)
                    model.ApprovalType = 1;
                else if (model.Approval1 && !model.Approval2)
                    model.ApprovalType = 2;
                else if (model.Approval1 && model.Approval2 && !model.Approval3)
                    model.ApprovalType = 3;
                model.CreatedBy = user.Identity.Name;
                this._service.ProcedureApproval(model);
                PERIOCasesheet entity = new PERIOCasesheet();
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

                entity.PerioDate = model.PerioDate;
                entity.PerioId = model.PerioId;
                this._uow.Repository<PERIOCasesheet>().Update(entity, false);
                this.TempData["Message"] = (object) "Approved Successfully ";
                return (ActionResult) this.RedirectToAction("ApprovalQueue", new RouteValueDictionary((object) new
                {
                    controller = "PERIO",
                    action = "ApprovalQueue"
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
        public ActionResult DeptAppointmentSearch()
        {
            try
            {
                OPDFollowupSearchViewModel followupSearchViewModel = new OPDFollowupSearchViewModel();
                this.GetPermissionforUser();
                CustomPrincipal user = this.User;
                List<string> stringList = new List<string>();
                string permission = Convert.ToString((object) PermissionsEnum.AppReschedule);
                List<DeptPermissions> deptPermission = user.DeptPermission;
                followupSearchViewModel.AccessYN = deptPermission
                                                       .Where<DeptPermissions>(
                                                           (Func<DeptPermissions, bool>) (x => x.DeptId.Equals(3)))
                                                       .Where<DeptPermissions>((Func<DeptPermissions, bool>) (x =>
                                                           x.PermissionName.Equals(permission)))
                                                       .ToList<DeptPermissions>().Count > 0;
                followupSearchViewModel.From_Date = DateTime.Now;
                followupSearchViewModel.To_Date = DateTime.Now;
                followupSearchViewModel.DeptId = 3;
                followupSearchViewModel.DeptCode = Department.PERIO.ToString();
                followupSearchViewModel.DeptName = DeparmentNameDto.StringBusinessUnits(Department.PERIO);
                return (ActionResult) this.View("../OPDRevisitRegistration/FollowupListByDept",
                    (object) followupSearchViewModel);
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
        public ActionResult UpdateSendForApproval(PERIOViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int perioId = model.PerioId;
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(3))
                    return (ActionResult) this.View("../Error/AccessDenied");
                if (this.ModelState.IsValid)
                {
                    if (!model.SendForApproval1)
                        model.SendForApproval1 = true;
                    else if (!model.SendForApproval2)
                        model.SendForApproval2 = true;
                    else if (!model.SendForApproval3)
                        model.SendForApproval3 = true;
                    model.ModifiedBy = user.Identity.Name;
                    this._service.UpdatePERIOPatient(model);
                    this._service.UpdateAllotment(model);
                    this._service.SendApproval(allotId);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.SavefollowUp(model);
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
                                return (ActionResult) this.RedirectToAction("ErrorWrite", new RouteValueDictionary(
                                    (object) new
                                    {
                                        controller = "Error",
                                        action = "ErrorWrite",
                                        message = (current.ErrorMessage + "-" + (object) current.Exception)
                                    }));
                            }
                        }
                    }
                }

                this.TempData["Message"] = (object) "Updated Successfully & Sent for Approval";
                return (ActionResult) this.RedirectToAction("EditOnly", new RouteValueDictionary((object) new
                {
                    controller = "PERIO",
                    action = "EditOnly",
                    allotId = allotId,
                    PerioId = perioId
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

        [HttpPost]
        public ActionResult InvestigationApproval(PERIOViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int perioId = model.PerioId;
            if (model.BillingLabRadQueueDetails != null)
            {
                foreach (BillingQueueServiceViewModel labRadQueueDetail in model.BillingLabRadQueueDetails)
                {
                    if (labRadQueueDetail != null && labRadQueueDetail.BillQueueId != 0 && labRadQueueDetail.IsApproved)
                        this._uow.Repository<BillQueueDetails>().GetEntitiesBySql(
                            string.Format(Queries.BillQueueApprovalUpdate, (object) "Y", (object) user.Identity.Name,
                                (object) labRadQueueDetail.BillQueueId));
                }
            }

            return (ActionResult) this.RedirectToAction("Approval", new RouteValueDictionary((object) new
            {
                controller = "PERIO",
                action = "Approval",
                allotId = allotId,
                CaserecordId = perioId
            }));
        }

        [HttpPost]
        public ActionResult ReferralApproval(PERIOViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int perioId = model.PerioId;
            if (model.ApprovedepartmentReferral != null)
            {
                foreach (ReferralStatusViewModel referralStatusViewModel in model.ApprovedepartmentReferral)
                {
                    if (referralStatusViewModel != null && referralStatusViewModel.ReferredId != 0L &&
                        referralStatusViewModel.IsApproved)
                        this._uow.Repository<ReferralStatus>().GetEntitiesBySql(
                            string.Format(Queries.ReferralApprovalUpdate, (object) "Y", (object) user.Identity.Name,
                                (object) referralStatusViewModel.ReferredId));
                }
            }

            return (ActionResult) this.RedirectToAction("Approval", new RouteValueDictionary((object) new
            {
                controller = "PERIO",
                action = "Approval",
                allotId = allotId,
                CaserecordId = perioId
            }));
        }

        [HttpPost]
        public ActionResult TreatmentApproval(PERIOViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int perioId = model.PerioId;
            if (model.BillingQueueDetails != null)
            {
                foreach (BillingQueueServiceViewModel billingQueueDetail in model.BillingQueueDetails)
                {
                    if (billingQueueDetail != null && billingQueueDetail.BillQueueId != 0 &&
                        billingQueueDetail.IsApproved)
                        this._uow.Repository<BillQueueDetails>().GetEntitiesBySql(
                            string.Format(Queries.BillQueueApprovalUpdate, (object) "Y", (object) user.Identity.Name,
                                (object) billingQueueDetail.BillQueueId));
                }
            }

            return (ActionResult) this.RedirectToAction("Approval", new RouteValueDictionary((object) new
            {
                controller = "PERIO",
                action = "Approval",
                allotId = allotId,
                CaserecordId = perioId
            }));
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpGet]
        public ActionResult CreateOpd()
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(15))
                    return (ActionResult)this.View("../Error/AccessDenied");
                return (ActionResult)this.View((object)this._opdService.BindPatientModel(
                    new OPDPatientRegistrationViewModel()
                    {
                        DepartmentId = (int)Department.PERIO,
                        RegDateDisplay = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt")
                    }));
            }
            catch (Exception ex)
            {
                return (ActionResult)this.RedirectToAction("ErrorWrite", new RouteValueDictionary((object)new
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
            CustomPrincipal user = this.User;
            if (!user.Departments.Contains(15))
                return (ActionResult)this.View("../Error/AccessDenied");
            int num = 0;
            if (this.ModelState.IsValid)
            {
                model.CreatedBy = user.Identity.Name;
                model.PatientName = model.PatientName.ToUpper();
                num = this._opdService.SavePatient(model);
            }
            else
            {
                foreach (ModelState modelState in (IEnumerable<ModelState>)this.ViewData.ModelState.Values)
                {
                    using (IEnumerator<ModelError> enumerator = modelState.Errors.GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            ModelError current = enumerator.Current;
                            return (ActionResult)this.RedirectToAction("ErrorWrite", new RouteValueDictionary(
                                (object)new
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
                return (ActionResult)this.RedirectToAction(nameof(CreateOpd));
            this.TempData["Message"] = (object)"Saved Successfully ";
            return (ActionResult)this.RedirectToAction("EditOpd", (object)new
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
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(15))
                    return (ActionResult)this.View("../Error/AccessDenied");
                OPDPatientRegistrationViewModel registrationViewModel = new OPDPatientRegistrationViewModel();
                return (ActionResult)this.View(nameof(EditOpd), (object)this._opdService.BindEditPatientModel(Id));
            }
            catch (Exception ex)
            {
                return (ActionResult)this.RedirectToAction("ErrorWrite", new RouteValueDictionary((object)new
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
            CustomPrincipal user = this.User;
            if (!user.Departments.Contains(15))
                return (ActionResult)this.View("../Error/AccessDenied");
            if (this.ModelState.IsValid)
            {

                model.ModifiedBy = user.Identity.Name;
                model.PatientName = model.PatientName.ToUpper();
                this._opdService.UpdatePatient(model);
            }
            else
            {
                foreach (ModelState modelState in (IEnumerable<ModelState>)this.ViewData.ModelState.Values)
                {
                    using (IEnumerator<ModelError> enumerator = modelState.Errors.GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            ModelError current = enumerator.Current;
                            return (ActionResult)this.RedirectToAction("ErrorWrite", new RouteValueDictionary(
                                (object)new
                                {
                                    controller = "Error",
                                    action = "ErrorWrite",
                                    message = (current.ErrorMessage + "-" + (object)current.Exception)
                                }));
                        }
                    }
                }
            }

            this.TempData["Message"] = (object)"Updated Successfully ";
            return (ActionResult)this.RedirectToAction(nameof(EditOpd), (object)new
            {
                Id = model.PatientId
            });
        }
    }
}
