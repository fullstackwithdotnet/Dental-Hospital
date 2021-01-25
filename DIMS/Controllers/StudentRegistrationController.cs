// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.StudentRegistrationController
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
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Metron.Entities;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace DIMS.Controllers
{
  public class StudentRegistrationController : BaseController
  {
    private const int CourseId = 1;
    private IUnitOfWork _uow;
    private IMASStudentRegistrationService _service;
    private IMASCodeService _Dropdownservice;

    public StudentRegistrationController(IUnitOfWork uow, IMASStudentRegistrationService service, IMASCodeService Dropdownservice, IUserService userservice)
      : base(uow, userservice)
    {
      this._uow = uow;
      this._service = service;
      this._Dropdownservice = Dropdownservice;
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg =>
      {
        cfg.CreateMap<StudentRegistrationViewModel, MASStudentRegistration>();
        cfg.CreateMap<MASStudentRegistration, StudentRegistrationViewModel>();
      }));
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Index()
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      return (ActionResult) this.View((object) new StudentRegistrationViewModel()
      {
        CourseIndexlist = (IEnumerable<MASCourse>) this._uow.Repository<MASCourse>().GetAll().OrderBy<MASCourse, string>((Func<MASCourse, string>) (x => x.CourseName))
      });
    }

    public ActionResult Details(int id)
    {
      return (ActionResult) this.View();
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Create()
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      var registrationViewModel = new StudentRegistrationViewModel()
      {
        CourseList = this.GetCourse(),
        Batchlist = this._Dropdownservice.GetCodesById(275)
      };
      registrationViewModel.Yearlist = this.GetCourseYearById(Convert.ToInt32(registrationViewModel.CourseList.FirstOrDefault<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected)).Value));
      registrationViewModel.Departmentlist = this._uow.Repository<MASDepartment>().GetEntitiesBySql(string.Format(Queries.LoadDept, (object) 0));
      return (ActionResult) this.View((object) registrationViewModel);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult Create(StudentRegistrationViewModel Studentregmodal)
    {
      try
      {
        Studentregmodal.StudentName = Studentregmodal.StudentName.ToUpper();
        var user = this.User;
        Studentregmodal.CreatedBy = user.Identity.Name;
        if (!user.Departments.Contains(17))
          return (ActionResult) this.View("../Error/AccessDenied");
        var entity = Mapper.Map<StudentRegistrationViewModel, MASStudentRegistration>(Studentregmodal);
        if (this.ModelState.IsValid)
        {
          if (Studentregmodal.StudentName != null && Studentregmodal.StudentRegNo != null)
          {
            entity.BatchTypeId = Studentregmodal.CodeId;
            if (Studentregmodal.CodeId != 0)
              entity.Batch = Studentregmodal.Batch1 + "-" + Studentregmodal.Batch2;
            entity.CreatedDate = new DateTime?(DateTime.Now);
            entity.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            this._service.Add(entity);
          }
        }
        else
        {
          foreach (var modelState in (IEnumerable<ModelState>) this.ViewData.ModelState.Values)
          {
            using (var enumerator = modelState.Errors.GetEnumerator())
            {
              if (enumerator.MoveNext())
              {
                var current = enumerator.Current;
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
        this.ModelState.Clear();
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
      var source = this._service.Get(id);
      var registrationViewModel1 = new StudentRegistrationViewModel();
      var registrationViewModel2 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<MASStudentRegistration, StudentRegistrationViewModel>())).CreateMapper().Map<MASStudentRegistration, StudentRegistrationViewModel>(source);
      if (registrationViewModel2.Batch != null)
      {
        var strArray = registrationViewModel2.Batch.Split('-');
        if (strArray.Length != 0)
        {
          registrationViewModel2.Batch1 = strArray[0].ToString();
          registrationViewModel2.Batch2 = strArray[1].ToString();
        }
      }
      source.StudentName = source.StudentName;
      source.StudentRegNo = source.StudentRegNo;
      registrationViewModel2.CourseList = this.GetCourse();
      registrationViewModel2.Yearlist = this.GetCourseYearById(source.StudentCourseId);
      registrationViewModel2.StudentCourseId = registrationViewModel2.StudentCourseId;
      registrationViewModel2.StudentYearId = registrationViewModel2.StudentYearId;
      registrationViewModel2.CodeId = source.BatchTypeId;
      registrationViewModel2.Batchlist = this._Dropdownservice.GetCodesById(275);
      registrationViewModel2.Departmentlist = this._uow.Repository<MASDepartment>().GetEntitiesBySql(string.Format(Queries.LoadDept, (object) 0));
      return (ActionResult) this.View(nameof (Edit), (object) registrationViewModel2);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult Edit(int id, StudentRegistrationViewModel Studentmodal)
    {
      try
      {
        var user = this.User;
        Studentmodal.ModifiedBy = user.Identity.Name;
        if (!user.Departments.Contains(17))
          return (ActionResult) this.View("../Error/AccessDenied");
        var entity = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<StudentRegistrationViewModel, MASStudentRegistration>().ForMember((Expression<Func<MASStudentRegistration, object>>) (x => (object) x.StudentId), (Action<IMemberConfigurationExpression<StudentRegistrationViewModel>>) (Igr => Igr.Ignore())))).CreateMapper().Map<StudentRegistrationViewModel, MASStudentRegistration>(Studentmodal);
        entity.StudentName = entity.StudentName.ToUpper();
        entity.StudentRegNo = entity.StudentRegNo;
        entity.StudentCourseId = entity.StudentCourseId;
        entity.StudentYearId = entity.StudentYearId;
        entity.DeptId = entity.DeptId;
        entity.BatchTypeId = Studentmodal.CodeId;
        if (Studentmodal.CodeId != 0)
          entity.Batch = Studentmodal.Batch1 + "-" + Studentmodal.Batch2;
        entity.ModifiedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
        entity.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
        int studentId;
        entity.StudentId = studentId = Studentmodal.StudentId;
        if (Studentmodal.IsChkActive)
          entity.DelInd = "1";
        this._service.Update(entity);
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

    [HttpGet]
    public ActionResult Delete(int id)
    {
      return (ActionResult) this.View();
    }

    [HttpPost]
    public ActionResult Delete(int id, FormCollection collection)
    {
      try
      {
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

    public JsonResult GetCourseYears(int id)
    {
      return this.Json((object) new SelectList((IEnumerable) this.GetCourseYearById(id), "Value", "Text"));
    }

    private List<SelectListItem> GetCourse()
    {
      var selectListItemList = new List<SelectListItem>();
      foreach (var masCourse in (IEnumerable<MASCourse>) this._uow.Repository<MASCourse>().GetAll(string.Format("delInd=0 and CourseId not in ({0})", (object) 4)).ToList<MASCourse>())
      {
        if (masCourse.CourseId == 1)
          selectListItemList.Add(new SelectListItem()
          {
            Text = masCourse.CourseName,
            Value = Convert.ToString(masCourse.CourseId),
            Selected = true
          });
        else
          selectListItemList.Add(new SelectListItem()
          {
            Text = masCourse.CourseName,
            Value = Convert.ToString(masCourse.CourseId)
          });
      }
      return selectListItemList;
    }

    public List<SelectListItem> GetCourseYearById(int id)
    {
      var selectListItemList = new List<SelectListItem>();
      var list = this._uow.Repository<MASCourseYear>().GetAll(string.Format("delInd=0 and CourseId= {0} ", (object) id)).Select<MASCourseYear, SelectListItem>((Func<MASCourseYear, SelectListItem>) (x => new SelectListItem()
      {
        Text = x.CourseYearName,
        Value = x.CourseYearId.ToString()
      })).ToList<SelectListItem>();
      if (list.Count<SelectListItem>() > 0)
        list.First<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected = true));
      return list;
    }

    public JsonResult GetStudentDetailsById(int? StudentCourseId)
    {
      var registrationViewModels = (IEnumerable<StudentRegistrationViewModel>) new List<StudentRegistrationViewModel>();
      if (StudentCourseId.HasValue)
      {
        var nullable = StudentCourseId;
        var num = 0;
        if ((nullable.GetValueOrDefault() == num ? (!nullable.HasValue ? 1 : 0) : 1) != 0)
          registrationViewModels = this._uow.Repository<StudentRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.GetStudentDetSearch, (object) this.User.GetRootUrl(), (object) StudentCourseId));
      }
      return this.Json((object) registrationViewModels);
    }

      public JsonResult GetStudentDetailsErpById(int? StudentCourseId)
      {
          var registrationViewModels = (IEnumerable<StudentRegistrationViewModel>)new List<StudentRegistrationViewModel>();
          var client = new RestClient(ConfigurationManager.AppSettings["ApiUrl"] +"api/student");
          var request = new RestRequest(Method.GET);
          request.AddHeader("cache-control", "no-cache");
          request.AddHeader("Connection", "keep-alive");
          request.AddHeader("accept-encoding", "gzip, deflate");
          request.AddHeader("Cache-Control", "no-cache");
          request.AddHeader("Accept", "*/*");
          var response = client.Execute<List<DtoStudentModel>>(request);
          var releases = response.Data;
          return this.Json(releases);
      }


    }
}
