// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.DoctorController
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
  public class DoctorController : BaseController
  {
    private IUnitOfWork _uow;
    private IMASDoctorService _service;

    public DoctorController(IUnitOfWork uow, IMASDoctorService service, IUserService userservice)
      : base(uow, userservice)
    {
      this._uow = uow;
      this._service = service;
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg =>
      {
        cfg.CreateMap<DoctorViewModal, MASDoctor>();
        cfg.CreateMap<MASDoctor, DoctorViewModal>();
      }));
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Index()
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      return (ActionResult) this.View((object) new DoctorViewModal()
      {
        DepartmentList = this._uow.Repository<MASDepartment>().GetEntitiesBySql(string.Format(Queries.LoadServiceDept, (object) 0))
      });
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Create()
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      return (ActionResult) this.View((object) new DoctorViewModal()
      {
        DepartmentList = this._uow.Repository<MASDepartment>().GetEntitiesBySql(string.Format(Queries.LoadDept, (object) 0)),
        DesignationList = (IEnumerable<MASDesignation>) this._uow.Repository<MASDesignation>().GetAll().OrderBy<MASDesignation, string>((Func<MASDesignation, string>) (A => A.DesigName))
      });
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult Create(DoctorViewModal model)
    {
      try
      {
        if (!this.User.Departments.Contains(17))
          return (ActionResult) this.View("../Error/AccessDenied");
        if (this.ModelState.IsValid)
        {
          if (this._service.CheckDoctorName(model.DoctorName, model.DeptId))
          {
            this.TempData["Message"] = (object) " Doctor Name already Exist";
          }
          else
          {
            MASDoctor entity = Mapper.Map<DoctorViewModal, MASDoctor>(model);
            entity.Anthetist = !model.IschkAnthetist ? "N" : "Y";
            entity.Surgeon = !model.IschkSurgeon ? "N" : "Y";
            this._uow.Repository<MASDoctor>().Add(entity, false);
          }
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
        return (ActionResult) this.RedirectToAction(nameof (Create));
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
    public ActionResult Edit(int id)
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      MASDoctor source = this._service.Get(id);
      DoctorViewModal doctorViewModal = Mapper.Map<MASDoctor, DoctorViewModal>(source);
      doctorViewModal.IschkAnthetist = source.Anthetist == "Y";
      doctorViewModal.IschkSurgeon = source.Surgeon == "Y";
      doctorViewModal.DepartmentList = this._uow.Repository<MASDepartment>().GetEntitiesBySql(string.Format(Queries.LoadDept, (object) 0));
      doctorViewModal.DesignationList = (IEnumerable<MASDesignation>) this._uow.Repository<MASDesignation>().GetAll().OrderBy<MASDesignation, string>((Func<MASDesignation, string>) (A => A.DesigName));
      return (ActionResult) this.View(nameof (Edit), (object) doctorViewModal);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult Edit(DoctorViewModal model)
    {
      try
      {
        if (!this.User.Departments.Contains(17))
          return (ActionResult) this.View("../Error/AccessDenied");
        if (this.ModelState.IsValid)
        {
          MASDoctor entity = Mapper.Map<DoctorViewModal, MASDoctor>(model);
          entity.Anthetist = !model.IschkAnthetist ? "N" : "Y";
          entity.Surgeon = !model.IschkSurgeon ? "N" : "Y";
          entity.DelInd = model.IsBlocked; 
          this._uow.Repository<MASDoctor>().Update(entity, false);
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
        return (ActionResult) this.RedirectToAction("Index");
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
    public ActionResult Details(int id)
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      MASDoctor masDoctor = this._service.Get(id);
      DoctorViewModal doctorViewModal = new DoctorViewModal()
      {
        DoctorId = masDoctor.DoctorId,
        DoctorName = masDoctor.DoctorName,
        Qualification = masDoctor.Qualification,
        DeptName = this._uow.Repository<MASDepartment>().Get(masDoctor.DeptId).DeptName,
        DesigName = this._uow.Repository<MASDesignation>().Get(masDoctor.DesigId).DesigName,
        Mobile = masDoctor.Mobile,
        Email = masDoctor.Email
      };
      doctorViewModal.DateofJoinDisplay = doctorViewModal.DateofJoin.ToString("dd/MM/yyyy");
      return (ActionResult) this.View(nameof (Details), (object) doctorViewModal);
    }

    public JsonResult GetDoctorDetailsById(int? DeptId)
    {
      IEnumerable<DoctorViewModal> doctorViewModals = (IEnumerable<DoctorViewModal>) new List<DoctorViewModal>();
      if (DeptId.HasValue)
      {
        int? nullable = DeptId;
        int num = 0;
        if ((nullable.GetValueOrDefault() == num ? (!nullable.HasValue ? 1 : 0) : 1) != 0)
          doctorViewModals = this._uow.Repository<DoctorViewModal>().GetEntitiesBySql(string.Format(Queries.GetDoctorDetSearch, (object) this.User.GetRootUrl(), (object) DeptId));
      }
      return this.Json((object) doctorViewModals);
    }

    public ActionResult EditApprovalPassword(int Id)
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      return (ActionResult) this.View((object) new DoctorViewModal()
      {
        DoctorId = Id,
        DoctorName = this._uow.Repository<MASDoctor>().Get(Id).DoctorName
      });
    }
  }
}
