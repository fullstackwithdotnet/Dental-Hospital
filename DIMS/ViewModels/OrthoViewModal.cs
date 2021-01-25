// Decompiled with JetBrains decompiler
// Type: DIMS.ViewModels.OrthoViewModal
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
    public class OrthoViewModal : EntityBase
    {
        [PrimaryKey] public int OrthoId { get; set; }

        [HiddenInput(DisplayValue = false)] public int PatientId { get; set; }

        public string MandatoryDummy { get; set; }

        public string OrthoNo { get; set; }

        public DateTime OrthoDate { get; set; }

        [Display(Name = "Chief Complaint")] public string ChiefComplaintName { get; set; }

        [DataType(DataType.MultilineText)] public string ChiefComplaint { get; set; }

        [Display(Name = "Informer")] public int? InformerId { get; set; }

        public string InformerDesc { get; set; }

        [Display(Name = "Disease")] public string MotherPregDisease { get; set; }

        public string MotherPregDiseaseDesc { get; set; }

        [Display(Name = "Medication")] public string MotherPreMedication { get; set; }

        public string MotherPreMedDesc { get; set; }

        [Display(Name = "Trauma")] public string MotherPreTrauma { get; set; }

        public string MotherPreTraDesc { get; set; }

        [Display(Name = "Delivery")] public int? DeliveryId { get; set; }

        [Display(Name = "Type")] public int? TypeId { get; set; }

        [Display(Name = "Prolonged Bottle Feeding")]
        public string BottleFeeding { get; set; }

        [Display(Name = "How Long")] public string BottleFeedingTime { get; set; }

        [Display(Name = "Habits")] public int? HabitsId { get; set; }

        [Display(Name = "Habits")] public string HabitsDesc { get; set; }

        [Display(Name = "Mouth Breathing Habits")]
        public int? MouthBreathingHabitsId { get; set; }

        [Display(Name = "Mouth")] public int? MouthId { get; set; }

        [Display(Name = "Snoring")] public int? SnoringId { get; set; }

        public string SnoringDesc { get; set; }

        [Display(Name = "Respiratory Obstrusion")]
        public string RespiratoryObstrusion { get; set; }

        [Display(Name = "Treatment Undergone")]
        public string TreatmentUndergone { get; set; }

        [Display(Name = "Mental Development")] public string MentalDevelopment { get; set; }

        [Display(Name = "Sitting")] public string SittingDesc { get; set; }

        [Display(Name = "Standing  ")] public string StandingDesc { get; set; }

        [Display(Name = "Teething")] public string TeethingDesc { get; set; }

        [Display(Name = "Crawling")] public string CrawlingDesc { get; set; }

        [Display(Name = "Walking")] public string WalkingDesc { get; set; }

        [Display(Name = "Speaking")] public string SpeakingDesc { get; set; }

        [Display(Name = "Injuries/Trauma")] public string InjuriesTrauma { get; set; }

        [Display(Name = "Major illness/injuries in the past/surgical operations undergone")]
        public string MajorIllness { get; set; }

        [Display(Name = "Presently under treatment for")]
        public string PresentTreatment { get; set; }

        [Display(Name = "Allergies")] public string Allergies { get; set; }

        [Display(Name = "Is the patient under medication currently?")]
        public string Medication { get; set; }

        [Display(Name = "Parents")] public string OcclusionParents { get; set; }

        [Display(Name = "Siblings")] public string OcclusionSiblings { get; set; }

        [Display(Name = "Esthetics")] public string EstheticDesc { get; set; }

        [Display(Name = "Functional")] public string FunctionDesc { get; set; }

        [Display(Name = "Speech")] public string SpeechHistory { get; set; }

        [Display(Name = "Hygiene")] public string HygieneDesc { get; set; }

        [Display(Name = "Patients Concern")] public int? ParentsConcernId { get; set; }

        [Display(Name = "Brushing Habits")] public int? BrushingHabitsId { get; set; }

        [Display(Name = "Boys:Has voice change occurred yet?If yes, when?")]
        public string PubertalBoys { get; set; }

        [Display(Name = "Girls:Has menstruation cycle started ?If yes, when?")]
        public string PubertalGirls { get; set; }

        [Display(Name = "Any other Information")]
        public string OtherInformation { get; set; }

        [Display(Name = "Height")] public int? HeightId { get; set; }

        public string HeightDesc { get; set; }

        [Display(Name = "Weight")] public int? WeightId { get; set; }

        public string WeightDesc { get; set; }

        [Display(Name = "Gait")] public string Gait { get; set; }

        [Display(Name = "Posture")] public string Posture { get; set; }

        [Display(Name = "Body Type")] public string BodyType { get; set; }

        [Display(Name = "Shape of Head")] public int? ShapeOfHeadId { get; set; }

        [Display(Name = "Facial Form")] public int? FacialFormId { get; set; }

        [Display(Name = "Facial Symmetry")] public string FacialSymmetry { get; set; }

        [Display(Name = "Facial Asymmetry")] public string FacialAsymmetry { get; set; }

        [Display(Name = "Facial Asymmetry")] public string FacialSymmetryDesc { get; set; }

        [Display(Name = "Facial Asymmetry")] public string FacialAsymmetryDesc { get; set; }

        [Display(Name = "Inter Labial Gap")] public string LabialGap { get; set; }

        [Display(Name = "Incisor exposure at rest (in mm)")]
        public string IncisorRest { get; set; }

        [Display(Name = "Incisor exposure on smile (in mm)")]
        public string IncisorSmile { get; set; }

        [Display(Name = "Facial Profile")] public int? FacialProfileId { get; set; }

        [Display(Name = "Face Divergence")] public int? FaceDivergenceId { get; set; }

        [Display(Name = "Naso Labial Angle")] public int? NasoLabialAngleId { get; set; }

        [Display(Name = "Clinical FMA")] public int? ClinicalFMAId { get; set; }

        [Display(Name = "mm")] public string Mm { get; set; }

        [Display(Name = "Lip Posture")] public int? LipPostureId { get; set; }

        [Display(Name = "Mento Labial Sulcus")]
        public int? MentoLabialSulcusId { get; set; }

        [Display(Name = "VTO")] public int? VTOId { get; set; }

        [Display(Name = "Finger/Thumb sucking")]
        public string ThumbSuckingDesc { get; set; }

        [Display(Name = "Tongue Thrust")] public string TongueThrustDesc { get; set; }

        [Display(Name = "Mouth Breathing")] public string MouthBreathingDesc { get; set; }

        [Display(Name = "Nail Biting")] public string NailBitingDesc { get; set; }

        [Display(Name = "Lip Biting/Sucking")] public string LipSuckingDesc { get; set; }

        [Display(Name = "Gum Chewing")] public string GumChewingDesc { get; set; }

        [Display(Name = "Any Other")] public string AnyOtherHabitsDesc { get; set; }

        [Display(Name = "Respiration")] public int? RespirationId { get; set; }

        [Display(Name = "Inter Occlusal Clearance")]
        public string OcclusionClearance { get; set; }

        [Display(Name = "Jaw Function")] public int? JawFunctionId { get; set; }

        public string JawFunctionDesc { get; set; }

        [Display(Name = "TMJ Symptoms")] public int? TMJComplaintId { get; set; }

        public string TMJComplaintDesc { get; set; }

        [Display(Name = "History of Pain")] public int? PainHistoryId { get; set; }

        [Display(Name = "History of Sounds")] public int? SoundsHistoryId { get; set; }

        [Display(Name = "TMJ Tenderness to Palpation")]
        public int? TMJPalpationId { get; set; }

        [Display(Name = "Right")] public bool TMJRight { get; set; }

        [Display(Name = "Left")] public bool TMJLeft { get; set; }

        [Display(Name = "Perioral Muscle Activity")]
        public int? PerioralMuscleActivityId { get; set; }

        [Display(Name = "Hyperactive Mentalis")]
        public string HyperactiveMentalis { get; set; }

        [Display(Name = "Hypertonic Upper Lip")]
        public string HypertonicUpperLip { get; set; }

        [Display(Name = "Muscle Tenderness to Palpation")]
        public int? MusclePalpationId { get; set; }

        public string MusclePalpationDesc { get; set; }

        [Display(Name = "Max. Mouth Opening")] public string MandMaxOpening { get; set; }

        [Display(Name = "Mandibular Deviation on Opening")]
        public string Protrusion { get; set; }

        [Display(Name = "Functional shift(Right anterior)")]
        public string ExcursionRight { get; set; }

        [Display(Name = "Functional shif(Left posterior")]
        public string ExcursionLeft { get; set; }

        [Display(Name = "Path of Mandibular Closure")]
        public string ClosurePath { get; set; }

        [Display(Name = "Deglutition")] public int? DeglutitionId { get; set; }

        [Display(Name = "Speech")] public int? SpeechId { get; set; }

        [Display(Name = "Incissor exposure during speech(In mm)")]
        public string SpeechDesc { get; set; }

        [Display(Name = "Duration")] public string HabitsDuration { get; set; }

        [Display(Name = "Intensity")] public string HabitsIntensity { get; set; }

        [Display(Name = "Frequency")] public string HabitsFrequency { get; set; }

        [Display(Name = "Gingiva")] public int? GingivaId { get; set; }

        public string GingivaDesc { get; set; }

        [Display(Name = "Frenal Attachment Upper")]
        public string FrenalUpper { get; set; }

        [Display(Name = "Frenal Attachment Lower")]
        public string FrenalLower { get; set; }

        [Display(Name = "Shape & Size")] public int? TongueSizeAndShapeId { get; set; }

        [Display(Name = "Ankyloglossia")] public int? TongueAnkyloglossiaId { get; set; }

        [Display(Name = "Shape")] public string TongueShape { get; set; }

        [Display(Name = "Crenation")] public int? TongueCrenationId { get; set; }

        [Display(Name = "Clinical Diagnosis")] public string ClinicalDiagnosis { get; set; }

        [Display(Name = "Deciduous Teeth")] public string DeciduousTeeth { get; set; }

        [Display(Name = "Others")] public string Others { get; set; }

        [Display(Name = "Movements")] public string TongueMovements { get; set; }

        [Display(Name = "Oral Hygiene Status")]
        public int? OralHygieneStatusId { get; set; }

        [Display(Name = "Mucogingival Junction")]
        public int? GingivalJunctionId { get; set; }

        [Display(Name = "No of Teeth Present")]
        public string NoTeethPresent { get; set; }

        [Display(Name = "Size")] public string TeethSize { get; set; }

        [Display(Name = "No of Unerrupted Teeth")]
        public string NoUnerruptedTeeth { get; set; }

        [Display(Name = "Form of Teeth")] public string FormTeeth { get; set; }

        [Display(Name = "Supernumerary Teeth")]
        public string SupernumeraryTeeth { get; set; }

        [Display(Name = "Caries")] public string Caries { get; set; }

        [Display(Name = "Missing Teeth")] public string MissingTeeth { get; set; }

        [Display(Name = "Restored Teeth")] public string Restorations { get; set; }

        [Display(Name = "Occlussal Wear Facts")]
        public string OcclussalWearFacts { get; set; }

        [Display(Name = "Arch Symmentry")] public int? MaxillarySymmentryId { get; set; }

        [Display(Name = "Shape")] public int? MaxillaryShapeId { get; set; }

        [Display(Name = "Arch Alignment")] public string MaxillaryAlignment { get; set; }

        [Display(Name = "Palatal Contour")] public int? PalatalContourId { get; set; }

        [Display(Name = "Arch Symmentry")] public int? MandibularSymmentryId { get; set; }

        [Display(Name = "Shape")] public int? MandibularShapeId { get; set; }

        [Display(Name = "Arch Alignment")] public string MandibularAlignment { get; set; }

        [Display(Name = "Right")] public string SpeeCurveRightyn { get; set; }

        [Display(Name = "Curve of Spee")] public string SpeeCurveRightdesc { get; set; }

        [Display(Name = "Left")] public string SpeeCurveLeftyn { get; set; }

        public string SpeeCurveLeftdesc { get; set; }

        [Display(Name = "Upper")] public string MaxilloMidlineUpperyn { get; set; }

        public string MaxilloMidlineUpperdesc { get; set; }

        [Display(Name = "Lower")] public string MaxilloMidlineLoweryn { get; set; }

        public string MaxilloMidlineLowerdesc { get; set; }

        [Display(Name = "Functional")] public string MaxilloFunctional { get; set; }

        [Display(Name = "Curve of Wilson")] public string WilsonCurve { get; set; }

        [Display(Name = "Curve of Monson")] public string WilsonMonson { get; set; }

        [Display(Name = "Right")] public string AnteroMolarRightyn { get; set; }

        public string AnteroMolarRightDesc { get; set; }

        [Display(Name = "Left")] public string AnteroMolarLeftyn { get; set; }

        public string AnteroMolarLeftDesc { get; set; }

        [Display(Name = "Right")] public string AnteroCanineRightyn { get; set; }

        public string AnteroCanineRightDesc { get; set; }

        [Display(Name = "Left")] public string AnteroCanineLeftyn { get; set; }

        public string AnteroCanineLeftDesc { get; set; }

        [Display(Name = "Incisor Relation")] public string AnteroIncisor { get; set; }

        [Display(Name = "Overjet")] public string AnteroOverjet { get; set; }

        [Display(Name = "Overbite")] public string VerticalOverbitemm { get; set; }

        public string VerticalOverbitePect { get; set; }

        [Display(Name = "Crossbite/Scissorbite")]
        public string TransverseCrossbite { get; set; }

        public string TransverseScissorbite { get; set; }

        [Display(Name = "Molar Relation")] public string MolarRelation { get; set; }

        [Display(Name = "Canine Relation")] public string CanineRelation { get; set; }

        [Display(Name = "Midline")] public string Midline { get; set; }

        [Display(Name = "MP3 F/CVMI1")] public bool CVMI1 { get; set; }

        [Display(Name = "MP3 FG/CVMI2")] public bool CVMI2 { get; set; }

        [Display(Name = "MP3 G/CVMI 3")] public bool CVMI3 { get; set; }

        [Display(Name = "MP3 H/CVMI4")] public bool CVMI4 { get; set; }

        [Display(Name = "MP3 H1/CVMI 5")] public bool CVMI5 { get; set; }

        [Display(Name = "MP3 1/CVMI 6")] public bool CVMI6 { get; set; }

        [Display(Name = "Sum Of Incisors(S.I.)")]
        public Decimal SumIncisors { get; set; }

        public string ToungeDuration { get; set; }

        public string ToungeFrequency { get; set; }

        public string ToungeIntensity { get; set; }

        public string MouthDuration { get; set; }

        public string MouthFrequency { get; set; }

        public string MouthIntensity { get; set; }

        public string NailDuration { get; set; }

        public string NailFrequency { get; set; }

        public string NailIntensity { get; set; }

        public string LipDuration { get; set; }

        public string LipFrequency { get; set; }

        public string LipIntensity { get; set; }

        public string GumDuration { get; set; }

        public string GumFrequency { get; set; }

        public string GumIntensity { get; set; }

        public string OtherDuration { get; set; }

        public string OtherFrequency { get; set; }

        public string OtherIntensity { get; set; }

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

        public PrescriptionsViewModel PrescriptionsList { get; set; }

        public IEnumerable<PrescriptionsViewModel> PrescriptionsDetails { get; set; }

        public IEnumerable<PrescriptionsViewModel> PreviousPrescriptionList { get; set; }

        public MedicalAlertViewModel medicalalertviewmodel { get; set; }

        public IEnumerable<MASChiefComplaint> ChiefComplaintlist { get; set; }

        public IEnumerable<MASCode> InformerList { get; set; }

        public IEnumerable<MASCode> DeliveryList { get; set; }

        public IEnumerable<MASCode> TypeList { get; set; }

        public IEnumerable<MASCode> HabitList { get; set; }

        public IEnumerable<MASCode> MouthBreathingList { get; set; }

        public IEnumerable<MASCode> MouthList { get; set; }

        public IEnumerable<MASCode> SnoringList { get; set; }

        public IEnumerable<MASCode> ParentConcernList { get; set; }

        public IEnumerable<MASCode> HeightList { get; set; }

        public IEnumerable<MASCode> WeightList { get; set; }

        public IEnumerable<MASCode> SmileArcList { get; set; }

        public IEnumerable<MASCode> HeadShapeList { get; set; }

        public IEnumerable<MASCode> FacialFormList { get; set; }

        public IEnumerable<MASCode> FacialProfileOrthoList { get; set; }

        public IEnumerable<MASCode> FacialDivergenceList { get; set; }

        public IEnumerable<MASCode> NasolabialAngleList { get; set; }

        public IEnumerable<MASCode> ClinicalFMAList { get; set; }

        public IEnumerable<MASCode> MentoLabialSulcusList { get; set; }

        public IEnumerable<MASCode> VtoList { get; set; }

        public IEnumerable<MASCode> TongueAnkyloglossiaList { get; set; }

        public IEnumerable<MASCode> TongueCrenationList { get; set; }

        public IEnumerable<MASCode> RespirationList { get; set; }

        public IEnumerable<MASCode> YesNoList { get; set; }

        public IEnumerable<MASCode> DeglutitionList { get; set; }

        public IEnumerable<MASCode> GingivalList { get; set; }

        public IEnumerable<MASCode> MaxiSymmetryList { get; set; }

        public IEnumerable<MASCode> MaxiShapeList { get; set; }

        public IEnumerable<MASCode> MandiSymmetryList { get; set; }

        public IEnumerable<MASCode> MandiShapeList { get; set; }

        public IEnumerable<MASCode> ShortFacialSymmetryList { get; set; }

        public IEnumerable<MASCode> GrowthPatternList { get; set; }

        public IEnumerable<MASCode> LipsShortList { get; set; }

        public IEnumerable<MASCode> CrossbiteShortList { get; set; }

        public IEnumerable<MASCode> MolarRelationLShortList { get; set; }

        public IEnumerable<MASCode> MolarRelationRShortList { get; set; }

        public IEnumerable<MASCode> CanineRelationLShortList { get; set; }

        public IEnumerable<MASCode> CanineRelationRShortList { get; set; }

        public IEnumerable<MASCode> FixedMechanotheraphyList { get; set; }

        public IEnumerable<MASCode> PainStartsList { get; set; }

        public IEnumerable<MASCode> SoundBeganList { get; set; }

        public IEnumerable<MASCode> FeedingPracticedList { get; set; }

        public IEnumerable<MASCode> SurgicalProcedureDoneList { get; set; }

        public IEnumerable<MASCode> LipPostureList { get; set; }

        public IEnumerable<MASCode> UsfhList { get; set; }

        public IEnumerable<MASCode> LafhList { get; set; }

        public IEnumerable<MASCode> NoseSizeList { get; set; }

        public IEnumerable<MASCode> MandibularPlaneList { get; set; }

        public IEnumerable<MASCode> ChinPositionList { get; set; }

        public IEnumerable<MASCode> MasticationList { get; set; }

        public IEnumerable<MASCode> SpeechList { get; set; }

        public IEnumerable<MASCode> PerioralMuscleActivityList { get; set; }

        public IEnumerable<MASCode> OralHygieneStatusList { get; set; }

        public IEnumerable<MASCode> BrushingHabitsList { get; set; }

        public IEnumerable<MASCode> GingivalJunctionList { get; set; }

        public IEnumerable<MASCode> PalatalContourList { get; set; }

        public IEnumerable<MASCode> TonsilList { get; set; }

        public IEnumerable<MASCode> TongueFunctionList { get; set; }

        public IEnumerable<MASCode> TongueSizeAndShapeList { get; set; }

        public IEnumerable<MASCode> TextureList { get; set; }

        public IEnumerable<ORTHOCasesheetProperties> Proplist { get; set; }

        public ApprovalViewModal approvalViewModal { get; set; }

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

        public IEnumerable<BillingViewModal> deptTreatmentList { get; set; }

        public RadioRegistrationViewModel radioRegistrationviewmodel { get; set; }

        public LaboratoryRegistrationViewModel laboratoryRegistrationviewmodel { get; set; }

        public List<ReferralStatusViewModel> CreatedepartmentReferredStatus { get; set; }

        public List<ReferralStatusViewModel> ViewdepartmentReferredStatus { get; set; }

        public IEnumerable<BillingQueueServiceViewModel> BillingQueueDetails { get; set; }

        public long TreatmentReferredId { get; set; }

        public IEnumerable<OrthoViewModal> Treatmentlist { get; set; }

        [Display(Name = "Date")] public string OrthoDateDisplay { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public OrthoStaticAnalysisViewModel OrthoAnalysisList { get; set; }

        public int? SteinerId { get; set; }

        public int? SteinerStaticId { get; set; }

        [Display(Name = "Letters")] public string Letters { get; set; }

        [Display(Name = "Measurement")] public string Measurements { get; set; }

        [Display(Name = "Mean")] public string Mean { get; set; }

        [Display(Name = "Pre Rx")] public string PreRx { get; set; }

        [Display(Name = "Post Growth Modulation")]
        public string PostModulation { get; set; }

        [Display(Name = "Post Rx")] public string PostRx { get; set; }

        [Display(Name = "Retention")] public string Retention { get; set; }

        [Display(Name = "Change")] public string Change { get; set; }

        [Display(Name = "8")] public string ToothUl8 { get; set; }

        [Display(Name = "7")] public string ToothUl7 { get; set; }

        [Display(Name = "6")] public string ToothUl6 { get; set; }

        [Display(Name = "5")] public string ToothUl5 { get; set; }

        [Display(Name = "4")] public string ToothUl4 { get; set; }

        [Display(Name = "3")] public string ToothUl3 { get; set; }

        [Display(Name = "2")] public string ToothUl2 { get; set; }

        [Display(Name = "1")] public string ToothUl1 { get; set; }

        [Display(Name = "8")] public string ToothUr8 { get; set; }

        [Display(Name = "7")] public string ToothUr7 { get; set; }

        [Display(Name = "6")] public string ToothUr6 { get; set; }

        [Display(Name = "5")] public string ToothUr5 { get; set; }

        [Display(Name = "4")] public string ToothUr4 { get; set; }

        [Display(Name = "3")] public string ToothUr3 { get; set; }

        [Display(Name = "2")] public string ToothUr2 { get; set; }

        [Display(Name = "1")] public string ToothUr1 { get; set; }

        [Display(Name = "8")] public string ToothLl8 { get; set; }

        [Display(Name = "7")] public string ToothLl7 { get; set; }

        [Display(Name = "6")] public string ToothLl6 { get; set; }

        [Display(Name = "5")] public string ToothLl5 { get; set; }

        [Display(Name = "4")] public string ToothLl4 { get; set; }

        [Display(Name = "3")] public string ToothLl3 { get; set; }

        [Display(Name = "2")] public string ToothLl2 { get; set; }

        [Display(Name = "1")] public string ToothLl1 { get; set; }

        [Display(Name = "8")] public string ToothLr8 { get; set; }

        [Display(Name = "7")] public string ToothLr7 { get; set; }

        [Display(Name = "6")] public string ToothLr6 { get; set; }

        [Display(Name = "5")] public string ToothLr5 { get; set; }

        [Display(Name = "4")] public string ToothLr4 { get; set; }

        [Display(Name = "3")] public string ToothLr3 { get; set; }

        [Display(Name = "2")] public string ToothLr2 { get; set; }

        [Display(Name = "1")] public string ToothLr1 { get; set; }

        [Display(Name = "Facial Symmentry")] public int? ShortFacialSymmetryId { get; set; }

        [Display(Name = "Growth Pattern")] public int? GrowthPatternId { get; set; }

        [Display(Name = "Lips")] public int? LipsShortId { get; set; }

        [Display(Name = "Crossbite")] public int? CrossbiteShortId { get; set; }

        [Display(Name = "Left")] public int? MolarRelationLShortId { get; set; }

        [Display(Name = "Right")] public int? MolarRelationRShortId { get; set; }

        [Display(Name = "Left")] public int? CanineRelationLShortId { get; set; }

        [Display(Name = "Right")] public int? CanineRelationRShortId { get; set; }

        [Display(Name = "E")] public string ToothUlE { get; set; }

        [Display(Name = "D")] public string ToothUlD { get; set; }

        [Display(Name = "C")] public string ToothUlC { get; set; }

        [Display(Name = "B")] public string ToothUlB { get; set; }

        [Display(Name = "A")] public string ToothUlA { get; set; }

        [Display(Name = "E")] public string ToothUrE { get; set; }

        [Display(Name = "D")] public string ToothUrD { get; set; }

        [Display(Name = "C")] public string ToothUrC { get; set; }

        [Display(Name = "B")] public string ToothUrB { get; set; }

        [Display(Name = "A")] public string ToothUrA { get; set; }

        [Display(Name = "E")] public string ToothLlE { get; set; }

        [Display(Name = "D")] public string ToothLlD { get; set; }

        [Display(Name = "C")] public string ToothLlC { get; set; }

        [Display(Name = "B")] public string ToothLlB { get; set; }

        [Display(Name = "A")] public string ToothLlA { get; set; }

        [Display(Name = "E")] public string ToothLrE { get; set; }

        [Display(Name = "D")] public string ToothLrD { get; set; }

        [Display(Name = "C")] public string ToothLrC { get; set; }

        [Display(Name = "B")] public string ToothLrB { get; set; }

        [Display(Name = "A")] public string ToothLrA { get; set; }

        [Display(Name = "Functional Examination")]
        public string FunctionalExamination { get; set; }

        [Display(Name = "Investigations Interpretation")]
        public string InvestigationsInterpretation { get; set; }

        [Display(Name = "Radiograph Interpretation")]
        public string RadiographInterpretation { get; set; }

        [Display(Name = "Diagnosis")] public string Diagnosis { get; set; }

        [Display(Name = "Fixed Mechanotheraphy")]
        public int? FixedMechanotheraphyId { get; set; }

        public int CasesheetType { get; set; }

        [Display(Name = "Treatment Plan")] public string TreatmentPlan { get; set; }

        [Display(Name = "Sinusitis")] public string Sinusitis { get; set; }

        [Display(Name = "Epilepsy")] public string Epilepsy { get; set; }

        [Display(Name = "Hepatitis")] public string Hepatitis { get; set; }

        [Display(Name = "Herpes")] public string Herpes { get; set; }

        [Display(Name = "Migraine")] public string Migraine { get; set; }

        [Display(Name = "Diabetes")] public string Diabetes { get; set; }

        [Display(Name = "Blood Pressure")] public string BloodPressure { get; set; }

        [Display(Name = "Heart Attack")] public string HeartAttack { get; set; }

        [Display(Name = "Blood Disorder")] public string BloodDisorder { get; set; }

        [Display(Name = "Rheumatic Fever")] public string RheumaticFever { get; set; }

        [Display(Name = "Tuberculosis")] public string Tuberculosis { get; set; }

        [Display(Name = "Psoriasis")] public string Psoriasis { get; set; }

        [Display(Name = "Std Infection")] public string StdInfection { get; set; }

        [Display(Name = "Hiv Infection")] public string HivInfection { get; set; }

        [Display(Name = "Cancer")] public string Cancer { get; set; }

        [Display(Name = "Swollen Glands")] public string SwollenGlands { get; set; }

        [Display(Name = "Bone Disorder")] public string BoneDisorder { get; set; }

        [Display(Name = "Sleep Apnowa")] public string SleepApnowa { get; set; }

        [Display(Name = "Ulcers")] public string Ulcers { get; set; }

        [Display(Name = "Any other significant Medical History?")]
        public string MedicalHistoryOther { get; set; }

        [Display(Name = "Is the patient under any Physician care at present?if yes Reason?")]
        public string PhysicianCare { get; set; }

        [Display(Name = "Has the patient ever been administrated general Anesthesia?If yes,when?why?")]
        public string GeneralAnesthesia { get; set; }

        [Display(Name = "Do you have any tooth pain?")]
        public string ToothPainDesc { get; set; }

        [Display(Name = "Have any tooth been removed?")]
        public string TeethRemoved { get; set; }

        [Display(Name = "Have you ever had any treatment for periodontal problem?")]
        public string PeriodontalProbTreatment { get; set; }

        [Display(Name = "Have you ever had any orthodontic treatment previously.?If yes,previous Dr. Name & Address")]
        public string OrthodonticTreatment { get; set; }

        [Display(Name = "Have there been any injuries to your mouth or teeth")]
        public string MouthTeethInjuries { get; set; }

        [Display(Name = "Have you fallen or had any injuries to your jaw or chin?")]
        public string JawChinFallenInjury { get; set; }

        [Display(Name = "Have you ever had any surgery in the head and neck region?")]
        public string HeadNeckSurgery { get; set; }

        [Display(Name = "Do you clench or grind your teeth?")]
        public string ClenchGrindTeeth { get; set; }

        [Display(Name = "Do your jaw muscles ever feel strained")]
        public string StrainedJawMuscles { get; set; }

        [Display(Name = "Does it hurts on chewing?")]
        public string HurtChewing { get; set; }

        [Display(Name = "Do you hear clicking or grating sounds in the jaw joints?If yes,describe?")]
        public string ClickingGratingSounds { get; set; }

        [Display(Name = "Clicking")] public string ClickingRightDesc { get; set; }

        [Display(Name = "Clicking")] public string ClickingLeftDesc { get; set; }

        [Display(Name = "Grating")] public string GratingRightDesc { get; set; }

        [Display(Name = "Grating")] public string GratingLeftDesc { get; set; }

        [Display(Name = "Do these sound began?")]
        public int? SoundBeganId { get; set; }

        [Display(Name = "Have you ever experienced difficulty in opening or closing your jaw?")]
        public string DiffOpenCloseJaw { get; set; }

        [Display(Name = "Have your jaws ever locked when open wide?")]
        public string JawLocked { get; set; }

        [Display(Name = "Have you ever had pain in your jaw joints?")]
        public string JawJointsPain { get; set; }

        [Display(Name = "Does pain starts?")] public int? PainStartsId { get; set; }

        [Display(Name = "What aggrevates pain?")]
        public string AggrevatesPain { get; set; }

        [Display(Name = "What reduces pain?")] public string ReducePain { get; set; }

        [Display(Name = "Type of Feeding Practiced?")]
        public int? FeedingPracticedId { get; set; }

        [Display(Name = "Duration")] public string FeedingDuration { get; set; }

        [Display(Name = "Frequency")] public string FeedingFrequency { get; set; }

        [Display(Name = "Type of Nipple")] public string NippleType { get; set; }

        [Display(Name = "Learning Disabilities")]
        public string LearningDisabilities { get; set; }

        [Display(Name = "Childhood Diseases")] public string ChildhoodDiseases { get; set; }

        [Display(Name = "Surgical Procedure Done")]
        public int? SurgicalProcedureDoneId { get; set; }

        [Display(Name = "Has any other family member undergone orthodontic Treatment?If yes,where?")]
        public string FamilyOrthoTreatment { get; set; }

        [Display(Name = "Type of malocclusion")]
        public string MalocclusionType { get; set; }

        [Display(Name = "Has patient reached adolescent growth?")]
        public string AdolescentGrowth { get; set; }

        [Display(Name = "Built")] public string Built { get; set; }

        [Display(Name = "Lip Tonicity")] public string LipTonicity { get; set; }

        [Display(Name = "Vertical Proportion")]
        public string VerticalProportion { get; set; }

        [Display(Name = "Upper lip Length")] public string UpperLipLength { get; set; }

        [Display(Name = "USFH")] public int? UsfhId { get; set; }

        [Display(Name = "LAFH")] public int? LafhId { get; set; }

        [Display(Name = "Size of Nose")] public int? NoseSizeId { get; set; }

        [Display(Name = "Occlusal Cant")] public string OcclusalCant { get; set; }

        [Display(Name = "Others")] public string ExtraOralOthers { get; set; }

        [Display(Name = "Position of chin")] public int? ChinPositionId { get; set; }

        [Display(Name = "Mandibular Plane")] public int? MandibularPlaneId { get; set; }

        [Display(Name = "Mastication")] public int? MasticationId { get; set; }

        [Display(Name = "Postural Position")] public string PosturalPosition { get; set; }

        [Display(Name = "Width of attached Gingiva")]
        public string GingivaAttachWidth { get; set; }

        [Display(Name = "Posture")] public string SoftTissuePosture { get; set; }

        [Display(Name = "Tonsil")] public int? TonsilId { get; set; }

        [Display(Name = "Function")] public int? TongueFunctionId { get; set; }

        [Display(Name = "Texture")] public int? TextureId { get; set; }

        [Display(Name = "Discolored")] public string DiscolouredTeeth { get; set; }

        [Display(Name = "Key Ridge")] public string KeyRidge { get; set; }

        [Display(Name = "Crowning/Spacing")] public string CrowningSpacing { get; set; }

        [Display(Name = "Individual Tooth Abnormality")]
        public string TeethAbnormality { get; set; }

        [Display(Name = "Deep bite")] public string VerticalDeepbitemm { get; set; }

        [Display(Name = "%")] public string VerticalDeepbitePer { get; set; }

        [Display(Name = "Closed bite")] public string VerticalClosedbitemm { get; set; }

        [Display(Name = "%")] public string VerticalClosedbitePer { get; set; }

        [Display(Name = "Open bite")] public string VerticalOpenbitemm { get; set; }

        [Display(Name = "%")] public string VerticalOpenbitePer { get; set; }

        [Display(Name = "Other Relevent Findings")]
        public string OtherReleventFindings { get; set; }

        [Display(Name = "Midline")] public string TransverseMidline { get; set; }

        [Display(Name = "Treatment Objectives")]
        public string TreatmentObjectives { get; set; }

        [Display(Name = "Anchorage Plan")] public string AnchoragePlan { get; set; }

        [Display(Name = "Treatment Sequence")] public string TreatmentSequence { get; set; }

        [Display(Name = "Appliance Design")] public string ApplianceDesign { get; set; }

        [Display(Name = "Retention Plan")] public string RetentionPlan { get; set; }

        [Display(Name = "Additional Discussion")]
        public string AdditionalDiscussion { get; set; }

        public string CasesheetName { get; set; }

        public string Informer { get; set; }

        public string Delivery { get; set; }

        public string Type { get; set; }

        public string Habits { get; set; }

        public string MouthBreathingHabits { get; set; }

        public string Mouth { get; set; }

        public string Snoring { get; set; }

        public string ParentsConcern { get; set; }

        public string Height { get; set; }

        public string Weight { get; set; }

        public string ShapeOfHead { get; set; }

        public string FaceForm { get; set; }

        public string SmileArc { get; set; }

        [Display(Name = "Face Profile")] public string FaceProfile { get; set; }

        [Display(Name = "Face Divergence")] public string FaceDivergence { get; set; }

        [Display(Name = "Naso Labial Angle")] public string NasoLabialAngleDn { get; set; }

        [Display(Name = "Mento Labial Sulcus")]
        public string MentoLabialSulcus { get; set; }

        public string VTO { get; set; }

        public string Respiration { get; set; }

        public string JawFunction { get; set; }

        public string TMJComplaint { get; set; }

        public string PainHistory { get; set; }

        public string SoundsHistory { get; set; }

        public string TMJPalpation { get; set; }

        public string MusclePalpation { get; set; }

        public string Deglutition { get; set; }

        public string Speech { get; set; }

        public string Gingiva { get; set; }

        public string TongueAnkloglossia { get; set; }

        public string TongueCrenation { get; set; }

        public string MaxillarySummentry { get; set; }

        public string MaxillaryShape { get; set; }

        public string MandibularSummentry { get; set; }

        public string MandibularShape { get; set; }

        public string ClinicalFMA { get; set; }

        public string SoundBegan { get; set; }

        public string PainStarts { get; set; }

        public string FeedingPracticed { get; set; }

        public string SurgicalProcedureDone { get; set; }

        public string LipPosture { get; set; }

        public string Usfh { get; set; }

        public string Lafh { get; set; }

        public string NoseSize { get; set; }

        public string ChinPosition { get; set; }

        public string MandibularPlane { get; set; }

        public string Mastication { get; set; }

        public string PerioralMuscleActivity { get; set; }

        [Display(Name = "Oral Hygiene Status")]
        public string OralHygieneStatus { get; set; }

        [Display(Name = "Brushing Habits")] public string BrushingHabits { get; set; }

        [Display(Name = "Brushing Habits")] public string GingivalJunction { get; set; }

        [Display(Name = "Palatal Contour")] public string PalatalContour { get; set; }

        [Display(Name = "Tonsil")] public string Tonsil { get; set; }

        [Display(Name = "Texture")] public string Texture { get; set; }

        [Display(Name = "Shape & Size")] public string TongueSizeAndShape { get; set; }

        [Display(Name = "Function")] public string TongueFunction { get; set; }

        public long OpNo { get; set; }

        public string PatientName { get; set; }

        public string Phone { get; set; }

        public string AgeGender { get; set; }

        public string Area { get; set; }

        public int? Age { get; set; }

        public int? GenderId { get; set; }

        [Display(Name = "Facial Symmetry")] public string ShortFacialSymmetry { get; set; }

        [Display(Name = "Lips")] public string LipsShort { get; set; }

        [Display(Name = "Growth Pattern")] public string GrowthPattern { get; set; }

        [Display(Name = "Molar Relation Right")]
        public string MolarRelationRShort { get; set; }

        [Display(Name = "Molar Relation Left")]
        public string MolarRelationLShort { get; set; }

        [Display(Name = "Canine Relation Left")]
        public string CanineRelationLShort { get; set; }

        [Display(Name = "Canine Relation Right")]
        public string CanineRelationRShort { get; set; }

        [Display(Name = "Crossbite")] public string CrossbiteShort { get; set; }

        [Display(Name = "Fixed Mechanotherapy")]
        public string FixedMechanotheraphy { get; set; }

        [Display(Name = "Frontal Image")]
        public string ExtraFrontalOralImage { get; set; }

        [Display(Name = "Frontal Smile View Image")]
        public string ExtraFrontalSmileOralImage { get; set; }

        [Display(Name = "Profile Image")]
        public string ExtraProfileOralImage { get; set; }

        [Display(Name = "3/4TH VIEW")]
        public string Extra34ViewOralImage { get; set; }

        [Display(Name = "3/4TH SMILE")]
        public string Extra34SmileOralImage { get; set; }

        [Display(Name = "VID")]
        public string ExtraVidOralImage { get; set; }

        [Display(Name = "Frontal Image")]
        public string IntraFrontalOralImage { get; set; }

        [Display(Name = "RIGHT BUCCAL OCCLUSON")]
        public string IntraRightBuccalOcculsionImage { get; set; }

        [Display(Name = "LEFT BUCCAL OCCLUSON")]
        public string IntraLeftBuccalOcculsionImage { get; set; }

        [Display(Name = "MAXILLARY OCCLUSON")]
        public string IntraMaxillaryOcculsionImage { get; set; }

        [Display(Name = "MANDBULAR OCCLUSON")]
        public string IntraMandBularImage { get; set; }

        public IEnumerable<FileUploadViewModel> FileUploadlist { get; set; }
    }
}
