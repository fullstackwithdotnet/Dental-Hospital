// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.PROSDIMViewModel
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
  public class PROSDIMViewModel : EntityBase
  {
    private DateTime _DiagnosisTpDiscDate = DateTime.Now;
    private DateTime _TreatmentOptionSelDate = DateTime.Now;
    private DateTime _PreMedicationDate = DateTime.Now;
    private DateTime _PostMedicationDate = DateTime.Now;
    private DateTime _ImplantPlacementDate = DateTime.Now;
    private DateTime _SutureRemovalDate = DateTime.Now;
    private DateTime _SecondStageSurgeryDate = DateTime.Now;
    private DateTime _ImpressionImpAbutDate = DateTime.Now;
    private DateTime _JigTrialDate = DateTime.Now;
    private DateTime _ProsthesisInsertionDate = DateTime.Now;
    private DateTime _ReviewDate = DateTime.Now;
    private DateTime _TryInDate = DateTime.Now;

    [PrimaryKey]
    public int ProsthoDIMId { get; set; }

    public string ProsthoDIMNo { get; set; }

    public int PatientId { get; set; }

    public string MandatoryDummy { get; set; }

    public DateTime? ProsthoDIMDate { get; set; }

    [Display(Name = "Chief Complaint")]
    public string ChiefComplaintName { get; set; }

    public string ChiefComplaint { get; set; }

    [Display(Name = "History of Present Illness")]
    public string HistoryOfPresent { get; set; }

    [Display(Name = "Dental History")]
    public string DentalHistory { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    [Display(Name = "Clenching")]
    public int? ClenchingId { get; set; }

    public string Clenching { get; set; }

    [Display(Name = "Grinding/Bruxism Type")]
    public int? GrindingBruxismId { get; set; }

    public string GrindingBruxism { get; set; }

    [Display(Name = "Masticatory Muscle Tenderness")]
    public int? MastiMuscleId { get; set; }

    [Display(Name = "Muscles of Mastication")]
    public string MastiMuscle { get; set; }

    [Display(Name = "Biting")]
    public int? BitingId { get; set; }

    public string Biting { get; set; }

    [Display(Name = "Chewing")]
    public int? ChewingId { get; set; }

    public string Chewing { get; set; }

    [Display(Name = "Chewing Area in Mouth")]
    public string ChewingArea { get; set; }

    [Display(Name = "Tongue Thrusting")]
    public string TongueThrusting { get; set; }

    [Display(Name = "Smoking")]
    public string Smoking { get; set; }

    [Display(Name = "Alcohol")]
    public string Alcohol { get; set; }

    [Display(Name = "Diabetes Mellitus")]
    public string DiabetesMellitus { get; set; }

    [Display(Name = "Hypertension")]
    public string Hypertension { get; set; }

    [Display(Name = "Bleeding Disorder")]
    public string BleedingOrder { get; set; }

    [Display(Name = "Thyroid Disorder")]
    public string ThyroidDisorders { get; set; }

    [Display(Name = "Bone Disorder")]
    public string BoneDisorders { get; set; }

    [Display(Name = "Oral Malignancy")]
    public string OralMalignancy { get; set; }

    [Display(Name = "Liver Cirrhosis")]
    public string LiverCirrhosis { get; set; }

    [Display(Name = "Angina Pectoris")]
    public string AnginaPectoris { get; set; }

    [Display(Name = "Myocardial Infarction")]
    public string Myocardialinfarction { get; set; }

    [Display(Name = "Any Radiation in Head and Neck Region")]
    public string AnyRadiationInHead { get; set; }

    [Display(Name = "Rheumatic Disorder")]
    public string RheumaticDisorder { get; set; }

    [Display(Name = "Renal Diseases")]
    public string RenalDiseases { get; set; }

    [Display(Name = "Pregnancy")]
    public string Pregnancy { get; set; }

    [Display(Name = "Subacute Baterial Endocarditis")]
    public string SubacuteBacterial { get; set; }

    [Display(Name = "Current Medications")]
    public string CurrentMedications { get; set; }

    [Display(Name = "Any other Medical Disorder")]
    public string AnyOtherMedical { get; set; }

    [Display(Name = "Facial Symmetry")]
    public string FacialSymmetry { get; set; }

    [Display(Name = "Regional Lymph Nodes")]
    public string LymphNode { get; set; }

    [Display(Name = "TMJ")]
    public string TMJ { get; set; }

    [Display(Name = "Tooth exposure during smile")]
    public int? ToothExposureId { get; set; }

    [Display(Name = "Lips")]
    public string Lips { get; set; }

    [Display(Name = "Mouth opening")]
    public int? MouthOpeningId { get; set; }

    [Display(Name = "Skin Complexion")]
    public string SkinComplexion { get; set; }

    [Display(Name = "Vertical")]
    public int? VerticalId { get; set; }

    [Display(Name = "Horizontal")]
    public string Horizontal { get; set; }

    [Display(Name = "Midline")]
    public string Midline { get; set; }

    [Display(Name = "Halitosis")]
    public string Halitosis { get; set; }

    [Display(Name = "Facial Profile")]
    public string FacialProfile { get; set; }

    [Display(Name = "Facial Form")]
    public string FacialForm { get; set; }

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

    public bool CHKTR4 { get; set; }

    [Display(Name = "4")]
    public string TR4 { get; set; }

    public bool CHKTR3 { get; set; }

    [Display(Name = "3")]
    public string TR3 { get; set; }

    public bool CHKTR2 { get; set; }

    [Display(Name = "2")]
    public string TR2 { get; set; }

    public bool CHKTR1 { get; set; }

    [Display(Name = "1")]
    public string TR1 { get; set; }

    [Display(Name = "Mucosa")]
    public int? MucosaId { get; set; }

    public string Mucosa { get; set; }

    [Display(Name = "Soft Tissue")]
    public int? SoftTissueId { get; set; }

    [Display(Name = "Tongue")]
    public string Tongue { get; set; }

    [Display(Name = "Hard Palate")]
    public string HardPalate { get; set; }

    [Display(Name = "Soft Palate")]
    public string SoftPalate { get; set; }

    [Display(Name = "Floor of Mouth")]
    public string FloorOfMouth { get; set; }

    [Display(Name = "Frenal Attachments")]
    public string FernalAttachments { get; set; }

    [Display(Name = "Muscle Attachments")]
    public string MuscleAttachments { get; set; }

    [Display(Name = "Adequancy of attached Gingiva")]
    public string AdequancyAttached { get; set; }

    [Display(Name = "Tension Test")]
    public string TensionTest { get; set; }

    [Display(Name = "Depth of Vestibule")]
    public int? DepthOfVestibuleId { get; set; }

    [Display(Name = "Vestibule")]
    public string Vestibule { get; set; }

    [Display(Name = "Teeth Present")]
    public string TeethPresent { get; set; }

    [Display(Name = "Missing Teeth")]
    public string MissingTeeth { get; set; }

    [Display(Name = "Loss of Contact")]
    public string LossOfContact { get; set; }

    [Display(Name = "Dental Caries")]
    public string DentalCaries { get; set; }

    [Display(Name = "Restored Teeth")]
    public string RestoredTeeth { get; set; }

    [Display(Name = "Oral Hygiene")]
    public string OralHygiene { get; set; }

    [Display(Name = "Impacted Teeth")]
    public string ImpactedTeeth { get; set; }

    [Display(Name = "Supernumerary Teeth")]
    public string SupernumTeeth { get; set; }

    [Display(Name = "Oral Hanging restorations")]
    public string OralHanging { get; set; }

    [Display(Name = "Attrition")]
    public string Attrition { get; set; }

    [Display(Name = "Abrasion")]
    public string Abrasion { get; set; }

    [Display(Name = "Erosion")]
    public string Erosion { get; set; }

    [Display(Name = "Abfraction")]
    public string Abfraction { get; set; }

    [Display(Name = "Existing occlusion")]
    public string ExistingOcclusion { get; set; }

    [Display(Name = "Overbite")]
    public string Overbite { get; set; }

    [Display(Name = "Overjet")]
    public string Overjet { get; set; }

    [Display(Name = "Cross Bite")]
    public string CrossBite { get; set; }

    [Display(Name = "Open Contacts")]
    public string OpenContacts { get; set; }

    [Display(Name = "Crowding")]
    public string Crowding { get; set; }

    [Display(Name = "Horizontal Facets")]
    public string HorizontalFacets { get; set; }

    [Display(Name = "Angular Facets")]
    public string AngularFacets { get; set; }

    [Display(Name = "Supra Contacts")]
    public string SupraContacts { get; set; }

    [Display(Name = "Plunger Cusp")]
    public string PlungerCusp { get; set; }

    [Display(Name = "Food Impaction")]
    public string FoodImpaction { get; set; }

    [Display(Name = "Trauma from Occlusion(Fremitus Test)")]
    public string Trauma { get; set; }

    [Display(Name = "Slide in Centric")]
    public string SlideInCentric { get; set; }

    [Display(Name = "Buccolingual Width of Ridge")]
    public string Buccolingual { get; set; }

    [Display(Name = "Mesidistal Space")]
    public string MesidistalSpace { get; set; }

    [Display(Name = "Radiograpic Height")]
    public string RadiograpicHeight { get; set; }

    [Display(Name = "OPG")]
    public string OPG { get; set; }

    [Display(Name = "CBCT")]
    public string CBCT { get; set; }

    [Display(Name = "Width of Keratinized soft tissue")]
    public string WidthOfKeratinized { get; set; }

    [Display(Name = "Soft tissue thickness overlying implant site")]
    public string SoftTissueThickness { get; set; }

    [Display(Name = "Inter Maxillary space")]
    public string InterMaxillarySpace { get; set; }

    [Display(Name = "Periodontal Health of adjacent Teeth")]
    public string PeriodontalHealth { get; set; }

    [Display(Name = "Supra Eruption of Opposing Teeth")]
    public string SupraEruption { get; set; }

    [Display(Name = "Drifting of Adjacent Teeth")]
    public string DriftingOfAdjacent { get; set; }

    [Display(Name = "Mouth Opening")]
    public string IPMouthOpening { get; set; }

    [Display(Name = "Others")]
    public string IPOthers { get; set; }

    [Display(Name = "Condition of Bone")]
    public string FindConditionOfBone { get; set; }

    [Display(Name = "Root Inclination of Adjacent Teeth")]
    public string RootInclination { get; set; }

    [Display(Name = "Others")]
    public string FindOthers { get; set; }

    [Display(Name = "Maxillary Sinus")]
    public string MaxillarySinus { get; set; }

    [Display(Name = "Interior Alveolar Canal")]
    public string InteriorAlveolar { get; set; }

    [Display(Name = "Nasal Floor")]
    public string NasalFloor { get; set; }

    [Display(Name = "Position of Incisive Foramen")]
    public string PositionOfIncisive { get; set; }

    [Display(Name = "Position of Mental Foramen")]
    public string PositionOfMental { get; set; }

    [Display(Name = "Adjacent Teeth")]
    public string AdjacentTeeth { get; set; }

    [Display(Name = "Condition of Bone")]
    public string ConditionOfBone { get; set; }

    [Display(Name = "Others")]
    public string Others { get; set; }

    [Display(Name = "CBCT / MDCT")]
    public string CBCTMDCT { get; set; }

    [Display(Name = "Bone Mapping")]
    public string BoneMapping { get; set; }

    [Display(Name = "Actual")]
    public string Actual { get; set; }

    [Display(Name = "Magnifaction Factor")]
    public string MagnifactionFactor { get; set; }

    [Display(Name = "Differential Diagnosis")]
    public string DifferentialDiagnosis { get; set; }

    [Display(Name = "Prognosis")]
    public string Prognosis { get; set; }

    [Display(Name = "Final Diagnosis")]
    public string FinalDiagnosis { get; set; }

    [Display(Name = "Emergency Phase")]
    public string EmergencyPhase { get; set; }

    [Display(Name = "Phase I-Therapy")]
    public string Phase1Therapy { get; set; }

    [Display(Name = "Casts")]
    public string Casts { get; set; }

    [Display(Name = "Not Required")]
    public string NotRequired { get; set; }

    [Display(Name = "Required")]
    public string Required { get; set; }

    [Display(Name = "Type of Graft")]
    public string TypeOfGraft { get; set; }

    [Display(Name = "Quantity of Graft")]
    public string QuantityOfGraft { get; set; }

    [Display(Name = "Type of Prosthesis")]
    public string PhaseIITypeOfPro { get; set; }

    [Display(Name = "Type")]
    public string Type { get; set; }

    [Display(Name = "Trade Names")]
    public string TradeNames { get; set; }

    [Display(Name = "Site")]
    public string Site { get; set; }

    [Display(Name = "Number")]
    public string Number { get; set; }

    [Display(Name = "Diameter")]
    public string Diameter { get; set; }

    [Display(Name = "Length")]
    public string Length { get; set; }

    [Display(Name = "First Stage of Surgery")]
    public string FirstStage { get; set; }

    [Display(Name = "Second Stage of Surgery")]
    public string SecondStage { get; set; }

    [Display(Name = "Type of Impression")]
    public string TypeOfImpression { get; set; }

    [Display(Name = "Work Authorization")]
    public string WorkAuthorization { get; set; }

    [Display(Name = "Type of Abutment")]
    public string TypeOfAbutment { get; set; }

    [Display(Name = "Type of Prosthesis")]
    public string TypeOfProsthesis { get; set; }

    [Display(Name = "Implant Abutment Connection")]
    public string ImplantAbutment { get; set; }

    [Display(Name = "Cement Screw Retained Prosthesis")]
    public string CementScrew { get; set; }

    [Display(Name = "Any Other")]
    public string AnyOther { get; set; }

    [Display(Name = "Maintenance Phase")]
    public string MaintenancePhase { get; set; }

    [Display(Name = "Estimated cost of Treatment")]
    public string EstimatedCost { get; set; }

    [Display(Name = "Diagnostic Impressions")]
    public string DiagnosticImp { get; set; }

    [Display(Name = "Diagnostic Casts")]
    public string DiagnosticCas { get; set; }

    [Display(Name = "stents")]
    public string Stents { get; set; }

    [Display(Name = "Implant Placement")]
    public string ImplantPlacement { get; set; }

    [Display(Name = "Healing Cap Placemen")]
    public string HealingCap { get; set; }

    [Display(Name = "Abutment Placement")]
    public string AbutmentPlacement { get; set; }

    [Display(Name = "Final Impression")]
    public string FinalImpression { get; set; }

    [Display(Name = "Metal Try In")]
    public string MetalTryIn { get; set; }

    [Display(Name = "Bisque Try In")]
    public string BisqueTryIn { get; set; }

    [Display(Name = "Osseous Grafting")]
    public string OsseousGrafting { get; set; }

    [Display(Name = "Soft Tissue Grafting")]
    public string SoftTissueGrafting { get; set; }

    [Display(Name = "Flap/Flaps")]
    public string Flaps { get; set; }

    [Display(Name = "Secondary Per-Mucosal Extension")]
    public string MucosalExtension { get; set; }

    [Display(Name = "Initial Loading")]
    public string InitialLoading { get; set; }

    [Display(Name = "Temporization")]
    public int? TemporizationId { get; set; }

    [Display(Name = "Loading")]
    public int? LoadingId { get; set; }

    [Display(Name = "Abutment Type")]
    public int? AbutmentTypeId { get; set; }

    [Display(Name = "Tray Type")]
    public int? TrayTypeId { get; set; }

    [Display(Name = "Impression Type")]
    public int? ImpressionTypeId { get; set; }

    [Display(Name = "Delivery")]
    public string Delivery { get; set; }

    [Display(Name = "Drug Allergy")]
    public string DrugAllergy { get; set; }

    [Display(Name = "Lips")]
    public int? LipsIncompId { get; set; }

    [Display(Name = "Gingiva")]
    public int? GingivaId { get; set; }

    [Display(Name = "Arch Shape")]
    public string ArchShape { get; set; }

    [Display(Name = "Ridge Configuration")]
    public string RidgeConfiguration { get; set; }

    [Display(Name = "Ridge Relationship")]
    public string RidgeRelationship { get; set; }

    [Display(Name = "Opposing Dentition")]
    public int? OpposingDentitionId { get; set; }

    [Display(Name = "Implant Site")]
    public string ImplantSite { get; set; }

    [Display(Name = "Bone Division")]
    public int? BoneDivisionId { get; set; }

    [Display(Name = "Prosthetic Option")]
    public int? ProstheticOptionId { get; set; }

    [Display(Name = "Inter Arch Space")]
    public string InterArchSpace { get; set; }

    [Display(Name = "Implant Details")]
    public string ImplantDetails { get; set; }

    [Display(Name = "Cardiac Disease")]
    public string CardiacDisease { get; set; }

    [Display(Name = "Etiotrophic Phase")]
    public string EtioTrophicPhase { get; set; }

    [Display(Name = "Surgical Phase")]
    public string SurgicalPhase { get; set; }

    [Display(Name = "Single Stage")]
    public string SingleStage { get; set; }

    [Display(Name = "Two Stage")]
    public string TwoStage { get; set; }

    [Display(Name = "System Used")]
    public string SystemUsed { get; set; }

    [Display(Name = "Impression")]
    public string Impression { get; set; }

    public string DiagnosisTpDisc { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime DiagnosisTpDiscDate
    {
      get
      {
        return this._DiagnosisTpDiscDate;
      }
      set
      {
        this._DiagnosisTpDiscDate = value;
      }
    }

    public string TreatmentOptionSel { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime TreatmentOptionSelDate
    {
      get
      {
        return this._TreatmentOptionSelDate;
      }
      set
      {
        this._TreatmentOptionSelDate = value;
      }
    }

    public string PreMedication { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime PreMedicationDate
    {
      get
      {
        return this._PreMedicationDate;
      }
      set
      {
        this._PreMedicationDate = value;
      }
    }

    public string PostMedication { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime PostMedicationDate
    {
      get
      {
        return this._PostMedicationDate;
      }
      set
      {
        this._PostMedicationDate = value;
      }
    }

    public string ImplantPlacementDesc { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime ImplantPlacementDate
    {
      get
      {
        return this._ImplantPlacementDate;
      }
      set
      {
        this._ImplantPlacementDate = value;
      }
    }

    public string SutureRemoval { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime SutureRemovalDate
    {
      get
      {
        return this._SutureRemovalDate;
      }
      set
      {
        this._SutureRemovalDate = value;
      }
    }

    public string SecondStageSurgery { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime SecondStageSurgeryDate
    {
      get
      {
        return this._SecondStageSurgeryDate;
      }
      set
      {
        this._SecondStageSurgeryDate = value;
      }
    }

    public string ImpressionImpAbut { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime ImpressionImpAbutDate
    {
      get
      {
        return this._ImpressionImpAbutDate;
      }
      set
      {
        this._ImpressionImpAbutDate = value;
      }
    }

    public string JigTrial { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime JigTrialDate
    {
      get
      {
        return this._JigTrialDate;
      }
      set
      {
        this._JigTrialDate = value;
      }
    }

    public string ProsthesisInsertion { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime ProsthesisInsertionDate
    {
      get
      {
        return this._ProsthesisInsertionDate;
      }
      set
      {
        this._ProsthesisInsertionDate = value;
      }
    }

    public string Review { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime ReviewDate
    {
      get
      {
        return this._ReviewDate;
      }
      set
      {
        this._ReviewDate = value;
      }
    }

    public string TryIn { get; set; }

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

    [Display(Name = "Cementation")]
    public string Cementation { get; set; }

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

    public IEnumerable<PROSDIMCasesheetProperties> Proplist { get; set; }

    public IEnumerable<MASChiefComplaint> ChiefComplaintlist { get; set; }

    public IEnumerable<MASCode> Clenchinglist { get; set; }

    public IEnumerable<MASCode> GrindingBruxismlist { get; set; }

    public IEnumerable<MASCode> MastiMusclelist { get; set; }

    public IEnumerable<MASCode> Bitinglist { get; set; }

    public IEnumerable<MASCode> Chewinglist { get; set; }

    public IEnumerable<MASCode> Verticallist { get; set; }

    public IEnumerable<MASCode> Mucosalist { get; set; }

    public IEnumerable<MASCode> SoftTissuelist { get; set; }

    public IEnumerable<MASCode> DepthOfVestibulelist { get; set; }

    public IEnumerable<MASCode> ToothExposurelist { get; set; }

    public IEnumerable<MASCode> MouthOpeninglist { get; set; }

    public IEnumerable<MASCode> TemporizationList { get; set; }

    public IEnumerable<MASCode> LoadingList { get; set; }

    public IEnumerable<MASCode> AbutmentTypeList { get; set; }

    public IEnumerable<MASCode> TrayTypeList { get; set; }

    public IEnumerable<MASCode> ImpressionTypeList { get; set; }

    public IEnumerable<MASCode> LipsIncompList { get; set; }

    public IEnumerable<MASCode> GingivaList { get; set; }

    public IEnumerable<MASCode> OpposingDentitionList { get; set; }

    public IEnumerable<MASCode> BoneDivisionList { get; set; }

    public IEnumerable<MASCode> ProstheticOptionList { get; set; }

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

    [Display(Name = "Clenching")]
    public string ClenchingDisplay { get; set; }

    [Display(Name = "Grinding/Bruxism Type")]
    public string GrindingBruxismDisplay { get; set; }

    [Display(Name = "Masticatory Muscle Tenderness")]
    public string MastiMuscleDisplay { get; set; }

    [Display(Name = "Biting")]
    public string BitingDisplay { get; set; }

    [Display(Name = "Chewing")]
    public string ChewingDisplay { get; set; }

    [Display(Name = "Tooth Exposure during Smile")]
    public string ToothExposure { get; set; }

    [Display(Name = "Vertical")]
    public string Vertical { get; set; }

    [Display(Name = "Mucosa")]
    public string MucosaDisplay { get; set; }

    [Display(Name = "Soft Tissue")]
    public string SoftTissue { get; set; }

    [Display(Name = "Depth of Vestibule")]
    public string DepthOfVestibule { get; set; }

    [Display(Name = "Mouth Opening")]
    public string MouthOpening { get; set; }

    [Display(Name = "Lips")]
    public string LipsIncomp { get; set; }

    [Display(Name = "Gingiva")]
    public string Gingiva { get; set; }

    [Display(Name = "Opposing Dentition")]
    public string OpposingDentition { get; set; }

    [Display(Name = "Bone Division")]
    public string BoneDivision { get; set; }

    [Display(Name = "Temporization")]
    public string Temporization { get; set; }

    [Display(Name = "Loading")]
    public string Loading { get; set; }

    [Display(Name = "Abutment Type")]
    public string AbutmentType { get; set; }

    [Display(Name = "Tray Type")]
    public string TrayType { get; set; }

    [Display(Name = "Impression Type")]
    public string ImpressionType { get; set; }

    public MedicalAlertViewModel medicalalertviewmodel { get; set; }

    public PrescriptionsViewModel PrescriptionsList { get; set; }

    public IEnumerable<PrescriptionsViewModel> PrescriptionsDetails { get; set; }

    public IEnumerable<PrescriptionsViewModel> PreviousPrescriptionList { get; set; }

    public ApprovalViewModal approvalViewModal { get; set; }

    public IEnumerable<BillingViewModal> deptTreatmentList { get; set; }

    public RadioRegistrationViewModel radioRegistrationviewmodel { get; set; }

    public LaboratoryRegistrationViewModel laboratoryRegistrationviewmodel { get; set; }

    [Display(Name = "Prosthetic Option")]
    public string ProstheticOption { get; set; }

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
