// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.OMRViewModel
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
  public class OMRViewModel : EntityBase
  {
    [PrimaryKey]
    public int OMRId { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int PatientId { get; set; }

    public string MandatoryDummy { get; set; }

    public string OMRNo { get; set; }

    public DateTime OMRDate { get; set; }

    [Display(Name = "Chief Complaint")]
    public string ChiefComplaintName { get; set; }

    [DataType(DataType.MultilineText)]
    [Display(Name = "Chief Complaint")]
    public string ChiefComplaint { get; set; }

    [Display(Name = "Present Illness")]
    public string PresentIllnessName { get; set; }

    [DataType(DataType.MultilineText)]
    public string PresentIllness { get; set; }

    [Display(Name = "Past Dental History")]
    [DataType(DataType.MultilineText)]
    public string PastDentalHistory { get; set; }

    [Display(Name = "Past Medical History")]
    [DataType(DataType.MultilineText)]
    public string PastMedicalHistory { get; set; }

    [Display(Name = "Family History")]
    [DataType(DataType.MultilineText)]
    public string FamilyHistory { get; set; }

    [Display(Name = "Others")]
    [DataType(DataType.MultilineText)]
    public string OthersHistory { get; set; }

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

    [Display(Name = "Others")]
    public string AbbreviationOthers { get; set; }

    [Display(Name = "Special Case(if any)")]
    public string SpecialCase { get; set; }

    public string Allergy { get; set; }

    [Display(Name = "Blood Transfusion")]
    public string BloodTransfusion { get; set; }

    [Display(Name = "Bleeding Diathesis")]
    public string BleedingDiathesis { get; set; }

    public string CNS { get; set; }

    public string Dermatology { get; set; }

    [Display(Name = "Diabetes Milletus")]
    public string DiabetesMilletus { get; set; }

    [Display(Name = "Drug Allergy")]
    public string DrugAllergy { get; set; }

    [Display(Name = "Endocrine Disorders")]
    public string EndocrineDisorders { get; set; }

    public string ENT { get; set; }

    [Display(Name = "Epileptic Seizures")]
    public string EpilepticSeizures { get; set; }

    public string GIT { get; set; }

    [Display(Name = "Heart Disease")]
    public string HeartDisease { get; set; }

    [Display(Name = "HIV Related Symptoms")]
    public string HIVRelatedSymptoms { get; set; }

    public string Hospitalization { get; set; }

    public string Hypertension { get; set; }

    public string Pregnancy { get; set; }

    public string Psychiatry { get; set; }

    public string Respiratory { get; set; }

    [Display(Name = "Rheumatic Fever")]
    public string RheumaticFever { get; set; }

    [Display(Name = "STD Urgenital")]
    public string STDUrgenital { get; set; }

    [Display(Name = "Drug History")]
    public string DrugHistory { get; set; }

    [Display(Name = "Others")]
    public string MedicalHistoryOthers { get; set; }

    [Display(Name = "Parafunctional Habits")]
    public int? ParafunctionalHabitsId { get; set; }

    [Display(Name = "Others")]
    public string ParafunctionalHabitsOthers { get; set; }

    public string Sleep { get; set; }

    [Display(Name = "Diet")]
    public int? DietId { get; set; }

    [Display(Name = "Alcohol Intake")]
    public string AlcoholDur { get; set; }

    public string AlcoholFreq { get; set; }

    [Display(Name = "Appetite")]
    public string AppetiteDur { get; set; }

    public string AppetiteFreq { get; set; }

    [Display(Name = "Beedi")]
    public string BeediDur { get; set; }

    public string BeediFreq { get; set; }

    [Display(Name = "Betel Chewing")]
    public string BetelChewingDur { get; set; }

    public string BetelChewingFreq { get; set; }

    [Display(Name = "Betel Nut")]
    public string BetelNutDur { get; set; }

    public string BetelNutFreq { get; set; }

    [Display(Name = "Chewing")]
    public string ChewingDur { get; set; }

    public string ChewingFreq { get; set; }

    [Display(Name = "Cigarette")]
    public string CigaretteDur { get; set; }

    public string CigaretteFreq { get; set; }

    [Display(Name = "Gutka")]
    public string GutkaDur { get; set; }

    public string GutkaFreq { get; set; }

    [Display(Name = "Pan")]
    public string PanDur { get; set; }

    public string PanFreq { get; set; }

    [Display(Name = "Smoking")]
    public string SmokingDur { get; set; }

    public string SmokingFreq { get; set; }

    [Display(Name = "Tobacco")]
    public string TobaccoDur { get; set; }

    public string TobaccoFreq { get; set; }

    [Display(Name = "Others")]
    [DataType(DataType.MultilineText)]
    public string PersonalHistoryOthers { get; set; }

    [Display(Name = "Brushing Method")]
    public int? BrushingHabitsMethodId { get; set; }

    public string BrushingHabitsMethodOthers { get; set; }

    [Display(Name = "Frequency")]
    public int? BrushingHabitsFreqId { get; set; }

    public string BrushingHabitsFreqOthers { get; set; }

    [Display(Name = "Duration")]
    public int? BrushingHabitsDurId { get; set; }

    public string BrushingHabitsDurOthers { get; set; }

    [Display(Name = "Frequency of changing brush")]
    public int? ChangingBrushFreqId { get; set; }

    public string ChangingBrushFreqOthers { get; set; }

    [Display(Name = "Type of dentifrice")]
    public int? DentifriceTypeId { get; set; }

    [Display(Name = "If any others dentifrice")]
    public string DentifriceTypeOthers { get; set; }

    [Display(Name = "Type of brush")]
    public int? BrushTypeId { get; set; }

    [Display(Name = "If any others brush type")]
    public string BrushTypeOthers { get; set; }

    [Display(Name = "Bowel Bladder Movements")]
    public string BowelBladderMovements { get; set; }

    [Display(Name = "Menstrual History")]
    [DataType(DataType.MultilineText)]
    public string MenstrualHistory { get; set; }

    public string Built { get; set; }

    public string Clubbing { get; set; }

    public string Conjuctiva { get; set; }

    public string Cyanosis { get; set; }

    public string Gait { get; set; }

    public string Hair { get; set; }

    public string Height { get; set; }

    public string Icterus { get; set; }

    public string Nails { get; set; }

    public string Nourishment { get; set; }

    public string Oedema { get; set; }

    public string Pallor { get; set; }

    public string Posture { get; set; }

    public string Sclera { get; set; }

    public string Skin { get; set; }

    public string Weight { get; set; }

    [Display(Name = "Others")]
    public string PhysicalExamOthers { get; set; }

    [Display(Name = "Blood Pressure")]
    public string BPDia { get; set; }

    public string BPSys { get; set; }

    public string Pulse { get; set; }

    [Display(Name = "Respiratory rate")]
    public string Respiratoryrate { get; set; }

    public string Temperature { get; set; }

    [Display(Name = "Others")]
    public string VitalOthers { get; set; }

    public string Eyes { get; set; }

    public string Face { get; set; }

    public string Nose { get; set; }

    [Display(Name = "Salivary Glands")]
    public string SalivaryGlands { get; set; }

    [Display(Name = "Skin")]
    public string ClinicalSkin { get; set; }

    [Display(Name = "Mouth Opening")]
    public string MouthOpening { get; set; }

    public string TMJ { get; set; }

    [Display(Name = "Others")]
    public string ClinicalExamOthers { get; set; }

    public string Clicking { get; set; }

    [Display(Name = "Difficult to open the next")]
    public string Difficultopennext { get; set; }

    [Display(Name = "Jaw Deviation")]
    public string JawDeviation { get; set; }

    [Display(Name = "Tenderness")]
    public string TemporoJointTenderness { get; set; }

    [Display(Name = "Others")]
    public string TemporoJointOthers { get; set; }

    [Display(Name = "Consistency")]
    public int? ConsistencyId { get; set; }

    [Display(Name = "Others")]
    public string ConsistencyOthers { get; set; }

    [Display(Name = "Cross Bite")]
    public string LymphCrossBite { get; set; }

    [Display(Name = "Group")]
    public int? GroupId { get; set; }

    [Display(Name = "Others")]
    public string GroupOthers { get; set; }

    public string Mobility { get; set; }

    public string Size { get; set; }

    [Display(Name = "Tenderness")]
    public string LymphTenderness { get; set; }

    public string Palpable { get; set; }

    public string Nonpalpable { get; set; }

    [Display(Name = "Others")]
    public string LymphOthers { get; set; }

    [Display(Name = "Lip")]
    public string CleftLip { get; set; }

    [Display(Name = "Palate")]
    public string CleftPalate { get; set; }

    public string Crowding { get; set; }

    public string Impaction { get; set; }

    public string Spacing { get; set; }

    [Display(Name = "Over Bite")]
    public int? OverBiteId { get; set; }

    [Display(Name = "Over Jet")]
    public int? OverJetId { get; set; }

    [Display(Name = "Cross Bite")]
    public string CrossBite { get; set; }

    [Display(Name = "Scissor Bite")]
    public string ScissorBite { get; set; }

    [Display(Name = "Moral Relationship")]
    public int? MoralRelationshipId { get; set; }

    [Display(Name = "Canine Relationship")]
    public int? CaninerelationshipId { get; set; }

    [Display(Name = "Skeletal Relationship")]
    public int? SkeletalRelationshipId { get; set; }

    [Display(Name = "Others")]
    public string HardTissueExamOthers { get; set; }

    public string Abrasion { get; set; }

    public string Attrition { get; set; }

    public string Erosion { get; set; }

    [Display(Name = "Molar Relation")]
    public int? OcculsionMolarRelationId { get; set; }

    public string OcculsionMolarOthers { get; set; }

    [Display(Name = "Canine Relation")]
    public int? OcculsionCanineRelationId { get; set; }

    public string OcculsionCanineOthers { get; set; }

    [Display(Name = "Others")]
    [DataType(DataType.MultilineText)]
    public string OcculsionOthers { get; set; }

    [Display(Name = "Class I")]
    public string Classl { get; set; }

    [Display(Name = "Class II")]
    public string Classll { get; set; }

    [Display(Name = "Class III")]
    public string Classlll { get; set; }

    [Display(Name = "Grade I")]
    public string Gradel { get; set; }

    [Display(Name = "Grade II")]
    public string Gradell { get; set; }

    [Display(Name = "Grade III")]
    public string Gradelll { get; set; }

    [Display(Name = "Tender on Percussion")]
    public string TenderPercussion { get; set; }

    [Display(Name = "Others")]
    public string MobilityOthers { get; set; }

    public string Labial { get; set; }

    public string Buccal { get; set; }

    [Display(Name = "Floor of the Mouth")]
    public string FloorMouth { get; set; }

    public string Vestibular { get; set; }

    public string Lingual { get; set; }

    public string Palatal { get; set; }

    [Display(Name = "Others")]
    public string MucosaOthers { get; set; }

    [Display(Name = "Orifices of Salivary Duct")]
    public string OrificesSalivaryDuct { get; set; }

    public string Stains { get; set; }

    public string Calculus { get; set; }

    public string Plaque { get; set; }

    [Display(Name = "Gingival Enlargement")]
    public string GingivalEnlargement { get; set; }

    [Display(Name = "Bleeding on Probing")]
    public string BleedingOnProbing { get; set; }

    [Display(Name = "Periodontal Pockets")]
    public string PeriodontalPockets { get; set; }

    [Display(Name = "Gingival Recession")]
    public string GingivalRecession { get; set; }

    [Display(Name = "Furcation Involvement")]
    public string FurcationInvolvement { get; set; }

    [Display(Name = "Mucogingival Problem")]
    public string MucogingivalProblem { get; set; }

    public string Generalised { get; set; }

    public string Localised { get; set; }

    [Display(Name = "Examination of Specific lesion")]
    public string SpecificLesion { get; set; }

    [Display(Name = "Local Examination of chief complaint/Specific findings")]
    public string LocalExaminationchiefComplaint { get; set; }

    public string Inspection { get; set; }

    public string Palpation { get; set; }

    public string Percussion { get; set; }

    public string Auscultation { get; set; }

    [Display(Name = "Temporalis")]
    public string Temporalis { get; set; }

    [Display(Name = "Masseter")]
    public string Masseter { get; set; }

    [Display(Name = "Medial Pterygoid")]
    public string MedialPterygoid { get; set; }

    [Display(Name = "Lateral Pterygoid")]
    public string LateralPterygoid { get; set; }

    [Display(Name = "Others")]
    public string MasticationOthers { get; set; }

    [Display(Name = "Tongue")]
    public string Tongue { get; set; }

    [Display(Name = "Normal")]
    public bool rdnColorNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnColorAbnormal { get; set; }

    [Display(Name = "Color")]
    public string Color { get; set; }

    [Display(Name = "Normal")]
    public bool rdnContourNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnContourAbnormal { get; set; }

    [Display(Name = "Contour")]
    public string Contour { get; set; }

    [Display(Name = "Normal")]
    public bool rdnConsistencyNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnConsistencyAbnormal { get; set; }

    [Display(Name = "Consistency")]
    public string GingivalConsistency { get; set; }

    [Display(Name = "Case Summary")]
    [DataType(DataType.MultilineText)]
    public string CaseSummary { get; set; }

    [Display(Name = "Provisional Diagnosis")]
    [DataType(DataType.MultilineText)]
    public string ProvisionalDiagnosis { get; set; }

    [Display(Name = "Differential Diagnosis")]
    [DataType(DataType.MultilineText)]
    public string DifferentialDiagnosis { get; set; }

    [Display(Name = "Final Diagnosis")]
    [DataType(DataType.MultilineText)]
    public string FinalDiagnosis { get; set; }

    [Display(Name = "Referred to Doctor")]
    public string ReferredSpecialityDoctor { get; set; }

    public string Speciality { get; set; }

    [Display(Name = "Consent Form")]
    public byte[] ConsentForm { get; set; }

    [Display(Name = "Attach Referral Letter")]
    public byte[] AttachReferralLetter { get; set; }

    [Display(Name = "Treatment Plan")]
    [DataType(DataType.MultilineText)]
    public string TreatmentPlan { get; set; }

    [DataType(DataType.MultilineText)]
    public string Prognosis { get; set; }

    [Display(Name = "Vitality Tests")]
    [DataType(DataType.MultilineText)]
    public string VitalityTests { get; set; }

    [Display(Name = "Physician’s  Note")]
    [DataType(DataType.MultilineText)]
    public string PhysicianNote { get; set; }

    [Display(Name = "Area Radio graphed")]
    [DataType(DataType.MultilineText)]
    public string AreaRadiographed { get; set; }

    [Display(Name = "Investigations & Radiographic Interpretation")]
    [DataType(DataType.MultilineText)]
    public string RadiographicInterpretation { get; set; }

    public string Root { get; set; }

    [Display(Name = "Normal Land Marks")]
    [DataType(DataType.MultilineText)]
    public string NormalLandMarks { get; set; }

    [Display(Name = "Radiographic Fault(If Any)")]
    [DataType(DataType.MultilineText)]
    public string RadiographicFault { get; set; }

    [Display(Name = "Radiographic Diagnosis")]
    [DataType(DataType.MultilineText)]
    public string RadiographicDiagnosis { get; set; }

    [DataType(DataType.MultilineText)]
    public string Grade { get; set; }

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

    public int ReferredOthersId { get; set; }

    public PatientInformationViewModel patientInformationViewModel { get; set; }

    public BillingQueueServiceViewModel billingQueueViewModal { get; set; }

    public BillingQueueServiceViewModel billingLabRadQueueViewModal { get; set; }

    public StudentAllotmentViewModel studentAllotmentViewModel { get; set; }

    public FollowupViewModal followupViewModal { get; set; }

    public IEnumerable<FollowupViewModal> followupList { get; set; }

    public IEnumerable<StudentAllotmentViewModel> studentProcedureNotesViewModel { get; set; }

    public IEnumerable<BillingViewModal> paidInvestigationList { get; set; }

    public IEnumerable<BillingViewModal> deptTreatmentList { get; set; }

    public IEnumerable<BillingViewModal> paidLabRadInvestigationList { get; set; }

    public List<ReferralStatusViewModel> CreatedepartmentReferredStatus { get; set; }

    public List<ReferralStatusViewModel> ViewdepartmentReferredStatus { get; set; }

    public IEnumerable<BillingQueueServiceViewModel> BillingQueueDetails { get; set; }

    public IEnumerable<BillingQueueServiceViewModel> BillingLabRadQueueDetails { get; set; }

    public long TreatmentReferredId { get; set; }

    public IEnumerable<FileUploadViewModel> FileUploadlist { get; set; }

    public ReferredToOthersViewModel referredtoOthersViewModel { get; set; }

    public PrescriptionsViewModel PrescriptionsList { get; set; }

    public IEnumerable<PrescriptionsViewModel> PrescriptionsDetails { get; set; }

    public IEnumerable<PrescriptionsViewModel> PreviousPrescriptionList { get; set; }

    public MedicalAlertViewModel medicalalertviewmodel { get; set; }

    public ApprovalViewModal approvalViewModal { get; set; }

    public RadioRegistrationViewModel radioRegistrationviewmodel { get; set; }

    public LaboratoryRegistrationViewModel laboratoryRegistrationviewmodel { get; set; }

    [Display(Name = "Frequency")]
    public string Frequency { get; set; }

    [Display(Name = "Duration")]
    public string Duration { get; set; }

    [Display(Name = "Normal")]
    public bool rdnLabialNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnLabialAbnormal { get; set; }

    [Display(Name = "Normal")]
    public bool rdnBuccalNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnBuccalAbnormal { get; set; }

    [Display(Name = "Normal")]
    public bool rdnFloorMouthNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnFloorMouthAbnormal { get; set; }

    [Display(Name = "Normal")]
    public bool rdnVestibularNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnVestibularAbnormal { get; set; }

    [Display(Name = "Normal")]
    public bool rdnLingualNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnLingualAbnormal { get; set; }

    [Display(Name = "Normal")]
    public bool rdnPalatalNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnPalatalAbnormal { get; set; }

    [Display(Name = "Normal")]
    public bool rdnStainsNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnStainsAbnormal { get; set; }

    [Display(Name = "Normal")]
    public bool rdnCalculusNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnCalculusAbnormal { get; set; }

    [Display(Name = "Normal")]
    public bool rdnPlaqueNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnPlaqueAbnormal { get; set; }

    [Display(Name = "Normal")]
    public bool rdnGingivalEnlargementNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnGingivalEnlargementAbnormal { get; set; }

    [Display(Name = "Normal")]
    public bool rdnBleedingOnProbingNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnBleedingOnProbingAbnormal { get; set; }

    [Display(Name = "Normal")]
    public bool rdnPeriodontalPocketsNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnPeriodontalPocketsAbnormal { get; set; }

    [Display(Name = "Normal")]
    public bool rdnGingivalRecessionNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnGingivalRecessionAbnormal { get; set; }

    [Display(Name = "Normal")]
    public bool rdnFurcationInvolvementNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnFurcationInvolvementAbnormal { get; set; }

    [Display(Name = "Normal")]
    public bool rdnMucogingivalProblemNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnMucogingivalProblemAbnormal { get; set; }

    public IEnumerable<MASChiefComplaint> ChiefComplaintlist { get; set; }

    public IEnumerable<MASPresentIllness> PresentIllnesslist { get; set; }

    public IEnumerable<MASCode> ParafunctionalHabitslist { get; set; }

    public IEnumerable<MASCode> BrushingHabitsMethodlist { get; set; }

    public IEnumerable<MASCode> BrushingHabitsFreqlist { get; set; }

    public IEnumerable<MASCode> BrushingHabitsDurlist { get; set; }

    public IEnumerable<MASCode> ChangingBrushFreqlist { get; set; }

    public IEnumerable<MASCode> DentifriceTypelist { get; set; }

    public IEnumerable<MASCode> BrushTypelist { get; set; }

    public IEnumerable<MASCode> Consistencylist { get; set; }

    public IEnumerable<MASCode> Grouplist { get; set; }

    public IEnumerable<MASCode> OverBitelist { get; set; }

    public IEnumerable<MASCode> OverJetlist { get; set; }

    public IEnumerable<MASCode> MoralRelationshiplist { get; set; }

    public IEnumerable<MASCode> CanineRelationshiplist { get; set; }

    public IEnumerable<MASCode> SkeletaRelationshiplist { get; set; }

    public IEnumerable<MASCode> OcculsionMolarRelationlist { get; set; }

    public IEnumerable<MASCode> OcculsionCanineRelationlist { get; set; }

    public Diet Diet { get; set; }

    public IEnumerable<SelectListItem> DietLister { get; set; }

    public IEnumerable<OMRCasesheetProperties> Proplist { get; set; }

    public IEnumerable<OMRViewModel> Treatmentlist { get; set; }

    [Display(Name = "Date")]
    public string OMRDateDisplay { get; set; }

    private bool GetCheckBoxValue(string dependentField)
    {
      return !string.IsNullOrEmpty(dependentField);
    }

    public OMRViewModel()
    {
      if (this.DietLister != null)
        return;
      this.DietLister = ((IEnumerable<string>) Enum.GetNames(typeof (Diet))).Select<string, SelectListItem>((Func<string, SelectListItem>) (name => new SelectListItem()
      {
        Text = name,
        Value = name
      }));
    }

    public string BrushingHabitsMethod { get; set; }

    public string BrushType { get; set; }

    public string Consistency { get; set; }

    public string Group { get; set; }

    public string OverBite { get; set; }

    public string OverJet { get; set; }

    public string MoralRelationship { get; set; }

    public string Caninerelationship { get; set; }

    public string SkeletalRelationship { get; set; }

    public string OcculsionMolarRelation { get; set; }

    public string OcculsionCanineRelation { get; set; }

    public string ParafunctionalHabits { get; set; }

    public string DietName { get; set; }

    public string BrushingHabitsFreq { get; set; }

    public string BrushingHabitsDur { get; set; }

    public string ChangingBrushFreq { get; set; }

    public string DentifriceType { get; set; }

    public long OpNo { get; set; }

    public string PatientName { get; set; }

    public string Phone { get; set; }

    public string AgeGender { get; set; }

    public string Area { get; set; }

    public int Age { get; set; }

    public int GenderId { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }
  }
}
