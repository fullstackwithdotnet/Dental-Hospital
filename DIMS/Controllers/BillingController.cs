// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.BillingController
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
  public class BillingController : BaseController
  {
    private IUnitOfWork _uow;
    private IBillingService _service;

    public BillingController(IUnitOfWork uow, IBillingService service, IUserService userservice)
      : base(uow, userservice)
    {
      _uow = uow;
      _service = service;
    }

      [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
      public ActionResult Index()
      {
          GetPermissionforUser();
          if (!User.Departments.Contains(18))
              return View("../Error/AccessDenied");
          return View("../Billing/Index", new BillingSearchViewModal()
          {
              From_Date = DateTime.Now,
              To_Date = DateTime.Now
          });
      }

      public JsonResult GetNewBillsSearchList(BillingSearchViewModal model)
    {
      string From_Date = model.From_Date.ToString("yyyy-MM-dd");
      string To_Date = model.To_Date.ToString("yyyy-MM-dd");
      string rootUrl = User.GetRootUrl();
      model.SearchDetails = _service.BillingList(From_Date, To_Date, rootUrl).ToList();
      return Json((object) model.SearchDetails);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpGet]
    public ActionResult Create(int id, int DeptId)
    {
      GetPermissionforUser();
      if (!User.Departments.Contains(18))
        return (ActionResult) View("../Error/AccessDenied");
      BillingViewModal billingViewModal = new BillingViewModal();
      return (ActionResult) View("../Billing/Create", (object) _service.BindBillingModel(id, DeptId));
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    [HttpPost]
    public ActionResult Create(BillingViewModal ViewModel)
    {
      if (!User.Departments.Contains(18))
        return (ActionResult) View("../Error/AccessDenied");
      int num = 0;
      if (ModelState.IsValid)
      {
        num = _service.SaveBilling(ViewModel);
      }
      else
      {
        foreach (ModelState modelState in (IEnumerable<ModelState>) ViewData.ModelState.Values)
        {
          using (IEnumerator<ModelError> enumerator = modelState.Errors.GetEnumerator())
          {
            if (enumerator.MoveNext())
            {
              ModelError current = enumerator.Current;
              return (ActionResult) RedirectToAction("ErrorWrite", new RouteValueDictionary((object) new
              {
                controller = "Error",
                action = "ErrorWrite",
                message = (current.ErrorMessage + "-" + (object) current.Exception)
              }));
            }
          }
        }
      }
      ViewModel.BillId = num;
      return (ActionResult) RedirectToAction("BillingReport", new RouteValueDictionary((object) new
      {
        BillId = num,
        DeptId = ViewModel.DeptId
      }));
    }

    public ActionResult CancelBill(int BillId)
    {
      GetPermissionforUser();
      if (!User.Departments.Contains(18))
        return (ActionResult) View("../Error/AccessDenied");
      BillingSearchViewModal billingSearchViewModal = new BillingSearchViewModal();
      if (BillId > 0)
        _service.CancelBill(BillId);
      return (ActionResult) View("../Billing/PaidBillSearch", (object) billingSearchViewModal);
    }

    public ActionResult BillingReport(int BillId, int DeptId)
    {
      GetPermissionforUser();
      if (!User.Departments.Contains(19))
        return (ActionResult) View("../Error/AccessDenied");
      BillingViewModal billingViewModal = new BillingViewModal();
      if (BillId > 0)
      {
        billingViewModal = _service.BindReport(BillId, "Billing");
        billingViewModal.BillServicesListforReport = _service.BillServicesListforReport(BillId, "BillingDetails");
        billingViewModal.BillPaymentListforReport = _service.BillPaymentListforReport(BillId, "BillingPaymentDetails");
        billingViewModal.DeptId = DeptId;
      }
      return (ActionResult) View("../Reports/BillingReport", (object) billingViewModal);
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff, Student")]
    public ActionResult Search()
    {
      GetPermissionforUser();
      if (!User.Departments.Contains(18))
        return (ActionResult) View("../Error/AccessDenied");
      return (ActionResult) View("../Billing/PaidBillSearch", (object) new BillingSearchViewModal()
      {
        From_Date = DateTime.Now,
        To_Date = DateTime.Now
      });
    }

    public JsonResult GetPaidBillsSearchList(BillingSearchViewModal model)
    {
      string From_Date = model.From_Date.ToString("yyyy-MM-dd");
      string To_Date = model.To_Date.ToString("yyyy-MM-dd");
      string rootUrl = User.GetRootUrl();
      model.SearchDetails = _service.PaidBillsList(From_Date, To_Date, rootUrl).ToList<BillSearchDetails>();
      return Json((object) model.SearchDetails);
    }
  }
}
