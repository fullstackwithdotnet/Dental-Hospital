// Decompiled with JetBrains decompiler
// Type: CStone.Entities.CONSRestorativeProcedureDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("CONSRestorativeProcedureDetails")]
  public class CONSRestorativeProcedureDetails : EntityBase
  {
    [PrimaryKey]
    public int RestorativeProId { get; set; }

    public int ConservativeId { get; set; }

    public string ToothNumber { get; set; }

    public string CavityType { get; set; }

    public string PhotographsCastsXray { get; set; }

    public string RestorativeMaterial { get; set; }

    public string CavityPreparation { get; set; }

    public string ImpressionPattern { get; set; }

    public string LinearBasesVarnish { get; set; }

    public string MatrixBandWedges { get; set; }

    public string Restoration { get; set; }

    public string FinishingAndRestoration { get; set; }

    public string DirectIndirectReview1 { get; set; }

    public string DirectIndirectReview2 { get; set; }

    public string DirectIndirectReview3 { get; set; }

    public string DeepCariesReview1 { get; set; }

    public string DeepCariesReview2 { get; set; }

    public string DeepCariesReview3 { get; set; }
  }
}
