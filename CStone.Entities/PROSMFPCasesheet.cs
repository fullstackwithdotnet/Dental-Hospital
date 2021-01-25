// Decompiled with JetBrains decompiler
// Type: CStone.Entities.PROSMFPCasesheet
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("PROSMFPCasesheet")]
  public class PROSMFPCasesheet : EntityBase
  {
    [PrimaryKey]
    public int ProsthoMFPId { get; set; }

    public string ProsthoMFPNo { get; set; }

    public int PatientId { get; set; }

    public DateTime? ProsthoMFPDate { get; set; }

    public string ChiefComplaint { get; set; }

    public string HistoryOfpresentIllness { get; set; }

    public string PastMedicalHistory { get; set; }

    public string PersonalHistory { get; set; }

    public string GeneralPhysicalExamination { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    public DateTime? LastVisitedDate { get; set; }

    public bool Approval1 { get; set; }

    public bool Approval2 { get; set; }

    public bool Approval3 { get; set; }

    public bool SendForApproval1 { get; set; }

    public bool SendForApproval2 { get; set; }

    public bool SendForApproval3 { get; set; }
  }
}
