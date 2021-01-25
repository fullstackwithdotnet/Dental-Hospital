// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.PHDViewModel
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
  public class PHDViewModel : EntityBase
  {
    [PrimaryKey]
    public int PHDId { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int PatientId { get; set; }

    public string MandatoryDummy { get; set; }

    public string PHDNo { get; set; }

    public DateTime PHDDate { get; set; }

    [Display(Name = "Chief Complaint")]
    public string ChiefComplaintName { get; set; }

    [DataType(DataType.MultilineText)]
    public string ChiefComplaint { get; set; }

    [Display(Name = "Provisional Diagnosis")]
    [DataType(DataType.MultilineText)]
    public string ProvisionalDiagnosis { get; set; }

    [Display(Name = "Differential Diagnosis")]
    [DataType(DataType.MultilineText)]
    public string DifferentialDiagnosis { get; set; }

    [Display(Name = "Investigations")]
    [DataType(DataType.MultilineText)]
    public string Investigations { get; set; }

    [Display(Name = "Investigations Report")]
    [DataType(DataType.MultilineText)]
    public string InvestigationsReport { get; set; }

    [Display(Name = "Final Diagnosis")]
    [DataType(DataType.MultilineText)]
    public string FinalDiagnosis { get; set; }

    public string AbbreviationOthers { get; set; }

    [Display(Name = "Health Education Counselling")]
    [DataType(DataType.MultilineText)]
    public string HealthEducation { get; set; }

    [DataType(DataType.MultilineText)]
    [Display(Name = "Flouride Application")]
    public string FlourideApplication { get; set; }

    [Display(Name = "Sealants")]
    [DataType(DataType.MultilineText)]
    public string Sealants { get; set; }

    [Display(Name = "Preventive ResinRestoration")]
    [DataType(DataType.MultilineText)]
    public string PreventiveResinRestoration { get; set; }

    [Display(Name = "Extraction")]
    [DataType(DataType.MultilineText)]
    public string Extraction { get; set; }

    [Display(Name = "Comprehensive Treatment")]
    [DataType(DataType.MultilineText)]
    public string ComprehensiveTreatment { get; set; }

    [Display(Name = "Museum Visit")]
    [DataType(DataType.MultilineText)]
    public string MuseumVisit { get; set; }

    [Display(Name = "Treatment Done At Camp")]
    [DataType(DataType.MultilineText)]
    public string TreatmentDoneAtCamp { get; set; }

    [Display(Name = "Treatment Done At Satellite Center")]
    [DataType(DataType.MultilineText)]
    public string TreatmentDoneAtSatelliteCenter { get; set; }

    [Display(Name = "Tobacco cessation counselling")]
    [DataType(DataType.MultilineText)]
    public string TobaccoCessationCounsellingDesc { get; set; }

    [Display(Name = "Diet Name")]
    public string DietName { get; set; }

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

    [Display(Name = "Emergency Phase")]
    public string EmergencyPhase { get; set; }

    [Display(Name = "Etiotropic Phase")]
    public string EtiotropicPhase { get; set; }

    [Display(Name = "Surgical Phase")]
    public string SurgicalPhase { get; set; }

    [Display(Name = "Restorative Phase")]
    public string RestorativePhase { get; set; }

    [Display(Name = "Corrective Phase")]
    public string CorrectivePhase { get; set; }

    [Display(Name = "Maintanance Phase")]
    public string MaintanancePhase { get; set; }

    [Display(Name = "Emergency Phase")]
    public string ComEmergencyPhase { get; set; }

    [Display(Name = "Promotive Phase")]
    public string PromotivePhase { get; set; }

    [Display(Name = "Preventive Phase")]
    public string PreventivePhase { get; set; }

    [Display(Name = "Curative Phase")]
    public string CurativePhase { get; set; }

    [Display(Name = "Rehabilitative Phase")]
    public string RehabilitativePhase { get; set; }

    [Display(Name = "Maintanance Phase")]
    public string ComMaintanancePhase { get; set; }

    public string Total { get; set; }

    public Decimal? CITotal { get; set; }

    public Decimal? DITotal { get; set; }

    public Decimal? OHITotal { get; set; }

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

    public string RecordInference { get; set; }

    public string DMFS55_1 { get; set; }

    public string DMFS55_2 { get; set; }

    public string DMFS55_3 { get; set; }

    public string DMFS55_4 { get; set; }

    public string DMFS55_5 { get; set; }

    public string DMFS54_1 { get; set; }

    public string DMFS54_2 { get; set; }

    public string DMFS54_3 { get; set; }

    public string DMFS54_4 { get; set; }

    public string DMFS54_5 { get; set; }

    public string DMFS53_1 { get; set; }

    public string DMFS53_2 { get; set; }

    public string DMFS53_3 { get; set; }

    public string DMFS53_4 { get; set; }

    public string DMFS52_1 { get; set; }

    public string DMFS52_2 { get; set; }

    public string DMFS52_3 { get; set; }

    public string DMFS52_4 { get; set; }

    public string DMFS51_1 { get; set; }

    public string DMFS51_2 { get; set; }

    public string DMFS51_3 { get; set; }

    public string DMFS51_4 { get; set; }

    public string DMFS61_1 { get; set; }

    public string DMFS61_2 { get; set; }

    public string DMFS61_3 { get; set; }

    public string DMFS61_4 { get; set; }

    public string DMFS62_1 { get; set; }

    public string DMFS62_2 { get; set; }

    public string DMFS62_3 { get; set; }

    public string DMFS62_4 { get; set; }

    public string DMFS63_1 { get; set; }

    public string DMFS63_2 { get; set; }

    public string DMFS63_3 { get; set; }

    public string DMFS63_4 { get; set; }

    public string DMFS64_1 { get; set; }

    public string DMFS64_2 { get; set; }

    public string DMFS64_3 { get; set; }

    public string DMFS64_4 { get; set; }

    public string DMFS64_5 { get; set; }

    public string DMFS65_1 { get; set; }

    public string DMFS65_2 { get; set; }

    public string DMFS65_3 { get; set; }

    public string DMFS65_4 { get; set; }

    public string DMFS65_5 { get; set; }

    public string DMFS18_1 { get; set; }

    public string DMFS18_2 { get; set; }

    public string DMFS18_3 { get; set; }

    public string DMFS18_4 { get; set; }

    public string DMFS18_5 { get; set; }

    public string DMFS17_1 { get; set; }

    public string DMFS17_2 { get; set; }

    public string DMFS17_3 { get; set; }

    public string DMFS17_4 { get; set; }

    public string DMFS17_5 { get; set; }

    public string DMFS16_1 { get; set; }

    public string DMFS16_2 { get; set; }

    public string DMFS16_3 { get; set; }

    public string DMFS16_4 { get; set; }

    public string DMFS16_5 { get; set; }

    public string DMFS15_1 { get; set; }

    public string DMFS15_2 { get; set; }

    public string DMFS15_3 { get; set; }

    public string DMFS15_4 { get; set; }

    public string DMFS15_5 { get; set; }

    public string DMFS14_1 { get; set; }

    public string DMFS14_2 { get; set; }

    public string DMFS14_3 { get; set; }

    public string DMFS14_4 { get; set; }

    public string DMFS14_5 { get; set; }

    public string DMFS13_1 { get; set; }

    public string DMFS13_2 { get; set; }

    public string DMFS13_3 { get; set; }

    public string DMFS13_4 { get; set; }

    public string DMFS12_1 { get; set; }

    public string DMFS12_2 { get; set; }

    public string DMFS12_3 { get; set; }

    public string DMFS12_4 { get; set; }

    public string DMFS11_1 { get; set; }

    public string DMFS11_2 { get; set; }

    public string DMFS11_3 { get; set; }

    public string DMFS11_4 { get; set; }

    public string DMFS21_1 { get; set; }

    public string DMFS21_2 { get; set; }

    public string DMFS21_3 { get; set; }

    public string DMFS21_4 { get; set; }

    public string DMFS22_1 { get; set; }

    public string DMFS22_2 { get; set; }

    public string DMFS22_3 { get; set; }

    public string DMFS22_4 { get; set; }

    public string DMFS23_1 { get; set; }

    public string DMFS23_2 { get; set; }

    public string DMFS23_3 { get; set; }

    public string DMFS23_4 { get; set; }

    public string DMFS24_1 { get; set; }

    public string DMFS24_2 { get; set; }

    public string DMFS24_3 { get; set; }

    public string DMFS24_4 { get; set; }

    public string DMFS24_5 { get; set; }

    public string DMFS25_1 { get; set; }

    public string DMFS25_2 { get; set; }

    public string DMFS25_3 { get; set; }

    public string DMFS25_4 { get; set; }

    public string DMFS25_5 { get; set; }

    public string DMFS26_1 { get; set; }

    public string DMFS26_2 { get; set; }

    public string DMFS26_3 { get; set; }

    public string DMFS26_4 { get; set; }

    public string DMFS26_5 { get; set; }

    public string DMFS27_1 { get; set; }

    public string DMFS27_2 { get; set; }

    public string DMFS27_3 { get; set; }

    public string DMFS27_4 { get; set; }

    public string DMFS27_5 { get; set; }

    public string DMFS28_1 { get; set; }

    public string DMFS28_2 { get; set; }

    public string DMFS28_3 { get; set; }

    public string DMFS28_4 { get; set; }

    public string DMFS28_5 { get; set; }

    public string DMFS48_1 { get; set; }

    public string DMFS48_2 { get; set; }

    public string DMFS48_3 { get; set; }

    public string DMFS48_4 { get; set; }

    public string DMFS48_5 { get; set; }

    public string DMFS47_1 { get; set; }

    public string DMFS47_2 { get; set; }

    public string DMFS47_3 { get; set; }

    public string DMFS47_4 { get; set; }

    public string DMFS47_5 { get; set; }

    public string DMFS46_1 { get; set; }

    public string DMFS46_2 { get; set; }

    public string DMFS46_3 { get; set; }

    public string DMFS46_4 { get; set; }

    public string DMFS46_5 { get; set; }

    public string DMFS45_1 { get; set; }

    public string DMFS45_2 { get; set; }

    public string DMFS45_3 { get; set; }

    public string DMFS45_4 { get; set; }

    public string DMFS45_5 { get; set; }

    public string DMFS44_1 { get; set; }

    public string DMFS44_2 { get; set; }

    public string DMFS44_3 { get; set; }

    public string DMFS44_4 { get; set; }

    public string DMFS44_5 { get; set; }

    public string DMFS43_1 { get; set; }

    public string DMFS43_2 { get; set; }

    public string DMFS43_3 { get; set; }

    public string DMFS43_4 { get; set; }

    public string DMFS42_1 { get; set; }

    public string DMFS42_2 { get; set; }

    public string DMFS42_3 { get; set; }

    public string DMFS42_4 { get; set; }

    public string DMFS41_1 { get; set; }

    public string DMFS41_2 { get; set; }

    public string DMFS41_3 { get; set; }

    public string DMFS41_4 { get; set; }

    public string DMFS31_1 { get; set; }

    public string DMFS31_2 { get; set; }

    public string DMFS31_3 { get; set; }

    public string DMFS31_4 { get; set; }

    public string DMFS32_1 { get; set; }

    public string DMFS32_2 { get; set; }

    public string DMFS32_3 { get; set; }

    public string DMFS32_4 { get; set; }

    public string DMFS33_1 { get; set; }

    public string DMFS33_2 { get; set; }

    public string DMFS33_3 { get; set; }

    public string DMFS33_4 { get; set; }

    public string DMFS34_1 { get; set; }

    public string DMFS34_2 { get; set; }

    public string DMFS34_3 { get; set; }

    public string DMFS34_4 { get; set; }

    public string DMFS34_5 { get; set; }

    public string DMFS35_1 { get; set; }

    public string DMFS35_2 { get; set; }

    public string DMFS35_3 { get; set; }

    public string DMFS35_4 { get; set; }

    public string DMFS35_5 { get; set; }

    public string DMFS36_1 { get; set; }

    public string DMFS36_2 { get; set; }

    public string DMFS36_3 { get; set; }

    public string DMFS36_4 { get; set; }

    public string DMFS36_5 { get; set; }

    public string DMFS37_1 { get; set; }

    public string DMFS37_2 { get; set; }

    public string DMFS37_3 { get; set; }

    public string DMFS37_4 { get; set; }

    public string DMFS37_5 { get; set; }

    public string DMFS38_1 { get; set; }

    public string DMFS38_2 { get; set; }

    public string DMFS38_3 { get; set; }

    public string DMFS38_4 { get; set; }

    public string DMFS38_5 { get; set; }

    public string DMFS85_1 { get; set; }

    public string DMFS85_2 { get; set; }

    public string DMFS85_3 { get; set; }

    public string DMFS85_4 { get; set; }

    public string DMFS85_5 { get; set; }

    public string DMFS84_1 { get; set; }

    public string DMFS84_2 { get; set; }

    public string DMFS84_3 { get; set; }

    public string DMFS84_4 { get; set; }

    public string DMFS84_5 { get; set; }

    public string DMFS83_1 { get; set; }

    public string DMFS83_2 { get; set; }

    public string DMFS83_3 { get; set; }

    public string DMFS83_4 { get; set; }

    public string DMFS82_1 { get; set; }

    public string DMFS82_2 { get; set; }

    public string DMFS82_3 { get; set; }

    public string DMFS82_4 { get; set; }

    public string DMFS81_1 { get; set; }

    public string DMFS81_2 { get; set; }

    public string DMFS81_3 { get; set; }

    public string DMFS81_4 { get; set; }

    public string DMFS71_1 { get; set; }

    public string DMFS71_2 { get; set; }

    public string DMFS71_3 { get; set; }

    public string DMFS71_4 { get; set; }

    public string DMFS72_1 { get; set; }

    public string DMFS72_2 { get; set; }

    public string DMFS72_3 { get; set; }

    public string DMFS72_4 { get; set; }

    public string DMFS73_1 { get; set; }

    public string DMFS73_2 { get; set; }

    public string DMFS73_3 { get; set; }

    public string DMFS73_4 { get; set; }

    public string DMFS74_1 { get; set; }

    public string DMFS74_2 { get; set; }

    public string DMFS74_3 { get; set; }

    public string DMFS74_4 { get; set; }

    public string DMFS74_5 { get; set; }

    public string DMFS75_1 { get; set; }

    public string DMFS75_2 { get; set; }

    public string DMFS75_3 { get; set; }

    public string DMFS75_4 { get; set; }

    public string DMFS75_5 { get; set; }

    public string DMFT55 { get; set; }

    public string DMFT54 { get; set; }

    public string DMFT53 { get; set; }

    public string DMFT52 { get; set; }

    public string DMFT51 { get; set; }

    public string DMFT61 { get; set; }

    public string DMFT62 { get; set; }

    public string DMFT63 { get; set; }

    public string DMFT64 { get; set; }

    public string DMFT65 { get; set; }

    public string DMFT18 { get; set; }

    public string DMFT17 { get; set; }

    public string DMFT16 { get; set; }

    public string DMFT15 { get; set; }

    public string DMFT14 { get; set; }

    public string DMFT13 { get; set; }

    public string DMFT12 { get; set; }

    public string DMFT11 { get; set; }

    public string DMFT21 { get; set; }

    public string DMFT22 { get; set; }

    public string DMFT23 { get; set; }

    public string DMFT24 { get; set; }

    public string DMFT25 { get; set; }

    public string DMFT26 { get; set; }

    public string DMFT27 { get; set; }

    public string DMFT28 { get; set; }

    public string DMFT48 { get; set; }

    public string DMFT47 { get; set; }

    public string DMFT46 { get; set; }

    public string DMFT45 { get; set; }

    public string DMFT44 { get; set; }

    public string DMFT43 { get; set; }

    public string DMFT42 { get; set; }

    public string DMFT41 { get; set; }

    public string DMFT31 { get; set; }

    public string DMFT32 { get; set; }

    public string DMFT33 { get; set; }

    public string DMFT34 { get; set; }

    public string DMFT35 { get; set; }

    public string DMFT36 { get; set; }

    public string DMFT37 { get; set; }

    public string DMFT38 { get; set; }

    public string DMFT85 { get; set; }

    public string DMFT84 { get; set; }

    public string DMFT83 { get; set; }

    public string DMFT82 { get; set; }

    public string DMFT81 { get; set; }

    public string DMFT71 { get; set; }

    public string DMFT72 { get; set; }

    public string DMFT73 { get; set; }

    public string DMFT74 { get; set; }

    public string DMFT75 { get; set; }

    public string DMFTDT { get; set; }

    public string DMFTMT { get; set; }

    public string DMFTFT { get; set; }

    public string DMFTTotal { get; set; }

    public string DMFSScore { get; set; }

    public string CPITNCPI1 { get; set; }

    public string CPITNCPI2 { get; set; }

    public string CPITNCPI3 { get; set; }

    public string CPITNCPI4 { get; set; }

    public string CPITNCPI5 { get; set; }

    public string CPITNCPI6 { get; set; }

    public string TN1 { get; set; }

    public string TN2 { get; set; }

    public string TN3 { get; set; }

    public string TN4 { get; set; }

    public string TN5 { get; set; }

    public string TN6 { get; set; }

    public string LOACPI1 { get; set; }

    public string LOACPI2 { get; set; }

    public string LOACPI3 { get; set; }

    public string LOACPI4 { get; set; }

    public string LOACPI5 { get; set; }

    public string LOACPI6 { get; set; }

    public string LOA1 { get; set; }

    public string LOA2 { get; set; }

    public string LOA3 { get; set; }

    public string LOA4 { get; set; }

    public string LOA5 { get; set; }

    public string LOA6 { get; set; }

    public string DMFSDT { get; set; }

    public string DMFSFT { get; set; }

    public string DMFSMT { get; set; }

    public string DMFSTotal { get; set; }

    public IEnumerable<SelectListItem> DietLister { get; set; }

    public IEnumerable<PHDCasesheetProperties> Proplist { get; set; }

    public OMRPHDCasesheetViewModel omrPHDCasesheetViewModel { get; set; }

    public IEnumerable<MASChiefComplaint> ChiefComplaintlist { get; set; }

    public int OMRId { get; set; }

    public bool IsCampPatient { get; set; }

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

    public long TreatmentReferredId { get; set; }

    public IEnumerable<FileUploadViewModel> FileUploadlist { get; set; }

    public MedicalAlertViewModel medicalalertviewmodel { get; set; }

    public ApprovalViewModal approvalViewModal { get; set; }

    public PrescriptionsViewModel PrescriptionsList { get; set; }

    public IEnumerable<PrescriptionsViewModel> PrescriptionsDetails { get; set; }

    public IEnumerable<PrescriptionsViewModel> PreviousPrescriptionList { get; set; }

    public IEnumerable<BillingViewModal> deptTreatmentList { get; set; }

    public RadioRegistrationViewModel radioRegistrationviewmodel { get; set; }

    public LaboratoryRegistrationViewModel laboratoryRegistrationviewmodel { get; set; }

    public IEnumerable<PHDViewModel> Treatmentlist { get; set; }

    [Display(Name = "Date")]
    public string PHDDateDisplay { get; set; }

    public long OpNo { get; set; }

    public string PatientName { get; set; }

    public string AgeGender { get; set; }

    public int Age { get; set; }

    public int GenderId { get; set; }

    public string Area { get; set; }

    [Display(Name = "Phone")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; }

    public PHDViewModel()
    {
      if (this.DietLister != null)
        return;
      this.DietLister = ((IEnumerable<string>) Enum.GetNames(typeof (Diet))).Select<string, SelectListItem>((Func<string, SelectListItem>) (name => new SelectListItem()
      {
        Text = name,
        Value = name
      }));
    }

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

    [Display(Name = "Form")]
    public string LiquidsForm { get; set; }

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

    public string ParafunctionalHabits { get; set; }

    public string Consistency { get; set; }

    public string Group { get; set; }

    [Display(Name = "Others")]
    public string ParafunctionalHabitsOthers { get; set; }

    public string BrushingHabitsMethod { get; set; }

    public string BrushingHabitsFreq { get; set; }

    public string BrushingHabitsDur { get; set; }

    public string ChangingBrushFreq { get; set; }

    public string DentifriceType { get; set; }

    public string BrushType { get; set; }

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

    [Display(Name = "Over Jet")]
    public int? OverJetId { get; set; }

    [Display(Name = "Cross Bite")]
    public string CrossBite { get; set; }

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

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public Diet Diet { get; set; }
  }
}
