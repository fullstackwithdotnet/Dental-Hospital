// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.DCardGetDetails
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;

namespace DIMS.ViewModels
{
  public class DCardGetDetails : EntityBase
  {
    public int PatientId { get; set; }

    public int TitleId { get; set; }

    public int GenderId { get; set; }

    public int OMRId { get; set; }

    public int CaseSheetId { get; set; }
  }
}
