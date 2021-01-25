// Decompiled with JetBrains decompiler
// Type: CStone.Entities.CONSTraumatisedToothDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("CONSTraumatisedToothDetails")]
  public class CONSTraumatisedToothDetails : EntityBase
  {
    [PrimaryKey]
    public int TraumatisedToothId { get; set; }

    public int ConservativeId { get; set; }

    public string ToothNumber { get; set; }

    public string EllisType { get; set; }

    public string SoftTissueInjuries { get; set; }

    public string FacialSkeletalInjuries { get; set; }

    public string LuxationInjuries { get; set; }

    public string TtVitalityTesting { get; set; }

    public string TtRadiographicInterpretation { get; set; }

    public string TtImpressionPhotographsCast { get; set; }

    public string TtRestoration { get; set; }

    public string Splinting { get; set; }

    public string CrownLengthening { get; set; }

    public string SurgicalManagement { get; set; }

    public string OrthodonticIntrusion { get; set; }

    public string TtReview1 { get; set; }

    public string TtReview2 { get; set; }

    public string TtReview3 { get; set; }
  }
}
