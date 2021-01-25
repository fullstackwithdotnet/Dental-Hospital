// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.ChartController
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Metron.Entities;

namespace DIMS.Controllers
{
  public class ChartController : Controller
  {
    private IUnitOfWork _uow;
    private IChartService _service;

    public ChartController(IUnitOfWork uow, IChartService service)
    {
      this._uow = uow;
      this._service = service;
    }

    public ActionResult Index()
    {
      return (ActionResult) this.View("../Chart/Index", (object) new ChartViewModel()
      {
        deptlist = (IEnumerable<MASDepartment>) this._uow.Repository<MASDepartment>().GetEntitiesBySql(string.Format(Queries.LoadServiceDept, (object) 0)).ToList<MASDepartment>()
      });
    }

    public JsonResult GetData(ChartViewModel Model)
    {
      IEnumerable<OPDPatientRegistration> list = (IEnumerable<OPDPatientRegistration>) this._uow.Repository<OPDPatientRegistration>().GetEntitiesBySql(string.Format(Queries.GetPatientCountforChart, (object) Model.FromDateValue, (object) Model.ToDateValue)).ToList<OPDPatientRegistration>();
      object[] objArray = new object[list.Count<OPDPatientRegistration>() + 1];
      objArray[0] = (object) new object[2]
      {
        (object) "Date",
        (object) "Patients"
      };
      int index = 0;
      foreach (OPDPatientRegistration patientRegistration in list)
      {
        ++index;
        objArray[index] = (object) new object[2]
        {
          (object) patientRegistration.DisplayDate,
          (object) patientRegistration.PatientId
        };
      }
      return this.Json((object) objArray);
    }
  }
}
