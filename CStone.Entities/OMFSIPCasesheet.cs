// Decompiled with JetBrains decompiler
// Type: CStone.Entities.OMFSIPCasesheet
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("OMFSIPCasesheet")]
  public class OMFSIPCasesheet : EntityBase
  {
    [PrimaryKey]
    public int OMFSIpId { get; set; }

    public int PatientId { get; set; }

    public int ScheduleId { get; set; }

    public DateTime OMFSIpDate { get; set; }

    public string OMFSIpNo { get; set; }

    public string ChiefComplaint { get; set; }

    public DateTime? PreMedicationTime { get; set; }

    public DateTime? InductionTime { get; set; }

    public DateTime? IntubationTime { get; set; }

    public DateTime? ExtubationTime { get; set; }

    public DateTime? CircuitTime { get; set; }

    public DateTime? AnaesthesiaSedationReversalTime { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? LastVisitedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }
  }
}
