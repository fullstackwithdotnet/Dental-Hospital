// Decompiled with JetBrains decompiler
// Type: CStone.Entities.CONSBleachingDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("CONSBleachingDetails")]
  public class CONSBleachingDetails : EntityBase
  {
    [PrimaryKey]
    public int BleachingId { get; set; }

    public int ConservativeId { get; set; }

    public string ToothNumber { get; set; }

    public string BlVitalityTesting { get; set; }

    public string PhotographsCasts { get; set; }

    public string BlRadiographicInterpretation { get; set; }

    public string DiscolouredToothShade { get; set; }

    public string AdjacentToothShade { get; set; }

    public string BleachingProcedure { get; set; }

    public string BReview1 { get; set; }

    public string BReview2 { get; set; }

    public string BReview3 { get; set; }

    public string ProsthesisType { get; set; }
  }
}
