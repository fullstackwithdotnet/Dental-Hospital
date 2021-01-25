// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.OMFSOPViewModel
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
  public class OMFSOPViewModel : EntityBase
  {
    [PrimaryKey]
    public int OMFSOpId { get; set; }

    [HiddenInput(DisplayValue = false)]
    public int PatientId { get; set; }

    public string OMFSOpNo { get; set; }

    public DateTime OMFSOpDate { get; set; }

    [Display(Name = "Chief Complaint")]
    public string ChiefComplaintName { get; set; }

    [DataType(DataType.MultilineText)]
    public string ChiefComplaint { get; set; }

    public string MandatoryDummy { get; set; }

    [Display(Name = "Findings")]
    public string Findings { get; set; }

    [Display(Name = "Upper Jaw")]
    public string UpperJaw { get; set; }

    [Display(Name = "Lower Jaw")]
    public string LowerJaw { get; set; }

    [Display(Name = "Upper Others")]
    public string UpperJawOthers { get; set; }

    [Display(Name = "Lower Others")]
    public string LowerJawOthers { get; set; }

    [Display(Name = "Medical Alerts")]
    public string MedicalAlerts { get; set; }

    public string Gait { get; set; }

    [Display(Name = "Built & Nourishment")]
    public string Built { get; set; }

    public string Nourishment { get; set; }

    public string Posture { get; set; }

    public string Height { get; set; }

    public string Weight { get; set; }

    public string Hair { get; set; }

    public string Nails { get; set; }

    public string Conjuctiva { get; set; }

    [Display(Name = "Pallor / Anemia")]
    public string Pallor { get; set; }

    public string Cyanosis { get; set; }

    public string Icterus { get; set; }

    public string Oedema { get; set; }

    public string Sclera { get; set; }

    public string Skin { get; set; }

    public string Clubbing { get; set; }

    [Display(Name = "Others")]
    public string PhysicalExamOthers { get; set; }

    public string Temperature { get; set; }

    [Display(Name = "Respiratory rate")]
    public string Respiratoryrate { get; set; }

    [Display(Name = "Pulse Rate")]
    public string Pulse { get; set; }

    [Display(Name = "Blood Pressure")]
    public string BPDia { get; set; }

    public string BPSys { get; set; }

    [Display(Name = "Others")]
    public string VitalOthers { get; set; }

    public string Face { get; set; }

    [Display(Name = "Skin")]
    public string ClinicalSkin { get; set; }

    public string Eyes { get; set; }

    public string Nose { get; set; }

    [Display(Name = "Salivary Glands")]
    public string SalivaryGlands { get; set; }

    [Display(Name = "Others")]
    public string ClinicalExamOthers { get; set; }

    public string Clicking { get; set; }

    [Display(Name = "Jaw Deviation")]
    public string JawDeviation { get; set; }

    [Display(Name = "Tenderness")]
    public string TemporoJointTenderness { get; set; }

    [Display(Name = "Mouth Opening")]
    public string MouthOpening { get; set; }

    [Display(Name = "Others")]
    public string TemporoJointOthers { get; set; }

    [Display(Name = "Group")]
    public int? GroupId { get; set; }

    public string Size { get; set; }

    [Display(Name = "Consistency")]
    public int? ConsistencyId { get; set; }

    [Display(Name = "Tenderness")]
    public string LymphTenderness { get; set; }

    public string Mobility { get; set; }

    [Display(Name = "Others")]
    public string LymphOthers { get; set; }

    [Display(Name = "Over Bite")]
    public int? OverBiteId { get; set; }

    [Display(Name = "Over Jet")]
    public int? OverJetId { get; set; }

    [Display(Name = "Cross Bite")]
    public string CrossBite { get; set; }

    [Display(Name = "Scissor Bite")]
    public string ScissorBite { get; set; }

    [Display(Name = "Lip")]
    public string CleftLip { get; set; }

    [Display(Name = "Palate")]
    public string CleftPalate { get; set; }

    public string Spacing { get; set; }

    public string Crowding { get; set; }

    public string Impaction { get; set; }

    [Display(Name = "Skeletal Relationship")]
    public int? SkeletalRelationshipId { get; set; }

    [Display(Name = "Moral Relationship")]
    public int? MoralRelationshipId { get; set; }

    [Display(Name = "Canine Relationship")]
    public int? CaninerelationshipId { get; set; }

    [Display(Name = "Pre operative")]
    public string Preoperative { get; set; }

    [Display(Name = "Models")]
    public string Models { get; set; }

    [Display(Name = "Splints")]
    public string Splints { get; set; }

    [Display(Name = "Diagnosis")]
    public string Diagnosis { get; set; }

    [Display(Name = "Provisional Diagnosis")]
    public string ProvisionalDiagnosis { get; set; }

    [Display(Name = "Differential Diagnosis")]
    public string DifferentialDiagnosis { get; set; }

    [Display(Name = "Treatment Plan")]
    public string TreatmentPlan { get; set; }

    [Display(Name = "Treatment Done")]
    public string TreatmentDone { get; set; }

    [Display(Name = "8")]
    public bool UR8 { get; set; }

    [Display(Name = "7")]
    public bool UR7 { get; set; }

    [Display(Name = "6")]
    public bool UR6 { get; set; }

    [Display(Name = "5")]
    public bool UR5 { get; set; }

    [Display(Name = "4")]
    public bool UR4 { get; set; }

    [Display(Name = "3")]
    public bool UR3 { get; set; }

    [Display(Name = "2")]
    public bool UR2 { get; set; }

    [Display(Name = "1")]
    public bool UR1 { get; set; }

    [Display(Name = "8")]
    public bool UL8 { get; set; }

    [Display(Name = "7")]
    public bool UL7 { get; set; }

    [Display(Name = "6")]
    public bool UL6 { get; set; }

    [Display(Name = "5")]
    public bool UL5 { get; set; }

    [Display(Name = "4")]
    public bool UL4 { get; set; }

    [Display(Name = "3")]
    public bool UL3 { get; set; }

    [Display(Name = "2")]
    public bool UL2 { get; set; }

    [Display(Name = "1")]
    public bool UL1 { get; set; }

    [Display(Name = "8")]
    public bool LR8 { get; set; }

    [Display(Name = "7")]
    public bool LR7 { get; set; }

    [Display(Name = "6")]
    public bool LR6 { get; set; }

    [Display(Name = "5")]
    public bool LR5 { get; set; }

    [Display(Name = "4")]
    public bool LR4 { get; set; }

    [Display(Name = "3")]
    public bool LR3 { get; set; }

    [Display(Name = "2")]
    public bool LR2 { get; set; }

    [Display(Name = "1")]
    public bool LR1 { get; set; }

    [Display(Name = "8")]
    public bool LL8 { get; set; }

    [Display(Name = "7")]
    public bool LL7 { get; set; }

    [Display(Name = "6")]
    public bool LL6 { get; set; }

    [Display(Name = "5")]
    public bool LL5 { get; set; }

    [Display(Name = "4")]
    public bool LL4 { get; set; }

    [Display(Name = "3")]
    public bool LL3 { get; set; }

    [Display(Name = "2")]
    public bool LL2 { get; set; }

    [Display(Name = "1")]
    public bool LL1 { get; set; }

    public bool Max1 { get; set; }

    public bool Max2 { get; set; }

    public bool Max3 { get; set; }

    public bool Max4 { get; set; }

    public bool Max5 { get; set; }

    public bool Max6 { get; set; }

    public bool Max7 { get; set; }

    public bool Max8 { get; set; }

    public bool Max9 { get; set; }

    public bool Max10 { get; set; }

    public bool Max11 { get; set; }

    public bool Max12 { get; set; }

    public bool Max13 { get; set; }

    public bool Max14 { get; set; }

    public bool Max15 { get; set; }

    public bool Max16 { get; set; }

    public bool Mand1 { get; set; }

    public bool Mand2 { get; set; }

    public bool Mand3 { get; set; }

    public bool Mand4 { get; set; }

    public bool Mand5 { get; set; }

    public bool Mand6 { get; set; }

    public bool Mand7 { get; set; }

    public bool Mand8 { get; set; }

    public bool Mand9 { get; set; }

    public bool Mand10 { get; set; }

    public bool Mand11 { get; set; }

    public bool Mand12 { get; set; }

    public bool Mand13 { get; set; }

    public bool Mand14 { get; set; }

    public bool Mand15 { get; set; }

    public bool Mand16 { get; set; }

    [Display(Name = "Mode of Onset")]
    public string HPIModeOfOnset { get; set; }

    [Display(Name = "Duration")]
    public string HPIDuration { get; set; }

    [Display(Name = "Frequency")]
    public string HPIFrequency { get; set; }

    [Display(Name = "Location")]
    public string HPILocation { get; set; }

    [Display(Name = "Severity")]
    public string HPISeverity { get; set; }

    [Display(Name = "Progression")]
    public string HPIProgression { get; set; }

    [Display(Name = "Aggrevating Factors")]
    public string HPIAggrevating { get; set; }

    [Display(Name = "Factors")]
    public string HPIFactors { get; set; }

    [Display(Name = "Relieving Factors")]
    public string HPIRelievingFact { get; set; }

    [Display(Name = "Any Previous Treatment")]
    public string HPIAnyPreviousTreat { get; set; }

    [Display(Name = "Drug History")]
    public string DrugHistory { get; set; }

    [Display(Name = "History of Allergy")]
    public string HisOfAllergy { get; set; }

    [Display(Name = "CNS")]
    public string CNS { get; set; }

    [Display(Name = "CVS")]
    public string CVS { get; set; }

    [Display(Name = "GIT")]
    public string GIT { get; set; }

    [Display(Name = "Renal System")]
    public string RenalSystem { get; set; }

    [Display(Name = "Hepatic System")]
    public string HepaticSystem { get; set; }

    [Display(Name = "Endocrine System")]
    public string EndocrineSystem { get; set; }

    [Display(Name = "Respiratory System")]
    public string RespiratorySystem { get; set; }

    [Display(Name = "Immunocompromised")]
    public string Immunocompromised { get; set; }

    [Display(Name = "Infections")]
    public string Infections { get; set; }

    [Display(Name = "Pregnancy / Lactation")]
    public string PregnancyLact { get; set; }

    [Display(Name = "Previous Treatment")]
    public string DHPreviousTreat { get; set; }

    [Display(Name = "Complications")]
    public string Complications { get; set; }

    [Display(Name = "Family History")]
    public string FamilyHistory { get; set; }

    [Display(Name = "Diet")]
    public int DietId { get; set; }

    public string DietName { get; set; }

    [Display(Name = "Mental State & Illness")]
    public string MentalStateIllness { get; set; }

    [Display(Name = "Attitude")]
    public string Attitude { get; set; }

    public Diet Diet { get; set; }

    public string Duration { get; set; }

    public string Frequency { get; set; }

    [Display(Name = "Smoking")]
    public string SmokingDur { get; set; }

    public string SmokingFreq { get; set; }

    [Display(Name = "Pan/Tobaco Chewing")]
    public string PanTobacoDur { get; set; }

    public string PanTobacoFreq { get; set; }

    [Display(Name = "Alcohol")]
    public string AlcoholDur { get; set; }

    public string AlcoholFreq { get; set; }

    [Display(Name = "Facial Symmetry")]
    public string FacialSymmetry { get; set; }

    [Display(Name = "Lymph Node")]
    public string LymphNode { get; set; }

    [Display(Name = "Salivary Gland")]
    public string SalivaryGlandIns { get; set; }

    public string SalivaryGlandPal { get; set; }

    [Display(Name = "TMJ")]
    public string TMJIns { get; set; }

    public string TMJPal { get; set; }

    [Display(Name = "Swelling,If Present")]
    public string SwellingIns { get; set; }

    public string SwellingPal { get; set; }

    public string Site { get; set; }

    public string Shape { get; set; }

    [Display(Name = "Pus Discharge")]
    public string PusDischarge { get; set; }

    public string Inspection { get; set; }

    public string Palpation { get; set; }

    [Display(Name = "Labial Mucosa")]
    public string LabialMucosaIns { get; set; }

    public string LabialMucosaPal { get; set; }

    [Display(Name = "Buccal Mucosa")]
    public string BuccalMucosaIns { get; set; }

    public string BuccalMucosaPal { get; set; }

    [Display(Name = "Floor Of Mouth")]
    public string FloorOfMouthIns { get; set; }

    public string FloorOfMouthPal { get; set; }

    [Display(Name = "Gingiva")]
    public string GingivaIns { get; set; }

    public string GingivaPal { get; set; }

    [Display(Name = "Hard Palate")]
    public string HardPalateIns { get; set; }

    public string HardPalatePal { get; set; }

    [Display(Name = "Soft Palate")]
    public string SoftPalateIns { get; set; }

    public string SoftPalatePal { get; set; }

    [Display(Name = "Tongue")]
    public string TongueIns { get; set; }

    public string TonguePal { get; set; }

    [Display(Name = "Lips")]
    public string LipsIns { get; set; }

    public string LipsPal { get; set; }

    [Display(Name = "Teeth Present")]
    public string TeethPresent { get; set; }

    [Display(Name = "Missing Teeth")]
    public string MissingTeeth { get; set; }

    [Display(Name = "Dental Caries")]
    public string DentalCaries { get; set; }

    [Display(Name = "Root Stump")]
    public string RootStump { get; set; }

    [Display(Name = "Development Defects")]
    public string DevelopmentDef { get; set; }

    [Display(Name = "Medications Given")]
    public string MedicationsGiven { get; set; }

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

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

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

    public IEnumerable<SelectListItem> DietLister { get; set; }

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

    public RadioRegistrationViewModel radioRegistrationviewmodel { get; set; }

    public LaboratoryRegistrationViewModel laboratoryRegistrationviewmodel { get; set; }

    public IEnumerable<BillingViewModal> deptTreatmentList { get; set; }

    public List<ReferralStatusViewModel> CreatedepartmentReferredStatus { get; set; }

    public List<ReferralStatusViewModel> ViewdepartmentReferredStatus { get; set; }

    public IEnumerable<BillingQueueServiceViewModel> BillingQueueDetails { get; set; }

    public long TreatmentReferredId { get; set; }

    public PrescriptionsViewModel PrescriptionsList { get; set; }

    public IEnumerable<PrescriptionsViewModel> PrescriptionsDetails { get; set; }

    public MedicalAlertViewModel medicalalertviewmodel { get; set; }

    public IEnumerable<PrescriptionsViewModel> PreviousPrescriptionList { get; set; }

    public IEnumerable<OMFSOPCasesheetProperties> Proplist { get; set; }

    public IEnumerable<MASChiefComplaint> ChiefComplaintlist { get; set; }

    public IEnumerable<MASCode> Consistencylist { get; set; }

    public IEnumerable<MASCode> Grouplist { get; set; }

    public IEnumerable<MASCode> OverBitelist { get; set; }

    public IEnumerable<MASCode> OverJetlist { get; set; }

    public IEnumerable<MASCode> MoralRelationshiplist { get; set; }

    public IEnumerable<MASCode> CanineRelationshiplist { get; set; }

    public IEnumerable<MASCode> SkeletaRelationshiplist { get; set; }

    public string ConsistencyDn { get; set; }

    [Display(Name = "Others")]
    public string LymphOthersDn { get; set; }

    public string OverBiteDn { get; set; }

    public string OverJetDn { get; set; }

    public string SkeletalRelationshipDn { get; set; }

    public string MoralRelationshipDn { get; set; }

    public string CaninerelationshipDn { get; set; }

    public string GroupDn { get; set; }

    public long OpNo { get; set; }

    public string PatientName { get; set; }

    public string Phone { get; set; }

    public string AgeGender { get; set; }

    public string Area { get; set; }

    public int Age { get; set; }

    public int GenderId { get; set; }

    public IEnumerable<FileUploadViewModel> FileUploadlist { get; set; }

    public ApprovalViewModal approvalViewModal { get; set; }

    public OMFSOPViewModel()
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
