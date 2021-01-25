// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.OMFSIPViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class OMFSIPViewModel : EntityBase
  {
    private DateTime _PreMedicationTime = DateTime.Now;
    private DateTime _InductionTime = DateTime.Now;
    private DateTime _IntubationTime = DateTime.Now;
    private DateTime _ExtubationTime = DateTime.Now;
    private DateTime _ClosedCircuitTime = DateTime.Now;
    private DateTime _AnaesthesiaSedationReversalTime = DateTime.Now;

    [PrimaryKey]
    public int OMFSIpId { get; set; }

    public string MandatoryDummy { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int PatientId { get; set; }

    public string OMFSIpNo { get; set; }

    public string Anaesthetist { get; set; }

    public DateTime OMFSIpDate { get; set; }

    [Display(Name = "Chief Complaint")]
    public string ChiefComplaintName { get; set; }

    [DataType(DataType.MultilineText)]
    public string ChiefComplaint { get; set; }

    [Display(Name = "8")]
    public bool UR8 { get; set; }

    [Display(Name = "7")]
    public bool UR7 { get; set; }

    [Display(Name = "6")]
    public bool UR6 { get; set; }

    [Display(Name = "5")]
    public bool UR5 { get; set; }

    [Display(Name = "4")]
    public bool UR4 { get; set; }

    [Display(Name = "3")]
    public bool UR3 { get; set; }

    [Display(Name = "2")]
    public bool UR2 { get; set; }

    [Display(Name = "1")]
    public bool UR1 { get; set; }

    [Display(Name = "8")]
    public bool UL8 { get; set; }

    [Display(Name = "7")]
    public bool UL7 { get; set; }

    [Display(Name = "6")]
    public bool UL6 { get; set; }

    [Display(Name = "5")]
    public bool UL5 { get; set; }

    [Display(Name = "4")]
    public bool UL4 { get; set; }

    [Display(Name = "3")]
    public bool UL3 { get; set; }

    [Display(Name = "2")]
    public bool UL2 { get; set; }

    [Display(Name = "1")]
    public bool UL1 { get; set; }

    [Display(Name = "8")]
    public bool LR8 { get; set; }

    [Display(Name = "7")]
    public bool LR7 { get; set; }

    [Display(Name = "6")]
    public bool LR6 { get; set; }

    [Display(Name = "5")]
    public bool LR5 { get; set; }

    [Display(Name = "4")]
    public bool LR4 { get; set; }

    [Display(Name = "3")]
    public bool LR3 { get; set; }

    [Display(Name = "2")]
    public bool LR2 { get; set; }

    [Display(Name = "1")]
    public bool LR1 { get; set; }

    [Display(Name = "8")]
    public bool LL8 { get; set; }

    [Display(Name = "7")]
    public bool LL7 { get; set; }

    [Display(Name = "6")]
    public bool LL6 { get; set; }

    [Display(Name = "5")]
    public bool LL5 { get; set; }

    [Display(Name = "4")]
    public bool LL4 { get; set; }

    [Display(Name = "3")]
    public bool LL3 { get; set; }

    [Display(Name = "2")]
    public bool LL2 { get; set; }

    [Display(Name = "1")]
    public bool LL1 { get; set; }

    public bool Max1 { get; set; }

    public bool Max2 { get; set; }

    public bool Max3 { get; set; }

    public bool Max4 { get; set; }

    public bool Max5 { get; set; }

    public bool Max6 { get; set; }

    public bool Max7 { get; set; }

    public bool Max8 { get; set; }

    public bool Max9 { get; set; }

    public bool Max10 { get; set; }

    public bool Max11 { get; set; }

    public bool Max12 { get; set; }

    public bool Max13 { get; set; }

    public bool Max14 { get; set; }

    public bool Max15 { get; set; }

    public bool Max16 { get; set; }

    public bool Mand1 { get; set; }

    public bool Mand2 { get; set; }

    public bool Mand3 { get; set; }

    public bool Mand4 { get; set; }

    public bool Mand5 { get; set; }

    public bool Mand6 { get; set; }

    public bool Mand7 { get; set; }

    public bool Mand8 { get; set; }

    public bool Mand9 { get; set; }

    public bool Mand10 { get; set; }

    public bool Mand11 { get; set; }

    public bool Mand12 { get; set; }

    public bool Mand13 { get; set; }

    public bool Mand14 { get; set; }

    public bool Mand15 { get; set; }

    public bool Mand16 { get; set; }

    [Display(Name = "Findings")]
    public string Findings { get; set; }

    [Display(Name = "Upper Jaw")]
    public string UpperJaw { get; set; }

    [Display(Name = "Lower Jaw")]
    public string LowerJaw { get; set; }

    [Display(Name = "Upper Others")]
    public string UpperJawOthers { get; set; }

    [Display(Name = "Lower Others")]
    public string LowerJawOthers { get; set; }

    [Display(Name = "Pre operative")]
    public string Preoperative { get; set; }

    [Display(Name = "Models")]
    public string Models { get; set; }

    [Display(Name = "Splints")]
    public string Splints { get; set; }

    [Display(Name = "Diagnosis")]
    public string Diagnosis { get; set; }

    [Display(Name = "Provisional Diagnosis")]
    public string ProvisionalDiagnosis { get; set; }

    [Display(Name = "Differential Diagnosis")]
    public string DifferentialDiagnosis { get; set; }

    [Display(Name = "Treatment Plan")]
    public string TreatmentPlan { get; set; }

    [Display(Name = "Treatment Done")]
    public string TreatmentDone { get; set; }

    [Display(Name = "Pre Operative Notes")]
    public string PreOperativeNotes { get; set; }

    [Display(Name = "Pre Medication")]
    public string PreMedication { get; set; }

    [Display(Name = "Operation Notes")]
    public string OperationNotes { get; set; }

    [DataType(DataType.Time)]
    [Display(Name = "Pre Medication Time")]
    public DateTime PreMedicationTime
    {
      get
      {
        return this._PreMedicationTime;
      }
      set
      {
        this._PreMedicationTime = value;
      }
    }

    [Display(Name = "Induction")]
    public string Induction { get; set; }

    [DataType(DataType.Time)]
    [Display(Name = "Induction Time")]
    public DateTime InductionTime
    {
      get
      {
        return this._InductionTime;
      }
      set
      {
        this._InductionTime = value;
      }
    }

    [Display(Name = "Intubation")]
    public string Intubation { get; set; }

    [DataType(DataType.Time)]
    [Display(Name = "Intubation Time")]
    public DateTime IntubationTime
    {
      get
      {
        return this._IntubationTime;
      }
      set
      {
        this._IntubationTime = value;
      }
    }

    [Display(Name = "Number")]
    public string Number { get; set; }

    [Display(Name = "Drugs")]
    public string Drugs { get; set; }

    [Display(Name = "Agents")]
    public string Agents { get; set; }

    [Display(Name = "Type")]
    public string Type { get; set; }

    [Display(Name = "Route")]
    public string Route { get; set; }

    [Display(Name = "Extubation")]
    public string Extubation { get; set; }

    [DataType(DataType.Time)]
    [Display(Name = "Extubation Time")]
    public DateTime ExtubationTime
    {
      get
      {
        return this._ExtubationTime;
      }
      set
      {
        this._ExtubationTime = value;
      }
    }

    [Display(Name = "Circuit")]
    public string Circuit { get; set; }

    [Display(Name = "Closed Circuit")]
    public string ClosedCircuit { get; set; }

    [DataType(DataType.Time)]
    [Display(Name = "Closed Circuit Time")]
    public DateTime ClosedCircuitTime
    {
      get
      {
        return this._ClosedCircuitTime;
      }
      set
      {
        this._ClosedCircuitTime = value;
      }
    }

    [Display(Name = "Anaesthesia Sedation Reversal")]
    public string AnaesthesiaSedationReversal { get; set; }

    [DataType(DataType.Time)]
    [Display(Name = "Anaesthesia Sedation Reversal Time")]
    public DateTime AnaesthesiaSedationReversalTime
    {
      get
      {
        return this._AnaesthesiaSedationReversalTime;
      }
      set
      {
        this._AnaesthesiaSedationReversalTime = value;
      }
    }

    [Display(Name = "Maintenance")]
    public string Maintenance { get; set; }

    [Display(Name = "Relaxant")]
    public string Relaxant { get; set; }

    [Display(Name = "Position")]
    public string Position { get; set; }

    [Display(Name = "Neostigmine")]
    public string Neostigmine { get; set; }

    [Display(Name = "Glycopyrolate")]
    public string Glycopyrolate { get; set; }

    [Display(Name = "Post Operative Complication")]
    public string PostOperativeComplication { get; set; }

    [Display(Name = "Total Fluid Input")]
    public string TotalFluid { get; set; }

    [Display(Name = "Urine Output")]
    public string UrineOutput { get; set; }

    [Display(Name = "Duration Of Surgery")]
    public string DurationOfSurgery { get; set; }

    [Display(Name = "Duration Of Anesthesia")]
    public string DurationOfAnesthesia { get; set; }

    [Display(Name = "Blood Loss")]
    public string BloodLoss { get; set; }

    public int? BloodLossId { get; set; }

    [Display(Name = "Replaced")]
    public string Replaced { get; set; }

    [Display(Name = "Bottles")]
    public string Bottles { get; set; }

    [Display(Name = "Post Operative Notes")]
    public string PostOperativeNotes { get; set; }

    [Display(Name = "Brief Summary Of Case")]
    public string BriefSummaryOfCase { get; set; }

    [Display(Name = "Discharge Diagnosis")]
    public string DischargeDiagnosis { get; set; }

    [Display(Name = "Discharge Treatment done")]
    public string DischargeTreatmentdone { get; set; }

    [Display(Name = "Investigation Performed")]
    public string InvestigationPerformed { get; set; }

    [Display(Name = "Condition Of Patient At Discharge")]
    public string ConditionOfPatientAtDischarge { get; set; }

    [Display(Name = "Discharge Medication")]
    public string DischargeMedication { get; set; }

    [Display(Name = "Instruction To Be Followed")]
    public string InstructionToBeFollowed { get; set; }

    [Display(Name = "Nurse Notes")]
    public string NurseNotes { get; set; }

    public int ScheduleId { get; set; }

    public bool IsApproved { get; set; }

    public PatientInformationViewModel patientInformationViewModel { get; set; }

    public FollowupViewModal followupViewModal { get; set; }

    public IEnumerable<FollowupViewModal> followupList { get; set; }

    public IEnumerable<StudentAllotmentViewModel> studentProcedureNotesViewModel { get; set; }

    public StudentAllotmentViewModel studentAllotmentViewModel { get; set; }

    public BillingQueueServiceViewModel billingQueueViewModal { get; set; }

    public IEnumerable<BillingQueueServiceViewModel> BillingQueueDetails { get; set; }

    public IEnumerable<MASChiefComplaint> ChiefComplaintlist { get; set; }

    public IEnumerable<OMFSIPCasesheetProperties> Proplist { get; set; }

    public List<ReferralStatusViewModel> CreatedepartmentReferredStatus { get; set; }

    public List<ReferralStatusViewModel> ViewdepartmentReferredStatus { get; set; }

    public OMFSIPScheduleViewModel OmfsipscheduleViewModel { get; set; }

    public IEnumerable<BillingViewModal> paidInvestigationList { get; set; }

    public IEnumerable<MASCode> OperationTypeList { get; set; }

    public IEnumerable<MASCode> BloodLossList { get; set; }

    public ReferredToOthersViewModel referredtoOthersViewModel { get; set; }

    public long TreatmentReferredId { get; set; }

    private bool GetCheckBoxValue(string dependentField)
    {
      return !string.IsNullOrEmpty(dependentField);
    }

    public int ReferredOthersId { get; set; }

    [Display(Name = "Date")]
    public string DisplayFromDate { get; set; }

    [Display(Name = "To Time")]
    public string ToTimeDisplay { get; set; }

    public IEnumerable<MASDoctor> DoctorList { get; set; }

    public long OpNo { get; set; }

    public string PatientName { get; set; }

    public string AgeGender { get; set; }

    public int Age { get; set; }

    public int GenderId { get; set; }

    public string Area { get; set; }

    [Display(Name = "Phone")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; }

    [Display(Name = "Date")]
    public string fromdateDisplay { get; set; }

    [Display(Name = "Time")]
    public string fromtimeDisplay { get; set; }

    [Display(Name = "Duration")]
    public string duration { get; set; }

    [Display(Name = "Operation")]
    public string operation { get; set; }

    [Display(Name = "Operation Type")]
    public string operationtype { get; set; }

    public IEnumerable<OMFSIPScheduleViewModel> ScheduleDetailsList { get; set; }

    public IEnumerable<FileUploadViewModel> FileUploadlist { get; set; }
  }
}
