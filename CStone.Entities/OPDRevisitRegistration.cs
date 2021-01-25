// Decompiled with JetBrains decompiler
// Type: CStone.Entities.OPDRevisitRegistration
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("OPDRevisitRegistration")]
  public class OPDRevisitRegistration : EntityBase
  {
    [PrimaryKey]
    public int RevisitId { get; set; }

    public DateTime? RevisitDate { get; set; }

    public int PatientId { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }
  }
}
