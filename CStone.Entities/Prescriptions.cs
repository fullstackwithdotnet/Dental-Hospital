// Decompiled with JetBrains decompiler
// Type: CStone.Entities.Prescriptions
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("Prescriptions")]
  public class Prescriptions : EntityBase
  {
    [PrimaryKey]
    public int PrescriptionId { get; set; }

    public DateTime? PrescriptionDate { get; set; }

    public int PatientId { get; set; }

    public int? DoctorId { get; set; }

    public int StudentId { get; set; }

    public int DeptId { get; set; }

    public long AllotId { get; set; }

    public int ReferredTreatmentId { get; set; }

    public int TypeId { get; set; }

    public string PresMedication { get; set; }

    public string PresStrength { get; set; }

    public Decimal PrescriptionQty { get; set; }

    public string PresFrequency { get; set; }

    public string PresDays { get; set; }

    public string PresTimes { get; set; }

    public string PresNotes { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }
  }
}
