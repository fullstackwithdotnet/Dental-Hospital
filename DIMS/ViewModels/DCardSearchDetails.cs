// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.DCardSearchDetails
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;

namespace DIMS.ViewModels
{
  public class DCardSearchDetails : EntityBase
  {
    public int CaseSheetId { get; set; }

    public long AllotId { get; set; }

    public string CaseSheetDate { get; set; }

    public string CaseSheetNo { get; set; }

    public long OpNo { get; set; }

    public string PatientName { get; set; }

    public int? Age { get; set; }

    public string GenderName { get; set; }

    public int? GenderId { get; set; }

    public string Area { get; set; }

    public string Link { get; set; }

    public string RowNo { get; set; }

    public string CategoryName { get; set; }

    public bool ChkSelect { get; set; }

    public long ReferralId { get; set; }

    public int? TitleId { get; set; }

    public int OMRId { get; set; }

    public int PatientId { get; set; }
  }
}
