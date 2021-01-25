// Decompiled with JetBrains decompiler
// Type: CStone.Entities.MASBillingServices
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  public class MASBillingServices : EntityBase
  {
    [PrimaryKey]
    public int ServiceId { get; set; }

    public string ServiceCode { get; set; }

    public string ServiceName { get; set; }

    public int DeptId { get; set; }

    public int GroupId { get; set; }

    public Decimal ServiceAmount { get; set; }

    public string HSNSACCode { get; set; }

    public string GSTPercentage { get; set; }

    public int ServiceType { get; set; }

    public string DeptCode { get; set; }

    public string Link { get; set; }

    public string GroupName { get; set; }
      public int DelInd { get; set; }
  }
}
