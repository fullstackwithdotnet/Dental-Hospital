// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IReferralStatusService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IReferralStatusService : IService<ReferralStatus>
  {
    IEnumerable<ReferralStatusViewModel> ReferralList(int PatientId);

    IEnumerable<ReferralStatusViewModel> DefaultReferralList(int PatientId, int FromDeptId);

    IEnumerable<ReferralStatusViewModel> ReferralApprovalList(int PatientId, int FromDeptId);

    void UpdateReferralStatus(ReferralStatus referralStatus);

    void DeletePreviousUnTreatedList(int PatientId, int FromDeptId, int ToDeptId);
  }
}
