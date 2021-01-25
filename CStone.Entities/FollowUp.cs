// Decompiled with JetBrains decompiler
// Type: CStone.Entities.FollowUp
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  public class FollowUp : EntityBase
  {
    [PrimaryKey]
    public int FollowupId { get; set; }

    public int PatientId { get; set; }

    public DateTime? FollowupDate { get; set; }

    public DateTime? FollowupTime { get; set; }

    public int DeptId { get; set; }

    public string FollowupReason { get; set; }

    public int RevisitId { get; set; }

    public string Status { get; set; }

    public string IgnoreReason { get; set; }

    public int ReferredTreatmentId { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }
  }
}
