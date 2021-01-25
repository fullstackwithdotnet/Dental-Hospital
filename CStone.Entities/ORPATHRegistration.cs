// Decompiled with JetBrains decompiler
// Type: CStone.Entities.ORPATHRegistration
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  public class ORPATHRegistration : EntityBase
  {
    [PrimaryKey]
    public int OrpathLabId { get; set; }

    public string OrpathLabNo { get; set; }

    public int BillId { get; set; }

    public int PatientId { get; set; }

    public DateTime? SampleCollectedDate { get; set; }

    public DateTime? SampleCollectedTime { get; set; }

    public string SampleCollectedBy { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }
  }
}
