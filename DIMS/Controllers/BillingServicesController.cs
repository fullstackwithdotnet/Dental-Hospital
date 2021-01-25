// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.BillingServicesController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using AutoMapper;
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
    public class BillingServicesController : BaseController
    {
        private IUnitOfWork _uow;
        private IMASBillingServicesService _service;
        private ICasesheetNoService _CasesheetNoService;

        public BillingServicesController(IUnitOfWork uow, IMASBillingServicesService service,
            ICasesheetNoService CasesheetNoService, IUserService userservice)
            : base(uow, userservice)
        {
            _uow = uow;
            _service = service;
            _CasesheetNoService = CasesheetNoService;
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<BillingServicesViewModal, MASBillingServices>();
                cfg.CreateMap<MASBillingServices, BillingServicesViewModal>();
                cfg.CreateMap<BillingSubServicesViewModal, MASBillingSubServices>();
                cfg.CreateMap<MASBillingSubServices, BillingSubServicesViewModal>();
            });
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpGet]
        public ActionResult Index()
        {
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            return View(new BillingServicesViewModal()
            {
                DepartmentList = _uow.Repository<MASDepartment>()
                    .GetEntitiesBySql(string.Format(Queries.LoadServiceDept, 0))
            });
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpGet]
        public ActionResult SubServicesList()
        {
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            return View(new BillingSubServicesViewModal()
            {
                DepartmentList = _uow.Repository<MASDepartment>()
                    .GetEntitiesBySql(string.Format(Queries.LoadServiceDept, 0))
            });
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpGet]
        public ActionResult Create()
        {
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            var servicesViewModal = new BillingServicesViewModal();
            servicesViewModal.DepartmentList = _uow.Repository<MASDepartment>()
                .GetEntitiesBySql(string.Format(Queries.LoadServiceDept, 0));
            var selectListItemList = new List<SelectListItem>();
            IEnumerable<MASGroup> source =
                _uow.Repository<MASGroup>().GetAll().OrderBy(x => x.GroupName);
            servicesViewModal.GroupList = source.Select(x => new SelectListItem()
            {
                Text = x.GroupName,
                Value = x.GroupId.ToString()
            }).ToList();
            servicesViewModal.Radio = 16;
            servicesViewModal.Lab = 20;
            return View(servicesViewModal);
        }


        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpGet]
        public ActionResult CreateChildService()
        {
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            var servicesViewModal = new BillingSubServicesViewModal
            {
                DepartmentList = _uow.Repository<MASDepartment>()
                    .GetEntitiesBySql(string.Format(Queries.LoadServiceDept, 0))
            };
           
            return View(servicesViewModal);
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpPost]
        public ActionResult Create(BillingServicesViewModal model)
        {
            try
            {
                if (!User.Departments.Contains(17))
                    return View("../Error/AccessDenied");
                if (ModelState.IsValid)
                {
                    if (_service.Checkservicename(model.Service, model.DeptId))
                    {
                        TempData["Message"] = "service Name already Exist";
                    }
                    else
                    {
                        var entity = Mapper.Map<BillingServicesViewModal, MASBillingServices>(model);
                        entity.ServiceName = model.Service;
                        if (model.GSTPercentage == null)
                            entity.GSTPercentage = "0";
                        entity.ServiceCode = _CasesheetNoService.GetBillingServiceNo(entity.DeptId);
                        _uow.Repository<MASBillingServices>().Add(entity, false);
                    }
                }
                else
                {
                    foreach (var modelState in ViewData.ModelState.Values)
                    {
                        using (var enumerator = modelState.Errors.GetEnumerator())
                        {
                            if (enumerator.MoveNext())
                            {
                                var current = enumerator.Current;
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

                return RedirectToAction(nameof(Create));
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpPost]
        public ActionResult CreateChildService(BillingSubServicesViewModal model)
        {
            try
            {
                if (!User.Departments.Contains(17))
                    return View("../Error/AccessDenied");
                if (ModelState.IsValid)
                {
                    if (_service.CheckChildServiceName(model.ServiceName, model.DeptId, model.ParentId))
                    {
                        TempData["Message"] = "service Name already Exist";
                    }
                    else
                    {
                        var entity = Mapper.Map<BillingSubServicesViewModal, MASBillingSubServices>(model);
                        //entity.ServiceName = model.Service;
                        if (model.GSTPercentage == null)
                            entity.GSTPercentage = "0";
                        entity.ServiceCode = _CasesheetNoService.GetBillingServiceNo(entity.DeptId);
                        _uow.Repository<MASBillingSubServices>().Add(entity, false);
                    }
                }
                else
                {
                    foreach (var modelState in ViewData.ModelState.Values)
                    {
                        using (var enumerator = modelState.Errors.GetEnumerator())
                        {
                            if (enumerator.MoveNext())
                            {
                                var current = enumerator.Current;
                                return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
                                {
                                    controller = "Error",
                                    action = "ErrorWrite",
                                    message = (current.ErrorMessage + "-" + (current.Exception as object))
                                }));
                            }
                        }
                    }
                }

                return RedirectToAction(nameof(CreateChildService));
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        public ActionResult Edit(int ServiceId)
        {
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            var servicesViewModal = Mapper.Map<MASBillingServices, BillingServicesViewModal>(_service.Get(ServiceId));
            servicesViewModal.DepartmentList = _uow.Repository<MASDepartment>()
                .GetEntitiesBySql(string.Format(Queries.LoadServiceDept, 0));
            var whereClause = "DeptId=" + servicesViewModal.DeptId;
            var selectListItemList = new List<SelectListItem>();
            var source = _uow.Repository<MASGroup>().GetAll(whereClause).OrderBy(x => x.GroupName);
            servicesViewModal.GroupList = source.Select(x => new SelectListItem()
            {
                Text = x.GroupName,
                Value = x.GroupId.ToString()
            }).ToList();
            servicesViewModal.Radio = 16;
            servicesViewModal.Lab = 20;
            return View(nameof(Edit), servicesViewModal);
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        public ActionResult EditChildService(int ServiceId, int DepartmentId, int ParentId)
        {
            GetPermissionforUser();
            if (!User.Departments.Contains(17))
                return View("../Error/AccessDenied");
            var sql =
                $"select * from MASBillingSubServices where DeptId = '{DepartmentId}'  and ParentId = '{ParentId}' and ServiceId = '{ServiceId}'";
            var servicesViewModal = Mapper.Map<MASBillingSubServices,BillingSubServicesViewModal>(_uow.Repository<MASBillingSubServices>().GetEntitiesBySql(sql).FirstOrDefault());
            servicesViewModal.DepartmentList = _uow.Repository<MASDepartment>()
                .GetEntitiesBySql(string.Format(Queries.LoadServiceDept, 0));
            return View(nameof(EditChildService), servicesViewModal);
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpPost]
        public ActionResult Edit(BillingServicesViewModal model)
        {
            try
            {
                if (!User.Departments.Contains(17))
                    return View("../Error/AccessDenied");
                var entity = Mapper.Map<BillingServicesViewModal, MASBillingServices>(model);
                if (model.GSTPercentage == null)
                    entity.GSTPercentage = "0";
                _uow.Repository<MASBillingServices>().Update(entity, false);
                if (model.DeptId == 16 || model.DeptId == 20)
                    _uow.Repository<MASDepartment>().GetEntitiesBySql(string.Format(Queries.UpdateGroupInRadioTemplate,
                        entity.GroupId, entity.ServiceId));
                return RedirectToAction(nameof(Edit), new RouteValueDictionary(new
                {
                    controller = "BillingServices",
                    action = nameof(Edit),
                    ServiceId = model.ServiceId
                }));
            }
            catch
            {
                return View();
            }
        }

        [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
        [HttpPost]
        public ActionResult EditChildService(BillingSubServicesViewModal model)
        {
            try
            {
                if (!User.Departments.Contains(17))
                    return View("../Error/AccessDenied");
                var entity = Mapper.Map<BillingSubServicesViewModal, MASBillingSubServices>(model);
                //entity.ServiceName = model.Service;
                if (model.GSTPercentage == null)
                    entity.GSTPercentage = "0";
                entity.ServiceCode = _CasesheetNoService.GetBillingServiceNo(entity.DeptId);
                _uow.Repository<MASBillingSubServices>().Update(entity, false);


                return RedirectToAction(nameof(EditChildService), new
                {
                    ServiceId = model.ServiceId,
                    DepartmentId = model.DeptId,
                    ParentId = model.ParentId
                });
            }
            catch
            {
                return View();
            }
        }

        public JsonResult GetServiceDetailsById(int? DeptId)
        {
            var servicesViewModal = new BillingServicesViewModal();
            if (DeptId.HasValue)
            {
                var nullable = DeptId;
                var num = 0;
                if ((nullable.GetValueOrDefault() == num ? (!nullable.HasValue ? 1 : 0) : 1) != 0)
                {
                    var whereClause = "DeptId=" + DeptId;
                    var rootUrl = User.GetRootUrl();
                    servicesViewModal.ServicesList = _uow.Repository<MASBillingServices>()
                        .GetEntitiesBySql(string.Format(Queries.ServiceDeptSearch, DeptId, rootUrl, 0.ToString()));
                    var selectListItemList = new List<SelectListItem>();
                    IEnumerable<MASGroup> source = _uow.Repository<MASGroup>().GetAll(whereClause)
                        .OrderBy(x => x.GroupName);
                    servicesViewModal.GroupList = source.Select(x => new SelectListItem()
                    {
                        Text = x.GroupName,
                        Value = x.GroupId.ToString()
                    }).ToList();
                }
            }
            return Json(servicesViewModal);
        }


        public JsonResult GetSubServiceDetailByParent(int? departmentId, int? parentId)
        {
            var servicesViewModal = new List<MASBillingSubServices>();
            if (departmentId.HasValue && parentId.HasValue)
            {
                var num = 0;
                if ((departmentId.GetValueOrDefault() == num ? (!departmentId.HasValue ? 1 : 0) : 1) == 0)
                    return Json(servicesViewModal);
                var rootUrl = User.GetRootUrl();
                servicesViewModal = _uow.Repository<MASBillingSubServices>()
                    .GetEntitiesBySql(string.Format(Queries.ChildServiceDeptSearch, departmentId, rootUrl, 0.ToString(), parentId)).ToList();
            }
            return Json(servicesViewModal);
        }

        public JsonResult CheckServiceName(string Service, int DeptId)
        {
            IEnumerable<MASBillingSubServices> masBillingServiceses = new List<MASBillingSubServices>();
            if (Service != null && !string.IsNullOrEmpty(Service))
                masBillingServiceses = _uow.Repository<MASBillingSubServices>()
                    .GetAll("ServiceName ='" + Service.Trim() + "' and DeptId =" + DeptId ?? "");
            return Json(masBillingServiceses);
        }

        public JsonResult DeleteService(int ServiceId)
        {
            var servicesViewModal = Mapper.Map<MASBillingServices, BillingServicesViewModal>(_service.Get(ServiceId));
            if (servicesViewModal != null)
            {
                var entity = Mapper.Map<BillingServicesViewModal, MASBillingServices>(servicesViewModal);
                if (servicesViewModal.GSTPercentage == null)
                    entity.GSTPercentage = "0";
                entity.DelInd = 1;
                _uow.Repository<MASBillingServices>().Update(entity, false);
                return Json(new{Success=true});
            }
            return Json(new { Success = false });

        }

        public JsonResult DeleteSubService(int ServiceId)
        {
            var servicesViewModal = Mapper.Map<MASBillingSubServices, BillingSubServicesViewModal>(_service.getChildService(ServiceId));
            if (servicesViewModal != null)
            {
                var entity = Mapper.Map<BillingSubServicesViewModal, MASBillingSubServices>(servicesViewModal);
                if (servicesViewModal.GSTPercentage == null)
                    entity.GSTPercentage = "0";
                entity.DelInd = 1;
                _uow.Repository<MASBillingSubServices>().Update(entity, false);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });

        }
    }
}
