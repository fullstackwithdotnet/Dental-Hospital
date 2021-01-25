// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.ORPATHRequisitionViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class ORPATHRequisitionViewModel : EntityBase
  {
    private DateTime _SampleCollectionTime = DateTime.Now;
    private DateTime _CulturingDateTime = DateTime.Now;
    private DateTime _BiopsyCollectionDate = DateTime.Now;
    private DateTime _BiopsyCollectionTime = DateTime.Now;
    private DateTime _SampleCollectionDate = DateTime.Now;

    [PrimaryKey]
    public int RequisitionId { get; set; }

    public int PatientId { get; set; }

    public string RequisitionNo { get; set; }

    public DateTime RequisitionDate { get; set; }

    public string MandatoryDummy { get; set; }

    [Display(Name = "Date")]
    public string RequisitionDateDisplay { get; set; }

    public int DeptId { get; set; }

    public long AllotId { get; set; }

    [Display(Name = "Department")]
    public string DeptName { get; set; }

    [Display(Name = "Chief complaint")]
    public string MChiefComplaint { get; set; }

    [Display(Name = "Chief Complaint")]
    public string BChiefComplaint { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    [Display(Name = "Collected Date")]
    public DateTime? MSampleCollectionDate { get; set; }

    [DataType(DataType.Time)]
    [Display(Name = "Time")]
    public DateTime? MSampleCollectionTime { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    [Display(Name = "Collected Date")]
    public DateTime? BBiopsyCollectionDate { get; set; }

    [DataType(DataType.Time)]
    [Display(Name = "Time")]
    public DateTime? BBiopsyCollectionTime { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    [Display(Name = "Culturing")]
    public DateTime? MCulturing { get; set; }

    [Display(Name = "Type of lesion")]
    public int? MTypeOfLesionId { get; set; }

    public string MLesion { get; set; }

    [Display(Name = "Lesion")]
    public string MTypeOfLesion { get; set; }

    [Display(Name = "Onset duration")]
    public string MOnsetDuration { get; set; }

    [Display(Name = "Progress")]
    public string MProgress { get; set; }

    [Display(Name = "Associated symptoms")]
    public string MAssociatedSymptoms { get; set; }

    [Display(Name = "Site and extent")]
    public string MSiteAndExtent { get; set; }

    [Display(Name = "Size and shape")]
    public string MSizeAndShape { get; set; }

    [Display(Name = "Colour")]
    public string MColour { get; set; }

    [Display(Name = "Margins")]
    public string MMargins { get; set; }

    [Display(Name = "Tenderness")]
    public int? MTendernessId { get; set; }

    [Display(Name = "Consistency")]
    public int? MConsistencyId { get; set; }

    [Display(Name = "Discharge")]
    public string MDischarge { get; set; }

    [Display(Name = "Lymph node status")]
    public int? MLymphNodeStatusId { get; set; }

    public string MLymphNodestatusName { get; set; }

    [Display(Name = "Lymph location level")]
    public string MLymphLocationlevel { get; set; }

    [Display(Name = "Number and size")]
    public string MNumberAndSize { get; set; }

    [Display(Name = "Lymph tenderness")]
    public int? MLymphTendernessId { get; set; }

    [Display(Name = "Lymph consistency")]
    public int? MLymphConsistencyId { get; set; }

    [Display(Name = "Fixity")]
    public string MFixity { get; set; }

    [Display(Name = "TNM staging")]
    public string MTnmStaging { get; set; }

    [Display(Name = "Extra oral findings")]
    public string MExtraOralFindings { get; set; }

    [Display(Name = "Radiographic findings")]
    public string MRadiographicFindings { get; set; }

    [Display(Name = "Investigations")]
    public string MInvestigations { get; set; }

    [Display(Name = "Provisional diagnosis")]
    public string MProvisionalDiagnosis { get; set; }

    [Display(Name = "Sample Collection Time")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
    [DataType(DataType.Time)]
    public DateTime SampleCollectionTime
    {
      get
      {
        return this._SampleCollectionTime;
      }
      set
      {
        this._SampleCollectionTime = value;
      }
    }

    [Display(Name = "Type of sample")]
    public int? MTypeOfSampleId { get; set; }

    [Display(Name = "Others")]
    public string MSampleOthers { get; set; }

    [Display(Name = "Operative findings")]
    public string MOperativeFindings { get; set; }

    [Display(Name = "Sample No")]
    public string MSampleNo { get; set; }

    [Display(Name = "Macroscopy")]
    public string MMacroscopy { get; set; }

    [Display(Name = "Culturing date and time")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    [DataType(DataType.DateTime)]
    public DateTime CulturingDateTime
    {
      get
      {
        return this._CulturingDateTime;
      }
      set
      {
        this._CulturingDateTime = value;
      }
    }

    [Display(Name = "Culturing type")]
    public string MCulturingType { get; set; }

    [Display(Name = "Biochemical test")]
    public string MBiochemicalTest { get; set; }

    [Display(Name = "Investigation Report")]
    public string MInvestigationReport { get; set; }

    [Display(Name = "Lesion")]
    public int? BTypeOfLesionId { get; set; }

    [Display(Name = "Type of lesion")]
    public string BTypeOfLesion { get; set; }

    [Display(Name = "Onset")]
    public string BOnsetDuration { get; set; }

    [Display(Name = "Progress")]
    public string BProgress { get; set; }

    [Display(Name = "Family history")]
    public string BFamilyHistory { get; set; }

    [Display(Name = "Frequency")]
    public string Frequency { get; set; }

    [Display(Name = "Duration")]
    public string Duration { get; set; }

    [Display(Name = "Qty")]
    public string Quantity { get; set; }

    [Display(Name = "Tobacco")]
    public string BTobaccoQty { get; set; }

    [Display(Name = "Duration")]
    public string BTobaccoDur { get; set; }

    [Display(Name = "Frequency")]
    public string BTobaccoFreq { get; set; }

    [Display(Name = "Smoke")]
    public string BSmokingQty { get; set; }

    [Display(Name = "Duration")]
    public string BSmokingDur { get; set; }

    [Display(Name = "Frequency")]
    public string BSmokingFreq { get; set; }

    [Display(Name = "Alcohol")]
    public string BAlcoholQty { get; set; }

    [Display(Name = "Duration")]
    public string BAlcoholDur { get; set; }

    [Display(Name = "Frequency")]
    public string BAlcoholFreq { get; set; }

    public string BOtherQty { get; set; }

    [Display(Name = "Duration")]
    public string BOtherDur { get; set; }

    [Display(Name = "Frequency")]
    public string BOtherFreq { get; set; }

    [Display(Name = "Other habits")]
    public string BOtherHabits { get; set; }

    [Display(Name = "Associated symptoms")]
    public string BAssociatedSymptoms { get; set; }

    [Display(Name = "Site and extent")]
    public string BSiteAndExtent { get; set; }

    [Display(Name = "Size and shape")]
    public string BSizeAndShape { get; set; }

    [Display(Name = "Colour")]
    public string BColour { get; set; }

    [Display(Name = "Margins")]
    public string BMargins { get; set; }

    [Display(Name = "Tenderness")]
    public int? BTendernessId { get; set; }

    [Display(Name = "Consistency")]
    public int? BConsistencyId { get; set; }

    [Display(Name = "Discharge")]
    public string BDischarge { get; set; }

    [Display(Name = "Texture")]
    public string BTexture { get; set; }

    [Display(Name = "Induration")]
    public string Binduration { get; set; }

    [Display(Name = "Hard tissue examination")]
    public string BHardTissueExamination { get; set; }

    [Display(Name = "Lymph node status")]
    public int? BLymphNodeStatusId { get; set; }

    [Display(Name = "Lymph location level")]
    public string BLymphLocationlevel { get; set; }

    [Display(Name = "Lymph tenderness")]
    public int? BLymphTendernessId { get; set; }

    [Display(Name = "Lymph consistency")]
    public int? BLymphConsistencyId { get; set; }

    [Display(Name = "Extra oral findings")]
    public string BExtraOralFindings { get; set; }

    [Display(Name = "Radiographic findings")]
    public string BRadiographicFindings { get; set; }

    [Display(Name = "Investigations")]
    public string BInvestigations { get; set; }

    [Display(Name = "Provisional diagnosis")]
    public string BProvisionalDiagnosis { get; set; }

    [Display(Name = "Biopsy collected date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime BiopsyCollectionDate
    {
      get
      {
        return this._BiopsyCollectionDate;
      }
      set
      {
        this._BiopsyCollectionDate = value;
      }
    }

    [Display(Name = "Biopsy collection time")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
    [DataType(DataType.Time)]
    public DateTime BiopsyCollectionTime
    {
      get
      {
        return this._BiopsyCollectionTime;
      }
      set
      {
        this._BiopsyCollectionTime = value;
      }
    }

    [Display(Name = "Type of sample")]
    public int? BTypeOfSampleId { get; set; }

    [Display(Name = "Site of biopsy")]
    public string BSiteOfBiopsy { get; set; }

    [Display(Name = "Hard tissue")]
    public int? BHardTissueId { get; set; }

    [Display(Name = "Previous Biopsy/Cytology Details")]
    public string BPreviousBiopsyCytologyDetails { get; set; }

    [Display(Name = "Operative findings")]
    public string BOperativeFindings { get; set; }

    [Display(Name = "Biopsy/Cytology No")]
    public string BBiopsyNo { get; set; }

    [Display(Name = "Macroscopy")]
    public string BMacroscopy { get; set; }

    [Display(Name = "Biopsy/Cytology Report")]
    public string BBiopsyReport { get; set; }

    [Display(Name = "Investigation Report")]
    public string BInvestigationReport { get; set; }

    public PatientInformationViewModel patientInformationViewModel { get; set; }

    public BillingQueueServiceViewModel BillingQueueViewModel { get; set; }

    public IEnumerable<BillingQueueServiceViewModel> BillingQueueDetails { get; set; }

    public IEnumerable<BillingViewModal> paidInvestigationList { get; set; }

    public IEnumerable<ORPATHCasesheetProperties> Proplist { get; set; }

    public IEnumerable<MASCode> Lesionlist { get; set; }

    public IEnumerable<MASCode> Tendernesslist { get; set; }

    public IEnumerable<MASCode> Consistencylist { get; set; }

    public IEnumerable<MASCode> TypeOfSamplelist { get; set; }

    public IEnumerable<MASCode> HardTissuelist { get; set; }

    public IEnumerable<MASCode> TypeOfBiopsylist { get; set; }

    public IEnumerable<MASCode> LymphNodeStatuslist { get; set; }

    public IEnumerable<ORPATHRequisitionViewModel> ORPATHDetails { get; set; }

    public StudentAllotmentViewModel studentAllotmentViewModel { get; set; }

    [Display(Name = "Sample Collection Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime SampleCollectionDate
    {
      get
      {
        return this._SampleCollectionDate;
      }
      set
      {
        this._SampleCollectionDate = value;
      }
    }

    [Display(Name = "Micro Biology")]
    public string MicroBiologyChiefComplaint { get; set; }

    [Display(Name = "Biopsy/Cytology")]
    public string BiopsyChiefComplaint { get; set; }

    public string MTypeOfLesionDn { get; set; }

    public string MTenderness { get; set; }

    public string MConsistency { get; set; }

    public string MLymphNodeStatus { get; set; }

    public string MLymphTenderness { get; set; }

    public string MLymphConsistency { get; set; }

    public string MTypeOfSample { get; set; }

    public string BTypeOfLesionDn { get; set; }

    public string BTenderness { get; set; }

    public string BConsistency { get; set; }

    public string BLymphNodeStatus { get; set; }

    public string BLymphTenderness { get; set; }

    public string BLymphConsistency { get; set; }

    public string BTypeOfSample { get; set; }

    public string BHardTissue { get; set; }

    public long TreatmentReferredId { get; set; }

    public bool Approval1 { get; set; }

    public bool Approval2 { get; set; }

    public bool Approval3 { get; set; }

    public bool SendForApproval1 { get; set; }

    public bool SendForApproval2 { get; set; }

    public bool SendForApproval3 { get; set; }

    public string DisplayApproval1 { get; set; }

    public string DisplayApproval2 { get; set; }

    public string DisplayApproval3 { get; set; }

    public bool ReadOnlyApproval1 { get; set; }

    public bool ReadOnlyApproval2 { get; set; }

    public bool ReadOnlyApproval3 { get; set; }

    public bool ReadOnlyApproval4 { get; set; }

    public int ApprovalType { get; set; }

    public string ReferralCount { get; set; }

    public string InvestigationCount { get; set; }

    public string TreatmentCount { get; set; }

    public string ApprovalStage { get; set; }

    public bool EditCaseSheetAccess { get; set; }

    public List<ReferralStatusViewModel> ApprovedepartmentReferral { get; set; }

    public IEnumerable<ORPATHRequisitionViewModel> Treatmentlist { get; set; }

    public int ReferredOthersId { get; set; }

    public BillingQueueServiceViewModel billingQueueViewModal { get; set; }

    public BillingQueueServiceViewModel billingLabRadQueueViewModal { get; set; }

    public FollowupViewModal followupViewModal { get; set; }

    public IEnumerable<FollowupViewModal> followupList { get; set; }

    public IEnumerable<StudentAllotmentViewModel> studentProcedureNotesViewModel { get; set; }

    public IEnumerable<BillingViewModal> deptTreatmentList { get; set; }

    public IEnumerable<BillingViewModal> paidLabRadInvestigationList { get; set; }

    public List<ReferralStatusViewModel> CreatedepartmentReferredStatus { get; set; }

    public List<ReferralStatusViewModel> ViewdepartmentReferredStatus { get; set; }

    public IEnumerable<BillingQueueServiceViewModel> BillingLabRadQueueDetails { get; set; }

    public IEnumerable<FileUploadViewModel> FileUploadlist { get; set; }

    public ReferredToOthersViewModel referredtoOthersViewModel { get; set; }

    public RadioRegistrationViewModel radioRegistrationviewmodel { get; set; }

    public LaboratoryRegistrationViewModel laboratoryRegistrationviewmodel { get; set; }

    public PrescriptionsViewModel PrescriptionsList { get; set; }

    public IEnumerable<PrescriptionsViewModel> PrescriptionsDetails { get; set; }

    public IEnumerable<PrescriptionsViewModel> PreviousPrescriptionList { get; set; }

    public MedicalAlertViewModel medicalalertviewmodel { get; set; }

    public ApprovalViewModal approvalViewModal { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public long OpNo { get; set; }

    public string PatientName { get; set; }

    public string Phone { get; set; }

    public string AgeGender { get; set; }

    public string Area { get; set; }

    public int Age { get; set; }

    public int GenderId { get; set; }
  }
}
