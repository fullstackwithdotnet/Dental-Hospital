// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.BillSearchDetails
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using System;

namespace DIMS.ViewModels
{
  public class BillSearchDetails : EntityBase
  {
    public int? BillId { get; set; }

    public string DisplayBillDateTime { get; set; }

    public string BillNo { get; set; }

    public string PatientName { get; set; }

    public string DeptName { get; set; }

    public string Link { get; set; }

    public Decimal NetAmount { get; set; }

    public Decimal BillAmount { get; set; }

    public long OpNo { get; set; }
  }
}
