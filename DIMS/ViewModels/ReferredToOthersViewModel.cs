// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.ReferredToOthersViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using Repository.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DIMS.ViewModels
{
  [Table("ReferredToOthers")]
  public class ReferredToOthersViewModel : EntityBase
  {
    [PrimaryKey]
    public int ReferredOthersId { get; set; }

    public int PatientId { get; set; }

    public DateTime? ReferredOthersDate { get; set; }

    public int DeptId { get; set; }

    public int CaseRecordId { get; set; }

    public int TreatmentId { get; set; }

    [Display(Name = "Doctor")]
    public string DoctorName { get; set; }

    [Display(Name = "Hospital Name")]
    public string HospitalName { get; set; }

    [DataType(DataType.MultilineText)]
    [Display(Name = "Reason")]
    public string ReferredOthersReason { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }
  }
}
