// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.PROSRPDViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Enums;
using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class PROSRPDViewModel : EntityBase
  {
    private DateTime _PreliminaryImpressionDate = DateTime.Now;
    private DateTime _DiagnosticCastsDate = DateTime.Now;
    private DateTime _DiagnosticSurveyDate = DateTime.Now;
    private DateTime _MouthPreparationDate = DateTime.Now;
    private DateTime _CustomTrayFDate = DateTime.Now;
    private DateTime _BorderMoldingDate = DateTime.Now;
    private DateTime _FinalImpressionDate = DateTime.Now;
    private DateTime _MasterCastDate = DateTime.Now;
    private DateTime _SurveyingDate = DateTime.Now;
    private DateTime _DesigningDate = DateTime.Now;
    private DateTime _WaxPatternsDate = DateTime.Now;
    private DateTime _CastingDate = DateTime.Now;
    private DateTime _TrimmingPolishingDate = DateTime.Now;
    private DateTime _MetalTrialDate = DateTime.Now;
    private DateTime _DentureBaseOcclusalDate = DateTime.Now;
    private DateTime _BiteRegistrationDate = DateTime.Now;
    private DateTime _TeethArrangementDate = DateTime.Now;
    private DateTime _TryInDate = DateTime.Now;
    private DateTime _DentureProcessingDate = DateTime.Now;
    private DateTime _InsertionDate = DateTime.Now;

    [PrimaryKey]
    [HiddenInput(DisplayValue = false)]
    public int ProsthoRPDId { get; set; }

    public string ProsthoRPDNo { get; set; }

    public int PatientId { get; set; }

    public string MandatoryDummy { get; set; }

    public DateTime? ProsthoRPDDate { get; set; }

    [Display(Name = "Chief Complaint")]
    public string ChiefComplaintName { get; set; }

    public string ChiefComplaint { get; set; }

    [Display(Name = "18")]
    public string T18 { get; set; }

    [Display(Name = "17")]
    public string T17 { get; set; }

    [Display(Name = "16")]
    public string T16 { get; set; }

    [Display(Name = "15")]
    public string T15 { get; set; }

    [Display(Name = "14")]
    public string T14 { get; set; }

    [Display(Name = "13")]
    public string T13 { get; set; }

    [Display(Name = "12")]
    public string T12 { get; set; }

    [Display(Name = "11")]
    public string T11 { get; set; }

    [Display(Name = "21")]
    public string T21 { get; set; }

    [Display(Name = "22")]
    public string T22 { get; set; }

    [Display(Name = "23")]
    public string T23 { get; set; }

    [Display(Name = "24")]
    public string T24 { get; set; }

    [Display(Name = "25")]
    public string T25 { get; set; }

    [Display(Name = "26")]
    public string T26 { get; set; }

    [Display(Name = "27")]
    public string T27 { get; set; }

    [Display(Name = "28")]
    public string T28 { get; set; }

    [Display(Name = "48")]
    public string T48 { get; set; }

    [Display(Name = "47")]
    public string T47 { get; set; }

    [Display(Name = "46")]
    public string T46 { get; set; }

    [Display(Name = "45")]
    public string T45 { get; set; }

    [Display(Name = "44")]
    public string T44 { get; set; }

    [Display(Name = "43")]
    public string T43 { get; set; }

    [Display(Name = "42")]
    public string T42 { get; set; }

    [Display(Name = "41")]
    public string T41 { get; set; }

    [Display(Name = "31")]
    public string T31 { get; set; }

    [Display(Name = "32")]
    public string T32 { get; set; }

    [Display(Name = "33")]
    public string T33 { get; set; }

    [Display(Name = "34")]
    public string T34 { get; set; }

    [Display(Name = "35")]
    public string T35 { get; set; }

    [Display(Name = "36")]
    public string T36 { get; set; }

    [Display(Name = "37")]
    public string T37 { get; set; }

    [Display(Name = "38")]
    public string T38 { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    [Display(Name = "Experience of old Denture")]
    public string ExperienceOfOldDen { get; set; }

    [Display(Name = "Reason")]
    public string MaxillaryReason { get; set; }

    [Display(Name = "Duration of Edentulism")]
    public string MaxillaryDuration { get; set; }

    [Display(Name = "Reason")]
    public string MandibularReason { get; set; }

    [Display(Name = "Duration of Edentulism")]
    public string MandibularDuration { get; set; }

    [Display(Name = "Pre Extraction Records")]
    public string PreExtractionRecords { get; set; }

    [Display(Name = "Mental Attitude")]
    public int? MentalAttitudeId { get; set; }

    [Display(Name = "Medical History")]
    public int? MedicalHistoryId { get; set; }

    [Display(Name = "Any other")]
    public string MedicalHistoryOther { get; set; }

    [Display(Name = "Diet")]
    public int DietId { get; set; }

    [Display(Name = "Habits")]
    public string Habits { get; set; }

    [Display(Name = "Oral Hygiene")]
    public string OralHygiene { get; set; }

    [Display(Name = "Skin Complexion")]
    public string SkinComplexion { get; set; }

    [Display(Name = "Form")]
    public string Form { get; set; }

    [Display(Name = "Profile")]
    public string Profile { get; set; }

    [Display(Name = "Symmetry")]
    public string Symmetry { get; set; }

    [Display(Name = "Vertical Face Height")]
    public int? VerticalFaceHeightId { get; set; }

    [Display(Name = "TMJ")]
    public string TMJ { get; set; }

    [Display(Name = "Length")]
    public string Length { get; set; }

    [Display(Name = "Thickness")]
    public string Thickness { get; set; }

    [Display(Name = "Support")]
    public string Support { get; set; }

    [Display(Name = "Mobility")]
    public string Mobility { get; set; }

    [Display(Name = "Commissure")]
    public string Commissure { get; set; }

    [Display(Name = "Others")]
    public string Others { get; set; }

    [Display(Name = "Neuromuscular Coordination")]
    public string NeuromuscularCoordi { get; set; }

    [Display(Name = "Speech")]
    public string Speech { get; set; }

    [Display(Name = "Furcation Involvement")]
    public string FurcationInvolve { get; set; }

    [Display(Name = "General Alignment")]
    public int? GeneralAlignmentId { get; set; }

    [Display(Name = "Vertical Overlap")]
    public string VerticalOverlap { get; set; }

    [Display(Name = "Horizontal Overlap")]
    public string HorizontalOverlap { get; set; }

    [Display(Name = "Type of Occlusion")]
    public int? TypeOfOcclusionId { get; set; }

    [Display(Name = "Any Protrusive Interference")]
    public string AnyProtrusiveInter { get; set; }

    [Display(Name = "Any working side Interference")]
    public string AnyWorkingSideInter { get; set; }

    [Display(Name = "Balancing side Contacts")]
    public string BalancingSideContacts { get; set; }

    [Display(Name = "Slide in Centric")]
    public int? SlideInCentricId { get; set; }

    [Display(Name = "Evaluation of Oral Mucosa")]
    public string EvalutationOfOral { get; set; }

    [Display(Name = "Color of Mucosa")]
    public int? ColorOfMucosaId { get; set; }

    [Display(Name = "Any Pathologic Changes")]
    public int? AnyPathologicChangesId { get; set; }

    [Display(Name = "Tissue Reaction to Wearing of Prosthesis")]
    public int? TissueReaToWearId { get; set; }

    public string AbbreviationOthers { get; set; }

    [Display(Name = "Upper")]
    public int? UpperId { get; set; }

    [Display(Name = "Lower")]
    public int? LowerId { get; set; }

    [Display(Name = "Any Undercut ")]
    public string AnyUndercut { get; set; }

    [Display(Name = "Any Torus Palatinus ")]
    public int? AnyTorusPalatinusId { get; set; }

    [Display(Name = "Inter-Ridge Distance")]
    public int? RidgeDistanceId { get; set; }

    [Display(Name = "Any High Muscle Attachment")]
    public string AnyHighMuscleAtt { get; set; }

    [Display(Name = "Quality")]
    public int? EvalSalivaQualityId { get; set; }

    [Display(Name = "Quantity")]
    public int? EvalSalivaQuantityId { get; set; }

    [Display(Name = "Evaluation of Space for Mandibular major Connector")]
    public int? SpaceForMandibularId { get; set; }

    [Display(Name = "Gingiva")]
    public string Gingiva { get; set; }

    [Display(Name = "Sulcus Pocket Depth")]
    public string SulcusPocketDepth { get; set; }

    [Display(Name = "Attached Gingiva")]
    public string AttachedGingiva { get; set; }

    [Display(Name = "Dental Caries")]
    public string DentalCaries { get; set; }

    [Display(Name = "Restoration")]
    public string Restoration { get; set; }

    [Display(Name = "Crown Root Ratio")]
    public string CrownRootRatio { get; set; }

    [Display(Name = "Root Angulation")]
    public string RootAngulation { get; set; }

    [Display(Name = "Root Fragments")]
    public string RootFragments { get; set; }

    [Display(Name = "Other Foreign Body")]
    public string OtherForeignBodie { get; set; }

    [Display(Name = "Unerupted Teeth")]
    public string UneruptedTeeth { get; set; }

    [Display(Name = "Crown Root Ratio")]
    public int? CrownRootRatioId { get; set; }

    public string CrownRootRatio1 { get; set; }

    public string CrownRootRatio2 { get; set; }

    public string CrownRootRatio3 { get; set; }

    public string CrownRootRatio4 { get; set; }

    [Display(Name = "Number of Roots")]
    public string NumberOfRoots1 { get; set; }

    public string NumberOfRoots2 { get; set; }

    public string NumberOfRoots3 { get; set; }

    public string NumberOfRoots4 { get; set; }

    [Display(Name = "Root Morphology")]
    public int? RootMorphologyId1 { get; set; }

    public string RootMorphology1 { get; set; }

    public string RootMorphology2 { get; set; }

    public string RootMorphology3 { get; set; }

    public string RootMorphology4 { get; set; }

    public int? RootMorphologyId2 { get; set; }

    public string RootMorphology5 { get; set; }

    public string RootMorphology6 { get; set; }

    public string RootMorphology7 { get; set; }

    public string RootMorphology8 { get; set; }

    [Display(Name = "Presence of Apical Pathology or Root Resorption")]
    public string PresOfApicalRoot1 { get; set; }

    public string PresOfApicalRoot2 { get; set; }

    public string PresOfApicalRoot3 { get; set; }

    public string PresOfApicalRoot4 { get; set; }

    [Display(Name = "Quality of Supporting Bone")]
    public string QualityOfSuppBone1 { get; set; }

    public string QualityOfSuppBone2 { get; set; }

    public string QualityOfSuppBone3 { get; set; }

    public string QualityOfSuppBone4 { get; set; }

    [Display(Name = "Width of Periodontal Ligament")]
    public string WidthOfPerioLigam1 { get; set; }

    public string WidthOfPerioLigam2 { get; set; }

    public string WidthOfPerioLigam3 { get; set; }

    public string WidthOfPerioLigam4 { get; set; }

    [Display(Name = "Continuity of Lamina Dura")]
    public string ContinuityOfLam1 { get; set; }

    public string ContinuityOfLam2 { get; set; }

    public string ContinuityOfLam3 { get; set; }

    public string ContinuityOfLam4 { get; set; }

    [Display(Name = "Any Vertical//Horizontal Bone loss Calculus Deposited")]
    public string AnyVerticalHorizBone1 { get; set; }

    public string AnyVerticalHorizBone2 { get; set; }

    public string AnyVerticalHorizBone3 { get; set; }

    public string AnyVerticalHorizBone4 { get; set; }

    [Display(Name = "Pre-Existing Restorations & Their Relations to Pulp")]
    public string PreExistingRestor1 { get; set; }

    public string PreExistingRestor2 { get; set; }

    [Display(Name = "Status of Root Canal fillings & Pulpal Morphology")]
    public string StatusOfRootCanal1 { get; set; }

    public string StatusOfRootCanal2 { get; set; }

    [Display(Name = "Occlusal plan")]
    public int? OcclusalPlanId { get; set; }

    [Display(Name = "Any Tooth / Teeth Migration")]
    public string AnyToothTeeth { get; set; }

    [Display(Name = "Axial Inclination")]
    public string AxialInclination { get; set; }

    [Display(Name = "Wear Facets")]
    public string WearFacets { get; set; }

    [Display(Name = "Inter Arch Distance")]
    public int? InterArchDistanceId { get; set; }

    [Display(Name = " Preliminary Impressions")]
    public string Preliminary { get; set; }

    [Display(Name = "Diagnostic Casts")]
    public string DiagnosticCasts { get; set; }

    [Display(Name = "Diagnostic Surveying")]
    public string DiagnosticSurveying { get; set; }

    [Display(Name = "Mouth Preparation")]
    public string MouthPreparation { get; set; }

    [Display(Name = "Custom Tray Fabrication")]
    public string CustomTray { get; set; }

    [Display(Name = "Border Molding")]
    public string BorderMolding { get; set; }

    [Display(Name = "Final Impression")]
    public string FinalImpressio { get; set; }

    [Display(Name = "Master Cast")]
    public string MasterCast { get; set; }

    [Display(Name = "Surveying")]
    public string Surveying { get; set; }

    [Display(Name = "Designing")]
    public string Designing { get; set; }

    [Display(Name = "Wax Patterns")]
    public string Waxpatterns { get; set; }

    [Display(Name = "Casting")]
    public string Casting { get; set; }

    [Display(Name = "Frame Work Trimming and Polishing")]
    public string FrameWorkTrim { get; set; }

    [Display(Name = "Metal Trial")]
    public string MetalTrial { get; set; }

    [Display(Name = "Denture Base and Occlusal Rims")]
    public string DentureBase { get; set; }

    [Display(Name = "Bite Registration")]
    public string BiteRegistration { get; set; }

    [Display(Name = "Teeth Arrangement")]
    public string TeethArrang { get; set; }

    [Display(Name = "Try In")]
    public string TryIn { get; set; }

    [Display(Name = "Denture Processing")]
    public string DentureProce { get; set; }

    [Display(Name = "Insertion")]
    public string Insertion { get; set; }

    [Display(Name = "CPD")]
    public bool CpdMetallic { get; set; }

    [Display(Name = "PPD")]
    public bool PpdAcrylic { get; set; }

    [Display(Name = "Kennedy Classification")]
    public string KennedyClassification { get; set; }

    [Display(Name = "Kennedy Classification Modification Space")]
    public string KennedyCModificationSpace { get; set; }

    public long AllotId { get; set; }

    public Diet Diet { get; set; }

    public IEnumerable<MASChiefComplaint> ChiefComplaintlist { get; set; }

    public IEnumerable<PROSRPDCasesheetProperties> Proplist { get; set; }

    public IEnumerable<MASCode> MedicalHistoryList { get; set; }

    public IEnumerable<MASCode> VerticalFaceHeightList { get; set; }

    public IEnumerable<MASCode> GeneralAlignmentList { get; set; }

    public IEnumerable<MASCode> TypeOfOcclusionList { get; set; }

    public IEnumerable<MASCode> SlideInCentricList { get; set; }

    public IEnumerable<MASCode> ColorOfMucosaList { get; set; }

    public IEnumerable<MASCode> AnyPathologicChangesList { get; set; }

    public IEnumerable<MASCode> TissueReaToWearList { get; set; }

    public IEnumerable<MASCode> UpperList { get; set; }

    public IEnumerable<MASCode> LowerList { get; set; }

    public IEnumerable<MASCode> AnyTorusPalatinusList { get; set; }

    public IEnumerable<MASCode> RidgeDistanceList { get; set; }

    public IEnumerable<MASCode> EvalSalivaQualityList { get; set; }

    public IEnumerable<MASCode> EvalSalivaQuantityList { get; set; }

    public IEnumerable<MASCode> SpaceForMandibularList { get; set; }

    public IEnumerable<MASCode> CrownRootRatioList { get; set; }

    public IEnumerable<MASCode> RootMorphologyId1List { get; set; }

    public IEnumerable<MASCode> RootMorphologyId2List { get; set; }

    public IEnumerable<MASCode> OcclusalPlanList { get; set; }

    public IEnumerable<MASCode> InterArchDistanceList { get; set; }

    public IEnumerable<SelectListItem> DietLister { get; set; }

    public IEnumerable<MASCode> MentalAttitudeList { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime PreliminaryImpressionDate
    {
      get
      {
        return this._PreliminaryImpressionDate;
      }
      set
      {
        this._PreliminaryImpressionDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime DiagnosticCastsDate
    {
      get
      {
        return this._DiagnosticCastsDate;
      }
      set
      {
        this._DiagnosticCastsDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime DiagnosticSurveyDate
    {
      get
      {
        return this._DiagnosticSurveyDate;
      }
      set
      {
        this._DiagnosticSurveyDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime MouthPreparationDate
    {
      get
      {
        return this._MouthPreparationDate;
      }
      set
      {
        this._MouthPreparationDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime CustomTrayFDate
    {
      get
      {
        return this._CustomTrayFDate;
      }
      set
      {
        this._CustomTrayFDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime BorderMoldingDate
    {
      get
      {
        return this._BorderMoldingDate;
      }
      set
      {
        this._BorderMoldingDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime FinalImpressionDate
    {
      get
      {
        return this._FinalImpressionDate;
      }
      set
      {
        this._FinalImpressionDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime MasterCastDate
    {
      get
      {
        return this._MasterCastDate;
      }
      set
      {
        this._MasterCastDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime SurveyingDate
    {
      get
      {
        return this._SurveyingDate;
      }
      set
      {
        this._SurveyingDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime DesigningDate
    {
      get
      {
        return this._DesigningDate;
      }
      set
      {
        this._DesigningDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime WaxPatternsDate
    {
      get
      {
        return this._WaxPatternsDate;
      }
      set
      {
        this._WaxPatternsDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime CastingDate
    {
      get
      {
        return this._CastingDate;
      }
      set
      {
        this._CastingDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime TrimmingPolishingDate
    {
      get
      {
        return this._TrimmingPolishingDate;
      }
      set
      {
        this._TrimmingPolishingDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime MetalTrialDate
    {
      get
      {
        return this._MetalTrialDate;
      }
      set
      {
        this._MetalTrialDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime DentureBaseOcclusalDate
    {
      get
      {
        return this._DentureBaseOcclusalDate;
      }
      set
      {
        this._DentureBaseOcclusalDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime BiteRegistrationDate
    {
      get
      {
        return this._BiteRegistrationDate;
      }
      set
      {
        this._BiteRegistrationDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime TeethArrangementDate
    {
      get
      {
        return this._TeethArrangementDate;
      }
      set
      {
        this._TeethArrangementDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime TryInDate
    {
      get
      {
        return this._TryInDate;
      }
      set
      {
        this._TryInDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime DentureProcessingDate
    {
      get
      {
        return this._DentureProcessingDate;
      }
      set
      {
        this._DentureProcessingDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime InsertionDate
    {
      get
      {
        return this._InsertionDate;
      }
      set
      {
        this._InsertionDate = value;
      }
    }

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

    public BillingQueueServiceViewModel billingLabRadQueueViewModal { get; set; }

    public IEnumerable<BillingViewModal> paidLabRadInvestigationList { get; set; }

    public IEnumerable<BillingQueueServiceViewModel> BillingLabRadQueueDetails { get; set; }

    public long TreatmentReferredId { get; set; }

    public int ReferredTreatmentId { get; set; }

    public PROSRPDViewModel()
    {
      if (this.DietLister != null)
        return;
      this.DietLister = ((IEnumerable<string>) Enum.GetNames(typeof (Diet))).Select<string, SelectListItem>((Func<string, SelectListItem>) (name => new SelectListItem()
      {
        Text = name,
        Value = name
      }));
    }

    [Display(Name = "Mental Attitude")]
    public string MentalAttitude { get; set; }

    [Display(Name = "Medical History")]
    public string MedicalHistory { get; set; }

    [Display(Name = "Vertical Face Height")]
    public string VerticalFaceHeight { get; set; }

    [Display(Name = "General Alignment")]
    public string GeneralAlignment { get; set; }

    [Display(Name = "Type of Occlusion")]
    public string TypeOfOcclusion { get; set; }

    [Display(Name = "Slide in Centric")]
    public string SlideInCentric { get; set; }

    [Display(Name = "Color of Mucosa")]
    public string ColorOfMucosa { get; set; }

    [Display(Name = "Any Pathologic Changes")]
    public string AnyPathologicChanges { get; set; }

    [Display(Name = "Tissue Reaction to Wearing of Prosthesis")]
    public string TissueReaToWear { get; set; }

    [Display(Name = "Upper")]
    public string Upper { get; set; }

    [Display(Name = "Lower")]
    public string Lower { get; set; }

    [Display(Name = "Any Torus Palatinus")]
    public string AnyTorusPalatinus { get; set; }

    [Display(Name = "Inter-Ridge Distance")]
    public string RidgeDistance { get; set; }

    [Display(Name = "Evaluation of Space for Mandibular Major Connector")]
    public string SpaceForMandibular { get; set; }

    [Display(Name = "Crown Root Ratio")]
    public string CrownRootRatioRep { get; set; }

    [Display(Name = "Occlusal Plan")]
    public string OcclusalPlan { get; set; }

    [Display(Name = "Inter-Ridge Distance")]
    public string InterArchDistance { get; set; }

    [Display(Name = "Root Morphology")]
    public string RootMorphologyint1 { get; set; }

    public string RootMorphologyint2 { get; set; }

    [Display(Name = "Diet")]
    public string DietName { get; set; }

    [Display(Name = "Quality")]
    public string EvalSalivaQuality { get; set; }

    [Display(Name = "Quantity")]
    public string EvalSalivaQuantity { get; set; }

    public long OpNo { get; set; }

    public string PatientName { get; set; }

    public string Phone { get; set; }

    public string AgeGender { get; set; }

    public string Area { get; set; }

    public int Age { get; set; }

    public int GenderId { get; set; }

    public MedicalAlertViewModel medicalalertviewmodel { get; set; }

    public PrescriptionsViewModel PrescriptionsList { get; set; }

    public IEnumerable<PrescriptionsViewModel> PrescriptionsDetails { get; set; }

    public IEnumerable<PrescriptionsViewModel> PreviousPrescriptionList { get; set; }

    public ApprovalViewModal approvalViewModal { get; set; }

    public IEnumerable<BillingViewModal> deptTreatmentList { get; set; }

    public RadioRegistrationViewModel radioRegistrationviewmodel { get; set; }

    public LaboratoryRegistrationViewModel laboratoryRegistrationviewmodel { get; set; }
  }
}
