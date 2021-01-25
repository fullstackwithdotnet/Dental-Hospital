// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.PEDOViewModel
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Metron.Entities;

namespace DIMS.ViewModels
{
  [Table("PEDOCasesheet")]
  public class PEDOViewModel : EntityBase
  {
    private DateTime _LastTreatmentDate = DateTime.Now;

    [PrimaryKey]
    public int PEDOId { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int PatientId { get; set; }

    public string MandatoryDummy { get; set; }

    public string PEDONo { get; set; }

    public DateTime PEDODate { get; set; }

    [Display(Name = "Chief Complaint")]
    public string ChiefComplaintName { get; set; }

    [DataType(DataType.MultilineText)]
    [Display(Name = "Chief Complaint")]
    public string ChiefComplaint { get; set; }

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

    public string AbbreviationOthers { get; set; }

    [Display(Name = "History of present illness")]
    public string PresentIllnessHistory { get; set; }

    public string PastMedicalHistory { get; set; }

    public string PastDentalHistory { get; set; }

    [Display(Name = "Original site of pain")]
    public string POriginalSitePain { get; set; }

    [Display(Name = "Origin and mode of onset")]
    public string POriginAndModeOnset { get; set; }

    [Display(Name = "Severity")]
    public string PSeverity { get; set; }

    [Display(Name = "Nature of pain")]
    public string PPainNature { get; set; }

    [Display(Name = "Proggression of pain")]
    public string PPainProggression { get; set; }

    [Display(Name = "Duration of pain")]
    public string PPainDuration { get; set; }

    [Display(Name = "Movement of pain")]
    public string PPainMovement { get; set; }

    [Display(Name = "Periodicity of pain")]
    public string PPainPeriodicity { get; set; }

    [Display(Name = "Effect of functional activity")]
    public string PFunctionalActivityEffect { get; set; }

    [Display(Name = "Precipitating factors")]
    public string PPrecipitatingFactors { get; set; }

    [Display(Name = "Relieving factors")]
    public string PRelievingFactors { get; set; }

    [Display(Name = "Associated symptoms")]
    public string PAssociatedSymptoms { get; set; }

    [Display(Name = "Onset")]
    public string SOnset { get; set; }

    [Display(Name = "Type")]
    public string SType { get; set; }

    [Display(Name = "Duration")]
    public string SDuration { get; set; }

    [Display(Name = "Proggression")]
    public string SProggression { get; set; }

    [Display(Name = "Site")]
    public string SSite { get; set; }

    [Display(Name = "Size")]
    public string SSize { get; set; }

    [Display(Name = "Severity")]
    public string SSeverity { get; set; }

    [Display(Name = "Aggravating factors")]
    public string SAggravatingFactors { get; set; }

    [Display(Name = "Relieving factors")]
    public string SRelievingFactors { get; set; }

    [Display(Name = "Associated symptoms")]
    public string SAssociatedSymptoms { get; set; }

    [Display(Name = "Any secondary changes")]
    public string SSecondaryChanges { get; set; }

    [Display(Name = "Reccurence of swelling")]
    public string SSwellingReccurence { get; set; }

    [Display(Name = "Have your child ever been advised to take antibiotics before dental treatment?")]
    public string AntibioticsBeforeTreatDesc { get; set; }

    [Display(Name = "Have your child had prolonged bleeding following extractions in the past?")]
    public string ProlongedBleedingPastDesc { get; set; }

    public string FirstDentalVisitDesc { get; set; }

    [Display(Name = "What was done")]
    public string TreatmentDone { get; set; }

    [Display(Name = "Has your child ever had any unfavourable dental experience?")]
    public string UnfavourableDentalExpDesc { get; set; }

    [Display(Name = "Have your child ever had an allergic reaction to “freezing”?")]
    public string FreezingAllergicDesc { get; set; }

    [Display(Name = "Have your child had any history of trauma?")]
    public string HistoryOfTraumaDesc { get; set; }

    [Display(Name = "Is the child under any physician care currently?")]
    public string PhysiciansCareDesc { get; set; }

    public string MedicationTakenDesc { get; set; }

    [Display(Name = "Dose")]
    public string MedicationDose { get; set; }

    [Display(Name = "Duration")]
    public string MedicationDuration { get; set; }

    [Display(Name = "Drug")]
    public string MedicationDrug { get; set; }

    [Display(Name = "Is the child suffering from any mental disability?")]
    public string MentalDisabilityDesc { get; set; }

    public string AllergyHistoryDesc { get; set; }

    [Display(Name = "Drug")]
    public string DrugDesc { get; set; }

    [Display(Name = "Food")]
    public string FoodDesc { get; set; }

    [Display(Name = "Inhalent")]
    public string InhalentDesc { get; set; }

    [Display(Name = "Does your child suffer from frequent illness such as cold/cough?")]
    public string FrequentIllnessDesc { get; set; }

    [Display(Name = "Was your child hospitalized for any reasons?")]
    public string HospitalizedDesc { get; set; }

    [Display(Name = "Was your child underwent for any kind of surgical treatment?")]
    public string SurgicalTreatmentDesc { get; set; }

    [Display(Name = "Has your child suffered from any of the childhood disease?")]
    public string ChildhoodDiseasesDesc { get; set; }

    public bool chkMumps { get; set; }

    [Display(Name = "Mumps")]
    public string Mumps { get; set; }

    public bool chkDiabetes { get; set; }

    [Display(Name = "Diabetes")]
    public string Diabetes { get; set; }

    public bool chkSpeechDefects { get; set; }

    [Display(Name = "Speech defects")]
    public string SpeechDefects { get; set; }

    public bool chkCongenitalHeartDisease { get; set; }

    [Display(Name = "Congenital heart disease")]
    public string CongenitalHeartDisease { get; set; }

    public bool chkMeasles { get; set; }

    [Display(Name = "Measles")]
    public string Measles { get; set; }

    public bool chkHepatitis { get; set; }

    [Display(Name = "Hepatitis")]
    public string Hepatitis { get; set; }

    public bool chkHearingDefects { get; set; }

    [Display(Name = "Hearing defects")]
    public string HearingDefects { get; set; }

    public bool chkRheumaticFever { get; set; }

    [Display(Name = "Rheumatic fever")]
    public string RheumaticFever { get; set; }

    public bool chkAsthma { get; set; }

    [Display(Name = "Asthma")]
    public string Asthma { get; set; }

    public bool chkEpilepsy { get; set; }

    [Display(Name = "Epilepsy")]
    public string Epilepsy { get; set; }

    public bool chkSensoryDefects { get; set; }

    [Display(Name = "Sensory defects")]
    public string SensoryDefects { get; set; }

    public bool chkBleedingDisorders { get; set; }

    [Display(Name = "Bleeding disorder")]
    public string BleedingDisorders { get; set; }

    public bool chkHiv { get; set; }

    [Display(Name = "Hiv")]
    public string Hiv { get; set; }

    public string SystemicInvolvements { get; set; }

    [Display(Name = "At what age the tooth brushing was initiated")]
    public string ToothBrushingInitiatedAge { get; set; }

    [Display(Name = "Finger")]
    public string FingerBrushMethod { get; set; }

    [Display(Name = "Other")]
    public string OtherBrushingMethod { get; set; }

    public string OtherBrushingMethodDesc { get; set; }

    public string SupervisedBrushingDesc { get; set; }

    [Display(Name = "What is the frequency of brushing?")]
    public int? BrushFrequencyId { get; set; }

    [Display(Name = "Night before bed time")]
    public string NightBeforeBedTime { get; set; }

    [Display(Name = "After meals")]
    public string AfterMeals { get; set; }

    [Display(Name = "Times")]
    public string NoOfTimes { get; set; }

    [Display(Name = "Duration")]
    public string Duration { get; set; }

    [Display(Name = "None")]
    public string OralHabitNone { get; set; }

    [Display(Name = "Finger/ thumb sucking")]
    public string FingerThumbFrequency { get; set; }

    public string FingerThumbIntensity { get; set; }

    public string FingerThumbDuration { get; set; }

    public string FingerThumbFingerNo { get; set; }

    public string FingerThumbAgeStop { get; set; }

    [Display(Name = "Tongue biting/thrusting")]
    public string TongueBitingFrequency { get; set; }

    public string TongueBitingIntensity { get; set; }

    public string TongueBitingDuration { get; set; }

    [Display(Name = "Bruxism")]
    public string BruxismFrequency { get; set; }

    public string BruxismIntensity { get; set; }

    public string BruxismDuration { get; set; }

    [Display(Name = "Mouth breathing")]
    public string MouthBreathingFrequency { get; set; }

    public string MouthBreathingIntensity { get; set; }

    public string MouthBreathingDuration { get; set; }

    [Display(Name = "Nail biting")]
    public string NailBiting { get; set; }

    [Display(Name = "Lip biting")]
    public string LipBiting { get; set; }

    [Display(Name = "Cheek biting")]
    public string CheekBiting { get; set; }

    [Display(Name = "Medical problems (Familial/Genetic)")]
    public string MedicalProblemFgDesc { get; set; }

    [Display(Name = "Dental problems (Familial/Genetic)")]
    public string DentalProblemsDesc { get; set; }

    [Display(Name = "Condition of mother during pregnancy?")]
    public int? ConditionOfMotherId { get; set; }

    [Display(Name = "Source of information?")]
    public int? InformerId { get; set; }

    [Display(Name = "Patient")]
    public string Patient { get; set; }

    [Display(Name = "Father")]
    public string Father { get; set; }

    [Display(Name = "Mother")]
    public string Mother { get; set; }

    public bool chkAnyOther { get; set; }

    [Display(Name = "Any other")]
    public string AnyOther { get; set; }

    [Display(Name = "ExtraOralLipPostToneDesc")]
    public string ExtraOralLipPostToneDesc { get; set; }

    [Display(Name = "Fair")]
    public string CoMFair { get; set; }

    [Display(Name = "Poor")]
    public string CoMPoor { get; set; }

    [Display(Name = "Significant history")]
    public string HospitalizationDesc { get; set; }

    [Display(Name = "Trauma")]
    public string TraumaDesc { get; set; }

    [Display(Name = "Any other significant history please specify")]
    public string SignicficantHistoryDesc { get; set; }

    [Display(Name = "Whether the delivery was")]
    public int? FullPrematureId { get; set; }

    [Display(Name = "Illness")]
    public string IllnessDesc { get; set; }

    public string Details { get; set; }

    public string Comments { get; set; }

    public string Reason { get; set; }

    [Display(Name = "Any Medication taken by the mother during Pregnancy")]
    public string PregnancyMedicationDesc { get; set; }

    public bool chkVitamins { get; set; }

    [Display(Name = "Vitamins")]
    public string Vitamins { get; set; }

    public bool chkAnalgesics { get; set; }

    [Display(Name = "Analgesics")]
    public string Analgesics { get; set; }

    public bool chkOthers { get; set; }

    [Display(Name = "Others")]
    public string Others { get; set; }

    public bool chkCalcium { get; set; }

    [Display(Name = "Calcium")]
    public string Calcium { get; set; }

    public bool chkAntibiotics { get; set; }

    [Display(Name = "Antibiotics")]
    public string Antibiotics { get; set; }

    [Display(Name = "Full term")]
    public bool rdnNatalFullterm { get; set; }

    [Display(Name = "Premature")]
    public bool rdnNatalPremature { get; set; }

    [Display(Name = "Delivery type")]
    public int? NormalCsectionId { get; set; }

    [Display(Name = "Normal")]
    public bool rdnNormalDelivery { get; set; }

    [Display(Name = "C section type")]
    public bool rdnCsectionTypeDelivery { get; set; }

    [Display(Name = "Forceps delivery")]
    public bool rdnForcepsDelivery { get; set; }

    [Display(Name = "Good")]
    public string BirthConditionGood { get; set; }

    [Display(Name = "Fair")]
    public string BirthConditionFair { get; set; }

    [Display(Name = "Poor")]
    public string BirthConditionPoor { get; set; }

    [Display(Name = "Was he/she jaundiced at birth, blue baby RH incompitability")]
    public string BlueBabyRhIncompDesc { get; set; }

    [Display(Name = "Significant History")]
    public string SignicficantHistory { get; set; }

    [Display(Name = "What was the type of feeding practiced?")]
    public string FeedingTypePracticed { get; set; }

    [Display(Name = "Breast Fed")]
    public string BreastFed { get; set; }

    [Display(Name = "Duration")]
    public string BreastFedDuration { get; set; }

    [Display(Name = "Duration")]
    public string BottleFedDuration { get; set; }

    [Display(Name = "Frequency")]
    public string BottleFedFrequency { get; set; }

    [Display(Name = "Duration")]
    public string CombinedFedDuration { get; set; }

    [Display(Name = "Frequency")]
    public string BreastFedFrequency { get; set; }

    [Display(Name = "Bottle Fed")]
    public string BottleFed { get; set; }

    [Display(Name = "Combined Fed")]
    public string CombinedFed { get; set; }

    [Display(Name = "Frequency")]
    public string CombinedFedFrequency { get; set; }

    public string SleepWithBottleDesc { get; set; }

    [Display(Name = "What were the contents of the bottle?")]
    public string BottleContents { get; set; }

    [Display(Name = "Type")]
    public string PacifierUsedTypeDesc { get; set; }

    [Display(Name = "Duration")]
    public string PacifierUsedDurationDesc { get; set; }

    [Display(Name = "Other Details")]
    public string PacifierUsedOtherDesc { get; set; }

    public bool chkEruptedToothAtBirthDesc { get; set; }

    [Display(Name = "At birth")]
    public string EruptedToothAtBirthDesc { get; set; }

    public bool chkEruptedToothThirtyDaysDesc { get; set; }

    [Display(Name = "Within 30 days of birth")]
    public string EruptedToothThirtyDaysDesc { get; set; }

    public string FirstEruptedToothAge { get; set; }

    [Display(Name = "Rating")]
    public string Rating { get; set; }

    [Display(Name = "Frankel's behaviour assessment")]
    public string FrankelsBehaviour { get; set; }

    [Display(Name = "Positive(+)")]
    public string RatePositive { get; set; }

    [Display(Name = "CheekBitingDuration")]
    public string CheekBitingDuration { get; set; }

    [Display(Name = "Negative(-)")]
    public string RateNegative { get; set; }

    [Display(Name = "What was the method of tooth brushing?")]
    public int? ToothBrushMethodId { get; set; }

    public string BalancedDietScoreDesc { get; set; }

    public string NoOfSolidExpo { get; set; }

    public string NoOfLiquidExpo { get; set; }

    public string OralHabitIntenisty { get; set; }

    public string OralHabitFrequency { get; set; }

    public string OralHabitDuration { get; set; }

    [Display(Name = "Built")]
    public string GeneralExamBuilt { get; set; }

    [Display(Name = "Gait")]
    public string GeneralExamGait { get; set; }

    [Display(Name = "Speech")]
    public string GeneralExamSpeech { get; set; }

    [Display(Name = "Height")]
    public string GeneralExamHeight { get; set; }

    [Display(Name = "Weight")]
    public string GeneralExamWeight { get; set; }

    [Display(Name = "Stature")]
    public string GeneralExamStature { get; set; }

    [Display(Name = "Temp")]
    public string GeneralExamTemp { get; set; }

    [Display(Name = "Blood Pressure")]
    public string GeneralExamBp { get; set; }

    [Display(Name = "Pulse rate")]
    public string GeneralExamPr { get; set; }

    [Display(Name = "Respiration rate")]
    public string GeneralExamRr { get; set; }

    public string ExtraOralFaceProfileDesc { get; set; }

    public string ExtraOralFaceSymmetry { get; set; }

    public string ExtraOralTmjDesc { get; set; }

    public string ExtraOralFacialFormDesc { get; set; }

    [Display(Name = "Hair and skin")]
    public string ExtraOralHairSkin { get; set; }

    [Display(Name = "Eye")]
    public string ExtraOralEye { get; set; }

    [Display(Name = "Ears")]
    public string ExtraOralEars { get; set; }

    [Display(Name = "Nose")]
    public string ExtraOralNose { get; set; }

    [Display(Name = "Chin")]
    public string ExtraOralChin { get; set; }

    [Display(Name = "Bleeding on probing")]
    public string SoftTissueBleedingProbing { get; set; }

    [Display(Name = "Palate")]
    public string SoftTissuePalate { get; set; }

    [Display(Name = "Frenum")]
    public string SoftTissueFrenum { get; set; }

    [Display(Name = "Lips")]
    public string SoftTissueLipsLesions { get; set; }

    [Display(Name = "Buccal and labial mucosa")]
    public string SoftTissueBuccalLabialMucosa { get; set; }

    [Display(Name = "Pharynx tonsils")]
    public string SoftTissuePharynxTonsils { get; set; }

    [Display(Name = "Others")]
    public string SoftTissueOthers { get; set; }

    [Display(Name = "Color")]
    public string SoftTissueColor { get; set; }

    [Display(Name = "Contour")]
    public string SoftTissueContour { get; set; }

    [Display(Name = "Size")]
    public string SoftTissueSize { get; set; }

    [Display(Name = "Consistency")]
    public string SoftTissueConsistency { get; set; }

    [Display(Name = "Surface texture")]
    public string SoftTissueSurfaceTexture { get; set; }

    [Display(Name = "Exudation")]
    public string SoftTissueExudation { get; set; }

    [Display(Name = "Eruption status")]
    public string HardTissueEruptionStatus { get; set; }

    [Display(Name = "Abnormalities")]
    public string Abnormalities { get; set; }

    [Display(Name = "Size")]
    public string HardTissueSize { get; set; }

    [Display(Name = "Shape")]
    public string HardTissueShape { get; set; }

    [Display(Name = "Number")]
    public string HardTissueNumber { get; set; }

    [Display(Name = "Hypoplastic teeth")]
    public string HardTissueHypoplasticTeeth { get; set; }

    [Display(Name = "Discoloration intrinsic")]
    public string HardTissueDiscolorationIntrinsic { get; set; }

    [Display(Name = "Discoloration extrinsic")]
    public string HardTissueDiscolorationExtrinsic { get; set; }

    [Display(Name = "Trauma")]
    public string HardTissueTrauma { get; set; }

    [Display(Name = "Erupting teeth")]
    public string HardTissueEruptingTeeth { get; set; }

    [Display(Name = "Fractured Teeth")]
    public string HardTissueFracturedTeeth { get; set; }

    [Display(Name = "Mobility")]
    public string HardTissueMobility { get; set; }

    public string MolarPrimaryFlushRight { get; set; }

    public string MolarPrimaryFlushLeft { get; set; }

    public string MolarPrimaryMesialRight { get; set; }

    public string MolarPrimaryMesialLeft { get; set; }

    public string MolarPrimaryDistalRight { get; set; }

    public string MolarPrimaryDistalLeft { get; set; }

    public string MolarPrimaryPrimateRight { get; set; }

    public string MolarPrimaryPrimateLeft { get; set; }

    public string MolarPermanentUneruptedRight { get; set; }

    public string MolarPermanentUneruptedLeft { get; set; }

    public string MolarPermanentEndToEndRight { get; set; }

    public string MolarPermanentEndToEndLeft { get; set; }

    public string MolarPermanentClassRightDesc { get; set; }

    public string MolarPermanentClassLeftDesc { get; set; }

    [Display(Name = "Overjet")]
    public string IncisorRelationOverjet { get; set; }

    public string IncisorRelationOverbiteDesc { get; set; }

    public string IncisorRelationOpenbiteRight { get; set; }

    public string IncisorRelationOpenbiteLeft { get; set; }

    [Display(Name = "Mesial step")]
    public string MolarPrimaryMesial { get; set; }

    [Display(Name = "Distal step")]
    public string MolarPrimaryDistal { get; set; }

    [Display(Name = "Primate space")]
    public string MolarPrimaryPrimate { get; set; }

    [Display(Name = "Flush terminal")]
    public string MolarPrimaryFlush { get; set; }

    [Display(Name = "Maxilla")]
    public string Maxilla { get; set; }

    [Display(Name = "Mandible")]
    public string Mandible { get; set; }

    [Display(Name = "Mandibular shift")]
    public string Mandibular { get; set; }

    [Display(Name = "Anterior")]
    public string Anterior { get; set; }

    [Display(Name = "Unerupted")]
    public string MolarPermanentUnerupted { get; set; }

    [Display(Name = "End to end")]
    public string MolarPermanentEndToEnd { get; set; }

    [Display(Name = "Class")]
    public string MolarPermanentClass { get; set; }

    public string MaxillaRight { get; set; }

    public string MaxillaLeft { get; set; }

    public string MandibleRight { get; set; }

    public string MandibleLeft { get; set; }

    public string AnteriorRight { get; set; }

    public string AnteriorLeft { get; set; }

    [Display(Name = "e")]
    public string MaxillaE { get; set; }

    [Display(Name = "f")]
    public string MaxillaF { get; set; }

    [Display(Name = "g")]
    public string MaxillaG { get; set; }

    [Display(Name = "h")]
    public string MaxillaH { get; set; }

    [Display(Name = "Total")]
    public string MaxillaTotal { get; set; }

    [Display(Name = "a")]
    public string MandibleA { get; set; }

    [Display(Name = "b")]
    public string MandibleB { get; set; }

    [Display(Name = "c")]
    public string MandibleC { get; set; }

    [Display(Name = "d")]
    public string MandibleD { get; set; }

    [Display(Name = "Total")]
    public string MandibleTotal { get; set; }

    [Display(Name = "Incisor Width (measured)")]
    public string IncisorWidthMaxilla { get; set; }

    public string IncisorWidthMandible { get; set; }

    [Display(Name = "Width of cuspids & bicuspids(predicted)")]
    public string CuspidsBicuspidMaxilla { get; set; }

    public string CuspidsBicuspidMandible { get; set; }

    [Display(Name = "Needed to achieve class I occlusion of molars(estimated)")]
    public string OcclusionMolarMaxilla { get; set; }

    public string OcclusionMolarMandible { get; set; }

    [Display(Name = "Estimated possibilities of maxilla")]
    public string PossibilityMaxillaRight { get; set; }

    public string PossibilityMaxillaLeft { get; set; }

    [Display(Name = "Increasing available space mandible")]
    public string MandibleSpaceRight { get; set; }

    public string MandibleSpaceLeft { get; set; }

    [Display(Name = "Remarks")]
    public string SpaceAnalysisRemarks { get; set; }

    public string WitsAppraisalAo { get; set; }

    public string WitsAppraisalBo { get; set; }

    [Display(Name = "Final Diagnosis")]
    public string FinalDiagnosis { get; set; }

    [Display(Name = "Provisional Diagnosis")]
    public string ProvisionalDiagnosis { get; set; }

    public string EmergencyTreatment { get; set; }

    public string BehaviorManagement { get; set; }

    public string MedicalReferences { get; set; }

    public string OralProphylaxis { get; set; }

    public string HomeCare { get; set; }

    public string DietCounselling { get; set; }

    public string FluorideApplication { get; set; }

    public string NonFluoride { get; set; }

    public string PitFissureSealants { get; set; }

    public string PRR { get; set; }

    public string CariesControl { get; set; }

    public string RestorationMaterial1 { get; set; }

    public string RestorationMaterial2 { get; set; }

    public string RestorationMaterial3 { get; set; }

    public string RestorationMaterial4 { get; set; }

    public string RestorationMaterial5 { get; set; }

    public string RestorationTooth1 { get; set; }

    public string RestorationTooth2 { get; set; }

    public string RestorationTooth3 { get; set; }

    public string RestorationTooth4 { get; set; }

    public string RestorationTooth5 { get; set; }

    public string Extraction { get; set; }

    public string SpaceManagementType1 { get; set; }

    public string SpaceManagementType2 { get; set; }

    public string SpaceManagementType3 { get; set; }

    public string SpaceManagementType4 { get; set; }

    public string SpaceManagementType5 { get; set; }

    public string SpaceManagementTooth1 { get; set; }

    public string SpaceManagementTooth2 { get; set; }

    public string SpaceManagementTooth3 { get; set; }

    public string SpaceManagementTooth4 { get; set; }

    public string SpaceManagementTooth5 { get; set; }

    public string OrthodonticReferral { get; set; }

    public string CorrectiveRemarks { get; set; }

    public string CorrectiveSurgeryAnyOther { get; set; }

    public string ReviewRecall { get; set; }

    public string EducationMotivation { get; set; }

    public string PeriodicAssessment { get; set; }

    public string EndodonticTreatmentDate { get; set; }

    public string EndodonticChiefComplaint { get; set; }

    public string EndodonticTeethHistory { get; set; }

    public string PainAbsent { get; set; }

    public string PainLocalised { get; set; }

    public string PainSharp { get; set; }

    public string PainThrobbing { get; set; }

    public string PainSpontaneous { get; set; }

    public string PainContinuous { get; set; }

    public string PainPresent { get; set; }

    public string PainDiffused { get; set; }

    public string PanDull { get; set; }

    public string PainReferred { get; set; }

    public string PainProvoked { get; set; }

    public string PainIntermittent { get; set; }

    public string ProvokedByCold { get; set; }

    public string ProvokedByHeat { get; set; }

    public string ProvokedBySweets { get; set; }

    public string ProvokedByPressure { get; set; }

    public string ProvokedByMastication { get; set; }

    public string ClinicalExamToothNo1 { get; set; }

    public string ClinicalExamToothNo2 { get; set; }

    public string ClinicalExamToothNo3 { get; set; }

    public string ClinicalExamToothNo4 { get; set; }

    public string ClinicalExamToothNo5 { get; set; }

    public string ClinicalExamToothNo6 { get; set; }

    public string ClinicalExamCold1 { get; set; }

    public string ClinicalExamCold2 { get; set; }

    public string ClinicalExamCold3 { get; set; }

    public string ClinicalExamCold4 { get; set; }

    public string ClinicalExamCold5 { get; set; }

    public string ClinicalExamCold6 { get; set; }

    public string ClinicalExamHeat1 { get; set; }

    public string ClinicalExamHeat2 { get; set; }

    public string ClinicalExamHeat3 { get; set; }

    public string ClinicalExamHeat4 { get; set; }

    public string ClinicalExamHeat5 { get; set; }

    public string ClinicalExamHeat6 { get; set; }

    public string ClinicalExamElectricPulp1 { get; set; }

    public string ClinicalExamElectricPulp2 { get; set; }

    public string ClinicalExamElectricPulp3 { get; set; }

    public string ClinicalExamElectricPulp4 { get; set; }

    public string ClinicalExamElectricPulp5 { get; set; }

    public string ClinicalExamElectricPulp6 { get; set; }

    public string ClinicalExamPercussion1 { get; set; }

    public string ClinicalExamPercussion2 { get; set; }

    public string ClinicalExamPercussion3 { get; set; }

    public string ClinicalExamPercussion4 { get; set; }

    public string ClinicalExamPercussion5 { get; set; }

    public string ClinicalExamPercussion6 { get; set; }

    public string ClinicalExamBiteTest1 { get; set; }

    public string ClinicalExamBiteTest2 { get; set; }

    public string ClinicalExamBiteTest3 { get; set; }

    public string ClinicalExamBiteTest4 { get; set; }

    public string ClinicalExamBiteTest5 { get; set; }

    public string ClinicalExamBiteTest6 { get; set; }

    public string ClinicalExamPalpation1 { get; set; }

    public string ClinicalExamPalpation2 { get; set; }

    public string ClinicalExamPalpation3 { get; set; }

    public string ClinicalExamPalpation4 { get; set; }

    public string ClinicalExamPalpation5 { get; set; }

    public string ClinicalExamPalpation6 { get; set; }

    public string ClinicalExamMobility1 { get; set; }

    public string ClinicalExamMobility2 { get; set; }

    public string ClinicalExamMobility3 { get; set; }

    public string ClinicalExamMobility4 { get; set; }

    public string ClinicalExamMobility5 { get; set; }

    public string ClinicalExamMobility6 { get; set; }

    public string PerioDepth1 { get; set; }

    public string PerioDepth2 { get; set; }

    public string PerioDepth3 { get; set; }

    public string PerioDepth4 { get; set; }

    public string PerioDepth5 { get; set; }

    public string PerioDepth6 { get; set; }

    public string PerioDepth7 { get; set; }

    public string PerioDepth8 { get; set; }

    public string PerioDepth9 { get; set; }

    public string PerioDepth10 { get; set; }

    public string PerioDepth11 { get; set; }

    public string PerioDepth12 { get; set; }

    public string PerioDepth13 { get; set; }

    public string PerioDepth14 { get; set; }

    public string PerioDepth15 { get; set; }

    public string PerioDepth16 { get; set; }

    public string PerioDepth17 { get; set; }

    public string PerioDepth18 { get; set; }

    public string PerioDepth19 { get; set; }

    public string PerioDepth20 { get; set; }

    public string PerioDepth21 { get; set; }

    public string PerioDepth22 { get; set; }

    public string PerioDepth23 { get; set; }

    public string PerioDepth24 { get; set; }

    public string PerioDepth25 { get; set; }

    public string PerioDepth26 { get; set; }

    public string PerioDepth27 { get; set; }

    public string PerioDepth28 { get; set; }

    public string PerioDepth29 { get; set; }

    public string PerioDepth30 { get; set; }

    public string PerioDepth31 { get; set; }

    public string PerioDepth32 { get; set; }

    public string PerioDepth33 { get; set; }

    public string PerioDepth34 { get; set; }

    public string PerioDepth35 { get; set; }

    public string PerioDepth36 { get; set; }

    public string SoftTissueSinusAbsent { get; set; }

    public string SoftTissueSinusPresent { get; set; }

    public string SoftTissueSinusLocation { get; set; }

    public string SoftTissueSwellingAbsent { get; set; }

    public string SoftTissueSwellingLocalized { get; set; }

    public string SoftTissueSwellingDiffused { get; set; }

    public string SoftTissueIntraoral { get; set; }

    public string SoftTissueExtraoral { get; set; }

    public string SoftTissueLocation { get; set; }

    public string LymphEnopathyAbsent { get; set; }

    public string LymphEnopathyPresent { get; set; }

    [Display(Name = "Lymph Nodes")]
    public string LymphNodes { get; set; }

    public string EtiologyCaries { get; set; }

    public string EtiologyMechanicalExposure { get; set; }

    public string EtiologyIntentional { get; set; }

    public string EtiologyRestorative { get; set; }

    public string EtiologyCoronalFracture { get; set; }

    public string EtiologyTrauma { get; set; }

    public string EtiologyPriorAccess { get; set; }

    public string EtiologyAttrition { get; set; }

    public string DiagnoisToothNo { get; set; }

    public string DiagnoisPulpNormal { get; set; }

    public string DiagnosisReversiblePulpitis { get; set; }

    public string DiagnosisIrreversiblePulpitis { get; set; }

    public string DiagnosisNecrosis { get; set; }

    public string DiagnosisPulpless { get; set; }

    public string PeriradicularNormal { get; set; }

    public string PeriradicularAcutePeriodontitis { get; set; }

    public string PeriradicalChronicPeriodontitis { get; set; }

    public string PeriradicalChronicAbscess { get; set; }

    public string PeriradicalAcuteAbscess { get; set; }

    public string PeriradicalOthers { get; set; }

    public string PrognosisFavourable { get; set; }

    public string PrognosisUnFavourable { get; set; }

    public string DiagnosisRemarks { get; set; }

    public string RecommendedTreatmentRemarks { get; set; }

    public string BacteriologicalOrganism { get; set; }

    public string BacteriologicalAntibiotic { get; set; }

    public string BacteriologicalCulture { get; set; }

    public string ToothInstrumentLength { get; set; }

    public string ToothRadioInstrumentLength { get; set; }

    public string ToothWorkingLength { get; set; }

    public string MBInstrumentLength { get; set; }

    public string MBRadioInstrumentLength { get; set; }

    public string MBWorkingLength { get; set; }

    public string MLInstrumentLength { get; set; }

    public string MLRadioInstrumentLength { get; set; }

    public string MLWorkingLength { get; set; }

    public string DBInstrumentLength { get; set; }

    public string DBRadioInstrumentLength { get; set; }

    public string DBWorkingLength { get; set; }

    public string DLInstrumentLength { get; set; }

    public string DLRadioInstrumentLength { get; set; }

    public string DLWorkingLength { get; set; }

    public string VisibleCavitiesDesc { get; set; }

    public string EnamelLesionsDesc { get; set; }

    public string Restoration3YearsDesc { get; set; }

    public string WhiteSpotDesc { get; set; }

    public string CaregiverActiveCariesDesc { get; set; }

    public string ChildBedBottleSugarDesc { get; set; }

    public string BrushingUntilEruptionDesc { get; set; }

    public string BrushingDaily5yDesc { get; set; }

    public string SupervisedBrushing5yDesc { get; set; }

    public string BeveragesPerDay5yDesc { get; set; }

    public string BrushingDaily6yDesc { get; set; }

    public string NonFluoridatedTpasteDesc { get; set; }

    public string BeveragesPerDay6yDesc { get; set; }

    public string VisibleHeavyPlaqueDesc { get; set; }

    public string DeepPitsAndFissuresDesc { get; set; }

    public string DefectiveRestorationsDesc { get; set; }

    public string SalivaReducingFactorsDesc { get; set; }

    public string OrthodonticOrIntraoralDesc { get; set; }

    public string RiskAssessmentHigh { get; set; }

    public string RiskAssessmentMedium { get; set; }

    public string RiskAssessmentLow { get; set; }

    public string SalivaTest { get; set; }

    public string PlaqueTest { get; set; }

    public string SalivaInference { get; set; }

    public string PlaqueInference { get; set; }

    public string OtherLabInvestigations { get; set; }

    [Display(Name = "55")]
    public string IndicesTeeth55 { get; set; }

    [Display(Name = "54")]
    public string IndicesTeeth54 { get; set; }

    [Display(Name = "53")]
    public string IndicesTeeth53 { get; set; }

    [Display(Name = "52")]
    public string IndicesTeeth52 { get; set; }

    [Display(Name = "51")]
    public string IndicesTeeth51 { get; set; }

    [Display(Name = "61")]
    public string IndicesTeeth61 { get; set; }

    [Display(Name = "62")]
    public string IndicesTeeth62 { get; set; }

    [Display(Name = "63")]
    public string IndicesTeeth63 { get; set; }

    [Display(Name = "64")]
    public string IndicesTeeth64 { get; set; }

    [Display(Name = "65")]
    public string IndicesTeeth65 { get; set; }

    [Display(Name = "71")]
    public string IndicesTeeth71 { get; set; }

    [Display(Name = "72")]
    public string IndicesTeeth72 { get; set; }

    [Display(Name = "73")]
    public string IndicesTeeth73 { get; set; }

    [Display(Name = "74")]
    public string IndicesTeeth74 { get; set; }

    [Display(Name = "75")]
    public string IndicesTeeth75 { get; set; }

    [Display(Name = "85")]
    public string IndicesTeeth85 { get; set; }

    [Display(Name = "84")]
    public string IndicesTeeth84 { get; set; }

    [Display(Name = "83")]
    public string IndicesTeeth83 { get; set; }

    [Display(Name = "82")]
    public string IndicesTeeth82 { get; set; }

    [Display(Name = "81")]
    public string IndicesTeeth81 { get; set; }

    [Display(Name = "11")]
    public string IndicesTeeth11 { get; set; }

    [Display(Name = "12")]
    public string IndicesTeeth12 { get; set; }

    [Display(Name = "13")]
    public string IndicesTeeth13 { get; set; }

    [Display(Name = "14")]
    public string IndicesTeeth14 { get; set; }

    [Display(Name = "15")]
    public string IndicesTeeth15 { get; set; }

    [Display(Name = "16")]
    public string IndicesTeeth16 { get; set; }

    [Display(Name = "17")]
    public string IndicesTeeth17 { get; set; }

    [Display(Name = "18")]
    public string IndicesTeeth18 { get; set; }

    [Display(Name = "21")]
    public string IndicesTeeth21 { get; set; }

    [Display(Name = "22")]
    public string IndicesTeeth22 { get; set; }

    [Display(Name = "23")]
    public string IndicesTeeth23 { get; set; }

    [Display(Name = "24")]
    public string IndicesTeeth24 { get; set; }

    [Display(Name = "25")]
    public string IndicesTeeth25 { get; set; }

    [Display(Name = "26")]
    public string IndicesTeeth26 { get; set; }

    [Display(Name = "27")]
    public string IndicesTeeth27 { get; set; }

    [Display(Name = "28")]
    public string IndicesTeeth28 { get; set; }

    [Display(Name = "31")]
    public string IndicesTeeth31 { get; set; }

    [Display(Name = "32")]
    public string IndicesTeeth32 { get; set; }

    [Display(Name = "33")]
    public string IndicesTeeth33 { get; set; }

    [Display(Name = "34")]
    public string IndicesTeeth34 { get; set; }

    [Display(Name = "35")]
    public string IndicesTeeth35 { get; set; }

    [Display(Name = "36")]
    public string IndicesTeeth36 { get; set; }

    [Display(Name = "37")]
    public string IndicesTeeth37 { get; set; }

    [Display(Name = "38")]
    public string IndicesTeeth38 { get; set; }

    [Display(Name = "41")]
    public string IndicesTeeth41 { get; set; }

    [Display(Name = "42")]
    public string IndicesTeeth42 { get; set; }

    [Display(Name = "43")]
    public string IndicesTeeth43 { get; set; }

    [Display(Name = "44")]
    public string IndicesTeeth44 { get; set; }

    [Display(Name = "45")]
    public string IndicesTeeth45 { get; set; }

    [Display(Name = "46")]
    public string IndicesTeeth46 { get; set; }

    [Display(Name = "47")]
    public string IndicesTeeth47 { get; set; }

    [Display(Name = "48")]
    public string IndicesTeeth48 { get; set; }

    public string IndicesDMFT { get; set; }

    public string IndicesDeft { get; set; }

    public string OralHygieneIndex { get; set; }

    public string OralHygieneGood { get; set; }

    public string OralHygieneFair { get; set; }

    public string OralHygienePoor { get; set; }

    [Display(Name = "Did the mother suffer from any of the following during pregnancy?")]
    public string MotherSuffered { get; set; }

    public string IncissorCrossbiteDesc { get; set; }

    [Display(Name = "Class")]
    public string CanineRelationClass { get; set; }

    [Display(Name = "Have you been told you are Rh -ve?")]
    public string RhNegative { get; set; }

    [Display(Name = "Is there blood incompatibility between you and your spouse?")]
    public string BloodIncompatibility { get; set; }

    [Display(Name = "Sitting")]
    public string SittingDesc { get; set; }

    [Display(Name = "Standing  ")]
    public string StandingDesc { get; set; }

    [Display(Name = "Teething")]
    public string TeethingDesc { get; set; }

    [Display(Name = "Crawling")]
    public string CrawlingDesc { get; set; }

    [Display(Name = "Walking")]
    public string WalkingDesc { get; set; }

    [Display(Name = "Speaking")]
    public string SpeakingDesc { get; set; }

    [Display(Name = "Does your child have frequent minor accidents or injuries")]
    public string MinorAccident { get; set; }

    [Display(Name = "Immunisation Status")]
    public string ImmunisationStatus { get; set; }

    [Display(Name = "Tooth brush")]
    public string ToothBrushMethod { get; set; }

    [Display(Name = "Facial Symmetry")]
    public int? ExtraOralFaceSymmetryId { get; set; }

    [Display(Name = "Have your child ever been advised to take antibiotics before dental treatment?")]
    public int? AntibioticsBeforeTreatId { get; set; }

    [Display(Name = "Rating")]
    public int? FrankelzRatingId { get; set; }

    [Display(Name = "Is this the Childs's First Dental visit")]
    public int? FirstDentalVisitId { get; set; }

    [Display(Name = "Has your child ever had any unfavourable dental experience?")]
    public string UnfavourableDentalExp { get; set; }

    [Display(Name = "Nail Biting")]
    public string NailBitingFrequency { get; set; }

    [Display(Name = "Have your child had any history of trauma?")]
    public int? HistoryOfTraumaId { get; set; }

    [Display(Name = "Nail Biting intensity")]
    public string NailBitingIntensity { get; set; }

    [Display(Name = "Is the child taking any medications?")]
    public string MedicationTaken { get; set; }

    [Display(Name = "Nail Biting Duration")]
    public string NailBitingDuration { get; set; }

    [Display(Name = "Is the child having any history of allergy?")]
    public int? AllergyHistoryId { get; set; }

    [Display(Name = "Lip Biting")]
    public string LipBitingFrequency { get; set; }

    [Display(Name = "Lip Biting Intensity")]
    public string LipBitingIntensity { get; set; }

    [Display(Name = "Lip Biting Duration")]
    public string LipBitingDuration { get; set; }

    [Display(Name = "Cheek Biting")]
    public string CheekBitingFrequency { get; set; }

    [Display(Name = "Is the child supervised while brushing")]
    public int? SupervisedBrushingId { get; set; }

    [Display(Name = "Nature of sleep")]
    public int? NatureOfSleepId { get; set; }

    [Display(Name = "Duration of sleep")]
    public int? DurationOfSleepId { get; set; }

    [Display(Name = "Does the child sleep during the day time")]
    public int? DayTimeSleepId { get; set; }

    [Display(Name = "Dryness of mouth during sleep")]
    public int? MouthDrynessId { get; set; }

    [Display(Name = "Snoring during sleep")]
    public int? SnoringSleepId { get; set; }

    [Display(Name = "Cheek Biting Intensity")]
    public string CheekBitingIntensity { get; set; }

    [Display(Name = "Parents consanguineous marriage")]
    public int? ParentsConsanguineousId { get; set; }

    [Display(Name = "Dental problems (Familial/Genetic)")]
    public int? DentalProblemsId { get; set; }

    [Display(Name = "Hospitalization")]
    public int? HospitalizationId { get; set; }

    [Display(Name = "Trauma")]
    public int? TraumaId { get; set; }

    [Display(Name = "Significant history")]
    public int? SignicficantHistoryId { get; set; }

    [Display(Name = "Illness")]
    public int? IllnessId { get; set; }

    [Display(Name = "Any medication taken by the mother during pregnancy")]
    public int? PregnancyMedicationId { get; set; }

    [Display(Name = "Was he/she jaundiced at birth, blue baby RH incompitability")]
    public int? BlueBabyRhIncompId { get; set; }

    [Display(Name = "Does/did the child sleep with bottle?")]
    public int? SleepWithBottleId { get; set; }

    [Display(Name = "Was the pacifier used?(Y/N)")]
    public string PacifierUsedDesc { get; set; }

    [Display(Name = "Did the child had an erupted tooth?")]
    public int? EruptedToothId { get; set; }

    [Display(Name = "Child's reaction to the visit?")]
    public int? VisitReactionChildId { get; set; }

    [Display(Name = "Does the child have any difficulty in communication/learning?")]
    public int? CommunicationDifficultyId { get; set; }

    public int? BalancedDietId { get; set; }

    [Display(Name = "Body type")]
    public int? GeneralBodyTypeId { get; set; }

    [Display(Name = "Facial profile")]
    public int? ExtraOralFaceProfileId { get; set; }

    [Display(Name = "Lips Posture and Toncity")]
    public int? ExtraOralLipPostToneId { get; set; }

    [Display(Name = "TMJ")]
    public int? ExtraOralTmjId { get; set; }

    [Display(Name = "Facial form")]
    public int? ExtraOralFacialFormId { get; set; }

    [Display(Name = "Gingiva")]
    public string SoftTissueGingiva { get; set; }

    [Display(Name = "Tongue")]
    public string SoftTissueTongue { get; set; }

    [Display(Name = "Floor of mouth")]
    public string SoftTissueMouthFloor { get; set; }

    [Display(Name = "mm")]
    public string Mm { get; set; }

    public int? MolarPermanentClassRightId { get; set; }

    public int? MolarPermanentClassLeftId { get; set; }

    [Display(Name = "Overbite")]
    public int? IncisorRelationOverbiteid { get; set; }

    [Display(Name = "Openbite")]
    public int? IncisorRelationOpenbiteId { get; set; }

    public int? CanineRelationClassRightId { get; set; }

    public int? CanineRelationClassLeftId { get; set; }

    [Display(Name = "Midline")]
    public int? MidlineId { get; set; }

    public int? MandibularRightId { get; set; }

    public int? MandibularLeftId { get; set; }

    public int? VisibleCavitiesId { get; set; }

    public int? EnamelLesionsId { get; set; }

    public int? Restoration3YearsId { get; set; }

    public int? WhiteSpotId { get; set; }

    public int? CaregiverActiveCariesId { get; set; }

    public int? ChildBedBottleSugarId { get; set; }

    public int? BrushingUntilEruptionId { get; set; }

    public int? BrushingDaily5yId { get; set; }

    public int? SupervisedBrushing5yId { get; set; }

    public int? BeveragesPerDay5yId { get; set; }

    public int? BrushingDaily6yId { get; set; }

    public int? NonFluoridatedTpasteId { get; set; }

    public int? BeveragesPerDay6yId { get; set; }

    public int? VisibleHeavyPlaqueId { get; set; }

    public int? DeepPitsAndFissuresId { get; set; }

    public int? DefectiveRestorationsId { get; set; }

    public int? SalivaReducingFactorsId { get; set; }

    public int? OrthodonticOrIntraoralId { get; set; }

    public int CodeId { get; set; }

    [Display(Name = "Blood Transfusion?")]
    public int? BloodTransfusionId { get; set; }

    [Display(Name = "Crossbite")]
    public int? IncissorCrossbiteId { get; set; }

    [Display(Name = "Does your child have difficulty making friends?")]
    public int? FriendsMakingDifficultyId { get; set; }

    [Display(Name = "Does your child fail to get along with other children?")]
    public int? GetAlongFailId { get; set; }

    [Display(Name = "Does your child have brothers/sisters?")]
    public int? BrotherSisterId { get; set; }

    [Display(Name = "If yes,what is there age?")]
    public string BrotherSisterAgeDesc { get; set; }

    [Display(Name = "Does your child has difficulty keeping up with his/her school work?")]
    public int? SchoolWorkDiffId { get; set; }

    [Display(Name = "Does your child fear the dentist?")]
    public int? DentistFearId { get; set; }

    [Display(Name = "Lip Type")]
    public int? LipTypeId { get; set; }

    [Display(Name = "Facial Divergence")]
    public int? FacialDivergenceId { get; set; }

    public string FacialDivergenceDesc { get; set; }

    [Display(Name = "Plaque")]
    public int? PlaqueId { get; set; }

    [Display(Name = "Calculus")]
    public int? CalculusId { get; set; }

    [Display(Name = "Stains(Extrinsic)")]
    public int? StainsId { get; set; }

    [Display(Name = "Shape of head")]
    public int? HeadShapeId { get; set; }

    public string HeadShapeDesc { get; set; }

    [Display(Name = "If yes,do you know why?")]
    public string DentistFearReason { get; set; }

    [Display(Name = "Emergency Phase")]
    public string EmergencyPhase { get; set; }

    [Display(Name = "Medical/ReferralPhase")]
    public string MedicalReferralPhase { get; set; }

    [Display(Name = "Systemic Phase")]
    public string SystemicPhase { get; set; }

    [Display(Name = "Preparatory Phase")]
    public string PreparatoryPhase { get; set; }

    [Display(Name = "Preventive Phase")]
    public string PreventivePhase { get; set; }

    [Display(Name = "Corrective Phase")]
    public string CorrectivePhase { get; set; }

    [Display(Name = "Maintenance Phase")]
    public string MaintenancePhase { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public string GIndexTotal { get; set; }

    public string Total { get; set; }

    public Decimal? CITotal { get; set; }

    public Decimal? DITotal { get; set; }

    public Decimal? OHITotal { get; set; }

    public string DMFTTotal { get; set; }

    public Decimal? DTotal { get; set; }

    public Decimal? MTotal { get; set; }

    public Decimal? FTotal { get; set; }

    public string DeftTotal { get; set; }

    public Decimal? dTotalD { get; set; }

    public Decimal? eTotalE { get; set; }

    public Decimal? fTotalF { get; set; }

    public Decimal? DIUpperTotal { get; set; }

    public Decimal? DILowerTotal { get; set; }

    public Decimal? CIUpperTotal { get; set; }

    public Decimal? CILowerTotal { get; set; }

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

    [Display(Name = "17")]
    public string GIT17_1 { get; set; }

    public string GIT17_2 { get; set; }

    public string GIT17_3 { get; set; }

    public string GIT17_4 { get; set; }

    [Display(Name = "16")]
    public string GIT16_1 { get; set; }

    public string GIT16_2 { get; set; }

    public string GIT16_3 { get; set; }

    public string GIT16_4 { get; set; }

    [Display(Name = "15")]
    public string GIT15_1 { get; set; }

    public string GIT15_2 { get; set; }

    public string GIT15_3 { get; set; }

    public string GIT15_4 { get; set; }

    [Display(Name = "14")]
    public string GIT14_1 { get; set; }

    public string GIT14_2 { get; set; }

    public string GIT14_3 { get; set; }

    public string GIT14_4 { get; set; }

    [Display(Name = "13")]
    public string GIT13_1 { get; set; }

    public string GIT13_2 { get; set; }

    public string GIT13_3 { get; set; }

    public string GIT13_4 { get; set; }

    [Display(Name = "12")]
    public string GIT12_1 { get; set; }

    public string GIT12_2 { get; set; }

    public string GIT12_3 { get; set; }

    public string GIT12_4 { get; set; }

    [Display(Name = "11")]
    public string GIT11_1 { get; set; }

    public string GIT11_2 { get; set; }

    public string GIT11_3 { get; set; }

    public string GIT11_4 { get; set; }

    [Display(Name = "21")]
    public string GIT21_1 { get; set; }

    public string GIT21_2 { get; set; }

    public string GIT21_3 { get; set; }

    public string GIT21_4 { get; set; }

    [Display(Name = "22")]
    public string GIT22_1 { get; set; }

    public string GIT22_2 { get; set; }

    public string GIT22_3 { get; set; }

    public string GIT22_4 { get; set; }

    [Display(Name = "23")]
    public string GIT23_1 { get; set; }

    public string GIT23_2 { get; set; }

    public string GIT23_3 { get; set; }

    public string GIT23_4 { get; set; }

    [Display(Name = "24")]
    public string GIT24_1 { get; set; }

    public string GIT24_2 { get; set; }

    public string GIT24_3 { get; set; }

    public string GIT24_4 { get; set; }

    [Display(Name = "25")]
    public string GIT25_1 { get; set; }

    public string GIT25_2 { get; set; }

    public string GIT25_3 { get; set; }

    public string GIT25_4 { get; set; }

    [Display(Name = "26")]
    public string GIT26_1 { get; set; }

    public string GIT26_2 { get; set; }

    public string GIT26_3 { get; set; }

    public string GIT26_4 { get; set; }

    [Display(Name = "27")]
    public string GIT27_1 { get; set; }

    public string GIT27_2 { get; set; }

    public string GIT27_3 { get; set; }

    public string GIT27_4 { get; set; }

    [Display(Name = "47")]
    public string GIT47_1 { get; set; }

    public string GIT47_2 { get; set; }

    public string GIT47_3 { get; set; }

    public string GIT47_4 { get; set; }

    [Display(Name = "46")]
    public string GIT46_1 { get; set; }

    public string GIT46_2 { get; set; }

    public string GIT46_3 { get; set; }

    public string GIT46_4 { get; set; }

    [Display(Name = "45")]
    public string GIT45_1 { get; set; }

    public string GIT45_2 { get; set; }

    public string GIT45_3 { get; set; }

    public string GIT45_4 { get; set; }

    [Display(Name = "44")]
    public string GIT44_1 { get; set; }

    public string GIT44_2 { get; set; }

    public string GIT44_3 { get; set; }

    public string GIT44_4 { get; set; }

    [Display(Name = "43")]
    public string GIT43_1 { get; set; }

    public string GIT43_2 { get; set; }

    public string GIT43_3 { get; set; }

    public string GIT43_4 { get; set; }

    [Display(Name = "42")]
    public string GIT42_1 { get; set; }

    public string GIT42_2 { get; set; }

    public string GIT42_3 { get; set; }

    public string GIT42_4 { get; set; }

    [Display(Name = "41")]
    public string GIT41_1 { get; set; }

    public string GIT41_2 { get; set; }

    public string GIT41_3 { get; set; }

    public string GIT41_4 { get; set; }

    [Display(Name = "31")]
    public string GIT31_1 { get; set; }

    public string GIT31_2 { get; set; }

    public string GIT31_3 { get; set; }

    public string GIT31_4 { get; set; }

    [Display(Name = "32")]
    public string GIT32_1 { get; set; }

    public string GIT32_2 { get; set; }

    public string GIT32_3 { get; set; }

    public string GIT32_4 { get; set; }

    [Display(Name = "33")]
    public string GIT33_1 { get; set; }

    public string GIT33_2 { get; set; }

    public string GIT33_3 { get; set; }

    public string GIT33_4 { get; set; }

    [Display(Name = "34")]
    public string GIT34_1 { get; set; }

    public string GIT34_2 { get; set; }

    public string GIT34_3 { get; set; }

    public string GIT34_4 { get; set; }

    [Display(Name = "35")]
    public string GIT35_1 { get; set; }

    public string GIT35_2 { get; set; }

    public string GIT35_3 { get; set; }

    public string GIT35_4 { get; set; }

    [Display(Name = "36")]
    public string GIT36_1 { get; set; }

    public string GIT36_2 { get; set; }

    public string GIT36_3 { get; set; }

    public string GIT36_4 { get; set; }

    [Display(Name = "37")]
    public string GIT37_1 { get; set; }

    public string GIT37_2 { get; set; }

    public string GIT37_3 { get; set; }

    public string GIT37_4 { get; set; }

    public string LipTypeDesc { get; set; }

    [Display(Name = "Maxillary Sinus")]
    public string MaxillarySinus { get; set; }

    [Display(Name = "Any Other Findings")]
    public string ExtraOralOther { get; set; }

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

    [Display(Name = "BreakFast")]
    public string DayOneBreakFast { get; set; }

    public string DayTwoBreakFast { get; set; }

    public string DayThreeBreakFast { get; set; }

    public string DayFourBreakFast { get; set; }

    public string DayFiveBreakFast { get; set; }

    public string DaySixBreakFast { get; set; }

    [Display(Name = "Lunch")]
    public string DayOneLunch { get; set; }

    public string DayTwoLunch { get; set; }

    public string DayThreeLunch { get; set; }

    public string DayFourLunch { get; set; }

    public string DayFiveLunch { get; set; }

    public string DaySixLunch { get; set; }

    [Display(Name = "Dinner")]
    public string DayOneDinner { get; set; }

    public string DayTwoDinner { get; set; }

    public string DayThreeDinner { get; set; }

    public string DayFourDinner { get; set; }

    public string DayFiveDinner { get; set; }

    public string DaySixDinner { get; set; }

    [Display(Name = "Snacks/Others")]
    public string DayOneSnacks { get; set; }

    public string DayTwoSnacks { get; set; }

    public string DayThreeSnacks { get; set; }

    public string DayFourSnacks { get; set; }

    public string DayFiveSnacks { get; set; }

    public string DaySixSnacks { get; set; }

    [Display(Name = "Vital Signs")]
    public string VitalSigns { get; set; }

    [Display(Name = "Was your child ever treated under CA?")]
    public string CaTreated { get; set; }

    [Display(Name = "Respiratory")]
    public int? RespiratoryId { get; set; }

    [Display(Name = "Cvs")]
    public int? CvsId { get; set; }

    [Display(Name = "Cns")]
    public int? CnsId { get; set; }

    [Display(Name = "Immune System")]
    public int? ImmuneSystemId { get; set; }

    [Display(Name = "Renal Disease")]
    public int? RenalDiseaseId { get; set; }

    public string RespiratoryOthers { get; set; }

    public string CvsOthers { get; set; }

    public string CnsOthers { get; set; }

    public string ImmuneSystemOthers { get; set; }

    public string RenalDiseaseOthers { get; set; }

    [Display(Name = "Git")]
    public string Git { get; set; }

    [Display(Name = "Hematological Disorders")]
    public string HematologicalDisorders { get; set; }

    [Display(Name = "Lip Sucking")]
    public string LipSucking { get; set; }

    [Display(Name = "Lip Sucking")]
    public string LipSuckingFrequency { get; set; }

    public string LipSuckingIntensity { get; set; }

    public string LipSuckingDuration { get; set; }

    [Display(Name = "Have you undergone dental Treatment?(Specify What Treatment)")]
    public string DentalTreatmentUndergone { get; set; }

    [Display(Name = "Does any of the parents have discoloured tooth?(Grey,Yellow or Brown)")]
    public string DiscolouredTooth { get; set; }

    [Display(Name = "Any other problem with parents teeth?(Shape,Size,Alignment)")]
    public string ParentsTeethProblem { get; set; }

    [Display(Name = "Parental attitude towards dental Appointments?")]
    public string ParentsDentalAttitude { get; set; }

    [Display(Name = "How was the child's condition during Birth?")]
    public string BirthConditionDesc { get; set; }

    [Display(Name = "Analysis Required?")]
    public string AnalysisRequiredDesc { get; set; }

    [Display(Name = "Study Models")]
    public string StudyModelsDesc { get; set; }

    [Display(Name = "Xray")]
    public string XrayDesc { get; set; }

    [Display(Name = "Cephalogram")]
    public string CephalogramDesc { get; set; }

    [Display(Name = "Carpel Xray")]
    public string CarpelXrayDesc { get; set; }

    [Display(Name = "Photographs")]
    public string PhotographsDesc { get; set; }

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

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    [Display(Name = "Last treatment date?")]
    public DateTime LastTreatmentDate
    {
      get
      {
        return this._LastTreatmentDate;
      }
      set
      {
        this._LastTreatmentDate = value;
      }
    }

    public List<ReferralStatusViewModel> ApprovedepartmentReferral { get; set; }

    public PatientInformationViewModel patientInformationViewModel { get; set; }

    public StudentAllotmentViewModel studentAllotmentViewModel { get; set; }

    public PrescriptionsViewModel PrescriptionsList { get; set; }

    public IEnumerable<PrescriptionsViewModel> PrescriptionsDetails { get; set; }

    public IEnumerable<PrescriptionsViewModel> PreviousPrescriptionList { get; set; }

    public IEnumerable<PEDOCasesheetProperties> Proplist { get; set; }

    public IEnumerable<MASCode> EruptedToothYesNoList { get; set; }

    public IEnumerable<MASCode> PhysicianCareYesNoList { get; set; }

    public IEnumerable<MASCode> FirstDentalVisitYesNoList { get; set; }

    public IEnumerable<MASCode> ToothBrushMethodList { get; set; }

    public IEnumerable<MASCode> AntibioticsBefoYesNoList { get; set; }

    public IEnumerable<MASCode> FrankelzRatingList { get; set; }

    public IEnumerable<MASCode> NormalCsectionList { get; set; }

    public IEnumerable<MASCode> MedicationYesNoList { get; set; }

    public IEnumerable<MASCode> MotherConditionList { get; set; }

    public IEnumerable<MASCode> AllergyHistoryYesNoList { get; set; }

    public IEnumerable<MASCode> ChildConditionList { get; set; }

    public IEnumerable<MASCode> HospitalizedYesNoList { get; set; }

    public IEnumerable<MASCode> InformationSourceList { get; set; }

    public IEnumerable<MASCode> BrushingFrequencyList { get; set; }

    public IEnumerable<MASCode> SupervisedYesNoList { get; set; }

    public IEnumerable<MASCode> LipTypeList { get; set; }

    public IEnumerable<MASCode> SleepNatureList { get; set; }

    public IEnumerable<MASCode> SleepDurationList { get; set; }

    public IEnumerable<MASCode> DaySleepList { get; set; }

    public IEnumerable<MASCode> MouthDrynessList { get; set; }

    public IEnumerable<MASCode> SnoringYesNoList { get; set; }

    public IEnumerable<MASCode> FullTermPrematureList { get; set; }

    public IEnumerable<MASCode> ConsanguineousMarriageYesNoList { get; set; }

    public IEnumerable<MASCode> DentalProblemsYesNoList { get; set; }

    public IEnumerable<MASCode> HospitalizationYesNoList { get; set; }

    public IEnumerable<MASCode> TraumaYesNoList { get; set; }

    public IEnumerable<MASCode> SignificantHistoryYesNoList { get; set; }

    public IEnumerable<MASCode> IllnessYesNoList { get; set; }

    public IEnumerable<MASCode> MomMedicationYesNoList { get; set; }

    public IEnumerable<MASCode> BluebabyYesNoList { get; set; }

    public IEnumerable<MASCode> BottleSleepYesNoList { get; set; }

    public IEnumerable<MASCode> PacifierYesNoList { get; set; }

    public IEnumerable<MASCode> VisitReactionList { get; set; }

    public IEnumerable<MASCode> CommuLearnDifficultyYesNoList { get; set; }

    public IEnumerable<MASCode> BodyTypeList { get; set; }

    public IEnumerable<MASCode> FacialFormExtraOralList { get; set; }

    public IEnumerable<MASCode> FacialProfileList { get; set; }

    public IEnumerable<MASCode> FacialSymmetryList { get; set; }

    public IEnumerable<MASCode> TmjList { get; set; }

    public IEnumerable<MASCode> LipsList { get; set; }

    public IEnumerable<MASCode> IncissorRelationOverbiteList { get; set; }

    public IEnumerable<MASCode> CrossbiteList { get; set; }

    public IEnumerable<MASCode> OpenbiteList { get; set; }

    public IEnumerable<MASCode> MolarClassRightList { get; set; }

    public IEnumerable<MASCode> MolarClassLeftList { get; set; }

    public IEnumerable<MASCode> CanineClassRightList { get; set; }

    public IEnumerable<MASCode> CanineClassLeftList { get; set; }

    public IEnumerable<MASCode> MidlineList { get; set; }

    public IEnumerable<MASCode> MandibularShiftRightList { get; set; }

    public IEnumerable<MASCode> MandibularShiftLeftList { get; set; }

    public IEnumerable<MASCode> BloodTransfusionList { get; set; }

    public IEnumerable<MASCode> FriendsMakingDifficultyList { get; set; }

    public IEnumerable<MASCode> GetAlongFailList { get; set; }

    public IEnumerable<MASCode> BrotherSisterList { get; set; }

    public IEnumerable<MASCode> SchoolWorkDiffList { get; set; }

    public IEnumerable<MASCode> DentistFearList { get; set; }

    public IEnumerable<MASCode> FacialDivergenceList { get; set; }

    public IEnumerable<MASCode> HeadShapeList { get; set; }

    public IEnumerable<MASCode> StainsList { get; set; }

    public IEnumerable<MASCode> CalculusList { get; set; }

    public IEnumerable<MASCode> PlaqueList { get; set; }

    public IEnumerable<MASCode> RespiratoryList { get; set; }

    public IEnumerable<MASCode> CvsList { get; set; }

    public IEnumerable<MASCode> CnsList { get; set; }

    public IEnumerable<MASCode> ImmuneSystemList { get; set; }

    public IEnumerable<MASCode> RenalDiseaseList { get; set; }

    public BillingQueueServiceViewModel billingLabRadQueueViewModal { get; set; }

    public IEnumerable<BillingViewModal> paidLabRadInvestigationList { get; set; }

    public IEnumerable<BillingQueueServiceViewModel> BillingLabRadQueueDetails { get; set; }

    public List<ReferralStatusViewModel> CreatedepartmentReferredStatus { get; set; }

    public IEnumerable<BillingQueueServiceViewModel> BillingQueueDetails { get; set; }

    public long TreatmentReferredId { get; set; }

    public FollowupViewModal followupViewModal { get; set; }

    public IEnumerable<FollowupViewModal> followupList { get; set; }

    public List<ReferralStatusViewModel> ViewdepartmentReferredStatus { get; set; }

    public IEnumerable<BillingViewModal> paidInvestigationList { get; set; }

    public IEnumerable<BillingViewModal> deptTreatmentList { get; set; }

    public RadioRegistrationViewModel radioRegistrationviewmodel { get; set; }

    public LaboratoryRegistrationViewModel laboratoryRegistrationviewmodel { get; set; }

    public BillingQueueServiceViewModel billingQueueViewModal { get; set; }

    public IEnumerable<StudentAllotmentViewModel> studentProcedureNotesViewModel { get; set; }

    public IEnumerable<FileUploadViewModel> FileUploadlist { get; set; }

    public MedicalAlertViewModel medicalalertviewmodel { get; set; }

    public IEnumerable<PEDOViewModel> Treatmentlist { get; set; }

    [Display(Name = "Date")]
    public string PEDODateDisplay { get; set; }

    public ApprovalViewModal approvalViewModal { get; set; }

    public string Stains { get; set; }

    public string Calculus { get; set; }

    [Display(Name = "Lip Type")]
    public string LipType { get; set; }

    [Display(Name = "Facial Divergence")]
    public string FacialDivergence { get; set; }

    [Display(Name = "Head Shape")]
    public string HeadShape { get; set; }

    [Display(Name = "Blood Transfusion?")]
    public string BloodTransfusionDn { get; set; }

    public string NormalCsectionIdDn { get; set; }

    public string RatingsFrank { get; set; }

    public string FirstDentalVisitDn { get; set; }

    public string AllergyHistoryDn { get; set; }

    public string SupervisedBrushingDn { get; set; }

    public string BrushFrequencyDn { get; set; }

    public string NatureOfSleep { get; set; }

    public string DurationOfSleep { get; set; }

    public string DayTimeSleep { get; set; }

    public string MouthDryness { get; set; }

    public string SnoringSleep { get; set; }

    public string ParentsConsanguineousDn { get; set; }

    public string Hospitalization { get; set; }

    public string Trauma { get; set; }

    public string SignicficantHistories { get; set; }

    public string Illness { get; set; }

    public string PregnancyMedication { get; set; }

    public string BlueBabyRhIncomp { get; set; }

    public string SleepWithBottle { get; set; }

    public string EruptedTooth { get; set; }

    public string ToothBrushMethodDn { get; set; }

    public string VisitReactionChild { get; set; }

    public string CommunicationDifficulty { get; set; }

    public string GeneralBodyTypeDn { get; set; }

    public string ExtraOralFaceProfile { get; set; }

    public string ExtraOralLipPostTone { get; set; }

    public string ExtraOralFaceForm { get; set; }

    public string MolarPermanentClassRight { get; set; }

    public string IncisorRelationOverbite { get; set; }

    public string IncisorRelationOpenbite { get; set; }

    public string CanineRelationClassRight { get; set; }

    public string CanineRelationClassLeft { get; set; }

    public string MandibleRightDn { get; set; }

    public string MandibularRight { get; set; }

    public string MandibularLeft { get; set; }

    public string Informer { get; set; }

    public string ConditionOfMotherDn { get; set; }

    public string FullPrematureIdDn { get; set; }

    public string BirthConditionDn { get; set; }

    public string ExtraOralFaceSymmetryDn { get; set; }

    public string CrossbiteIncissor { get; set; }

    public string ExtraOralTmj { get; set; }

    public string MolarPermanentClassLeft { get; set; }

    public string Mline { get; set; }

    public string Respiratory { get; set; }

    public string Cvs { get; set; }

    public string Cns { get; set; }

    public string ImmuneSystem { get; set; }

    public string RenalDisease { get; set; }

    public string BloodTransfusion { get; set; }

    public string FriendsMakingDifficulty { get; set; }

    public string GetAlongFail { get; set; }

    public string BrotherSister { get; set; }

    public string SchoolWorkDiff { get; set; }

    public string DentistFear { get; set; }

    public string Plaque { get; set; }

    public long OpNo { get; set; }

    public string PatientName { get; set; }

    public string Phone { get; set; }

    public string AgeGender { get; set; }

    public string Area { get; set; }

    public int? Age { get; set; }

    public int? GenderId { get; set; }
  }
}
