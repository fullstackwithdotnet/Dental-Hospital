﻿// Decompiled with JetBrains decompiler
// Type: CStone.Entities.OMFSIPCasesheetPropertyValues
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("OMFSIPCasesheetPropertyValues")]
  public class OMFSIPCasesheetPropertyValues : EntityBase
  {
    [PrimaryKey]
    public int ValueId { get; set; }

    public int PropId { get; set; }

    public int OMFSIpId { get; set; }

    public string PropValues { get; set; }
  }
}
