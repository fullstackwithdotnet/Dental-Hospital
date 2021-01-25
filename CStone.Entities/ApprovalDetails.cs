// Decompiled with JetBrains decompiler
// Type: CStone.Entities.ApprovalDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  public class ApprovalDetails : EntityBase
  {
    [PrimaryKey]
    public long ApprovalId { get; set; }

    public DateTime ApprovalDate { get; set; }

    public int ApprovalTypeId { get; set; }

    public int Revision { get; set; }

    public long PatientId { get; set; }

    public int DoctorId { get; set; }

    public int DeptId { get; set; }

    public int CaserecordId { get; set; }

    public int ReferredTreatmentId { get; set; }

    public string Reason { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string CreatedSystem { get; set; }
  }
}
