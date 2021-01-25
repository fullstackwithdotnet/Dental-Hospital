// Decompiled with JetBrains decompiler
// Type: CStone.Entities.ORPATHCasesheet
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("ORPATHCasesheet")]
  public class ORPATHCasesheet : EntityBase
  {
    [PrimaryKey]
    public int RequisitionId { get; set; }

    public int PatientId { get; set; }

    public string RequisitionNo { get; set; }

    public DateTime RequisitionDate { get; set; }

    public DateTime? LastVisitedDate { get; set; }

    public int DeptId { get; set; }

    public string MChiefComplaint { get; set; }

    public string BChiefComplaint { get; set; }

    public DateTime? MSampleCollectionDate { get; set; }

    public DateTime? MSampleCollectionTime { get; set; }

    public DateTime? MCulturing { get; set; }

    public DateTime? BBiopsyCollectionDate { get; set; }

    public DateTime? BBiopsyCollectionTime { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    public bool Approval1 { get; set; }

    public bool Approval2 { get; set; }

    public bool Approval3 { get; set; }

    public bool SendForApproval1 { get; set; }

    public bool SendForApproval2 { get; set; }

    public bool SendForApproval3 { get; set; }
  }
}
