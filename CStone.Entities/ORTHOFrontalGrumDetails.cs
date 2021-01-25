// Decompiled with JetBrains decompiler
// Type: CStone.Entities.ORTHOFrontalGrumDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("ORTHOFrontalGrumDetails")]
  public class ORTHOFrontalGrumDetails : EntityBase
  {
    [PrimaryKey]
    public int FrontalGrumId { get; set; }

    public int FrontalGrumStaticId { get; set; }

    public int OrthoId { get; set; }

    public string LeftPreRx { get; set; }

    public string LeftPostRx { get; set; }

    public string RightPreRx { get; set; }

    public string RightPostRx { get; set; }

    public string DiffPreRx { get; set; }

    public string DiffPostRx { get; set; }
  }
}
