// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.ReferralStatusViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DIMS.ViewModels
{
  public class ReferralStatusViewModel : EntityBase
  {
    public long ReferredId { get; set; }

    public int PatientId { get; set; }

    public int FromdeptId { get; set; }

    public DateTime? RevisitDate { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [DisplayName("Date")]
    public DateTime? FromDate { get; set; }

    [DisplayName("Reason to refer")]
    public string ReferredReason { get; set; }

    public int ToDeptId { get; set; }

    public string Priority { get; set; }

    [DisplayName("Room No")]
    public string RoomNo { get; set; }

    [Display(Name = "Status")]
    public string TreatmentStatus { get; set; }

    [Display(Name = "Treatment Done")]
    public DateTime? TreatmentDate { get; set; }

    public int ReferredTreatmentId { get; set; }

    [Display(Name = "Visit Type")]
    public string VisitType { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string ModifiedSystem { get; set; }

    public bool chkToDeptId { get; set; }

    [DisplayName("From Dept")]
    public string FromDeptName { get; set; }

    [DisplayName("To Dept")]
    public string ToDeptName { get; set; }

    [DisplayName("From Dept")]
    public string FromDeptCode { get; set; }

    [DisplayName("To Dept")]
    public string ToDeptCode { get; set; }

    [DisplayName("Date")]
    public string FromDateDisplay { get; set; }

    [Display(Name = "Treatment Date")]
    public string TreatmentDateDisplay { get; set; }

    public PatientInformationViewModel patientInformationViewModel { get; set; }

    public ReferredToOthersViewModel referredToOthersViewModel { get; set; }

    public IEnumerable<ReferralStatusViewModel> ViewdepartmentReferredStatus { get; set; }

    [Display(Name = "OP No.")]
    public long OpNo { get; set; }

    [Display(Name = "Patient")]
    public string PatientName { get; set; }

    [Display(Name = "Address.")]
    [DataType(DataType.MultilineText)]
    public string Address { get; set; }

    [Display(Name = "Phone")]
    public string Phone { get; set; }

    [DisplayName("Age/Gender")]
    public int Age { get; set; }

    public int GenderId { get; set; }

    public int RevistId { get; set; }

    public string Link { get; set; }

    public int CaserecordId { get; set; }

    public string TreatmentCode { get; set; }

    public string ApprovalStatus { get; set; }

    public bool IsApproved { get; set; }

    public string Approvalvalue { get; set; }
  }
}
