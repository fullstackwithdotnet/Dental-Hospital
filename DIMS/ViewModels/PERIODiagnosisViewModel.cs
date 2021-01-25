// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.PERIODiagnosisViewModel
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
  public class PERIODiagnosisViewModel : EntityBase
  {
    private DateTime _PerioDate = DateTime.Now;

    public PERIODiagnosisViewModel()
    {
      if (this.DietLister != null)
        return;
      this.DietLister = ((IEnumerable<string>) Enum.GetNames(typeof (Diet))).Select<string, SelectListItem>((Func<string, SelectListItem>) (name => new SelectListItem()
      {
        Text = name,
        Value = name
      }));
    }

    [PrimaryKey]
    public int PerioId { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int PatientId { get; set; }

    public string MandatoryDummy { get; set; }

    public string PerioNo { get; set; }

    [Display(Name = "Perio.Date")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime PerioDate
    {
      get
      {
        return this._PerioDate;
      }
      set
      {
        this._PerioDate = value;
      }
    }

    [Display(Name = "Chief Complaint")]
    public string ChiefComplaintName { get; set; }

    [DataType(DataType.MultilineText)]
    public string ChiefComplaint { get; set; }

    [Display(Name = "History of Present Illness")]
    [DataType(DataType.MultilineText)]
    public string HistoryofPresentIllness { get; set; }

    [Display(Name = "Past Dental History")]
    [DataType(DataType.MultilineText)]
    public string PastDentalHistory { get; set; }

    [Display(Name = "Family History")]
    [DataType(DataType.MultilineText)]
    public string FamilyHistory { get; set; }

    [DataType(DataType.MultilineText)]
    [Display(Name = "Details with Drug History")]
    public string DetailsWithDrugHistory { get; set; }

    [Display(Name = "Pernicious Oral Habit")]
    public int? PerniciousOralHabitId { get; set; }

    public string AbbreviationOthers { get; set; }

    public string Allergy { get; set; }

    public string GIT { get; set; }

    public string CNS { get; set; }

    public string ENT { get; set; }

    public string Psychiatry { get; set; }

    public string Medication { get; set; }

    [Display(Name = "Skin Lesions")]
    public string SkinLesions { get; set; }

    [Display(Name = "Other Endocrinological")]
    public string OtherEndocrinological { get; set; }

    [Display(Name = "Trauma/Accidents")]
    public string TraumaAccidents { get; set; }

    [Display(Name = "Others")]
    public string HistoryOthers { get; set; }

    [Display(Name = "Bleeding Diathesis")]
    public string BleedingDiathesis { get; set; }

    public string Pregnancy { get; set; }

    [Display(Name = "Epileptic Seizures")]
    public string EpilepticSeizures { get; set; }

    public string Dermatology { get; set; }

    [Display(Name = "Rheumatic Fever")]
    public string RheumaticFever { get; set; }

    [Display(Name = "Heart Disease")]
    public string HeartDisease { get; set; }

    [Display(Name = "Diabetes Mellitus")]
    public string DiabetesMellitus { get; set; }

    public string Hypertension { get; set; }

    [Display(Name = "Drug Allergy")]
    public string DrugAllergy { get; set; }

    public string Respiratory { get; set; }

    [Display(Name = "STD and Urogenita")]
    public string STDAndUrogenita { get; set; }

    [Display(Name = "HIV Related Symptoms")]
    public string HIVRelatedSymptoms { get; set; }

    [Display(Name = "Diet")]
    public int DietId { get; set; }

    public Diet Diet { get; set; }

    [Display(Name = "Alcohol Intake")]
    public string AlcoholIntake { get; set; }

    public string Sleep { get; set; }

    [Display(Name = "Tobacco")]
    public string TobaccoDur { get; set; }

    public string TobaccoFreq { get; set; }

    [Display(Name = "Chewing")]
    public string ChewingDur { get; set; }

    public string ChewingFreq { get; set; }

    [Display(Name = "Smoking")]
    public string SmokingDur { get; set; }

    public string SmokingFreq { get; set; }

    [Display(Name = "Betel Nut")]
    public string BetelNutDur { get; set; }

    public string BetelNutFreq { get; set; }

    [Display(Name = "Pan")]
    public string PanDur { get; set; }

    public string PanFreq { get; set; }

    [Display(Name = "Gutka")]
    public string GutkaDur { get; set; }

    public string GutkaFreq { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int? BrushingHabitsMethodId { get; set; }

    [Display(Name = "Method")]
    public string BrushingHabitsMethodName { get; set; }

    public string BrushingHabitsMethodOthers { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int? BrushingHabitsFreqId { get; set; }

    [Display(Name = "Frequency")]
    public string BrushingHabitsFreqName { get; set; }

    public string BrushingHabitsFreqOthers { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int? BrushingHabitsDurId { get; set; }

    [Display(Name = "Duration")]
    public string BrushingHabitsDurName { get; set; }

    public string BrushingHabitsDurOthers { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int? ChangingBrushFreqId { get; set; }

    [Display(Name = "Frequency of changing Brush")]
    public string ChangingBrushFreqName { get; set; }

    public string ChangingBrushFreqOthers { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int? DentifriceTypeId { get; set; }

    [Display(Name = "Type of Dentifrice")]
    public string DentifriceTypeName { get; set; }

    public string DentifriceTypeOthers { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int? BrushTypeId { get; set; }

    [Display(Name = "Type of Brush")]
    public string BrushTypeName { get; set; }

    public string BrushTypeOthers { get; set; }

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

    [Display(Name = "Gingival Inflammation")]
    public string GingivalInflammation { get; set; }

    [Display(Name = "Gingival Enlargement")]
    public string GingivalEnlargement { get; set; }

    public string Pericorontis { get; set; }

    [Display(Name = "Stains and Calculus")]
    public string StainsAndCalculus { get; set; }

    [Display(Name = "Color of Gingival")]
    public string ColorOfGingival { get; set; }

    [Display(Name = "Bleeding on Probing")]
    public string BleedingOnProbing { get; set; }

    [Display(Name = "Size and Contour")]
    public string SizeAndContour { get; set; }

    [Display(Name = "Consistency")]
    public string Consistency { get; set; }

    [Display(Name = "Surface Texture")]
    public string SurfaceTexture { get; set; }

    [Display(Name = "Frenal attachment")]
    public string FrenalAttachment { get; set; }

    [Display(Name = "Vestibule")]
    public string Vistibule { get; set; }

    [Display(Name = "Width of attached Gingiva")]
    public string WidthOfAttachedGingival { get; set; }

    public string Pocket { get; set; }

    [Display(Name = "Furcation Grades")]
    public string FrucationGrades { get; set; }

    [Display(Name = "Mobility Grades")]
    public string MobilityGrades { get; set; }

    [Display(Name = "Recession Grades")]
    public string RecessionGrades { get; set; }

    [Display(Name = "No of Teeth Present")]
    public string NoOfTeetPresent { get; set; }

    [Display(Name = "Missing Teeth")]
    public string MissingTeeth { get; set; }

    [Display(Name = "Caries and Restored Teeth")]
    public string CariesAndRestoredTeeth { get; set; }

    public string Spacing { get; set; }

    [Display(Name = "Wasting Diseases")]
    public string WastingDiseases { get; set; }

    [Display(Name = "Type of Occlusion")]
    public string OcclusalAnalysisTypeOfOcclusion { get; set; }

    [Display(Name = "Fremitus Test")]
    public string OcclusalAnalysisFremitusTest { get; set; }

    [Display(Name = "Provisional Diagnosis")]
    public string Diagnosis { get; set; }

    [Display(Name = "Final Diagnosis")]
    public string FinalDiagnosis { get; set; }

    [Display(Name = "Overall Prognosis")]
    public string OverallPrognosis { get; set; }

    [Display(Name = "Individual Tooth Prognosis")]
    public string IndividualPrognosis { get; set; }

    [Display(Name = "Investigation Result")]
    public string InvestigationResult { get; set; }

    [Display(Name = "Emergency / Immediate Phase")]
    public string EmergencyPhase { get; set; }

    [Display(Name = "Educational Motivation")]
    public string NonSurgicalPhase { get; set; }

    [Display(Name = "Surgical Phase")]
    public string SurgicalPhase { get; set; }

    [Display(Name = "Restorative Phase")]
    public string RestorativePhase { get; set; }

    [Display(Name = "Maintenance Phase")]
    public string MaintenancePhase { get; set; }

    [Display(Name = "Others")]
    public string TreatmentPlanOthers { get; set; }

    [Display(Name = "Maintenance Phase")]
    public string OralProphylaxis { get; set; }

    [Display(Name = "Maintenance Phase")]
    public string Laser { get; set; }

    [Display(Name = "Etiotropic Phase")]
    public string Implants { get; set; }

    public string Frequency { get; set; }

    public string Duration { get; set; }

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

    public IEnumerable<MASCode> ParafunctionalHabitslist { get; set; }

    public IEnumerable<MASCode> BrushingHabitsMethodlist { get; set; }

    public IEnumerable<MASCode> BrushingHabitsFreqlist { get; set; }

    public IEnumerable<MASCode> BrushingHabitsDurlist { get; set; }

    public IEnumerable<MASCode> ChangingBrushFreqlist { get; set; }

    public IEnumerable<MASCode> DentifriceTypelist { get; set; }

    public IEnumerable<MASCode> BrushTypelist { get; set; }

    public IEnumerable<SelectListItem> DietLister { get; set; }

    public IEnumerable<PERIODiagnosisCasesheetProperties> Proplist { get; set; }

    public IEnumerable<ReferralStatusViewModel> ReferredPatientList { get; set; }

    public List<ReferralStatusViewModel> ApprovedepartmentReferral { get; set; }

    public int ReferredOthersId { get; set; }

    public PatientInformationViewModel patientInformationViewModel { get; set; }

    public BillingQueueServiceViewModel billingQueueViewModal { get; set; }

    public IEnumerable<BillingViewModal> deptTreatmentList { get; set; }

    public StudentAllotmentViewModel studentAllotmentViewModel { get; set; }

    public FollowupViewModal followupViewModal { get; set; }

    public IEnumerable<FollowupViewModal> followupList { get; set; }

    public IEnumerable<StudentAllotmentViewModel> studentProcedureNotesViewModel { get; set; }

    public IEnumerable<BillingViewModal> paidInvestigationList { get; set; }

    public List<ReferralStatusViewModel> CreatedepartmentReferredStatus { get; set; }

    public List<ReferralStatusViewModel> ViewdepartmentReferredStatus { get; set; }

    public IEnumerable<BillingQueueServiceViewModel> BillingQueueDetails { get; set; }

    public IEnumerable<FileUploadViewModel> FileUploadlist { get; set; }

    public ApprovalViewModal approvalViewModal { get; set; }

    public long TreatmentReferredId { get; set; }

    public PrescriptionsViewModel PrescriptionsList { get; set; }

    public IEnumerable<PrescriptionsViewModel> PrescriptionsDetails { get; set; }

    public IEnumerable<PrescriptionsViewModel> PreviousPrescriptionList { get; set; }

    public MedicalAlertViewModel medicalalertviewmodel { get; set; }

    public BillingQueueServiceViewModel billingLabRadQueueViewModal { get; set; }

    public IEnumerable<BillingViewModal> paidLabRadInvestigationList { get; set; }

    public IEnumerable<BillingQueueServiceViewModel> BillingLabRadQueueDetails { get; set; }

    public RadioRegistrationViewModel radioRegistrationviewmodel { get; set; }

    public LaboratoryRegistrationViewModel laboratoryRegistrationviewmodel { get; set; }

    public string GT18_1 { get; set; }

    public string GT18_2 { get; set; }

    public string GT18_3 { get; set; }

    public string GT18_4 { get; set; }

    public string GT17_1 { get; set; }

    public string GT17_2 { get; set; }

    public string GT17_3 { get; set; }

    public string GT17_4 { get; set; }

    public string GT16_1 { get; set; }

    public string GT16_2 { get; set; }

    public string GT16_3 { get; set; }

    public string GT16_4 { get; set; }

    public string GT15_1 { get; set; }

    public string GT15_2 { get; set; }

    public string GT15_3 { get; set; }

    public string GT15_4 { get; set; }

    public string GT14_1 { get; set; }

    public string GT14_2 { get; set; }

    public string GT14_3 { get; set; }

    public string GT14_4 { get; set; }

    public string GT13_1 { get; set; }

    public string GT13_2 { get; set; }

    public string GT13_3 { get; set; }

    public string GT13_4 { get; set; }

    public string GT12_1 { get; set; }

    public string GT12_2 { get; set; }

    public string GT12_3 { get; set; }

    public string GT12_4 { get; set; }

    public string GT11_1 { get; set; }

    public string GT11_2 { get; set; }

    public string GT11_3 { get; set; }

    public string GT11_4 { get; set; }

    public string GT21_1 { get; set; }

    public string GT21_2 { get; set; }

    public string GT21_3 { get; set; }

    public string GT21_4 { get; set; }

    public string GT22_1 { get; set; }

    public string GT22_2 { get; set; }

    public string GT22_3 { get; set; }

    public string GT22_4 { get; set; }

    public string GT23_1 { get; set; }

    public string GT23_2 { get; set; }

    public string GT23_3 { get; set; }

    public string GT23_4 { get; set; }

    public string GT24_1 { get; set; }

    public string GT24_2 { get; set; }

    public string GT24_3 { get; set; }

    public string GT24_4 { get; set; }

    public string GT25_1 { get; set; }

    public string GT25_2 { get; set; }

    public string GT25_3 { get; set; }

    public string GT25_4 { get; set; }

    public string GT26_1 { get; set; }

    public string GT26_2 { get; set; }

    public string GT26_3 { get; set; }

    public string GT26_4 { get; set; }

    public string GT27_1 { get; set; }

    public string GT27_2 { get; set; }

    public string GT27_3 { get; set; }

    public string GT27_4 { get; set; }

    public string GT28_1 { get; set; }

    public string GT28_2 { get; set; }

    public string GT28_3 { get; set; }

    public string GT28_4 { get; set; }

    public string GT48_1 { get; set; }

    public string GT48_2 { get; set; }

    public string GT48_3 { get; set; }

    public string GT48_4 { get; set; }

    public string GT47_1 { get; set; }

    public string GT47_2 { get; set; }

    public string GT47_3 { get; set; }

    public string GT47_4 { get; set; }

    public string GT46_1 { get; set; }

    public string GT46_2 { get; set; }

    public string GT46_3 { get; set; }

    public string GT46_4 { get; set; }

    public string GT45_1 { get; set; }

    public string GT45_2 { get; set; }

    public string GT45_3 { get; set; }

    public string GT45_4 { get; set; }

    public string GT44_1 { get; set; }

    public string GT44_2 { get; set; }

    public string GT44_3 { get; set; }

    public string GT44_4 { get; set; }

    public string GT43_1 { get; set; }

    public string GT43_2 { get; set; }

    public string GT43_3 { get; set; }

    public string GT43_4 { get; set; }

    public string GT42_1 { get; set; }

    public string GT42_2 { get; set; }

    public string GT42_3 { get; set; }

    public string GT42_4 { get; set; }

    public string GT41_1 { get; set; }

    public string GT41_2 { get; set; }

    public string GT41_3 { get; set; }

    public string GT41_4 { get; set; }

    public string GT31_1 { get; set; }

    public string GT31_2 { get; set; }

    public string GT31_3 { get; set; }

    public string GT31_4 { get; set; }

    public string GT32_1 { get; set; }

    public string GT32_2 { get; set; }

    public string GT32_3 { get; set; }

    public string GT32_4 { get; set; }

    public string GT33_1 { get; set; }

    public string GT33_2 { get; set; }

    public string GT33_3 { get; set; }

    public string GT33_4 { get; set; }

    public string GT34_1 { get; set; }

    public string GT34_2 { get; set; }

    public string GT34_3 { get; set; }

    public string GT34_4 { get; set; }

    public string GT35_1 { get; set; }

    public string GT35_2 { get; set; }

    public string GT35_3 { get; set; }

    public string GT35_4 { get; set; }

    public string GT36_1 { get; set; }

    public string GT36_2 { get; set; }

    public string GT36_3 { get; set; }

    public string GT36_4 { get; set; }

    public string GT37_1 { get; set; }

    public string GT37_2 { get; set; }

    public string GT37_3 { get; set; }

    public string GT37_4 { get; set; }

    public string GT38_1 { get; set; }

    public string GT38_2 { get; set; }

    public string GT38_3 { get; set; }

    public string GT38_4 { get; set; }

    public Decimal? GITotal { get; set; }

    public string Total { get; set; }

    public Decimal? CITotal { get; set; }

    public Decimal? DITotal { get; set; }

    public Decimal? OHITotal { get; set; }

    public Decimal? DIUpperTotal { get; set; }

    public Decimal? DILowerTotal { get; set; }

    public Decimal? CIUpperTotal { get; set; }

    public Decimal? CILowerTotal { get; set; }

    [Display(Name = "16")]
    public string OT16 { get; set; }

    [Display(Name = "11")]
    public string OT11 { get; set; }

    [Display(Name = "26")]
    public string OT26 { get; set; }

    [Display(Name = "46")]
    public string OT46 { get; set; }

    [Display(Name = "31")]
    public string OT31 { get; set; }

    [Display(Name = "36")]
    public string OT36 { get; set; }

    public string DIT_1 { get; set; }

    public string DIT_2 { get; set; }

    public string DIT_3 { get; set; }

    public string DIT_4 { get; set; }

    public string DIT_5 { get; set; }

    public string DIT_6 { get; set; }

    public string CIT_1 { get; set; }

    public string CIT_2 { get; set; }

    public string CIT_3 { get; set; }

    public string CIT_4 { get; set; }

    public string CIT_5 { get; set; }

    public string CIT_6 { get; set; }

    public string T18_1 { get; set; }

    public string T18_2 { get; set; }

    public string T18_3 { get; set; }

    public string T18_4 { get; set; }

    public string T18_5 { get; set; }

    public string T18_6 { get; set; }

    public string T17_1 { get; set; }

    public string T17_2 { get; set; }

    public string T17_3 { get; set; }

    public string T17_4 { get; set; }

    public string T17_5 { get; set; }

    public string T17_6 { get; set; }

    public string T16_1 { get; set; }

    public string T16_2 { get; set; }

    public string T16_3 { get; set; }

    public string T16_4 { get; set; }

    public string T16_5 { get; set; }

    public string T16_6 { get; set; }

    public string T15_1 { get; set; }

    public string T15_2 { get; set; }

    public string T15_3 { get; set; }

    public string T15_4 { get; set; }

    public string T15_5 { get; set; }

    public string T15_6 { get; set; }

    public string T14_1 { get; set; }

    public string T14_2 { get; set; }

    public string T14_3 { get; set; }

    public string T14_4 { get; set; }

    public string T14_5 { get; set; }

    public string T14_6 { get; set; }

    public string T13_1 { get; set; }

    public string T13_2 { get; set; }

    public string T13_3 { get; set; }

    public string T13_4 { get; set; }

    public string T13_5 { get; set; }

    public string T13_6 { get; set; }

    public string T12_1 { get; set; }

    public string T12_2 { get; set; }

    public string T12_3 { get; set; }

    public string T12_4 { get; set; }

    public string T12_5 { get; set; }

    public string T12_6 { get; set; }

    public string T11_1 { get; set; }

    public string T11_2 { get; set; }

    public string T11_3 { get; set; }

    public string T11_4 { get; set; }

    public string T11_5 { get; set; }

    public string T11_6 { get; set; }

    public string T21_1 { get; set; }

    public string T21_2 { get; set; }

    public string T21_3 { get; set; }

    public string T21_4 { get; set; }

    public string T21_5 { get; set; }

    public string T21_6 { get; set; }

    public string T22_1 { get; set; }

    public string T22_2 { get; set; }

    public string T22_3 { get; set; }

    public string T22_4 { get; set; }

    public string T22_5 { get; set; }

    public string T22_6 { get; set; }

    public string T23_1 { get; set; }

    public string T23_2 { get; set; }

    public string T23_3 { get; set; }

    public string T23_4 { get; set; }

    public string T23_5 { get; set; }

    public string T23_6 { get; set; }

    public string T24_1 { get; set; }

    public string T24_2 { get; set; }

    public string T24_3 { get; set; }

    public string T24_4 { get; set; }

    public string T24_5 { get; set; }

    public string T24_6 { get; set; }

    public string T25_1 { get; set; }

    public string T25_2 { get; set; }

    public string T25_3 { get; set; }

    public string T25_4 { get; set; }

    public string T25_5 { get; set; }

    public string T25_6 { get; set; }

    public string T26_1 { get; set; }

    public string T26_2 { get; set; }

    public string T26_3 { get; set; }

    public string T26_4 { get; set; }

    public string T26_5 { get; set; }

    public string T26_6 { get; set; }

    public string T27_1 { get; set; }

    public string T27_2 { get; set; }

    public string T27_3 { get; set; }

    public string T27_4 { get; set; }

    public string T27_5 { get; set; }

    public string T27_6 { get; set; }

    public string T28_1 { get; set; }

    public string T28_2 { get; set; }

    public string T28_3 { get; set; }

    public string T28_4 { get; set; }

    public string T28_5 { get; set; }

    public string T28_6 { get; set; }

    public string T48_1 { get; set; }

    public string T48_2 { get; set; }

    public string T48_3 { get; set; }

    public string T48_4 { get; set; }

    public string T48_5 { get; set; }

    public string T48_6 { get; set; }

    public string T47_1 { get; set; }

    public string T47_2 { get; set; }

    public string T47_3 { get; set; }

    public string T47_4 { get; set; }

    public string T47_5 { get; set; }

    public string T47_6 { get; set; }

    public string T46_1 { get; set; }

    public string T46_2 { get; set; }

    public string T46_3 { get; set; }

    public string T46_4 { get; set; }

    public string T46_5 { get; set; }

    public string T46_6 { get; set; }

    public string T45_1 { get; set; }

    public string T45_2 { get; set; }

    public string T45_3 { get; set; }

    public string T45_4 { get; set; }

    public string T45_5 { get; set; }

    public string T45_6 { get; set; }

    public string T44_1 { get; set; }

    public string T44_2 { get; set; }

    public string T44_3 { get; set; }

    public string T44_4 { get; set; }

    public string T44_5 { get; set; }

    public string T44_6 { get; set; }

    public string T43_1 { get; set; }

    public string T43_2 { get; set; }

    public string T43_3 { get; set; }

    public string T43_4 { get; set; }

    public string T43_5 { get; set; }

    public string T43_6 { get; set; }

    public string T42_1 { get; set; }

    public string T42_2 { get; set; }

    public string T42_3 { get; set; }

    public string T42_4 { get; set; }

    public string T42_5 { get; set; }

    public string T42_6 { get; set; }

    public string T41_1 { get; set; }

    public string T41_2 { get; set; }

    public string T41_3 { get; set; }

    public string T41_4 { get; set; }

    public string T41_5 { get; set; }

    public string T41_6 { get; set; }

    public string T31_1 { get; set; }

    public string T31_2 { get; set; }

    public string T31_3 { get; set; }

    public string T31_4 { get; set; }

    public string T31_5 { get; set; }

    public string T31_6 { get; set; }

    public string T32_1 { get; set; }

    public string T32_2 { get; set; }

    public string T32_3 { get; set; }

    public string T32_4 { get; set; }

    public string T32_5 { get; set; }

    public string T32_6 { get; set; }

    public string T33_1 { get; set; }

    public string T33_2 { get; set; }

    public string T33_3 { get; set; }

    public string T33_4 { get; set; }

    public string T33_5 { get; set; }

    public string T33_6 { get; set; }

    public string T34_1 { get; set; }

    public string T34_2 { get; set; }

    public string T34_3 { get; set; }

    public string T34_4 { get; set; }

    public string T34_5 { get; set; }

    public string T34_6 { get; set; }

    public string T35_1 { get; set; }

    public string T35_2 { get; set; }

    public string T35_3 { get; set; }

    public string T35_4 { get; set; }

    public string T35_5 { get; set; }

    public string T35_6 { get; set; }

    public string T36_1 { get; set; }

    public string T36_2 { get; set; }

    public string T36_3 { get; set; }

    public string T36_4 { get; set; }

    public string T36_5 { get; set; }

    public string T36_6 { get; set; }

    public string T37_1 { get; set; }

    public string T37_2 { get; set; }

    public string T37_3 { get; set; }

    public string T37_4 { get; set; }

    public string T37_5 { get; set; }

    public string T37_6 { get; set; }

    public string T38_1 { get; set; }

    public string T38_2 { get; set; }

    public string T38_3 { get; set; }

    public string T38_4 { get; set; }

    public string T38_5 { get; set; }

    public string T38_6 { get; set; }

    public IEnumerable<PERIODiagnosisViewModel> Treatmentlist { get; set; }

    [Display(Name = "Date")]
    public string PerioDateDisplay { get; set; }

    [Display(Name = "Method")]
    public string BrushingHabitsMethod { get; set; }

    [Display(Name = "Frequency")]
    public string BrushingHabitsFreq { get; set; }

    [Display(Name = "Duration")]
    public string BrushingHabitsDur { get; set; }

    [Display(Name = "Frequency of changing Brush")]
    public string ChangingBrushFreq { get; set; }

    [Display(Name = "Type of Dentifrice")]
    public string DentifriceType { get; set; }

    [Display(Name = "Type of Brush")]
    public string BrushType { get; set; }

    [Display(Name = "Pernicious Oral Habit")]
    public string PerniciousOralHabit { get; set; }

    public string DietName { get; set; }

    [Display(Name = "Interdental aids")]
    public string InterdentalAids { get; set; }

    [Display(Name = "Use of Mouthrinses")]
    public string UseofMouthrinses { get; set; }

    public string Bruxism { get; set; }

    public string Clenching { get; set; }

    [Display(Name = "Lip/Cheek/FingerNail/Tounge Biting")]
    public string FingerNail { get; set; }

    [Display(Name = "Tooth pick/Object Wedging")]
    public string ObjectWedging { get; set; }

    [Display(Name = "Tongue Thrusting")]
    public string TongueThrusting { get; set; }

    [Display(Name = " Mouth Breathing")]
    public string MouthBreathing { get; set; }

    [Display(Name = "Thumb Sucking")]
    public string ThumbSucking { get; set; }

    [Display(Name = "Built")]
    public string Built { get; set; }

    public string Eyes { get; set; }

    public string Lips { get; set; }

    [Display(Name = "Jaw Symmetry")]
    public string JawSymmetry { get; set; }

    [Display(Name = "Lymph Nodes")]
    public string LymphNodes { get; set; }

    [Display(Name = " Tempero Mandibular Joint")]
    public string TemperoMandibularJoint { get; set; }

    [Display(Name = "Buccal / Labial Mucosa")]
    public string BuccalMucosa { get; set; }

    [Display(Name = "Tongue/Floor of the Mouth")]
    public string FlooroftheMouth { get; set; }

    [Display(Name = "Palate / Vestibules")]
    public string PalateVestibules { get; set; }

    [Display(Name = "Arch Form")]
    public string ArchForm { get; set; }

    [Display(Name = "Food Impaction")]
    public string FoodImpaction { get; set; }

    public string Halitosis { get; set; }

    public string Exudation { get; set; }

    [Display(Name = "Any Others")]
    public string OthersHabitsFreq { get; set; }

    public string OthersHabitsDur { get; set; }

    [Display(Name = "Marginal ridge incompetency")]
    public string MarginalRidgeIncompetancy { get; set; }

    [Display(Name = "Food Impaction")]
    public string FoodImpactionHard { get; set; }

    [Display(Name = "Halitosis")]
    public string HalitosisHard { get; set; }

    [Display(Name = "Faulty Restoration")]
    public string FaultyRestoration { get; set; }

    [Display(Name = "Stains/Discoloration")]
    public string StainsDiscoloration { get; set; }

    [Display(Name = "Calculus")]
    public string Calculus { get; set; }

    [Display(Name = "Tooth Number & Grade of Involvement")]
    public string GradeofInvolvementFurcation { get; set; }

    [Display(Name = "Tooth Number & Grade of Involvement")]
    public string GradeofInvolvementMobility { get; set; }

    public string Attrition { get; set; }

    public string Abrasion { get; set; }

    public string Erosion { get; set; }

    [Display(Name = "Molar Relationship")]
    public string MolarRelationship { get; set; }

    public string Overbite { get; set; }

    public string Overjet { get; set; }

    public string Openbite { get; set; }

    public string Crossbite { get; set; }

    [Display(Name = "Premature contact")]
    public string PrematureContact { get; set; }

    [Display(Name = "Marginal ridges")]
    public string MarginalRidges { get; set; }

    [Display(Name = "Plunger cusps")]
    public string PlungerCusps { get; set; }

    [Display(Name = "Trauma from occlusion(Primary/Secondary)")]
    public string TraumaOcclusion { get; set; }

    [Display(Name = "Pathologic Migration")]
    public string PathologicMigration { get; set; }

    [Display(Name = "Proximal Contact")]
    public string ProximalContact { get; set; }

    public string Rotation { get; set; }

    [Display(Name = "Faulty Restoration")]
    public string FaultyRestorationOcclusal { get; set; }

    public string Malalignment { get; set; }

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
