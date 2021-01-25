// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.ORTHOCasesheetService
// Assembly: DIMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B4386025-DCA5-411F-B793-388E39BEE397
// Assembly location: F:\projects\DENTAL HOSPITAL MANAGEMENT\HKES\hkes\bin\DIMS.dll

using AutoMapper;
using DIMS.Enums;
using DIMS.Services.Abstract;
using DIMS.ViewModels;
using Repository.Base;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Metron.Entities;

namespace DIMS.Services.Implementation
{
  public class ORTHOCasesheetService : ServiceBase<ORTHOCasesheet>, IORTHOCasesheetService, IService<ORTHOCasesheet>
  {
    private IUnitOfWork _uow;
    private IOPDPatientRegistrationService _OPDservice;
    private IMASCodeService _Dropdownservice;
    private IMASChiefComplaintService _ChiefComplaintservice;
    private IReferralStatusService _Referralservice;
    private IMASPresentIllnessService _PresentIllnessService;
    private IBillQueueService _BillQueueservice;
    private IORTHOAnalysisService _AnalysisService;
    private IStudentAllotmentService _AllotmentService;
    private IFollowUpService _FollowUpService;
    private ICasesheetNoService _CasesheetNoService;
    private IPrescriptionsService _PrescriptionsService;

    public ORTHOCasesheetService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._OPDservice = new OPDPatientRegistrationService(this._uow);
      this._Dropdownservice = new MASCodeService(this._uow);
      this._ChiefComplaintservice = new MASChiefComplaintService(this._uow);
      this._Referralservice = new ReferralStatusService(this._uow);
      this._PresentIllnessService = new MASPresentIllnessService(this._uow);
      this._BillQueueservice = new BillQueueService(this._uow);
      this._AnalysisService = new ORTHOAnalysisService(this._uow);
      this._AllotmentService = new StudentAllotmentService(this._uow);
      this._FollowUpService = new FollowUpService(this._uow);
      this._CasesheetNoService = new CasesheetNoService(this._uow);
      this._PrescriptionsService = new PrescriptionsService(this._uow);
    }

    public OrthoViewModal GetORTHOPatientDetails(int Id)
    {
      return this._uow.Repository<OrthoViewModal>().GetEntitiesBySql(string.Format("exec GetORTHOCasesheet {0}", Id)).FirstOrDefault<OrthoViewModal>();
    }

    public IEnumerable<ORTHOCasesheetProperties> GetProperties()
    {
      return this._uow.Repository<ORTHOCasesheetProperties>().GetAll();
    }

    public OrthoViewModal BindOrthoPatientModel(long allotId)
    {
      OrthoViewModal orthoViewModal = new OrthoViewModal();
      if (!string.IsNullOrEmpty(allotId.ToString()))
      {
        IEnumerable<MASCode> allCodes = this._Dropdownservice.GetAllCodes();
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + 5;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 5);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        orthoViewModal.TreatmentReferredId = informationViewModel.ReferredId;
        orthoViewModal.patientInformationViewModel = informationViewModel;
        orthoViewModal.studentAllotmentViewModel = allotmentViewModel;
        orthoViewModal.PatientId = informationViewModel.PatientId;
        orthoViewModal.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        orthoViewModal.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        orthoViewModal.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        orthoViewModal.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 5,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        orthoViewModal.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 5, 0);
        orthoViewModal.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 5, 0);
        orthoViewModal.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 5);
        orthoViewModal.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        orthoViewModal.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 5);
        orthoViewModal.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 5, 0);
        orthoViewModal.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(5);
        ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
        {
          PatientId = informationViewModel.PatientId,
          FromdeptId = 5
        };
        orthoViewModal.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 5).ToList<ReferralStatusViewModel>();
        orthoViewModal.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        orthoViewModal.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, orthoViewModal.PatientId))
        };
        orthoViewModal.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, orthoViewModal.PatientId))
        };
        orthoViewModal.InformerList = allCodes.Where<MASCode>(x => x.CodeTypeId == 25).ToList<MASCode>();
        orthoViewModal.DeliveryList = allCodes.Where<MASCode>(x => x.CodeTypeId == 26).ToList<MASCode>();
        orthoViewModal.TypeList = allCodes.Where<MASCode>(x => x.CodeTypeId == 27).ToList<MASCode>();
        orthoViewModal.HabitList = allCodes.Where<MASCode>(x => x.CodeTypeId == 28).ToList<MASCode>();
        orthoViewModal.MouthBreathingList = allCodes.Where<MASCode>(x => x.CodeTypeId == 29).ToList<MASCode>();
        orthoViewModal.SnoringList = allCodes.Where<MASCode>(x => x.CodeTypeId == 30).ToList<MASCode>();
        orthoViewModal.MouthList = allCodes.Where<MASCode>(x => x.CodeTypeId == 31).ToList<MASCode>();
        orthoViewModal.ParentConcernList = allCodes.Where<MASCode>(x => x.CodeTypeId == 32).ToList<MASCode>();
        orthoViewModal.HeightList = allCodes.Where<MASCode>(x => x.CodeTypeId == 33).ToList<MASCode>();
        orthoViewModal.WeightList = allCodes.Where<MASCode>(x => x.CodeTypeId == 34).ToList<MASCode>();
        orthoViewModal.SmileArcList = allCodes.Where<MASCode>(x => x.CodeTypeId == 35).ToList<MASCode>();
        orthoViewModal.HeadShapeList = allCodes.Where<MASCode>(x => x.CodeTypeId == 186).ToList<MASCode>();
        orthoViewModal.FacialFormList = allCodes.Where<MASCode>(x => x.CodeTypeId == 187).ToList<MASCode>();
        orthoViewModal.FacialProfileOrthoList = allCodes.Where<MASCode>(x => x.CodeTypeId == 188).ToList<MASCode>();
        orthoViewModal.FacialDivergenceList = allCodes.Where<MASCode>(x => x.CodeTypeId == 189).ToList<MASCode>();
        orthoViewModal.NasolabialAngleList = allCodes.Where<MASCode>(x => x.CodeTypeId == 190).ToList<MASCode>();
        orthoViewModal.ClinicalFMAList = allCodes.Where<MASCode>(x => x.CodeTypeId == 191).ToList<MASCode>();
        orthoViewModal.MentoLabialSulcusList = allCodes.Where<MASCode>(x => x.CodeTypeId == 192).ToList<MASCode>();
        orthoViewModal.VtoList = allCodes.Where<MASCode>(x => x.CodeTypeId == 193).ToList<MASCode>();
        orthoViewModal.TongueAnkyloglossiaList = allCodes.Where<MASCode>(x => x.CodeTypeId == 195).ToList<MASCode>();
        orthoViewModal.TongueCrenationList = allCodes.Where<MASCode>(x => x.CodeTypeId == 196).ToList<MASCode>();
        orthoViewModal.RespirationList = allCodes.Where<MASCode>(x => x.CodeTypeId == 37).ToList<MASCode>();
        orthoViewModal.YesNoList = allCodes.Where<MASCode>(x => x.CodeTypeId == 38).ToList<MASCode>();
        orthoViewModal.DeglutitionList = allCodes.Where<MASCode>(x => x.CodeTypeId == 36).ToList<MASCode>();
        orthoViewModal.GingivalList = allCodes.Where<MASCode>(x => x.CodeTypeId == 39).ToList<MASCode>();
        orthoViewModal.MaxiSymmetryList = allCodes.Where<MASCode>(x => x.CodeTypeId == 40).ToList<MASCode>();
        orthoViewModal.MaxiShapeList = allCodes.Where<MASCode>(x => x.CodeTypeId == 41).ToList<MASCode>();
        orthoViewModal.MandiSymmetryList = allCodes.Where<MASCode>(x => x.CodeTypeId == 42).ToList<MASCode>();
        orthoViewModal.MandiShapeList = allCodes.Where<MASCode>(x => x.CodeTypeId == 43).ToList<MASCode>();
        orthoViewModal.ShortFacialSymmetryList = allCodes.Where<MASCode>(x => x.CodeTypeId == 245).ToList<MASCode>();
        orthoViewModal.GrowthPatternList = allCodes.Where<MASCode>(x => x.CodeTypeId == 246).ToList<MASCode>();
        orthoViewModal.LipsShortList = allCodes.Where<MASCode>(x => x.CodeTypeId == 247).ToList<MASCode>();
        orthoViewModal.CrossbiteShortList = allCodes.Where<MASCode>(x => x.CodeTypeId == 248).ToList<MASCode>();
        orthoViewModal.MolarRelationLShortList = allCodes.Where<MASCode>(x => x.CodeTypeId == 249).ToList<MASCode>();
        orthoViewModal.MolarRelationRShortList = allCodes.Where<MASCode>(x => x.CodeTypeId == 250).ToList<MASCode>();
        orthoViewModal.CanineRelationLShortList = allCodes.Where<MASCode>(x => x.CodeTypeId == 251).ToList<MASCode>();
        orthoViewModal.CanineRelationRShortList = allCodes.Where<MASCode>(x => x.CodeTypeId == 252).ToList<MASCode>();
        orthoViewModal.FixedMechanotheraphyList = allCodes.Where<MASCode>(x => x.CodeTypeId == 253).ToList<MASCode>();
        orthoViewModal.PainStartsList = allCodes.Where<MASCode>(x => x.CodeTypeId == (int) byte.MaxValue).ToList<MASCode>();
        orthoViewModal.SoundBeganList = allCodes.Where<MASCode>(x => x.CodeTypeId == 254).ToList<MASCode>();
        orthoViewModal.SurgicalProcedureDoneList = allCodes.Where<MASCode>(x => x.CodeTypeId == 257).ToList<MASCode>();
        orthoViewModal.FeedingPracticedList = allCodes.Where<MASCode>(x => x.CodeTypeId == 256).ToList<MASCode>();
        orthoViewModal.LipPostureList = allCodes.Where<MASCode>(x => x.CodeTypeId == 258).ToList<MASCode>();
        orthoViewModal.UsfhList = allCodes.Where<MASCode>(x => x.CodeTypeId == 259).ToList<MASCode>();
        orthoViewModal.LafhList = allCodes.Where<MASCode>(x => x.CodeTypeId == 260).ToList<MASCode>();
        orthoViewModal.NoseSizeList = allCodes.Where<MASCode>(x => x.CodeTypeId == 261).ToList<MASCode>();
        orthoViewModal.MandibularPlaneList = allCodes.Where<MASCode>(x => x.CodeTypeId == 263).ToList<MASCode>();
        orthoViewModal.ChinPositionList = allCodes.Where<MASCode>(x => x.CodeTypeId == 262).ToList<MASCode>();
        orthoViewModal.MasticationList = allCodes.Where<MASCode>(x => x.CodeTypeId == 264).ToList<MASCode>();
        orthoViewModal.SpeechList = allCodes.Where<MASCode>(x => x.CodeTypeId == 126).ToList<MASCode>();
        orthoViewModal.PerioralMuscleActivityList = allCodes.Where<MASCode>(x => x.CodeTypeId == 265).ToList<MASCode>();
        orthoViewModal.OralHygieneStatusList = allCodes.Where<MASCode>(x => x.CodeTypeId == 266).ToList<MASCode>();
        orthoViewModal.BrushingHabitsList = allCodes.Where<MASCode>(x => x.CodeTypeId == 267).ToList<MASCode>();
        orthoViewModal.GingivalJunctionList = allCodes.Where<MASCode>(x => x.CodeTypeId == 268).ToList<MASCode>();
        orthoViewModal.PalatalContourList = allCodes.Where<MASCode>(x => x.CodeTypeId == 269).ToList<MASCode>();
        orthoViewModal.TonsilList = allCodes.Where<MASCode>(x => x.CodeTypeId == 270).ToList<MASCode>();
        orthoViewModal.TongueFunctionList = allCodes.Where<MASCode>(x => x.CodeTypeId == 271).ToList<MASCode>();
        orthoViewModal.TongueSizeAndShapeList = allCodes.Where<MASCode>(x => x.CodeTypeId == 272).ToList<MASCode>();
        orthoViewModal.TextureList = allCodes.Where<MASCode>(x => x.CodeTypeId == 273).ToList<MASCode>();
        IEnumerable<ORTHOCasesheetProperties> properties = this.GetProperties();
        orthoViewModal.Proplist = properties;
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        orthoViewModal.medicalalertviewmodel = medicalAlertViewModel2;
      }
      return orthoViewModal;
    }

    public int SaveORTHOPatient(OrthoViewModal model)
    {
      try
      {
        model.MandatoryDummy = "Y";
        ORTHOCasesheet orthoCasesheet = new ORTHOCasesheet();
        ORTHOCasesheet entity1 = new MapperConfiguration(cfg => cfg.CreateMap<OrthoViewModal, ORTHOCasesheet>()).CreateMapper().Map<OrthoViewModal, ORTHOCasesheet>(model);
        entity1.OrthoDate = DateTime.Now;
        entity1.CreatedDate = new DateTime?(DateTime.Now);
        entity1.LastVisitedDate = new DateTime?(DateTime.Now);
        entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
        entity1.OrthoNo = this._CasesheetNoService.GetOrthoNo();
        int num = this._uow.Repository<ORTHOCasesheet>().Add(entity1, false);
        IEnumerable<ORTHOCasesheetProperties> all = this._uow.Repository<ORTHOCasesheetProperties>().GetAll();
        foreach (PropertyInfo property in model.GetType().GetProperties())
        {
          PropertyInfo prop = property;
          if (all.FirstOrDefault<ORTHOCasesheetProperties>(a => a.PropertyName == prop.Name) != null)
          {
            string name = prop.Name;
            object obj = prop.GetValue(model, null);
            if (obj != null)
            {
              ORTHOCasesheetPropertyValues entity2 = new ORTHOCasesheetPropertyValues();
              entity2.OrthoId = num;
              ORTHOCasesheetProperties casesheetProperties = all.FirstOrDefault<ORTHOCasesheetProperties>(a => a.PropertyName == prop.Name);
              if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
              {
                entity2.PropId = casesheetProperties.PropertyId;
                entity2.PropValues = obj.ToString();
                this._uow.Repository<ORTHOCasesheetPropertyValues>().Add(entity2, false);
              }
            }
          }
        }
        model.OrthoId = num;
        if (model.CreatedepartmentReferredStatus != null)
        {
          ReferralStatus entity2 = new ReferralStatus();
          foreach (ReferralStatusViewModel createdepartmentReferredStatu in model.CreatedepartmentReferredStatus)
          {
            if (createdepartmentReferredStatu.chkToDeptId)
            {
              entity2.PatientId = model.PatientId;
              entity2.FromdeptId = 5;
              entity2.ReferredReason = createdepartmentReferredStatu.ReferredReason;
              entity2.ToDeptId = createdepartmentReferredStatu.ToDeptId;
              entity2.Priority = createdepartmentReferredStatu.Priority;
              entity2.RoomNo = createdepartmentReferredStatu.RoomNo;
              entity2.TreatmentStatus = createdepartmentReferredStatu.TreatmentStatus;
              entity2.FromDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
              entity2.CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
              entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
              this._Referralservice.Add(entity2);
            }
          }
        }
        if (model.BillingQueueDetails != null)
          this._BillQueueservice.SaveInvestigation(model.BillingQueueDetails, new BillQueueDetails()
          {
            CaserecordId = num,
            ReferredTreatmentId = 0,
            DeptId = 5,
            PatientId = model.PatientId
          });
        if (model.BillingLabRadQueueDetails != null)
          this._BillQueueservice.SaveInvestigation(model.BillingLabRadQueueDetails, new BillQueueDetails()
          {
            CaserecordId = num,
            ReferredTreatmentId = 0,
            DeptId = 5,
            PatientId = model.PatientId
          });
        if (model.PrescriptionsDetails != null)
          this._PrescriptionsService.SavePrescriptions(model.PrescriptionsDetails, new Prescriptions()
          {
            PatientId = model.PatientId,
            AllotId = model.studentAllotmentViewModel.AllotId,
            DoctorId = model.studentAllotmentViewModel.DoctorId,
            StudentId = model.studentAllotmentViewModel.StudentId,
            DeptId = 5,
            ReferredTreatmentId = 0
          });
        return num;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public OrthoViewModal BindEditORTHOPatientModel(long allotId, int OrthoId)
    {
      try
      {
        IEnumerable<MASCode> allCodes = this._Dropdownservice.GetAllCodes();
        OrthoViewModal orthoPatientDetails = this.GetORTHOPatientDetails(OrthoId);
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + 5;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 5);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        allotmentViewModel.DoctorId = informationViewModel.DoctorId;
        ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
        {
          PatientId = informationViewModel.PatientId,
          FromdeptId = 5
        };
        orthoPatientDetails.patientInformationViewModel = informationViewModel;
        orthoPatientDetails.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 5).ToList<ReferralStatusViewModel>();
        orthoPatientDetails.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        orthoPatientDetails.ApprovedepartmentReferral = this._Referralservice.ReferralApprovalList(informationViewModel.PatientId, 5).ToList<ReferralStatusViewModel>();
        orthoPatientDetails.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 5, 0);
        orthoPatientDetails.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 5);
        orthoPatientDetails.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        orthoPatientDetails.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 5);
        orthoPatientDetails.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 5, 0);
        orthoPatientDetails.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(5);
        orthoPatientDetails.studentAllotmentViewModel = allotmentViewModel;
        orthoPatientDetails.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 5, 0);
        orthoPatientDetails.TreatmentReferredId = informationViewModel.ReferredId;
        orthoPatientDetails.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 5,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        orthoPatientDetails.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 5, 0);
        orthoPatientDetails.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        orthoPatientDetails.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        orthoPatientDetails.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        orthoPatientDetails.InformerList = allCodes.Where<MASCode>(x => x.CodeTypeId == 25).ToList<MASCode>();
        orthoPatientDetails.DeliveryList = allCodes.Where<MASCode>(x => x.CodeTypeId == 26).ToList<MASCode>();
        orthoPatientDetails.TypeList = allCodes.Where<MASCode>(x => x.CodeTypeId == 27).ToList<MASCode>();
        orthoPatientDetails.HabitList = allCodes.Where<MASCode>(x => x.CodeTypeId == 28).ToList<MASCode>();
        orthoPatientDetails.MouthBreathingList = allCodes.Where<MASCode>(x => x.CodeTypeId == 29).ToList<MASCode>();
        orthoPatientDetails.SnoringList = allCodes.Where<MASCode>(x => x.CodeTypeId == 30).ToList<MASCode>();
        orthoPatientDetails.MouthList = allCodes.Where<MASCode>(x => x.CodeTypeId == 31).ToList<MASCode>();
        orthoPatientDetails.ParentConcernList = allCodes.Where<MASCode>(x => x.CodeTypeId == 32).ToList<MASCode>();
        orthoPatientDetails.HeightList = allCodes.Where<MASCode>(x => x.CodeTypeId == 33).ToList<MASCode>();
        orthoPatientDetails.WeightList = allCodes.Where<MASCode>(x => x.CodeTypeId == 34).ToList<MASCode>();
        orthoPatientDetails.SmileArcList = allCodes.Where<MASCode>(x => x.CodeTypeId == 35).ToList<MASCode>();
        orthoPatientDetails.RespirationList = allCodes.Where<MASCode>(x => x.CodeTypeId == 37).ToList<MASCode>();
        orthoPatientDetails.YesNoList = allCodes.Where<MASCode>(x => x.CodeTypeId == 38).ToList<MASCode>();
        orthoPatientDetails.DeglutitionList = allCodes.Where<MASCode>(x => x.CodeTypeId == 36).ToList<MASCode>();
        orthoPatientDetails.GingivalList = allCodes.Where<MASCode>(x => x.CodeTypeId == 39).ToList<MASCode>();
        orthoPatientDetails.MaxiSymmetryList = allCodes.Where<MASCode>(x => x.CodeTypeId == 40).ToList<MASCode>();
        orthoPatientDetails.MaxiShapeList = allCodes.Where<MASCode>(x => x.CodeTypeId == 41).ToList<MASCode>();
        orthoPatientDetails.MandiSymmetryList = allCodes.Where<MASCode>(x => x.CodeTypeId == 42).ToList<MASCode>();
        orthoPatientDetails.MandiShapeList = allCodes.Where<MASCode>(x => x.CodeTypeId == 43).ToList<MASCode>();
        orthoPatientDetails.HeadShapeList = allCodes.Where<MASCode>(x => x.CodeTypeId == 186).ToList<MASCode>();
        orthoPatientDetails.FacialFormList = allCodes.Where<MASCode>(x => x.CodeTypeId == 187).ToList<MASCode>();
        orthoPatientDetails.FacialProfileOrthoList = allCodes.Where<MASCode>(x => x.CodeTypeId == 188).ToList<MASCode>();
        orthoPatientDetails.FacialDivergenceList = allCodes.Where<MASCode>(x => x.CodeTypeId == 189).ToList<MASCode>();
        orthoPatientDetails.NasolabialAngleList = allCodes.Where<MASCode>(x => x.CodeTypeId == 190).ToList<MASCode>();
        orthoPatientDetails.ClinicalFMAList = allCodes.Where<MASCode>(x => x.CodeTypeId == 191).ToList<MASCode>();
        orthoPatientDetails.MentoLabialSulcusList = allCodes.Where<MASCode>(x => x.CodeTypeId == 192).ToList<MASCode>();
        orthoPatientDetails.VtoList = allCodes.Where<MASCode>(x => x.CodeTypeId == 193).ToList<MASCode>();
        orthoPatientDetails.TongueAnkyloglossiaList = allCodes.Where<MASCode>(x => x.CodeTypeId == 195).ToList<MASCode>();
        orthoPatientDetails.TongueCrenationList = allCodes.Where<MASCode>(x => x.CodeTypeId == 196).ToList<MASCode>();
        orthoPatientDetails.ShortFacialSymmetryList = allCodes.Where<MASCode>(x => x.CodeTypeId == 245).ToList<MASCode>();
        orthoPatientDetails.GrowthPatternList = allCodes.Where<MASCode>(x => x.CodeTypeId == 246).ToList<MASCode>();
        orthoPatientDetails.LipsShortList = allCodes.Where<MASCode>(x => x.CodeTypeId == 247).ToList<MASCode>();
        orthoPatientDetails.CrossbiteShortList = allCodes.Where<MASCode>(x => x.CodeTypeId == 248).ToList<MASCode>();
        orthoPatientDetails.MolarRelationLShortList = allCodes.Where<MASCode>(x => x.CodeTypeId == 249).ToList<MASCode>();
        orthoPatientDetails.MolarRelationRShortList = allCodes.Where<MASCode>(x => x.CodeTypeId == 250).ToList<MASCode>();
        orthoPatientDetails.CanineRelationLShortList = allCodes.Where<MASCode>(x => x.CodeTypeId == 251).ToList<MASCode>();
        orthoPatientDetails.CanineRelationRShortList = allCodes.Where<MASCode>(x => x.CodeTypeId == 252).ToList<MASCode>();
        orthoPatientDetails.FixedMechanotheraphyList = allCodes.Where<MASCode>(x => x.CodeTypeId == 253).ToList<MASCode>();
        orthoPatientDetails.PainStartsList = allCodes.Where<MASCode>(x => x.CodeTypeId == (int) byte.MaxValue).ToList<MASCode>();
        orthoPatientDetails.SoundBeganList = allCodes.Where<MASCode>(x => x.CodeTypeId == 254).ToList<MASCode>();
        orthoPatientDetails.FeedingPracticedList = allCodes.Where<MASCode>(x => x.CodeTypeId == 256).ToList<MASCode>();
        orthoPatientDetails.SurgicalProcedureDoneList = allCodes.Where<MASCode>(x => x.CodeTypeId == 257).ToList<MASCode>();
        orthoPatientDetails.LipPostureList = allCodes.Where<MASCode>(x => x.CodeTypeId == 258).ToList<MASCode>();
        orthoPatientDetails.UsfhList = allCodes.Where<MASCode>(x => x.CodeTypeId == 259).ToList<MASCode>();
        orthoPatientDetails.LafhList = allCodes.Where<MASCode>(x => x.CodeTypeId == 260).ToList<MASCode>();
        orthoPatientDetails.NoseSizeList = allCodes.Where<MASCode>(x => x.CodeTypeId == 261).ToList<MASCode>();
        orthoPatientDetails.MandibularPlaneList = allCodes.Where<MASCode>(x => x.CodeTypeId == 263).ToList<MASCode>();
        orthoPatientDetails.ChinPositionList = allCodes.Where<MASCode>(x => x.CodeTypeId == 262).ToList<MASCode>();
        orthoPatientDetails.MasticationList = allCodes.Where<MASCode>(x => x.CodeTypeId == 264).ToList<MASCode>();
        orthoPatientDetails.SpeechList = allCodes.Where<MASCode>(x => x.CodeTypeId == 126).ToList<MASCode>();
        orthoPatientDetails.PerioralMuscleActivityList = allCodes.Where<MASCode>(x => x.CodeTypeId == 265).ToList<MASCode>();
        orthoPatientDetails.OralHygieneStatusList = allCodes.Where<MASCode>(x => x.CodeTypeId == 266).ToList<MASCode>();
        orthoPatientDetails.BrushingHabitsList = allCodes.Where<MASCode>(x => x.CodeTypeId == 267).ToList<MASCode>();
        orthoPatientDetails.GingivalJunctionList = allCodes.Where<MASCode>(x => x.CodeTypeId == 268).ToList<MASCode>();
        orthoPatientDetails.PalatalContourList = allCodes.Where<MASCode>(x => x.CodeTypeId == 269).ToList<MASCode>();
        orthoPatientDetails.TonsilList = allCodes.Where<MASCode>(x => x.CodeTypeId == 270).ToList<MASCode>();
        orthoPatientDetails.TongueFunctionList = allCodes.Where<MASCode>(x => x.CodeTypeId == 271).ToList<MASCode>();
        orthoPatientDetails.TongueSizeAndShapeList = allCodes.Where<MASCode>(x => x.CodeTypeId == 272).ToList<MASCode>();
        orthoPatientDetails.TextureList = allCodes.Where<MASCode>(x => x.CodeTypeId == 273).ToList<MASCode>();
        OrthoStaticAnalysisViewModel analysisViewModel = new OrthoStaticAnalysisViewModel();
        analysisViewModel.AnalysisList = this._AnalysisService.AnalysisList().ToList<OrthoStaticAnalysisViewModel>();
        orthoPatientDetails.OrthoAnalysisList = analysisViewModel;
        analysisViewModel.OrthoId = OrthoId;
        IEnumerable<ORTHOCasesheetProperties> properties = this.GetProperties();
        orthoPatientDetails.Proplist = properties;
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        orthoPatientDetails.medicalalertviewmodel = medicalAlertViewModel2;
        orthoPatientDetails.approvalViewModal = new ApprovalViewModal()
        {
          ApprovalTypeId = 1,
          CaserecordId = OrthoId,
          DeptId = 5,
          DoctorId = allotmentViewModel.DoctorId,
          PatientId = orthoPatientDetails.PatientId,
          ReferredTreatmentId = 0
        };
        orthoPatientDetails.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, orthoPatientDetails.PatientId))
        };
        orthoPatientDetails.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, orthoPatientDetails.PatientId))
        };
        if (orthoPatientDetails.Approval1)
        {
          orthoPatientDetails.DisplayApproval1 = "Approved";
          orthoPatientDetails.ReadOnlyApproval1 = true;
        }
        else
        {
          orthoPatientDetails.DisplayApproval1 = "Not Approved";
          orthoPatientDetails.ReadOnlyApproval1 = false;
        }
        if (orthoPatientDetails.Approval2)
        {
          orthoPatientDetails.DisplayApproval2 = "Approved";
          orthoPatientDetails.ReadOnlyApproval2 = true;
          orthoPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = true;
        }
        else
        {
          orthoPatientDetails.DisplayApproval2 = "Not Approved";
          orthoPatientDetails.ReadOnlyApproval2 = false;
          orthoPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = false;
        }
        if (orthoPatientDetails.Approval3)
        {
          orthoPatientDetails.DisplayApproval3 = "Casesheet Closed";
          orthoPatientDetails.ReadOnlyApproval3 = true;
          orthoPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = true;
          orthoPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = true;
        }
        else
        {
          orthoPatientDetails.DisplayApproval3 = "Not Approved";
          orthoPatientDetails.ReadOnlyApproval3 = false;
          orthoPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = false;
          orthoPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = false;
        }
        OrthoViewModal orthoViewModal = this._uow.Repository<OrthoViewModal>().GetEntitiesBySql(string.Format(Queries.DisplayOrthoApprovalStage, 5, orthoPatientDetails.OrthoId)).FirstOrDefault<OrthoViewModal>();
        orthoPatientDetails.ApprovalStage = orthoViewModal == null ? "Not Initiated" : (!(orthoViewModal.ApprovalStage == "") ? orthoViewModal.ApprovalStage : "Not Initiated");
        orthoPatientDetails.ReadOnlyApproval4 = true;
        return orthoPatientDetails;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int UpdateORTHOPatient(OrthoViewModal ViewModel)
    {
      ViewModel.OrthoDate = Convert.ToDateTime(ViewModel.OrthoDate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
      ViewModel.OrthoId = ViewModel.OrthoId;
      ORTHOCasesheet orthoCasesheet = new ORTHOCasesheet();
      Mapper.Initialize(cfg => cfg.CreateMap<OrthoViewModal, ORTHOCasesheet>());
      ORTHOCasesheet entity1 = Mapper.Map<OrthoViewModal, ORTHOCasesheet>(ViewModel);
      entity1.ModifiedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
      entity1.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      this._uow.Repository<ORTHOCasesheet>().Update(entity1, false);
      IEnumerable<ORTHOCasesheetProperties> all1 = this._uow.Repository<ORTHOCasesheetProperties>().GetAll();
      IEnumerable<ORTHOCasesheetPropertyValues> all2 = this._uow.Repository<ORTHOCasesheetPropertyValues>().GetAll("OrthoId=" + ViewModel.OrthoId);
      foreach (PropertyInfo property1 in ViewModel.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<ORTHOCasesheetProperties>(a => a.PropertyName == prop.Name) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue(ViewModel, null);
          if (obj != null)
          {
            ORTHOCasesheetPropertyValues entity2 = new ORTHOCasesheetPropertyValues();
            entity2.OrthoId = ViewModel.OrthoId;
            ORTHOCasesheetProperties property = all1.FirstOrDefault<ORTHOCasesheetProperties>(a => a.PropertyName == prop.Name);
            if ((!(property.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(property.PropertyDataType == "int") || (int) obj != 0))
            {
              ORTHOCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<ORTHOCasesheetPropertyValues>(a =>
              {
                  if (a.PropId == property.PropertyId)
                      return a.OrthoId == ViewModel.OrthoId;
                  return false;
              });
              if (casesheetPropertyValues != null)
              {
                entity2.PropId = property.PropertyId;
                entity2.PropValues = obj.ToString();
                entity2.ValueId = casesheetPropertyValues.ValueId;
                this._uow.Repository<ORTHOCasesheetPropertyValues>().Update(entity2, false);
              }
              else
              {
                entity2.PropId = property.PropertyId;
                entity2.PropValues = obj.ToString();
                this._uow.Repository<ORTHOCasesheetPropertyValues>().Add(entity2, false);
              }
            }
          }
        }
      }
      ReferralStatus entity3 = new ReferralStatus();
      if (ViewModel.CreatedepartmentReferredStatus != null)
      {
        foreach (ReferralStatusViewModel createdepartmentReferredStatu in ViewModel.CreatedepartmentReferredStatus)
        {
          if (createdepartmentReferredStatu.chkToDeptId)
          {
            entity3.PatientId = ViewModel.PatientId;
            entity3.FromdeptId = 5;
            entity3.ReferredReason = createdepartmentReferredStatu.ReferredReason;
            entity3.ToDeptId = createdepartmentReferredStatu.ToDeptId;
            entity3.Priority = createdepartmentReferredStatu.Priority;
            entity3.RoomNo = createdepartmentReferredStatu.RoomNo;
            entity3.TreatmentStatus = createdepartmentReferredStatu.TreatmentStatus;
            entity3.FromDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            entity3.CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            entity3.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            this._Referralservice.Add(entity3);
          }
        }
      }
      if (ViewModel.BillingQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.OrthoId,
          ReferredTreatmentId = 0,
          DeptId = 5,
          PatientId = ViewModel.PatientId
        });
      if (ViewModel.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.OrthoId,
          ReferredTreatmentId = 0,
          DeptId = 5,
          PatientId = ViewModel.PatientId
        });
      if (ViewModel.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(ViewModel.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = ViewModel.PatientId,
          AllotId = ViewModel.studentAllotmentViewModel.AllotId,
          DoctorId = ViewModel.studentAllotmentViewModel.DoctorId,
          StudentId = ViewModel.studentAllotmentViewModel.StudentId,
          DeptId = 5,
          ReferredTreatmentId = 0
        });
      return entity1.OrthoId;
    }

    public StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType)
    {
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      int DeptId = 5;
      string DeptCode = Department.ORTHO.ToString();
      return this._AllotmentService.DisplayAllotment(PatientId, ReferredId, CourseType, DeptId, DeptCode);
    }

    public void UpdateAllotment(OrthoViewModal model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        ProcedureNotes = model.studentAllotmentViewModel.ProcedureNotes,
        ProcedureNotesDate = new DateTime?(DateTime.Now),
        DoctorId = model.studentAllotmentViewModel.DoctorId,
        AllotId = model.studentAllotmentViewModel.AllotId,
        CaserecordId = model.OrthoId,
        ReferredTreatmentId = 0
      });
    }

    public void SavefollowUp(OrthoViewModal model)
    {
      this._FollowUpService.Add(new FollowUp()
      {
        PatientId = model.PatientId,
        FollowupDate = new DateTime?(model.followupViewModal.FollowupDate),
        FollowupTime = new DateTime?(model.followupViewModal.FollowupTime),
        DeptId = 5,
        FollowupReason = model.followupViewModal.FollowupReason,
        CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))),
        CreatedSystem = this._Dropdownservice.GetIPAddress(false),
        ReferredTreatmentId = 0
      });
    }

    public void UpdateReferralStatus(OrthoViewModal model)
    {
      int patientId = model.PatientId;
      int num = 5;
      this._Referralservice.UpdateReferralStatus(new ReferralStatus()
      {
        VisitType = "dbo.GetVisitType(" + patientId + "," + num + "," + model.TreatmentReferredId + ")",
        ReferredId = model.TreatmentReferredId,
        ReferredTreatmentId = 0
      });
    }

    public List<OrthoSearchDetails> orthoSearchDetails(string From_Date, string To_Date, string url)
    {
      string str = "(OP.IsDummy ='N')";
      if (this._uow.Repository<IsDummyEnable>().GetAll("SearchForm ='Y'").Count<IsDummyEnable>() > 0)
        str = "(0=0)";
      return this._uow.Repository<OrthoSearchDetails>().GetEntitiesBySql(string.Format(Queries.ORTHOSearch, (object) From_Date, (object) To_Date, (object) 5, (object) url, (object) str)).ToList<OrthoSearchDetails>();
    }

    public OrthoViewModal BindTreatmentList(long allotId)
    {
      OrthoViewModal orthoViewModal = new OrthoViewModal();
      if (!string.IsNullOrEmpty(allotId.ToString()))
      {
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + 5;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (Gender) informationViewModel.GenderId;
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        orthoViewModal.TreatmentReferredId = informationViewModel.ReferredId;
        orthoViewModal.patientInformationViewModel = informationViewModel;
        orthoViewModal.studentAllotmentViewModel = allotmentViewModel;
        orthoViewModal.PatientId = informationViewModel.PatientId;
        orthoViewModal.Treatmentlist = this._uow.Repository<OrthoViewModal>().GetEntitiesBySql(string.Format(Queries.ORTHOPatientTreatmentList, informationViewModel.PatientId)).ToList<OrthoViewModal>();
      }
      return orthoViewModal;
    }

    public OrthoViewModal BindORTHOPatientReport(int OrthoId)
    {
      try
      {
        OrthoViewModal orthoViewModal1 = new OrthoViewModal();
        OrthoViewModal orthoViewModal2 = this._uow.Repository<OrthoViewModal>().GetEntitiesBySql(string.Format("exec GetORTHOCasesheetReport {0}", OrthoId)).FirstOrDefault<OrthoViewModal>();
        orthoViewModal2.patientInformationViewModel = new PatientInformationViewModel()
        {
          PatientName = orthoViewModal2.PatientName,
          AgeGender = orthoViewModal2.Age.ToString() + "/" + (Gender) orthoViewModal2.GenderId.Value,
          Phone = orthoViewModal2.Phone,
          OpNo = orthoViewModal2.OpNo,
          Area = orthoViewModal2.Area
        };
        orthoViewModal2.FileUploadlist = this._uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, orthoViewModal2.PatientId, 5));
        orthoViewModal2.paidInvestigationList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.PaidBillsforReport, (object) orthoViewModal2.PatientId, (object) 5, (object) OrthoId, (object) 0, (object) 20, (object) 16));
        orthoViewModal2.deptTreatmentList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.DeptTreatmentforReport, (object) orthoViewModal2.PatientId, (object) 5, (object) OrthoId, (object) 0));
        ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
        {
          PatientId = orthoViewModal2.PatientId,
          FromdeptId = 5
        };
        orthoViewModal2.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(orthoViewModal2.PatientId).ToList<ReferralStatusViewModel>();
        orthoViewModal2.followupViewModal = new FollowupViewModal()
        {
          PatientId = orthoViewModal2.PatientId,
          DeptId = 5,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        orthoViewModal2.followupList = this._FollowUpService.LoadFollowupList(orthoViewModal2.PatientId, 5, 0);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        orthoViewModal2.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(orthoViewModal2.PatientId, 5, 0);
        return orthoViewModal2;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public List<StudentAllotmentViewModel> orthoCasesheetApprovalList(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.ORTHOApproval, (object) From_Date, (object) To_Date, (object) 5, (object) url)).ToList<StudentAllotmentViewModel>();
    }

    public void ProcedureApproval(OrthoViewModal model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        DoctorApproval = "Y",
        DoctorApprovalDate = new DateTime?(DateTime.Now),
        AllotId = model.studentAllotmentViewModel.AllotId
      });
    }

    public void SendApproval(long AllotId)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        DoctorApproval = "N",
        ProcedureNotesDate = new DateTime?(DateTime.Now),
        AllotId = AllotId
      });
    }
  }
}
