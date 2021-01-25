// Decompiled with JetBrains decompiler
// Type: CStone.Entities.CONSIncompleteRootFormDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("CONSIncompleteRootFormDetails")]
  public class CONSIncompleteRootFormDetails : EntityBase
  {
    [PrimaryKey]
    public int IncompleteRootId { get; set; }

    public int ConservativeId { get; set; }

    public string ToothNumber { get; set; }

    public string IncVitality { get; set; }

    public string IncPhotographCast { get; set; }

    public string IncRadiographInterpretation { get; set; }

    public string IncTreatmentProcedure { get; set; }

    public string IncMaterialChoice { get; set; }

    public string IncSiteOfManagement { get; set; }

    public string IncOtherRestorativePro { get; set; }

    public string IncReview1 { get; set; }

    public string IncReview2 { get; set; }

    public string IncReview3 { get; set; }
  }
}
