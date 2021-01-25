// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.LaboratoryController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Infrastructure;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace DIMS.Controllers
{
  public class LaboratoryController : BaseController
  {
    private IUnitOfWork _uow;
    private ILaboratoryRegistrationService _Laboratory;

    public LaboratoryController(IUnitOfWork uow, ILaboratoryRegistrationService Laboratory, IUserService userservice)
      : base(uow, userservice)
    {
      _uow = uow;
      _Laboratory = Laboratory;
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult Index()
    {
      GetPermissionforUser();
      if (!User.Departments.Contains(20))
        return View("../Error/AccessDenied");
      return View("../Laboratory/Index", new LaboratoryRegistrationSearchViewModel()
      {
          From_Date = DateTime.Now,
          To_Date = DateTime.Now,
          DeptId = 20
      });
    }

    public JsonResult GetLaboratorySearchList(LaboratoryRegistrationSearchViewModel model)
    {
      var From_Date = model.From_Date.ToString("yyyy-MM-dd");
      var To_Date = model.To_Date.ToString("yyyy-MM-dd");
      var deptId = model.DeptId;
      var rootUrl = User.GetRootUrl();
      model.SearchDetails = _Laboratory.BillingList(deptId, From_Date, To_Date, rootUrl).ToList<LaboratoryRegistrationSearchDetails>();
      return Json(model.SearchDetails);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Create(int id, int patientid)
    {
      GetPermissionforUser();
      if (!User.Departments.Contains(20))
        return View("../Error/AccessDenied");
      var registrationViewModel1 = new LaboratoryRegistrationViewModel();
      var registrationViewModel2 = _Laboratory.BindLaboratoryModel(id, patientid);
      registrationViewModel2.TestNameList = _Laboratory.TestNamesList(id).ToList<LaboratoryRegistrationViewModel>();
      if (registrationViewModel2.TestNameList.Count<LaboratoryRegistrationViewModel>() == 0)
        TempData["Message"] = "Add Test Definer Items To Save";
      return View(registrationViewModel2);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult Create(LaboratoryRegistrationViewModel modal)
    {
      try
      {
        var user = User;
        if (!user.Departments.Contains(20))
          return View("../Error/AccessDenied");
        modal.CreatedBy = user.Identity.Name;
        var num = 0;
        if (ModelState.IsValid)
        {
          num = _Laboratory.SaveLaboratory(modal);
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
        return RedirectToAction("Edit", new
        {
            Id = num
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Edit(int Id = 0)
    {
      GetPermissionforUser();
      if (!User.Departments.Contains(20))
        return View("../Error/AccessDenied");
      var registrationViewModel1 = new LaboratoryRegistrationViewModel();
      var registrationViewModel2 = _Laboratory.BindEditLaboratoryModel(Id);
      registrationViewModel2.TestNameEditList = _Laboratory.TestNameEditList(Id).ToList<LaboratoryRegistrationViewModel>();
      return View("../Laboratory/Edit", registrationViewModel2);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult TestItemCreate(int serviceId, int LabDetId, int resultId)
    {
      GetPermissionforUser();
      if (!User.Departments.Contains(20))
        return View("../Error/AccessDenied");
      var registrationViewModel1 = new LaboratoryRegistrationViewModel();
      var registrationViewModel2 = _Laboratory.BindEditHeadLaboratoryModel(serviceId, LabDetId, resultId);
      registrationViewModel2.TestItemList = _Laboratory.TestItemList(serviceId, LabDetId, resultId).ToList<LaboratoryRegistrationViewModel>();
      return View("../Laboratory/TestItemCreate", registrationViewModel2);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult TestItemCreate(LaboratoryRegistrationViewModel model)
    {
      try
      {
        var user = User;
        if (!user.Departments.Contains(20))
          return View("../Error/AccessDenied");
        var num = 0;
        if (ModelState.IsValid)
        {
          model.CreatedBy = user.Identity.Name;
          num = _Laboratory.SaveEditLaboratory(model);
          if (num == 0)
            num = model.LaboratoryResultId;
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
        if (num > 0)
          TempData["Message"] = "Saved Successfully";
        return RedirectToAction(nameof (TestItemCreate), new RouteValueDictionary(new
        {
            controller = "Laboratory",
            action = nameof (TestItemCreate),
            serviceId = model.ServiceId,
            LabDetId = model.LaboratoryDetId,
            resultId = num
        }));
      }
      catch (Exception ex)
      {
        return RedirectToAction("ErrorWrite", new RouteValueDictionary(new
        {
            controller = "Error",
            action = "ErrorWrite",
            message = ex.ToString()
        }));
      }
    }

    public ActionResult Search()
    {
      GetPermissionforUser();
      if (!User.Departments.Contains(20))
        return View("../Error/AccessDenied");
      return View("../Laboratory/Search", new LaboratoryRegistrationSearchViewModel()
      {
          From_Date = DateTime.Now,
          To_Date = DateTime.Now
      });
    }

    public JsonResult GetOrpathEntrySearchList(LaboratoryRegistrationSearchViewModel model)
    {
      var From_Date = model.From_Date.ToString("yyyy-MM-dd");
      var To_Date = model.To_Date.ToString("yyyy-MM-dd");
      var rootUrl = User.GetRootUrl();
      model.SearchDetails = _Laboratory.SearchList(From_Date, To_Date, rootUrl).ToList<LaboratoryRegistrationSearchDetails>();
      return Json(model.SearchDetails);
    }

    public ActionResult Report(int Id)
    {
      var registrationViewModel = new LaboratoryRegistrationViewModel();
      return View("../Reports/LaboratoryReport", _Laboratory.BindLaboratoryPatientReport(Id));
    }

    public JsonResult GetLabDetForLaboratory(int LaboratoryId, int PatientId)
    {
      return Json(new LaboratoryRegistrationViewModel()
      {
          LabHeaderforLaboratory = _uow.Repository<LaboratoryRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.GetLabHeaderforAllDepts, LaboratoryId)),
          LabDetforLaboratory = _uow.Repository<LaboratoryRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.GetLabDetforAllDepts, LaboratoryId)),
          FileUploadlistforLaboratory = _uow.Repository<LaboratoryRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, PatientId, 20))
      }, JsonRequestBehavior.AllowGet);
    }
  }
}
