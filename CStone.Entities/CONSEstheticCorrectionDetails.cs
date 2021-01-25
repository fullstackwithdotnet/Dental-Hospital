// Decompiled with JetBrains decompiler
// Type: CStone.Entities.CONSEstheticCorrectionDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("CONSEstheticCorrectionDetails")]
  public class CONSEstheticCorrectionDetails : EntityBase
  {
    [PrimaryKey]
    public int EstheticCorrId { get; set; }

    public int ConservativeId { get; set; }

    public string ToothNumber { get; set; }

    public string ImpressionPhotograph { get; set; }

    public string VitalityTesting { get; set; }

    public string RadiographicInterpretation { get; set; }

    public string TreatmentProcedure { get; set; }

    public string ToothPreparation { get; set; }

    public string ShadeSelection { get; set; }

    public string EcInsertionCementation { get; set; }

    public string FinishingPolishing { get; set; }

    public string EcReview1 { get; set; }

    public string EcReview2 { get; set; }
  }
}
