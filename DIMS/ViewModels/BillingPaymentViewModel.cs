// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.BillingPaymentViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class BillingPaymentViewModel
  {
    public int BillPayId { get; set; }

    public int BillId { get; set; }

    [DisplayName("Paymode")]
    public int CodeId { get; set; }

    public string CodeDescription { get; set; }

    public IEnumerable<MASPaymode> Paymodelist { get; set; }

    [DisplayName("Reference No")]
    public string ReferenceNo { get; set; }

    [DisplayName("Amount ")]
    public Decimal AmountReceived { get; set; }

    public bool IsCashChk { get; set; }

    public bool IsCardChk { get; set; }

    public bool IsInsuranceChk { get; set; }

    public string Cash { get; set; }

    public string Card { get; set; }

    public string Insurance { get; set; }
  }
}
