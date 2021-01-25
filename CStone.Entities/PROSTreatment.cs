// Decompiled with JetBrains decompiler
// Type: CStone.Entities.PROSTreatment
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;

namespace Metron.Entities
{
  [Table("PROSTreatment")]
  public class PROSTreatment : EntityBase
  {
    public int ProsTreatmentId { get; set; }

    public string TreatmentName { get; set; }

    public string Visibility { get; set; }
  }
}
