// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.RadioRegistrationSearchDetails
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;

namespace DIMS.ViewModels
{
  public class RadioRegistrationSearchDetails : EntityBase
  {
    public int? BillId { get; set; }

    public string BillNo { get; set; }

    public string BillDateTimeDisplay { get; set; }

    public int PatientId { get; set; }

    public string PatientName { get; set; }

    public long OpNo { get; set; }

    public string DeptCode { get; set; }

    public string Link { get; set; }

    public string LabNo { get; set; }

    public string CreatedDateDisplay { get; set; }
  }
}
