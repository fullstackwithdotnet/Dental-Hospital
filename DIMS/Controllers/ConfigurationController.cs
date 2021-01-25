// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.ConfigurationController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Infrastructure;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Metron.Entities;

namespace DIMS.Controllers
{
  public class ConfigurationController : BaseController
  {
    private IUnitOfWork _uow;
    private IDCardsService _service;
    private IMASCodeService _Dropdownservice;

    public ConfigurationController(IUnitOfWork uow, IDCardsService service, IMASCodeService Dropdownservice, IUserService userservice)
      : base(uow, userservice)
    {
      this._uow = uow;
      this._service = service;
      this._Dropdownservice = Dropdownservice;
    }

    [HttpGet]
    public ActionResult Index()
    {
      DCardsSearchViewModal dcardsSearchViewModal = new DCardsSearchViewModal();
      IsDummyEnable isDummyEnable = new IsDummyEnable();
      IsDummyEnable isDummy = this._service.GetIsDummy();
      dcardsSearchViewModal.IsSearchEnable = isDummy.SearchForm.Trim() == "Y";
      dcardsSearchViewModal.IsReportEnable = isDummy.ReportForm.Trim() == "Y";
      dcardsSearchViewModal.DummyEnableId = isDummy.DummyEnableId;
      string whereClause1 = "DelInd=0";
      dcardsSearchViewModal.Categorylist = this._uow.Repository<MASCategory>().GetAll(whereClause1);
      dcardsSearchViewModal.ToDepartmentList = this._uow.Repository<MASDepartment>().GetEntitiesBySql(string.Format(Queries.DCardLoadDept, (object) 0));
      string whereClause2 = " Visibility ='Y' and DeptId =" + (object) 7;
      dcardsSearchViewModal.TreatmentTypeDetails = this._uow.Repository<TreatmentTypes>().GetAll(whereClause2);
      dcardsSearchViewModal.Reg = DateTime.Now;
      return (ActionResult) this.View("../Configuration/IndexNew", (object) dcardsSearchViewModal);
    }

    public JsonResult Update(DCardsSearchViewModal model)
    {
      IsDummyEnable isDummyEnable = new IsDummyEnable();
      if (this.ModelState.IsValid)
        this._uow.Repository<IsDummyEnable>().GetEntitiesBySql(string.Format(Queries.IsDummySearch, (object) model.SearchForm, (object) model.ReportForm, (object) model.DummyEnableId));
      return this.Json((object) true);
    }

    [CustomAuthorize(Roles = "Admin,Staff")]
    public ActionResult ConfigurationDetails()
    {
      DCardsSearchViewModal dcardsSearchViewModal = new DCardsSearchViewModal();
      IsDummyEnable isDummyEnable = new IsDummyEnable();
      IsDummyEnable isDummy = this._service.GetIsDummy();
      dcardsSearchViewModal.IsSearchEnable = isDummy.SearchForm.Trim() == "Y";
      dcardsSearchViewModal.IsReportEnable = isDummy.ReportForm.Trim() == "Y";
      dcardsSearchViewModal.DummyEnableId = isDummy.DummyEnableId;
      string whereClause1 = "DelInd=0";
      dcardsSearchViewModal.Categorylist = this._uow.Repository<MASCategory>().GetAll(whereClause1);
      dcardsSearchViewModal.ToDepartmentList = this._uow.Repository<MASDepartment>().GetEntitiesBySql(string.Format(Queries.DCardLoadDept, (object) 0));
      string whereClause2 = " Visibility ='Y' and DeptId =" + (object) 7;
      dcardsSearchViewModal.TreatmentTypeDetails = this._uow.Repository<TreatmentTypes>().GetAll(whereClause2);
      dcardsSearchViewModal.Reg = DateTime.Now;
      return (ActionResult) this.View("../Configuration/IndexNew", (object) dcardsSearchViewModal);
    }

    public JsonResult GetCountDetails(string SearchDate, int DeptId, int TreatmentId)
    {
      string str1 = "(0=0)";
      string str2 = " R.VisitType ='N' ";
      if (7 == DeptId)
        str1 = "R.ReferredTreatmentId = " + (object) TreatmentId;
      int count1 = this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardActualCount, (object) SearchDate, (object) DeptId, (object) str1, (object) str2)).ToList<DCardSearchDetails>().Count;
      string str3 = "(0=0)";
      string str4 = " R.VisitType ='R' ";
      if (7 == DeptId)
        str3 = "R.ReferredTreatmentId = " + (object) TreatmentId;
      int count2 = this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardActualCount, (object) SearchDate, (object) DeptId, (object) str3, (object) str4)).ToList<DCardSearchDetails>().Count;
      string str5 = "(0=0)";
      string str6 = " R.VisitType ='N' and OP.IsDummy='N'";
      if (7 == DeptId)
        str5 = "R.ReferredTreatmentId = " + (object) TreatmentId;
      int count3 = this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardActualCount, (object) SearchDate, (object) DeptId, (object) str5, (object) str6)).ToList<DCardSearchDetails>().Count;
      string str7 = "(0=0)";
      string str8 = " R.VisitType ='R' and OP.IsDummy='N'";
      if (7 == DeptId)
        str7 = "R.ReferredTreatmentId = " + (object) TreatmentId;
      int count4 = this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardActualCount, (object) SearchDate, (object) DeptId, (object) str7, (object) str8)).ToList<DCardSearchDetails>().Count;
      string str9 = "(0=0)";
      string str10 = " R.VisitType ='N' and OP.IsDummy='Y'";
      if (7 == DeptId)
        str9 = "R.ReferredTreatmentId = " + (object) TreatmentId;
      int count5 = this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardActualCount, (object) SearchDate, (object) DeptId, (object) str9, (object) str10)).ToList<DCardSearchDetails>().Count;
      string str11 = "(0=0)";
      string str12 = " R.VisitType ='R' and OP.IsDummy='Y'";
      if (7 == DeptId)
        str11 = "R.ReferredTreatmentId = " + (object) TreatmentId;
      int count6 = this._uow.Repository<DCardSearchDetails>().GetEntitiesBySql(string.Format(Queries.DCardActualCount, (object) SearchDate, (object) DeptId, (object) str11, (object) str12)).ToList<DCardSearchDetails>().Count;
      return this.Json((object) new DCardsSearchViewModal()
      {
        TotalNewVisitCount = count1,
        TotalRevisitCount = count2,
        NewVisitOrginalCount = count3,
        RevisitOrginalCount = count4,
        NewVisitDcardCount = count5,
        RevisitDcardCount = count6
      });
    }

    public ActionResult DCardSave(DCardsSearchViewModal model)
    {
      if (model.ToDeptId == 2)
        this._service.SaveOMFSPatient(model);
      else if (model.ToDeptId == 3)
        this._service.SavePERIOPatient(model);
      else if (model.ToDeptId == 4)
        this._service.SaveCONSPatient(model);
      else if (model.ToDeptId == 5)
        this._service.SaveORTHOPatient(model);
      else if (model.ToDeptId == 6)
        this._service.SavePEDOPatient(model);
      else if (model.ToDeptId == 7)
      {
        if (model.PROSTreatmentId == 1)
          this._service.SavePROSCDPatient(model);
        else if (model.PROSTreatmentId == 2)
          this._service.SavePROSRPDPatient(model);
        else if (model.PROSTreatmentId == 5)
          this._service.SavePROSDIMPatient(model);
        else if (model.PROSTreatmentId == 3)
          this._service.SavePROSFPDPatient(model);
        else if (model.PROSTreatmentId == 4)
          this._service.SavePROSMFPPatient(model);
      }
      else if (model.ToDeptId == 8)
        this._service.SavePHDPatient(model);
      this.TempData["Message"] = (object) "Saved Successfully";
      return (ActionResult) this.RedirectToAction("ConfigurationDetails", new RouteValueDictionary((object) new
      {
        controller = "Configuration",
        action = "ConfigurationDetails"
      }));
    }

    public ActionResult DCardRevisitSave(DCardsSearchViewModal model)
    {
      if (model.ToDeptId == 2)
        this._service.SaveRevisitOMFSPatient(model);
      else if (model.ToDeptId == 3)
        this._service.SaveRevisitPERIOPatient(model);
      else if (model.ToDeptId == 4)
        this._service.SaveRevisitCONSPatient(model);
      else if (model.ToDeptId == 5)
        this._service.SaveRevisitORTHOPatient(model);
      else if (model.ToDeptId == 6)
        this._service.SaveRevisitPEDOPatient(model);
      else if (model.ToDeptId == 7)
      {
        if (model.PROSTreatmentId == 1)
          this._service.SaveRevisitPROSCDPatient(model);
        else if (model.PROSTreatmentId == 2)
          this._service.SaveRevisitPROSRPDPatient(model);
        else if (model.PROSTreatmentId == 5)
          this._service.SaveRevisitPROSDIMPatient(model);
        else if (model.PROSTreatmentId == 3)
          this._service.SaveRevisitPROSFPDPatient(model);
        else if (model.PROSTreatmentId == 4)
          this._service.SaveRevisitPROSMFPPatient(model);
      }
      else if (model.ToDeptId == 8)
        this._service.SaveRevisitPHDPatient(model);
      this.TempData["Message"] = (object) "Saved Successfully";
      return (ActionResult) this.RedirectToAction("ConfigurationDetails", new RouteValueDictionary((object) new
      {
        controller = "Configuration",
        action = "ConfigurationDetails"
      }));
    }
  }
}
