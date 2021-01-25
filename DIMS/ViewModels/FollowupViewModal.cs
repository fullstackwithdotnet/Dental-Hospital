// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.FollowupViewModal
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DIMS.ViewModels
{
  [Table("FollowUp")]
  public class FollowupViewModal : EntityBase
  {
    private DateTime _FollowupDate = DateTime.Now;
    private DateTime _FollowupTime = DateTime.Now;

    public int FollowupId { get; set; }

    [Display(Name = "Patient Name")]
    public string PatientName { get; set; }

    public int PatientId { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    [Display(Name = "Date")]
    public DateTime FollowupDate
    {
      get
      {
        return this._FollowupDate;
      }
      set
      {
        this._FollowupDate = value;
      }
    }

    [DataType(DataType.Time)]
    [Display(Name = "Time")]
    public DateTime FollowupTime
    {
      get
      {
        return this._FollowupTime;
      }
      set
      {
        this._FollowupTime = value;
      }
    }

    [Display(Name = "Next Appointment Date")]
    public bool chkProcedure { get; set; }

    public int DeptId { get; set; }

    [Display(Name = "Dept Name")]
    public string DeptName { get; set; }

    [Display(Name = "Treatment")]
    public string FollowupReason { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    [Display(Name = "Date")]
    public string FollowupDateDisplay { get; set; }

    [Display(Name = "Time")]
    public string FollowupTimeDisplay { get; set; }

    [Display(Name = "OP No.")]
    public long OpNo { get; set; }

    public int RevisitId { get; set; }

    public int Age { get; set; }

    public string Gender { get; set; }

    public string Status { get; set; }

    public string IgnoreReason { get; set; }

    public string DeptCode { get; set; }

    public string AgeGender { get; set; }

    public PatientInformationViewModel patientInformationViewModel { get; set; }
  }
}
