// Decompiled with JetBrains decompiler
// Type: CStone.Entities.BillingPaymentDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  public class BillingPaymentDetails : EntityBase
  {
    [PrimaryKey]
    public int BillPayId { get; set; }

    public int BillId { get; set; }

    public int CodeId { get; set; }

    public string ReferenceNo { get; set; }

    public Decimal AmountReceived { get; set; }
  }
}
