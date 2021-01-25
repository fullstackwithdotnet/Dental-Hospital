// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.OMRPHDCasesheetViewModel
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
  public class OMRPHDCasesheetViewModel : EntityBase
  {
    [PrimaryKey]
    public int OMRId { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int PatientId { get; set; }

    public DateTime OMRDate { get; set; }

    [Display(Name = "Chief Complaint")]
    public string ChiefComplaintName { get; set; }

    [DataType(DataType.MultilineText)]
    public string ChiefComplaint { get; set; }

    [Display(Name = "Frequency")]
    public string Frequency { get; set; }

    [Display(Name = "Duration")]
    public string Duration { get; set; }

    [Display(Name = "Normal")]
    public bool rdnLipsNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnLipsAbnormal { get; set; }

    [Display(Name = "Normal")]
    public bool rdnLabialNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnLabialAbnormal { get; set; }

    [Display(Name = "Normal")]
    public bool rdnLossOfAttachmentNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnLossOfAttachmentAbnormal { get; set; }

    [Display(Name = "Normal")]
    public bool rdnFrenumAttachmentNormal { get; set; }

    [Display(Name = "Abnormal")]
    public bool rdnFrenumAttachmentAbnormal { get; set; }

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

    [Display(Name = "Day 1")]
    public string DayOneDiet { get; set; }

    [Display(Name = "Day 2")]
    public string DayTwoDiet { get; set; }

    [Display(Name = "Day 3")]
    public string DayThreeDiet { get; set; }

    [Display(Name = "Day 4")]
    public string DayFourDiet { get; set; }

    [Display(Name = "Day 5")]
    public string DayFiveDiet { get; set; }

    [Display(Name = "Day 6")]
    public string DaySixDiet { get; set; }

    [Display(Name = "Morning Breakfast 8AM - 9AM")]
    public string DayOneBreakFast { get; set; }

    public string DayTwoBreakFast { get; set; }

    public string DayThreeBreakFast { get; set; }

    public string DayFourBreakFast { get; set; }

    public string DayFiveBreakFast { get; set; }

    public string DaySixBreakFast { get; set; }

    [Display(Name = "In Between Meals 11AM - 12 NOON")]
    public string DayOneMorngSnacks { get; set; }

    public string DayTwoMorngSnacks { get; set; }

    public string DayThreeMorngSnacks { get; set; }

    public string DayFourMorngSnacks { get; set; }

    public string DayFiveMorngSnacks { get; set; }

    public string DaySixMorngSnacks { get; set; }

    [Display(Name = "Lunch 1PM - 2 PM")]
    public string DayOneLunch { get; set; }

    public string DayTwoLunch { get; set; }

    public string DayThreeLunch { get; set; }

    public string DayFourLunch { get; set; }

    public string DayFiveLunch { get; set; }

    public string DaySixLunch { get; set; }

    [Display(Name = "In Between Meals 4PM - 5 PM")]
    public string DayOneSnacks { get; set; }

    public string DayTwoSnacks { get; set; }

    public string DayThreeSnacks { get; set; }

    public string DayFourSnacks { get; set; }

    public string DayFiveSnacks { get; set; }

    public string DaySixSnacks { get; set; }

    [Display(Name = "Dinner 8PM - 9 PM")]
    public string DayOneDinner { get; set; }

    public string DayTwoDinner { get; set; }

    public string DayThreeDinner { get; set; }

    public string DayFourDinner { get; set; }

    public string DayFiveDinner { get; set; }

    public string DaySixDinner { get; set; }

    public string SolidAndStickyFirm { get; set; }

    public string SlowlyDissolvingFirm { get; set; }

    [Display(Name = "Frequency")]
    public string LiquidsFrequency { get; set; }

    public string SolidAndStickyFrequency { get; set; }

    public string SlowlyDissolvingFrequency { get; set; }

    [Display(Name = "Points")]
    public string LiquidsPoints { get; set; }

    public string SolidAndStickyPoints { get; set; }

    public string SlowlyDissolvingPoints { get; set; }

    [Display(Name = "Total Score")]
    public string TotalScore { get; set; }

    public string Inference { get; set; }

    [Display(Name = "Liquids")]
    public string Liquids { get; set; }

    [Display(Name = "Solid And Sticky")]
    public string SolidAndSticky { get; set; }

    [Display(Name = "Slowly Dissolving")]
    public string SlowlyDissolving { get; set; }

    [Display(Name = "Firm")]
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

    public IEnumerable<OMRCasesheetProperties> Proplist { get; set; }

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

    [Display(Name = "HIV RelatedSymptoms")]
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

    public string Profile { get; set; }

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

    [Display(Name = "Facial Symmetry")]
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

    public string CrossBite { get; set; }

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

    [Display(Name = "Others")]
    public string OcculsionMolarOthers { get; set; }

    [Display(Name = "Canine Relation")]
    public int? OcculsionCanineRelationId { get; set; }

    [Display(Name = "Others")]
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

    public string Gradel { get; set; }

    public string Gradell { get; set; }

    public string Gradelll { get; set; }

    [Display(Name = "Tender on Percussion")]
    public string TenderPercussion { get; set; }

    [Display(Name = "Others")]
    public string MobilityOthers { get; set; }

    public string Labial { get; set; }

    [Display(Name = "Frenum Attachment")]
    public string FrenumAttachment { get; set; }

    [Display(Name = "Loss Of Attachment")]
    public string LossOfAttachment { get; set; }

    public string Lips { get; set; }

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

    [Display(Name = "Bleeding On Probing")]
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

    [Display(Name = "Examination Of Specific lesion")]
    public string SpecificLesion { get; set; }

    [Display(Name = "Local Examination of chief complaint/Specific findings")]
    public string LocalExaminationchiefComplaint { get; set; }

    public string Inspection { get; set; }

    public string Palpation { get; set; }

    public string Percussion { get; set; }

    public string Auscultation { get; set; }

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

    [Display(Name = "Case Summary")]
    [DataType(DataType.MultilineText)]
    public string CaseSummary { get; set; }

    public Diet Diet { get; set; }

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

    public IEnumerable<SelectListItem> DietLister { get; set; }

    public OMRPHDCasesheetViewModel()
    {
      if (this.DietLister != null)
        return;
      this.DietLister = ((IEnumerable<string>) Enum.GetNames(typeof (Diet))).Select<string, SelectListItem>((Func<string, SelectListItem>) (name => new SelectListItem()
      {
        Text = name,
        Value = name
      }));
    }
  }
}
