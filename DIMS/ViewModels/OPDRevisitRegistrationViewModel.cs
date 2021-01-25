// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.OPDRevisitRegistrationViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DIMS.ViewModels
{
  [Table("OPDRevisitRegistration")]
  public class OPDRevisitRegistrationViewModel : EntityBase
  {
    [PrimaryKey]
    public int RevisitId { get; set; }

    [Display(Name = "Date")]
    public string RevisitDateDisplay { get; set; }

    public int PatientId { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    [Display(Name = "OP No.")]
    public long OPNo { get; set; }

    [Display(Name = "Patient Name")]
    public string PatientName { get; set; }

    [Display(Name = "Age/Gender")]
    public string AgeGender { get; set; }

    public int Age { get; set; }

    public int GenderId { get; set; }

    [Display(Name = "Phone")]
    public string Phone { get; set; }

    public int DeptId { get; set; }

    public int FollowupId { get; set; }

    public string FollowupReason { get; set; }

    public long ReferredId { get; set; }

    public IEnumerable<PatientSearchViewModal> PatientList { get; set; }

    public List<ReferralStatusViewModel> CreatedepartmentReferredStatus { get; set; }

    public IEnumerable<FollowupViewModal> RevisitFollowUpList { get; set; }

    public IEnumerable<FollowupViewModal> FollowUpList { get; set; }

    public IEnumerable<OPDRevisitRegistrationViewModel> RevisitedPatientList { get; set; }

    public string flag { get; set; }

    public bool IsChecked { get; set; }

    public DateTime RevisitDate { get; set; }
  }
}
