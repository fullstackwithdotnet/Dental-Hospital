// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.OrthoAnalysisViewModal
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
  public class OrthoAnalysisViewModal : EntityBase
  {
    public int OrthoId { get; set; }

    public int PatientId { get; set; }

    public int AnalysisId { get; set; }

    [Display(Name = "Analysis Name")]
    public string AnalysisDisplayName { get; set; }

    public string OrthoNo { get; set; }

    public int InferenceId { get; set; }

    public int SteinerId { get; set; }

    public int SteinerStaticId { get; set; }

    [Display(Name = "Letters")]
    public string Letters { get; set; }

    [Display(Name = "Measurement")]
    public string Measurements { get; set; }

    [Display(Name = "Mean")]
    public string Mean { get; set; }

    [Display(Name = "Pre Rx")]
    public string PreRx { get; set; }

    [Display(Name = "Post Growth Modulation")]
    public string PostModulation { get; set; }

    [Display(Name = "Post Rx")]
    public string PostRx { get; set; }

    [Display(Name = "Retention")]
    public string Retention { get; set; }

    [Display(Name = "Change")]
    public int DownsStaticId { get; set; }

    public int DownsId { get; set; }

    public int SchwarzStaticId { get; set; }

    [PrimaryKey]
    public int SchwarzId { get; set; }

    [Display(Name = "Parameter")]
    public string Parameters { get; set; }

    public int McNamaraStaticId { get; set; }

    public int McNamaraId { get; set; }

    public int TweedsStaticId { get; set; }

    public int TweedsId { get; set; }

    public string Angle { get; set; }

    public int RakosiStaticId { get; set; }

    public int RakosiId { get; set; }

    public int BurstoneHardStaticId { get; set; }

    public int BurstoneHardId { get; set; }

    public string Male { get; set; }

    public string Female { get; set; }

    public string Indicator { get; set; }

    public int BurstoneSoftStaticId { get; set; }

    public int BurstoneSoftId { get; set; }

    public int FrontalGrumStaticId { get; set; }

    public int FrontalGrumId { get; set; }

    public string Field { get; set; }

    public string LeftPreRx { get; set; }

    public string LeftPostRx { get; set; }

    public string RightPreRx { get; set; }

    public string RightPostRx { get; set; }

    public string DiffPreRx { get; set; }

    public string DiffPostRx { get; set; }

    public int FrontalEstheticsStaticId { get; set; }

    public int FrontalEstheticId { get; set; }

    public string Percentage { get; set; }

    public string Value { get; set; }

    public int JawBasesStaticId { get; set; }

    public int JawBasesId { get; set; }

    public string Change { get; set; }

    public string UpperLip { get; set; }

    public string LowerLip { get; set; }

    public string Inference { get; set; }

    public Decimal Upper1Rt { get; set; }

    public Decimal Upper2Rt { get; set; }

    public Decimal Upper3Rt { get; set; }

    public Decimal Upper4Rt { get; set; }

    public Decimal Upper5Rt { get; set; }

    public Decimal Upper6Rt { get; set; }

    public Decimal Upper1Lt { get; set; }

    public Decimal Upper2Lt { get; set; }

    public Decimal Upper3Lt { get; set; }

    public Decimal Upper4Lt { get; set; }

    public Decimal Upper5Lt { get; set; }

    public Decimal Upper6Lt { get; set; }

    public Decimal Lower1Rt { get; set; }

    public Decimal Lower2Rt { get; set; }

    public Decimal Lower3Rt { get; set; }

    public Decimal Lower4Rt { get; set; }

    public Decimal Lower5Rt { get; set; }

    public Decimal Lower6Rt { get; set; }

    public Decimal Lower1Lt { get; set; }

    public Decimal Lower2Lt { get; set; }

    public Decimal Lower3Lt { get; set; }

    public Decimal Lower4Lt { get; set; }

    public Decimal Lower5Lt { get; set; }

    public Decimal Lower6Lt { get; set; }

    [Display(Name = "12TM")]
    public Decimal Upper12Tm { get; set; }

    public Decimal Lower12Tm { get; set; }

    [Display(Name = "ANT 6TM")]
    public Decimal Upper6Tm { get; set; }

    public Decimal Lower6Tm { get; set; }

    [Display(Name = "ANTERIOR RATIO")]
    public Decimal AnteriorRatio { get; set; }

    [Display(Name = "OVERALL RATIO")]
    public Decimal OverallRatio { get; set; }

    [Display(Name = "Mandibular Excess")]
    public Decimal MandibularExcess6Tm { get; set; }

    [Display(Name = "Maxillary Excess")]
    public Decimal MaxillaryExcess6Tm { get; set; }

    [Display(Name = "Mandibular Excess")]
    public Decimal MandibularExcess12Tm { get; set; }

    [Display(Name = "Maxillary Excess")]
    public Decimal MaxillaryExcess12Tm { get; set; }

    [Display(Name = "Tooth")]
    public Decimal UpperTooth { get; set; }

    public Decimal LowerTooth { get; set; }

    [Display(Name = "Arch")]
    public Decimal UpperArch { get; set; }

    public Decimal LowerArch { get; set; }

    [Display(Name = "Difference")]
    public Decimal UpperDifference { get; set; }

    public Decimal LowerDifference { get; set; }

    [Display(Name = "Inference")]
    public Decimal UpperInference { get; set; }

    public Decimal LowerInference { get; set; }

    [Display(Name = "Sum Of Incisors(S.I.)")]
    public Decimal SumIncisors { get; set; }

    [Display(Name = "Calculated pre molar value(C.P.V)")]
    public Decimal CalcPreMolar { get; set; }

    [Display(Name = "Calculated molar value(C.M.V)")]
    public Decimal CalcMolar { get; set; }

    [Display(Name = "Measured premolar value(M.P.V)")]
    public Decimal MeasPreMolar { get; set; }

    [Display(Name = "Measured molar value(M.M.V)")]
    public Decimal MeasMolar { get; set; }

    [Display(Name = "PreMolar Diameter(P.M.D)")]
    public Decimal DiamPreMolar { get; set; }

    [Display(Name = "Total Tooth Material")]
    public Decimal TotalToothMaterial { get; set; }

    [Display(Name = "PreMolar Basal Arch Width(PMBAW)")]
    public Decimal BasalPreMolarArch { get; set; }

    [Display(Name = "PreMolar Basal Arch Width %")]
    public Decimal BasalPreMolarPect { get; set; }

    [Display(Name = "Total Space Available(Arch Perimeter)")]
    public Decimal UpperTotalSpace { get; set; }

    [Display(Name = "Space Obtainable by Detoration")]
    public Decimal UpperSpaceDerotation { get; set; }

    [Display(Name = "Crowding")]
    public Decimal UpperCrowding { get; set; }

    [Display(Name = "Incisor Proclination")]
    public Decimal UpperProclination { get; set; }

    [Display(Name = "Space Needed for Levelilng")]
    public Decimal UpperLeveling { get; set; }

    [Display(Name = "Total Space Required")]
    public Decimal UpperSpaceRequired { get; set; }

    [Display(Name = "Discrepancy")]
    public Decimal UpperDisperency { get; set; }

    [Display(Name = "Midline Deviated By")]
    public int UpperMidlineDeviation { get; set; }

    [Display(Name = "Side By")]
    public Decimal UpperMidlinemm { get; set; }

    [Display(Name = "Right Side")]
    public Decimal UpperSpaceNeedRight { get; set; }

    [Display(Name = "Left Side")]
    public Decimal UpperSpaceNeedLeft { get; set; }

    [Display(Name = "Total Space Available(Arch Perimeter)")]
    public Decimal LowerTotalSpace { get; set; }

    [Display(Name = "Space Obtainable by Detoration")]
    public Decimal LowerSpaceDerotation { get; set; }

    [Display(Name = "Crowding")]
    public Decimal LowerCrowding { get; set; }

    [Display(Name = "Incisor Proclination")]
    public Decimal LowerProclination { get; set; }

    [Display(Name = "Space Needed for Levelilng")]
    public Decimal LowerLeveling { get; set; }

    [Display(Name = "Total Space Required")]
    public Decimal LowerSpaceRequired { get; set; }

    [Display(Name = "Discrepancy")]
    public Decimal LowerDisperency { get; set; }

    [Display(Name = "Midline Deviated By")]
    public int LowerMidlineDeviation { get; set; }

    [Display(Name = "Side By")]
    public Decimal LowerMidlinemm { get; set; }

    [Display(Name = "Right Side")]
    public Decimal LowerMolarRight { get; set; }

    [Display(Name = "Left Side")]
    public Decimal LowerMolarLeft { get; set; }

    [Display(Name = "Right Side")]
    public Decimal LowerSpaceNeedRight { get; set; }

    [Display(Name = "Left Side")]
    public Decimal LowerSpaceNeedLeft { get; set; }

    public int SagittalStaticId { get; set; }

    public int SagittalId { get; set; }

    [Display(Name = "Measurement")]
    public string PreRxMeasurement { get; set; }

    [Display(Name = "Class")]
    public string PreRxClass { get; set; }

    [Display(Name = "Measurement")]
    public string SurgeryMeasurement { get; set; }

    [Display(Name = "Class")]
    public string SurgeryClass { get; set; }

    [Display(Name = "Measurement")]
    public string PostRxMeasurement { get; set; }

    [Display(Name = "Class")]
    public string PostRxClass { get; set; }

    [Display(Name = "Measurement")]
    public string RetentionMeasurement { get; set; }

    [Display(Name = "Class")]
    public string RetentionClass { get; set; }

    [Display(Name = "Measurement")]
    public string ChangeMeasurement { get; set; }

    [Display(Name = "Class")]
    public string ChangeClass { get; set; }

    [Display(Name = "SN Length")]
    public string SNLength { get; set; }

    [Display(Name = "Basic Upper Lip")]
    public string BasicUpperLip { get; set; }

    [Display(Name = "Soft Tissue Chin")]
    public string SoftTissue { get; set; }

    [Display(Name = "Skeletal")]
    public string Skeletal { get; set; }

    [Display(Name = "Severity")]
    public string Severity { get; set; }

    [Display(Name = "Soft Tissues")]
    public string InferSoftTissue { get; set; }

    public int DiscrepancyStaticId { get; set; }

    public int DiscrepancyId { get; set; }

    [Display(Name = "Fault With")]
    public int InferFault { get; set; }

    [Display(Name = "Size")]
    public string InferSize { get; set; }

    [Display(Name = "Placement")]
    public string InferPlacement { get; set; }

    [Display(Name = "Pre Treatment")]
    public string PreTreatment { get; set; }

    [Display(Name = "Pre Surgical")]
    public string PreSurgical { get; set; }

    [Display(Name = "Post Alignment")]
    public string PostAlignment { get; set; }

    [Display(Name = "Post Surgical")]
    public string PostSurgical { get; set; }

    [Display(Name = "Mid Treatment")]
    public string MidTreatment { get; set; }

    [Display(Name = "Post treatment")]
    public string Posttreatment { get; set; }

    public int SkeletalStaticId { get; set; }

    public int SkeletalId { get; set; }

    public int VerticalStaticId { get; set; }

    public int VerticalId { get; set; }

    public string VerticalSkeletal { get; set; }

    public string VerticalDental { get; set; }

    public string ShortUpperLip { get; set; }

    public string AlveolarIncisors { get; set; }

    public string PalatalCortex { get; set; }

    public string ShymphysealCortex { get; set; }

    public string SagittalNeeded { get; set; }

    public string SagittalVertical { get; set; }

    public string MandMax { get; set; }

    public string SagittalDetails { get; set; }

    public string MandMaxDetails { get; set; }

    public string GrowthMod { get; set; }

    public string Surgery { get; set; }

    public string NormalRelation { get; set; }

    public string Camouflage { get; set; }

    public string NeededDetails { get; set; }

    public string NotNeededDetails { get; set; }

    public string Justifcation { get; set; }

    public string SummarySagittal { get; set; }

    public string SummaryVertical { get; set; }

    public int DentoUpperId { get; set; }

    public int DentoUpperStaticId { get; set; }

    public string MidRx { get; set; }

    [Display(Name = "In the Relation of Cranium and Maxilla")]
    public string CraniumMaxilla { get; set; }

    [Display(Name = "Incisor Retraction Needed")]
    public string IncisorRetraction { get; set; }

    [Display(Name = "For Coumflage Treatment")]
    public string CoumflageTreatment { get; set; }

    [Display(Name = "Corrected Figure for Upper Incisor Retraction")]
    public string CorrectedRetraction { get; set; }

    [Display(Name = "a)Nasolabial Angle")]
    public string NasoAngle { get; set; }

    [Display(Name = "b)Lip Strain")]
    public string LipStrain { get; set; }

    [Display(Name = "c)Lip Thickness")]
    public string LipThickness { get; set; }

    [Display(Name = "d)Lips in Relation to Esthetic Lines")]
    public string LipestheticLines { get; set; }

    public int DentoLowerId { get; set; }

    public int DentoLowerStaticId { get; set; }

    [Display(Name = "In the Relation of Cranium and Maxilla")]
    public string LowerCraniumMaxilla { get; set; }

    [Display(Name = "Incisor Retraction Needed")]
    public string LowerIncisorRetraction { get; set; }

    [Display(Name = "For Coumflage Treatment")]
    public string LowerCoumflageTreatment { get; set; }

    [Display(Name = "Corrected Figure for Upper Incisor Retraction")]
    public string LowerCorrectedRetraction { get; set; }

    [Display(Name = "a)Mentolabial Angle")]
    public string MentolabialAngle { get; set; }

    [Display(Name = "b)Holdaway Ratio")]
    public string HoldawayRatio { get; set; }

    [Display(Name = "c)Lower Lip Thickness")]
    public string LowerLipThickness { get; set; }

    [Display(Name = "d)Lips in Relation to Esthetic Lines")]
    public string LipLowerestheticLines { get; set; }

    public int CompositeId { get; set; }

    public int CompositeStaticId { get; set; }

    public string Actual { get; set; }

    public string CompositeInference { get; set; }

    public int HoldawayStaticId { get; set; }

    public int HoldawayId { get; set; }

    [Display(Name = "Inference")]
    public string HoldInference { get; set; }

    public int ArnettStaticId { get; set; }

    public int ArnettId { get; set; }

    public int CranialMaxStaticId { get; set; }

    public int CranialMaxId { get; set; }

    [Display(Name = "Maxilla to Cranial Base")]
    public string CranialMaxMeasurements { get; set; }

    [Display(Name = "Normal")]
    public string CranialMaxMean { get; set; }

    [Display(Name = "Pt Value")]
    public string PtValue { get; set; }

    public int CranialManStaticId { get; set; }

    public int CranialManId { get; set; }

    [Display(Name = "Mandible to Cranial Base")]
    public string CranialManMeasurements { get; set; }

    [Display(Name = "Normal")]
    public string CranialManMean { get; set; }

    public int MaxManStaticId { get; set; }

    public int MaxManId { get; set; }

    public int GrowthStaticId { get; set; }

    public int GrowthId { get; set; }

    public int SoftTissueStaticId { get; set; }

    public int SoftTissueId { get; set; }

    public int RickettsStaticId { get; set; }

    public int RickettsId { get; set; }

    public string Angles { get; set; }

    [Display(Name = "Mean At 9 Years")]
    public string MeanAt { get; set; }

    [Display(Name = "Age Change")]
    public string AgeChange { get; set; }

    public string During { get; set; }

    public int BjroksStaticId { get; set; }

    public int BjroksId { get; set; }

    public IEnumerable<MASCode> MidlineList { get; set; }

    public IEnumerable<MASCode> UpperLipList { get; set; }

    public IEnumerable<MASCode> SoftTissueList { get; set; }

    public IEnumerable<MASCode> SkeletalList { get; set; }

    public IEnumerable<MASCode> SeverityList { get; set; }

    public IEnumerable<MASCode> InferSoftTissueList { get; set; }

    public IEnumerable<MASCode> SkeletSagittalList { get; set; }

    public IEnumerable<MASCode> SkeletAlterationList { get; set; }

    public IEnumerable<ORTHOCasesheetProperties> Proplist { get; set; }

    [Display(Name = "Yes")]
    public bool rdnNasolabialAngleYes { get; set; }

    [Display(Name = "No")]
    public bool rdnNasolabialAngleNo { get; set; }

    [Display(Name = "Yes")]
    public bool rdnLipStrainYes { get; set; }

    [Display(Name = "No")]
    public bool rdnLipStrainNo { get; set; }

    [Display(Name = "Yes")]
    public bool rdnLipThicknessYes { get; set; }

    [Display(Name = "No")]
    public bool rdnLipThicknessNo { get; set; }

    [Display(Name = "Yes")]
    public bool rdnLipestheticLinesYes { get; set; }

    [Display(Name = "No")]
    public bool rdnLipestheticLinesNo { get; set; }

    [Display(Name = "Yes")]
    public bool rdnMentolabialYes { get; set; }

    [Display(Name = "No")]
    public bool rdnMentolabialNo { get; set; }

    [Display(Name = "Yes")]
    public bool rdnHoldawayRatioYes { get; set; }

    [Display(Name = "No")]
    public bool rdnHoldawayRatioNo { get; set; }

    [Display(Name = "Yes")]
    public bool rdnLowerLipThicknessYes { get; set; }

    [Display(Name = "No")]
    public bool rdnLowerLipThicknessNo { get; set; }

    [Display(Name = "Yes")]
    public bool rdnLipLowerestheticLinesYes { get; set; }

    [Display(Name = "No")]
    public bool rdnLipLowerestheticLinesNo { get; set; }

    [Display(Name = "Tooth No")]
    public string ToothNo { get; set; }

    [Display(Name = "RT")]
    public string UpperRt { get; set; }

    [Display(Name = "LT")]
    public string UpperLt { get; set; }

    [Display(Name = "RT")]
    public string LowerRt { get; set; }

    [Display(Name = "LT")]
    public string LowerLt { get; set; }

    [Display(Name = "1")]
    public string ToothNo1 { get; set; }

    [Display(Name = "2")]
    public string ToothNo2 { get; set; }

    [Display(Name = "3")]
    public string ToothNo3 { get; set; }

    [Display(Name = "4")]
    public string ToothNo4 { get; set; }

    [Display(Name = "5")]
    public string ToothNo5 { get; set; }

    [Display(Name = "6")]
    public string ToothNo6 { get; set; }

    [Display(Name = "Male 0 mm")]
    public string WitsMale { get; set; }

    [Display(Name = "Female -1 mm")]
    public string WitsFemale { get; set; }

    [Display(Name = "Sum of Mesio")]
    public Decimal MoyerMesio { get; set; }

    [Display(Name = " Distal width of lower Incisor ")]
    public Decimal MoyerIncisor { get; set; }

    [Display(Name = "Total")]
    public Decimal SumMoyerIncisor { get; set; }

    [Display(Name = "Predicted width of 3,4,5 ")]
    public Decimal MoyerWidth { get; set; }

    [Display(Name = "Space available for 3,4,5 ")]
    public Decimal MoyerSpace { get; set; }

    [Display(Name = "mesiodistal width of four lower Incisor ")]
    public Decimal TanakaMesiodistal { get; set; }

    [Display(Name = "Estimated width of mandibular canine & premolars ")]
    public Decimal TanakaMandibular { get; set; }

    [Display(Name = "meriodistal width of four lower Incisors ")]
    public Decimal TanakaMeriodistal { get; set; }

    [Display(Name = "Estimated width of maxillary canine & premolars")]
    public Decimal TanakaMaxillary { get; set; }

    [Display(Name = "Actual width of primary molars")]
    public Decimal ActualWidth { get; set; }

    [Display(Name = "Radiographic width of primary molars")]
    public Decimal RadiographicWidth { get; set; }

    [Display(Name = "Actual width of unerupted premolar   ")]
    public Decimal RadiographicPremolar { get; set; }

    public long OpNo { get; set; }

    public string PatientName { get; set; }

    public string AgeGender { get; set; }

    public int Age { get; set; }

    public int GenderId { get; set; }

    public string Area { get; set; }

    [Display(Name = "Phone")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; }

    [Display(Name = "Analysis Name")]
    public string DownsAnalysisName { get; set; }

    [Display(Name = "Inference")]
    public string DownsInference { get; set; }

    [Display(Name = "Analysis Name")]
    public string SchwarzAnalysisName { get; set; }

    [Display(Name = "Inference")]
    public string SchwarzInference { get; set; }

    [Display(Name = "Analysis Name")]
    public string RakosiAnalysisName { get; set; }

    [Display(Name = "Inference")]
    public string RakosiInference { get; set; }

    [Display(Name = "Analysis Name")]
    public string HardAnalysisName { get; set; }

    [Display(Name = "Inference")]
    public string HardInference { get; set; }

    [Display(Name = "Analysis Name")]
    public string SoftAnalysisName { get; set; }

    [Display(Name = "Inference")]
    public string SoftInference { get; set; }

    [Display(Name = "Analysis Name")]
    public string GrummonsAnalysisName { get; set; }

    [Display(Name = "Inference")]
    public string GrummonsInference { get; set; }

    [Display(Name = "Analysis Name")]
    public string EstheticsAnalysisName { get; set; }

    [Display(Name = "Inference")]
    public string EstheticsInference { get; set; }

    [Display(Name = "Analysis Name")]
    public string HoldawayAnalysisName { get; set; }

    [Display(Name = "Inference")]
    public string HoldawayInference { get; set; }

    [Display(Name = "Analysis Name")]
    public string TweedsAnalysisName { get; set; }

    [Display(Name = "Inference")]
    public string TweedsInference { get; set; }

    [Display(Name = "Analysis Name")]
    public string RickettsAnalysisName { get; set; }

    [Display(Name = "Inference")]
    public string RickettsInference { get; set; }

    [Display(Name = "Analysis Name")]
    public string McNamaraAnalysisName { get; set; }

    [Display(Name = "Inference")]
    public string McNamaraInference { get; set; }

    [Display(Name = "Analysis Name")]
    public string WitsAnalysisName { get; set; }

    [Display(Name = "Inference")]
    public string WitsInference { get; set; }

    [Display(Name = "Analysis Name")]
    public string LinderAnalysisName { get; set; }

    [Display(Name = "Inference")]
    public string LinderInference { get; set; }

    [Display(Name = "Analysis Name")]
    public string MoyerAnalysisName { get; set; }

    [Display(Name = "Inference")]
    public string MoyerInference { get; set; }

    [Display(Name = "Analysis Name")]
    public string TanakaAnalysisName { get; set; }

    [Display(Name = "Inference")]
    public string TanakaInference { get; set; }

    [Display(Name = "Analysis Name")]
    public string RadiographicAnalysisName { get; set; }

    [Display(Name = "Inference")]
    public string RadiographicInference { get; set; }

    public List<OrthoAnalysisViewModal> SteinerAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> DownsAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> SchwarzAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> McNamaraAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> TweedsAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> RakosiAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> BurstoneHardAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> BurstoneSoftAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> GrummonsAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> EstheticsAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> SagittalAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> DiscrepancyAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> SKeletalAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> DivergenceAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> VerticalAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> DentoUpperAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> DentoLowerAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> CompositeAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> HoldawayAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> ArnettAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> MaxillaAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> MandibleAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> MaxtoMandAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> GrowthAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> SoftTissueAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> RickettsAnalysisList { get; set; }

    public IEnumerable<OrthoAnalysisViewModal> BjroksAnalysisList { get; set; }

    public PatientInformationViewModel patientInformationViewModel { get; set; }
  }
}
