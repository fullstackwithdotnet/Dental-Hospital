// Decompiled with JetBrains decompiler
// Type: CStone.Entities.ReferredToOthers
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  public class ReferredToOthers : EntityBase
  {
    [PrimaryKey]
    public int ReferredOthersId { get; set; }

    public int PatientId { get; set; }

    public DateTime? ReferredOthersDate { get; set; }

    public int DeptId { get; set; }

    public int CaseRecordId { get; set; }

    public int TreatmentId { get; set; }

    public string DoctorName { get; set; }

    public string HospitalName { get; set; }

    public string ReferredOthersReason { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }
  }
}
