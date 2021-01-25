// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.TemplateViewModal
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class TemplateViewModal : EntityBase
  {
    public int RadioTempId { get; set; }

    [Display(Name = "Service")]
    public int ServiceId { get; set; }

    public string ServiceName { get; set; }

    [Display(Name = "Group")]
    public int GroupId { get; set; }

    public string GroupName { get; set; }

    public bool IsChecked { get; set; }

    [Display(Name = "Department")]
    public string Dept { get; set; }

    [Display(Name = "Department")]
    public string DeptName { get; set; }

    public int DeptId { get; set; }

    [Display(Name = "Radiology")]
    public bool rdnRadiology { get; set; }

    [Display(Name = "Oral Pathology")]
    public bool rdnOralPathology { get; set; }

    public bool rdnSelect { get; set; }

    public IEnumerable<MASBillingServices> ServicesList { get; set; }

    public IEnumerable<MASGroup> GroupList { get; set; }

    public TemplateDetailsViewModel radiologyViewModal { get; set; }

    public IEnumerable<TemplateDetailsViewModel> ServiceListDetails { get; set; }
  }
}
