// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.OrthoStaticAnalysisViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DIMS.ViewModels
{
  public class OrthoStaticAnalysisViewModel : EntityBase
  {
    public IEnumerable<OrthoStaticAnalysisViewModel> AnalysisList = (IEnumerable<OrthoStaticAnalysisViewModel>) new List<OrthoStaticAnalysisViewModel>();

    public int OrthoId { get; set; }

    public int AnalysisId { get; set; }

    [Display(Name = "Analysis Name")]
    public string AnalysisDisplayName { get; set; }

    public string AnalysisName { get; set; }

    public string Visibility { get; set; }
  }
}
