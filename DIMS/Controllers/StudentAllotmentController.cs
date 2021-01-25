// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.StudentAllotmentController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using AutoMapper;
using DIMS.Infrastructure;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Metron.Entities;

namespace DIMS.Controllers
{
  public class StudentAllotmentController : Controller
  {
    private IUnitOfWork _uow;
    private IStudentAllotmentService _service;
    private IMASCodeService _Dropdownservice;

    public StudentAllotmentController(IUnitOfWork uow, IStudentAllotmentService service, IMASCodeService Dropdownservice)
    {
      this._uow = uow;
      this._service = service;
      this._Dropdownservice = Dropdownservice;
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg =>
      {
        cfg.CreateMap<StudentAllotmentViewModel, StudentAllotment>();
        cfg.CreateMap<StudentAllotment, StudentAllotmentViewModel>();
      }));
    }

    public ActionResult Index()
    {
      return (ActionResult) this.View();
    }

    [HttpPost]
    public ActionResult Create(StudentAllotmentViewModel StudentAllotmodal)
    {
      if (this.ModelState.IsValid)
      {
        if (StudentAllotmodal.StudentId > 0)
        {
          CustomPrincipal user = this.User as CustomPrincipal;
          StudentAllotmodal.CreatedBy = user.Identity.Name;
          StudentAllotmodal.CreatedDate = new DateTime?(DateTime.Now);
          StudentAllotmodal.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
          this._service.Add(Mapper.Map<StudentAllotmentViewModel, StudentAllotment>(StudentAllotmodal));
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
      return (ActionResult) null;
    }

    [HttpPost]
    public ActionResult IPCreate(StudentAllotmentViewModel StudentAllotmodal)
    {
      if (this.ModelState.IsValid)
      {
        CustomPrincipal user = this.User as CustomPrincipal;
        StudentAllotmodal.CreatedBy = user.Identity.Name;
        StudentAllotmodal.CreatedDate = new DateTime?(DateTime.Now);
        StudentAllotmodal.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
        StudentAllotment entity = Mapper.Map<StudentAllotmentViewModel, StudentAllotment>(StudentAllotmodal);
        entity.ReferredTreatmentId = 7;
        this._service.Add(entity);
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
      return (ActionResult) null;
    }

    public JsonResult GetAllotSearchList(DeptHomeViewModel model)
    {
      string rootUrl = (this.User as CustomPrincipal).GetRootUrl();
      string str1 = model.From_Date.ToString("yyyy-MM-dd");
      string str2 = model.To_Date.ToString("yyyy-MM-dd");
      int deptId = model.DeptId;
      string str3 = model.ControllerName.Trim();
      string str4 = "/";
      string str5 = str3;
      string str6 = rootUrl + str4 + str5;
      model.deptAllotedListViewModel = (IEnumerable<DeptAllotedListViewModel>) this._uow.Repository<DeptAllotedListViewModel>().GetEntitiesBySql(string.Format(Queries.AllotHomeWithDate, (object) deptId, (object) str1, (object) str2, (object) str6)).ToList<DeptAllotedListViewModel>();
      return this.Json((object) model.deptAllotedListViewModel);
    }

        public JsonResult GetAllotSearchListWithDiagnosis(DeptHomeViewModel model)
        {
            string rootUrl = (this.User as CustomPrincipal).GetRootUrl();
            string str1 = model.From_Date.ToString("yyyy-MM-dd");
            string str2 = model.To_Date.ToString("yyyy-MM-dd");
            int deptId = model.DeptId;
            string str3 = model.ControllerName.Trim();
            string str4 = "/";
            string str5 = str3;
            string str6 = rootUrl + str4 + str5;
            model.deptAllotedListViewModel = (IEnumerable<DeptAllotedListViewModel>)this._uow.Repository<DeptAllotedListViewModel>().GetEntitiesBySql(string.Format(Queries.AllotHomeWithDatePerioDiagnosis, (object)deptId, (object)str1, (object)str2, (object)str6)).ToList<DeptAllotedListViewModel>();
            return this.Json((object)model.deptAllotedListViewModel);
        }

        public JsonResult GetAllotDoctorOrStudentlist(int AllotTypeId, int DeptId)
    {
      return this.Json((object) new SelectList((IEnumerable) this.GetDetailsById(AllotTypeId, DeptId), "Value", "Text"));
    }

    public List<SelectListItem> GetDetailsById(int AllotTypeId, int DeptId)
    {
      List<SelectListItem> source = new List<SelectListItem>();
      IEnumerable<MASStudentRegistration> entitiesBySql;
      switch (AllotTypeId)
      {
        case 2:
          entitiesBySql = this._uow.Repository<MASStudentRegistration>().GetEntitiesBySql(string.Format(Queries.AllotmentStudentPGDropdownlist, (object) DeptId));
          break;
        case 4:
          entitiesBySql = this._uow.Repository<MASStudentRegistration>().GetEntitiesBySql(string.Format(Queries.AllotmentDoctorDropdownlist, (object) DeptId));
          break;
        default:
          entitiesBySql = this._uow.Repository<MASStudentRegistration>().GetEntitiesBySql(string.Format(Queries.AllotmentStudentUGDropdownlist, (object) 1));
          break;
      }
      if (entitiesBySql.Count<MASStudentRegistration>() > 0)
      {
        source = entitiesBySql.Select<MASStudentRegistration, SelectListItem>((Func<MASStudentRegistration, SelectListItem>) (x => new SelectListItem()
        {
          Text = x.StudentName,
          Value = x.StudentId.ToString()
        })).ToList<SelectListItem>();
        if (source.Count<SelectListItem>() > 0)
          source.First<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected = true));
      }
      return source;
    }

    public JsonResult GetScheduleAllotSearchList(DeptHomeViewModel model)
    {
      string str1 = model.From_Date.ToString("yyyy-MM-dd");
      string str2 = model.To_Date.ToString("yyyy-MM-dd");
      int deptId = model.DeptId;
      string str3 = model.ControllerName.Trim();
      model.deptAllotedListViewModel = (IEnumerable<DeptAllotedListViewModel>) this._uow.Repository<DeptAllotedListViewModel>().GetEntitiesBySql(string.Format(Queries.AllotWithDateOMFS, (object) deptId, (object) str1, (object) str2, (object) str3)).ToList<DeptAllotedListViewModel>();
      return this.Json((object) model.deptAllotedListViewModel);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult Delete(long Id)
    {
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      return (ActionResult) this.PartialView("../StudentAllotment/_StudentDeleteAllotment", (object) this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.AllotDelete, (object) Id)).SingleOrDefault<StudentAllotmentViewModel>());
    }

    [HttpPost]
    public void Delete(StudentAllotmentViewModel StudentAllotmodal)
    {
      this._uow.Repository<BillingDetails>().GetEntitiesBySql(string.Format(Queries.DeleteAllotment, (object) StudentAllotmodal.AllotId));
    }
  }
}
