﻿// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.OMRSearchDetails
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;

namespace DIMS.ViewModels
{
  public class OMRSearchDetails : EntityBase
  {
    public int? OMRId { get; set; }

    public long AllotId { get; set; }

    public string OMRDate { get; set; }

    public string OMRNo { get; set; }

    public long OpNo { get; set; }

    public string PatientName { get; set; }

    public int? Age { get; set; }

    public string Phone { get; set; }

    public string GenderName { get; set; }

    public string Area { get; set; }

    public string Link { get; set; }

    public string ApprovalType { get; set; }

    public string VisitType { get; set; }
  }
}
