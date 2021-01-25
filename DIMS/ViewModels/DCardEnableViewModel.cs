// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.DCardEnableViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using System.ComponentModel.DataAnnotations;

namespace DIMS.ViewModels
{
  public class DCardEnableViewModel : EntityBase
  {
    [Display(Name = "Search Form")]
    public bool IsSearchEnable { get; set; }

    public bool IsReportEnable { get; set; }

    public int DummyEnableId { get; set; }

    [Display(Name = "Report Form")]
    public string ReportForm { get; set; }

    public string SearchForm { get; set; }
  }
}
