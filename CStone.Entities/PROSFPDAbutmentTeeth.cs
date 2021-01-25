// Decompiled with JetBrains decompiler
// Type: CStone.Entities.PROSFPDAbutmentTeeth
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("PROSFPDAbutmentTeeth")]
  public class PROSFPDAbutmentTeeth : EntityBase
  {
    [PrimaryKey]
    public int AbutmentTeethId { get; set; }

    public int ProsthoFPDId { get; set; }

    public string ToothNumber { get; set; }

    public string Location { get; set; }

    public string Crown { get; set; }

    public string Size { get; set; }

    public string Fracture { get; set; }

    public string Length { get; set; }

    public string Discoloration { get; set; }

    public string Position { get; set; }

    public string Wearfacets { get; set; }

    public string Caries { get; set; }

    public string Restorations { get; set; }

    public string Vitality { get; set; }

    public string Mobility { get; set; }
  }
}
