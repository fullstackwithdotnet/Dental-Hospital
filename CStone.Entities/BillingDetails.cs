// Decompiled with JetBrains decompiler
// Type: CStone.Entities.BillingDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  public class BillingDetails : EntityBase
  {
    [PrimaryKey]
    public int BillDetId { get; set; }

    public int BillId { get; set; }

    public int BillQueueId { get; set; }

    public int ServiceId { get; set; }

    public int ServiceQty { get; set; }

    public Decimal ServiceRate { get; set; }

    public Decimal ServiceAmount { get; set; }

    public Decimal GSTPercentage { get; set; }

    public Decimal ServiceCGST { get; set; }

    public Decimal ServiceSGST { get; set; }

    public Decimal DiscountPer { get; set; }

    public Decimal DiscountAmt { get; set; }

    public Decimal NetAmount { get; set; }

    public string IsBillPaid { get; set; }

    public Decimal PayableAmount { get; set; }
  }
}
