﻿// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.OPDSearchDetails
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;

namespace DIMS.ViewModels
{
  public class OPDSearchDetails : EntityBase
  {
    public string RegDate { get; set; }

    public string RegTime { get; set; }

    public long PatientId { get; set; }

    public long OPNo { get; set; }

    public string PatientName { get; set; }

    public int? Age { get; set; }

    public string Area { get; set; }

    public string Category { get; set; }

    public string Link { get; set; }

    public string Phone { get; set; }

    public string Revisit { get; set; }

    public string Refferal { get; set; }
  }
}
