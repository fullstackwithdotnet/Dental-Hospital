// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.PROSCDViewModel
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
  public class PROSCDViewModel : EntityBase
  {
    private DateTime _PrimaryImprDate = DateTime.Now;
    [Display(Name = "Date")]
    private DateTime _PreProsPhaseDate = DateTime.Now;
    private DateTime _MakingOfDisgnosDate = DateTime.Now;
    private DateTime _DesignOfCustomDate = DateTime.Now;
    private DateTime _PreprationOfCustomDate = DateTime.Now;
    private DateTime _BorderMoldingDate = DateTime.Now;
    private DateTime _FinalImpressionDate = DateTime.Now;
    private DateTime _MakingOfMasterCastsDate = DateTime.Now;
    private DateTime _TrialDentureBasedDate = DateTime.Now;
    private DateTime _MaxilloMandibularDate = DateTime.Now;
    private DateTime _TransferOfRecArticuarDate = DateTime.Now;
    private DateTime _SelectionOfTeethDate = DateTime.Now;
    private DateTime _ArrangementOfTeethDate = DateTime.Now;
    private DateTime _AnteriorTryDate = DateTime.Now;
    private DateTime _PosteriorTryDate = DateTime.Now;
    private DateTime _LaboratoryRemDate = DateTime.Now;
    private DateTime _FinishingPolisDate = DateTime.Now;
    private DateTime _ClinicalRemDate = DateTime.Now;
    private DateTime _DentureInsertionDate = DateTime.Now;

    [PrimaryKey]
    [HiddenInput(DisplayValue = false)]
    public int ProsthoCDId { get; set; }

    public string ProsthoCDNo { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int PatientId { get; set; }

    public string MandatoryDummy { get; set; }

    public DateTime? ProsthoCDDate { get; set; }

    [Display(Name = "Chief Complaint")]
    public string ChiefComplaintName { get; set; }

    public string ChiefComplaint { get; set; }

    [Display(Name = "Medical History")]
    public string MedicalHistory { get; set; }

    [Display(Name = "Past Dental History")]
    public string PastDentalHist { get; set; }

    [Display(Name = "Cause of Loss of Teeth")]
    public int? CauOfLossOfTeeId { get; set; }

    public string CauOfLossOfTeeOth { get; set; }

    [Display(Name = "Anterior")]
    public string SeMaxillaryAnt { get; set; }

    [Display(Name = "Posterior")]
    public string SeMaxillaryPost { get; set; }

    [Display(Name = "Anterior")]
    public string SeMandibularAnt { get; set; }

    [Display(Name = "Posterior")]
    public string SeMandibularPost { get; set; }

    [Display(Name = "Number")]
    public string PreMaxillaryNum { get; set; }

    [Display(Name = "Type")]
    public string PreMaxillaryType { get; set; }

    [Display(Name = "Number")]
    public string PreMandibularNum { get; set; }

    [Display(Name = "Type")]
    public string PreMandibularTy { get; set; }

    [Display(Name = "Duration of Complete Edentulousness")]
    public string DurOfComplet { get; set; }

    [Display(Name = "Patient's opinion Existing Denture")]
    public string PatientOpinion { get; set; }

    [Display(Name = "Complaints on Existing")]
    public string ComplaintsOnExist { get; set; }

    [Display(Name = "Previous Denture's")]
    public string PreviousDentures { get; set; }

    [Display(Name = "Casts")]
    public string Casts { get; set; }

    [Display(Name = "Measurements")]
    public string Measurements { get; set; }

    [Display(Name = "Old dentures")]
    public string OldDentures { get; set; }

    [Display(Name = "Built")]
    public int? BuiltId { get; set; }

    [Display(Name = "Nutritional status")]
    public int? NutritionalStatusId { get; set; }

    [Display(Name = "Psychological attitude")]
    public int? PsychologicalAttId { get; set; }

    [Display(Name = "Habits")]
    public int? HabitsId { get; set; }

    public string HabitsOther { get; set; }

    [Display(Name = "Patient's Expectancy")]
    public int? PatientExpectId { get; set; }

    [Display(Name = "Motivation for Denture")]
    public int? MotiForDentureId { get; set; }

    [Display(Name = "Facial symmetry")]
    public int? FacialSymmetryId { get; set; }

    [Display(Name = "Temporomandibular Joint")]
    public int? TemporomanJointId { get; set; }

    [Display(Name = "Skin Complexion")]
    public int? SkinComplexionId { get; set; }

    [Display(Name = "Facial Form")]
    public int? FacialFormId { get; set; }

    [Display(Name = "Facial profile")]
    public int? FacialProfileId { get; set; }

    [Display(Name = "Muscle tone")]
    public int? MuscleToneId { get; set; }

    [Display(Name = "Neuromuscular control")]
    public int? NeuromControlId { get; set; }

    [Display(Name = "Mouth opening")]
    public string MouthOpening { get; set; }

    [Display(Name = "Length")]
    public int? LengthId { get; set; }

    [Display(Name = "Tonicity")]
    public int? TonicityId { get; set; }

    [Display(Name = "Lip competency")]
    public int? lipCompetId { get; set; }

    [Display(Name = "Lip thickness")]
    public int? LipThicknessId { get; set; }

    [Display(Name = "Lip contact")]
    public int? LipContactId { get; set; }

    [Display(Name = "Lip support")]
    public int? LipSupportId { get; set; }

    [Display(Name = "Others")]
    public string Others { get; set; }

    [Display(Name = "Nasolabial fold")]
    public int? NasolabialFoldId { get; set; }

    [Display(Name = "Philtrum")]
    public int? PhiltrumId { get; set; }

    [Display(Name = "Speech")]
    public int? SpeechId { get; set; }

    [Display(Name = "Buccal")]
    public int? BuccalId { get; set; }

    [Display(Name = "Floor of Mouth")]
    public int? FloorOfMouthId { get; set; }

    [Display(Name = "Palate Hard")]
    public int? PalateHardId { get; set; }

    [Display(Name = "Soft")]
    public int? SoftId { get; set; }

    [Display(Name = "Palatal Vault")]
    public int? PalatalVaultId { get; set; }

    [Display(Name = "Palatine Torus")]
    public int? PalatineTorusId { get; set; }

    public int? TorusUniBilateralId { get; set; }

    [Display(Name = "Required Surgery")]
    public string RequiredSurgery { get; set; }

    [Display(Name = "Required Surgery")]
    public string RequiredSurgerySevere { get; set; }

    [Display(Name = "Severe UnderCuts")]
    public int? SevereUnderCutsId { get; set; }

    public int? UndercutsUniBilateralId { get; set; }

    [Display(Name = "Soft Palate")]
    public int? SoftPalateId { get; set; }

    [Display(Name = "Width of Posterior Palatal Seal")]
    public int? WidthOfPosterId { get; set; }

    [Display(Name = "Palatal Sensitivity")]
    public int? PalatalSensitivityId { get; set; }

    [Display(Name = "Tongue Size")]
    public int? TongueSizeId { get; set; }

    [Display(Name = "Tongue Position")]
    public int? TonguePositionId { get; set; }

    [Display(Name = "Genial Tubercles")]
    public int? GenialTuberclesId { get; set; }

    [Display(Name = "Lateral Throat Form")]
    public int? LateralThroatId { get; set; }

    [Display(Name = "Saliva quality")]
    public int? SalivaQualityId { get; set; }

    [Display(Name = "Saliva quantity")]
    public int? SalivaQuantityId { get; set; }

    [Display(Name = "Inter ridge distance")]
    public string InterridgeDis { get; set; }

    public string Labial { get; set; }

    public string Buccal { get; set; }

    public string Left { get; set; }

    public string Right { get; set; }

    public string Lingual { get; set; }

    [Display(Name = "Single")]
    public string MaxSingleLabial { get; set; }

    public string MaxSingleBuccalLeft { get; set; }

    public string MaxSingleBuccalRight { get; set; }

    [Display(Name = "Multiple")]
    public string MaxMultipleLabial { get; set; }

    public string MaxMultipleBucLeft { get; set; }

    public string MaxMultipleBucRight { get; set; }

    [Display(Name = "Prominent")]
    public string MaxProminentLabial { get; set; }

    public string MaxProminentBucLeft { get; set; }

    public string MaxProminentBucRight { get; set; }

    [Display(Name = "Not Prominent")]
    public string MaxNotPromLabial { get; set; }

    public string MaxNotPromLeft { get; set; }

    public string MaxNotPromRight { get; set; }

    [Display(Name = "Close to Crest")]
    public string MaxCloseToLabial { get; set; }

    public string MaxCloseToCrestLeft { get; set; }

    public string MaxCloseToCrestRight { get; set; }

    [Display(Name = "Ant Other")]
    public string MaxAnyOtherLabial { get; set; }

    public string MaxAnyOtherLeft { get; set; }

    public string MaxAnyOtherRight { get; set; }

    [Display(Name = "Arch Form")]
    public int? MaxArchFormId { get; set; }

    [Display(Name = "Height Anterior")]
    public int? MaxHeightAntId { get; set; }

    [Display(Name = "Height Posterior")]
    public int? MaxHeightPostId { get; set; }

    [Display(Name = "Width Anterior")]
    public int? MaxWidthAnteriorId { get; set; }

    [Display(Name = "Width Posterior")]
    public int? MaxWidthPosteriorId { get; set; }

    [Display(Name = "Undercuts")]
    public int? MaxUndercutsId { get; set; }

    public string MaxUnderLocation { get; set; }

    [Display(Name = "Bony Prominence")]
    public int? MaxBonyProminId { get; set; }

    public string MaxBonyProLocation { get; set; }

    [Display(Name = "Hypermobile Tissue")]
    public int? MaxHypermobileId { get; set; }

    public string MaxHypermobLocation { get; set; }

    [Display(Name = "Mucosa")]
    public int? MaxMucosaId { get; set; }

    [Display(Name = "Special Features")]
    public string MaxSpecialFeatures { get; set; }

    [Display(Name = "Single")]
    public string ManSingleLabial { get; set; }

    public string ManSingleBuccalLeft { get; set; }

    public string ManSingleBuccalRight { get; set; }

    public string ManSingleLingu { get; set; }

    [Display(Name = "Multiple")]
    public string ManMultipleLabial { get; set; }

    public string ManMultipleBucLeft { get; set; }

    public string ManMultipleBucRight { get; set; }

    public string ManMultipleLingu { get; set; }

    [Display(Name = "Prominent")]
    public string ManProminentLabial { get; set; }

    public string ManProminentBucLeft { get; set; }

    public string ManProminentBucRight { get; set; }

    public string ManProminentLingu { get; set; }

    [Display(Name = "Not Prominent")]
    public string ManNotPromLabial { get; set; }

    public string ManNotPromLeft { get; set; }

    public string ManNotPromRight { get; set; }

    public string ManNotPromLingu { get; set; }

    [Display(Name = "Close to Crest")]
    public string ManCloseToLabial { get; set; }

    public string ManCloseToCrestLeft { get; set; }

    public string ManCloseToCrestRight { get; set; }

    public string ManCloseToLingu { get; set; }

    [Display(Name = "Any other")]
    public string ManAnyOtherLabial { get; set; }

    public string ManAnyOtherLeft { get; set; }

    public string ManAnyOtherRight { get; set; }

    public string ManAnyOtherLingu { get; set; }

    [Display(Name = "Arch Form")]
    public int? ManArchFormId { get; set; }

    [Display(Name = "Height Anterior")]
    public int? ManHeightAntId { get; set; }

    [Display(Name = "Height Posterior")]
    public int? ManHeightPostId { get; set; }

    [Display(Name = "Width Anterior")]
    public int? ManWidthAnteriorId { get; set; }

    [Display(Name = "Width Posterior")]
    public int? ManWidthPosteriorId { get; set; }

    [Display(Name = "Undercuts")]
    public int? ManUndercutsId { get; set; }

    public string ManUnderLocation { get; set; }

    [Display(Name = "Bony Prominence")]
    public int? ManBonyProminId { get; set; }

    public string ManBonyProLocation { get; set; }

    [Display(Name = "Hypermobile tissue")]
    public int? ManHypermobileId { get; set; }

    public string ManHypermobLocation { get; set; }

    [Display(Name = "Mucosa")]
    public int? ManMucosaId { get; set; }

    [Display(Name = "Special Features")]
    public string ManSpecialFeatures { get; set; }

    [Display(Name = "Mylohyoid Ridge")]
    public int? ManMylohyoidRidId { get; set; }

    [Display(Name = "Hygiene")]
    public int? HygieneId { get; set; }

    [Display(Name = "Condition of Base")]
    public int? ConditionBaseId { get; set; }

    [Display(Name = "Condition of Teeth")]
    public int? ConditionTeethId { get; set; }

    [Display(Name = "Esthetics")]
    public int? EstheticsId { get; set; }

    [Display(Name = "Phonetics")]
    public int? PhoneticsId { get; set; }

    [Display(Name = "Retention")]
    public int? RetentionId { get; set; }

    [Display(Name = "Stability")]
    public int? StabilityId { get; set; }

    [Display(Name = "Support")]
    public int? SupportId { get; set; }

    [Display(Name = "Acceptable")]
    public string Acceptable { get; set; }

    [Display(Name = "Unacceptable")]
    public string Unacceptable { get; set; }

    [Display(Name = "Adaptation of Base")]
    public string AdaAccep { get; set; }

    public string AdaUnaccep { get; set; }

    [Display(Name = "Centric occlusion")]
    public string CenAccep { get; set; }

    public string CenUnaccep { get; set; }

    [Display(Name = "Vertical Dimension")]
    public string VertAccep { get; set; }

    public string VertUnaccep { get; set; }

    [Display(Name = "Inadequate")]
    public string VerInadequate { get; set; }

    [Display(Name = "Excessive")]
    public string VerExcessive { get; set; }

    [Display(Name = "Patient Education/Motivation")]
    public string PatientEducation { get; set; }

    [Display(Name = "Local")]
    public string TreatPlanLocal { get; set; }

    [Display(Name = "General")]
    public string TreatPlanGeneral { get; set; }

    [Display(Name = "Relief required Area")]
    public string RelirfRequiredArea { get; set; }

    [Display(Name = "Primary Impression")]
    public string PrimaryImpression { get; set; }

    [Display(Name = "Corrective Master Impression")]
    public string SecondaryImpression { get; set; }

    [Display(Name = "Denture Base")]
    public string DentureBase { get; set; }

    [Display(Name = "Orientation Jaw Relation")]
    public string OrientationJaw { get; set; }

    [Display(Name = "Vertical Jaw Relation")]
    public string VerticalJaw { get; set; }

    [Display(Name = "Horizontal Jaw Relation")]
    public string HorizontalJaw { get; set; }

    [Display(Name = "Occlusion")]
    public string Occlusion { get; set; }

    [Display(Name = "Mould")]
    public string Mould { get; set; }

    [Display(Name = "Shade")]
    public string Shade { get; set; }

    [Display(Name = "Type")]
    public string Type { get; set; }

    [Display(Name = "Specify for Any Other Materials/Techniques")]
    public string AnyOtherMaterials { get; set; }

    [Display(Name = "Prognosis")]
    public int? PrognosisId { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime PrimaryImprDate
    {
      get
      {
        return this._PrimaryImprDate;
      }
      set
      {
        this._PrimaryImprDate = value;
      }
    }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime PreProsPhaseDate
    {
      get
      {
        return this._PreProsPhaseDate;
      }
      set
      {
        this._PreProsPhaseDate = value;
      }
    }

    [Display(Name = "Type of Tray")]
    public string PriMaxTypeOfTray { get; set; }

    [Display(Name = "Tray No")]
    public string PriMaxTrayNo { get; set; }

    [Display(Name = "Material")]
    public string PriMaxMaterial { get; set; }

    [Display(Name = "Type of Tray")]
    public string PriManTypeOfTray { get; set; }

    [Display(Name = "Tray No")]
    public string PriManTrayNo { get; set; }

    [Display(Name = "Material")]
    public string PriManMaterial { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime MakingOfDisgnosDate
    {
      get
      {
        return this._MakingOfDisgnosDate;
      }
      set
      {
        this._MakingOfDisgnosDate = value;
      }
    }

    public string MakingOfDisgnos { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime DesignOfCustomDate
    {
      get
      {
        return this._DesignOfCustomDate;
      }
      set
      {
        this._DesignOfCustomDate = value;
      }
    }

    public string DesignOfCustom { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime PreprationOfCustomDate
    {
      get
      {
        return this._PreprationOfCustomDate;
      }
      set
      {
        this._PreprationOfCustomDate = value;
      }
    }

    public string PreprationOfCustom { get; set; }

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

    [Display(Name = "Material")]
    public string BorderMaterial { get; set; }

    [Display(Name = "Method")]
    public string BorderMethod { get; set; }

    [Display(Name = "PPS Design")]
    public string BorderPPS { get; set; }

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

    [Display(Name = "Material")]
    public string FinaMaxMaterial { get; set; }

    [Display(Name = "Technique")]
    public string FinaMaxTechnique { get; set; }

    [Display(Name = "Material")]
    public string FinaManMaterial { get; set; }

    [Display(Name = "Technique")]
    public string FinaManTechnique { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime MakingOfMasterCastsDate
    {
      get
      {
        return this._MakingOfMasterCastsDate;
      }
      set
      {
        this._MakingOfMasterCastsDate = value;
      }
    }

    public string MakingOfMasterCasts { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime TrialDentureBasedDate
    {
      get
      {
        return this._TrialDentureBasedDate;
      }
      set
      {
        this._TrialDentureBasedDate = value;
      }
    }

    public string TrialDentureBased { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime MaxilloMandibularDate
    {
      get
      {
        return this._MaxilloMandibularDate;
      }
      set
      {
        this._MaxilloMandibularDate = value;
      }
    }

    [Display(Name = "Labial Fullness")]
    public string LabialFullness { get; set; }

    [Display(Name = "Visibility")]
    public string Visibility { get; set; }

    [Display(Name = "Orientation of Anterior Plane")]
    public string OrientOfAnteriorPlane { get; set; }

    [Display(Name = "Orientation of Posterior Plane")]
    public string OrientOfPosteriorPlane { get; set; }

    [Display(Name = "a.Orientation jaw Relation of face bow Record to Articulatior")]
    public string OrientjawRelation { get; set; }

    [Display(Name = "Vertical dimension at rest")]
    public string VerticalDimAtRest { get; set; }

    [Display(Name = "Vertical dimension at occulsion")]
    public string VerticalDimAtocculsion { get; set; }

    [Display(Name = "Interocclusal distance")]
    public string InterocclusalDistance { get; set; }

    [Display(Name = "VDR")]
    public string VDR { get; set; }

    [Display(Name = "VDO")]
    public string VDO { get; set; }

    [Display(Name = "Method")]
    public string CentricRelationMethod { get; set; }

    [Display(Name = "Material")]
    public string CentricRelationMaterial { get; set; }

    [Display(Name = "Verification of centric jaw Relation")]
    public string VerifiOfCentricjaw { get; set; }

    [Display(Name = "Protrusive")]
    public string Protrusive { get; set; }

    [Display(Name = "Right Lateral")]
    public string RightLateral { get; set; }

    [Display(Name = "Left Lateral")]
    public string LeftLateral { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime TransferOfRecArticuarDate
    {
      get
      {
        return this._TransferOfRecArticuarDate;
      }
      set
      {
        this._TransferOfRecArticuarDate = value;
      }
    }

    [Display(Name = "Horizontal")]
    public string ConHorizontalRight { get; set; }

    public string ConHorizontalLeft { get; set; }

    [Display(Name = "Lateral")]
    public string ConLeteralRight { get; set; }

    public string ConLeteralLeft { get; set; }

    [Display(Name = "Horizontal")]
    public string IncHorizontalRight { get; set; }

    public string IncHorizontalLeft { get; set; }

    [Display(Name = "Lateral")]
    public string IncLeteralRight { get; set; }

    public string IncLeteralLeft { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime SelectionOfTeethDate
    {
      get
      {
        return this._SelectionOfTeethDate;
      }
      set
      {
        this._SelectionOfTeethDate = value;
      }
    }

    [Display(Name = "Brand Name")]
    public string BrandAnterior { get; set; }

    public string BrandPosterior { get; set; }

    [Display(Name = "Type")]
    public string TypeAnterior { get; set; }

    public string TypePosterior { get; set; }

    [Display(Name = "Material")]
    public string MaterialAnterior { get; set; }

    public string MaterialPosterior { get; set; }

    [Display(Name = "Shade")]
    public string ShadeAnterior { get; set; }

    public string ShadePosterior { get; set; }

    [Display(Name = "Size")]
    public string SizeAnterior { get; set; }

    public string SizePosterior { get; set; }

    [Display(Name = "Shape")]
    public string ShapeAnterior { get; set; }

    public string ShapePosterior { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime ArrangementOfTeethDate
    {
      get
      {
        return this._ArrangementOfTeethDate;
      }
      set
      {
        this._ArrangementOfTeethDate = value;
      }
    }

    [Display(Name = "Occlusion")]
    public string ArrangementOcclusion { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime AnteriorTryDate
    {
      get
      {
        return this._AnteriorTryDate;
      }
      set
      {
        this._AnteriorTryDate = value;
      }
    }

    [Display(Name = "Comfort")]
    public string AntComfort { get; set; }

    [Display(Name = "Retention")]
    public string AntRetention { get; set; }

    [Display(Name = "Stability")]
    public string AntStability { get; set; }

    [Display(Name = "Overjet")]
    public string AntOverjet { get; set; }

    [Display(Name = "Overbite")]
    public string AntOverbite { get; set; }

    [Display(Name = "Esthetics")]
    public string AntEsthetics { get; set; }

    [Display(Name = "Phonetics")]
    public string AntPhonetics { get; set; }

    [Display(Name = "Prostrusive contact")]
    public string AntProstrusiveContact { get; set; }

    [Display(Name = "Acceptance")]
    public string AntAcceptance { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime PosteriorTryDate
    {
      get
      {
        return this._PosteriorTryDate;
      }
      set
      {
        this._PosteriorTryDate = value;
      }
    }

    [Display(Name = "Comfort")]
    public string PosComfort { get; set; }

    [Display(Name = "Vertical relation")]
    public string PosVerticalRelation { get; set; }

    [Display(Name = "Centric occlusion")]
    public string PosCentricOcclusion { get; set; }

    [Display(Name = "Eccentric occlusion")]
    public string PosEccentricOcclusion { get; set; }

    [Display(Name = "Interocclusal distance")]
    public string PosInterocclusalDistance { get; set; }

    [Display(Name = "Acceptance")]
    public string PosAcceptance { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    [Display(Name = "Date")]
    public DateTime LaboratoryRemDate
    {
      get
      {
        return this._LaboratoryRemDate;
      }
      set
      {
        this._LaboratoryRemDate = value;
      }
    }

    public string LaboratoryRem { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime FinishingPolisDate
    {
      get
      {
        return this._FinishingPolisDate;
      }
      set
      {
        this._FinishingPolisDate = value;
      }
    }

    public string FinishingPolis { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime ClinicalRemDate
    {
      get
      {
        return this._ClinicalRemDate;
      }
      set
      {
        this._ClinicalRemDate = value;
      }
    }

    public string ClinicalRem { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime DentureInsertionDate
    {
      get
      {
        return this._DentureInsertionDate;
      }
      set
      {
        this._DentureInsertionDate = value;
      }
    }

    public string DentureInsertion { get; set; }

    public string Diagnosis { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string Maxillary { get; set; }

    public string Mandibular { get; set; }

    public string Anterior { get; set; }

    public string Posterior { get; set; }

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

    public IEnumerable<PROSCDCasesheetProperties> Proplist { get; set; }

    public IEnumerable<MASCode> CauseOfLossOfTeethlist { get; set; }

    public IEnumerable<MASCode> BuiltList { get; set; }

    public IEnumerable<MASCode> NutritionalStatusList { get; set; }

    public IEnumerable<MASCode> PsycologicalAttList { get; set; }

    public IEnumerable<MASCode> ProsHabitsList { get; set; }

    public IEnumerable<MASCode> patientExpectList { get; set; }

    public IEnumerable<MASCode> MotiForDentureList { get; set; }

    public IEnumerable<MASCode> FacialSymmetryList { get; set; }

    public IEnumerable<MASCode> TemporomanJointList { get; set; }

    public IEnumerable<MASCode> SkinComplexionList { get; set; }

    public IEnumerable<MASCode> FacialFormList { get; set; }

    public IEnumerable<MASCode> FacialProfileList { get; set; }

    public IEnumerable<MASCode> MuscleToneIdList { get; set; }

    public IEnumerable<MASCode> NeuromControlList { get; set; }

    public IEnumerable<MASCode> LengthList { get; set; }

    public IEnumerable<MASCode> TonicityList { get; set; }

    public IEnumerable<MASCode> lipCompetList { get; set; }

    public IEnumerable<MASCode> LipThicknessList { get; set; }

    public IEnumerable<MASCode> LipContactList { get; set; }

    public IEnumerable<MASCode> LipSupportList { get; set; }

    public IEnumerable<MASCode> NasolabialFoldList { get; set; }

    public IEnumerable<MASCode> PhiltrumList { get; set; }

    public IEnumerable<MASCode> SpeechList { get; set; }

    public IEnumerable<MASCode> BuccalList { get; set; }

    public IEnumerable<MASCode> FloorOfMouthList { get; set; }

    public IEnumerable<MASCode> PalateHardList { get; set; }

    public IEnumerable<MASCode> SoftList { get; set; }

    public IEnumerable<MASCode> PalatalVaultList { get; set; }

    public IEnumerable<MASCode> PalatineTorusList { get; set; }

    public IEnumerable<MASCode> PalatineTorusPresentList { get; set; }

    public IEnumerable<MASCode> SoftPalateList { get; set; }

    public IEnumerable<MASCode> WidthOfPosterList { get; set; }

    public IEnumerable<MASCode> PalatalSensitivityList { get; set; }

    public IEnumerable<MASCode> TongueSizeList { get; set; }

    public IEnumerable<MASCode> TonguePositionList { get; set; }

    public IEnumerable<MASCode> GenialTuberclesList { get; set; }

    public IEnumerable<MASCode> LateralThroatIdList { get; set; }

    public IEnumerable<MASCode> SalivaQualityList { get; set; }

    public IEnumerable<MASCode> SalivaQuantityList { get; set; }

    public IEnumerable<MASCode> MaxArchFormList { get; set; }

    public IEnumerable<MASCode> MaxHeightAntList { get; set; }

    public IEnumerable<MASCode> MaxHeightPostList { get; set; }

    public IEnumerable<MASCode> MaxWidthAnteriorList { get; set; }

    public IEnumerable<MASCode> MaxWidthPosteriorList { get; set; }

    public IEnumerable<MASCode> MaxUndercutsList { get; set; }

    public IEnumerable<MASCode> MaxBonyProminList { get; set; }

    public IEnumerable<MASCode> MaxHypermobileList { get; set; }

    public IEnumerable<MASCode> MaxMucosaList { get; set; }

    public IEnumerable<MASCode> ManArchFormList { get; set; }

    public IEnumerable<MASCode> ManHeightAntList { get; set; }

    public IEnumerable<MASCode> ManHeightPostList { get; set; }

    public IEnumerable<MASCode> ManWidthAnteriorList { get; set; }

    public IEnumerable<MASCode> ManWidthPosteriorList { get; set; }

    public IEnumerable<MASCode> ManUndercutsList { get; set; }

    public IEnumerable<MASCode> ManBonyProminList { get; set; }

    public IEnumerable<MASCode> ManHypermobileList { get; set; }

    public IEnumerable<MASCode> ManMucosaList { get; set; }

    public IEnumerable<MASCode> ManMylohyoidRidList { get; set; }

    public IEnumerable<MASCode> HygieneList { get; set; }

    public IEnumerable<MASCode> TorusUniBilateralList { get; set; }

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

    public string MaxCloseToLeft { get; set; }

    [Display(Name = "Cause of loss of Teeth")]
    public string CauOfLossOfTee { get; set; }

    [Display(Name = "Built")]
    public string Built { get; set; }

    [Display(Name = "Nutritional Status")]
    public string NutritionalStatus { get; set; }

    [Display(Name = "Psychological Attitude")]
    public string PsychologicalAtt { get; set; }

    [Display(Name = "Habits")]
    public string Habits { get; set; }

    [Display(Name = "Patient's Expectancy")]
    public string PatientExpect { get; set; }

    [Display(Name = "Motivation for Denture")]
    public string MotiForDenture { get; set; }

    [Display(Name = "Facial Symmetry")]
    public string FacialSymmetry { get; set; }

    [Display(Name = "Temporomandibular Joint")]
    public string TemporomanJoint { get; set; }

    [Display(Name = "Skin Complexion")]
    public string SkinComplexion { get; set; }

    [Display(Name = "Facial Form")]
    public string FacialForm { get; set; }

    [Display(Name = "Facial Profile")]
    public string FacialProfile { get; set; }

    [Display(Name = "Muscle Tone")]
    public string MuscleTone { get; set; }

    [Display(Name = "Neuromuscular Control")]
    public string NeuromControl { get; set; }

    [Display(Name = "Length")]
    public string Length { get; set; }

    [Display(Name = "Tonicity")]
    public string Tonicity { get; set; }

    [Display(Name = "Lip Competency")]
    public string lipCompet { get; set; }

    [Display(Name = "Lip Thickness")]
    public string LipThickness { get; set; }

    [Display(Name = "Lip Contact")]
    public string LipContact { get; set; }

    [Display(Name = "Lip Support")]
    public string LipSupport { get; set; }

    [Display(Name = "Nasolabial Fold")]
    public string NasolabialFold { get; set; }

    [Display(Name = "Philtrum")]
    public string Philtrum { get; set; }

    [Display(Name = "Speech")]
    public string Speech { get; set; }

    [Display(Name = "Floor of Mouth")]
    public string FloorOfMouth { get; set; }

    [Display(Name = "Palate Hard")]
    public string PalateHard { get; set; }

    [Display(Name = "Soft")]
    public string Soft { get; set; }

    [Display(Name = "Palatal Vault")]
    public string PalatalVault { get; set; }

    [Display(Name = "Palatine Torus")]
    public string PalatineTorus { get; set; }

    public string PalatineTorusPresent { get; set; }

    [Display(Name = "Soft Palate")]
    public string SoftPalate { get; set; }

    [Display(Name = "Width of Posterior Palatal Seal")]
    public string WidthOfPoster { get; set; }

    [Display(Name = "Palatal Sensitivity")]
    public string PalatalSensitivity { get; set; }

    [Display(Name = "Tongue Size")]
    public string TongueSize { get; set; }

    [Display(Name = "Tongue Position")]
    public string TonguePosition { get; set; }

    [Display(Name = "Genial Tubercles")]
    public string GenialTubercles { get; set; }

    [Display(Name = "Lateral Throat Form")]
    public string LateralThroat { get; set; }

    [Display(Name = "Saliva Quality")]
    public string SalivaQuality { get; set; }

    [Display(Name = "Saliva Quantity")]
    public string SalivaQuantity { get; set; }

    [Display(Name = "Arch Form")]
    public string MaxArchForm { get; set; }

    [Display(Name = "Height Anterior")]
    public string MaxHeightAnt { get; set; }

    [Display(Name = "Height Posterior")]
    public string MaxHeightPost { get; set; }

    [Display(Name = "Width Anterior")]
    public string MaxWidthAnterior { get; set; }

    [Display(Name = "Width Posterior")]
    public string MaxWidthPosterior { get; set; }

    [Display(Name = "Undercuts")]
    public string MaxUndercuts { get; set; }

    [Display(Name = "Bony Prominence")]
    public string MaxBonyPromin { get; set; }

    [Display(Name = "Hypermobile Tissue")]
    public string MaxHypermobile { get; set; }

    [Display(Name = "Mucosa")]
    public string MaxMucosa { get; set; }

    [Display(Name = "Arch Form")]
    public string ManArchForm { get; set; }

    [Display(Name = "Height Anterior")]
    public string ManHeightAnt { get; set; }

    [Display(Name = "Height Posterior")]
    public string ManHeightPost { get; set; }

    [Display(Name = "Width Anterior")]
    public string ManWidthAnterior { get; set; }

    [Display(Name = "Width Posterior")]
    public string ManWidthPosterior { get; set; }

    [Display(Name = "Undercuts")]
    public string ManUndercuts { get; set; }

    [Display(Name = "Bony Prominence")]
    public string ManBonyPromin { get; set; }

    [Display(Name = "Hypermobile Tissue")]
    public string ManHypermobile { get; set; }

    [Display(Name = "Mucosa")]
    public string ManMucosa { get; set; }

    [Display(Name = "Mylohyoid Ridge")]
    public string ManMylohyoidRid { get; set; }

    [Display(Name = "Hygiene")]
    public string Hygiene { get; set; }

    [Display(Name = "Condition of Base")]
    public string ConditionBase { get; set; }

    [Display(Name = "Condition of Teeth")]
    public string ConditionTeeth { get; set; }

    [Display(Name = "Esthetics")]
    public string Esthetics { get; set; }

    [Display(Name = "Phonetics")]
    public string Phonetics { get; set; }

    [Display(Name = "Retention")]
    public string Retention { get; set; }

    [Display(Name = "Stability")]
    public string Stability { get; set; }

    [Display(Name = "Support")]
    public string Support { get; set; }

    [Display(Name = "Built")]
    public string Prognosis { get; set; }

    public long OpNo { get; set; }

    public string PatientName { get; set; }

    public string Phone { get; set; }

    public string AgeGender { get; set; }

    public string Area { get; set; }

    public int Age { get; set; }

    public int GenderId { get; set; }

    [Display(Name = "Date")]
    public string PrimaryImprDateDisplay { get; set; }

    [Display(Name = "Date")]
    public string MakingOfDisgnosDateDisplay { get; set; }

    [Display(Name = "Date")]
    public string DesignOfCustomDateDisplay { get; set; }

    [Display(Name = "Date")]
    public string PreprationOfCustomDateDisplay { get; set; }

    [Display(Name = "Date")]
    public string BorderMoldingDateDisplayDisplay { get; set; }

    [Display(Name = "Date")]
    public string FinalImpressioDisplayDisplay { get; set; }

    [Display(Name = "Date")]
    public string MakingOfMasterCastsDateDisplay { get; set; }

    [Display(Name = "Date")]
    public string TrialDentureBasedDateDisplay { get; set; }

    [Display(Name = "Date")]
    public string MaxilloMandibularDateDisplay { get; set; }

    [Display(Name = "Date")]
    public string TransferOfRecArticuarDateDisplay { get; set; }

    [Display(Name = "Date")]
    public string SelectionOfTeethDateDisplay { get; set; }

    [Display(Name = "Date")]
    public string ArrangementOfTeethDateDisplay { get; set; }

    [Display(Name = "Date")]
    public string AnteriorTryDateDisplay { get; set; }

    [Display(Name = "Date")]
    public string PosteriorTryDateDisplay { get; set; }

    [Display(Name = "Date")]
    public string LaboratoryRemDateDisplay { get; set; }

    [Display(Name = "Date")]
    public string FinishingPolisDateDisplay { get; set; }

    [Display(Name = "Date")]
    public string ClinicalRemDateDisplay { get; set; }

    [Display(Name = "Date")]
    public string DentureInsertionDateDisplay { get; set; }

    public MedicalAlertViewModel medicalalertviewmodel { get; set; }

    public PrescriptionsViewModel PrescriptionsList { get; set; }

    public IEnumerable<PrescriptionsViewModel> PrescriptionsDetails { get; set; }

    public IEnumerable<PrescriptionsViewModel> PreviousPrescriptionList { get; set; }

    public RadioRegistrationViewModel radioRegistrationviewmodel { get; set; }

    public LaboratoryRegistrationViewModel laboratoryRegistrationviewmodel { get; set; }

    public ApprovalViewModal approvalViewModal { get; set; }

    public IEnumerable<BillingViewModal> deptTreatmentList { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }
  }
}
