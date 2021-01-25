// Decompiled with JetBrains decompiler
// Type: CStone.Entities.OMFSIPCasesheetProperties
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("OMFSIPCasesheetProperties")]
  public class OMFSIPCasesheetProperties : EntityBase
  {
    [PrimaryKey]
    public int PropertyId { get; set; }

    public string PropertyName { get; set; }

    public string PropertyDataType { get; set; }

    public string PropertyDisplayName { get; set; }

    public string PropertyVisibility { get; set; }
  }
}
