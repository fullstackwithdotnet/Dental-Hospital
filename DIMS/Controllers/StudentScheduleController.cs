// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.StudentScheduleController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Infrastructure;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace DIMS.Controllers
{
  public class StudentScheduleController : BaseController
  {
    private const int CourseId = 1;
    private IUnitOfWork _uow;
    private IMASCodeService _Dropdownservice;
    private IStudentScheduleService _service;

    public StudentScheduleController(IUnitOfWork uow, IMASCodeService Dropdownservice, IStudentScheduleService service, IUserService userservice)
      : base(uow, userservice)
    {
      this._uow = uow;
      this._service = service;
      this._Dropdownservice = Dropdownservice;
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult Index()
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      StudentScheduleViewModel scheduleViewModel = new StudentScheduleViewModel();
      return (ActionResult) this.View((object) this._service.BindSearch());
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Create()
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      StudentScheduleViewModel scheduleViewModel = new StudentScheduleViewModel();
      return (ActionResult) this.View((object) this._service.BindInitialViewModel());
    }

    public JsonResult GetScheduledStudentDetails(string StudentCourseId, string StudentYearId, string SchFromDate, string SchToDate, string DeptId)
    {
      DateTime dateTime1 = Convert.ToDateTime(SchFromDate);
      DateTime dateTime2 = Convert.ToDateTime(SchToDate);
      SchFromDate = dateTime1.ToString("yyyy-MM-dd");
      SchToDate = dateTime2.ToString("yyyy-MM-dd");
      return this.Json((object) new List<SelectList>()
      {
        new SelectList((IEnumerable) this._service.GetFromStudent(StudentCourseId, StudentYearId, SchFromDate, SchToDate, DeptId), "Value", "Text"),
        new SelectList((IEnumerable) this._service.GetToStudent(StudentCourseId, StudentYearId, SchFromDate, SchToDate, DeptId), "Value", "Text")
      });
    }

    public JsonResult GetAllStudentDetails(string StudentCourseId, string StudentYearId, string SchFromDate, string SchToDate, string DeptId)
    {
      DateTime dateTime1 = Convert.ToDateTime(SchFromDate);
      DateTime dateTime2 = Convert.ToDateTime(SchToDate);
      SchFromDate = dateTime1.ToString("yyyy-MM-dd");
      SchToDate = dateTime2.ToString("yyyy-MM-dd");
      return this.Json((object) new List<SelectList>()
      {
        new SelectList((IEnumerable) this._service.GetAllStudent(StudentCourseId, StudentYearId, SchFromDate, SchToDate, DeptId), "Value", "Text")
      });
    }

    public JsonResult GetScheduledStudentList(string StudentCourseId, string StudentYearId)
    {
      return this.Json((object) this._service.DisplayStudentSchedule(StudentCourseId, StudentYearId));
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult Create(StudentScheduleViewModel model)
    {
      CustomPrincipal user = this.User;
      model.CreatedBy = user.Identity.Name;
      if (!user.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      if (this.ModelState.IsValid)
      {
        JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
        model.ToSelectStudent = (IEnumerable<int>) scriptSerializer.Deserialize<int[]>(model.SelectedStudents);
        if (model.DeptId != 0 && model.ToSelectStudent != null)
        {
          foreach (int num in model.ToSelectStudent)
          {
            model.StudentId = num;
            model.CreatedDate = new DateTime?(DateTime.Now);
            model.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            this._service.SaveStudentSchedule(model);
          }
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
      model = this._service.BindSearch();
      return (ActionResult) this.View("Index", (object) model);
    }

    public JsonResult GetCourseYears(int id)
    {
      return this.Json((object) new SelectList((IEnumerable) this._service.GetCourseYearById(id), "Value", "Text"));
    }

    public JsonResult GetStudentScheduleSearchList(StudentScheduleViewModel model)
    {
      string From_Date = model.SchFromDate.ToString("yyyy-MM-dd");
      string To_Date = model.SchToDate.ToString("yyyy-MM-dd");
      int deptId = model.DeptId;
      model.SearchDetails = this._service.SearchDetails(From_Date, To_Date, deptId);
      return this.Json((object) model.SearchDetails);
    }
  }
}
