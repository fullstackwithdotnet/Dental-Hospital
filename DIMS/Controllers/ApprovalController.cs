// Decompiled with JetBrains decompiler
// Type: DIMS.Controllers.ApprovalController
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
  public class ApprovalController : Controller
  {
    private IUnitOfWork _uow;
    private IApprovalService _service;
    private IMASCodeService _Dropdownservice;

    public ApprovalController(IUnitOfWork uow, IApprovalService service, IMASCodeService Dropdownservice)
    {
      this._uow = uow;
      this._service = service;
      this._Dropdownservice = Dropdownservice;
    }

    [CustomAuthorize(Roles = "Admin, HOD, Staff")]
    public ActionResult CaseSheetApproval(int ApprovalTypeId = 0, int CaserecordId = 0, int DeptId = 0, int DoctorId = 0, long PatientId = 0, int ReferredTreatmentId = 0)
    {
      ApprovalViewModal approvalViewModal1 = new ApprovalViewModal();
      ApprovalViewModal approvalViewModal2 = this._service.DisplayCaseSheetApproval(ApprovalTypeId, CaserecordId, DeptId, DoctorId, PatientId, ReferredTreatmentId);
      approvalViewModal2.ApprovalList = (IList<ApprovalViewModal>) this._uow.Repository<ApprovalViewModal>().GetEntitiesBySql(string.Format(Queries.PreviousApproval, (object) ApprovalTypeId, (object) CaserecordId, (object) DeptId, (object) PatientId, (object) ReferredTreatmentId)).ToList<ApprovalViewModal>();
      return (ActionResult) this.PartialView("../Approval/_ApprovalDetails", (object) approvalViewModal2);
    }

    public JsonResult CheckDoctorPassword(string Password, int DoctorId)
    {
      if (this._uow.Repository<MASDoctor>().GetAll("ApprovalPassword ='" + Password + "' and DoctorId =" + (object) DoctorId).Count<MASDoctor>() > 0)
        return this.Json((object) true);
      return this.Json((object) false);
    }

    [HttpPost]
    public ActionResult Create(ApprovalViewModal model)
    {
      if (this.ModelState.IsValid)
      {
        ApprovalDetails approvalDetails = new ApprovalDetails();
        ApprovalDetails entity = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<ApprovalViewModal, ApprovalDetails>())).CreateMapper().Map<ApprovalViewModal, ApprovalDetails>(model);
        entity.ApprovalDate = DateTime.Now;
        CustomPrincipal user = this.User as CustomPrincipal;
        entity.CreatedBy = user.Identity.Name;
        entity.CreatedDate = new DateTime?(DateTime.Now);
        entity.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
        this._service.Add(entity);
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
      this.ModelState.Clear();
      return (ActionResult) this.RedirectToAction(nameof (Create));
    }
  }
}
