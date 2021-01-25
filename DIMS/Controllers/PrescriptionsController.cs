// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.PrescriptionsController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using System.Web.Mvc;

namespace DIMS.Controllers
{
  public class PrescriptionsController : BaseController
  {
    private IUnitOfWork _uow;
    private IPrescriptionsService _service;

    public PrescriptionsController(IUnitOfWork uow, IPrescriptionsService service, IUserService userservice)
      : base(uow, userservice)
    {
      this._uow = uow;
      this._service = service;
    }

    public ActionResult PresciptionReport(long AllotId)
    {
      PrescriptionsViewModel prescriptionsViewModel = new PrescriptionsViewModel();
      return (ActionResult) this.View("../Reports/PreviousPrescriptionReport", (object) this._service.PrescriptionReport(AllotId));
    }

    [HttpPost]
    public JsonResult DeleteBillingQueueDetails(int PrescriptionId)
    {
      this._service.Delete(PrescriptionId);
      return this.Json((object) true);
    }
  }
}
