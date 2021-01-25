// Decompiled with JetBrains decompiler
// Type: CStone.Entities.CONSSurgicalProcedureDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("CONSSurgicalProcedureDetails")]
  public class CONSSurgicalProcedureDetails : EntityBase
  {
    [PrimaryKey]
    public int SurgicalProId { get; set; }

    public int ConservativeId { get; set; }

    public string ToothNumber { get; set; }

    public string Premedication { get; set; }

    public string AnesthesiaUsed { get; set; }

    public string FlapDesign { get; set; }

    public string Graft { get; set; }

    public string Suturing { get; set; }

    public string SpReview1 { get; set; }

    public string SpReview2 { get; set; }

    public string SpReview3 { get; set; }
  }
}
