// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.OPDFollowupSearchDetails
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;

namespace DIMS.ViewModels
{
  public class OPDFollowupSearchDetails : EntityBase
  {
    public int FollowupId { get; set; }

    public long PatientId { get; set; }

    public string FollowupDate { get; set; }

    public string FollowupTime { get; set; }

    public long OPNo { get; set; }

    public string PatientName { get; set; }

    public int Age { get; set; }

    public string Gender { get; set; }

    public string Area { get; set; }

    public string DeptCode { get; set; }

    public string FollowupReason { get; set; }

    public string Revisit { get; set; }

    public string Reschedule { get; set; }

    public string Phone { get; set; }
  }
}
