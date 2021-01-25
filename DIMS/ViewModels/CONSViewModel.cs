// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.CONSViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using DIMS.Enums;
using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Metron.Entities;

namespace DIMS.ViewModels
{
  public class CONSViewModel : EntityBase
  {
    [PrimaryKey]
    public int ConservativeId { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int PatientId { get; set; }

    public string MandatoryDummy { get; set; }

    public string CONSNo { get; set; }

    public DateTime ConservativeDate { get; set; }

    [Display(Name = "Chief Complaint")]
    public string ChiefComplaintName { get; set; }

    public string ChiefComplaint { get; set; }

    [Display(Name = "Clinical Findings")]
    public string ClinicalFindings { get; set; }

    [Display(Name = "Treatment Plan")]
    public string TreatmentPlan { get; set; }

    [Display(Name = "55")]
    public string T55 { get; set; }

    [Display(Name = "54")]
    public string T54 { get; set; }

    [Display(Name = "53")]
    public string T53 { get; set; }

    [Display(Name = "52")]
    public string T52 { get; set; }

    [Display(Name = "51")]
    public string T51 { get; set; }

    [Display(Name = "61")]
    public string T61 { get; set; }

    [Display(Name = "62")]
    public string T62 { get; set; }

    [Display(Name = "63")]
    public string T63 { get; set; }

    [Display(Name = "64")]
    public string T64 { get; set; }

    [Display(Name = "65")]
    public string T65 { get; set; }

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

    [Display(Name = "35")]
    public string T34 { get; set; }

    [Display(Name = "35")]
    public string T35 { get; set; }

    [Display(Name = "36")]
    public string T36 { get; set; }

    [Display(Name = "37")]
    public string T37 { get; set; }

    [Display(Name = "38")]
    public string T38 { get; set; }

    [Display(Name = "85")]
    public string T85 { get; set; }

    [Display(Name = "84")]
    public string T84 { get; set; }

    [Display(Name = "83")]
    public string T83 { get; set; }

    [Display(Name = "82")]
    public string T82 { get; set; }

    [Display(Name = "81")]
    public string T81 { get; set; }

    [Display(Name = "71")]
    public string T71 { get; set; }

    [Display(Name = "72")]
    public string T72 { get; set; }

    [Display(Name = "73")]
    public string T73 { get; set; }

    [Display(Name = "74")]
    public string T74 { get; set; }

    [Display(Name = "75")]
    public string T75 { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    [Display(Name = "Radiographic Finding")]
    public string RadiographicFinding { get; set; }

    [Display(Name = "Normal")]
    public string PVOnColdNormal { get; set; }

    [Display(Name = "Delayed")]
    public string PVOnColdDelayed { get; set; }

    [Display(Name = "Non Responsive")]
    public string PVOnColdNonRes { get; set; }

    [Display(Name = "Normal")]
    public string PVOnHeatNormal { get; set; }

    [Display(Name = "Delayed")]
    public string PVOnHeatDelayed { get; set; }

    [Display(Name = "Non Responsive")]
    public string PVOnHeatNonRes { get; set; }

    [Display(Name = "Normal")]
    public string PVOnElecNormal { get; set; }

    [Display(Name = "Delayed")]
    public string PVOnElecDelayed { get; set; }

    [Display(Name = "Non Responsive")]
    public string PVOnElecNonRes { get; set; }

    [Display(Name = "Filling")]
    public string Filling { get; set; }

    [Display(Name = "Bleaching")]
    public string Bleaching { get; set; }

    [Display(Name = "Crown")]
    public string Crown { get; set; }

    [Display(Name = "Root Canal")]
    public string RootCanal { get; set; }

    [Display(Name = "Post Root Canal")]
    public string PostRootCanal { get; set; }

    [Display(Name = "Other Treatment")]
    public string OtherTreatment { get; set; }

    [Display(Name = "History of present illness")]
    public string PresentIllnessHistory { get; set; }

    [Display(Name = "Medical history")]
    public string MedicalHistory { get; set; }

    [Display(Name = "Drug history")]
    public string DrugHistory { get; set; }

    [Display(Name = "Past Dental History")]
    public string PastDentalHistory { get; set; }

    [Display(Name = "Family History")]
    public string FamilyHistory { get; set; }

    [Display(Name = "Personal History")]
    public string PersonalHistory { get; set; }

    [Display(Name = "Diet")]
    public string Diet { get; set; }

    [Display(Name = "Habits")]
    public string Habits { get; set; }

    [Display(Name = "Oral Hygiene")]
    public string OralHygiene { get; set; }

    [Display(Name = "Built")]
    public string Built { get; set; }

    [Display(Name = "Nutritional Status")]
    public string NutritionalStatus { get; set; }

    [Display(Name = "Psychological Attitude")]
    public string PsychologicalAttitude { get; set; }

    [Display(Name = "Picol")]
    public string Picol { get; set; }

    [Display(Name = "Blood Pressure")]
    public string BloodPressure { get; set; }

    [Display(Name = "Temperature")]
    public string Temperature { get; set; }

    [Display(Name = "Facial Symmetry")]
    public string FacialSymmetry { get; set; }

    [Display(Name = "Tmj")]
    public string Tmj { get; set; }

    [Display(Name = "Any Other")]
    public string ExtraOralOther { get; set; }

    public string AbbreviationOthers { get; set; }

    [Display(Name = "Buccal Mucosa")]
    public string BuccalMucosa { get; set; }

    [Display(Name = "Gingiva")]
    public string Gingiva { get; set; }

    [Display(Name = "Palate")]
    public string Palate { get; set; }

    [Display(Name = "Tongue")]
    public string Tongue { get; set; }

    [Display(Name = "Floor of Mouth")]
    public string FloorOfMouth { get; set; }

    [Display(Name = "Vestibule")]
    public string Vestibule { get; set; }

    [Display(Name = "Lips")]
    public string Lips { get; set; }

    [Display(Name = "IOPAR/RVG/BITE WING/OCCLUSAL")]
    public string IoparRvgBiteOcclussal { get; set; }

    [Display(Name = "18")]
    public string VitalityTooth18 { get; set; }

    [Display(Name = "17")]
    public string VitalityTooth17 { get; set; }

    [Display(Name = "16")]
    public string VitalityTooth16 { get; set; }

    [Display(Name = "15")]
    public string VitalityTooth15 { get; set; }

    [Display(Name = "14")]
    public string VitalityTooth14 { get; set; }

    [Display(Name = "13")]
    public string VitalityTooth13 { get; set; }

    [Display(Name = "12")]
    public string VitalityTooth12 { get; set; }

    [Display(Name = "11")]
    public string VitalityTooth11 { get; set; }

    [Display(Name = "21")]
    public string VitalityTooth21 { get; set; }

    [Display(Name = "22")]
    public string VitalityTooth22 { get; set; }

    [Display(Name = "23")]
    public string VitalityTooth23 { get; set; }

    [Display(Name = "24")]
    public string VitalityTooth24 { get; set; }

    [Display(Name = "25")]
    public string VitalityTooth25 { get; set; }

    [Display(Name = "26")]
    public string VitalityTooth26 { get; set; }

    [Display(Name = "27")]
    public string VitalityTooth27 { get; set; }

    [Display(Name = "28")]
    public string VitalityTooth28 { get; set; }

    [Display(Name = "48")]
    public string VitalityTooth48 { get; set; }

    [Display(Name = "47")]
    public string VitalityTooth47 { get; set; }

    [Display(Name = "46")]
    public string VitalityTooth46 { get; set; }

    [Display(Name = "45")]
    public string VitalityTooth45 { get; set; }

    [Display(Name = "44")]
    public string VitalityTooth44 { get; set; }

    [Display(Name = "43")]
    public string VitalityTooth43 { get; set; }

    [Display(Name = "42")]
    public string VitalityTooth42 { get; set; }

    [Display(Name = "41")]
    public string VitalityTooth41 { get; set; }

    [Display(Name = "31")]
    public string VitalityTooth31 { get; set; }

    [Display(Name = "32")]
    public string VitalityTooth32 { get; set; }

    [Display(Name = "33")]
    public string VitalityTooth33 { get; set; }

    [Display(Name = "34")]
    public string VitalityTooth34 { get; set; }

    [Display(Name = "35")]
    public string VitalityTooth35 { get; set; }

    [Display(Name = "36")]
    public string VitalityTooth36 { get; set; }

    [Display(Name = "37")]
    public string VitalityTooth37 { get; set; }

    [Display(Name = "38")]
    public string VitalityTooth38 { get; set; }

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

    public IEnumerable<CONSCasesheetProperties> Proplist { get; set; }

    public PrescriptionsViewModel PrescriptionsList { get; set; }

    public MedicalAlertViewModel medicalalertviewmodel { get; set; }

    public IEnumerable<PrescriptionsViewModel> PrescriptionsDetails { get; set; }

    public IEnumerable<PrescriptionsViewModel> PreviousPrescriptionList { get; set; }

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

    public ApprovalViewModal approvalViewModal { get; set; }

    public BillingQueueServiceViewModel billingLabRadQueueViewModal { get; set; }

    public IEnumerable<BillingViewModal> paidLabRadInvestigationList { get; set; }

    public IEnumerable<BillingQueueServiceViewModel> BillingLabRadQueueDetails { get; set; }

    public RadioRegistrationViewModel radioRegistrationviewmodel { get; set; }

    public LaboratoryRegistrationViewModel laboratoryRegistrationviewmodel { get; set; }

    public IEnumerable<BillingViewModal> deptTreatmentList { get; set; }

    public long TreatmentReferredId { get; set; }

    public IEnumerable<FileUploadViewModel> FileUploadlist { get; set; }

    public IEnumerable<CONSViewModel> Treatmentlist { get; set; }

    public IEnumerable<SelectListItem> ToothLister { get; set; }

    public CONSViewModel()
    {
      if (this.ToothLister != null)
        return;
      this.ToothLister = ((IEnumerable<string>) Enum.GetNames(typeof (ToothNumbers))).Select<string, SelectListItem>((Func<string, SelectListItem>) (name => new SelectListItem()
      {
        Text = name,
        Value = name
      }));
    }

    public int RestorativeProId { get; set; }

    [DisplayName("Tooth Number")]
    public ToothNumbers ToothNumbers { get; set; }

    [Display(Name = "Tooth No.")]
    public string ToothNumber { get; set; }

    [Display(Name = "Type of cavity")]
    public string CavityType { get; set; }

    [Display(Name = "Photographs/Casts/X ray")]
    public string PhotographsCastsXray { get; set; }

    [Display(Name = "Restorative Material")]
    public string RestorativeMaterial { get; set; }

    [Display(Name = "Cavity Preparation")]
    public string CavityPreparation { get; set; }

    [Display(Name = "Impression/Pattern")]
    public string ImpressionPattern { get; set; }

    [Display(Name = "Linear/Bases/Varnish")]
    public string LinearBasesVarnish { get; set; }

    [Display(Name = "Matrix Band and Wedges")]
    public string MatrixBandWedges { get; set; }

    [Display(Name = "Restoration")]
    public string Restoration { get; set; }

    [Display(Name = "Finishing and Restoration")]
    public string FinishingAndRestoration { get; set; }

    [Display(Name = "Review 1")]
    public string DirectIndirectReview1 { get; set; }

    [Display(Name = "Review 2")]
    public string DirectIndirectReview2 { get; set; }

    [Display(Name = "Review 3")]
    public string DirectIndirectReview3 { get; set; }

    [Display(Name = "Review 1")]
    public string DeepCariesReview1 { get; set; }

    [Display(Name = "Review 2")]
    public string DeepCariesReview2 { get; set; }

    [Display(Name = "Review 3")]
    public string DeepCariesReview3 { get; set; }

    public IEnumerable<CONSViewModel> RpList { get; set; }

    public int PostAndCoreId { get; set; }

    [DisplayName("Tooth Number")]
    public ToothNumbers ToothNumbersA { get; set; }

    [Display(Name = "Photographs/Impressions")]
    public string PhotographsImpression { get; set; }

    [Display(Name = "Type of Post")]
    public string TypeOfPost { get; set; }

    [Display(Name = "Post Space Preparation")]
    public string PostSpacePreparation { get; set; }

    [Display(Name = "Pattern/Impression(Direct/Indirect)")]
    public string PatternImpression { get; set; }

    [Display(Name = "Temporization")]
    public string Temporization { get; set; }

    [Display(Name = "Insertion and Cementation")]
    public string InsertionCementation { get; set; }

    [Display(Name = "Insertion and Temporization")]
    public string InsertionTemporization { get; set; }

    [Display(Name = "Review 1")]
    public string PcReview1 { get; set; }

    [Display(Name = "Review 2")]
    public string PcReview2 { get; set; }

    [Display(Name = "Review 3")]
    public string PcReview3 { get; set; }

    public IEnumerable<CONSViewModel> PcList { get; set; }

    public int SurgicalProId { get; set; }

    [DisplayName("Tooth Number")]
    public ToothNumbers ToothNumbersB { get; set; }

    [Display(Name = "A.Premedication")]
    public string Premedication { get; set; }

    [Display(Name = "B.Anesthesia Used")]
    public string AnesthesiaUsed { get; set; }

    [Display(Name = "C.Flap Design")]
    public string FlapDesign { get; set; }

    [Display(Name = "D.Graft")]
    public string Graft { get; set; }

    [Display(Name = "E.Suturing")]
    public string Suturing { get; set; }

    [Display(Name = "Review 1")]
    public string SpReview1 { get; set; }

    [Display(Name = "Review 2")]
    public string SpReview2 { get; set; }

    [Display(Name = "Review 3")]
    public string SpReview3 { get; set; }

    public IEnumerable<CONSViewModel> SpList { get; set; }

    public int EstheticCorrId { get; set; }

    [DisplayName("Tooth Number")]
    public ToothNumbers ToothNumbersC { get; set; }

    [Display(Name = "Impression Photograph")]
    public string ImpressionPhotograph { get; set; }

    [Display(Name = "Vitality Testing")]
    public string VitalityTesting { get; set; }

    [Display(Name = "Radiographic Interpretation")]
    public string RadiographicInterpretation { get; set; }

    [Display(Name = "Treatment Procedure")]
    public string TreatmentProcedure { get; set; }

    [Display(Name = "Tooth Preparation")]
    public string ToothPreparation { get; set; }

    [Display(Name = "Shade Selection")]
    public string ShadeSelection { get; set; }

    [Display(Name = "Insertion Cementation")]
    public string EcInsertionCementation { get; set; }

    [Display(Name = "Finishing Polishing")]
    public string FinishingPolishing { get; set; }

    [Display(Name = "Review 1")]
    public string EcReview1 { get; set; }

    [Display(Name = "Review 2")]
    public string EcReview2 { get; set; }

    public IEnumerable<CONSViewModel> EcList { get; set; }

    public int BleachingId { get; set; }

    [DisplayName("Tooth Number")]
    public ToothNumbers ToothNumbersD { get; set; }

    [Display(Name = "Vitality Testing")]
    public string BlVitalityTesting { get; set; }

    [Display(Name = "Photographs Casts")]
    public string PhotographsCasts { get; set; }

    [Display(Name = "Radiographic Interpretation")]
    public string BlRadiographicInterpretation { get; set; }

    [Display(Name = "Shade of Discoloured Tooth")]
    public string DiscolouredToothShade { get; set; }

    [Display(Name = "Shade of Adjacent Tooth")]
    public string AdjacentToothShade { get; set; }

    [Display(Name = "Bleaching Procedure")]
    public string BleachingProcedure { get; set; }

    [Display(Name = "Review 1")]
    public string BReview1 { get; set; }

    [Display(Name = "Review 2")]
    public string BReview2 { get; set; }

    [Display(Name = "Review 3")]
    public string BReview3 { get; set; }

    [Display(Name = "Type of prosthesis(if needed)")]
    public string ProsthesisType { get; set; }

    public IEnumerable<CONSViewModel> BlList { get; set; }

    public int TraumatisedToothId { get; set; }

    [DisplayName("Tooth Number")]
    public ToothNumbers ToothNumbersE { get; set; }

    [Display(Name = "Elli's Type")]
    public string EllisType { get; set; }

    [Display(Name = "Soft Tissue Injuries")]
    public string SoftTissueInjuries { get; set; }

    [Display(Name = "Facial Skeletal Injuries")]
    public string FacialSkeletalInjuries { get; set; }

    [Display(Name = "Luxation Injuries")]
    public string LuxationInjuries { get; set; }

    [Display(Name = "Vitality Testing")]
    public string TtVitalityTesting { get; set; }

    [Display(Name = "Radiographic Interpretation")]
    public string TtRadiographicInterpretation { get; set; }

    [Display(Name = "Impression/Photographs/Cast")]
    public string TtImpressionPhotographsCast { get; set; }

    [Display(Name = "Restoration")]
    public string TtRestoration { get; set; }

    [Display(Name = "Splinting")]
    public string Splinting { get; set; }

    [Display(Name = "Crown Lengthening")]
    public string CrownLengthening { get; set; }

    [Display(Name = "Surgical Management")]
    public string SurgicalManagement { get; set; }

    [Display(Name = "Orthodontic Intrusion/Extrusion/Other Tooth Movement")]
    public string OrthodonticIntrusion { get; set; }

    [Display(Name = "Review 1")]
    public string TtReview1 { get; set; }

    [Display(Name = "Review 2")]
    public string TtReview2 { get; set; }

    [Display(Name = "Review 3")]
    public string TtReview3 { get; set; }

    public IEnumerable<CONSViewModel> TtList { get; set; }

    public int RootCanalId { get; set; }

    [DisplayName("Tooth Number")]
    public ToothNumbers ToothNumbersF { get; set; }

    [Display(Name = "Radiographic Interpretation")]
    public string RcRadiographicInterpretation { get; set; }

    [Display(Name = "Access Opening Canal Location and Pulp extirpation")]
    public string AccessOpeningCanal { get; set; }

    [Display(Name = "No of Canal")]
    public string NoOfCanal { get; set; }

    [Display(Name = "Additional Canals")]
    public string AdditionalCanals { get; set; }

    [Display(Name = "Working Length Determination")]
    public string WorkingLengthDetermination { get; set; }

    [Display(Name = "Shaping & Cleaning")]
    public string ShapingAndCleaning { get; set; }

    [Display(Name = "Rotary Instrumentation/Manual Filing")]
    public string RotaryInstrumentation { get; set; }

    [Display(Name = "Irrigant Used")]
    public string IrrigantUsed { get; set; }

    [Display(Name = "Intracanal Medicament Placed")]
    public string IntracanalMedicament { get; set; }

    [Display(Name = "Master Cone Selection")]
    public string MasterConeSelection { get; set; }

    [Display(Name = "Obturation Technique")]
    public string ObturationTechnique { get; set; }

    [Display(Name = "Post Endodontic Restoration")]
    public string PostEndodonticRestoration { get; set; }

    [Display(Name = "Prosthetic Rehabilitation")]
    public string ProstheticRehabilitation { get; set; }

    public IEnumerable<CONSViewModel> RcList { get; set; }

    public int ReRootCanalId { get; set; }

    [DisplayName("Tooth Number")]
    public ToothNumbers ToothNumbersG { get; set; }

    [Display(Name = "Radiographic Interpretation")]
    public string ReRadiographicInterpretation { get; set; }

    [Display(Name = "G.P. Removal and Canal Location")]
    public string GpRemovalCanalLocation { get; set; }

    [Display(Name = "No of Canal")]
    public string ReNoOfCanal { get; set; }

    [Display(Name = "Additional Canals")]
    public string ReAdditionalCanals { get; set; }

    [Display(Name = "Working Length Determination")]
    public string ReWorkingLengthDetermination { get; set; }

    [Display(Name = "Shaping & Cleaning")]
    public string ReShapingAndCleaning { get; set; }

    [Display(Name = "Rotary Instrumentation/Manual Filing")]
    public string ReRotaryInstrumentation { get; set; }

    [Display(Name = "Irrigant Used")]
    public string ReIrrigantUsed { get; set; }

    [Display(Name = "Intracanal Medicament Placed")]
    public string ReIntracanalMedicament { get; set; }

    [Display(Name = "Master Cone Selection")]
    public string ReMasterConeSelection { get; set; }

    [Display(Name = "Obturation Technique")]
    public string ReObturationTechnique { get; set; }

    [Display(Name = "Post Endodontic Restoration")]
    public string RePostEndodonticRestoration { get; set; }

    [Display(Name = "Prosthetic Rehabilitation")]
    public string ReProstheticRehabilitation { get; set; }

    public IEnumerable<CONSViewModel> ReList { get; set; }

    public int IncompleteRootId { get; set; }

    [DisplayName("Tooth Number")]
    public ToothNumbers ToothNumbersH { get; set; }

    [Display(Name = "Vitality")]
    public string IncVitality { get; set; }

    [Display(Name = "Photographa,cast")]
    public string IncPhotographCast { get; set; }

    [Display(Name = "Radiograph Interpretation")]
    public string IncRadiographInterpretation { get; set; }

    [Display(Name = "Treatment Procedure")]
    public string IncTreatmentProcedure { get; set; }

    [Display(Name = "Material Choice")]
    public string IncMaterialChoice { get; set; }

    [Display(Name = "Site of Management")]
    public string IncSiteOfManagement { get; set; }

    [Display(Name = "Other Restorative Procedure")]
    public string IncOtherRestorativePro { get; set; }

    [Display(Name = "Review1")]
    public string IncReview1 { get; set; }

    [Display(Name = "Review2")]
    public string IncReview2 { get; set; }

    [Display(Name = "Review3")]
    public string IncReview3 { get; set; }

    public IEnumerable<CONSViewModel> RfList { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    [Display(Name = "Date")]
    public string CONSDateDisplay { get; set; }

    public long OpNo { get; set; }

    public string PatientName { get; set; }

    public string AgeGender { get; set; }

    public int? Age { get; set; }

    public int? GenderId { get; set; }

    public string Area { get; set; }

    [Display(Name = "Phone")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; }
  }
}
