// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.TemplateDetailsViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using System.ComponentModel.DataAnnotations;

namespace DIMS.ViewModels
{
  public class TemplateDetailsViewModel : EntityBase
  {
    public int RadioTempId { get; set; }

    public int RadioTempDetId { get; set; }

    [Display(Name = "Service Items")]
    public string ServiceItems { get; set; }

    [Display(Name = "Display Order")]
    public string DisplayOrder { get; set; }

    [Display(Name = "Header")]
    public string IsHeader { get; set; }

    [Display(Name = "Male Normal Range")]
    public string MaleNormalRange { get; set; }

    [Display(Name = "Female Normal Range")]
    public string FemaleNormalRange { get; set; }

    [Display(Name = "Unit")]
    public string Unit { get; set; }

    public bool IsChecked { get; set; }
  }
}
