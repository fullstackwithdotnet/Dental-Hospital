// Decompiled with JetBrains decompiler
// Type: CStone.Entities.LaboratoryResultEntryDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  public class LaboratoryResultEntryDetails : EntityBase
  {
    [PrimaryKey]
    public int LaboratoryResultDetId { get; set; }

    public int LaboratoryResultId { get; set; }

    public int RadioTempDetId { get; set; }

    public string Result { get; set; }
  }
}
