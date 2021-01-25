// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.RadiologyController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Infrastructure;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Metron.Entities;

namespace DIMS.Controllers
{
  public class RadiologyController : BaseController
  {
    private IUnitOfWork _uow;
    private IRadioRegistrationService _radiology;

    public RadiologyController(IUnitOfWork uow, IRadioRegistrationService radiology, IUserService userservice)
      : base(uow, userservice)
    {
      this._uow = uow;
      this._radiology = radiology;
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult Index()
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(16))
        return (ActionResult) this.View("../Error/AccessDenied");
      return (ActionResult) this.View("../Radiology/Index", (object) new RadioRegistrationSearchViewModal()
      {
        From_Date = DateTime.Now,
        To_Date = DateTime.Now,
        DeptId = 16
      });
    }

    public JsonResult GetRadioSearchList(RadioRegistrationSearchViewModal model)
    {
      string From_Date = model.From_Date.ToString("yyyy-MM-dd");
      string To_Date = model.To_Date.ToString("yyyy-MM-dd");
      int deptId = model.DeptId;
      string rootUrl = this.User.GetRootUrl();
      model.SearchDetails = this._radiology.BillingList(deptId, From_Date, To_Date, rootUrl).ToList<RadioRegistrationSearchDetails>();
      return this.Json((object) model.SearchDetails);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Create(int id, int patientid)
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(16))
        return (ActionResult) this.View("../Error/AccessDenied");
      RadioRegistrationViewModel registrationViewModel1 = new RadioRegistrationViewModel();
      RadioRegistrationViewModel registrationViewModel2 = this._radiology.BindRadiologyModel(id, patientid);
      registrationViewModel2.TestNameList = (IEnumerable<RadioRegistrationViewModel>) this._radiology.TestNamesList(id).ToList<RadioRegistrationViewModel>();
      if (registrationViewModel2.TestNameList.Count<RadioRegistrationViewModel>() == 0)
        this.TempData["Message"] = (object) "Add Test Definer Items To Save";
      return (ActionResult) this.View((object) registrationViewModel2);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult Create(RadioRegistrationViewModel modal)
    {
      try
      {
        CustomPrincipal user = this.User;
        if (!user.Departments.Contains(16))
          return (ActionResult) this.View("../Error/AccessDenied");
        int num = 0;
        modal.CreatedBy = user.Identity.Name;
        if (this.ModelState.IsValid)
        {
          num = this._radiology.SaveRadiolgy(modal);
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
        return (ActionResult) this.RedirectToAction("Edit", (object) new
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
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(16))
        return (ActionResult) this.View("../Error/AccessDenied");
      RadioRegistrationViewModel registrationViewModel1 = new RadioRegistrationViewModel();
      RadioRegistrationViewModel registrationViewModel2 = this._radiology.BindEditRadiologyModel(Id);
      registrationViewModel2.TestNameEditList = (IEnumerable<RadioRegistrationViewModel>) this._radiology.TestNameEditList(Id).ToList<RadioRegistrationViewModel>();
      return (ActionResult) this.View("../Radiology/Edit", (object) registrationViewModel2);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult TestItemCreate(int serviceId, int LabDetId, int resultId)
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(16))
        return (ActionResult) this.View("../Error/AccessDenied");
      RadioRegistrationViewModel registrationViewModel = new RadioRegistrationViewModel()
      {
        LabDetId = LabDetId,
        ServiceId = serviceId,
        ResultId = resultId
      };
      return (ActionResult) this.View("../Radiology/TestItemCreate", (object) this._radiology.BindEditHeadRadiologyModel(serviceId, LabDetId, resultId));
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult TestItemCreate(RadioRegistrationViewModel model)
    {
      try
      {
        CustomPrincipal user = this.User;
        if (!user.Departments.Contains(16))
          return (ActionResult) this.View("../Error/AccessDenied");
        int num = 0;
        if (this.ModelState.IsValid)
        {
          model.CreatedBy = user.Identity.Name;
          num = this._radiology.SaveEditRadiolgy(model);
          if (num == 0)
            num = model.ResultId;
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
        if (num > 0)
          this.TempData["Message"] = (object) "Saved Successfully";
        return (ActionResult) this.RedirectToAction(nameof (TestItemCreate), new RouteValueDictionary((object) new
        {
          controller = "Radiology",
          action = nameof (TestItemCreate),
          serviceId = model.ServiceId,
          LabDetId = model.LabDetId,
          resultId = num
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

    public ActionResult Search()
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(16))
        return (ActionResult) this.View("../Error/AccessDenied");
      return (ActionResult) this.View("../Radiology/Search", (object) new RadioRegistrationSearchViewModal()
      {
        From_Date = DateTime.Now,
        To_Date = DateTime.Now
      });
    }

    public JsonResult GetRadioEntrySearchList(RadioRegistrationSearchViewModal model)
    {
      string From_Date = model.From_Date.ToString("yyyy-MM-dd");
      string To_Date = model.To_Date.ToString("yyyy-MM-dd");
      string rootUrl = this.User.GetRootUrl();
      model.SearchDetails = this._radiology.SearchList(From_Date, To_Date, rootUrl).ToList<RadioRegistrationSearchDetails>();
      return this.Json((object) model.SearchDetails);
    }

    [HttpPost]
    public JsonResult DeleteFileUploadDetails(int FileId)
    {
      this._uow.Repository<RADIOResultEntry>().GetEntitiesBySql(string.Format(Queries.DeleteUploadedFile, (object) FileId)).FirstOrDefault<RADIOResultEntry>();
      return this.Json((object) true);
    }

    public FileResult DownloadFile(string fileName)
    {
      string fileName1 = Path.Combine(this.Server.MapPath("~/Content/Upload/"), fileName);
      return (FileResult) this.File(fileName1, MimeMapping.GetMimeMapping(fileName1), fileName);
    }

    public ActionResult RadiologyReport(int Id)
    {
      this.GetPermissionforUser();
      if (!this.User.Departments.Contains(19))
        return (ActionResult) this.View("../Error/AccessDenied");
      RadioRegistrationViewModel registrationViewModel = new RadioRegistrationViewModel();
      return (ActionResult) this.View("../Reports/RadiologyReport", (object) this._radiology.BindReportRadiologyModel(Id));
    }

    public JsonResult GetRadioDetForRadiography(int LabId, int PatientId)
    {
      return this.Json((object) new RadioRegistrationViewModel()
      {
        RadioHeaderforRadiography = this._uow.Repository<RadioRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.GetRadioHeaderforAllDepts, (object) LabId)),
        RadioDetforRadiography = this._uow.Repository<RadioRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.GetRadioDetforAllDepts, (object) LabId)),
        FileUploadlistforRadiography = this._uow.Repository<RadioRegistrationViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) PatientId, (object) 16))
      }, JsonRequestBehavior.AllowGet);
    }
  }
}
