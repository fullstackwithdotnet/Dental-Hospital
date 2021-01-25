// Decompiled with JetBrains decompiler
// Type: CStone.Entities.UserRoles
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("UserRoles")]
  public class UserRoles : EntityBase
  {
    [PrimaryKey]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
  }
}
