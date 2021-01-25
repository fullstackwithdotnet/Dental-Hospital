// Decompiled with JetBrains decompiler
// Type: CStone.Entities.ORTHOStaticMandCranial
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  public class ORTHOStaticMandCranial : EntityBase
  {
    [PrimaryKey]
    public int CranialManStaticId { get; set; }

    public string CranialManMeasurements { get; set; }

    public string CranialManMean { get; set; }
  }
}
