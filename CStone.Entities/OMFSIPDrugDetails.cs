// Decompiled with JetBrains decompiler
// Type: CStone.Entities.OMFSIPDrugDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("OMFSIPDrugDetails")]
  public class OMFSIPDrugDetails : EntityBase
  {
    [PrimaryKey]
    public int DrugDetailsId { get; set; }

    public int DrugId { get; set; }

    public int OMFSIpId { get; set; }

    public string Dose { get; set; }

    public string OrderFrom { get; set; }

    public int FromFlag { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }
  }
}
