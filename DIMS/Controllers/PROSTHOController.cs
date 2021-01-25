// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.PROSTHOController
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
    public class PROSTHOController : BaseController
    {
        private IUnitOfWork _uow;
        private IPROSCasesheetService _service;
        private IOPDPatientRegistrationService _opdService;

        public PROSTHOController(IUnitOfWork uow, IPROSCasesheetService service, IUserService userservice,
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
                if (user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(7)))
                        .Where<DeptPermissions>(
                            (Func<DeptPermissions, bool>) (x => x.PermissionName.Equals(permission)))
                        .ToList<DeptPermissions>().Count <= 0)
                    return (ActionResult) this.View("../Error/AccessDenied");
                return (ActionResult) this.View("../PROSTHO/Index", (object) new DeptHomeViewModel()
                {
                    From_Date = DateTime.Now,
                    To_Date = DateTime.Now,
                    DeptId = 7,
                    ControllerName = "PROSTHO"
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
                                                  >((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(7)))
                                                  .Where<DeptPermissions>((Func<DeptPermissions, bool>) (x =>
                                                      x.PermissionName.Equals(permission))).ToList<DeptPermissions>()
                                                  .Count > 0;
                if (!user.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                deptHomeViewModel.From_Date = DateTime.Now;
                deptHomeViewModel.To_Date = DateTime.Now;
                deptHomeViewModel.DeptId = 7;
                deptHomeViewModel.ControllerName = "PROSTHO";
                return (ActionResult) this.View("../PROSTHO/TreatmentIndex", (object) deptHomeViewModel);
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
            try
            {
                StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
                return (ActionResult) this.PartialView("../StudentAllotment/_StudentAllotment",
                    (object) this._service.DisplayAllotment(PatientId, ReferredId, CourseType));
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

        public ActionResult Operations(long allotId, int patientId)
        {
            try
            {
                return (ActionResult) this.RedirectToAction("PROSTreatment", new RouteValueDictionary((object) new
                {
                    allotId = allotId,
                    patientId = patientId
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

        [HttpGet]
        public ActionResult PROSTreatment(long allotId, int patientId)
        {
            try
            {
                PROSTreatmentViewModel treatmentViewModel = new PROSTreatmentViewModel();
                return (ActionResult) this.View("../PROSTHO/PROSTreatment",
                    (object) this._service.Treatment(allotId, patientId));
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
        public ActionResult PROSTreatmentBack(long allotId, int patientId)
        {
            try
            {
                return (ActionResult) this.View("../PROSTHO/PROSTreatment", (object) new PROSTreatmentViewModel()
                {
                    ProsList = (IEnumerable<PROSTreatmentViewModel>) this._service.PROSTreatmentList(allotId)
                        .ToList<PROSTreatmentViewModel>(),
                    PatientId = patientId
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
        [HttpGet]
        public ActionResult Create(int treatId, long allotId, int patientId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                PROSCDViewModel proscdViewModel = new PROSCDViewModel();
                PROSRPDViewModel prosrpdViewModel = new PROSRPDViewModel();
                PROSFPDViewModel prosfpdViewModel = new PROSFPDViewModel();
                PROSMFPViewModel prosmfpViewModel = new PROSMFPViewModel();
                PROSDIMViewModel prosdimViewModel = new PROSDIMViewModel();
                switch (treatId)
                {
                    case 1:
                        return (ActionResult) this.View("../PROSTHO/PROSCDCreate",
                            (object) this._service.BindPROSCDPatientModel(treatId, allotId));
                    case 2:
                        return (ActionResult) this.View("../PROSTHO/PROSRPDCreate",
                            (object) this._service.BindPROSRPDPatientModel(treatId, allotId));
                    case 3:
                        return (ActionResult) this.View("../PROSTHO/PROSFPDCreate",
                            (object) this._service.BindPROSFPDPatientModel(treatId, allotId));
                    case 4:
                        return (ActionResult) this.View("../PROSTHO/PROSMFPCreate",
                            (object) this._service.BindPROSMFPPatientModel(treatId, allotId));
                    case 5:
                        return (ActionResult) this.View("../PROSTHO/PROSDIMCreate",
                            (object) this._service.BindPROSDIMPatientModel(treatId, allotId));
                    default:
                        return (ActionResult) this.View();
                }
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
        public ActionResult PROSCDCreate(PROSCDViewModel model)
        {
            try
            {
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                int num = 0;
                if (this.ModelState.IsValid)
                {
                    model.CreatedBy = user.Identity.Name;
                    num = this._service.PROSCDSavePatient(model);
                    model.ProsthoCDId = num;
                    if (!model.Approval1)
                        model.ApprovalType = 1;
                    this._service.PROSCDUpdateAllotment(model);
                    this._service.PROSCDUpdateReferralStatus(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSCDSavefollowUp(model);
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

                return (ActionResult) this.RedirectToAction("CDEditOnly", new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = "CDEditOnly",
                    allotId = model.studentAllotmentViewModel.AllotId,
                    PROSCDId = num
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
        public ActionResult CDEditOnly(long allotId, int PROSCDId)
        {
            try
            {
                this.GetPermissionforUser();
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                int treatId = 1;
                PROSCDViewModel proscdViewModel1 = new PROSCDViewModel();
                PROSCDViewModel proscdViewModel2 = this._service.PROSCDBindEditPatientModel(treatId, allotId, PROSCDId);
                IEnumerable<DeptPermissions> source =
                    user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(7)));
                proscdViewModel2.EditCaseSheetAccess =
                    source.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x =>
                            x.PermissionName.Equals(Convert.ToString((object) PermissionsEnum.EditFreezeCase))))
                        .ToList<DeptPermissions>().Count > 0;
                return (ActionResult) this.View("../PROSTHO/PROSCDEdit", (object) proscdViewModel2);
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
        public ActionResult CDEditOnly(PROSCDViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int prosthoCdId = model.ProsthoCDId;
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                if (this.ModelState.IsValid)
                {
                    model.ModifiedBy = user.Identity.Name;
                    this._service.PROSCDUpdatePatient(model);
                    this._service.PROSCDUpdateAllotment(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSCDSavefollowUp(model);
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
                return (ActionResult) this.RedirectToAction(nameof(CDEditOnly), new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = nameof(CDEditOnly),
                    allotId = allotId,
                    PROSCDId = prosthoCdId
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
        public ActionResult CDEdit(int treatId, long allotId, int PROSCDId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                PROSCDViewModel proscdViewModel = new PROSCDViewModel();
                return (ActionResult) this.View("../PROSTHO/PROSCDEdit",
                    (object) this._service.PROSCDBindEditPatientModel(treatId, allotId, PROSCDId));
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
        public ActionResult CDEdit(PROSCDViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int prosthoCdId = model.ProsthoCDId;
                int num = 1;
                if (!this.User.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                if (this.ModelState.IsValid)
                {
                    this._service.PROSCDUpdatePatient(model);
                    this._service.PROSCDUpdateAllotment(model);
                    this._service.PROSCDUpdateReferralStatus(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSCDSavefollowUp(model);
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
                return (ActionResult) this.RedirectToAction(nameof(CDEdit), new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = nameof(CDEdit),
                    treatId = num,
                    allotId = allotId,
                    PROSCDId = prosthoCdId
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
        public ActionResult PROSRPDCreate(PROSRPDViewModel model)
        {
            try
            {
                CustomPrincipal user = this.User;
                int num = 0;
                if (this.ModelState.IsValid)
                {
                    model.CreatedBy = user.Identity.Name;
                    num = this._service.PROSRPDSavePatient(model);
                    model.ProsthoRPDId = num;
                    if (!model.Approval1)
                        model.ApprovalType = 1;
                    this._service.PROSRPDUpdateAllotment(model);
                    this._service.PROSRPDUpdateReferralStatus(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSRPDSavefollowUp(model);
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

                return (ActionResult) this.RedirectToAction("RPDEditOnly", new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = "RPDEditOnly",
                    allotId = model.studentAllotmentViewModel.AllotId,
                    PROSRPDId = num
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
        public ActionResult RPDEditOnly(long allotId, int PROSRPDId)
        {
            try
            {
                this.GetPermissionforUser();
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                int treatId = 2;
                PROSRPDViewModel prosrpdViewModel1 = new PROSRPDViewModel();
                PROSRPDViewModel prosrpdViewModel2 =
                    this._service.PROSRPDBindEditPatientModel(treatId, allotId, PROSRPDId);
                IEnumerable<DeptPermissions> source =
                    user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(7)));
                prosrpdViewModel2.EditCaseSheetAccess =
                    source.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x =>
                            x.PermissionName.Equals(Convert.ToString((object) PermissionsEnum.EditFreezeCase))))
                        .ToList<DeptPermissions>().Count > 0;
                return (ActionResult) this.View("../PROSTHO/PROSRPDEdit", (object) prosrpdViewModel2);
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
        public ActionResult RPDEditOnly(PROSRPDViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int prosthoRpdId = model.ProsthoRPDId;
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                if (this.ModelState.IsValid)
                {
                    model.ModifiedBy = user.Identity.Name;
                    this._service.PROSRPDUpdatePatient(model);
                    this._service.PROSRPDUpdateAllotment(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSRPDSavefollowUp(model);
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
                return (ActionResult) this.RedirectToAction(nameof(RPDEditOnly), new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = nameof(RPDEditOnly),
                    allotId = allotId,
                    PROSRPDId = prosthoRpdId
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
        public ActionResult RPDEdit(int treatId, long allotId, int PROSRPDId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                PROSRPDViewModel prosrpdViewModel = new PROSRPDViewModel();
                return (ActionResult) this.View("../PROSTHO/PROSRPDEdit",
                    (object) this._service.PROSRPDBindEditPatientModel(treatId, allotId, PROSRPDId));
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
        public ActionResult RPDEdit(PROSRPDViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int prosthoRpdId = model.ProsthoRPDId;
                int num = 2;
                if (!this.User.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                if (this.ModelState.IsValid)
                {
                    this._service.PROSRPDUpdatePatient(model);
                    this._service.PROSRPDUpdateAllotment(model);
                    this._service.PROSRPDUpdateReferralStatus(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSRPDSavefollowUp(model);
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
                return (ActionResult) this.RedirectToAction(nameof(RPDEdit), new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = nameof(RPDEdit),
                    treatId = num,
                    allotId = allotId,
                    PROSRPDId = prosthoRpdId
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
        public ActionResult PROSFPDCreate(PROSFPDViewModel model)
        {
            try
            {
                CustomPrincipal user = this.User;
                int num = 0;
                if (this.ModelState.IsValid)
                {
                    model.CreatedBy = user.Identity.Name;
                    num = this._service.PROSFPDSavePatient(model);
                    model.ProsthoFPDId = num;
                    if (!model.Approval1)
                        model.ApprovalType = 1;
                    this._service.PROSFPDUpdateAllotment(model);
                    this._service.PROSFPDUpdateReferralStatus(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSFPDSavefollowUp(model);
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

                return (ActionResult) this.RedirectToAction("FPDEditOnly", new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = "FPDEditOnly",
                    allotId = model.studentAllotmentViewModel.AllotId,
                    PROSFPDId = num
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
        public ActionResult FPDEditOnly(long allotId, int PROSFPDId)
        {
            try
            {
                this.GetPermissionforUser();
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                int treatId = 3;
                PROSFPDViewModel prosfpdViewModel1 = new PROSFPDViewModel();
                PROSFPDViewModel prosfpdViewModel2 =
                    this._service.PROSFPDBindEditPatientModel(treatId, allotId, PROSFPDId);
                IEnumerable<DeptPermissions> source =
                    user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(7)));
                prosfpdViewModel2.EditCaseSheetAccess =
                    source.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x =>
                            x.PermissionName.Equals(Convert.ToString((object) PermissionsEnum.EditFreezeCase))))
                        .ToList<DeptPermissions>().Count > 0;
                return (ActionResult) this.View("../PROSTHO/PROSFPDEdit", (object) prosfpdViewModel2);
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
        public ActionResult FPDEditOnly(PROSFPDViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int prosthoFpdId = model.ProsthoFPDId;
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                if (this.ModelState.IsValid)
                {
                    model.ModifiedBy = user.Identity.Name;
                    this._service.PROSFPDUpdatePatient(model);
                    this._service.PROSFPDUpdateAllotment(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSFPDSavefollowUp(model);
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
                return (ActionResult) this.RedirectToAction(nameof(FPDEditOnly), new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = nameof(FPDEditOnly),
                    allotId = allotId,
                    PROSFPDId = prosthoFpdId
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
        public ActionResult FPDEdit(int treatId, long allotId, int PROSFPDId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                PROSFPDViewModel prosfpdViewModel = new PROSFPDViewModel();
                return (ActionResult) this.View("../PROSTHO/PROSFPDEdit",
                    (object) this._service.PROSFPDBindEditPatientModel(treatId, allotId, PROSFPDId));
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
        public ActionResult FPDEdit(PROSFPDViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int prosthoFpdId = model.ProsthoFPDId;
                int num = 3;
                if (!this.User.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                if (this.ModelState.IsValid)
                {
                    this._service.PROSFPDUpdatePatient(model);
                    this._service.PROSFPDUpdateAllotment(model);
                    this._service.PROSFPDUpdateReferralStatus(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSFPDSavefollowUp(model);
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
                return (ActionResult) this.RedirectToAction(nameof(FPDEdit), new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = nameof(FPDEdit),
                    treatId = num,
                    allotId = allotId,
                    PROSFPDId = prosthoFpdId
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

        [HttpGet]
        public JsonResult AbutmentTeethDetailsById(int Id, int ProsthoFPDId)
        {
            return this.Json((object) new PROSFPDViewModel()
            {
                AbutList = this._uow.Repository<PROSFPDViewModel>().GetEntitiesBySql(
                    string.Format(Queries.GetAbutmentToothDetailsById, (object) ProsthoFPDId, (object) Id))
            }.AbutList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteAbutmentTeethDet(int AbutmentTeethId)
        {
            this._uow.Repository<CONSRestorativeProcedureDetails>()
                .GetEntitiesBySql(string.Format(Queries.DeleteAbutToothDetails, (object) AbutmentTeethId));
            return this.Json((object) true);
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpPost]
        public ActionResult PROSMFPCreate(PROSMFPViewModel model)
        {
            try
            {
                CustomPrincipal user = this.User;
                int num = 0;
                if (this.ModelState.IsValid)
                {
                    model.CreatedBy = user.Identity.Name;
                    num = this._service.PROSMFPSavePatient(model);
                    model.ProsthoMFPId = num;
                    if (!model.Approval1)
                        model.ApprovalType = 1;
                    this._service.PROSMFPUpdateAllotment(model);
                    this._service.PROSMFPUpdateReferralStatus(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSMFPSavefollowUp(model);
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

                return (ActionResult) this.RedirectToAction("MFPEditOnly", new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = "MFPEditOnly",
                    allotId = model.studentAllotmentViewModel.AllotId,
                    PROSMFPId = num
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
        public ActionResult MFPEditOnly(long allotId, int PROSMFPId)
        {
            try
            {
                this.GetPermissionforUser();
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                int treatId = 4;
                PROSMFPViewModel prosmfpViewModel1 = new PROSMFPViewModel();
                PROSMFPViewModel prosmfpViewModel2 =
                    this._service.PROSMFPBindEditPatientModel(treatId, allotId, PROSMFPId);
                IEnumerable<DeptPermissions> source =
                    user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(7)));
                prosmfpViewModel2.EditCaseSheetAccess =
                    source.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x =>
                            x.PermissionName.Equals(Convert.ToString((object) PermissionsEnum.EditFreezeCase))))
                        .ToList<DeptPermissions>().Count > 0;
                return (ActionResult) this.View("../PROSTHO/PROSMFPEdit", (object) prosmfpViewModel2);
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
        public ActionResult MFPEditOnly(PROSMFPViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int prosthoMfpId = model.ProsthoMFPId;
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                if (this.ModelState.IsValid)
                {
                    model.ModifiedBy = user.Identity.Name;
                    this._service.PROSMFPUpdatePatient(model);
                    this._service.PROSMFPUpdateAllotment(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSMFPSavefollowUp(model);
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
                return (ActionResult) this.RedirectToAction(nameof(MFPEditOnly), new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = nameof(MFPEditOnly),
                    allotId = allotId,
                    PROSMFPId = prosthoMfpId
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
        public ActionResult MFPEdit(int treatId, long allotId, int PROSMFPId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                PROSMFPViewModel prosmfpViewModel = new PROSMFPViewModel();
                return (ActionResult) this.View("../PROSTHO/PROSMFPEdit",
                    (object) this._service.PROSMFPBindEditPatientModel(treatId, allotId, PROSMFPId));
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
        public ActionResult MFPEdit(PROSMFPViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int prosthoMfpId = model.ProsthoMFPId;
                int num = 4;
                if (!this.User.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                if (this.ModelState.IsValid)
                {
                    this._service.PROSMFPUpdatePatient(model);
                    this._service.PROSMFPUpdateAllotment(model);
                    this._service.PROSMFPUpdateReferralStatus(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSMFPSavefollowUp(model);
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
                return (ActionResult) this.RedirectToAction(nameof(MFPEdit), new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = nameof(MFPEdit),
                    treatId = num,
                    allotId = allotId,
                    PROSMFPId = prosthoMfpId
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

        [HttpGet]
        public JsonResult MfpTreatProDetailsById(int Id, int ProsthoMFPId)
        {
            return this.Json((object) new PROSMFPViewModel()
            {
                MfpTreatList = this._uow.Repository<PROSMFPViewModel>()
                    .GetEntitiesBySql(string.Format(Queries.GetProsMfpDetailsById, (object) ProsthoMFPId, (object) Id))
            }.MfpTreatList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteMfpTreatProDet(int ProsMaxilloProId)
        {
            this._uow.Repository<PROSMFPTreatmentDescription>()
                .GetEntitiesBySql(string.Format(Queries.DeleteMfpTreatProDetails, (object) ProsMaxilloProId));
            return this.Json((object) true);
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpPost]
        public ActionResult PROSDIMCreate(PROSDIMViewModel model)
        {
            try
            {
                CustomPrincipal user = this.User;
                int num = 0;
                if (this.ModelState.IsValid)
                {
                    model.CreatedBy = user.Identity.Name;
                    num = this._service.PROSDIMSavePatient(model);
                    model.ProsthoDIMId = num;
                    if (!model.Approval1)
                        model.ApprovalType = 1;
                    this._service.PROSDIMUpdateAllotment(model);
                    this._service.PROSDIMUpdateReferralStatus(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSDIMSavefollowUp(model);
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

                return (ActionResult) this.RedirectToAction("DIMEditOnly", new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = "DIMEditOnly",
                    allotId = model.studentAllotmentViewModel.AllotId,
                    PROSDIMId = num
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
        public ActionResult DIMEditOnly(long allotId, int PROSDIMId)
        {
            try
            {
                this.GetPermissionforUser();
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                int treatId = 5;
                PROSDIMViewModel prosdimViewModel1 = new PROSDIMViewModel();
                PROSDIMViewModel prosdimViewModel2 =
                    this._service.PROSDIMBindEditPatientModel(treatId, allotId, PROSDIMId);
                IEnumerable<DeptPermissions> source =
                    user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(7)));
                prosdimViewModel2.EditCaseSheetAccess =
                    source.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x =>
                            x.PermissionName.Equals(Convert.ToString((object) PermissionsEnum.EditFreezeCase))))
                        .ToList<DeptPermissions>().Count > 0;
                return (ActionResult) this.View("../PROSTHO/PROSDIMEdit", (object) prosdimViewModel2);
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
        public ActionResult DIMEditOnly(PROSDIMViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int prosthoDimId = model.ProsthoDIMId;
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                if (this.ModelState.IsValid)
                {
                    model.ModifiedBy = user.Identity.Name;
                    this._service.PROSDIMUpdatePatient(model);
                    this._service.PROSDIMUpdateAllotment(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSDIMSavefollowUp(model);
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
                return (ActionResult) this.RedirectToAction(nameof(DIMEditOnly), new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = nameof(DIMEditOnly),
                    allotId = allotId,
                    PROSDIMId = prosthoDimId
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
        public ActionResult DIMEdit(int treatId, long allotId, int PROSDIMId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                PROSDIMViewModel prosdimViewModel = new PROSDIMViewModel();
                return (ActionResult) this.View("../PROSTHO/PROSDIMEdit",
                    (object) this._service.PROSDIMBindEditPatientModel(treatId, allotId, PROSDIMId));
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
        public ActionResult DIMEdit(PROSDIMViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int prosthoDimId = model.ProsthoDIMId;
                int num = 5;
                if (!this.User.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                if (this.ModelState.IsValid)
                {
                    this._service.PROSDIMUpdatePatient(model);
                    this._service.PROSDIMUpdateAllotment(model);
                    this._service.PROSDIMUpdateReferralStatus(model);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSDIMSavefollowUp(model);
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
                return (ActionResult) this.RedirectToAction(nameof(DIMEdit), new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = nameof(DIMEdit),
                    treatId = num,
                    allotId = allotId,
                    PROSDIMId = prosthoDimId
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
        public ActionResult Search()
        {
            try
            {
                this.GetPermissionforUser();
                CustomPrincipal user = this.User;
                List<string> stringList = new List<string>();
                if (user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(7)))
                        .ToList<DeptPermissions>().Count <= 0)
                    return (ActionResult) this.View("../Error/AccessDenied");
                return (ActionResult) this.View("../PROSTHO/_PROSSearchOptions", (object) new PROSSearchViewModel()
                {
                    From_Date = DateTime.Now,
                    To_Date = DateTime.Now,
                    TreatmentTypeDetails = (IEnumerable<TreatmentTypes>) this._uow.Repository<TreatmentTypes>()
                        .GetEntitiesBySql(string.Format(Queries.TreatmentsTypes, (object) 7)).ToList<TreatmentTypes>()
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

        public JsonResult GetPROSSearchList(PROSSearchViewModel model)
        {
            string From_Date = model.From_Date.ToString("yyyy-MM-dd");
            string To_Date = model.To_Date.ToString("yyyy-MM-dd");
            int treatmentId = model.TreatmentId;
            string rootUrl = this.User.GetRootUrl();
            model.SearchDetails = this._service.prosthoSearchDetails(From_Date, To_Date, treatmentId, rootUrl);
            return this.Json((object) model.SearchDetails);
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpGet]
        public ActionResult CDReport(int Id)
        {
            try
            {
                PROSCDViewModel proscdViewModel = new PROSCDViewModel();
                return (ActionResult) this.View("../Reports/PROSCDCaserecordReport",
                    (object) this._service.BindPROSCDPatientReport(Id));
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
        public ActionResult RPDReport(int Id)
        {
            try
            {
                PROSRPDViewModel prosrpdViewModel = new PROSRPDViewModel();
                return (ActionResult) this.View("../Reports/PROSRPDCaserecordReport",
                    (object) this._service.BindPROSRPDPatientReport(Id));
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
        public ActionResult FPDReport(int Id)
        {
            try
            {
                PROSFPDViewModel prosfpdViewModel = new PROSFPDViewModel();
                return (ActionResult) this.View("../Reports/PROSFPDCaserecordReport",
                    (object) this._service.BindPROSFPDPatientReport(Id));
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
        public ActionResult MFPReport(int Id)
        {
            try
            {
                PROSMFPViewModel prosmfpViewModel = new PROSMFPViewModel();
                return (ActionResult) this.View("../Reports/PROSMFPCaserecordReport",
                    (object) this._service.BindPROSMFPPatientReport(Id));
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
        public ActionResult DIMReport(int Id)
        {
            try
            {
                PROSDIMViewModel prosdimViewModel = new PROSDIMViewModel();
                return (ActionResult) this.View("../Reports/PROSDIMCaserecordReport",
                    (object) this._service.BindPROSDIMPatientReport(Id));
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
                if (user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(7)))
                        .Where<DeptPermissions>(
                            (Func<DeptPermissions, bool>) (x => x.PermissionName.Equals(permission)))
                        .ToList<DeptPermissions>().Count <= 0)
                    return (ActionResult) this.View("../Error/AccessDenied");
                return (ActionResult) this.View("../PROSTHO/_PROSApprovalQueue",
                    (object) new CasesheetApprovalListViewModel()
                    {
                        From_Date = DateTime.Now,
                        To_Date = DateTime.Now,
                        TreatmentTypeDetails = (IEnumerable<TreatmentTypes>) this._uow.Repository<TreatmentTypes>()
                            .GetEntitiesBySql(string.Format(Queries.TreatmentsTypes, (object) 7))
                            .ToList<TreatmentTypes>()
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

        public JsonResult GetPROSApprovalList(CasesheetApprovalListViewModel model)
        {
            string From_Date = model.From_Date.ToString("yyyy-MM-dd");
            string To_Date = model.To_Date.ToString("yyyy-MM-dd");
            int treatmentId = model.TreatmentId;
            string rootUrl = this.User.GetRootUrl();
            switch (treatmentId)
            {
                case 1:
                    model.SearchDetails = this._service.ProsCDCasesheetApprovalList(From_Date, To_Date, rootUrl);
                    break;
                case 2:
                    model.SearchDetails = this._service.ProsRPDCasesheetApprovalList(From_Date, To_Date, rootUrl);
                    break;
                case 3:
                    model.SearchDetails = this._service.ProsFPDCasesheetApprovalList(From_Date, To_Date, rootUrl);
                    break;
                case 4:
                    model.SearchDetails = this._service.ProsMFPCasesheetApprovalList(From_Date, To_Date, rootUrl);
                    break;
                case 5:
                    model.SearchDetails = this._service.ProsDIMCasesheetApprovalList(From_Date, To_Date, rootUrl);
                    break;
            }

            return this.Json((object) model.SearchDetails);
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpGet]
        public ActionResult CDApproval(long allotId, int CaserecordId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                PROSCDViewModel proscdViewModel1 = new PROSCDViewModel();
                PROSCDViewModel proscdViewModel2 = this._service.PROSCDBindEditPatientModel(1, allotId, CaserecordId);
                proscdViewModel2.FileUploadlist = this._uow.Repository<FileUploadViewModel>()
                    .GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) proscdViewModel2.PatientId,
                        (object) 7));
                proscdViewModel2.approvalViewModal = new ApprovalViewModal()
                {
                    CaserecordId = proscdViewModel2.ProsthoCDId,
                    DeptId = 7,
                    DoctorId = proscdViewModel2.studentAllotmentViewModel.DoctorId,
                    PatientId = (long) proscdViewModel2.PatientId,
                    ReferredTreatmentId = 0
                };
                if (!proscdViewModel2.Approval1)
                    proscdViewModel2.ApprovalType = 1;
                else if (proscdViewModel2.Approval1 && !proscdViewModel2.Approval2)
                    proscdViewModel2.ApprovalType = 2;
                else if (proscdViewModel2.Approval1 && proscdViewModel2.Approval2 && !proscdViewModel2.Approval3)
                    proscdViewModel2.ApprovalType = 3;
                proscdViewModel2.approvalViewModal.ApprovalTypeId = proscdViewModel2.ApprovalType;
                return (ActionResult) this.View("../PROSTHO/_CDApproval", (object) proscdViewModel2);
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
        public ActionResult CDApproval(PROSCDViewModel model)
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
                this._service.CDProcedureApproval(model);
                PROSCDCasesheet entity = new PROSCDCasesheet();
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

                entity.ProsthoCDDate = model.ProsthoCDDate;
                entity.ProsthoCDId = model.ProsthoCDId;
                this._uow.Repository<PROSCDCasesheet>().Update(entity, false);
                this.TempData["Message"] = (object) "Approved Successfully ";
                return (ActionResult) this.RedirectToAction("ApprovalQueue", new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
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
        [HttpGet]
        public ActionResult RPDApproval(long allotId, int CaserecordId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                PROSRPDViewModel prosrpdViewModel1 = new PROSRPDViewModel();
                PROSRPDViewModel prosrpdViewModel2 =
                    this._service.PROSRPDBindEditPatientModel(2, allotId, CaserecordId);
                prosrpdViewModel2.FileUploadlist = this._uow.Repository<FileUploadViewModel>()
                    .GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) prosrpdViewModel2.PatientId,
                        (object) 7));
                prosrpdViewModel2.approvalViewModal = new ApprovalViewModal()
                {
                    CaserecordId = prosrpdViewModel2.ProsthoRPDId,
                    DeptId = 7,
                    DoctorId = prosrpdViewModel2.studentAllotmentViewModel.DoctorId,
                    PatientId = (long) prosrpdViewModel2.PatientId,
                    ReferredTreatmentId = 0
                };
                if (!prosrpdViewModel2.Approval1)
                    prosrpdViewModel2.ApprovalType = 1;
                else if (prosrpdViewModel2.Approval1 && !prosrpdViewModel2.Approval2)
                    prosrpdViewModel2.ApprovalType = 2;
                else if (prosrpdViewModel2.Approval1 && prosrpdViewModel2.Approval2 && !prosrpdViewModel2.Approval3)
                    prosrpdViewModel2.ApprovalType = 3;
                prosrpdViewModel2.approvalViewModal.ApprovalTypeId = prosrpdViewModel2.ApprovalType;
                return (ActionResult) this.View("../PROSTHO/_RPDApproval", (object) prosrpdViewModel2);
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
        public ActionResult RPDApproval(PROSRPDViewModel model)
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
                this._service.RPDProcedureApproval(model);
                PROSRPDCasesheet entity = new PROSRPDCasesheet();
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

                entity.ProsthoRPDDate = model.ProsthoRPDDate;
                entity.ProsthoRPDId = model.ProsthoRPDId;
                this._uow.Repository<PROSRPDCasesheet>().Update(entity, false);
                this.TempData["Message"] = (object) "Approved Successfully ";
                return (ActionResult) this.RedirectToAction("ApprovalQueue", new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
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
        [HttpGet]
        public ActionResult FPDApproval(long allotId, int CaserecordId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                PROSFPDViewModel prosfpdViewModel1 = new PROSFPDViewModel();
                PROSFPDViewModel prosfpdViewModel2 =
                    this._service.PROSFPDBindEditPatientModel(3, allotId, CaserecordId);
                prosfpdViewModel2.FileUploadlist = this._uow.Repository<FileUploadViewModel>()
                    .GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) prosfpdViewModel2.PatientId,
                        (object) 7));
                prosfpdViewModel2.approvalViewModal = new ApprovalViewModal()
                {
                    CaserecordId = prosfpdViewModel2.ProsthoFPDId,
                    DeptId = 7,
                    DoctorId = prosfpdViewModel2.studentAllotmentViewModel.DoctorId,
                    PatientId = (long) prosfpdViewModel2.PatientId,
                    ReferredTreatmentId = 0
                };
                if (!prosfpdViewModel2.Approval1)
                    prosfpdViewModel2.ApprovalType = 1;
                else if (prosfpdViewModel2.Approval1 && !prosfpdViewModel2.Approval2)
                    prosfpdViewModel2.ApprovalType = 2;
                else if (prosfpdViewModel2.Approval1 && prosfpdViewModel2.Approval2 && !prosfpdViewModel2.Approval3)
                    prosfpdViewModel2.ApprovalType = 3;
                prosfpdViewModel2.approvalViewModal.ApprovalTypeId = prosfpdViewModel2.ApprovalType;
                return (ActionResult) this.View("../PROSTHO/_FPDApproval", (object) prosfpdViewModel2);
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
        public ActionResult FPDApproval(PROSFPDViewModel model)
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
                this._service.FPDProcedureApproval(model);
                PROSFPDCasesheet entity = new PROSFPDCasesheet();
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

                entity.ProsthoFPDDate = model.ProsthoFPDDate;
                entity.ProsthoFPDId = model.ProsthoFPDId;
                this._uow.Repository<PROSFPDCasesheet>().Update(entity, false);
                this.TempData["Message"] = (object) "Approved Successfully ";
                return (ActionResult) this.RedirectToAction("ApprovalQueue", new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
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
        [HttpGet]
        public ActionResult MFPApproval(long allotId, int CaserecordId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                PROSMFPViewModel prosmfpViewModel1 = new PROSMFPViewModel();
                PROSMFPViewModel prosmfpViewModel2 =
                    this._service.PROSMFPBindEditPatientModel(4, allotId, CaserecordId);
                prosmfpViewModel2.FileUploadlist = this._uow.Repository<FileUploadViewModel>()
                    .GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) prosmfpViewModel2.PatientId,
                        (object) 7));
                prosmfpViewModel2.approvalViewModal = new ApprovalViewModal()
                {
                    CaserecordId = prosmfpViewModel2.ProsthoMFPId,
                    DeptId = 7,
                    DoctorId = prosmfpViewModel2.studentAllotmentViewModel.DoctorId,
                    PatientId = (long) prosmfpViewModel2.PatientId,
                    ReferredTreatmentId = 0
                };
                if (!prosmfpViewModel2.Approval1)
                    prosmfpViewModel2.ApprovalType = 1;
                else if (prosmfpViewModel2.Approval1 && !prosmfpViewModel2.Approval2)
                    prosmfpViewModel2.ApprovalType = 2;
                else if (prosmfpViewModel2.Approval1 && prosmfpViewModel2.Approval2 && !prosmfpViewModel2.Approval3)
                    prosmfpViewModel2.ApprovalType = 3;
                prosmfpViewModel2.approvalViewModal.ApprovalTypeId = prosmfpViewModel2.ApprovalType;
                return (ActionResult) this.View("../PROSTHO/_MFPApproval", (object) prosmfpViewModel2);
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
        public ActionResult MFPApproval(PROSMFPViewModel model)
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
                this._service.MFPProcedureApproval(model);
                PROSMFPCasesheet entity = new PROSMFPCasesheet();
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

                entity.ProsthoMFPDate = model.ProsthoMFPDate;
                entity.ProsthoMFPId = model.ProsthoMFPId;
                this._uow.Repository<PROSMFPCasesheet>().Update(entity, false);
                this.TempData["Message"] = (object) "Approved Successfully ";
                return (ActionResult) this.RedirectToAction("ApprovalQueue", new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
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
        [HttpGet]
        public ActionResult DIMApproval(long allotId, int CaserecordId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(7))
                    return (ActionResult) this.View("../Error/AccessDenied");
                PROSDIMViewModel prosdimViewModel1 = new PROSDIMViewModel();
                PROSDIMViewModel prosdimViewModel2 =
                    this._service.PROSDIMBindEditPatientModel(5, allotId, CaserecordId);
                prosdimViewModel2.FileUploadlist = this._uow.Repository<FileUploadViewModel>()
                    .GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) prosdimViewModel2.PatientId,
                        (object) 7));
                prosdimViewModel2.approvalViewModal = new ApprovalViewModal()
                {
                    CaserecordId = prosdimViewModel2.ProsthoDIMId,
                    DeptId = 7,
                    DoctorId = prosdimViewModel2.studentAllotmentViewModel.DoctorId,
                    PatientId = (long) prosdimViewModel2.PatientId,
                    ReferredTreatmentId = 0
                };
                if (!prosdimViewModel2.Approval1)
                    prosdimViewModel2.ApprovalType = 1;
                else if (prosdimViewModel2.Approval1 && !prosdimViewModel2.Approval2)
                    prosdimViewModel2.ApprovalType = 2;
                else if (prosdimViewModel2.Approval1 && prosdimViewModel2.Approval2 && !prosdimViewModel2.Approval3)
                    prosdimViewModel2.ApprovalType = 3;
                prosdimViewModel2.approvalViewModal.ApprovalTypeId = prosdimViewModel2.ApprovalType;
                return (ActionResult) this.View("../PROSTHO/_DIMApproval", (object) prosdimViewModel2);
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
        public ActionResult DIMApproval(PROSDIMViewModel model)
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
                this._service.DIMProcedureApproval(model);
                PROSDIMCasesheet entity = new PROSDIMCasesheet();
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

                entity.ProsthoDIMDate = model.ProsthoDIMDate;
                entity.ProsthoDIMId = model.ProsthoDIMId;
                this._uow.Repository<PROSDIMCasesheet>().Update(entity, false);
                this.TempData["Message"] = (object) "Approved Successfully ";
                return (ActionResult) this.RedirectToAction("ApprovalQueue", new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
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
                                                           (Func<DeptPermissions, bool>) (x => x.DeptId.Equals(7)))
                                                       .Where<DeptPermissions>((Func<DeptPermissions, bool>) (x =>
                                                           x.PermissionName.Equals(permission)))
                                                       .ToList<DeptPermissions>().Count > 0;
                followupSearchViewModel.From_Date = DateTime.Now;
                followupSearchViewModel.To_Date = DateTime.Now;
                followupSearchViewModel.DeptId = 7;
                followupSearchViewModel.DeptCode = Department.PROSTHO.ToString();
                followupSearchViewModel.DeptName = DeparmentNameDto.StringBusinessUnits(Department.PROSTHO);
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
        public ActionResult UpdateSendForApprovalCd(PROSCDViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int prosthoCdId = model.ProsthoCDId;
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(7))
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
                    this._service.PROSCDUpdatePatient(model);
                    this._service.PROSCDUpdateAllotment(model);
                    this._service.SendApproval(allotId);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSCDSavefollowUp(model);
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
                return (ActionResult) this.RedirectToAction("CDEditOnly", new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = "CDEditOnly",
                    allotId = allotId,
                    PROSCDId = prosthoCdId
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
        public ActionResult InvestigationApprovalCd(PROSCDViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int prosthoCdId = model.ProsthoCDId;
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

            return (ActionResult) this.RedirectToAction("CDApproval", new RouteValueDictionary((object) new
            {
                controller = "PROSTHO",
                action = "CDApproval",
                allotId = allotId,
                CaserecordId = prosthoCdId
            }));
        }

        [HttpPost]
        public ActionResult ReferralApprovalCd(PROSCDViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int prosthoCdId = model.ProsthoCDId;
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

            return (ActionResult) this.RedirectToAction("CDApproval", new RouteValueDictionary((object) new
            {
                controller = "PROSTHO",
                action = "CDApproval",
                allotId = allotId,
                CaserecordId = prosthoCdId
            }));
        }

        [HttpPost]
        public ActionResult TreatmentApprovalCd(PROSCDViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int prosthoCdId = model.ProsthoCDId;
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

            return (ActionResult) this.RedirectToAction("CDApproval", new RouteValueDictionary((object) new
            {
                controller = "PROSTHO",
                action = "CDApproval",
                allotId = allotId,
                CaserecordId = prosthoCdId
            }));
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpPost]
        public ActionResult UpdateSendForApprovalDim(PROSDIMViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int prosthoDimId = model.ProsthoDIMId;
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(7))
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
                    this._service.PROSDIMUpdatePatient(model);
                    this._service.PROSDIMUpdateAllotment(model);
                    this._service.SendApproval(allotId);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSDIMSavefollowUp(model);
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
                return (ActionResult) this.RedirectToAction("DIMEditOnly", new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = "DIMEditOnly",
                    allotId = allotId,
                    PROSDIMId = prosthoDimId
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
        public ActionResult InvestigationApprovalDim(PROSDIMViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int prosthoDimId = model.ProsthoDIMId;
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

            return (ActionResult) this.RedirectToAction("DIMApproval", new RouteValueDictionary((object) new
            {
                controller = "PROSTHO",
                action = "DIMApproval",
                allotId = allotId,
                CaserecordId = prosthoDimId
            }));
        }

        [HttpPost]
        public ActionResult ReferralApprovalDim(PROSDIMViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int prosthoDimId = model.ProsthoDIMId;
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

            return (ActionResult) this.RedirectToAction("DIMApproval", new RouteValueDictionary((object) new
            {
                controller = "PROSTHO",
                action = "DIMApproval",
                allotId = allotId,
                CaserecordId = prosthoDimId
            }));
        }

        [HttpPost]
        public ActionResult TreatmentApprovalDim(PROSDIMViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int prosthoDimId = model.ProsthoDIMId;
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

            return (ActionResult) this.RedirectToAction("DIMApproval", new RouteValueDictionary((object) new
            {
                controller = "PROSTHO",
                action = "DIMApproval",
                allotId = allotId,
                CaserecordId = prosthoDimId
            }));
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpPost]
        public ActionResult UpdateSendForApprovalFpd(PROSFPDViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int prosthoFpdId = model.ProsthoFPDId;
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(7))
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
                    this._service.PROSFPDUpdatePatient(model);
                    this._service.PROSFPDUpdateAllotment(model);
                    this._service.SendApproval(allotId);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSFPDSavefollowUp(model);
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
                return (ActionResult) this.RedirectToAction("FPDEditOnly", new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = "FPDEditOnly",
                    allotId = allotId,
                    PROSFPDId = prosthoFpdId
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
        public ActionResult InvestigationApprovalFpd(PROSFPDViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int prosthoFpdId = model.ProsthoFPDId;
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

            return (ActionResult) this.RedirectToAction("FPDApproval", new RouteValueDictionary((object) new
            {
                controller = "PROSTHO",
                action = "FPDApproval",
                allotId = allotId,
                CaserecordId = prosthoFpdId
            }));
        }

        [HttpPost]
        public ActionResult ReferralApprovalFpd(PROSFPDViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int prosthoFpdId = model.ProsthoFPDId;
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

            return (ActionResult) this.RedirectToAction("FPDApproval", new RouteValueDictionary((object) new
            {
                controller = "PROSTHO",
                action = "FPDApproval",
                allotId = allotId,
                CaserecordId = prosthoFpdId
            }));
        }

        [HttpPost]
        public ActionResult TreatmentApprovalFpd(PROSFPDViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int prosthoFpdId = model.ProsthoFPDId;
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

            return (ActionResult) this.RedirectToAction("FPDApproval", new RouteValueDictionary((object) new
            {
                controller = "PROSTHO",
                action = "FPDApproval",
                allotId = allotId,
                CaserecordId = prosthoFpdId
            }));
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpPost]
        public ActionResult UpdateSendForApprovalRpd(PROSRPDViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int prosthoRpdId = model.ProsthoRPDId;
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(7))
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
                    this._service.PROSRPDUpdatePatient(model);
                    this._service.PROSRPDUpdateAllotment(model);
                    this._service.SendApproval(allotId);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSRPDSavefollowUp(model);
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
                return (ActionResult) this.RedirectToAction("RPDEditOnly", new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = "RPDEditOnly",
                    allotId = allotId,
                    PROSRPDId = prosthoRpdId
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
        public ActionResult InvestigationApprovalRpd(PROSRPDViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int prosthoRpdId = model.ProsthoRPDId;
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

            return (ActionResult) this.RedirectToAction("RPDApproval", new RouteValueDictionary((object) new
            {
                controller = "PROSTHO",
                action = "RPDApproval",
                allotId = allotId,
                CaserecordId = prosthoRpdId
            }));
        }

        [HttpPost]
        public ActionResult ReferralApprovalRpd(PROSRPDViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int prosthoRpdId = model.ProsthoRPDId;
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

            return (ActionResult) this.RedirectToAction("RPDApproval", new RouteValueDictionary((object) new
            {
                controller = "PROSTHO",
                action = "RPDApproval",
                allotId = allotId,
                CaserecordId = prosthoRpdId
            }));
        }

        [HttpPost]
        public ActionResult TreatmentApprovalRpd(PROSRPDViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int prosthoRpdId = model.ProsthoRPDId;
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

            return (ActionResult) this.RedirectToAction("RPDApproval", new RouteValueDictionary((object) new
            {
                controller = "PROSTHO",
                action = "RPDApproval",
                allotId = allotId,
                CaserecordId = prosthoRpdId
            }));
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpPost]
        public ActionResult UpdateSendForApprovalMfp(PROSMFPViewModel model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int prosthoMfpId = model.ProsthoMFPId;
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(7))
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
                    this._service.PROSMFPUpdatePatient(model);
                    this._service.PROSMFPUpdateAllotment(model);
                    this._service.SendApproval(allotId);
                    if (model.followupViewModal != null && model.followupViewModal.chkProcedure)
                        this._service.PROSMFPSavefollowUp(model);
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
                return (ActionResult) this.RedirectToAction("MFPEditOnly", new RouteValueDictionary((object) new
                {
                    controller = "PROSTHO",
                    action = "MFPEditOnly",
                    allotId = allotId,
                    PROSMFPId = prosthoMfpId
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
        public ActionResult InvestigationApprovalMfp(PROSMFPViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int prosthoMfpId = model.ProsthoMFPId;
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

            return (ActionResult) this.RedirectToAction("MFPApproval", new RouteValueDictionary((object) new
            {
                controller = "PROSTHO",
                action = "MFPApproval",
                allotId = allotId,
                CaserecordId = prosthoMfpId
            }));
        }

        [HttpPost]
        public ActionResult ReferralApprovalMfp(PROSMFPViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int prosthoMfpId = model.ProsthoMFPId;
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

            return (ActionResult) this.RedirectToAction("MFPApproval", new RouteValueDictionary((object) new
            {
                controller = "PROSTHO",
                action = "MFPApproval",
                allotId = allotId,
                CaserecordId = prosthoMfpId
            }));
        }

        [HttpPost]
        public ActionResult TreatmentApprovalMfp(PROSMFPViewModel model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int prosthoMfpId = model.ProsthoMFPId;
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

            return (ActionResult) this.RedirectToAction("MFPApproval", new RouteValueDictionary((object) new
            {
                controller = "PROSTHO",
                action = "MFPApproval",
                allotId = allotId,
                CaserecordId = prosthoMfpId
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
                    return (ActionResult) this.View("../Error/AccessDenied");
                return (ActionResult) this.View((object) this._opdService.BindPatientModel(
                    new OPDPatientRegistrationViewModel()
                    {
                        DepartmentId = (int) Department.PROSTHO,
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
