// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.MedicalAlertViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using System.Collections.Generic;
using System.ComponentModel;

namespace DIMS.ViewModels
{
  public class MedicalAlertViewModel : EntityBase
  {
    public IEnumerable<MedicalAlertViewModel> MedicalAlertList { get; set; }

    [DisplayName("MEDICAL ALERT :")]
    public string PROPVALUES { get; set; }

    [DisplayName("MLC ALERT :")]
    public string MLCValue { get; set; }
  }
}
