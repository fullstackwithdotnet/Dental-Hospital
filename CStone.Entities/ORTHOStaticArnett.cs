// Decompiled with JetBrains decompiler
// Type: CStone.Entities.ORTHOStaticArnett
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("ORTHOStaticArnett")]
  public class ORTHOStaticArnett : EntityBase
  {
    [PrimaryKey]
    public int ArnettStaticId { get; set; }

    public string Measurements { get; set; }

    public string Female { get; set; }

    public string Male { get; set; }

    public string Indicator { get; set; }
  }
}
