// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.ScheduleSearchDetails
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;

namespace DIMS.ViewModels
{
  public class ScheduleSearchDetails : EntityBase
  {
    public int ScheduleId { get; set; }

    public int PatientId { get; set; }

    public string PatientName { get; set; }

    public int GenderId { get; set; }

    public string AgeGender { get; set; }

    public string GenderName { get; set; }

    public string Age { get; set; }

    public long OpNo { get; set; }

    public int TypeId { get; set; }

    public long ReferredId { get; set; }

    public string FromDate { get; set; }

    public string TypeName { get; set; }

    public string Status { get; set; }

    public string Link { get; set; }

    public string UGStudent { get; set; }

    public string PGStudent { get; set; }

    public int StatusId { get; set; }

    public string Operation { get; set; }

    public string StatusName { get; set; }
  }
}
