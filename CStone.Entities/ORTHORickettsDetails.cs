// Decompiled with JetBrains decompiler
// Type: CStone.Entities.ORTHORickettsDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("ORTHORickettsDetails")]
  public class ORTHORickettsDetails : EntityBase
  {
    [PrimaryKey]
    public int RickettsId { get; set; }

    public int RickettsStaticId { get; set; }

    public int OrthoId { get; set; }

    public string PreRx { get; set; }

    public string During { get; set; }

    public string PostRx { get; set; }
  }
}
