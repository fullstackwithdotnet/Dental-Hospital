// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.DepartmentController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

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
  public class DepartmentController : Controller
  {
    private IUnitOfWork _uow;
    private IMASDepartmentService _service;

    public DepartmentController(IUnitOfWork uow, IMASDepartmentService service)
    {
      this._uow = uow;
      this._service = service;
    }

    public ActionResult Index()
    {
      IOrderedEnumerable<MASDepartment> orderedEnumerable = this._uow.Repository<MASDepartment>().GetAll().OrderBy<MASDepartment, string>((Func<MASDepartment, string>) (A => A.DeptName));
      List<DepartmentViewModal> departmentViewModalList = new List<DepartmentViewModal>();
      foreach (MASDepartment masDepartment in (IEnumerable<MASDepartment>) orderedEnumerable)
        departmentViewModalList.Add(new DepartmentViewModal()
        {
          DeptId = masDepartment.DeptId,
          DeptCode = masDepartment.DeptCode,
          DeptName = masDepartment.DeptName
        });
      return (ActionResult) this.View((object) departmentViewModalList);
    }

    [HttpGet]
    public ActionResult Details(int id)
    {
      MASDepartment masDepartment = this._service.Get(id);
      return (ActionResult) this.View(nameof (Details), (object) new DepartmentViewModal()
      {
        DeptId = masDepartment.DeptId,
        DeptCode = masDepartment.DeptCode,
        DeptName = masDepartment.DeptName
      });
    }

    [HttpGet]
    public ActionResult Create()
    {
      return (ActionResult) this.View((object) new DepartmentViewModal());
    }

    [HttpPost]
    public ActionResult Create(DepartmentViewModal Deptmodal)
    {
      if (this.ModelState.IsValid)
      {
        this._service.Add(new MASDepartment()
        {
          DeptCode = Deptmodal.DeptCode,
          DeptName = Deptmodal.DeptName
        });
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
      this.ModelState.Clear();
      return (ActionResult) this.RedirectToAction(nameof (Create));
    }

    [HttpGet]
    public ActionResult Edit(int id)
    {
      MASDepartment masDepartment = this._service.Get(id);
      return (ActionResult) this.View(nameof (Edit), (object) new DepartmentViewModal()
      {
        DeptId = masDepartment.DeptId,
        DeptName = masDepartment.DeptName,
        DeptCode = masDepartment.DeptCode
      });
    }

    [HttpPost]
    public ActionResult Edit(DepartmentViewModal Deptmodal)
    {
      if (this.ModelState.IsValid)
      {
        this._service.Update(new MASDepartment()
        {
          DeptCode = Deptmodal.DeptCode,
          DeptName = Deptmodal.DeptName,
          DeptId = Deptmodal.DeptId
        });
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
  }
}
