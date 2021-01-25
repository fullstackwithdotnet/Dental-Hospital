// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.LaboratoryTemplateController
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
  public class LaboratoryTemplateController : Controller
  {
    private IUnitOfWork _uow;
    private ITemplateService _service;

    public LaboratoryTemplateController(IUnitOfWork uow, ITemplateService service)
    {
      this._uow = uow;
      this._service = service;
    }

    public ActionResult Index()
    {
      return (ActionResult) this.View("../LaboratoryTemplate/Index", (object) new TemplateSearchViewModal()
      {
        From_Date = DateTime.Now,
        To_Date = DateTime.Now,
        DeptId = 20
      });
    }

    public JsonResult GetTemplateSearchList(TemplateSearchViewModal model)
    {
      string From_Date = model.From_Date.ToString("yyyy-MM-dd");
      string To_Date = model.To_Date.ToString("yyyy-MM-dd");
      int deptId = model.DeptId;
      string rootUrl = (this.User as CustomPrincipal).GetRootUrl();
      model.SearchDetails = this._service.LaboratoryList(deptId, From_Date, To_Date, rootUrl).ToList<TemplateSearchDetails>();
      return this.Json((object) model.SearchDetails);
    }

    [HttpGet]
    public ActionResult Create()
    {
      int id = 0;
      TemplateViewModal templateViewModal1 = new TemplateViewModal();
      TemplateDetailsViewModel detailsViewModel = new TemplateDetailsViewModel();
      TemplateViewModal templateViewModal2 = this._service.BindLaboratoryModel();
      templateViewModal2.radiologyViewModal = detailsViewModel;
      templateViewModal2.ServiceListDetails = this._service.ServiceListDetails(id);
      return (ActionResult) this.View((object) templateViewModal2);
    }

    [HttpPost]
    public ActionResult Create(TemplateViewModal modal)
    {
      try
      {
        if (this.ModelState.IsValid)
        {
          if (modal.ServiceListDetails != null)
            return (ActionResult) this.RedirectToAction("Edit", (object) new
            {
              Id = this._service.SaveRadiolgy(modal)
            });
          this.TempData["Message"] = (object) " Enter Test Items to save";
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
        throw ex;
      }
    }

    [HttpGet]
    public ActionResult Edit(int Id = 0)
    {
      RADIOTemplate source = this._service.Get(Id);
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<RADIOTemplate, TemplateViewModal>()));
      TemplateViewModal templateViewModal = Mapper.Map<RADIOTemplate, TemplateViewModal>(source);
      TemplateDetailsViewModel detailsViewModel = new TemplateDetailsViewModel();
      templateViewModal.RadioTempId = Id;
      templateViewModal.radiologyViewModal = detailsViewModel;
      templateViewModal.ServiceListDetails = this._service.ServiceListLaboratoryDetails(Id);
      string whereClause = "DeptId=" + (object) 20;
      templateViewModal.ServicesList = (IEnumerable<MASBillingServices>) this._uow.Repository<MASBillingServices>().GetAll(whereClause).OrderBy<MASBillingServices, string>((Func<MASBillingServices, string>) (x => x.ServiceName));
      templateViewModal.GroupList = (IEnumerable<MASGroup>) this._uow.Repository<MASGroup>().GetAll(whereClause).OrderBy<MASGroup, string>((Func<MASGroup, string>) (x => x.GroupName));
      return (ActionResult) this.View("../LaboratoryTemplate/Edit", (object) templateViewModal);
    }

    [HttpPost]
    public JsonResult Edit(TemplateDetailsViewModel model)
    {
      int radioTempId = model.RadioTempId;
      if (this.ModelState.IsValid)
        this._service.UpdateRadiolgy(model);
      int num = 20;
      return this.Json((object) this._uow.Repository<TemplateDetailsViewModel>().GetEntitiesBySql(string.Format(Queries.ServiceDetails, (object) radioTempId, (object) num)).ToList<TemplateDetailsViewModel>());
    }

    [HttpPost]
    public JsonResult DeleteServiceListDetails(int RadioTempDetId)
    {
      this._uow.Repository<RADIOTemplateDetails>().GetEntitiesBySql(string.Format(Queries.DeleteRadioDetails, (object) RadioTempDetId));
      return this.Json((object) true);
    }

    [HttpGet]
    public JsonResult LabDetailsById(int Id, int RadioTempId)
    {
      return this.Json((object) new TemplateViewModal()
      {
        ServiceListDetails = this._uow.Repository<TemplateDetailsViewModel>().GetEntitiesBySql(string.Format(Queries.GetTemplateDetbyId, (object) RadioTempId, (object) Id))
      }.ServiceListDetails, JsonRequestBehavior.AllowGet);
    }
  }
}
