// Decompiled with JetBrains decompiler
// Type: CStone.Entities.ORTHOStaticFrontalEsthetics
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  public class ORTHOStaticFrontalEsthetics : EntityBase
  {
    [PrimaryKey]
    public int FrontalEstheticsStaticId { get; set; }

    public string Field { get; set; }

    public string Percentage { get; set; }
  }
}
