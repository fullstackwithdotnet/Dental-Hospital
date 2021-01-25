// Decompiled with JetBrains decompiler
// Type: CStone.Entities.RADIORegistrationDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  public class RADIORegistrationDetails : EntityBase
  {
    [PrimaryKey]
    public int LabDetId { get; set; }

    public int LabId { get; set; }

    public int ServiceId { get; set; }

    public string TeethNo { get; set; }

    public string TestDone { get; set; }

    public DateTime TestDate { get; set; }
  }
}
