// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.ORPATHTemplateController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using Repository.Base;
using System.Web.Mvc;

namespace DIMS.Controllers
{
  public class ORPATHTemplateController : Controller
  {
    private IUnitOfWork _uow;
    private ITemplateService _radiology;

    public ORPATHTemplateController(IUnitOfWork uow, ITemplateService radiology)
    {
      this._uow = uow;
      this._radiology = radiology;
    }
  }
}
