// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.PROSMFPViewModel
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
  public class PROSMFPViewModel : EntityBase
  {
    private DateTime _MfpTreatmentDateUpdate = DateTime.Now;

    [PrimaryKey]
    [HiddenInput(DisplayValue = false)]
    public int ProsthoMFPId { get; set; }

    public string ProsthoMFPNo { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int PatientId { get; set; }

    public string MandatoryDummy { get; set; }

    public DateTime? ProsthoMFPDate { get; set; }

    [Display(Name = "Chief Complaint")]
    public string ChiefComplaintName { get; set; }

    public string ChiefComplaint { get; set; }

    [Display(Name = "History of Present illnes")]
    public string HistoryOfpresentIllness { get; set; }

    [Display(Name = "Past Medical History")]
    public string PastMedicalHistory { get; set; }

    [Display(Name = "Personal History")]
    public string PersonalHistory { get; set; }

    [Display(Name = "General Physical Examination")]
    public string GeneralPhysicalExamination { get; set; }

    [Display(Name = "Facial Profile")]
    public int? FacialProfileId { get; set; }

    [Display(Name = "Shape of Face")]
    public int? ShapeOfFaceId { get; set; }

    [Display(Name = "Vertical face Height")]
    public int? VertFaceHeightId { get; set; }

    [Display(Name = "Tone of Facial Tissues")]
    public int? ToneOfFacialId { get; set; }

    [Display(Name = "TMJ")]
    public int? TMJId { get; set; }

    [Display(Name = "Movements of Mandible")]
    public int? MoveOfMandId { get; set; }

    [Display(Name = "Mouth Length")]
    public int? MouthLengthId { get; set; }

    [Display(Name = "Lip Length")]
    public int? LipLengthSLId { get; set; }

    public int? LipLengthTTId { get; set; }

    public int? LipLengthNTId { get; set; }

    [Display(Name = "Lip support")]
    public int? LipSupportId { get; set; }

    public bool CHKTL8 { get; set; }

    [Display(Name = "8")]
    public string TL8 { get; set; }

    public bool CHKTL7 { get; set; }

    [Display(Name = "7")]
    public string TL7 { get; set; }

    public bool CHKTL6 { get; set; }

    [Display(Name = "6")]
    public string TL6 { get; set; }

    public bool CHKTL5 { get; set; }

    [Display(Name = "5")]
    public string TL5 { get; set; }

    public bool CHKTL4 { get; set; }

    [Display(Name = "4")]
    public string TL4 { get; set; }

    public bool CHKTL3 { get; set; }

    [Display(Name = "3")]
    public string TL3 { get; set; }

    public bool CHKTL2 { get; set; }

    [Display(Name = "2")]
    public string TL2 { get; set; }

    public bool CHKTL1 { get; set; }

    [Display(Name = "1")]
    public string TL1 { get; set; }

    public bool CHKBL8 { get; set; }

    [Display(Name = "8")]
    public string BL8 { get; set; }

    public bool CHKBL7 { get; set; }

    [Display(Name = "7")]
    public string BL7 { get; set; }

    public bool CHKBL6 { get; set; }

    [Display(Name = "6")]
    public string BL6 { get; set; }

    public bool CHKBL5 { get; set; }

    [Display(Name = "5")]
    public string BL5 { get; set; }

    public bool CHKBL4 { get; set; }

    [Display(Name = "4")]
    public string BL4 { get; set; }

    public bool CHKBL3 { get; set; }

    [Display(Name = "3")]
    public string BL3 { get; set; }

    public bool CHKBL2 { get; set; }

    [Display(Name = "2")]
    public string BL2 { get; set; }

    public bool CHKBL1 { get; set; }

    [Display(Name = "1")]
    public string BL1 { get; set; }

    public bool CHKTR1 { get; set; }

    [Display(Name = "1")]
    public string TR1 { get; set; }

    public bool CHKTR2 { get; set; }

    [Display(Name = "2")]
    public string TR2 { get; set; }

    public bool CHKTR3 { get; set; }

    [Display(Name = "3")]
    public string TR3 { get; set; }

    public bool CHKTR4 { get; set; }

    [Display(Name = "4")]
    public string TR4 { get; set; }

    public bool CHKTR5 { get; set; }

    [Display(Name = "5")]
    public string TR5 { get; set; }

    public bool CHKTR6 { get; set; }

    [Display(Name = "6")]
    public string TR6 { get; set; }

    public bool CHKTR7 { get; set; }

    [Display(Name = "7")]
    public string TR7 { get; set; }

    public bool CHKTR8 { get; set; }

    [Display(Name = "8")]
    public string TR8 { get; set; }

    public bool CHKBR1 { get; set; }

    [Display(Name = "1")]
    public string BR1 { get; set; }

    public bool CHKBR2 { get; set; }

    [Display(Name = "2")]
    public string BR2 { get; set; }

    public bool CHKBR3 { get; set; }

    [Display(Name = "3")]
    public string BR3 { get; set; }

    public bool CHKBR4 { get; set; }

    [Display(Name = "4")]
    public string BR4 { get; set; }

    public bool CHKBR5 { get; set; }

    [Display(Name = "5")]
    public string BR5 { get; set; }

    public bool CHKBR6 { get; set; }

    [Display(Name = "6")]
    public string BR6 { get; set; }

    public bool CHKBR7 { get; set; }

    [Display(Name = "7")]
    public string BR7 { get; set; }

    public bool CHKBR8 { get; set; }

    [Display(Name = "8")]
    public string BR8 { get; set; }

    [Display(Name = "Carious Teeth")]
    public string CariousTeeth { get; set; }

    [Display(Name = "Restored Teeth")]
    public string RestoredTeeth { get; set; }

    [Display(Name = "Wear Facets")]
    public string WearFacets { get; set; }

    [Display(Name = "Malformation")]
    public string Malformation { get; set; }

    [Display(Name = "Fracture")]
    public string Fracture { get; set; }

    [Display(Name = "Pulp Vitality Test")]
    public string PulpVitalityTest { get; set; }

    [Display(Name = "Tilting / Drifting")]
    public string TiltingDrifting { get; set; }

    [Display(Name = "Any wasting Disease of Teeth – i.e. Abrasion, erosion etc")]
    public string AnywasDisOfTeeth { get; set; }

    [Display(Name = "Occlusion")]
    public string Occlusion { get; set; }

    [Display(Name = "Stains")]
    public string Stains { get; set; }

    [Display(Name = "Calculus")]
    public string Calculus { get; set; }

    [Display(Name = "Gingival Enlargement")]
    public string GingivalEnlargement { get; set; }

    [Display(Name = "Bleeding on Probing")]
    public string BleedingOnProbing { get; set; }

    [Display(Name = "Periodontal Pockets")]
    public string PeriodontalPockets { get; set; }

    [Display(Name = "Gingival Recession")]
    public string GingivalRecession { get; set; }

    [Display(Name = "Lip")]
    public string Lip { get; set; }

    [Display(Name = "Cheek")]
    public string Cheek { get; set; }

    [Display(Name = "Floor of The Mouth")]
    public string FloorOfTheMouth { get; set; }

    [Display(Name = "Vestibule")]
    public string Vestibule { get; set; }

    [Display(Name = "Tongue")]
    public string Tongue { get; set; }

    [Display(Name = "Palate")]
    public string Palate { get; set; }

    [Display(Name = "Orifices of Salivary Duct")]
    public string OrificesOfSalDuct { get; set; }

    [Display(Name = "Surgery Executed")]
    public string SurgeryExecuted { get; set; }

    [Display(Name = "a) Dimensions")]
    public string Diamensions { get; set; }

    [Display(Name = "b) Boundaries")]
    public string Boundaries { get; set; }

    [Display(Name = "Histopathological Findings")]
    public string HistopathFindings { get; set; }

    public string Investigations { get; set; }

    [Display(Name = "Diagnosis")]
    public string Diagnosis { get; set; }

    [Display(Name = "Treatment Planning")]
    public string TreatmentPlanning { get; set; }

    [Display(Name = "Summary")]
    public string Summary { get; set; }

    [Display(Name = "Facial and Oral Defects")]
    public string FacialAndOralDefect { get; set; }

    [Display(Name = "Existing Mfp Comments")]
    public string ExistingMfpComments { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    public long AllotId { get; set; }

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

    public IEnumerable<MASChiefComplaint> ChiefComplaintlist { get; set; }

    public IEnumerable<PROSMFPCasesheetProperties> Proplist { get; set; }

    public IEnumerable<MASCode> FacialProfileList { get; set; }

    public IEnumerable<MASCode> ShapeOfFaceList { get; set; }

    public IEnumerable<MASCode> VertFaceHeightList { get; set; }

    public IEnumerable<MASCode> ToneOfFacialList { get; set; }

    public IEnumerable<MASCode> TMJList { get; set; }

    public IEnumerable<MASCode> MoveOfMandList { get; set; }

    public IEnumerable<MASCode> MouthLengthList { get; set; }

    public IEnumerable<MASCode> LipLengthSLList { get; set; }

    public IEnumerable<MASCode> LipLengthTTList { get; set; }

    public IEnumerable<MASCode> LipLengthNTList { get; set; }

    public IEnumerable<MASCode> LipSupportList { get; set; }

    public IEnumerable<MASCode> ToothExposureList { get; set; }

    public BillingQueueServiceViewModel billingLabRadQueueViewModal { get; set; }

    public IEnumerable<BillingViewModal> paidLabRadInvestigationList { get; set; }

    public IEnumerable<BillingQueueServiceViewModel> BillingLabRadQueueDetails { get; set; }

    public PatientInformationViewModel patientInformationViewModel { get; set; }

    public BillingQueueServiceViewModel billingQueueViewModal { get; set; }

    public StudentAllotmentViewModel studentAllotmentViewModel { get; set; }

    public FollowupViewModal followupViewModal { get; set; }

    public IEnumerable<FollowupViewModal> followupList { get; set; }

    public IEnumerable<StudentAllotmentViewModel> studentProcedureNotesViewModel { get; set; }

    public IEnumerable<BillingViewModal> paidInvestigationList { get; set; }

    public List<ReferralStatusViewModel> CreatedepartmentReferredStatus { get; set; }

    public List<ReferralStatusViewModel> ViewdepartmentReferredStatus { get; set; }

    public IEnumerable<BillingQueueServiceViewModel> BillingQueueDetails { get; set; }

    public IEnumerable<FileUploadViewModel> FileUploadlist { get; set; }

    public long TreatmentReferredId { get; set; }

    public int ProsMaxilloProId { get; set; }

    [Display(Name = "Treatment Date")]
    public DateTime? MfpTreatmentDate { get; set; }

    public string MfpTreatmentDateDisplay { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime MfpTreatmentDateUpdate
    {
      get
      {
        return this._MfpTreatmentDateUpdate;
      }
      set
      {
        this._MfpTreatmentDateUpdate = value;
      }
    }

    [Display(Name = "Steps/Procedure Description")]
    public string MfpTreatmentDescription { get; set; }

    [Display(Name = "Remarks")]
    public string MfpRemarks { get; set; }

    public IEnumerable<PROSMFPViewModel> MfpTreatList { get; set; }

    [Display(Name = "Facial Profile")]
    public string FacialProfile { get; set; }

    [Display(Name = "Shape of Face")]
    public string ShapeOfFace { get; set; }

    [Display(Name = "Vertical Face Height")]
    public string VertFaceHeight { get; set; }

    [Display(Name = "Tone of Facial Tissues")]
    public string ToneOfFacial { get; set; }

    [Display(Name = "TMJ")]
    public string TMJ { get; set; }

    [Display(Name = "Movements of Mandible")]
    public string MoveOfMand { get; set; }

    [Display(Name = "Mouth Length")]
    public string MouthLength { get; set; }

    [Display(Name = "Lip Length")]
    public string LipLengthSL { get; set; }

    public string LipLengthTT { get; set; }

    public string LipLengthNT { get; set; }

    [Display(Name = "Lip support")]
    public string LipSupport { get; set; }

    public long OpNo { get; set; }

    public string PatientName { get; set; }

    public string Phone { get; set; }

    public string AgeGender { get; set; }

    public string Area { get; set; }

    public int Age { get; set; }

    public int GenderId { get; set; }

    public PrescriptionsViewModel PrescriptionsList { get; set; }

    public IEnumerable<PrescriptionsViewModel> PrescriptionsDetails { get; set; }

    public IEnumerable<PrescriptionsViewModel> PreviousPrescriptionList { get; set; }

    public MedicalAlertViewModel medicalalertviewmodel { get; set; }

    public ApprovalViewModal approvalViewModal { get; set; }

    public IEnumerable<BillingViewModal> deptTreatmentList { get; set; }

    public RadioRegistrationViewModel radioRegistrationviewmodel { get; set; }

    public LaboratoryRegistrationViewModel laboratoryRegistrationviewmodel { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }
  }
}
