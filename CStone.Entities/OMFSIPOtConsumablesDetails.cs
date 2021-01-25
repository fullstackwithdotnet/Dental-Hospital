// Decompiled with JetBrains decompiler
// Type: CStone.Entities.OMFSIPOtConsumablesDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("OMFSIPOtConsumablesDetails")]
  public class OMFSIPOtConsumablesDetails : EntityBase
  {
    [PrimaryKey]
    public int ConsumableId { get; set; }

    public int OMFSIpId { get; set; }

    public int ProductId { get; set; }

    public string MaterialUsed { get; set; }

    public int MaterialQty { get; set; }

    public Decimal MaterialRate { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }
  }
}
