﻿// Decompiled with JetBrains decompiler
// Type: CStone.Entities.ORTHOStaticSoftTissue
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using Repository.Base;

namespace Metron.Entities
{
  public class ORTHOStaticSoftTissue : EntityBase
  {
    public int SoftTissueStaticId { get; set; }

    public string Measurements { get; set; }

    public string Mean { get; set; }

    public string DelInd { get; set; }
  }
}
