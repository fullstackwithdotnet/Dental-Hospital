// Decompiled with JetBrains decompiler
// Type: CStone.Entities.StudentAllotment
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("StudentAllotment")]
  public class StudentAllotment : EntityBase
  {
    [PrimaryKey]
    public long AllotId { get; set; }

    public DateTime? AllotDate { get; set; }

    public int PatientId { get; set; }

    public long ReferredId { get; set; }

    public int DeptId { get; set; }

    public int AllotTypeId { get; set; }

    public int StudentId { get; set; }

    public int? DoctorId { get; set; }

    public int CaserecordId { get; set; }

    public int ApprovalType { get; set; }

    public string ProcedureNotes { get; set; }

    public DateTime? ProcedureNotesDate { get; set; }

    public string DoctorApproval { get; set; }

    public DateTime? DoctorApprovalDate { get; set; }

    public int ReferredTreatmentId { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }
  }
}
