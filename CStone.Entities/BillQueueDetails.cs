// Decompiled with JetBrains decompiler
// Type: CStone.Entities.BillQueueDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
    public class BillQueueDetails : EntityBase
    {
        [PrimaryKey] public int BillQueueId { get; set; }

        public DateTime? BillQueueDate { get; set; }

        public int PatientId { get; set; }

        public int DeptId { get; set; }

        public int ServiceId { get; set; }
        public int ChildServiceId { get; set; }
       // public string ChildServiceName { get; set; }

        public int Qty { get; set; }

        public Decimal Rate { get; set; }

        public Decimal Amount { get; set; }

        public Decimal DiscountPer { get; set; }

        public Decimal DiscountAmt { get; set; }

        public Decimal NetAmount { get; set; }

        public Decimal BalanceAmount { get; set; }

        public Decimal PayableAmount { get; set; }

        public Decimal PaidAmount { get; set; }

        public string TeethNo { get; set; }

        public string DiscountGivenBy { get; set; }

        public string DiscountPurpose { get; set; }

        public string IsBillPaid { get; set; }

        public int RequisitionId { get; set; }

        public int CaserecordId { get; set; }

        public int ReferredTreatmentId { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string CreatedSystem { get; set; }

        public string ModifiedSystem { get; set; }
    }
}
