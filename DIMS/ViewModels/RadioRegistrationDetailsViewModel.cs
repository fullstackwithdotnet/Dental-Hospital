// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.RadioRegistrationDetailsViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using Repository.Core;
using System.ComponentModel.DataAnnotations;

namespace DIMS.ViewModels
{
  public class RadioRegistrationDetailsViewModel : EntityBase
  {
    [PrimaryKey]
    public int LabDetId { get; set; }

    public int LabId { get; set; }

    public int ServiceId { get; set; }

    public string ToothNo { get; set; }

    public int RadioTempDetId { get; set; }

    [Display(Name = "Test Items ")]
    public string ServiceItems { get; set; }

    public int GroupId { get; set; }

    [Display(Name = "Group  ")]
    public string GroupName { get; set; }

    public int RadioTempId { get; set; }

    public string Impression { get; set; }

    public string Remarks { get; set; }

    public string Result { get; set; }
  }
}
