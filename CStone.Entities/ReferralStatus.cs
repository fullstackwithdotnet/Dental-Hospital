// Decompiled with JetBrains decompiler
// Type: CStone.Entities.ReferralStatus
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  public class ReferralStatus : EntityBase
  {
    [PrimaryKey]
    public long ReferredId { get; set; }

    public int PatientId { get; set; }

    public int FromdeptId { get; set; }

    public DateTime? FromDate { get; set; }

    public string ReferredReason { get; set; }

    public int ToDeptId { get; set; }

    public string Priority { get; set; }

    public string RoomNo { get; set; }

    public string TreatmentStatus { get; set; }

    public DateTime? TreatmentDate { get; set; }

    public int ReferredTreatmentId { get; set; }

    public string VisitType { get; set; }

    public int RevisitId { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    public string IsApproved { get; set; }
  }
}
