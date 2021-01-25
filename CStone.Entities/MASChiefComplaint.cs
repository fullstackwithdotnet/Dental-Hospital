// Decompiled with JetBrains decompiler
// Type: CStone.Entities.MASChiefComplaint
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  public class MASChiefComplaint : EntityBase
  {
    [PrimaryKey]
    public int ChiefComplaintId { get; set; }

    public string ChiefComplaintType { get; set; }

    public string ChiefComplaint { get; set; }
  }
}
