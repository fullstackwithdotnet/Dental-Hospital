// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.PROSFPDViewModel
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
  public class PROSFPDViewModel : EntityBase
  {
    private DateTime _FPDDiagnosisTpDiscDate = DateTime.Now;
    private DateTime _FPDTreatmentOptionSelDate = DateTime.Now;
    private DateTime _FPDImplantPlacementDate = DateTime.Now;
    private DateTime _FPDSutureRemovalDate = DateTime.Now;
    private DateTime _FPDSecondStageSurgeryDate = DateTime.Now;
    private DateTime _FPDImpressionImpAbutDate = DateTime.Now;
    private DateTime _FPDJigTrialDate = DateTime.Now;
    private DateTime _FPDProsthesisInsertionDate = DateTime.Now;
    private DateTime _FPDReviewDate = DateTime.Now;

    [PrimaryKey]
    [HiddenInput(DisplayValue = false)]
    public int ProsthoFPDId { get; set; }

    public string ProsthoFPDNo { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int PatientId { get; set; }

    public string MandatoryDummy { get; set; }

    public DateTime? ProsthoFPDDate { get; set; }

    [Display(Name = "Chief Complaint")]
    public string ChiefComplaintName { get; set; }

    public string ChiefComplaint { get; set; }

    [Display(Name = "History of Chief Complaint")]
    public string HistChiefComp { get; set; }

    [Display(Name = "Medical History")]
    public string MedicalHistory { get; set; }

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

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string CreatedSystem { get; set; }

    public string ModifiedSystem { get; set; }

    [Display(Name = "LA Allergy")]
    public string LAAllergy { get; set; }

    public string Reasons { get; set; }

    public string Frequency { get; set; }

    public string Duration { get; set; }

    [Display(Name = "Maxillary")]
    public string MaxillaryReason { get; set; }

    public string MaxillaryDuration { get; set; }

    [Display(Name = "Mandibular")]
    public string MandibularReason { get; set; }

    public string MandibularDuration { get; set; }

    public string PreExtraction { get; set; }

    public string ClenchingDur { get; set; }

    [Display(Name = "Clenching")]
    public string ClenchingFreq { get; set; }

    public string GrindingDur { get; set; }

    [Display(Name = "Grinding")]
    public string GrindingFreq { get; set; }

    public string BruxismDur { get; set; }

    [Display(Name = "Bruxism")]
    public string BruxismFreq { get; set; }

    [Display(Name = "Mastiatory  Muscle Tenderness")]
    public string MasticatoryMusFreq { get; set; }

    public string MasticatoryMusDur { get; set; }

    public string PanChewingDur { get; set; }

    [Display(Name = "Pan Chewing")]
    public string PanChewingFreq { get; set; }

    public string TongueThrustingDur { get; set; }

    [Display(Name = "Tongue Thrusting")]
    public string TongueThrustingFreq { get; set; }

    public string SmokingDur { get; set; }

    [Display(Name = "Smoking")]
    public string SmokingFreq { get; set; }

    public string AlcoholDur { get; set; }

    [Display(Name = "Alcohol")]
    public string AlcoholFreq { get; set; }

    [Display(Name = "Skin Complexion")]
    public string SkinComplexion { get; set; }

    [Display(Name = "Form")]
    public string Form { get; set; }

    [Display(Name = "Profile")]
    public string Profile { get; set; }

    [Display(Name = "Symmetry")]
    public string Symmetry { get; set; }

    [Display(Name = "TMJ")]
    public int? TMJId { get; set; }

    [Display(Name = "Mastic Muscles")]
    public string MasticMuscles { get; set; }

    [Display(Name = "Floor of Mouth")]
    public string FloorOfMouth { get; set; }

    [Display(Name = "Lips")]
    public string Lips { get; set; }

    [Display(Name = "Cheek")]
    public string Cheek { get; set; }

    [Display(Name = "Tongue")]
    public string Tongue { get; set; }

    [Display(Name = "Hard Palate")]
    public string HardPalate { get; set; }

    [Display(Name = "Soft Palate")]
    public string SoftPalate { get; set; }

    [Display(Name = "Saliva")]
    public string Saliva { get; set; }

    [Display(Name = "Edentulous Area")]
    public string EdentulousArea { get; set; }

    [Display(Name = "Number")]
    public string EdenNumber { get; set; }

    [Display(Name = "Location")]
    public string EdenLocation { get; set; }

    [Display(Name = "Span size")]
    public string SpanSize { get; set; }

    [Display(Name = "Residual Alveolar Ridge")]
    public string ResidualAiveolarRid { get; set; }

    [Display(Name = "Shape")]
    public string Shape { get; set; }

    [Display(Name = "Size")]
    public string EdenSize { get; set; }

    [Display(Name = "Mucosa Covering Ridge")]
    public string MucosaCover { get; set; }

    [Display(Name = "Occlusogingival Length")]
    public string OcclusogingLength { get; set; }

    [Display(Name = "Tooth Exposure during Smile")]
    public string ToothExposure { get; set; }

    [Display(Name = "Vertical")]
    public string Vertical { get; set; }

    [Display(Name = "Horizontal")]
    public string Horizontal { get; set; }

    [Display(Name = "Extrusion of opposing Teeth")]
    public string ExtrusionOfOpposing { get; set; }

    [Display(Name = "Oral Hygiene Status")]
    public string OralHygieneStatus { get; set; }

    [Display(Name = "Other Abnormalities")]
    public string OtherAbnormalities { get; set; }

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

    [Display(Name = "Right")]
    public string GroupFunRight { get; set; }

    [Display(Name = "Left")]
    public string GroupFunLeft { get; set; }

    [Display(Name = "Right")]
    public string CaninProtRight { get; set; }

    [Display(Name = "Left")]
    public string CaninProtLeft { get; set; }

    [Display(Name = "Slide in Centric")]
    public string SlideInCentric { get; set; }

    [Display(Name = "Overjet(mm)")]
    public string Overjet { get; set; }

    [Display(Name = "Overbite(mm)")]
    public string Overbite { get; set; }

    [Display(Name = "Others")]
    public string Others { get; set; }

    [Display(Name = "color")]
    public string color { get; set; }

    [Display(Name = "Position and Contour")]
    public string PositionAndCont { get; set; }

    [Display(Name = "Furcation Involvement")]
    public string FurcationInvol { get; set; }

    [Display(Name = "Frenal Attachments")]
    public string FrenalAttach { get; set; }

    [Display(Name = "Sulcus Depth of Mesial ")]
    public string SulcusDepth { get; set; }

    [Display(Name = "Distal Abutments")]
    public string DistalAbutmco { get; set; }

    [Display(Name = "Abutment Teeth")]
    public string AbutmentTeeth { get; set; }

    [Display(Name = "Redentulous Areas")]
    public string RedentulousAreas { get; set; }

    [Display(Name = "Crown-Root Ratio")]
    public string CrownRootRatio { get; set; }

    [Display(Name = "Number of Roots")]
    public string NoOfRoots { get; set; }

    [Display(Name = "Root Morphology")]
    public string RootMorphology { get; set; }

    [Display(Name = "Axial Inclination")]
    public string AxialInclination { get; set; }

    [Display(Name = "Apical Pathology")]
    public string ApicalPathology { get; set; }

    [Display(Name = "Continuity of Lamina Dura")]
    public string ContinuityOflamina { get; set; }

    [Display(Name = "Vertical and Horizontal Bone Loss")]
    public string VerticalAndHoriz { get; set; }

    [Display(Name = "Root Furcation Involvement")]
    public string RootFurcation { get; set; }

    [Display(Name = "Calculus Deposit")]
    public string CalculusDeposit { get; set; }

    [Display(Name = "Status of Root Canal Fillings")]
    public string StatusOfRootCanal { get; set; }

    [Display(Name = "Pupal Morphology")]
    public string PupalMorphology { get; set; }

    [Display(Name = "Status of Restorations")]
    public string StatusOfRestora { get; set; }

    [Display(Name = "Residual Root or Verified Area Underlying Edentulous Space")]
    public string ResidualRoot { get; set; }

    [Display(Name = "Others")]
    public string REOthers { get; set; }

    [Display(Name = "Diagnostic Casts")]
    public string DiagnosticCasts { get; set; }

    [Display(Name = "Diagnosis")]
    public string Diagnosis { get; set; }

    [Display(Name = "Treatment Plan")]
    public string TreatmentPlan { get; set; }

    [Display(Name = "Occluso-gingival")]
    public string OcclusoGingMesial { get; set; }

    public string OcclusoGingDistal { get; set; }

    [Display(Name = "Connector area")]
    public string ConnectorMesial { get; set; }

    public string ConnectorDistal { get; set; }

    [Display(Name = "Mesiodistal")]
    public string MesiodistalMesial { get; set; }

    public string MesiodistalDistal { get; set; }

    [Display(Name = "Buccolingual")]
    public string BuccolingualMesial { get; set; }

    public string BuccolingualDistal { get; set; }

    [Display(Name = "Supra Eruptions")]
    public string SupraEruptions { get; set; }

    [Display(Name = "Tooth Migrations")]
    public string ToothMigrations { get; set; }

    [Display(Name = "Axial Inclination")]
    public string DiagAxialInclination { get; set; }

    [Display(Name = "Wear Facets")]
    public string DiagWearfacets { get; set; }

    [Display(Name = "Classification of FPD")]
    public string ClassificationOfFPD { get; set; }

    [Display(Name = "Other Habits")]
    public string OtherHabits { get; set; }

    public string FPDDiagnosisTpDisc { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime FPDDiagnosisTpDiscDate
    {
      get
      {
        return this._FPDDiagnosisTpDiscDate;
      }
      set
      {
        this._FPDDiagnosisTpDiscDate = value;
      }
    }

    public string FPDTreatmentOptionSel { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime FPDTreatmentOptionSelDate
    {
      get
      {
        return this._FPDTreatmentOptionSelDate;
      }
      set
      {
        this._FPDTreatmentOptionSelDate = value;
      }
    }

    public string FPDImplantPlacementDesc { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime FPDImplantPlacementDate
    {
      get
      {
        return this._FPDImplantPlacementDate;
      }
      set
      {
        this._FPDImplantPlacementDate = value;
      }
    }

    public string FPDSutureRemoval { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime FPDSutureRemovalDate
    {
      get
      {
        return this._FPDSutureRemovalDate;
      }
      set
      {
        this._FPDSutureRemovalDate = value;
      }
    }

    public string FPDSecondStageSurgery { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime FPDSecondStageSurgeryDate
    {
      get
      {
        return this._FPDSecondStageSurgeryDate;
      }
      set
      {
        this._FPDSecondStageSurgeryDate = value;
      }
    }

    public string FPDImpressionImpAbut { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime FPDImpressionImpAbutDate
    {
      get
      {
        return this._FPDImpressionImpAbutDate;
      }
      set
      {
        this._FPDImpressionImpAbutDate = value;
      }
    }

    public string FPDJigTrial { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime FPDJigTrialDate
    {
      get
      {
        return this._FPDJigTrialDate;
      }
      set
      {
        this._FPDJigTrialDate = value;
      }
    }

    public string FPDProsthesisInsertion { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime FPDProsthesisInsertionDate
    {
      get
      {
        return this._FPDProsthesisInsertionDate;
      }
      set
      {
        this._FPDProsthesisInsertionDate = value;
      }
    }

    public string FPDReview { get; set; }

    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    [DataType(DataType.Date)]
    public DateTime FPDReviewDate
    {
      get
      {
        return this._FPDReviewDate;
      }
      set
      {
        this._FPDReviewDate = value;
      }
    }

    public string Mesial { get; set; }

    public string Distal { get; set; }

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

    public IEnumerable<PROSFPDCasesheetProperties> Proplist { get; set; }

    public IEnumerable<MASChiefComplaint> ChiefComplaintlist { get; set; }

    public IEnumerable<MASCode> TMJlist { get; set; }

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

    public MedicalAlertViewModel medicalalertviewmodel { get; set; }

    public PrescriptionsViewModel PrescriptionsList { get; set; }

    public IEnumerable<PrescriptionsViewModel> PrescriptionsDetails { get; set; }

    public IEnumerable<PrescriptionsViewModel> PreviousPrescriptionList { get; set; }

    public ApprovalViewModal approvalViewModal { get; set; }

    public IEnumerable<BillingViewModal> deptTreatmentList { get; set; }

    public string TMJ { get; set; }

    public long OpNo { get; set; }

    public string PatientName { get; set; }

    public string Phone { get; set; }

    public string AgeGender { get; set; }

    public string Area { get; set; }

    public int Age { get; set; }

    public int GenderId { get; set; }

    public IEnumerable<SelectListItem> ToothLister { get; set; }

    public PROSFPDViewModel()
    {
      if (this.ToothLister != null)
        return;
      this.ToothLister = ((IEnumerable<string>) Enum.GetNames(typeof (ToothNumbers))).Select<string, SelectListItem>((Func<string, SelectListItem>) (name => new SelectListItem()
      {
        Text = name,
        Value = name
      }));
    }

    [DisplayName("Tooth Number")]
    public ToothNumbers ToothNumbers { get; set; }

    [Display(Name = "Tooth No.")]
    public string ToothNumber { get; set; }

    public int AbutmentTeethId { get; set; }

    [Display(Name = "Location")]
    public string Location { get; set; }

    [Display(Name = "Number")]
    public string Number { get; set; }

    [Display(Name = "Crown")]
    public string Crown { get; set; }

    [Display(Name = "Size")]
    public string Size { get; set; }

    [Display(Name = "Fracture")]
    public string Fracture { get; set; }

    [Display(Name = "Length")]
    public string Length { get; set; }

    [Display(Name = "Discoloration")]
    public string Discoloration { get; set; }

    [Display(Name = "Position")]
    public string Position { get; set; }

    [Display(Name = "Wear facets")]
    public string Wearfacets { get; set; }

    [Display(Name = "Caries")]
    public string Caries { get; set; }

    [Display(Name = "Restorations")]
    public string Restorations { get; set; }

    [Display(Name = "Vitality")]
    public string Vitality { get; set; }

    [Display(Name = "Mobility")]
    public string Mobility { get; set; }

    public IEnumerable<PROSFPDViewModel> AbutList { get; set; }

    public RadioRegistrationViewModel radioRegistrationviewmodel { get; set; }

    public LaboratoryRegistrationViewModel laboratoryRegistrationviewmodel { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }
  }
}
