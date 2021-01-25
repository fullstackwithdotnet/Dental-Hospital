// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.DesignationViewModal
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Core;
using System.ComponentModel.DataAnnotations;

namespace DIMS.ViewModels
{
  public class DesignationViewModal
  {
    [PrimaryKey]
    public int DesigId { get; set; }

    [Display(Name = "Designation Name")]
    public string DesigName { get; set; }
  }
}
