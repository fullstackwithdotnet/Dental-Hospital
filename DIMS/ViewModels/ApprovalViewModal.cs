// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.ApprovalViewModal
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DIMS.ViewModels
{
  public class ApprovalViewModal : EntityBase
  {
    public IList<ApprovalViewModal> ApprovalList;

    [PrimaryKey]
    public long ApprovalId { get; set; }

    public DateTime ApprovalDate { get; set; }

    public int ApprovalTypeId { get; set; }

    public int Revision { get; set; }

    public long PatientId { get; set; }

    public int? DoctorId { get; set; }

    public int DeptId { get; set; }

    public int CaserecordId { get; set; }

    public int ReferredTreatmentId { get; set; }

    public string Reason { get; set; }

    [Display(Name = "Password")]
    public string ApprovalPassword { get; set; }

    [Display(Name = "Doctor")]
    public string ApprovalDoctorName { get; set; }

    public string MessageBox { get; set; }

    [Display(Name = "Date")]
    public string DisplayApprovalDate { get; set; }

    [Display(Name = "Approved By")]
    public string CreatedBy { get; set; }
  }
}
