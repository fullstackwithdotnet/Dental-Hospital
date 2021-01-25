// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.CONSController
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
    public class CONSController : BaseController
    {
        private IUnitOfWork _uow;
        private ICONSCasesheetService _service;
        private IOPDPatientRegistrationService _opdService;


        public CONSController(IUnitOfWork uow, ICONSCasesheetService service, IUserService userservice,
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
                if (user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(4)))
                        .Where<DeptPermissions>(
                            (Func<DeptPermissions, bool>) (x => x.PermissionName.Equals(permission)))
                        .ToList<DeptPermissions>().Count <= 0)
                    return (ActionResult) this.View("../Error/AccessDenied");
                return (ActionResult) this.View("../CONS/Index", (object) new DeptHomeViewModel()
                {
                    From_Date = DateTime.Now,
                    To_Date = DateTime.Now,
                    DeptId = 4,
                    ControllerName = "CONS"
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
                DeptHomeViewModel deptHomeViewModel = new DeptHomeViewModel();
                this.GetPermissionforUser();
                CustomPrincipal user = this.User;
                List<string> stringList = new List<string>();
                string permission = Convert.ToString((object) PermissionsEnum.CancelAllotment);
                List<DeptPermissions> deptPermission = user.DeptPermission;
                deptHomeViewModel.AccessYNo = deptPermission
                                                  .Where<DeptPermissions
                                                  >((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(4)))
                                                  .Where<DeptPermissions>((Func<DeptPermissions, bool>) (x =>
                                                      x.PermissionName.Equals(permission))).ToList<DeptPermissions>()
                                                  .Count > 0;
                if (!user.Departments.Contains(4))
                    return (ActionResult) this.View("../Error/AccessDenied");
                deptHomeViewModel.From_Date = DateTime.Now;
                deptHomeViewModel.To_Date = DateTime.Now;
                deptHomeViewModel.DeptId = 4;
                deptHomeViewModel.ControllerName = "CONS";
                return (ActionResult) this.View("../CONS/TreatmentIndex", (object) deptHomeViewModel);
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
                if (!this.User.Departments.Contains(4))
                    return (ActionResult) this.View("../Error/AccessDenied");
                CONSViewModel consViewModel = new CONSViewModel();
                return (ActionResult) this.View("../CONS/CONSPatientTreatmentList",
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
                if (!this.User.Departments.Contains(4))
                    return (ActionResult) this.View("../Error/AccessDenied");
                CONSViewModel consViewModel = new CONSViewModel();
                return (ActionResult) this.View("../CONS/Create", (object) this._service.BindCONSPatientModel(allotId));
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
        public ActionResult Create(CONSViewModel model)
        {
            try
            {
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(4))
                    return (ActionResult) this.View("../Error/AccessDenied");
                int num = 0;
                if (this.ModelState.IsValid)
                {
                    model.CreatedBy = user.Identity.Name;
                    num = this._service.SaveCONSPatient(model);
                    model.ConservativeId = num;
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
                    controller = "CONS",
                    action = "EditOnly",
                    allotId = model.studentAllotmentViewModel.AllotId,
                    CONSId = num
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
        public ActionResult Edit(long allotId, int CONSId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(4))
                    return (ActionResult) this.View("../Error/AccessDenied");
                CONSViewModel consViewModel = new CONSViewModel();
                return (ActionResult) this.View("../CONS/Edit",
                    (object) this._service.BindEditCONSPatientModel(allotId, CONSId));
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
        public ActionResult Edit(CONSViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int conservativeId = model.ConservativeId;
                if (!this.User.Departments.Contains(4))
                    return (ActionResult) this.View("../Error/AccessDenied");
                if (this.ModelState.IsValid)
                {
                    this._service.UpdateCONSPatient(model);
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
                    controller = "CONS",
                    action = nameof(Edit),
                    allotId = allotId,
                    CONSId = conservativeId
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
        public ActionResult EditOnly(long allotId, int CONSId)
        {
            try
            {
                this.GetPermissionforUser();
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(4))
                    return (ActionResult) this.View("../Error/AccessDenied");
                CONSViewModel consViewModel1 = new CONSViewModel();
                CONSViewModel consViewModel2 = this._service.BindEditCONSPatientModel(allotId, CONSId);
                IEnumerable<DeptPermissions> source =
                    user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(4)));
                consViewModel2.EditCaseSheetAccess =
                    source.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x =>
                            x.PermissionName.Equals(Convert.ToString((object) PermissionsEnum.EditFreezeCase))))
                        .ToList<DeptPermissions>().Count > 0;
                return (ActionResult) this.View("../CONS/Edit", (object) consViewModel2);
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
        public ActionResult EditOnly(CONSViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int conservativeId = model.ConservativeId;
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(4))
                    return (ActionResult) this.View("../Error/AccessDenied");
                if (this.ModelState.IsValid)
                {
                    model.ModifiedBy = user.Identity.Name;
                    this._service.UpdateCONSPatient(model);
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
                    controller = "CONS",
                    action = nameof(EditOnly),
                    allotId = allotId,
                    CONSId = conservativeId
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

        public ActionResult Allotment(int PatientId = 0, long ReferredId = 0, int CourseType = 0)
        {
            StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
            return (ActionResult) this.PartialView("../StudentAllotment/_StudentAllotment",
                (object) this._service.DisplayAllotment(PatientId, ReferredId, CourseType));
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        public ActionResult Search()
        {
            try
            {
                this.GetPermissionforUser();
                CustomPrincipal user = this.User;
                List<string> stringList = new List<string>();
                foreach (DeptPermissions deptPermissions in user.DeptPermission)
                {
                    if (deptPermissions.DeptId == 4)
                        stringList.Add(deptPermissions.PermissionName);
                }

                if (stringList.Count <= 0)
                    return (ActionResult) this.View("../Error/AccessDenied");
                return (ActionResult) this.View("../CONS/_CONSSearchOptions", (object) new CONSSearchViewModel()
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

        public JsonResult GetCONSSearchList(CONSSearchViewModel model)
        {
            string From_Date = model.From_Date.ToString("yyyy-MM-dd");
            string To_Date = model.To_Date.ToString("yyyy-MM-dd");
            string rootUrl = this.User.GetRootUrl();
            model.SearchDetails = this._service.consSearchDetails(From_Date, To_Date, rootUrl);
            return this.Json((object) model.SearchDetails);
        }

        public ActionResult Report(int Id)
        {
            try
            {
                CONSViewModel consViewModel = new CONSViewModel();
                return (ActionResult) this.View("../Reports/CONSCaserecordReport",
                    (object) this._service.BindCONSPatientReport(Id));
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
        public ActionResult ApprovalQueue()
        {
            try
            {
                this.GetPermissionforUser();
                CustomPrincipal user = this.User;
                List<string> stringList = new List<string>();
                string permission = Convert.ToString((object) PermissionsEnum.ProcedureApproval);
                if (user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(4)))
                        .Where<DeptPermissions>(
                            (Func<DeptPermissions, bool>) (x => x.PermissionName.Equals(permission)))
                        .ToList<DeptPermissions>().Count <= 0)
                    return (ActionResult) this.View("../Error/AccessDenied");
                return (ActionResult) this.View("../CONS/_CONSApprovalQueue",
                    (object) new CasesheetApprovalListViewModel()
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

        public JsonResult GetCONSApprovalList(CasesheetApprovalListViewModel model)
        {
            string From_Date = model.From_Date.ToString("yyyy-MM-dd");
            string To_Date = model.To_Date.ToString("yyyy-MM-dd");
            string rootUrl = this.User.GetRootUrl();
            model.SearchDetails = this._service.ConsCasesheetApprovalList(From_Date, To_Date, rootUrl);
            return this.Json((object) model.SearchDetails);
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpGet]
        public ActionResult Approval(long allotId, int CaserecordId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(4))
                    return (ActionResult) this.View("../Error/AccessDenied");
                CONSViewModel consViewModel1 = new CONSViewModel();
                CONSViewModel consViewModel2 = this._service.BindEditCONSPatientModel(allotId, CaserecordId);
                consViewModel2.FileUploadlist = this._uow.Repository<FileUploadViewModel>()
                    .GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) consViewModel2.PatientId,
                        (object) 4));
                consViewModel2.approvalViewModal = new ApprovalViewModal()
                {
                    CaserecordId = consViewModel2.ConservativeId,
                    DeptId = 4,
                    DoctorId = consViewModel2.studentAllotmentViewModel.DoctorId,
                    PatientId = (long) consViewModel2.PatientId,
                    ReferredTreatmentId = 0
                };
                if (!consViewModel2.Approval1)
                    consViewModel2.ApprovalType = 1;
                else if (consViewModel2.Approval1 && !consViewModel2.Approval2)
                    consViewModel2.ApprovalType = 2;
                else if (consViewModel2.Approval1 && consViewModel2.Approval2 && !consViewModel2.Approval3)
                    consViewModel2.ApprovalType = 3;
                consViewModel2.approvalViewModal.ApprovalTypeId = consViewModel2.ApprovalType;
                return (ActionResult) this.View("../CONS/Approval", (object) consViewModel2);
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
        public ActionResult UpdateSendForApproval(CONSViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int conservativeId = model.ConservativeId;
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(4))
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
                    this._service.UpdateCONSPatient(model);
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
                    controller = "CONS",
                    action = "EditOnly",
                    allotId = allotId,
                    CONSId = conservativeId
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
        public ActionResult Approval(CONSViewModel model)
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
                CONSCasesheet entity = new CONSCasesheet();
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

                entity.ConservativeDate = new DateTime?(model.ConservativeDate);
                entity.ConservativeId = model.ConservativeId;
                this._uow.Repository<CONSCasesheet>().Update(entity, false);
                this.TempData["Message"] = (object) "Approved Successfully ";
                return (ActionResult) this.RedirectToAction("ApprovalQueue", new RouteValueDictionary((object) new
                {
                    controller = "CONS",
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

        [HttpPost]
        public ActionResult InvestigationApproval(CONSViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int conservativeId = model.ConservativeId;
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
                controller = "CONS",
                action = "Approval",
                allotId = allotId,
                CaserecordId = conservativeId
            }));
        }

        [HttpPost]
        public ActionResult ReferralApproval(CONSViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int conservativeId = model.ConservativeId;
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
                controller = "CONS",
                action = "Approval",
                allotId = allotId,
                CaserecordId = conservativeId
            }));
        }

        [HttpPost]
        public ActionResult TreatmentApproval(CONSViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int conservativeId = model.ConservativeId;
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
                controller = "CONS",
                action = "Approval",
                allotId = allotId,
                CaserecordId = conservativeId
            }));
        }

        [HttpPost]
        public JsonResult DeleteRestorativeProToothDet(int RestorativeProId)
        {
            this._uow.Repository<CONSRestorativeProcedureDetails>()
                .GetEntitiesBySql(string.Format(Queries.DeleteToothDetails, (object) RestorativeProId));
            return this.Json((object) true);
        }

        [HttpPost]
        public JsonResult DeletePostAndCoreToothDet(int PostAndCoreId)
        {
            this._uow.Repository<CONSPostAndCoreDetails>()
                .GetEntitiesBySql(string.Format(Queries.DeletePcToothDetails, (object) PostAndCoreId));
            return this.Json((object) true);
        }

        [HttpPost]
        public JsonResult DeleteSurgicalProToothDet(int SurgicalProId)
        {
            this._uow.Repository<CONSSurgicalProcedureDetails>()
                .GetEntitiesBySql(string.Format(Queries.DeleteSpToothDetails, (object) SurgicalProId));
            return this.Json((object) true);
        }

        [HttpPost]
        public JsonResult DeleteEstheticCorrToothDet(int EstheticCorrId)
        {
            this._uow.Repository<CONSEstheticCorrectionDetails>()
                .GetEntitiesBySql(string.Format(Queries.DeleteEcToothDetails, (object) EstheticCorrId));
            return this.Json((object) true);
        }

        [HttpPost]
        public JsonResult DeleteBleachingToothDet(int BleachingId)
        {
            this._uow.Repository<CONSBleachingDetails>()
                .GetEntitiesBySql(string.Format(Queries.DeleteBlToothDetails, (object) BleachingId));
            return this.Json((object) true);
        }

        [HttpPost]
        public JsonResult DeleteTraumatisedToothDet(int TraumatisedToothId)
        {
            this._uow.Repository<CONSTraumatisedToothDetails>()
                .GetEntitiesBySql(string.Format(Queries.DeleteTtToothDetails, (object) TraumatisedToothId));
            return this.Json((object) true);
        }

        [HttpPost]
        public JsonResult DeleteRootCanalToothDet(int RootCanalId)
        {
            this._uow.Repository<CONSRootCanalDetails>()
                .GetEntitiesBySql(string.Format(Queries.DeleteRcToothDetails, (object) RootCanalId));
            return this.Json((object) true);
        }

        [HttpPost]
        public JsonResult DeleteReRootCanalToothDet(int ReRootCanalId)
        {
            this._uow.Repository<CONSReRctDetails>()
                .GetEntitiesBySql(string.Format(Queries.DeleteReToothDetails, (object) ReRootCanalId));
            return this.Json((object) true);
        }

        [HttpPost]
        public JsonResult DeleteIncRootFormToothDet(int IncompleteRootId)
        {
            this._uow.Repository<CONSIncompleteRootFormDetails>()
                .GetEntitiesBySql(string.Format(Queries.DeleteRfToothDetails, (object) IncompleteRootId));
            return this.Json((object) true);
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
                                                           (Func<DeptPermissions, bool>) (x => x.DeptId.Equals(4)))
                                                       .Where<DeptPermissions>((Func<DeptPermissions, bool>) (x =>
                                                           x.PermissionName.Equals(permission)))
                                                       .ToList<DeptPermissions>().Count > 0;
                followupSearchViewModel.From_Date = DateTime.Now;
                followupSearchViewModel.To_Date = DateTime.Now;
                followupSearchViewModel.DeptId = 4;
                followupSearchViewModel.DeptCode = Department.CONS.ToString();
                followupSearchViewModel.DeptName = DeparmentNameDto.StringBusinessUnits(Department.CONS);
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

        [HttpGet]
        public JsonResult RestorativeProDetailsById(int Id, int ConservativeId)
        {
            return this.Json((object) new CONSViewModel()
            {
                RpList = this._uow.Repository<CONSViewModel>().GetEntitiesBySql(
                    string.Format(Queries.GetConsRestorativeProcedureDetailsById, (object) ConservativeId, (object) Id))
            }.RpList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult PostAndCoreDetailsById(int Id, int ConservativeId)
        {
            return this.Json((object) new CONSViewModel()
            {
                PcList = this._uow.Repository<CONSViewModel>()
                    .GetEntitiesBySql(string.Format(Queries.GetPostAndCoreDetailsById, (object) ConservativeId,
                        (object) Id))
            }.PcList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SurgicalProDetailsById(int Id, int ConservativeId)
        {
            return this.Json((object) new CONSViewModel()
            {
                SpList = this._uow.Repository<CONSViewModel>()
                    .GetEntitiesBySql(string.Format(Queries.GetSurgicalProDetailsById, (object) ConservativeId,
                        (object) Id))
            }.SpList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EstheticCorrDetailsById(int Id, int ConservativeId)
        {
            return this.Json((object) new CONSViewModel()
            {
                EcList = this._uow.Repository<CONSViewModel>().GetEntitiesBySql(
                    string.Format(Queries.GetEstheticCorrDetailsById, (object) ConservativeId, (object) Id))
            }.EcList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BleachingDetailsById(int Id, int ConservativeId)
        {
            return this.Json((object) new CONSViewModel()
            {
                BlList = this._uow.Repository<CONSViewModel>()
                    .GetEntitiesBySql(string.Format(Queries.GetBleachingDetailsById, (object) ConservativeId,
                        (object) Id))
            }.BlList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult TraumatisedToothDetailsById(int Id, int ConservativeId)
        {
            return this.Json((object) new CONSViewModel()
            {
                TtList = this._uow.Repository<CONSViewModel>().GetEntitiesBySql(
                    string.Format(Queries.GetTraumatisedToothDetailsById, (object) ConservativeId, (object) Id))
            }.TtList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RootCanalDetailsById(int Id, int ConservativeId)
        {
            return this.Json((object) new CONSViewModel()
            {
                RcList = this._uow.Repository<CONSViewModel>()
                    .GetEntitiesBySql(string.Format(Queries.GetRootCanalDetailsById, (object) ConservativeId,
                        (object) Id))
            }.RcList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ReRctDetails(int Id, int ConservativeId)
        {
            return this.Json((object) new CONSViewModel()
            {
                ReList = this._uow.Repository<CONSViewModel>()
                    .GetEntitiesBySql(string.Format(Queries.GetConsReRctDetailsById, (object) ConservativeId,
                        (object) Id))
            }.ReList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult IncompleteRootDetails(int Id, int ConservativeId)
        {
            return this.Json((object) new CONSViewModel()
            {
                RfList = this._uow.Repository<CONSViewModel>().GetEntitiesBySql(
                    string.Format(Queries.GetConsIncompleteRootFormDetailsById, (object) ConservativeId, (object) Id))
            }.RfList, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpGet]
        public ActionResult CreateOpd()
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(15))
                    return (ActionResult) this.View("../Error/AccessDenied");
                return (ActionResult) this.View((object) this._opdService.BindPatientModel(
                    new OPDPatientRegistrationViewModel()
                    {
                        DepartmentId = (int) Department.ORTHO,
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
        public ActionResult CreateOpd(OPDPatientRegistrationViewModel model)
        {
            CustomPrincipal user = this.User;
            if (!user.Departments.Contains(15))
                return (ActionResult) this.View("../Error/AccessDenied");
            int num = 0;
            if (this.ModelState.IsValid)
            {
                model.CreatedBy = user.Identity.Name;
                model.PatientName = model.PatientName.ToUpper();
                num = this._opdService.SavePatient(model);
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

            if (num <= 0)
                return (ActionResult) this.RedirectToAction(nameof(CreateOpd));
            this.TempData["Message"] = (object) "Saved Successfully ";
            return (ActionResult) this.RedirectToAction("EditOpd", (object) new
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
                    return (ActionResult) this.View("../Error/AccessDenied");
                OPDPatientRegistrationViewModel registrationViewModel = new OPDPatientRegistrationViewModel();
                return (ActionResult) this.View(nameof(EditOpd), (object) this._opdService.BindEditPatientModel(Id));
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
        public ActionResult EditOpd(OPDPatientRegistrationViewModel model)
        {
            CustomPrincipal user = this.User;
            if (!user.Departments.Contains(15))
                return (ActionResult) this.View("../Error/AccessDenied");
            if (this.ModelState.IsValid)
            {

                model.ModifiedBy = user.Identity.Name;
                model.PatientName = model.PatientName.ToUpper();
                this._opdService.UpdatePatient(model);
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
            return (ActionResult) this.RedirectToAction(nameof(EditOpd), (object) new
            {
                Id = model.PatientId
            });
        }
    }
}
