// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.OrthoController
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
    public class OrthoController : BaseController
    {
        private IUnitOfWork _uow;
        private IORTHOCasesheetService _service;
        private IORTHOAnalysisService _Analysis;
        private IMASCodeService _Dropdownservice;
        private IOPDPatientRegistrationService _opdService;


        public OrthoController(IUnitOfWork uow, IORTHOCasesheetService OrthoService, IORTHOAnalysisService Analysis,
            IMASCodeService Dropdownservice, IUserService userservice, IOPDPatientRegistrationService opdService)
            : base(uow, userservice)
        {
            this._uow = uow;
            this._opdService = opdService;
            this._service = OrthoService;
            this._Analysis = Analysis;
            this._Dropdownservice = Dropdownservice;
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
                if (user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(5)))
                        .Where<DeptPermissions>(
                            (Func<DeptPermissions, bool>) (x => x.PermissionName.Equals(permission)))
                        .ToList<DeptPermissions>().Count <= 0)
                    return (ActionResult) this.View("../Error/AccessDenied");
                return (ActionResult) this.View("../ORTHO/Index", (object) new DeptHomeViewModel()
                {
                    From_Date = DateTime.Now,
                    To_Date = DateTime.Now,
                    DeptId = 5,
                    ControllerName = "Ortho"
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

        [CustomAuthorize(Roles = "Admin, HOD, Staff,Student")]
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
                                                  >((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(5)))
                                                  .Where<DeptPermissions>((Func<DeptPermissions, bool>) (x =>
                                                      x.PermissionName.Equals(permission))).ToList<DeptPermissions>()
                                                  .Count > 0;
                if (!user.Departments.Contains(5))
                    return (ActionResult) this.View("../Error/AccessDenied");
                deptHomeViewModel.From_Date = DateTime.Now;
                deptHomeViewModel.To_Date = DateTime.Now;
                deptHomeViewModel.DeptId = 5;
                deptHomeViewModel.ControllerName = "Ortho";
                return (ActionResult) this.View("../ORTHO/TreatmentIndex", (object) deptHomeViewModel);
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
                if (!this.User.Departments.Contains(5))
                    return (ActionResult) this.View("../Error/AccessDenied");
                OrthoViewModal orthoViewModal = new OrthoViewModal();
                return (ActionResult) this.View("../ORTHO/ORTHOPatientTreatmentList",
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
                if (!this.User.Departments.Contains(5))
                    return (ActionResult) this.View("../Error/AccessDenied");
                OrthoViewModal orthoViewModal = new OrthoViewModal();
                return (ActionResult) this.View("../ORTHO/Create",
                    (object) this._service.BindOrthoPatientModel(allotId));
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
        public ActionResult Create(OrthoViewModal model)
        {
            try
            {
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(5))
                    return (ActionResult) this.View("../Error/AccessDenied");
                int num = 0;
                if (this.ModelState.IsValid)
                {
                    model.CreatedBy = user.Identity.Name;
                    model.CasesheetType = 2;
                    num = this._service.SaveORTHOPatient(model);
                    model.OrthoId = num;
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
                    controller = "ORTHO",
                    action = "EditOnly",
                    allotId = model.studentAllotmentViewModel.AllotId,
                    OrthoId = num
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
        public ActionResult ShortCreate(long allotId, int patientId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(5))
                    return (ActionResult) this.View("../Error/AccessDenied");
                OrthoViewModal orthoViewModal = new OrthoViewModal();
                return (ActionResult) this.View("../ORTHO/ShortCreate",
                    (object) this._service.BindOrthoPatientModel(allotId));
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
        public ActionResult ShortCreate(OrthoViewModal model)
        {
            try
            {
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(5))
                    return (ActionResult) this.View("../Error/AccessDenied");
                int num = 0;
                if (this.ModelState.IsValid)
                {
                    model.CreatedBy = user.Identity.Name;
                    model.CasesheetType = 1;
                    num = this._service.SaveORTHOPatient(model);
                    model.OrthoId = num;
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
                    controller = "ORTHO",
                    action = "EditOnly",
                    allotId = model.studentAllotmentViewModel.AllotId,
                    OrthoId = num
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
        public ActionResult Edit(long allotId, int OrthoId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(5))
                    return (ActionResult) this.View("../Error/AccessDenied");
                OrthoViewModal orthoViewModal1 = new OrthoViewModal();
                OrthoViewModal orthoViewModal2 = this._service.BindEditORTHOPatientModel(allotId, OrthoId);
                if (orthoViewModal2.CasesheetType == 1)
                    return (ActionResult) this.View("../ORTHO/ShortEdit", (object) orthoViewModal2);
                return (ActionResult) this.View("../ORTHO/Edit", (object) orthoViewModal2);
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
        public ActionResult Edit(OrthoViewModal model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int orthoId = model.OrthoId;
                if (!this.User.Departments.Contains(5))
                    return (ActionResult) this.View("../Error/AccessDenied");
                if (this.ModelState.IsValid)
                {
                    this._service.UpdateORTHOPatient(model);
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
                    controller = "ORTHO",
                    action = nameof(Edit),
                    allotId = allotId,
                    OrthoId = orthoId
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
        public ActionResult EditOnly(long allotId, int OrthoId)
        {
            try
            {
                this.GetPermissionforUser();
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(5))
                    return (ActionResult) this.View("../Error/AccessDenied");
                OrthoViewModal orthoViewModal1 = new OrthoViewModal();
                OrthoViewModal orthoViewModal2 = this._service.BindEditORTHOPatientModel(allotId, OrthoId);
                IEnumerable<DeptPermissions> source =
                    user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(5)));
                orthoViewModal2.EditCaseSheetAccess =
                    source.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x =>
                            x.PermissionName.Equals(Convert.ToString((object) PermissionsEnum.EditFreezeCase))))
                        .ToList<DeptPermissions>().Count > 0;
                if (orthoViewModal2.CasesheetType == 1)
                    return (ActionResult) this.View("../ORTHO/ShortEdit", (object) orthoViewModal2);
                return (ActionResult) this.View("../ORTHO/Edit", (object) orthoViewModal2);
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
        public ActionResult EditOnly(OrthoViewModal model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int orthoId = model.OrthoId;
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(5))
                    return (ActionResult) this.View("../Error/AccessDenied");
                if (this.ModelState.IsValid)
                {
                    model.ModifiedBy = user.Identity.Name;
                    this._service.UpdateORTHOPatient(model);
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
                return (ActionResult) this.RedirectToAction(nameof(EditOnly), new RouteValueDictionary((object) new
                {
                    controller = "ORTHO",
                    action = nameof(EditOnly),
                    allotId = allotId,
                    OrthoId = orthoId
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
                if (user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(5)))
                        .ToList<DeptPermissions>().Count <= 0)
                    return (ActionResult) this.View("../Error/AccessDenied");
                return (ActionResult) this.View("../ORTHO/_ORTHOSearchOptions", (object) new OrthoSearchViewModel()
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

        public JsonResult GetOrthoSearchList(OrthoSearchViewModel model)
        {
            string From_Date = model.From_Date.ToString("yyyy-MM-dd");
            string To_Date = model.To_Date.ToString("yyyy-MM-dd");
            string rootUrl = this.User.GetRootUrl();
            model.SearchDetails = this._service.orthoSearchDetails(From_Date, To_Date, rootUrl);
            return this.Json((object) model.SearchDetails);
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
                if (user.DeptPermission.Where<DeptPermissions>((Func<DeptPermissions, bool>) (x => x.DeptId.Equals(5)))
                        .Where<DeptPermissions>(
                            (Func<DeptPermissions, bool>) (x => x.PermissionName.Equals(permission)))
                        .ToList<DeptPermissions>().Count <= 0)
                    return (ActionResult) this.View("../Error/AccessDenied");
                return (ActionResult) this.View("../ORTHO/_ORTHOApprovalQueue",
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

        public JsonResult GetORTHOApprovalList(CasesheetApprovalListViewModel model)
        {
            string From_Date = model.From_Date.ToString("yyyy-MM-dd");
            string To_Date = model.To_Date.ToString("yyyy-MM-dd");
            string rootUrl = this.User.GetRootUrl();
            model.SearchDetails = this._service.orthoCasesheetApprovalList(From_Date, To_Date, rootUrl);
            return this.Json((object) model.SearchDetails);
        }

        public ActionResult Analysis(int Id, int Analysis = 0)
        {
            try
            {
                this.GetPermissionforUser();
                OrthoAnalysisViewModal analysisViewModal1 = new OrthoAnalysisViewModal();
                analysisViewModal1.OrthoId = Id;
                analysisViewModal1.AnalysisId = Analysis;
                switch (Analysis)
                {
                    case 1:
                        OrthoAnalysisViewModal analysisViewModal2 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal2.SteinerAnalysisList =
                            this._Analysis.SteinerAnalysisList(Id).ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/SteinerEdit", (object) analysisViewModal2);
                    case 2:
                        OrthoAnalysisViewModal analysisViewModal3 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal3.DentoUpperAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.DentoUpperAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/DentoUpperEdit", (object) analysisViewModal3);
                    case 3:
                        OrthoAnalysisViewModal analysisViewModal4 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal4.DownsAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.DownsAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/DownsEdit", (object) analysisViewModal4);
                    case 4:
                        OrthoAnalysisViewModal analysisViewModal5 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal5.SchwarzAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.SchwarzAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/SchwarzEdit", (object) analysisViewModal5);
                    case 5:
                        OrthoAnalysisViewModal analysisViewModal6 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal6.RakosiAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.RakosiAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/RakosiEdit", (object) analysisViewModal6);
                    case 6:
                        OrthoAnalysisViewModal analysisViewModal7 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal7.BurstoneHardAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.BurstoneHardAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/BurstoneHardEdit", (object) analysisViewModal7);
                    case 7:
                        OrthoAnalysisViewModal analysisViewModal8 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal8.BurstoneSoftAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.BurstoneSoftAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/BurstoneSoftEdit", (object) analysisViewModal8);
                    case 8:
                        OrthoAnalysisViewModal analysisViewModal9 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal9.GrummonsAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.GrummonsAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/GrummonsEdit", (object) analysisViewModal9);
                    case 9:
                        OrthoAnalysisViewModal analysisViewModal10 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal10.EstheticsAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.EstheticsAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/EstheticsEdit", (object) analysisViewModal10);
                    case 10:
                        OrthoAnalysisViewModal analysisViewModal11 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal11.HoldawayAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.HoldawayAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/HoldawaySoftEdit", (object) analysisViewModal11);
                    case 11:
                        OrthoAnalysisViewModal analysisViewModal12 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal12.ArnettAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.ArnettAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/ArnettsSoftEdit", (object) analysisViewModal12);
                    case 12:
                        OrthoAnalysisViewModal analysisViewModal13 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal13.MaxillaAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.MaxillaAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        analysisViewModal13.MandibleAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.MandibleAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/CranialBaseEdit", (object) analysisViewModal13);
                    case 13:
                        OrthoAnalysisViewModal analysisViewModal14 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal14.MaxtoMandAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.MaxtoMandAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/MaxillaMandibleEdit", (object) analysisViewModal14);
                    case 14:
                        OrthoAnalysisViewModal analysisViewModal15 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal15.GrowthAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.GrowthAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/GrowthPatternEdit", (object) analysisViewModal15);
                    case 15:
                        OrthoAnalysisViewModal analysisViewModal16 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal16.DentoLowerAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.DentoLowerAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/DentoLowerEdit", (object) analysisViewModal16);
                    case 16:
                        OrthoAnalysisViewModal analysisViewModal17 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal17.SoftTissueAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.SoftTissueAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/SoftTissueEdit", (object) analysisViewModal17);
                    case 17:
                        OrthoAnalysisViewModal analysisViewModal18 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal18.TweedsAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.TweedsAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/TweedsEdit", (object) analysisViewModal18);
                    case 18:
                        OrthoAnalysisViewModal analysisViewModal19 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal19.RickettsAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.RickettsAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/RickettsEdit", (object) analysisViewModal19);
                    case 19:
                        OrthoAnalysisViewModal analysisViewModal20 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal20.McNamaraAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.McNamaraAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/McNamaraEdit", (object) analysisViewModal20);
                    case 20:
                        OrthoAnalysisViewModal analysisViewModal21 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal21.BjroksAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.BjroksAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/BjorksEdit", (object) analysisViewModal21);
                    case 21:
                        OrthoAnalysisViewModal analysisViewModal22 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal22.SagittalAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.SagittalAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        analysisViewModal22.UpperLipList = this._Dropdownservice.GetCodesById(50);
                        analysisViewModal22.SoftTissueList = this._Dropdownservice.GetCodesById(51);
                        analysisViewModal22.SkeletalList = this._Dropdownservice.GetCodesById(52);
                        analysisViewModal22.SeverityList = this._Dropdownservice.GetCodesById(53);
                        analysisViewModal22.InferSoftTissueList = this._Dropdownservice.GetCodesById(54);
                        return (ActionResult) this.View("../ORTHO/SagittalEdit", (object) analysisViewModal22);
                    case 22:
                        analysisViewModal1.AnalysisId = Analysis;
                        return (ActionResult) this.View("../ORTHO/DiscrepancyEdit",
                            (object) this._Analysis.BindDiscrepancy(Id, Analysis));
                    case 23:
                        OrthoAnalysisViewModal analysisViewModal23 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal23.VerticalAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.VerticalAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        analysisViewModal23.SkeletSagittalList = this._Dropdownservice.GetCodesById(55);
                        analysisViewModal23.SkeletAlterationList = this._Dropdownservice.GetCodesById(56);
                        return (ActionResult) this.View("../ORTHO/VerticalEdit", (object) analysisViewModal23);
                    case 24:
                        OrthoAnalysisViewModal analysisViewModal24 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal24.AnalysisId = Analysis;
                        return (ActionResult) this.View("../ORTHO/WitsEdit", (object) analysisViewModal24);
                    case 25:
                        OrthoAnalysisViewModal analysisViewModal25 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal25.CompositeAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.CompositeAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/CompositeSoftEdit", (object) analysisViewModal25);
                    case 26:
                        return (ActionResult) this.View("../ORTHO/BoltonEdit",
                            (object) this._Analysis.BindDetails(Id, Analysis));
                    case 27:
                        return (ActionResult) this.View("../ORTHO/CareysEdit",
                            (object) this._Analysis.BindDetails(Id, Analysis));
                    case 28:
                        this._Analysis.sumIncisor(Id);
                        OrthoAnalysisViewModal analysisViewModal26 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal26.AnalysisId = Analysis;
                        return (ActionResult) this.View("../ORTHO/PontsEdit", (object) analysisViewModal26);
                    case 29:
                        return (ActionResult) this.View("../ORTHO/AshleyHowesEdit",
                            (object) this._Analysis.BindDetails(Id, Analysis));
                    case 30:
                        OrthoAnalysisViewModal analysisViewModal27 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal27.DiscrepancyAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.DiscrepancyAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        analysisViewModal27.SkeletAlterationList = this._Dropdownservice.GetCodesById(56);
                        return (ActionResult) this.View("../ORTHO/DiscrepancySagittalEdit",
                            (object) analysisViewModal27);
                    case 31:
                        OrthoAnalysisViewModal analysisViewModal28 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal28.SKeletalAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.SKeletalAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/SkeletalEdit", (object) analysisViewModal28);
                    case 32:
                        OrthoAnalysisViewModal analysisViewModal29 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal29.DivergenceAnalysisList =
                            (IEnumerable<OrthoAnalysisViewModal>) this._Analysis.DivergenceAnalysisList(Id)
                                .ToList<OrthoAnalysisViewModal>();
                        return (ActionResult) this.View("../ORTHO/DivergenceEdit", (object) analysisViewModal29);
                    case 33:
                        OrthoAnalysisViewModal analysisViewModal30 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal30.AnalysisId = Analysis;
                        return (ActionResult) this.View("../ORTHO/MoyersEdit", (object) analysisViewModal30);
                    case 34:
                        OrthoAnalysisViewModal analysisViewModal31 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal31.AnalysisId = Analysis;
                        return (ActionResult) this.View("../ORTHO/TanakaJohnstonEdit", (object) analysisViewModal31);
                    case 35:
                        OrthoAnalysisViewModal analysisViewModal32 = this._Analysis.BindDetails(Id, Analysis);
                        analysisViewModal32.AnalysisId = Analysis;
                        return (ActionResult) this.View("../ORTHO/RadiographicEdit", (object) analysisViewModal32);
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

        public ActionResult SaveSteiner(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveSteiner(modal);
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

                modal.SteinerAnalysisList =
                    this._Analysis.SteinerAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/SteinerEdit", (object) modal);
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

        public ActionResult SaveDowns(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveDowns(modal);
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

                modal.DownsAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .DownsAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/DownsEdit", (object) modal);
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

        public ActionResult SaveSchwarz(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveSchwarz(modal);
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

                modal.SchwarzAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .SchwarzAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/SchwarzEdit", (object) modal);
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

        public ActionResult SaveRakosi(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveRakosi(modal);
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

                modal.RakosiAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .RakosiAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/RakosiEdit", (object) modal);
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

        public ActionResult SaveBurstoneHard(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveBurstoneHard(modal);
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

                modal.BurstoneHardAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .BurstoneHardAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/BurstoneHardEdit", (object) modal);
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

        public ActionResult SaveBurstoneSoft(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveBurstoneSoft(modal);
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

                modal.BurstoneSoftAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .BurstoneSoftAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/BurstoneSoftEdit", (object) modal);
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

        public ActionResult SaveGrummons(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveGrummons(modal);
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

                modal.GrummonsAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .GrummonsAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/GrummonsEdit", (object) modal);
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

        public ActionResult SaveEsthetics(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveEsthetics(modal);
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

                modal.EstheticsAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .EstheticsAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/EstheticsEdit", (object) modal);
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

        public ActionResult SaveHoldaway(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveHoldaway(modal);
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

                modal.HoldawayAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .HoldawayAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/HoldawaySoftEdit", (object) modal);
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

        public ActionResult SaveArnett(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveArnett(modal);
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

                modal.ArnettAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .ArnettAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/ArnettsSoftEdit", (object) modal);
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

        public ActionResult SaveCranial(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveCranial(modal);
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

                modal.MaxillaAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .MaxillaAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.MandibleAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .MandibleAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/CranialBaseEdit", (object) modal);
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

        public ActionResult SaveMaxMand(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveMaxMand(modal);
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

                modal.MaxtoMandAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .MaxtoMandAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/MaxillaMandibleEdit", (object) modal);
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

        public ActionResult SaveGrowth(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveGrowth(modal);
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

                modal.GrowthAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .GrowthAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/GrowthPatternEdit", (object) modal);
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

        public ActionResult SaveDentoLower(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveDentoLower(modal);
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

                modal.DentoLowerAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .DentoLowerAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/DentoLowerEdit", (object) modal);
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

        public ActionResult SaveSoftTissue(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveSoftTissue(modal);
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

                modal.SoftTissueAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .SoftTissueAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/SoftTissueEdit", (object) modal);
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

        public ActionResult SaveTweeds(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveTweeds(modal);
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

                modal.TweedsAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .TweedsAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/TweedsEdit", (object) modal);
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

        public ActionResult SaveRicketts(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveRicketts(modal);
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

                modal.RickettsAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .RickettsAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/RickettsEdit", (object) modal);
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

        public ActionResult SaveMcNamara(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveMcNamara(modal);
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

                modal.McNamaraAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .McNamaraAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/McNamaraEdit", (object) modal);
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

        public ActionResult SaveBjroks(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveBjroks(modal);
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

                modal.BjroksAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .BjroksAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/BjorksEdit", (object) modal);
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

        public ActionResult SaveSagittal(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveSagittal(modal);
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

                modal.SagittalAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .SagittalAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.UpperLipList = this._Dropdownservice.GetCodesById(50);
                modal.SoftTissueList = this._Dropdownservice.GetCodesById(51);
                modal.SkeletalList = this._Dropdownservice.GetCodesById(52);
                modal.SeverityList = this._Dropdownservice.GetCodesById(53);
                modal.InferSoftTissueList = this._Dropdownservice.GetCodesById(54);
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/SagittalEdit", (object) modal);
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

        public ActionResult SaveDiscrepancy(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveDiscrepancy(modal);
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

                modal.DiscrepancyAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .DiscrepancyAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.SkeletAlterationList = this._Dropdownservice.GetCodesById(56);
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/DiscrepancySagittalEdit", (object) modal);
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

        public ActionResult SaveVerticalRelation(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveVerticalRelation(modal);
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

                modal.VerticalAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .VerticalAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.SkeletSagittalList = this._Dropdownservice.GetCodesById(55);
                modal.SkeletAlterationList = this._Dropdownservice.GetCodesById(56);
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/VerticalEdit", (object) modal);
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

        public ActionResult SaveComposite(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveComposite(modal);
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

                modal.CompositeAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .CompositeAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/CompositeSoftEdit", (object) modal);
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

        public ActionResult SaveDentoUpper(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveDentoUpper(modal);
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

                modal.DentoUpperAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .DentoUpperAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/DentoUpperEdit", (object) modal);
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

        public ActionResult SaveSkeletal(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveSkeletal(modal);
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

                modal.SKeletalAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .SKeletalAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/SkeletalEdit", (object) modal);
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

        public ActionResult SaveJawBases(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveJawBases(modal);
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

                modal.DivergenceAnalysisList = (IEnumerable<OrthoAnalysisViewModal>) this._Analysis
                    .DivergenceAnalysisList(modal.OrthoId).ToList<OrthoAnalysisViewModal>();
                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/DivergenceEdit", (object) modal);
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

        public ActionResult SaveBolton(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveModalAnalysis(modal);
                    modal.AnalysisDisplayName =
                        this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
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

                return (ActionResult) this.View("../ORTHO/BoltonEdit", (object) modal);
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

        public ActionResult SavePonts(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveModalAnalysis(modal);
                    modal.AnalysisDisplayName =
                        this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
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

                return (ActionResult) this.View("../ORTHO/PontsEdit", (object) modal);
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

        public ActionResult SaveAshleyHowes(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveModalAnalysis(modal);
                    modal.AnalysisDisplayName =
                        this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
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

                return (ActionResult) this.View("../ORTHO/AshleyHowesEdit", (object) modal);
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

        public ActionResult SaveModalDiscrepancy(OrthoAnalysisViewModal modal)
        {
            try
            {
                int orthoId = modal.OrthoId;
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveModalAnalysis(modal);
                    modal.AnalysisDisplayName =
                        this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
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

                modal.MidlineList = this._Dropdownservice.GetCodesById(49);
                return (ActionResult) this.View("../ORTHO/DiscrepancyEdit", (object) modal);
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

        public ActionResult SaveCareys(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveModalAnalysis(modal);
                    modal.AnalysisDisplayName =
                        this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
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

                return (ActionResult) this.View("../ORTHO/CareysEdit", (object) modal);
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

        public ActionResult SaveWits(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveModalAnalysis(modal);
                    modal.AnalysisDisplayName =
                        this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
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

                return (ActionResult) this.View("../ORTHO/WitsEdit", (object) modal);
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

        public ActionResult SaveMoyer(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveModalAnalysis(modal);
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

                modal.AnalysisDisplayName =
                    this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
                return (ActionResult) this.View("../ORTHO/MoyersEdit", (object) modal);
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

        public ActionResult SaveTanaka(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveModalAnalysis(modal);
                    modal.AnalysisDisplayName =
                        this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
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

                return (ActionResult) this.View("../ORTHO/TanakaJohnstonEdit", (object) modal);
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

        public ActionResult SaveRadiographic(OrthoAnalysisViewModal modal)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    this._Analysis.SaveModalAnalysis(modal);
                    modal.AnalysisDisplayName =
                        this._uow.Repository<ORTHOAnalysis>().Get(modal.AnalysisId).AnalysisDisplayName;
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

                return (ActionResult) this.View("../ORTHO/RadiographicEdit", (object) modal);
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

        public ActionResult OrthoAnalysisReport(int OrthoId, int AnalysisId = 0)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(19))
                    return (ActionResult) this.View("../Error/AccessDenied");
                OrthoAnalysisViewModal analysisViewModal = new OrthoAnalysisViewModal();
                return (ActionResult) this.View("../Reports/OrthoAnalysisReport",
                    (object) this._Analysis.BindReportAnalysisDetails(OrthoId));
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

        public ActionResult Report(int Id)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(19))
                    return (ActionResult) this.View("../Error/AccessDenied");
                OrthoViewModal orthoViewModal1 = new OrthoViewModal();
                OrthoViewModal orthoViewModal2 = this._service.BindORTHOPatientReport(Id);
                if (orthoViewModal2.CasesheetType == 2)
                    return (ActionResult) this.View("../Reports/ORTHOCaserecordReport", (object) orthoViewModal2);
                return (ActionResult) this.View("../Reports/ORTHOShortCaserecordReport", (object) orthoViewModal2);
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
        public ActionResult Approval(long allotId, int CaserecordId)
        {
            try
            {
                this.GetPermissionforUser();
                if (!this.User.Departments.Contains(5))
                    return (ActionResult) this.View("../Error/AccessDenied");
                OrthoViewModal orthoViewModal1 = new OrthoViewModal();
                OrthoViewModal orthoViewModal2 = this._service.BindEditORTHOPatientModel(allotId, CaserecordId);
                orthoViewModal2.FileUploadlist = this._uow.Repository<FileUploadViewModel>()
                    .GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) orthoViewModal2.PatientId,
                        (object) 5));
                orthoViewModal2.approvalViewModal = new ApprovalViewModal()
                {
                    CaserecordId = orthoViewModal2.OrthoId,
                    DeptId = 5,
                    DoctorId = orthoViewModal2.studentAllotmentViewModel.DoctorId,
                    PatientId = (long) orthoViewModal2.PatientId,
                    ReferredTreatmentId = 0
                };
                if (!orthoViewModal2.Approval1)
                    orthoViewModal2.ApprovalType = 1;
                else if (orthoViewModal2.Approval1 && !orthoViewModal2.Approval2)
                    orthoViewModal2.ApprovalType = 2;
                else if (orthoViewModal2.Approval1 && orthoViewModal2.Approval2 && !orthoViewModal2.Approval3)
                    orthoViewModal2.ApprovalType = 3;
                orthoViewModal2.approvalViewModal.ApprovalTypeId = orthoViewModal2.ApprovalType;
                return (ActionResult) this.View("../ORTHO/Approval", (object) orthoViewModal2);
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
        public ActionResult Approval(OrthoViewModal model)
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
                ORTHOCasesheet entity = new ORTHOCasesheet();
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

                entity.OrthoDate = model.OrthoDate;
                entity.OrthoId = model.OrthoId;
                this._uow.Repository<ORTHOCasesheet>().Update(entity, false);
                this.TempData["Message"] = (object) "Approved Successfully ";
                return (ActionResult) this.RedirectToAction("ApprovalQueue", new RouteValueDictionary((object) new
                {
                    controller = "ORTHO",
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
                                                           (Func<DeptPermissions, bool>) (x => x.DeptId.Equals(5)))
                                                       .Where<DeptPermissions>((Func<DeptPermissions, bool>) (x =>
                                                           x.PermissionName.Equals(permission)))
                                                       .ToList<DeptPermissions>().Count > 0;
                followupSearchViewModel.From_Date = DateTime.Now;
                followupSearchViewModel.To_Date = DateTime.Now;
                followupSearchViewModel.DeptId = 5;
                followupSearchViewModel.DeptCode = Department.ORTHO.ToString();
                followupSearchViewModel.DeptName = DeparmentNameDto.StringBusinessUnits(Department.ORTHO);
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
        public ActionResult UpdateSendForApproval(OrthoViewModal model)
        {
            try
            {
                long allotId = model.studentAllotmentViewModel.AllotId;
                int orthoId = model.OrthoId;
                CustomPrincipal user = this.User;
                if (!user.Departments.Contains(5))
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
                    this._service.UpdateORTHOPatient(model);
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
                    controller = "Ortho",
                    action = "EditOnly",
                    allotId = allotId,
                    OrthoId = orthoId
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
        public ActionResult InvestigationApproval(OrthoViewModal model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int orthoId = model.OrthoId;
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
                controller = "Ortho",
                action = "Approval",
                allotId = allotId,
                CaserecordId = orthoId
            }));
        }

        [HttpPost]
        public ActionResult ReferralApproval(OrthoViewModal model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int orthoId = model.OrthoId;
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
                controller = "Ortho",
                action = "Approval",
                allotId = allotId,
                CaserecordId = orthoId
            }));
        }

        [HttpPost]
        public ActionResult TreatmentApproval(OrthoViewModal model)
        {
            CustomPrincipal user = this.User;
            long allotId = model.studentAllotmentViewModel.AllotId;
            int orthoId = model.OrthoId;
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
                controller = "Ortho",
                action = "Approval",
                allotId = allotId,
                CaserecordId = orthoId
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
