// Decompiled with JetBrains decompiler
// Type: CStone.Entities.ORTHOGrowthDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("ORTHOGrowthDetails")]
  public class ORTHOGrowthDetails : EntityBase
  {
    [PrimaryKey]
    public int GrowthId { get; set; }

    public int GrowthStaticId { get; set; }

    public int OrthoId { get; set; }

    public string PtValue { get; set; }
  }
}
