// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.ApprovalService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Metron.Entities;
using Repository.Base;
using Repository.Core;

namespace DIMS.Services.Implementation
{
  public class ApprovalService : ServiceBase<ApprovalDetails>, IApprovalService, IService<ApprovalDetails>
  {
    private IUnitOfWork _uow;

    public ApprovalService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
    }

    public ApprovalViewModal DisplayCaseSheetApproval(int ApprovalTypeId, int CaserecordId, int DeptId, int DoctorId, long PatientId, int ReferredTreatmentId)
    {
      return new ApprovalViewModal()
      {
        ApprovalTypeId = ApprovalTypeId,
        CaserecordId = CaserecordId,
        DeptId = DeptId,
        DoctorId = new int?(DoctorId),
        PatientId = PatientId,
        ReferredTreatmentId = ReferredTreatmentId,
        ApprovalDoctorName = this._uow.Repository<MASDoctor>().Get(DoctorId).DoctorName
      };
    }
  }
}
