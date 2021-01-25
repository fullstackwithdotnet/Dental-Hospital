// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Abstract.IBillQueueService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.ViewModels;
using Repository.Base;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.Services.Abstract
{
  public interface IBillQueueService : IService<BillQueueDetails>
  {
    IEnumerable<BillingQueueServiceViewModel> BillingList();

    IEnumerable<BillingQueueServiceViewModel> BillServicesList(int PatientId, int DeptId, int TreamentId);

    IEnumerable<BillingQueueServiceViewModel> BillLabRadServicesList(int PatientId, int DeptId, int TreamentId);

    IEnumerable<BillingQueueServiceViewModel> BillingServicesList(int id, int DeptId);

    IEnumerable<BillingQueueServiceViewModel> RequisitionServicesList(int PatientId, int DeptId, int RequisitionId);

    IEnumerable<BillingQueueServiceViewModel> RequisitionList(int PatientId, int DeptId);

    int SaveInvestigation(IEnumerable<BillingQueueServiceViewModel> model, BillQueueDetails billDetails);

    BillingQueueServiceViewModel GetServicesListByDeptId(int DeptId);

    BillingQueueServiceViewModel GetServicesListByLabRad();

    IEnumerable<BillingViewModal> BillpaidInvestigationList(int PatientId, int DeptId);

    IEnumerable<BillingViewModal> BillpaidtreatemList(int PatientId, int DeptId);
  }
}
