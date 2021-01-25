// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.ErrorController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using Repository.Base;
using System.Web.Mvc;

namespace DIMS.Controllers
{
  public class ErrorController : Controller
  {
    private IUnitOfWork _uow;
    private IErrorLogService _service;

    public ErrorController(IUnitOfWork uow, IErrorLogService service)
    {
      this._uow = uow;
      this._service = service;
    }

    public ActionResult AccessDenied()
    {
      return (ActionResult) this.View();
    }

    public ActionResult Error()
    {
      return (ActionResult) this.View();
    }

    public ActionResult ErrorWrite(string message)
    {
      this._service.LogException(message);
      return (ActionResult) this.View();
    }
  }
}
