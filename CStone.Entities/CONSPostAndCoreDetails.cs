// Decompiled with JetBrains decompiler
// Type: CStone.Entities.CONSPostAndCoreDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("CONSPostAndCoreDetails")]
  public class CONSPostAndCoreDetails : EntityBase
  {
    [PrimaryKey]
    public int PostAndCoreId { get; set; }

    public int ConservativeId { get; set; }

    public string ToothNumber { get; set; }

    public string PhotographsImpression { get; set; }

    public string TypeOfPost { get; set; }

    public string PostSpacePreparation { get; set; }

    public string PatternImpression { get; set; }

    public string Temporization { get; set; }

    public string InsertionCementation { get; set; }

    public string InsertionTemporization { get; set; }

    public string PcReview1 { get; set; }

    public string PcReview2 { get; set; }

    public string PcReview3 { get; set; }
  }
}
