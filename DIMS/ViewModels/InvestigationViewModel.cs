// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.InvestigationViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using System;
using System.Collections.Generic;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class InvestigationViewModel
  {
    public long BillQueueId { get; set; }

    public DateTime BillQueueDate { get; set; }

    public int PatientId { get; set; }

    public int DeptId { get; set; }

    public int ServiceId { get; set; }

    public string ServiceName { get; set; }

    public int Qty { get; set; }

    public Decimal Rate { get; set; }

    public Decimal Amount { get; set; }

    public Decimal? DiscountPer { get; set; }

    public Decimal NetAmount { get; set; }

    public string TeethNo { get; set; }

    public string DiscountGivenBy { get; set; }

    public string DiscountPurpose { get; set; }

    public char IsBillPaid { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    public IEnumerable<MASDepartment> DepartmentList { get; set; }

    public IEnumerable<MASBillingServices> BillingServiceList { get; set; }
  }
}
