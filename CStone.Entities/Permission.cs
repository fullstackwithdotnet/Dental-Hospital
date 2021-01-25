// Decompiled with JetBrains decompiler
// Type: CStone.Entities.Permission
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("Permission")]
  public class Permission : EntityBase
  {
    [PrimaryKey]
    public int PermissionId { get; set; }

    public string PermissionName { get; set; }

    public string DisplayName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
  }
}
