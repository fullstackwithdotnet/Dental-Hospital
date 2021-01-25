// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.InvestigationController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Metron.Entities;

namespace DIMS.Controllers
{
  public class InvestigationController : Controller
  {
    private IUnitOfWork _uow;
    private IBillQueueService _BillQueueservice;

    public InvestigationController(IUnitOfWork uow, IBillQueueService BillQueueservice)
    {
      this._uow = uow;
      this._BillQueueservice = BillQueueservice;
    }

    public ActionResult Index()
    {
      return (ActionResult) this.View();
    }

    [HttpPost]
    public ActionResult AddBillingServices(BillingQueueServiceViewModel billingQueueViewModal)
    {
      return (ActionResult) this.PartialView("../Investigation/_InvestigationList", (object) new List<BillingQueueServiceViewModel>()
      {
        new BillingQueueServiceViewModel()
        {
          ServiceId = billingQueueViewModal.ServiceId,
          Amount = billingQueueViewModal.Amount,
          Qty = billingQueueViewModal.Qty,
          Rate = billingQueueViewModal.Rate,
          DiscountPer = billingQueueViewModal.DiscountPer,
          NetAmount = billingQueueViewModal.NetAmount,
          TeethNo = billingQueueViewModal.TeethNo
        }
      });
    }

    [HttpGet]
    public JsonResult GetBillingServices(int Id)
    {
      List<BillingQueueServiceViewModel> serviceViewModelList = new List<BillingQueueServiceViewModel>();
      foreach (MASBillingServices masBillingServices in this._uow.Repository<MASBillingServices>().GetAll(string.Format("ServiceId = {0}", (object) Id)))
        serviceViewModelList.Add(new BillingQueueServiceViewModel()
        {
          ServiceId = masBillingServices.ServiceId,
          Amount = masBillingServices.ServiceAmount,
          Qty = 1,
          Rate = masBillingServices.ServiceAmount
        });
      return this.Json((object) serviceViewModelList, JsonRequestBehavior.AllowGet);
    }

    [HttpPost]
    public JsonResult DeleteBillingQueueDetails(int BillQueueId)
    {
      if (BillQueueId > 0)
      {
        BillingQueueServiceViewModel serviceViewModel = this._uow.Repository<BillingQueueServiceViewModel>().GetEntitiesBySql(string.Format(Queries.GetBillQueueAppStatus, (object) BillQueueId)).FirstOrDefault<BillingQueueServiceViewModel>();
        if (serviceViewModel != null)
        {
          if (serviceViewModel.Approvalvalue.Trim() == "Y")
            return this.Json((object) false);
          int num = serviceViewModel.Approvalvalue.Trim() == "N" ? 1 : 0;
          this._BillQueueservice.Delete(BillQueueId);
          return this.Json((object) true);
        }
      }
      return this.Json((object) null);
    }

    public JsonResult GetLabRadGroup(int Id)
    {
      return this.Json((object) new SelectList((IEnumerable) this.GetServiceByDeptId(Id), "Value", "Text"));
    }

    public List<SelectListItem> GetServiceByDeptId(int Id)
    {
      string whereClause = string.Format("DeptId =" + (object) Id);
      List<SelectListItem> source1 = new List<SelectListItem>();
      IEnumerable<MASGroup> source2 = (IEnumerable<MASGroup>) this._uow.Repository<MASGroup>().GetAll(whereClause).OrderBy<MASGroup, string>((Func<MASGroup, string>) (x => x.GroupName));
      if (source2.Count<MASGroup>() > 0)
      {
        source1 = source2.Select<MASGroup, SelectListItem>((Func<MASGroup, SelectListItem>) (x => new SelectListItem()
        {
          Text = x.GroupName,
          Value = x.GroupId.ToString()
        })).ToList<SelectListItem>();
        source1.First<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected = true));
      }
      return source1;
    }

    public JsonResult GetLabRadServices(int Id)
    {
      return this.Json((object) new SelectList((IEnumerable) this.GetServiceByGroupId(Id), "Value", "Text"));
    }

    public List<SelectListItem> GetServiceByGroupId(int Id)
    {
      string whereClause = string.Format("GroupId =" + (object) Id);
      List<SelectListItem> source = new List<SelectListItem>();
      IEnumerable<MASBillingServices> list = (IEnumerable<MASBillingServices>) this._uow.Repository<MASBillingServices>().GetAll(whereClause).ToList<MASBillingServices>();
      if (list.Count<MASBillingServices>() > 0)
      {
        source = list.Select<MASBillingServices, SelectListItem>((Func<MASBillingServices, SelectListItem>) (x => new SelectListItem()
        {
          Text = x.ServiceName,
          Value = x.ServiceId.ToString()
        })).ToList<SelectListItem>();
        source.First<SelectListItem>((Func<SelectListItem, bool>) (x => x.Selected = true));
      }
      return source;
    }
  }
}
