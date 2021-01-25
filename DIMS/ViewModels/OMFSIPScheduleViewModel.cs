// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.OMFSIPScheduleViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class OMFSIPScheduleViewModel : EntityBase
  {
    private DateTime _OperationScheduleFromDate = DateTime.Now;
    private DateTime _OperationScheduleToDate = DateTime.Now;
    private DateTime _OperationScheduleFromTime = DateTime.Now;
    private DateTime _OperationScheduleToTime = DateTime.Now;

    [Key]
    public int ScheduleId { get; set; }

    public int PatientId { get; set; }

    [Display(Name = "Surgeon")]
    public int SurgeonId { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    [Display(Name = " Date")]
    public DateTime OperationScheduleFromDate
    {
      get
      {
        return this._OperationScheduleFromDate;
      }
      set
      {
        this._OperationScheduleFromDate = value;
      }
    }

    public long ReferredId { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    [Display(Name = "To Date")]
    public DateTime OperationScheduleToDate
    {
      get
      {
        return this._OperationScheduleToDate;
      }
      set
      {
        this._OperationScheduleToDate = value;
      }
    }

    [DataType(DataType.Time)]
    [Display(Name = " Time")]
    public DateTime OperationScheduleFromTime
    {
      get
      {
        return this._OperationScheduleFromTime;
      }
      set
      {
        this._OperationScheduleFromTime = value;
      }
    }

    [DataType(DataType.Time)]
    [Display(Name = "To Time")]
    public DateTime OperationScheduleToTime
    {
      get
      {
        return this._OperationScheduleToTime;
      }
      set
      {
        this._OperationScheduleToTime = value;
      }
    }

    public string FromTime { get; set; }

    [Display(Name = "Patient Name")]
    public string PatientName { get; set; }

    [Display(Name = "Age/Gender")]
    public string AgeGender { get; set; }

    public int GenderId { get; set; }

    public string GenderName { get; set; }

    [Display(Name = "Age")]
    public string Age { get; set; }

    [Display(Name = "OP No")]
    public long OPNo { get; set; }

    [Display(Name = "Date")]
    public string FromDate { get; set; }

    [Display(Name = "Duration")]
    public string Duration { get; set; }

    [Display(Name = "Operation")]
    public string Operation { get; set; }

    [Display(Name = "Type")]
    public int TypeId { get; set; }

    [Display(Name = "Assistant Surgeon")]
    public string AssistantSurgeon { get; set; }

    [Display(Name = "Anaesthetist")]
    public string Anaesthetist { get; set; }

    [Display(Name = "Assisting Nurse")]
    public string AssistingNurse { get; set; }

    public int DoctorId { get; set; }

    public string DoctorName { get; set; }

    public long TreatmentReferredId { get; set; }

    public string Status { get; set; }

    public IEnumerable<MASCode> OperationTypeList { get; set; }

    public IEnumerable<OMFSIPCasesheetProperties> Proplist { get; set; }

    public IEnumerable<MASDoctor> DoctorList { get; set; }

    public IEnumerable<int> FromSelectSurgeon { get; set; }

    public IEnumerable<int> ToSelectSurgeon { get; set; }

    public string SelectedSurgeon { get; set; }

    public IEnumerable<MASDoctor> FromSurgeonList { get; set; }

    public IEnumerable<OMFSIPScheduleViewModel> ToSurgeonList { get; set; }

    public IEnumerable<int> FromSelectAnthetist { get; set; }

    public IEnumerable<int> ToSelectAnthetist { get; set; }

    public string SelectedAnthetist { get; set; }

    public IEnumerable<MASDoctor> FromAnthetistList { get; set; }

    public IEnumerable<OMFSIPScheduleViewModel> ToAnthetistList { get; set; }

    public PatientInformationViewModel patientInformationViewModel { get; set; }

    public FollowupViewModal followupViewModal { get; set; }

    public IEnumerable<FollowupViewModal> followupList { get; set; }

    public IEnumerable<StudentAllotmentViewModel> studentProcedureNotesViewModel { get; set; }

    public StudentAllotmentViewModel studentAllotmentViewModel { get; set; }

    public ScheduleSearchDetails schedulesearchdetails { get; set; }
  }
}
