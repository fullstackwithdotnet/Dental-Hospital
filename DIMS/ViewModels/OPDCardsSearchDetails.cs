// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.OPDCardsSearchDetails
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;

namespace DIMS.ViewModels
{
  public class OPDCardsSearchDetails : EntityBase
  {
    public int PatientId { get; set; }

    public string RegDate { get; set; }

    public long OpNo { get; set; }

    public string PatientName { get; set; }

    public int Age { get; set; }

    public string GenderName { get; set; }

    public int TitleId { get; set; }

    public string Dob { get; set; }

    public int GenderId { get; set; }

    public string Phone { get; set; }

    public string Area { get; set; }

    public string Address { get; set; }

    public string CategoryName { get; set; }

    public bool ChkSelect { get; set; }

    public int RowNo { get; set; }
  }
}
