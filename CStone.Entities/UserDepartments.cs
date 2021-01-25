// Decompiled with JetBrains decompiler
// Type: CStone.Entities.UserDepartments
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System.ComponentModel.DataAnnotations.Schema;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  [Table("UserDepartments")]
  public class UserDepartments : EntityBase
  {
    [PrimaryKey]
    public int UserDeptId { get; set; }

    public int UserId { get; set; }

    public int DeptId { get; set; }

    public string DeptCode { get; set; }
  }
}
