// Decompiled with JetBrains decompiler
// Type: CStone.Entities.OMFSIPSchedule
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("OMFSIPSchedule")]
  public class OMFSIPSchedule : EntityBase
  {
    [PrimaryKey]
    public int ScheduleId { get; set; }

    public int PatientId { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public DateTime? FromTime { get; set; }

    public DateTime? ToTime { get; set; }

    public string Duration { get; set; }

    public string Operation { get; set; }

    public int TypeId { get; set; }

    public string Anaesthetist { get; set; }

    public int StatusId { get; set; }

    public long ReferredId { get; set; }

    public string AssistantSurgeon { get; set; }

    public string Anesthetist { get; set; }

    public string AssistingNurse { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }
  }
}
