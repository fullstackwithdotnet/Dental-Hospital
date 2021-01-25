// Decompiled with JetBrains decompiler
// Type: CStone.Entities.OMFSIPOperativeVitalsDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("OMFSIPOperativeVitalsDetails")]
  public class OMFSIPOperativeVitalsDetails : EntityBase
  {
    [PrimaryKey]
    public int VitalId { get; set; }

    public int OMFSIpId { get; set; }

    public DateTime? VitalDate { get; set; }

    public DateTime? VitalTime { get; set; }

    public string Bp { get; set; }

    public string VitalBp { get; set; }

    public string Pulse { get; set; }

    public string RespiratoryRate { get; set; }

    public string Temperature { get; set; }

    public string Input { get; set; }

    public string Output { get; set; }

    public string Miscellaneous { get; set; }

    public int FromFlag { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }
  }
}
