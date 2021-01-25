// Decompiled with JetBrains decompiler
// Type: CStone.Entities.PROSMFPTreatmentDescription
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("PROSMFPTreatmentDescription")]
  public class PROSMFPTreatmentDescription : EntityBase
  {
    [PrimaryKey]
    public int ProsMaxilloProId { get; set; }

    public int ProsthoMFPId { get; set; }

    public DateTime? MfpTreatmentDate { get; set; }

    public string MfpTreatmentDescription { get; set; }

    public string MfpRemarks { get; set; }
  }
}
