// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.ReferralStatusService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class ReferralStatusService : ServiceBase<ReferralStatus>, IReferralStatusService, IService<ReferralStatus>
  {
    private IUnitOfWork _uow;
    private IMASCodeService _Dropdownservice;

    public ReferralStatusService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._Dropdownservice = (IMASCodeService) new MASCodeService(this._uow);
    }

    public IEnumerable<ReferralStatusViewModel> ReferralList(int PatientId)
    {
      List<ReferralStatusViewModel> referralStatusViewModelList = new List<ReferralStatusViewModel>();
      return (IEnumerable<ReferralStatusViewModel>) this._uow.Repository<ReferralStatusViewModel>().GetEntitiesBySql(string.Format(Queries.ReferralListWithLink, (object) PatientId)).ToList<ReferralStatusViewModel>();
    }

    public IEnumerable<ReferralStatusViewModel> DefaultReferralList(int PatientId, int FromDeptId)
    {
      List<ReferralStatusViewModel> referralStatusViewModelList = new List<ReferralStatusViewModel>();
      return (IEnumerable<ReferralStatusViewModel>) this._uow.Repository<ReferralStatusViewModel>().GetEntitiesBySql(string.Format(Queries.ReferralByDefault, (object) FromDeptId)).ToList<ReferralStatusViewModel>();
    }

    public void UpdateReferralStatus(ReferralStatus referralStatus)
    {
      this._uow.Repository<ReferralStatus>().GetEntitiesBySql(string.Format(Queries.UpdateReferralStatus, (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), (object) referralStatus.VisitType, (object) referralStatus.ReferredTreatmentId, (object) DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), (object) this._Dropdownservice.GetIPAddress(false), (object) referralStatus.ReferredId));
    }

    public IEnumerable<ReferralStatusViewModel> ReferralApprovalList(int PatientId, int FromDeptId)
    {
      List<ReferralStatusViewModel> referralStatusViewModelList = new List<ReferralStatusViewModel>();
      return (IEnumerable<ReferralStatusViewModel>) this._uow.Repository<ReferralStatusViewModel>().GetEntitiesBySql(string.Format(Queries.ReferralApprovalListWithLink, (object) PatientId, (object) FromDeptId)).ToList<ReferralStatusViewModel>();
    }

    public void DeletePreviousUnTreatedList(int PatientId, int FromDeptId, int ToDeptId)
    {
      this._uow.Repository<ReferralStatus>().GetEntitiesBySql(string.Format(Queries.DeletePreviousUnTreatedPatientList, (object) PatientId, (object) FromDeptId, (object) ToDeptId));
    }
  }
}
