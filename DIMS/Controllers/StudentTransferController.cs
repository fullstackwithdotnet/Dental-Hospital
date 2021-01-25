// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.StudentTransferController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Infrastructure;
using DIMS.Services.Abstract;
using DIMS.Services.Implementation;
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
  public class StudentTransferController : BaseController
  {
    private IUnitOfWork _uow;
    private IStudentTransferService _service;
    private IMASCodeService _Dropdownservice;

    public StudentTransferController(IUnitOfWork uow, StudentTransferService service, IMASCodeService Dropdownservice, IUserService userservice)
      : base(uow, userservice)
    {
      this._uow = uow;
      this._service = (IStudentTransferService) service;
      this._Dropdownservice = Dropdownservice;
    }

    public ActionResult Index()
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
      StudentTransferViewModel transferViewModel = new StudentTransferViewModel();
      return (ActionResult) this.View((object) this._service.BindStudentTransferModel());
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult Create(StudentTransferViewModel model)
    {
      CustomPrincipal user = this.User;
      model.ModifiedBy = user.Identity.Name;
      if (!user.Departments.Contains(17))
        return (ActionResult) this.View("../Error/AccessDenied");
      if (this.ModelState.IsValid)
      {
        JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
        model.ToSelectStudent = (IEnumerable<int>) scriptSerializer.Deserialize<int[]>(model.SelectedStudents);
        if (model.ToSelectStudent != null)
        {
          foreach (int num in model.ToSelectStudent)
          {
            model.StudentId = num;
            model.ModifiedDate = DateTime.Now;
            model.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
            this._service.UpdateStudentTransfer(model);
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
      model = this._service.BindStudentTransferModel();
      return (ActionResult) this.View(nameof (Create), (object) model);
    }

    public JsonResult GetAllStudentDetails(string StudentCourseId, string StudentYearId, int DeptId, string BatchTypeId)
    {
      return this.Json((object) new List<SelectList>()
      {
        new SelectList((IEnumerable) this._service.GetAllStudent(StudentCourseId, StudentYearId, DeptId, BatchTypeId), "Value", "Text")
      });
    }

    public JsonResult GetCourseYears(int id)
    {
      if (id == 1)
        return this.Json((object) new SelectList((IEnumerable) this._service.GetCourseYearById(id), "Value", "Text"));
      return this.Json((object) new SelectList((IEnumerable) this._service.GetCourseYearById(id), "Value", "Text"));
    }
  }
}
