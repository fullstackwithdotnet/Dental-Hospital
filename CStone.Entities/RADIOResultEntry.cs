// Decompiled with JetBrains decompiler
// Type: CStone.Entities.RADIOResultEntry
// Assembly: CStone.Entities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 21054DA0-1390-4C7F-BB1A-575D1104CB0B
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\CStone.Entities.dll

using System;
using Repository.Base;
using Repository.Core;

namespace Metron.Entities
{
  public class RADIOResultEntry : EntityBase
  {
    [PrimaryKey]
    public int ResultId { get; set; }

    public int LabDetId { get; set; }

    public int ServiceId { get; set; }

    public int RadioTempId { get; set; }

    public string Impression { get; set; }

    public string Remarks { get; set; }

    public string AreaRadiographed { get; set; }

    public string Root { get; set; }

    public string NormalLandMarks { get; set; }

    public string RadiographicFault { get; set; }

    public string RadiographicDiagnosis { get; set; }

    public string Grade { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }
  }
}
