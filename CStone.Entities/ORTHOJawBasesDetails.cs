﻿// Decompiled with JetBrains decompiler
// Type: CStone.Entities.ORTHOJawBasesDetails
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("ORTHOJawBasesDetails")]
  public class ORTHOJawBasesDetails : EntityBase
  {
    [PrimaryKey]
    public int JawBasesId { get; set; }

    public int JawBasesStaticId { get; set; }

    public int OrthoId { get; set; }

    public string PreRx { get; set; }

    public string PostModulation { get; set; }

    public string PostRx { get; set; }

    public string Retention { get; set; }

    public string Change { get; set; }
  }
}
