// Decompiled with JetBrains decompiler
// Type: CStone.Entities.RADIOTemplateDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("RADIOTemplateDetails")]
  public class RADIOTemplateDetails : EntityBase
  {
    [PrimaryKey]
    public int RadioTempDetId { get; set; }

    public int RadioTempId { get; set; }

    public string ServiceItems { get; set; }

    public string DisplayOrder { get; set; }

    public string IsHeader { get; set; }

    public string MaleNormalRange { get; set; }

    public string FemaleNormalRange { get; set; }

    public string Unit { get; set; }
  }
}
