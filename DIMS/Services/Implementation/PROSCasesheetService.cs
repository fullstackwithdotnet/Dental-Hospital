// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.PROSCasesheetService
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
  public class PROSCasesheetService : ServiceBase<TreatmentTypes>, IPROSCasesheetService, IService<TreatmentTypes>
  {
    private IUnitOfWork _uow;
    private IOPDPatientRegistrationService _OPDservice;
    private IMASChiefComplaintService _ChiefComplaintservice;
    private IMASCodeService _Dropdownservice;
    private IReferralStatusService _Referralservice;
    private IBillQueueService _BillQueueservice;
    private IStudentAllotmentService _AllotmentService;
    private IFollowUpService _FollowUpService;
    private ICasesheetNoService _CasesheetNoService;
    private IPrescriptionsService _PrescriptionsService;

    public PROSCasesheetService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._OPDservice = (IOPDPatientRegistrationService) new OPDPatientRegistrationService(this._uow);
      this._ChiefComplaintservice = (IMASChiefComplaintService) new MASChiefComplaintService(this._uow);
      this._Dropdownservice = (IMASCodeService) new MASCodeService(this._uow);
      this._Referralservice = (IReferralStatusService) new ReferralStatusService(this._uow);
      this._BillQueueservice = (IBillQueueService) new BillQueueService(this._uow);
      this._AllotmentService = (IStudentAllotmentService) new StudentAllotmentService(this._uow);
      this._FollowUpService = (IFollowUpService) new FollowUpService(this._uow);
      this._CasesheetNoService = (ICasesheetNoService) new CasesheetNoService(this._uow);
      this._PrescriptionsService = (IPrescriptionsService) new PrescriptionsService(this._uow);
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg =>
      {
        cfg.CreateMap<PROSTreatmentViewModel, TreatmentTypes>();
        cfg.CreateMap<TreatmentTypes, PROSTreatmentViewModel>();
      }));
    }

    public StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType)
    {
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      int DeptId = 7;
      string DeptCode = Department.PROSTHO.ToString();
      return this._AllotmentService.DisplayAllotment(PatientId, ReferredId, CourseType, DeptId, DeptCode);
    }

    public IEnumerable<PROSTreatmentViewModel> PROSTreatmentList(long allotId)
    {
      List<PROSTreatmentViewModel> treatmentViewModelList = new List<PROSTreatmentViewModel>();
      foreach (TreatmentTypes treatmentTypes in this._uow.Repository<TreatmentTypes>().GetEntitiesBySql(string.Format(Queries.Treatments, (object) 7)))
        treatmentViewModelList.Add(new PROSTreatmentViewModel()
        {
          TreatmentId = treatmentTypes.TreatmentId,
          TreatmentName = treatmentTypes.TreatmentName,
          AllotId = allotId
        });
      return (IEnumerable<PROSTreatmentViewModel>) treatmentViewModelList;
    }

    public PROSTreatmentViewModel Treatment(long allotId, int patientId)
    {
      PROSTreatmentViewModel treatmentViewModel = new PROSTreatmentViewModel();
      treatmentViewModel.ProsList = (IEnumerable<PROSTreatmentViewModel>) this.PROSTreatmentList(allotId).ToList<PROSTreatmentViewModel>();
      treatmentViewModel.CDRegPatientList = (IEnumerable<PROSTreatmentViewModel>) this.PROSCDRegPatientList(patientId).ToList<PROSTreatmentViewModel>();
      treatmentViewModel.RPDRegPatientList = (IEnumerable<PROSTreatmentViewModel>) this.PROSRPDRegPatientList(patientId).ToList<PROSTreatmentViewModel>();
      treatmentViewModel.FPDRegPatientList = (IEnumerable<PROSTreatmentViewModel>) this.PROSFPDRegPatientList(patientId).ToList<PROSTreatmentViewModel>();
      treatmentViewModel.MFPRegPatientList = (IEnumerable<PROSTreatmentViewModel>) this.PROSMFPRegPatientList(patientId).ToList<PROSTreatmentViewModel>();
      treatmentViewModel.DIMRegPatientList = (IEnumerable<PROSTreatmentViewModel>) this.PROSDIMRegPatientList(patientId).ToList<PROSTreatmentViewModel>();
      treatmentViewModel.PatientId = patientId;
      PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
      string whereClause = "DeptId=" + (object) 7;
      informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
      informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      allotmentViewModel.AllotId = allotId;
      allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
      allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
      treatmentViewModel.patientInformationViewModel = informationViewModel;
      treatmentViewModel.studentAllotmentViewModel = allotmentViewModel;
      return treatmentViewModel;
    }

    public IEnumerable<PROSTreatmentViewModel> PROSCDRegPatientList(int patientId)
    {
      List<PROSTreatmentViewModel> treatmentViewModelList = new List<PROSTreatmentViewModel>();
      foreach (PROSTreatmentViewModel treatmentViewModel in this._uow.Repository<PROSTreatmentViewModel>().GetEntitiesBySql(string.Format(Queries.PROSCDRegPatient, (object) patientId)))
        treatmentViewModelList.Add(new PROSTreatmentViewModel()
        {
          ProsthoCDId = treatmentViewModel.ProsthoCDId,
          ProsthoCDNo = treatmentViewModel.ProsthoCDNo,
          ProsthoCDdateDisplay = treatmentViewModel.ProsthoCDdateDisplay,
          prosCDchiefcomplaint = treatmentViewModel.prosCDchiefcomplaint
        });
      return (IEnumerable<PROSTreatmentViewModel>) treatmentViewModelList;
    }

    public IEnumerable<PROSCDCasesheetProperties> GetCDCProperties()
    {
      return this._uow.Repository<PROSCDCasesheetProperties>().GetAll();
    }

    public PROSCDViewModel GetPROSCDPatientDetails(int id)
    {
      return this._uow.Repository<PROSCDViewModel>().GetEntitiesBySql(string.Format("exec GetPROSCDCasesheet {0}", (object) id)).FirstOrDefault<PROSCDViewModel>();
    }

    public PROSCDViewModel BindPROSCDPatientModel(int treatId, long allotId)
    {
      PROSCDViewModel proscdViewModel = new PROSCDViewModel();
      if (!string.IsNullOrEmpty(allotId.ToString()))
      {
        IEnumerable<MASCode> allCodes = this._Dropdownservice.GetAllCodes();
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + (object) 7;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 7);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        proscdViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        proscdViewModel.patientInformationViewModel = informationViewModel;
        proscdViewModel.studentAllotmentViewModel = allotmentViewModel;
        proscdViewModel.PatientId = informationViewModel.PatientId;
        proscdViewModel.AllotId = informationViewModel.AllotId;
        proscdViewModel.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        proscdViewModel.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 7, 1);
        proscdViewModel.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 7);
        proscdViewModel.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(7);
        proscdViewModel.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 7, 1);
        proscdViewModel.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 7);
        proscdViewModel.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        proscdViewModel.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 7).ToList<ReferralStatusViewModel>();
        proscdViewModel.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 7, 1);
        proscdViewModel.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        proscdViewModel.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        proscdViewModel.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        proscdViewModel.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 7,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        proscdViewModel.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 7, 0);
        proscdViewModel.CauseOfLossOfTeethlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 59)).ToList<MASCode>();
        proscdViewModel.BuiltList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 60)).ToList<MASCode>();
        proscdViewModel.NutritionalStatusList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 61)).ToList<MASCode>();
        proscdViewModel.PsycologicalAttList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 62)).ToList<MASCode>();
        proscdViewModel.ProsHabitsList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 63)).ToList<MASCode>();
        proscdViewModel.patientExpectList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 110)).ToList<MASCode>();
        proscdViewModel.MotiForDentureList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 111)).ToList<MASCode>();
        proscdViewModel.FacialSymmetryList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 82)).ToList<MASCode>();
        proscdViewModel.TemporomanJointList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 112)).ToList<MASCode>();
        proscdViewModel.SkinComplexionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 113)).ToList<MASCode>();
        proscdViewModel.FacialFormList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 114)).ToList<MASCode>();
        proscdViewModel.FacialProfileList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 81)).ToList<MASCode>();
        proscdViewModel.MuscleToneIdList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 115)).ToList<MASCode>();
        proscdViewModel.NeuromControlList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 116)).ToList<MASCode>();
        proscdViewModel.LengthList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 119)).ToList<MASCode>();
        proscdViewModel.TonicityList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 120)).ToList<MASCode>();
        proscdViewModel.lipCompetList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 121)).ToList<MASCode>();
        proscdViewModel.LipThicknessList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 122)).ToList<MASCode>();
        proscdViewModel.LipContactList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 123)).ToList<MASCode>();
        proscdViewModel.LipSupportList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 124)).ToList<MASCode>();
        proscdViewModel.NasolabialFoldList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 125)).ToList<MASCode>();
        proscdViewModel.PhiltrumList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 161)).ToList<MASCode>();
        proscdViewModel.SpeechList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 126)).ToList<MASCode>();
        proscdViewModel.BuccalList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == (int) sbyte.MaxValue)).ToList<MASCode>();
        proscdViewModel.FloorOfMouthList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 128)).ToList<MASCode>();
        proscdViewModel.PalateHardList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 129)).ToList<MASCode>();
        proscdViewModel.SoftList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 130)).ToList<MASCode>();
        proscdViewModel.PalatalVaultList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 131)).ToList<MASCode>();
        proscdViewModel.PalatineTorusList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 132)).ToList<MASCode>();
        proscdViewModel.PalatineTorusPresentList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 133)).ToList<MASCode>();
        proscdViewModel.SoftPalateList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 134)).ToList<MASCode>();
        proscdViewModel.WidthOfPosterList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 135)).ToList<MASCode>();
        proscdViewModel.PalatalSensitivityList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 136)).ToList<MASCode>();
        proscdViewModel.TongueSizeList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 137)).ToList<MASCode>();
        proscdViewModel.TonguePositionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 162)).ToList<MASCode>();
        proscdViewModel.GenialTuberclesList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 138)).ToList<MASCode>();
        proscdViewModel.LateralThroatIdList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 139)).ToList<MASCode>();
        proscdViewModel.SalivaQualityList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 140)).ToList<MASCode>();
        proscdViewModel.SalivaQuantityList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 141)).ToList<MASCode>();
        proscdViewModel.MaxArchFormList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 142)).ToList<MASCode>();
        proscdViewModel.MaxHeightAntList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 143)).ToList<MASCode>();
        proscdViewModel.MaxHeightPostList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 144)).ToList<MASCode>();
        proscdViewModel.MaxWidthAnteriorList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 145)).ToList<MASCode>();
        proscdViewModel.MaxWidthPosteriorList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 146)).ToList<MASCode>();
        proscdViewModel.MaxUndercutsList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 147)).ToList<MASCode>();
        proscdViewModel.MaxBonyProminList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 148)).ToList<MASCode>();
        proscdViewModel.MaxHypermobileList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 149)).ToList<MASCode>();
        proscdViewModel.MaxMucosaList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 150)).ToList<MASCode>();
        proscdViewModel.ManArchFormList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 151)).ToList<MASCode>();
        proscdViewModel.ManHeightAntList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 152)).ToList<MASCode>();
        proscdViewModel.ManHeightPostList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 153)).ToList<MASCode>();
        proscdViewModel.ManWidthAnteriorList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 154)).ToList<MASCode>();
        proscdViewModel.ManWidthPosteriorList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 155)).ToList<MASCode>();
        proscdViewModel.ManUndercutsList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 156)).ToList<MASCode>();
        proscdViewModel.ManBonyProminList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 157)).ToList<MASCode>();
        proscdViewModel.ManHypermobileList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 158)).ToList<MASCode>();
        proscdViewModel.ManMucosaList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 159)).ToList<MASCode>();
        proscdViewModel.ManMylohyoidRidList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 160)).ToList<MASCode>();
        proscdViewModel.HygieneList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 218)).ToList<MASCode>();
        proscdViewModel.TorusUniBilateralList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 219)).ToList<MASCode>();
        proscdViewModel.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) proscdViewModel.PatientId))
        };
        proscdViewModel.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) proscdViewModel.PatientId))
        };
        IEnumerable<PROSCDCasesheetProperties> cdcProperties = this.GetCDCProperties();
        proscdViewModel.Proplist = cdcProperties;
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        proscdViewModel.medicalalertviewmodel = medicalAlertViewModel2;
      }
      return proscdViewModel;
    }

    public int PROSCDSavePatient(PROSCDViewModel model)
    {
      model.MandatoryDummy = "Y";
      PROSCDCasesheet proscdCasesheet = new PROSCDCasesheet();
      PROSCDCasesheet entity1 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PROSCDViewModel, PROSCDCasesheet>())).CreateMapper().Map<PROSCDViewModel, PROSCDCasesheet>(model);
      entity1.ProsthoCDId = 0;
      entity1.ProsthoCDDate = new DateTime?(DateTime.Now);
      entity1.CreatedDate = new DateTime?(DateTime.Now);
      entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      entity1.ProsthoCDNo = this._CasesheetNoService.GetProsthoCDNo();
      int num = this._uow.Repository<PROSCDCasesheet>().Add(entity1, false);
      IEnumerable<PROSCDCasesheetProperties> all = this._uow.Repository<PROSCDCasesheetProperties>().GetAll();
      foreach (PropertyInfo property in model.GetType().GetProperties())
      {
        PropertyInfo prop = property;
        if (all.FirstOrDefault<PROSCDCasesheetProperties>((Func<PROSCDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) model, (object[]) null);
          if (obj != null)
          {
            PROSCDCasesheetPropertyValues entity2 = new PROSCDCasesheetPropertyValues();
            entity2.ProsthoCDId = num;
            PROSCDCasesheetProperties casesheetProperties = all.FirstOrDefault<PROSCDCasesheetProperties>((Func<PROSCDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
            {
              entity2.PropId = new int?(casesheetProperties.PropertyId);
              entity2.PropValues = obj.ToString();
              this._uow.Repository<PROSCDCasesheetPropertyValues>().Add(entity2, false);
            }
          }
        }
      }
      model.ProsthoCDId = num;
      if (model.CreatedepartmentReferredStatus != null)
      {
        ReferralStatus entity2 = new ReferralStatus();
        foreach (ReferralStatusViewModel createdepartmentReferredStatu in model.CreatedepartmentReferredStatus)
        {
          if (createdepartmentReferredStatu.chkToDeptId)
          {
            entity2.PatientId = model.PatientId;
            entity2.FromdeptId = 7;
            entity2.ReferredReason = createdepartmentReferredStatu.ReferredReason;
            entity2.ToDeptId = createdepartmentReferredStatu.ToDeptId;
            entity2.Priority = createdepartmentReferredStatu.Priority;
            entity2.RoomNo = createdepartmentReferredStatu.RoomNo;
            entity2.TreatmentStatus = createdepartmentReferredStatu.TreatmentStatus;
            ReferralStatus referralStatus1 = entity2;
            DateTime now = DateTime.Now;
            DateTime? nullable1 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            referralStatus1.FromDate = nullable1;
            ReferralStatus referralStatus2 = entity2;
            now = DateTime.Now;
            DateTime? nullable2 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            referralStatus2.CreatedDate = nullable2;
            entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            this._Referralservice.Add(entity2);
          }
        }
      }
      if (model.BillingQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(model.BillingQueueDetails, new BillQueueDetails()
        {
          CaserecordId = num,
          ReferredTreatmentId = 1,
          DeptId = 7,
          PatientId = model.PatientId
        });
      if (model.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(model.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = num,
          ReferredTreatmentId = 1,
          DeptId = 7,
          PatientId = model.PatientId
        });
      if (model.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(model.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = model.PatientId,
          AllotId = model.studentAllotmentViewModel.AllotId,
          DoctorId = model.studentAllotmentViewModel.DoctorId,
          StudentId = model.studentAllotmentViewModel.StudentId,
          DeptId = 7,
          ReferredTreatmentId = 1
        });
      return num;
    }

    public int PROSCDUpdatePatient(PROSCDViewModel ViewModel)
    {
      ViewModel.ProsthoCDId = ViewModel.ProsthoCDId;
      PROSCDCasesheet proscdCasesheet = new PROSCDCasesheet();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PROSCDViewModel, PROSCDCasesheet>()));
      PROSCDCasesheet entity1 = Mapper.Map<PROSCDViewModel, PROSCDCasesheet>(ViewModel);
      entity1.ModifiedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
      entity1.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      this._uow.Repository<PROSCDCasesheet>().Update(entity1, false);
      IEnumerable<PROSCDCasesheetProperties> all1 = this._uow.Repository<PROSCDCasesheetProperties>().GetAll();
      IEnumerable<PROSCDCasesheetPropertyValues> all2 = this._uow.Repository<PROSCDCasesheetPropertyValues>().GetAll("ProsthoCDId=" + (object) ViewModel.ProsthoCDId);
      foreach (PropertyInfo property1 in ViewModel.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<PROSCDCasesheetProperties>((Func<PROSCDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) ViewModel, (object[]) null);
          if (obj != null)
          {
            PROSCDCasesheetPropertyValues entity2 = new PROSCDCasesheetPropertyValues();
            entity2.ProsthoCDId = ViewModel.ProsthoCDId;
            PROSCDCasesheetProperties property = all1.FirstOrDefault<PROSCDCasesheetProperties>((Func<PROSCDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(property.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(property.PropertyDataType == "int") || (int) obj != 0))
            {
              PROSCDCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<PROSCDCasesheetPropertyValues>((Func<PROSCDCasesheetPropertyValues, bool>) (a =>
              {
                int? propId = a.PropId;
                int propertyId = property.PropertyId;
                if ((propId.GetValueOrDefault() == propertyId ? (propId.HasValue ? 1 : 0) : 0) != 0)
                  return a.ProsthoCDId == ViewModel.ProsthoCDId;
                return false;
              }));
              if (casesheetPropertyValues != null)
              {
                entity2.PropId = new int?(property.PropertyId);
                entity2.PropValues = obj.ToString();
                entity2.ValueId = casesheetPropertyValues.ValueId;
                this._uow.Repository<PROSCDCasesheetPropertyValues>().Update(entity2, false);
              }
              else
              {
                entity2.PropId = new int?(property.PropertyId);
                entity2.PropValues = obj.ToString();
                this._uow.Repository<PROSCDCasesheetPropertyValues>().Add(entity2, false);
              }
            }
          }
        }
      }
      if (ViewModel.CreatedepartmentReferredStatus != null)
      {
        ReferralStatus entity2 = new ReferralStatus();
        foreach (ReferralStatusViewModel createdepartmentReferredStatu in ViewModel.CreatedepartmentReferredStatus)
        {
          if (createdepartmentReferredStatu.chkToDeptId)
          {
            entity2.PatientId = ViewModel.PatientId;
            entity2.FromdeptId = 7;
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
      if (ViewModel.BillingQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.ProsthoCDId,
          DeptId = 7,
          PatientId = ViewModel.PatientId,
          ReferredTreatmentId = 1
        });
      if (ViewModel.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.ProsthoCDId,
          ReferredTreatmentId = 1,
          DeptId = 7,
          PatientId = ViewModel.PatientId
        });
      if (ViewModel.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(ViewModel.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = ViewModel.PatientId,
          AllotId = ViewModel.studentAllotmentViewModel.AllotId,
          DoctorId = ViewModel.studentAllotmentViewModel.DoctorId,
          StudentId = ViewModel.studentAllotmentViewModel.StudentId,
          DeptId = 7,
          ReferredTreatmentId = 1
        });
      return entity1.ProsthoCDId;
    }

    public PROSCDViewModel PROSCDBindEditPatientModel(int treatId, long allotId, int PROSCDId)
    {
      try
      {
        IEnumerable<MASCode> allCodes = this._Dropdownservice.GetAllCodes();
        PROSCDViewModel proscdPatientDetails = this.GetPROSCDPatientDetails(PROSCDId);
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + (object) 7;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 7);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        allotmentViewModel.DoctorId = informationViewModel.DoctorId;
        ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
        {
          PatientId = informationViewModel.PatientId,
          FromdeptId = 7
        };
        proscdPatientDetails.patientInformationViewModel = informationViewModel;
        proscdPatientDetails.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 7).ToList<ReferralStatusViewModel>();
        proscdPatientDetails.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        proscdPatientDetails.ApprovedepartmentReferral = this._Referralservice.ReferralApprovalList(informationViewModel.PatientId, 7).ToList<ReferralStatusViewModel>();
        proscdPatientDetails.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 7, 1);
        proscdPatientDetails.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        proscdPatientDetails.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 7);
        proscdPatientDetails.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 7, 1);
        proscdPatientDetails.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 7);
        proscdPatientDetails.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(7);
        proscdPatientDetails.studentAllotmentViewModel = allotmentViewModel;
        proscdPatientDetails.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 7, 1);
        proscdPatientDetails.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        proscdPatientDetails.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        proscdPatientDetails.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        proscdPatientDetails.TreatmentReferredId = informationViewModel.ReferredId;
        proscdPatientDetails.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 7,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        proscdPatientDetails.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 7, 1);
        proscdPatientDetails.CauseOfLossOfTeethlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 59)).ToList<MASCode>();
        proscdPatientDetails.BuiltList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 60)).ToList<MASCode>();
        proscdPatientDetails.NutritionalStatusList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 61)).ToList<MASCode>();
        proscdPatientDetails.PsycologicalAttList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 62)).ToList<MASCode>();
        proscdPatientDetails.ProsHabitsList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 63)).ToList<MASCode>();
        proscdPatientDetails.patientExpectList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 110)).ToList<MASCode>();
        proscdPatientDetails.MotiForDentureList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 111)).ToList<MASCode>();
        proscdPatientDetails.FacialSymmetryList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 82)).ToList<MASCode>();
        proscdPatientDetails.TemporomanJointList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 112)).ToList<MASCode>();
        proscdPatientDetails.SkinComplexionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 113)).ToList<MASCode>();
        proscdPatientDetails.FacialFormList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 114)).ToList<MASCode>();
        proscdPatientDetails.FacialProfileList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 81)).ToList<MASCode>();
        proscdPatientDetails.MuscleToneIdList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 115)).ToList<MASCode>();
        proscdPatientDetails.NeuromControlList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 116)).ToList<MASCode>();
        proscdPatientDetails.LengthList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 119)).ToList<MASCode>();
        proscdPatientDetails.TonicityList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 120)).ToList<MASCode>();
        proscdPatientDetails.lipCompetList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 121)).ToList<MASCode>();
        proscdPatientDetails.LipThicknessList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 122)).ToList<MASCode>();
        proscdPatientDetails.LipContactList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 123)).ToList<MASCode>();
        proscdPatientDetails.LipSupportList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 124)).ToList<MASCode>();
        proscdPatientDetails.NasolabialFoldList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 125)).ToList<MASCode>();
        proscdPatientDetails.PhiltrumList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 161)).ToList<MASCode>();
        proscdPatientDetails.SpeechList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 126)).ToList<MASCode>();
        proscdPatientDetails.BuccalList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == (int) sbyte.MaxValue)).ToList<MASCode>();
        proscdPatientDetails.FloorOfMouthList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 128)).ToList<MASCode>();
        proscdPatientDetails.PalateHardList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 129)).ToList<MASCode>();
        proscdPatientDetails.SoftList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 130)).ToList<MASCode>();
        proscdPatientDetails.PalatalVaultList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 131)).ToList<MASCode>();
        proscdPatientDetails.PalatineTorusList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 132)).ToList<MASCode>();
        proscdPatientDetails.PalatineTorusPresentList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 133)).ToList<MASCode>();
        proscdPatientDetails.SoftPalateList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 134)).ToList<MASCode>();
        proscdPatientDetails.WidthOfPosterList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 135)).ToList<MASCode>();
        proscdPatientDetails.PalatalSensitivityList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 136)).ToList<MASCode>();
        proscdPatientDetails.TongueSizeList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 137)).ToList<MASCode>();
        proscdPatientDetails.TonguePositionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 162)).ToList<MASCode>();
        proscdPatientDetails.GenialTuberclesList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 138)).ToList<MASCode>();
        proscdPatientDetails.LateralThroatIdList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 139)).ToList<MASCode>();
        proscdPatientDetails.SalivaQualityList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 140)).ToList<MASCode>();
        proscdPatientDetails.SalivaQuantityList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 141)).ToList<MASCode>();
        proscdPatientDetails.MaxArchFormList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 142)).ToList<MASCode>();
        proscdPatientDetails.MaxHeightAntList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 143)).ToList<MASCode>();
        proscdPatientDetails.MaxHeightPostList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 144)).ToList<MASCode>();
        proscdPatientDetails.MaxWidthAnteriorList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 145)).ToList<MASCode>();
        proscdPatientDetails.MaxWidthPosteriorList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 146)).ToList<MASCode>();
        proscdPatientDetails.MaxUndercutsList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 147)).ToList<MASCode>();
        proscdPatientDetails.MaxBonyProminList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 148)).ToList<MASCode>();
        proscdPatientDetails.MaxHypermobileList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 149)).ToList<MASCode>();
        proscdPatientDetails.MaxMucosaList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 150)).ToList<MASCode>();
        proscdPatientDetails.ManArchFormList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 151)).ToList<MASCode>();
        proscdPatientDetails.ManHeightAntList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 152)).ToList<MASCode>();
        proscdPatientDetails.ManHeightPostList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 153)).ToList<MASCode>();
        proscdPatientDetails.ManWidthAnteriorList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 154)).ToList<MASCode>();
        proscdPatientDetails.ManWidthPosteriorList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 155)).ToList<MASCode>();
        proscdPatientDetails.ManUndercutsList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 156)).ToList<MASCode>();
        proscdPatientDetails.ManBonyProminList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 157)).ToList<MASCode>();
        proscdPatientDetails.ManHypermobileList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 158)).ToList<MASCode>();
        proscdPatientDetails.ManMucosaList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 159)).ToList<MASCode>();
        proscdPatientDetails.ManMylohyoidRidList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 160)).ToList<MASCode>();
        proscdPatientDetails.HygieneList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 218)).ToList<MASCode>();
        proscdPatientDetails.TorusUniBilateralList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 219)).ToList<MASCode>();
        IEnumerable<PROSCDCasesheetProperties> cdcProperties = this.GetCDCProperties();
        proscdPatientDetails.Proplist = cdcProperties;
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        proscdPatientDetails.medicalalertviewmodel = medicalAlertViewModel2;
        proscdPatientDetails.approvalViewModal = new ApprovalViewModal()
        {
          ApprovalTypeId = 1,
          CaserecordId = PROSCDId,
          DeptId = 7,
          DoctorId = allotmentViewModel.DoctorId,
          PatientId = (long) proscdPatientDetails.PatientId,
          ReferredTreatmentId = 1
        };
        proscdPatientDetails.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) proscdPatientDetails.PatientId))
        };
        proscdPatientDetails.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) proscdPatientDetails.PatientId))
        };
        if (proscdPatientDetails.Approval1)
        {
          proscdPatientDetails.DisplayApproval1 = "Approved";
          proscdPatientDetails.ReadOnlyApproval1 = true;
        }
        else
        {
          proscdPatientDetails.DisplayApproval1 = "Not Approved";
          proscdPatientDetails.ReadOnlyApproval1 = false;
        }
        if (proscdPatientDetails.Approval2)
        {
          proscdPatientDetails.DisplayApproval2 = "Approved";
          proscdPatientDetails.ReadOnlyApproval2 = true;
          proscdPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = true;
        }
        else
        {
          proscdPatientDetails.DisplayApproval2 = "Not Approved";
          proscdPatientDetails.ReadOnlyApproval2 = false;
          proscdPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = false;
        }
        if (proscdPatientDetails.Approval3)
        {
          proscdPatientDetails.DisplayApproval3 = "Casesheet Closed";
          proscdPatientDetails.ReadOnlyApproval3 = true;
          proscdPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = true;
          proscdPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = true;
        }
        else
        {
          proscdPatientDetails.DisplayApproval3 = "Not Approved";
          proscdPatientDetails.ReadOnlyApproval3 = false;
          proscdPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = false;
          proscdPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = false;
        }
        PROSCDViewModel proscdViewModel = this._uow.Repository<PROSCDViewModel>().GetEntitiesBySql(string.Format(Queries.DisplayProsCdApprovalStage, (object) 7, (object) proscdPatientDetails.ProsthoCDId)).FirstOrDefault<PROSCDViewModel>();
        proscdPatientDetails.ApprovalStage = proscdViewModel == null ? "Not Initiated" : (!(proscdViewModel.ApprovalStage == "") ? proscdViewModel.ApprovalStage : "Not Initiated");
        proscdPatientDetails.ReadOnlyApproval4 = true;
        return proscdPatientDetails;
      }
      catch
      {
        throw;
      }
    }

    public void PROSCDSavefollowUp(PROSCDViewModel model)
    {
      this._FollowUpService.Add(new FollowUp()
      {
        PatientId = model.PatientId,
        FollowupDate = new DateTime?(model.followupViewModal.FollowupDate),
        FollowupTime = new DateTime?(model.followupViewModal.FollowupTime),
        DeptId = 7,
        FollowupReason = model.followupViewModal.FollowupReason,
        CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))),
        CreatedSystem = this._Dropdownservice.GetIPAddress(false),
        ReferredTreatmentId = 1
      });
    }

    public void PROSCDUpdateAllotment(PROSCDViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        ProcedureNotes = model.studentAllotmentViewModel.ProcedureNotes,
        ProcedureNotesDate = new DateTime?(DateTime.Now),
        DoctorId = model.studentAllotmentViewModel.DoctorId,
        AllotId = model.studentAllotmentViewModel.AllotId,
        CaserecordId = model.ProsthoCDId,
        ReferredTreatmentId = 1
      });
    }

    public void PROSCDUpdateReferralStatus(PROSCDViewModel model)
    {
      int patientId = model.PatientId;
      int num = 7;
      this._Referralservice.UpdateReferralStatus(new ReferralStatus()
      {
        VisitType = "dbo.GetVisitType(" + (object) patientId + "," + (object) num + "," + (object) model.TreatmentReferredId + ")",
        ReferredId = model.TreatmentReferredId,
        ReferredTreatmentId = 1
      });
    }

    public PROSCDViewModel BindPROSCDPatientReport(int PROSCDId)
    {
      PROSCDViewModel proscdViewModel1 = new PROSCDViewModel();
      PROSCDViewModel proscdViewModel2 = this._uow.Repository<PROSCDViewModel>().GetEntitiesBySql(string.Format("exec GetPROSCDCasesheetReport {0}", (object) PROSCDId)).FirstOrDefault<PROSCDViewModel>();
      proscdViewModel2.patientInformationViewModel = new PatientInformationViewModel()
      {
        PatientName = proscdViewModel2.PatientName,
        AgeGender = proscdViewModel2.Age.ToString() + "/" + (object) (Gender) proscdViewModel2.GenderId,
        Phone = proscdViewModel2.Phone,
        OpNo = proscdViewModel2.OpNo,
        Area = proscdViewModel2.Area
      };
      proscdViewModel2.FileUploadlist = this._uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) proscdViewModel2.PatientId, (object) 7));
      proscdViewModel2.paidInvestigationList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.PaidBillsforReport, (object) proscdViewModel2.PatientId, (object) 7, (object) PROSCDId, (object) 1, (object) 20, (object) 16));
      proscdViewModel2.deptTreatmentList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.DeptTreatmentforReport, (object) proscdViewModel2.PatientId, (object) 7, (object) PROSCDId, (object) 1));
      ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
      {
        PatientId = proscdViewModel2.PatientId,
        FromdeptId = 7
      };
      proscdViewModel2.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(proscdViewModel2.PatientId).ToList<ReferralStatusViewModel>();
      proscdViewModel2.followupViewModal = new FollowupViewModal()
      {
        PatientId = proscdViewModel2.PatientId,
        DeptId = 7,
        FollowupDate = DateTime.Now,
        FollowupTime = DateTime.Now
      };
      proscdViewModel2.followupList = this._FollowUpService.LoadFollowupList(proscdViewModel2.PatientId, 7, 1);
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      proscdViewModel2.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(proscdViewModel2.PatientId, 7, 1);
      return proscdViewModel2;
    }

    public IEnumerable<PROSTreatmentViewModel> PROSRPDRegPatientList(int patientId)
    {
      List<PROSTreatmentViewModel> treatmentViewModelList = new List<PROSTreatmentViewModel>();
      foreach (PROSTreatmentViewModel treatmentViewModel in this._uow.Repository<PROSTreatmentViewModel>().GetEntitiesBySql(string.Format(Queries.PROSRPDRegPatient, (object) patientId)))
        treatmentViewModelList.Add(new PROSTreatmentViewModel()
        {
          ProsthoRPDId = treatmentViewModel.ProsthoRPDId,
          ProsthoRPDNo = treatmentViewModel.ProsthoRPDNo,
          prosthoRPDdateDisplay = treatmentViewModel.prosthoRPDdateDisplay,
          prosRPDchiefcomplaint = treatmentViewModel.prosRPDchiefcomplaint
        });
      return (IEnumerable<PROSTreatmentViewModel>) treatmentViewModelList;
    }

    public IEnumerable<PROSRPDCasesheetProperties> GetRPDProperties()
    {
      return this._uow.Repository<PROSRPDCasesheetProperties>().GetAll();
    }

    public PROSRPDViewModel GetPROSRPDPatientDetails(int id)
    {
      return this._uow.Repository<PROSRPDViewModel>().GetEntitiesBySql(string.Format("exec GetPROSRPDCasesheet {0}", (object) id)).FirstOrDefault<PROSRPDViewModel>();
    }

    public PROSRPDViewModel BindPROSRPDPatientModel(int treatId, long allotId)
    {
      PROSRPDViewModel prosrpdViewModel = new PROSRPDViewModel();
      if (!string.IsNullOrEmpty(allotId.ToString()))
      {
        IEnumerable<MASCode> allCodes = this._Dropdownservice.GetAllCodes();
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + (object) 7;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 7);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        prosrpdViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        prosrpdViewModel.patientInformationViewModel = informationViewModel;
        prosrpdViewModel.studentAllotmentViewModel = allotmentViewModel;
        prosrpdViewModel.PatientId = informationViewModel.PatientId;
        prosrpdViewModel.AllotId = informationViewModel.AllotId;
        prosrpdViewModel.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        prosrpdViewModel.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 7, 2);
        prosrpdViewModel.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 7);
        prosrpdViewModel.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(7);
        prosrpdViewModel.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 7, 2);
        prosrpdViewModel.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 7);
        prosrpdViewModel.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        prosrpdViewModel.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 7).ToList<ReferralStatusViewModel>();
        prosrpdViewModel.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 7, 2);
        prosrpdViewModel.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        prosrpdViewModel.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        prosrpdViewModel.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        prosrpdViewModel.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 7,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        prosrpdViewModel.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 7, 0);
        prosrpdViewModel.MedicalHistoryList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 164)).ToList<MASCode>();
        prosrpdViewModel.VerticalFaceHeightList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 165)).ToList<MASCode>();
        prosrpdViewModel.GeneralAlignmentList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 166)).ToList<MASCode>();
        prosrpdViewModel.TypeOfOcclusionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 167)).ToList<MASCode>();
        prosrpdViewModel.SlideInCentricList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 168)).ToList<MASCode>();
        prosrpdViewModel.ColorOfMucosaList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 169)).ToList<MASCode>();
        prosrpdViewModel.AnyPathologicChangesList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 180)).ToList<MASCode>();
        prosrpdViewModel.TissueReaToWearList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 181)).ToList<MASCode>();
        prosrpdViewModel.UpperList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 170)).ToList<MASCode>();
        prosrpdViewModel.LowerList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 171)).ToList<MASCode>();
        prosrpdViewModel.AnyTorusPalatinusList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 172)).ToList<MASCode>();
        prosrpdViewModel.RidgeDistanceList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 173)).ToList<MASCode>();
        prosrpdViewModel.EvalSalivaQualityList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 182)).ToList<MASCode>();
        prosrpdViewModel.EvalSalivaQuantityList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 183)).ToList<MASCode>();
        prosrpdViewModel.SpaceForMandibularList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 174)).ToList<MASCode>();
        prosrpdViewModel.CrownRootRatioList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 175)).ToList<MASCode>();
        prosrpdViewModel.RootMorphologyId1List = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 176)).ToList<MASCode>();
        prosrpdViewModel.RootMorphologyId2List = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 177)).ToList<MASCode>();
        prosrpdViewModel.OcclusalPlanList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 178)).ToList<MASCode>();
        prosrpdViewModel.InterArchDistanceList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 179)).ToList<MASCode>();
        prosrpdViewModel.MentalAttitudeList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 220)).ToList<MASCode>();
        IEnumerable<PROSRPDCasesheetProperties> rpdProperties = this.GetRPDProperties();
        prosrpdViewModel.Proplist = rpdProperties;
        prosrpdViewModel.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) prosrpdViewModel.PatientId))
        };
        prosrpdViewModel.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) prosrpdViewModel.PatientId))
        };
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        prosrpdViewModel.medicalalertviewmodel = medicalAlertViewModel2;
      }
      return prosrpdViewModel;
    }

    public int PROSRPDSavePatient(PROSRPDViewModel model)
    {
      model.MandatoryDummy = "Y";
      PROSRPDCasesheet prosrpdCasesheet = new PROSRPDCasesheet();
      PROSRPDCasesheet entity1 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PROSRPDViewModel, PROSRPDCasesheet>())).CreateMapper().Map<PROSRPDViewModel, PROSRPDCasesheet>(model);
      entity1.ProsthoRPDId = 0;
      entity1.ProsthoRPDDate = new DateTime?(DateTime.Now);
      entity1.DietId = (int) model.Diet;
      entity1.CreatedDate = new DateTime?(DateTime.Now);
      entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      entity1.ProsthoRPDNo = this._CasesheetNoService.GetProsthoRPDNo();
      int num = this._uow.Repository<PROSRPDCasesheet>().Add(entity1, false);
      IEnumerable<PROSRPDCasesheetProperties> all = this._uow.Repository<PROSRPDCasesheetProperties>().GetAll();
      foreach (PropertyInfo property in model.GetType().GetProperties())
      {
        PropertyInfo prop = property;
        if (all.FirstOrDefault<PROSRPDCasesheetProperties>((Func<PROSRPDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) model, (object[]) null);
          if (obj != null)
          {
            PROSRPDCasesheetPropertyValues entity2 = new PROSRPDCasesheetPropertyValues();
            entity2.ProsthoRPDId = num;
            PROSRPDCasesheetProperties casesheetProperties = all.FirstOrDefault<PROSRPDCasesheetProperties>((Func<PROSRPDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            entity2.PropId = casesheetProperties.PropertyId;
            entity2.PropValues = obj.ToString();
            this._uow.Repository<PROSRPDCasesheetPropertyValues>().Add(entity2, false);
          }
        }
      }
      model.ProsthoRPDId = num;
      if (model.CreatedepartmentReferredStatus != null)
      {
        ReferralStatus entity2 = new ReferralStatus();
        foreach (ReferralStatusViewModel createdepartmentReferredStatu in model.CreatedepartmentReferredStatus)
        {
          if (createdepartmentReferredStatu.chkToDeptId)
          {
            entity2.PatientId = model.PatientId;
            entity2.FromdeptId = 7;
            entity2.ReferredReason = createdepartmentReferredStatu.ReferredReason;
            entity2.ToDeptId = createdepartmentReferredStatu.ToDeptId;
            entity2.Priority = createdepartmentReferredStatu.Priority;
            entity2.RoomNo = createdepartmentReferredStatu.RoomNo;
            entity2.TreatmentStatus = createdepartmentReferredStatu.TreatmentStatus;
            ReferralStatus referralStatus1 = entity2;
            DateTime now = DateTime.Now;
            DateTime? nullable1 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            referralStatus1.FromDate = nullable1;
            ReferralStatus referralStatus2 = entity2;
            now = DateTime.Now;
            DateTime? nullable2 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            referralStatus2.CreatedDate = nullable2;
            entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            this._Referralservice.Add(entity2);
          }
        }
      }
      if (model.BillingQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(model.BillingQueueDetails, new BillQueueDetails()
        {
          CaserecordId = num,
          ReferredTreatmentId = 2,
          DeptId = 7,
          PatientId = model.PatientId
        });
      if (model.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(model.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = num,
          ReferredTreatmentId = 2,
          DeptId = 7,
          PatientId = model.PatientId
        });
      if (model.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(model.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = model.PatientId,
          AllotId = model.studentAllotmentViewModel.AllotId,
          DoctorId = model.studentAllotmentViewModel.DoctorId,
          StudentId = model.studentAllotmentViewModel.StudentId,
          DeptId = 7,
          ReferredTreatmentId = 2
        });
      return num;
    }

    public int PROSRPDUpdatePatient(PROSRPDViewModel ViewModel)
    {
      ViewModel.ProsthoRPDId = ViewModel.ProsthoRPDId;
      ViewModel.DietId = (int) ViewModel.Diet;
      PROSRPDCasesheet prosrpdCasesheet = new PROSRPDCasesheet();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PROSRPDViewModel, PROSRPDCasesheet>()));
      PROSRPDCasesheet entity1 = Mapper.Map<PROSRPDViewModel, PROSRPDCasesheet>(ViewModel);
      entity1.ModifiedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      entity1.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
      this._uow.Repository<PROSRPDCasesheet>().Update(entity1, false);
      IEnumerable<PROSRPDCasesheetProperties> all1 = this._uow.Repository<PROSRPDCasesheetProperties>().GetAll();
      IEnumerable<PROSRPDCasesheetPropertyValues> all2 = this._uow.Repository<PROSRPDCasesheetPropertyValues>().GetAll("ProsthoRPDId=" + (object) ViewModel.ProsthoRPDId);
      foreach (PropertyInfo property1 in ViewModel.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<PROSRPDCasesheetProperties>((Func<PROSRPDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) ViewModel, (object[]) null);
          if (obj != null)
          {
            PROSRPDCasesheetPropertyValues entity2 = new PROSRPDCasesheetPropertyValues();
            entity2.ProsthoRPDId = ViewModel.ProsthoRPDId;
            PROSRPDCasesheetProperties property = all1.FirstOrDefault<PROSRPDCasesheetProperties>((Func<PROSRPDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            PROSRPDCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<PROSRPDCasesheetPropertyValues>((Func<PROSRPDCasesheetPropertyValues, bool>) (a =>
            {
              if (a.PropId == property.PropertyId)
                return a.ProsthoRPDId == ViewModel.ProsthoRPDId;
              return false;
            }));
            if (casesheetPropertyValues != null)
            {
              entity2.PropId = property.PropertyId;
              entity2.PropValues = obj.ToString();
              entity2.ValueId = casesheetPropertyValues.ValueId;
              this._uow.Repository<PROSRPDCasesheetPropertyValues>().Update(entity2, false);
            }
            else
            {
              entity2.PropId = property.PropertyId;
              entity2.PropValues = obj.ToString();
              this._uow.Repository<PROSRPDCasesheetPropertyValues>().Add(entity2, false);
            }
          }
        }
      }
      if (ViewModel.CreatedepartmentReferredStatus != null)
      {
        ReferralStatus entity2 = new ReferralStatus();
        foreach (ReferralStatusViewModel createdepartmentReferredStatu in ViewModel.CreatedepartmentReferredStatus)
        {
          if (createdepartmentReferredStatu.chkToDeptId)
          {
            entity2.PatientId = ViewModel.PatientId;
            entity2.FromdeptId = 7;
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
      if (ViewModel.BillingQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.ProsthoRPDId,
          DeptId = 7,
          PatientId = ViewModel.PatientId,
          ReferredTreatmentId = 2
        });
      if (ViewModel.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(ViewModel.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = ViewModel.PatientId,
          AllotId = ViewModel.studentAllotmentViewModel.AllotId,
          DoctorId = ViewModel.studentAllotmentViewModel.DoctorId,
          StudentId = ViewModel.studentAllotmentViewModel.StudentId,
          DeptId = 7,
          ReferredTreatmentId = 2
        });
      if (ViewModel.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.ProsthoRPDId,
          ReferredTreatmentId = 2,
          DeptId = 7,
          PatientId = ViewModel.PatientId
        });
      return entity1.ProsthoRPDId;
    }

    public PROSRPDViewModel PROSRPDBindEditPatientModel(int treatId, long allotId, int PROSRPDId)
    {
      IEnumerable<MASCode> allCodes = this._Dropdownservice.GetAllCodes();
      PROSRPDViewModel prosrpdPatientDetails = this.GetPROSRPDPatientDetails(PROSRPDId);
      PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
      string whereClause = "DeptId=" + (object) 7;
      informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
      informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
      informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 7);
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      allotmentViewModel.AllotId = allotId;
      allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
      allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
      allotmentViewModel.DoctorId = informationViewModel.DoctorId;
      prosrpdPatientDetails.Diet = (Diet) prosrpdPatientDetails.DietId;
      ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
      {
        PatientId = informationViewModel.PatientId,
        FromdeptId = 7
      };
      prosrpdPatientDetails.patientInformationViewModel = informationViewModel;
      prosrpdPatientDetails.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
      prosrpdPatientDetails.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 7);
      prosrpdPatientDetails.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 7, 2);
      prosrpdPatientDetails.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 7).ToList<ReferralStatusViewModel>();
      prosrpdPatientDetails.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
      prosrpdPatientDetails.ApprovedepartmentReferral = this._Referralservice.ReferralApprovalList(informationViewModel.PatientId, 7).ToList<ReferralStatusViewModel>();
      prosrpdPatientDetails.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 7, 2);
      prosrpdPatientDetails.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 7);
      prosrpdPatientDetails.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(7);
      prosrpdPatientDetails.studentAllotmentViewModel = allotmentViewModel;
      prosrpdPatientDetails.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 7, 2);
      prosrpdPatientDetails.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
      prosrpdPatientDetails.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
      prosrpdPatientDetails.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
      prosrpdPatientDetails.TreatmentReferredId = informationViewModel.ReferredId;
      prosrpdPatientDetails.followupViewModal = new FollowupViewModal()
      {
        PatientId = informationViewModel.PatientId,
        DeptId = 7,
        FollowupDate = DateTime.Now,
        FollowupTime = DateTime.Now
      };
      prosrpdPatientDetails.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 7, 3);
      prosrpdPatientDetails.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
      prosrpdPatientDetails.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
      prosrpdPatientDetails.MedicalHistoryList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 164)).ToList<MASCode>();
      prosrpdPatientDetails.VerticalFaceHeightList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 165)).ToList<MASCode>();
      prosrpdPatientDetails.GeneralAlignmentList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 166)).ToList<MASCode>();
      prosrpdPatientDetails.TypeOfOcclusionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 167)).ToList<MASCode>();
      prosrpdPatientDetails.SlideInCentricList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 168)).ToList<MASCode>();
      prosrpdPatientDetails.ColorOfMucosaList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 169)).ToList<MASCode>();
      prosrpdPatientDetails.AnyPathologicChangesList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 180)).ToList<MASCode>();
      prosrpdPatientDetails.TissueReaToWearList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 181)).ToList<MASCode>();
      prosrpdPatientDetails.UpperList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 170)).ToList<MASCode>();
      prosrpdPatientDetails.LowerList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 171)).ToList<MASCode>();
      prosrpdPatientDetails.AnyTorusPalatinusList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 172)).ToList<MASCode>();
      prosrpdPatientDetails.RidgeDistanceList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 173)).ToList<MASCode>();
      prosrpdPatientDetails.EvalSalivaQualityList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 182)).ToList<MASCode>();
      prosrpdPatientDetails.EvalSalivaQuantityList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 183)).ToList<MASCode>();
      prosrpdPatientDetails.SpaceForMandibularList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 174)).ToList<MASCode>();
      prosrpdPatientDetails.CrownRootRatioList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 175)).ToList<MASCode>();
      prosrpdPatientDetails.RootMorphologyId1List = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 176)).ToList<MASCode>();
      prosrpdPatientDetails.RootMorphologyId2List = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 177)).ToList<MASCode>();
      prosrpdPatientDetails.OcclusalPlanList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 178)).ToList<MASCode>();
      prosrpdPatientDetails.InterArchDistanceList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 179)).ToList<MASCode>();
      prosrpdPatientDetails.MentalAttitudeList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 220)).ToList<MASCode>();
      IEnumerable<PROSRPDCasesheetProperties> rpdProperties = this.GetRPDProperties();
      prosrpdPatientDetails.Proplist = rpdProperties;
      MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
      MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
      prosrpdPatientDetails.medicalalertviewmodel = medicalAlertViewModel2;
      prosrpdPatientDetails.approvalViewModal = new ApprovalViewModal()
      {
        ApprovalTypeId = 1,
        CaserecordId = PROSRPDId,
        DeptId = 7,
        DoctorId = allotmentViewModel.DoctorId,
        PatientId = (long) prosrpdPatientDetails.PatientId,
        ReferredTreatmentId = 2
      };
      prosrpdPatientDetails.radioRegistrationviewmodel = new RadioRegistrationViewModel()
      {
        RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) prosrpdPatientDetails.PatientId))
      };
      prosrpdPatientDetails.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
      {
        LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) prosrpdPatientDetails.PatientId))
      };
      if (prosrpdPatientDetails.Approval1)
      {
        prosrpdPatientDetails.DisplayApproval1 = "Approved";
        prosrpdPatientDetails.ReadOnlyApproval1 = true;
      }
      else
      {
        prosrpdPatientDetails.DisplayApproval1 = "Not Approved";
        prosrpdPatientDetails.ReadOnlyApproval1 = false;
      }
      if (prosrpdPatientDetails.Approval2)
      {
        prosrpdPatientDetails.DisplayApproval2 = "Approved";
        prosrpdPatientDetails.ReadOnlyApproval2 = true;
        prosrpdPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = true;
      }
      else
      {
        prosrpdPatientDetails.DisplayApproval2 = "Not Approved";
        prosrpdPatientDetails.ReadOnlyApproval2 = false;
        prosrpdPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = false;
      }
      if (prosrpdPatientDetails.Approval3)
      {
        prosrpdPatientDetails.DisplayApproval3 = "Casesheet Closed";
        prosrpdPatientDetails.ReadOnlyApproval3 = true;
        prosrpdPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = true;
        prosrpdPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = true;
      }
      else
      {
        prosrpdPatientDetails.DisplayApproval3 = "Not Approved";
        prosrpdPatientDetails.ReadOnlyApproval3 = false;
        prosrpdPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = false;
        prosrpdPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = false;
      }
      PROSRPDViewModel prosrpdViewModel = this._uow.Repository<PROSRPDViewModel>().GetEntitiesBySql(string.Format(Queries.DisplayProsRpdApprovalStage, (object) 7, (object) prosrpdPatientDetails.ProsthoRPDId)).FirstOrDefault<PROSRPDViewModel>();
      prosrpdPatientDetails.ApprovalStage = prosrpdViewModel == null ? "Not Initiated" : (!(prosrpdViewModel.ApprovalStage == "") ? prosrpdViewModel.ApprovalStage : "Not Initiated");
      prosrpdPatientDetails.ReadOnlyApproval4 = true;
      return prosrpdPatientDetails;
    }

    public void PROSRPDSavefollowUp(PROSRPDViewModel model)
    {
      this._FollowUpService.Add(new FollowUp()
      {
        PatientId = model.PatientId,
        FollowupDate = new DateTime?(model.followupViewModal.FollowupDate),
        FollowupTime = new DateTime?(model.followupViewModal.FollowupTime),
        DeptId = 7,
        FollowupReason = model.followupViewModal.FollowupReason,
        CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))),
        CreatedSystem = this._Dropdownservice.GetIPAddress(false),
        ReferredTreatmentId = 2
      });
    }

    public void PROSRPDUpdateAllotment(PROSRPDViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        ProcedureNotes = model.studentAllotmentViewModel.ProcedureNotes,
        ProcedureNotesDate = new DateTime?(DateTime.Now),
        DoctorId = model.studentAllotmentViewModel.DoctorId,
        AllotId = model.studentAllotmentViewModel.AllotId,
        CaserecordId = model.ProsthoRPDId,
        ReferredTreatmentId = 2
      });
    }

    public void PROSRPDUpdateReferralStatus(PROSRPDViewModel model)
    {
      int patientId = model.PatientId;
      int num = 7;
      this._Referralservice.UpdateReferralStatus(new ReferralStatus()
      {
        VisitType = "dbo.GetVisitType(" + (object) patientId + "," + (object) num + "," + (object) model.TreatmentReferredId + ")",
        ReferredId = model.TreatmentReferredId,
        ReferredTreatmentId = 2
      });
    }

    public PROSRPDViewModel BindPROSRPDPatientReport(int PROSRPDId)
    {
      PROSRPDViewModel prosrpdViewModel1 = new PROSRPDViewModel();
      PROSRPDViewModel prosrpdViewModel2 = this._uow.Repository<PROSRPDViewModel>().GetEntitiesBySql(string.Format("exec GetPROSRPDCasesheetReport {0}", (object) PROSRPDId)).FirstOrDefault<PROSRPDViewModel>();
      prosrpdViewModel2.DietName = Convert.ToString((object) (Diet) prosrpdViewModel2.DietId);
      prosrpdViewModel2.patientInformationViewModel = new PatientInformationViewModel()
      {
        PatientName = prosrpdViewModel2.PatientName,
        AgeGender = prosrpdViewModel2.Age.ToString() + "/" + (object) (Gender) prosrpdViewModel2.GenderId,
        Phone = prosrpdViewModel2.Phone,
        OpNo = prosrpdViewModel2.OpNo,
        Area = prosrpdViewModel2.Area
      };
      prosrpdViewModel2.FileUploadlist = this._uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) prosrpdViewModel2.PatientId, (object) 7));
      prosrpdViewModel2.paidInvestigationList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.PaidBillsforReport, (object) prosrpdViewModel2.PatientId, (object) 7, (object) PROSRPDId, (object) 2, (object) 20, (object) 16));
      prosrpdViewModel2.deptTreatmentList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.DeptTreatmentforReport, (object) prosrpdViewModel2.PatientId, (object) 7, (object) PROSRPDId, (object) 2));
      ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
      {
        PatientId = prosrpdViewModel2.PatientId,
        FromdeptId = 7
      };
      prosrpdViewModel2.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(prosrpdViewModel2.PatientId).ToList<ReferralStatusViewModel>();
      prosrpdViewModel2.followupViewModal = new FollowupViewModal()
      {
        PatientId = prosrpdViewModel2.PatientId,
        DeptId = 7,
        FollowupDate = DateTime.Now,
        FollowupTime = DateTime.Now
      };
      prosrpdViewModel2.followupList = this._FollowUpService.LoadFollowupList(prosrpdViewModel2.PatientId, 7, 2);
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      prosrpdViewModel2.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(prosrpdViewModel2.PatientId, 7, 2);
      return prosrpdViewModel2;
    }

    public IEnumerable<PROSTreatmentViewModel> PROSFPDRegPatientList(int patientId)
    {
      List<PROSTreatmentViewModel> treatmentViewModelList = new List<PROSTreatmentViewModel>();
      foreach (PROSTreatmentViewModel treatmentViewModel in this._uow.Repository<PROSTreatmentViewModel>().GetEntitiesBySql(string.Format(Queries.PROSFPDRegPatient, (object) patientId)))
        treatmentViewModelList.Add(new PROSTreatmentViewModel()
        {
          ProsthoFPDId = treatmentViewModel.ProsthoFPDId,
          ProsthoFPDNo = treatmentViewModel.ProsthoFPDNo,
          prosthoFPDdateDisplay = treatmentViewModel.prosthoFPDdateDisplay,
          prosFPDchiefcomplaint = treatmentViewModel.prosFPDchiefcomplaint
        });
      return (IEnumerable<PROSTreatmentViewModel>) treatmentViewModelList;
    }

    public IEnumerable<PROSFPDCasesheetProperties> GetFPDProperties()
    {
      return this._uow.Repository<PROSFPDCasesheetProperties>().GetAll();
    }

    public PROSFPDViewModel GetPROSFPDPatientDetails(int id)
    {
      return this._uow.Repository<PROSFPDViewModel>().GetEntitiesBySql(string.Format("exec GetPROSFPDCasesheet {0}", (object) id)).FirstOrDefault<PROSFPDViewModel>();
    }

    public PROSFPDViewModel BindPROSFPDPatientModel(int treatId, long allotId)
    {
      PROSFPDViewModel prosfpdViewModel = new PROSFPDViewModel();
      if (!string.IsNullOrEmpty(allotId.ToString()))
      {
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + (object) 7;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 7);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        prosfpdViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        prosfpdViewModel.patientInformationViewModel = informationViewModel;
        prosfpdViewModel.studentAllotmentViewModel = allotmentViewModel;
        prosfpdViewModel.PatientId = informationViewModel.PatientId;
        prosfpdViewModel.AllotId = informationViewModel.AllotId;
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        prosfpdViewModel.medicalalertviewmodel = medicalAlertViewModel2;
        prosfpdViewModel.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        prosfpdViewModel.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 7, 3);
        prosfpdViewModel.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 7);
        prosfpdViewModel.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(7);
        prosfpdViewModel.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 7, 3);
        prosfpdViewModel.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 7);
        prosfpdViewModel.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        prosfpdViewModel.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 7).ToList<ReferralStatusViewModel>();
        prosfpdViewModel.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 7, 3);
        prosfpdViewModel.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        prosfpdViewModel.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        prosfpdViewModel.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        prosfpdViewModel.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 7,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        prosfpdViewModel.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 7, 0);
        prosfpdViewModel.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) prosfpdViewModel.PatientId))
        };
        prosfpdViewModel.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) prosfpdViewModel.PatientId))
        };
        prosfpdViewModel.TMJlist = this._Dropdownservice.GetCodesById(83);
        IEnumerable<PROSFPDCasesheetProperties> fpdProperties = this.GetFPDProperties();
        prosfpdViewModel.Proplist = fpdProperties;
      }
      return prosfpdViewModel;
    }

    public int PROSFPDSavePatient(PROSFPDViewModel model)
    {
      model.MandatoryDummy = "Y";
      PROSFPDCasesheet prosfpdCasesheet = new PROSFPDCasesheet();
      PROSFPDCasesheet entity1 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PROSFPDViewModel, PROSFPDCasesheet>())).CreateMapper().Map<PROSFPDViewModel, PROSFPDCasesheet>(model);
      entity1.ProsthoFPDId = 0;
      entity1.ProsthoFPDDate = new DateTime?(DateTime.Now);
      entity1.CreatedDate = new DateTime?(DateTime.Now);
      entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      if (model.CHKTL4)
        model.TL4 = "Y";
      else
        model.TL3 = "N";
      model.TL3 = !model.CHKTL3 ? "N" : "Y";
      model.TL2 = !model.CHKTL2 ? "N" : "Y";
      model.TL1 = !model.CHKTL1 ? "N" : "Y";
      model.TR1 = !model.CHKTR1 ? "N" : "Y";
      model.TR2 = !model.CHKTR2 ? "N" : "Y";
      model.TR3 = !model.CHKTR3 ? "N" : "Y";
      model.TR4 = !model.CHKTR4 ? "N" : "Y";
      entity1.ProsthoFPDNo = this._CasesheetNoService.GetProsthoFPDNo();
      int num = this._uow.Repository<PROSFPDCasesheet>().Add(entity1, false);
      IEnumerable<PROSFPDCasesheetProperties> all = this._uow.Repository<PROSFPDCasesheetProperties>().GetAll();
      foreach (PropertyInfo property in model.GetType().GetProperties())
      {
        PropertyInfo prop = property;
        if (all.FirstOrDefault<PROSFPDCasesheetProperties>((Func<PROSFPDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) model, (object[]) null);
          if (obj != null)
          {
            PROSFPDCasesheetPropertyValues entity2 = new PROSFPDCasesheetPropertyValues();
            entity2.ProsthoFPDId = num;
            PROSFPDCasesheetProperties casesheetProperties = all.FirstOrDefault<PROSFPDCasesheetProperties>((Func<PROSFPDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            entity2.PropId = casesheetProperties.PropertyId;
            entity2.PropValues = obj.ToString();
            this._uow.Repository<PROSFPDCasesheetPropertyValues>().Add(entity2, false);
          }
        }
      }
      model.ProsthoFPDId = num;
      if (model.CreatedepartmentReferredStatus != null)
      {
        ReferralStatus entity2 = new ReferralStatus();
        foreach (ReferralStatusViewModel createdepartmentReferredStatu in model.CreatedepartmentReferredStatus)
        {
          if (createdepartmentReferredStatu.chkToDeptId)
          {
            entity2.PatientId = model.PatientId;
            entity2.FromdeptId = 7;
            entity2.ReferredReason = createdepartmentReferredStatu.ReferredReason;
            entity2.ToDeptId = createdepartmentReferredStatu.ToDeptId;
            entity2.Priority = createdepartmentReferredStatu.Priority;
            entity2.RoomNo = createdepartmentReferredStatu.RoomNo;
            entity2.TreatmentStatus = createdepartmentReferredStatu.TreatmentStatus;
            ReferralStatus referralStatus1 = entity2;
            DateTime now = DateTime.Now;
            DateTime? nullable1 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            referralStatus1.FromDate = nullable1;
            ReferralStatus referralStatus2 = entity2;
            now = DateTime.Now;
            DateTime? nullable2 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            referralStatus2.CreatedDate = nullable2;
            entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            this._Referralservice.Add(entity2);
          }
        }
      }
      if (model.BillingQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(model.BillingQueueDetails, new BillQueueDetails()
        {
          CaserecordId = num,
          ReferredTreatmentId = 3,
          DeptId = 7,
          PatientId = model.PatientId
        });
      if (model.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(model.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = num,
          ReferredTreatmentId = 3,
          DeptId = 7,
          PatientId = model.PatientId
        });
      if (model.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(model.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = model.PatientId,
          AllotId = model.studentAllotmentViewModel.AllotId,
          DoctorId = model.studentAllotmentViewModel.DoctorId,
          StudentId = model.studentAllotmentViewModel.StudentId,
          DeptId = 7,
          ReferredTreatmentId = 3
        });
      return num;
    }

    public int PROSFPDUpdatePatient(PROSFPDViewModel ViewModel)
    {
      ViewModel.ProsthoFPDId = ViewModel.ProsthoFPDId;
      PROSFPDCasesheet prosfpdCasesheet = new PROSFPDCasesheet();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PROSFPDViewModel, PROSFPDCasesheet>()));
      PROSFPDCasesheet entity1 = Mapper.Map<PROSFPDViewModel, PROSFPDCasesheet>(ViewModel);
      entity1.ModifiedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      entity1.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
      if (ViewModel.CHKTL4)
        ViewModel.TL4 = "Y";
      else
        ViewModel.TL3 = "N";
      ViewModel.TL3 = !ViewModel.CHKTL3 ? "N" : "Y";
      ViewModel.TL2 = !ViewModel.CHKTL2 ? "N" : "Y";
      ViewModel.TL1 = !ViewModel.CHKTL1 ? "N" : "Y";
      ViewModel.TR1 = !ViewModel.CHKTR1 ? "N" : "Y";
      ViewModel.TR2 = !ViewModel.CHKTR2 ? "N" : "Y";
      ViewModel.TR3 = !ViewModel.CHKTR3 ? "N" : "Y";
      ViewModel.TR4 = !ViewModel.CHKTR4 ? "N" : "Y";
      this._uow.Repository<PROSFPDCasesheet>().Update(entity1, false);
      IEnumerable<PROSFPDCasesheetProperties> all1 = this._uow.Repository<PROSFPDCasesheetProperties>().GetAll();
      IEnumerable<PROSFPDCasesheetPropertyValues> all2 = this._uow.Repository<PROSFPDCasesheetPropertyValues>().GetAll("ProsthoFPDId=" + (object) ViewModel.ProsthoFPDId);
      foreach (PropertyInfo property1 in ViewModel.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<PROSFPDCasesheetProperties>((Func<PROSFPDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) ViewModel, (object[]) null);
          if (obj != null)
          {
            PROSFPDCasesheetPropertyValues entity2 = new PROSFPDCasesheetPropertyValues();
            entity2.ProsthoFPDId = ViewModel.ProsthoFPDId;
            PROSFPDCasesheetProperties property = all1.FirstOrDefault<PROSFPDCasesheetProperties>((Func<PROSFPDCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            PROSFPDCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<PROSFPDCasesheetPropertyValues>((Func<PROSFPDCasesheetPropertyValues, bool>) (a =>
            {
              if (a.PropId == property.PropertyId)
                return a.ProsthoFPDId == ViewModel.ProsthoFPDId;
              return false;
            }));
            if (casesheetPropertyValues != null)
            {
              entity2.PropId = property.PropertyId;
              entity2.PropValues = obj.ToString();
              entity2.ValueId = casesheetPropertyValues.ValueId;
              this._uow.Repository<PROSFPDCasesheetPropertyValues>().Update(entity2, false);
            }
            else
            {
              entity2.PropId = property.PropertyId;
              entity2.PropValues = obj.ToString();
              this._uow.Repository<PROSFPDCasesheetPropertyValues>().Add(entity2, false);
            }
          }
        }
      }
      if (ViewModel.CreatedepartmentReferredStatus != null)
      {
        ReferralStatus entity2 = new ReferralStatus();
        foreach (ReferralStatusViewModel createdepartmentReferredStatu in ViewModel.CreatedepartmentReferredStatus)
        {
          if (createdepartmentReferredStatu.chkToDeptId)
          {
            entity2.PatientId = ViewModel.PatientId;
            entity2.FromdeptId = 7;
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
      if (ViewModel.BillingQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.ProsthoFPDId,
          DeptId = 7,
          PatientId = ViewModel.PatientId,
          ReferredTreatmentId = 3
        });
      if (ViewModel.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(ViewModel.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = ViewModel.PatientId,
          AllotId = ViewModel.studentAllotmentViewModel.AllotId,
          DoctorId = ViewModel.studentAllotmentViewModel.DoctorId,
          StudentId = ViewModel.studentAllotmentViewModel.StudentId,
          DeptId = 7,
          ReferredTreatmentId = 3
        });
      if (ViewModel.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.ProsthoFPDId,
          ReferredTreatmentId = 3,
          DeptId = 7,
          PatientId = ViewModel.PatientId
        });
      PROSFPDAbutmentTeeth entity3 = new PROSFPDAbutmentTeeth();
      ToothNumbers toothNumbers;
      if (ViewModel.AbutList != null)
      {
        foreach (PROSFPDViewModel abut in ViewModel.AbutList)
        {
          if (abut.AbutmentTeethId != 0)
          {
            if (abut.ToothNumber != null)
            {
              entity3.ToothNumber = abut.ToothNumber;
            }
            else
            {
              PROSFPDAbutmentTeeth prosfpdAbutmentTeeth = entity3;
              toothNumbers = abut.ToothNumbers;
              string str = toothNumbers.ToString().Trim();
              prosfpdAbutmentTeeth.ToothNumber = str;
            }
            entity3.AbutmentTeethId = abut.AbutmentTeethId;
            entity3.Location = abut.Location;
            entity3.Crown = abut.Crown;
            entity3.Vitality = abut.Vitality;
            entity3.Mobility = abut.Mobility;
            entity3.Size = abut.Size;
            entity3.Length = abut.Length;
            entity3.Position = abut.Position;
            entity3.Caries = abut.Caries;
            entity3.Fracture = abut.Fracture;
            entity3.Discoloration = abut.Discoloration;
            entity3.Wearfacets = abut.Wearfacets;
            entity3.Restorations = abut.Restorations;
            this._uow.Repository<PROSFPDAbutmentTeeth>().Update(entity3, false);
          }
          else
          {
            entity3.AbutmentTeethId = 0;
            entity3.ProsthoFPDId = ViewModel.ProsthoFPDId;
            entity3.Location = abut.Location;
            PROSFPDAbutmentTeeth prosfpdAbutmentTeeth = entity3;
            toothNumbers = abut.ToothNumbers;
            string str = toothNumbers.ToString().Trim();
            prosfpdAbutmentTeeth.ToothNumber = str;
            entity3.Crown = abut.Crown;
            entity3.Vitality = abut.Vitality;
            entity3.Mobility = abut.Mobility;
            entity3.Size = abut.Size;
            entity3.Length = abut.Length;
            entity3.Position = abut.Position;
            entity3.Caries = abut.Caries;
            entity3.Fracture = abut.Fracture;
            entity3.Discoloration = abut.Discoloration;
            entity3.Wearfacets = abut.Wearfacets;
            entity3.Restorations = abut.Restorations;
            this._uow.Repository<PROSFPDAbutmentTeeth>().Add(entity3, false);
          }
        }
      }
      return entity1.ProsthoFPDId;
    }

    public PROSFPDViewModel PROSFPDBindEditPatientModel(int treatId, long allotId, int PROSFPDId)
    {
      PROSFPDViewModel prosfpdPatientDetails = this.GetPROSFPDPatientDetails(PROSFPDId);
      prosfpdPatientDetails.CHKTL4 = prosfpdPatientDetails.TL4 == "Y";
      prosfpdPatientDetails.CHKTL3 = prosfpdPatientDetails.TL3 == "Y";
      prosfpdPatientDetails.CHKTL2 = prosfpdPatientDetails.TL2 == "Y";
      prosfpdPatientDetails.CHKTL1 = prosfpdPatientDetails.TL1 == "Y";
      prosfpdPatientDetails.CHKTR1 = prosfpdPatientDetails.TR1 == "Y";
      prosfpdPatientDetails.CHKTR2 = prosfpdPatientDetails.TR2 == "Y";
      prosfpdPatientDetails.CHKTR3 = prosfpdPatientDetails.TR3 == "Y";
      prosfpdPatientDetails.CHKTR4 = prosfpdPatientDetails.TR4 == "Y";
      PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
      string whereClause = "DeptId=" + (object) 7;
      informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
      informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
      informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 7);
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      allotmentViewModel.AllotId = allotId;
      allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
      allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
      allotmentViewModel.DoctorId = informationViewModel.DoctorId;
      ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
      {
        PatientId = informationViewModel.PatientId,
        FromdeptId = 7
      };
      prosfpdPatientDetails.patientInformationViewModel = informationViewModel;
      prosfpdPatientDetails.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 7).ToList<ReferralStatusViewModel>();
      prosfpdPatientDetails.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
      prosfpdPatientDetails.ApprovedepartmentReferral = this._Referralservice.ReferralApprovalList(informationViewModel.PatientId, 7).ToList<ReferralStatusViewModel>();
      prosfpdPatientDetails.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 7, 3);
      prosfpdPatientDetails.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
      prosfpdPatientDetails.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 7);
      prosfpdPatientDetails.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 7, 3);
      prosfpdPatientDetails.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 7);
      prosfpdPatientDetails.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(7);
      prosfpdPatientDetails.studentAllotmentViewModel = allotmentViewModel;
      prosfpdPatientDetails.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 7, 3);
      prosfpdPatientDetails.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
      prosfpdPatientDetails.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
      prosfpdPatientDetails.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
      prosfpdPatientDetails.TreatmentReferredId = informationViewModel.ReferredId;
      prosfpdPatientDetails.followupViewModal = new FollowupViewModal()
      {
        PatientId = informationViewModel.PatientId,
        DeptId = 7,
        FollowupDate = DateTime.Now,
        FollowupTime = DateTime.Now
      };
      prosfpdPatientDetails.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 7, 3);
      prosfpdPatientDetails.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
      prosfpdPatientDetails.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
      prosfpdPatientDetails.AbutList = this.AbutList(PROSFPDId);
      MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
      MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
      prosfpdPatientDetails.medicalalertviewmodel = medicalAlertViewModel2;
      prosfpdPatientDetails.approvalViewModal = new ApprovalViewModal()
      {
        ApprovalTypeId = 1,
        CaserecordId = PROSFPDId,
        DeptId = 7,
        DoctorId = allotmentViewModel.DoctorId,
        PatientId = (long) prosfpdPatientDetails.PatientId,
        ReferredTreatmentId = 3
      };
      prosfpdPatientDetails.radioRegistrationviewmodel = new RadioRegistrationViewModel()
      {
        RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) prosfpdPatientDetails.PatientId))
      };
      prosfpdPatientDetails.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
      {
        LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) prosfpdPatientDetails.PatientId))
      };
      prosfpdPatientDetails.TMJlist = this._Dropdownservice.GetCodesById(83);
      IEnumerable<PROSFPDCasesheetProperties> fpdProperties = this.GetFPDProperties();
      prosfpdPatientDetails.Proplist = fpdProperties;
      if (prosfpdPatientDetails.Approval1)
      {
        prosfpdPatientDetails.DisplayApproval1 = "Approved";
        prosfpdPatientDetails.ReadOnlyApproval1 = true;
      }
      else
      {
        prosfpdPatientDetails.DisplayApproval1 = "Not Approved";
        prosfpdPatientDetails.ReadOnlyApproval1 = false;
      }
      if (prosfpdPatientDetails.Approval2)
      {
        prosfpdPatientDetails.DisplayApproval2 = "Approved";
        prosfpdPatientDetails.ReadOnlyApproval2 = true;
        prosfpdPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = true;
      }
      else
      {
        prosfpdPatientDetails.DisplayApproval2 = "Not Approved";
        prosfpdPatientDetails.ReadOnlyApproval2 = false;
        prosfpdPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = false;
      }
      if (prosfpdPatientDetails.Approval3)
      {
        prosfpdPatientDetails.DisplayApproval3 = "Casesheet Closed";
        prosfpdPatientDetails.ReadOnlyApproval3 = true;
        prosfpdPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = true;
        prosfpdPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = true;
      }
      else
      {
        prosfpdPatientDetails.DisplayApproval3 = "Not Approved";
        prosfpdPatientDetails.ReadOnlyApproval3 = false;
        prosfpdPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = false;
        prosfpdPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = false;
      }
      PROSFPDViewModel prosfpdViewModel = this._uow.Repository<PROSFPDViewModel>().GetEntitiesBySql(string.Format(Queries.DisplayProsFpdApprovalStage, (object) 7, (object) prosfpdPatientDetails.ProsthoFPDId)).FirstOrDefault<PROSFPDViewModel>();
      prosfpdPatientDetails.ApprovalStage = prosfpdViewModel == null ? "Not Initiated" : (!(prosfpdViewModel.ApprovalStage == "") ? prosfpdViewModel.ApprovalStage : "Not Initiated");
      prosfpdPatientDetails.ReadOnlyApproval4 = true;
      return prosfpdPatientDetails;
    }

    public IEnumerable<PROSFPDViewModel> AbutList(int PROSFPDId)
    {
      List<PROSFPDViewModel> prosfpdViewModelList = new List<PROSFPDViewModel>();
      return (IEnumerable<PROSFPDViewModel>) this._uow.Repository<PROSFPDViewModel>().GetEntitiesBySql(string.Format(Queries.GetToothNumByPROSFPDId, (object) PROSFPDId)).ToList<PROSFPDViewModel>();
    }

    public void PROSFPDSavefollowUp(PROSFPDViewModel model)
    {
      this._FollowUpService.Add(new FollowUp()
      {
        PatientId = model.PatientId,
        FollowupDate = new DateTime?(model.followupViewModal.FollowupDate),
        FollowupTime = new DateTime?(model.followupViewModal.FollowupTime),
        DeptId = 7,
        FollowupReason = model.followupViewModal.FollowupReason,
        CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))),
        CreatedSystem = this._Dropdownservice.GetIPAddress(false),
        ReferredTreatmentId = 3
      });
    }

    public void PROSFPDUpdateAllotment(PROSFPDViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        ProcedureNotes = model.studentAllotmentViewModel.ProcedureNotes,
        ProcedureNotesDate = new DateTime?(DateTime.Now),
        DoctorId = model.studentAllotmentViewModel.DoctorId,
        AllotId = model.studentAllotmentViewModel.AllotId,
        CaserecordId = model.ProsthoFPDId,
        ReferredTreatmentId = 3
      });
    }

    public void PROSFPDUpdateReferralStatus(PROSFPDViewModel model)
    {
      int patientId = model.PatientId;
      int num = 7;
      this._Referralservice.UpdateReferralStatus(new ReferralStatus()
      {
        VisitType = "dbo.GetVisitType(" + (object) patientId + "," + (object) num + "," + (object) model.TreatmentReferredId + ")",
        ReferredId = model.TreatmentReferredId,
        ReferredTreatmentId = 3
      });
    }

    public PROSFPDViewModel BindPROSFPDPatientReport(int PROSFPDId)
    {
      PROSFPDViewModel prosfpdViewModel1 = new PROSFPDViewModel();
      PROSFPDViewModel prosfpdViewModel2 = this._uow.Repository<PROSFPDViewModel>().GetEntitiesBySql(string.Format("exec GetPROSFPDCasesheetReport {0}", (object) PROSFPDId)).FirstOrDefault<PROSFPDViewModel>();
      prosfpdViewModel2.patientInformationViewModel = new PatientInformationViewModel()
      {
        PatientName = prosfpdViewModel2.PatientName,
        AgeGender = prosfpdViewModel2.Age.ToString() + "/" + (object) (Gender) prosfpdViewModel2.GenderId,
        Phone = prosfpdViewModel2.Phone,
        OpNo = prosfpdViewModel2.OpNo,
        Area = prosfpdViewModel2.Area
      };
      prosfpdViewModel2.FileUploadlist = this._uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) prosfpdViewModel2.PatientId, (object) 7));
      prosfpdViewModel2.paidInvestigationList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.PaidBillsforReport, (object) prosfpdViewModel2.PatientId, (object) 7, (object) PROSFPDId, (object) 3, (object) 20, (object) 16));
      prosfpdViewModel2.deptTreatmentList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.DeptTreatmentforReport, (object) prosfpdViewModel2.PatientId, (object) 7, (object) PROSFPDId, (object) 3));
      prosfpdViewModel2.AbutList = (IEnumerable<PROSFPDViewModel>) this._uow.Repository<PROSFPDViewModel>().GetEntitiesBySql(string.Format(Queries.GetToothNumByPROSFPDId, (object) PROSFPDId)).ToList<PROSFPDViewModel>();
      ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
      {
        PatientId = prosfpdViewModel2.PatientId,
        FromdeptId = 7
      };
      prosfpdViewModel2.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(prosfpdViewModel2.PatientId).ToList<ReferralStatusViewModel>();
      prosfpdViewModel2.followupViewModal = new FollowupViewModal()
      {
        PatientId = prosfpdViewModel2.PatientId,
        DeptId = 7,
        FollowupDate = DateTime.Now,
        FollowupTime = DateTime.Now
      };
      prosfpdViewModel2.followupList = this._FollowUpService.LoadFollowupList(prosfpdViewModel2.PatientId, 7, 3);
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      prosfpdViewModel2.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(prosfpdViewModel2.PatientId, 7, 3);
      return prosfpdViewModel2;
    }

    public IEnumerable<PROSTreatmentViewModel> PROSMFPRegPatientList(int patientId)
    {
      List<PROSTreatmentViewModel> treatmentViewModelList = new List<PROSTreatmentViewModel>();
      return (IEnumerable<PROSTreatmentViewModel>) this._uow.Repository<PROSTreatmentViewModel>().GetEntitiesBySql(string.Format(Queries.PROSMFPRegPatient, (object) patientId)).ToList<PROSTreatmentViewModel>();
    }

    public IEnumerable<PROSMFPCasesheetProperties> GetMFPProperties()
    {
      return this._uow.Repository<PROSMFPCasesheetProperties>().GetAll();
    }

    public PROSMFPViewModel GetPROSMFPPatientDetails(int id)
    {
      return this._uow.Repository<PROSMFPViewModel>().GetEntitiesBySql(string.Format("exec GetPROSMFPCasesheet {0}", (object) id)).FirstOrDefault<PROSMFPViewModel>();
    }

    public PROSMFPViewModel BindPROSMFPPatientModel(int treatId, long allotId)
    {
      try
      {
        PROSMFPViewModel prosmfpViewModel = new PROSMFPViewModel();
        if (!string.IsNullOrEmpty(allotId.ToString()))
        {
          IEnumerable<MASCode> allCodes = this._Dropdownservice.GetAllCodes();
          PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
          string whereClause = "DeptId=" + (object) 7;
          informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
          informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
          informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 7);
          StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
          allotmentViewModel.AllotId = allotId;
          allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
          allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
          prosmfpViewModel.TreatmentReferredId = informationViewModel.ReferredId;
          prosmfpViewModel.patientInformationViewModel = informationViewModel;
          prosmfpViewModel.studentAllotmentViewModel = allotmentViewModel;
          prosmfpViewModel.PatientId = informationViewModel.PatientId;
          prosmfpViewModel.AllotId = informationViewModel.AllotId;
          prosmfpViewModel.radioRegistrationviewmodel = new RadioRegistrationViewModel()
          {
            RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) prosmfpViewModel.PatientId))
          };
          prosmfpViewModel.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
          {
            LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) prosmfpViewModel.PatientId))
          };
          prosmfpViewModel.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
          prosmfpViewModel.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 7, 4);
          prosmfpViewModel.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 7);
          prosmfpViewModel.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(7);
          prosmfpViewModel.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 7, 4);
          prosmfpViewModel.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 7);
          prosmfpViewModel.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
          prosmfpViewModel.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 7).ToList<ReferralStatusViewModel>();
          prosmfpViewModel.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 7, 4);
          prosmfpViewModel.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
          prosmfpViewModel.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
          prosmfpViewModel.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
          prosmfpViewModel.followupViewModal = new FollowupViewModal()
          {
            PatientId = informationViewModel.PatientId,
            DeptId = 7,
            FollowupDate = DateTime.Now,
            FollowupTime = DateTime.Now
          };
          prosmfpViewModel.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 7, 0);
          IEnumerable<PROSMFPCasesheetProperties> mfpProperties = this.GetMFPProperties();
          prosmfpViewModel.Proplist = mfpProperties;
          prosmfpViewModel.FacialProfileList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 81)).ToList<MASCode>();
          prosmfpViewModel.ShapeOfFaceList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 199)).ToList<MASCode>();
          prosmfpViewModel.VertFaceHeightList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 165)).ToList<MASCode>();
          prosmfpViewModel.ToneOfFacialList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 198)).ToList<MASCode>();
          prosmfpViewModel.TMJList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 200)).ToList<MASCode>();
          prosmfpViewModel.MoveOfMandList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 201)).ToList<MASCode>();
          prosmfpViewModel.MouthLengthList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 202)).ToList<MASCode>();
          prosmfpViewModel.LipLengthSLList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 203)).ToList<MASCode>();
          prosmfpViewModel.LipLengthTTList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 204)).ToList<MASCode>();
          prosmfpViewModel.LipLengthNTList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 205)).ToList<MASCode>();
          prosmfpViewModel.LipSupportList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 206)).ToList<MASCode>();
          MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
          MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
          prosmfpViewModel.medicalalertviewmodel = medicalAlertViewModel2;
        }
        return prosmfpViewModel;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int PROSMFPSavePatient(PROSMFPViewModel model)
    {
      model.MandatoryDummy = "Y";
      PROSMFPCasesheet prosmfpCasesheet = new PROSMFPCasesheet();
      PROSMFPCasesheet entity1 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PROSMFPViewModel, PROSMFPCasesheet>())).CreateMapper().Map<PROSMFPViewModel, PROSMFPCasesheet>(model);
      entity1.ProsthoMFPId = 0;
      entity1.ProsthoMFPDate = new DateTime?(DateTime.Now);
      entity1.CreatedDate = new DateTime?(DateTime.Now);
      entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      model.TL8 = !model.CHKTL8 ? "N" : "Y";
      model.TL7 = !model.CHKTL7 ? "N" : "Y";
      model.TL6 = !model.CHKTL6 ? "N" : "Y";
      model.TL5 = !model.CHKTL5 ? "N" : "Y";
      model.TL4 = !model.CHKTL4 ? "N" : "Y";
      model.TL3 = !model.CHKTL3 ? "N" : "Y";
      model.TL2 = !model.CHKTL2 ? "N" : "Y";
      model.TL1 = !model.CHKTL1 ? "N" : "Y";
      model.BL8 = !model.CHKBL8 ? "N" : "Y";
      model.BL7 = !model.CHKBL7 ? "N" : "Y";
      model.BL6 = !model.CHKBL6 ? "N" : "Y";
      model.BL5 = !model.CHKBL5 ? "N" : "Y";
      model.BL4 = !model.CHKBL4 ? "N" : "Y";
      model.BL3 = !model.CHKBL3 ? "N" : "Y";
      model.BL2 = !model.CHKBL2 ? "N" : "Y";
      model.BL1 = !model.CHKBL1 ? "N" : "Y";
      model.TR1 = !model.CHKTR1 ? "N" : "Y";
      model.TR2 = !model.CHKTR2 ? "N" : "Y";
      model.TR3 = !model.CHKTR3 ? "N" : "Y";
      model.TR4 = !model.CHKTR4 ? "N" : "Y";
      model.TR5 = !model.CHKTR5 ? "N" : "Y";
      model.TR6 = !model.CHKTR6 ? "N" : "Y";
      model.TR7 = !model.CHKTR7 ? "N" : "Y";
      model.TR8 = !model.CHKTR8 ? "N" : "Y";
      model.BR1 = !model.CHKBR1 ? "N" : "Y";
      model.BR2 = !model.CHKBR2 ? "N" : "Y";
      model.BR3 = !model.CHKBR3 ? "N" : "Y";
      model.BR4 = !model.CHKBR4 ? "N" : "Y";
      model.BR5 = !model.CHKBR5 ? "N" : "Y";
      model.BR6 = !model.CHKBR6 ? "N" : "Y";
      model.BR7 = !model.CHKBR7 ? "N" : "Y";
      model.BR8 = !model.CHKBR8 ? "N" : "Y";
      entity1.ProsthoMFPNo = this._CasesheetNoService.GetProsthoMFPNo();
      int num = this._uow.Repository<PROSMFPCasesheet>().Add(entity1, false);
      IEnumerable<PROSMFPCasesheetProperties> all = this._uow.Repository<PROSMFPCasesheetProperties>().GetAll();
      foreach (PropertyInfo property in model.GetType().GetProperties())
      {
        PropertyInfo prop = property;
        if (all.FirstOrDefault<PROSMFPCasesheetProperties>((Func<PROSMFPCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) model, (object[]) null);
          if (obj != null)
          {
            PROSMFPCasesheetPropertyValues entity2 = new PROSMFPCasesheetPropertyValues();
            entity2.ProsthoMFPId = num;
            PROSMFPCasesheetProperties casesheetProperties = all.FirstOrDefault<PROSMFPCasesheetProperties>((Func<PROSMFPCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            entity2.PropId = casesheetProperties.PropertyId;
            entity2.PropValues = obj.ToString();
            this._uow.Repository<PROSMFPCasesheetPropertyValues>().Add(entity2, false);
          }
        }
      }
      model.ProsthoMFPId = num;
      if (model.CreatedepartmentReferredStatus != null)
      {
        ReferralStatus entity2 = new ReferralStatus();
        foreach (ReferralStatusViewModel createdepartmentReferredStatu in model.CreatedepartmentReferredStatus)
        {
          if (createdepartmentReferredStatu.chkToDeptId)
          {
            entity2.PatientId = model.PatientId;
            entity2.FromdeptId = 7;
            entity2.ReferredReason = createdepartmentReferredStatu.ReferredReason;
            entity2.ToDeptId = createdepartmentReferredStatu.ToDeptId;
            entity2.Priority = createdepartmentReferredStatu.Priority;
            entity2.RoomNo = createdepartmentReferredStatu.RoomNo;
            entity2.TreatmentStatus = createdepartmentReferredStatu.TreatmentStatus;
            ReferralStatus referralStatus1 = entity2;
            DateTime now = DateTime.Now;
            DateTime? nullable1 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            referralStatus1.FromDate = nullable1;
            ReferralStatus referralStatus2 = entity2;
            now = DateTime.Now;
            DateTime? nullable2 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            referralStatus2.CreatedDate = nullable2;
            entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            this._Referralservice.Add(entity2);
          }
        }
      }
      if (model.BillingQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(model.BillingQueueDetails, new BillQueueDetails()
        {
          CaserecordId = num,
          ReferredTreatmentId = 4,
          DeptId = 7,
          PatientId = model.PatientId
        });
      if (model.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(model.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = num,
          ReferredTreatmentId = 4,
          DeptId = 7,
          PatientId = model.PatientId
        });
      if (model.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(model.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = model.PatientId,
          AllotId = model.studentAllotmentViewModel.AllotId,
          DoctorId = model.studentAllotmentViewModel.DoctorId,
          StudentId = model.studentAllotmentViewModel.StudentId,
          DeptId = 7,
          ReferredTreatmentId = 4
        });
      return num;
    }

    public int PROSMFPUpdatePatient(PROSMFPViewModel ViewModel)
    {
      ViewModel.ProsthoMFPId = ViewModel.ProsthoMFPId;
      PROSMFPCasesheet prosmfpCasesheet = new PROSMFPCasesheet();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PROSMFPViewModel, PROSMFPCasesheet>()));
      PROSMFPCasesheet entity1 = Mapper.Map<PROSMFPViewModel, PROSMFPCasesheet>(ViewModel);
      entity1.ModifiedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      entity1.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
      ViewModel.TL8 = !ViewModel.CHKTL8 ? "N" : "Y";
      ViewModel.TL7 = !ViewModel.CHKTL7 ? "N" : "Y";
      ViewModel.TL6 = !ViewModel.CHKTL6 ? "N" : "Y";
      ViewModel.TL5 = !ViewModel.CHKTL5 ? "N" : "Y";
      ViewModel.TL4 = !ViewModel.CHKTL4 ? "N" : "Y";
      ViewModel.TL3 = !ViewModel.CHKTL3 ? "N" : "Y";
      ViewModel.TL2 = !ViewModel.CHKTL2 ? "N" : "Y";
      ViewModel.TL1 = !ViewModel.CHKTL1 ? "N" : "Y";
      ViewModel.BL8 = !ViewModel.CHKBL8 ? "N" : "Y";
      ViewModel.BL7 = !ViewModel.CHKBL7 ? "N" : "Y";
      ViewModel.BL6 = !ViewModel.CHKBL6 ? "N" : "Y";
      ViewModel.BL5 = !ViewModel.CHKBL5 ? "N" : "Y";
      ViewModel.BL4 = !ViewModel.CHKBL4 ? "N" : "Y";
      ViewModel.BL3 = !ViewModel.CHKBL3 ? "N" : "Y";
      ViewModel.BL2 = !ViewModel.CHKBL2 ? "N" : "Y";
      ViewModel.BL1 = !ViewModel.CHKBL1 ? "N" : "Y";
      ViewModel.TR1 = !ViewModel.CHKTR1 ? "N" : "Y";
      ViewModel.TR2 = !ViewModel.CHKTR2 ? "N" : "Y";
      ViewModel.TR3 = !ViewModel.CHKTR3 ? "N" : "Y";
      ViewModel.TR4 = !ViewModel.CHKTR4 ? "N" : "Y";
      ViewModel.TR5 = !ViewModel.CHKTR5 ? "N" : "Y";
      ViewModel.TR6 = !ViewModel.CHKTR6 ? "N" : "Y";
      ViewModel.TR7 = !ViewModel.CHKTR7 ? "N" : "Y";
      ViewModel.TR8 = !ViewModel.CHKTR8 ? "N" : "Y";
      ViewModel.BR1 = !ViewModel.CHKBR1 ? "N" : "Y";
      ViewModel.BR2 = !ViewModel.CHKBR2 ? "N" : "Y";
      ViewModel.BR3 = !ViewModel.CHKBR3 ? "N" : "Y";
      ViewModel.BR4 = !ViewModel.CHKBR4 ? "N" : "Y";
      ViewModel.BR5 = !ViewModel.CHKBR5 ? "N" : "Y";
      ViewModel.BR6 = !ViewModel.CHKBR6 ? "N" : "Y";
      ViewModel.BR7 = !ViewModel.CHKBR7 ? "N" : "Y";
      ViewModel.BR8 = !ViewModel.CHKBR8 ? "N" : "Y";
      this._uow.Repository<PROSMFPCasesheet>().Update(entity1, false);
      IEnumerable<PROSMFPCasesheetProperties> all1 = this._uow.Repository<PROSMFPCasesheetProperties>().GetAll();
      IEnumerable<PROSMFPCasesheetPropertyValues> all2 = this._uow.Repository<PROSMFPCasesheetPropertyValues>().GetAll("ProsthoMFPId=" + (object) ViewModel.ProsthoMFPId);
      foreach (PropertyInfo property1 in ViewModel.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<PROSMFPCasesheetProperties>((Func<PROSMFPCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) ViewModel, (object[]) null);
          if (obj != null)
          {
            PROSMFPCasesheetPropertyValues entity2 = new PROSMFPCasesheetPropertyValues();
            entity2.ProsthoMFPId = ViewModel.ProsthoMFPId;
            PROSMFPCasesheetProperties property = all1.FirstOrDefault<PROSMFPCasesheetProperties>((Func<PROSMFPCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            PROSMFPCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<PROSMFPCasesheetPropertyValues>((Func<PROSMFPCasesheetPropertyValues, bool>) (a =>
            {
              if (a.PropId == property.PropertyId)
                return a.ProsthoMFPId == ViewModel.ProsthoMFPId;
              return false;
            }));
            if (casesheetPropertyValues != null)
            {
              entity2.PropId = property.PropertyId;
              entity2.PropValues = obj.ToString();
              entity2.ValueId = casesheetPropertyValues.ValueId;
              this._uow.Repository<PROSMFPCasesheetPropertyValues>().Update(entity2, false);
            }
            else
            {
              entity2.PropId = property.PropertyId;
              entity2.PropValues = obj.ToString();
              this._uow.Repository<PROSMFPCasesheetPropertyValues>().Add(entity2, false);
            }
          }
        }
      }
      if (ViewModel.CreatedepartmentReferredStatus != null)
      {
        ReferralStatus entity2 = new ReferralStatus();
        foreach (ReferralStatusViewModel createdepartmentReferredStatu in ViewModel.CreatedepartmentReferredStatus)
        {
          if (createdepartmentReferredStatu.chkToDeptId)
          {
            entity2.PatientId = ViewModel.PatientId;
            entity2.FromdeptId = 7;
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
      if (ViewModel.BillingQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.ProsthoMFPId,
          DeptId = 7,
          PatientId = ViewModel.PatientId,
          ReferredTreatmentId = 4
        });
      if (ViewModel.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(ViewModel.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = ViewModel.PatientId,
          AllotId = ViewModel.studentAllotmentViewModel.AllotId,
          DoctorId = ViewModel.studentAllotmentViewModel.DoctorId,
          StudentId = ViewModel.studentAllotmentViewModel.StudentId,
          DeptId = 7,
          ReferredTreatmentId = 4
        });
      PROSMFPTreatmentDescription entity3 = new PROSMFPTreatmentDescription();
      if (ViewModel.MfpTreatList != null)
      {
        foreach (PROSMFPViewModel mfpTreat in ViewModel.MfpTreatList)
        {
          if (mfpTreat.ProsMaxilloProId != 0)
          {
            entity3.ProsMaxilloProId = mfpTreat.ProsMaxilloProId;
            entity3.MfpTreatmentDate = new DateTime?(Convert.ToDateTime(mfpTreat.MfpTreatmentDateDisplay));
            entity3.MfpTreatmentDescription = mfpTreat.MfpTreatmentDescription;
            entity3.MfpRemarks = mfpTreat.MfpRemarks;
            this._uow.Repository<PROSMFPTreatmentDescription>().Update(entity3, false);
          }
          else
          {
            entity3.ProsMaxilloProId = 0;
            entity3.ProsthoMFPId = ViewModel.ProsthoMFPId;
            entity3.MfpTreatmentDate = new DateTime?(Convert.ToDateTime(mfpTreat.MfpTreatmentDateDisplay));
            entity3.MfpTreatmentDescription = mfpTreat.MfpTreatmentDescription;
            entity3.MfpRemarks = mfpTreat.MfpRemarks;
            this._uow.Repository<PROSMFPTreatmentDescription>().Add(entity3, false);
          }
        }
      }
      if (ViewModel.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.ProsthoMFPId,
          ReferredTreatmentId = 4,
          DeptId = 7,
          PatientId = ViewModel.PatientId
        });
      return entity1.ProsthoMFPId;
    }

    public PROSMFPViewModel PROSMFPBindEditPatientModel(int treatId, long allotId, int PROSMFPId)
    {
      IEnumerable<MASCode> allCodes = this._Dropdownservice.GetAllCodes();
      PROSMFPViewModel prosmfpPatientDetails = this.GetPROSMFPPatientDetails(PROSMFPId);
      prosmfpPatientDetails.CHKTL8 = prosmfpPatientDetails.TL8 == "Y";
      prosmfpPatientDetails.CHKTL7 = prosmfpPatientDetails.TL7 == "Y";
      prosmfpPatientDetails.CHKTL6 = prosmfpPatientDetails.TL6 == "Y";
      prosmfpPatientDetails.CHKTL5 = prosmfpPatientDetails.TL5 == "Y";
      prosmfpPatientDetails.CHKTL4 = prosmfpPatientDetails.TL4 == "Y";
      prosmfpPatientDetails.CHKTL3 = prosmfpPatientDetails.TL3 == "Y";
      prosmfpPatientDetails.CHKTL2 = prosmfpPatientDetails.TL2 == "Y";
      prosmfpPatientDetails.CHKTL1 = prosmfpPatientDetails.TL1 == "Y";
      prosmfpPatientDetails.CHKBL8 = prosmfpPatientDetails.BL8 == "Y";
      prosmfpPatientDetails.CHKBL7 = prosmfpPatientDetails.BL7 == "Y";
      prosmfpPatientDetails.CHKBL6 = prosmfpPatientDetails.BL6 == "Y";
      prosmfpPatientDetails.CHKBL5 = prosmfpPatientDetails.BL5 == "Y";
      prosmfpPatientDetails.CHKBL4 = prosmfpPatientDetails.BL4 == "Y";
      prosmfpPatientDetails.CHKBL3 = prosmfpPatientDetails.BL3 == "Y";
      prosmfpPatientDetails.CHKBL2 = prosmfpPatientDetails.BL2 == "Y";
      prosmfpPatientDetails.CHKBL1 = prosmfpPatientDetails.BL1 == "Y";
      prosmfpPatientDetails.CHKTR1 = prosmfpPatientDetails.TR1 == "Y";
      prosmfpPatientDetails.CHKTR2 = prosmfpPatientDetails.TR2 == "Y";
      prosmfpPatientDetails.CHKTR3 = prosmfpPatientDetails.TR3 == "Y";
      prosmfpPatientDetails.CHKTR4 = prosmfpPatientDetails.TR4 == "Y";
      prosmfpPatientDetails.CHKTR5 = prosmfpPatientDetails.TR5 == "Y";
      prosmfpPatientDetails.CHKTR6 = prosmfpPatientDetails.TR6 == "Y";
      prosmfpPatientDetails.CHKTR7 = prosmfpPatientDetails.TR7 == "Y";
      prosmfpPatientDetails.CHKTR8 = prosmfpPatientDetails.TR8 == "Y";
      prosmfpPatientDetails.CHKBR1 = prosmfpPatientDetails.BR1 == "Y";
      prosmfpPatientDetails.CHKBR2 = prosmfpPatientDetails.BR2 == "Y";
      prosmfpPatientDetails.CHKBR3 = prosmfpPatientDetails.BR3 == "Y";
      prosmfpPatientDetails.CHKBR4 = prosmfpPatientDetails.BR4 == "Y";
      prosmfpPatientDetails.CHKBR5 = prosmfpPatientDetails.BR5 == "Y";
      prosmfpPatientDetails.CHKBR6 = prosmfpPatientDetails.BR6 == "Y";
      prosmfpPatientDetails.CHKBR7 = prosmfpPatientDetails.BR7 == "Y";
      prosmfpPatientDetails.CHKBR8 = prosmfpPatientDetails.BR8 == "Y";
      PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
      string whereClause = "DeptId=" + (object) 7;
      informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
      informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
      informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 7);
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      allotmentViewModel.AllotId = allotId;
      allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
      allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
      allotmentViewModel.DoctorId = informationViewModel.DoctorId;
      ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
      {
        PatientId = informationViewModel.PatientId,
        FromdeptId = 7
      };
      prosmfpPatientDetails.patientInformationViewModel = informationViewModel;
      prosmfpPatientDetails.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 7).ToList<ReferralStatusViewModel>();
      prosmfpPatientDetails.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
      prosmfpPatientDetails.ApprovedepartmentReferral = this._Referralservice.ReferralApprovalList(informationViewModel.PatientId, 7).ToList<ReferralStatusViewModel>();
      prosmfpPatientDetails.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 7, 4);
      prosmfpPatientDetails.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
      prosmfpPatientDetails.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 7);
      prosmfpPatientDetails.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 7, 4);
      prosmfpPatientDetails.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 7);
      prosmfpPatientDetails.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(7);
      prosmfpPatientDetails.studentAllotmentViewModel = allotmentViewModel;
      prosmfpPatientDetails.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 7, 4);
      prosmfpPatientDetails.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
      prosmfpPatientDetails.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
      prosmfpPatientDetails.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
      prosmfpPatientDetails.TreatmentReferredId = informationViewModel.ReferredId;
      prosmfpPatientDetails.followupViewModal = new FollowupViewModal()
      {
        PatientId = informationViewModel.PatientId,
        DeptId = 7,
        FollowupDate = DateTime.Now,
        FollowupTime = DateTime.Now
      };
      prosmfpPatientDetails.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 7, 4);
      prosmfpPatientDetails.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
      prosmfpPatientDetails.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
      prosmfpPatientDetails.MfpTreatList = this.MfpTreatList(PROSMFPId);
      prosmfpPatientDetails.FacialProfileList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 81)).ToList<MASCode>();
      prosmfpPatientDetails.ShapeOfFaceList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 199)).ToList<MASCode>();
      prosmfpPatientDetails.VertFaceHeightList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 165)).ToList<MASCode>();
      prosmfpPatientDetails.ToneOfFacialList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 198)).ToList<MASCode>();
      prosmfpPatientDetails.TMJList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 200)).ToList<MASCode>();
      prosmfpPatientDetails.MoveOfMandList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 201)).ToList<MASCode>();
      prosmfpPatientDetails.MouthLengthList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 202)).ToList<MASCode>();
      prosmfpPatientDetails.LipLengthSLList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 203)).ToList<MASCode>();
      prosmfpPatientDetails.LipLengthTTList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 204)).ToList<MASCode>();
      prosmfpPatientDetails.LipLengthNTList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 205)).ToList<MASCode>();
      prosmfpPatientDetails.LipSupportList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 206)).ToList<MASCode>();
      MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
      MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
      prosmfpPatientDetails.medicalalertviewmodel = medicalAlertViewModel2;
      prosmfpPatientDetails.radioRegistrationviewmodel = new RadioRegistrationViewModel()
      {
        RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) prosmfpPatientDetails.PatientId))
      };
      prosmfpPatientDetails.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
      {
        LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) prosmfpPatientDetails.PatientId))
      };
      prosmfpPatientDetails.approvalViewModal = new ApprovalViewModal()
      {
        ApprovalTypeId = 1,
        CaserecordId = PROSMFPId,
        DeptId = 7,
        DoctorId = allotmentViewModel.DoctorId,
        PatientId = (long) prosmfpPatientDetails.PatientId,
        ReferredTreatmentId = 4
      };
      IEnumerable<PROSMFPCasesheetProperties> mfpProperties = this.GetMFPProperties();
      prosmfpPatientDetails.Proplist = mfpProperties;
      if (prosmfpPatientDetails.Approval1)
      {
        prosmfpPatientDetails.DisplayApproval1 = "Approved";
        prosmfpPatientDetails.ReadOnlyApproval1 = true;
      }
      else
      {
        prosmfpPatientDetails.DisplayApproval1 = "Not Approved";
        prosmfpPatientDetails.ReadOnlyApproval1 = false;
      }
      if (prosmfpPatientDetails.Approval2)
      {
        prosmfpPatientDetails.DisplayApproval2 = "Approved";
        prosmfpPatientDetails.ReadOnlyApproval2 = true;
        prosmfpPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = true;
      }
      else
      {
        prosmfpPatientDetails.DisplayApproval2 = "Not Approved";
        prosmfpPatientDetails.ReadOnlyApproval2 = false;
        prosmfpPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = false;
      }
      if (prosmfpPatientDetails.Approval3)
      {
        prosmfpPatientDetails.DisplayApproval3 = "Casesheet Closed";
        prosmfpPatientDetails.ReadOnlyApproval3 = true;
        prosmfpPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = true;
        prosmfpPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = true;
      }
      else
      {
        prosmfpPatientDetails.DisplayApproval3 = "Not Approved";
        prosmfpPatientDetails.ReadOnlyApproval3 = false;
        prosmfpPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = false;
        prosmfpPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = false;
      }
      PROSMFPViewModel prosmfpViewModel = this._uow.Repository<PROSMFPViewModel>().GetEntitiesBySql(string.Format(Queries.DisplayProsMfpApprovalStage, (object) 7, (object) prosmfpPatientDetails.ProsthoMFPId)).FirstOrDefault<PROSMFPViewModel>();
      prosmfpPatientDetails.ApprovalStage = prosmfpViewModel == null ? "Not Initiated" : (!(prosmfpViewModel.ApprovalStage == "") ? prosmfpViewModel.ApprovalStage : "Not Initiated");
      prosmfpPatientDetails.ReadOnlyApproval4 = true;
      return prosmfpPatientDetails;
    }

    public IEnumerable<PROSMFPViewModel> MfpTreatList(int PROSMFPId)
    {
      List<PROSMFPViewModel> prosmfpViewModelList = new List<PROSMFPViewModel>();
      return (IEnumerable<PROSMFPViewModel>) this._uow.Repository<PROSMFPViewModel>().GetEntitiesBySql(string.Format(Queries.GetMfpTreatDetailsByProsMfpId, (object) PROSMFPId)).ToList<PROSMFPViewModel>();
    }

    public void PROSMFPSavefollowUp(PROSMFPViewModel model)
    {
      this._FollowUpService.Add(new FollowUp()
      {
        PatientId = model.PatientId,
        FollowupDate = new DateTime?(model.followupViewModal.FollowupDate),
        FollowupTime = new DateTime?(model.followupViewModal.FollowupTime),
        DeptId = 7,
        FollowupReason = model.followupViewModal.FollowupReason,
        CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))),
        CreatedSystem = this._Dropdownservice.GetIPAddress(false),
        ReferredTreatmentId = 4
      });
    }

    public void PROSMFPUpdateAllotment(PROSMFPViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        ProcedureNotes = model.studentAllotmentViewModel.ProcedureNotes,
        ProcedureNotesDate = new DateTime?(DateTime.Now),
        DoctorId = model.studentAllotmentViewModel.DoctorId,
        AllotId = model.studentAllotmentViewModel.AllotId,
        CaserecordId = model.ProsthoMFPId,
        ReferredTreatmentId = 4
      });
    }

    public void PROSMFPUpdateReferralStatus(PROSMFPViewModel model)
    {
      int patientId = model.PatientId;
      int num = 7;
      this._Referralservice.UpdateReferralStatus(new ReferralStatus()
      {
        VisitType = "dbo.GetVisitType(" + (object) patientId + "," + (object) num + "," + (object) model.TreatmentReferredId + ")",
        ReferredId = model.TreatmentReferredId,
        ReferredTreatmentId = 4
      });
    }

    public PROSMFPViewModel BindPROSMFPPatientReport(int PROSMFPId)
    {
      PROSMFPViewModel prosmfpViewModel1 = new PROSMFPViewModel();
      PROSMFPViewModel prosmfpViewModel2 = this._uow.Repository<PROSMFPViewModel>().GetEntitiesBySql(string.Format("exec GetPROSMFPCasesheetReport {0}", (object) PROSMFPId)).FirstOrDefault<PROSMFPViewModel>();
      prosmfpViewModel2.patientInformationViewModel = new PatientInformationViewModel()
      {
        PatientName = prosmfpViewModel2.PatientName,
        AgeGender = prosmfpViewModel2.Age.ToString() + "/" + (object) (Gender) prosmfpViewModel2.GenderId,
        Phone = prosmfpViewModel2.Phone,
        OpNo = prosmfpViewModel2.OpNo,
        Area = prosmfpViewModel2.Area
      };
      prosmfpViewModel2.FileUploadlist = this._uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) prosmfpViewModel2.PatientId, (object) 7));
      prosmfpViewModel2.paidInvestigationList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.PaidBillsforReport, (object) prosmfpViewModel2.PatientId, (object) 7, (object) PROSMFPId, (object) 4, (object) 20, (object) 16));
      prosmfpViewModel2.deptTreatmentList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.DeptTreatmentforReport, (object) prosmfpViewModel2.PatientId, (object) 7, (object) PROSMFPId, (object) 4));
      ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
      {
        PatientId = prosmfpViewModel2.PatientId,
        FromdeptId = 7
      };
      prosmfpViewModel2.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(prosmfpViewModel2.PatientId).ToList<ReferralStatusViewModel>();
      prosmfpViewModel2.followupViewModal = new FollowupViewModal()
      {
        PatientId = prosmfpViewModel2.PatientId,
        DeptId = 7,
        FollowupDate = DateTime.Now,
        FollowupTime = DateTime.Now
      };
      prosmfpViewModel2.followupList = this._FollowUpService.LoadFollowupList(prosmfpViewModel2.PatientId, 7, 4);
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      prosmfpViewModel2.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(prosmfpViewModel2.PatientId, 7, 4);
      return prosmfpViewModel2;
    }

    public IEnumerable<PROSTreatmentViewModel> PROSDIMRegPatientList(int patientId)
    {
      List<PROSTreatmentViewModel> treatmentViewModelList = new List<PROSTreatmentViewModel>();
      foreach (PROSTreatmentViewModel treatmentViewModel in this._uow.Repository<PROSTreatmentViewModel>().GetEntitiesBySql(string.Format(Queries.PROSDIMRegPatient, (object) patientId)))
        treatmentViewModelList.Add(new PROSTreatmentViewModel()
        {
          ProsthoDIMId = treatmentViewModel.ProsthoDIMId,
          ProsthoDIMNo = treatmentViewModel.ProsthoDIMNo,
          prosthoDIMdateDisplay = treatmentViewModel.prosthoDIMdateDisplay,
          prosDIMchiefcomplaint = treatmentViewModel.prosDIMchiefcomplaint
        });
      return (IEnumerable<PROSTreatmentViewModel>) treatmentViewModelList;
    }

    public IEnumerable<PROSDIMCasesheetProperties> GetDIMProperties()
    {
      return this._uow.Repository<PROSDIMCasesheetProperties>().GetAll();
    }

    public PROSDIMViewModel GetPROSDIMPatientDetails(int id)
    {
      return this._uow.Repository<PROSDIMViewModel>().GetEntitiesBySql(string.Format("exec GetPROSDIMCasesheet {0}", (object) id)).FirstOrDefault<PROSDIMViewModel>();
    }

    public PROSDIMViewModel BindPROSDIMPatientModel(int treatId, long allotId)
    {
      PROSDIMViewModel prosdimViewModel = new PROSDIMViewModel();
      if (!string.IsNullOrEmpty(allotId.ToString()))
      {
        IEnumerable<MASCode> allCodes = this._Dropdownservice.GetAllCodes();
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + (object) 7;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 7);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        prosdimViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        prosdimViewModel.patientInformationViewModel = informationViewModel;
        prosdimViewModel.studentAllotmentViewModel = allotmentViewModel;
        prosdimViewModel.PatientId = informationViewModel.PatientId;
        prosdimViewModel.AllotId = informationViewModel.AllotId;
        prosdimViewModel.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        prosdimViewModel.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 7, 5);
        prosdimViewModel.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 7);
        prosdimViewModel.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(7);
        prosdimViewModel.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 7, 5);
        prosdimViewModel.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 7);
        prosdimViewModel.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        prosdimViewModel.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 7).ToList<ReferralStatusViewModel>();
        prosdimViewModel.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 7, 5);
        prosdimViewModel.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        prosdimViewModel.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        prosdimViewModel.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        prosdimViewModel.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 7,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        prosdimViewModel.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 7, 0);
        prosdimViewModel.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) prosdimViewModel.PatientId))
        };
        prosdimViewModel.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) prosdimViewModel.PatientId))
        };
        IEnumerable<PROSDIMCasesheetProperties> dimProperties = this.GetDIMProperties();
        prosdimViewModel.Proplist = dimProperties;
        prosdimViewModel.Clenchinglist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 207)).ToList<MASCode>();
        prosdimViewModel.GrindingBruxismlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 208)).ToList<MASCode>();
        prosdimViewModel.MastiMusclelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 209)).ToList<MASCode>();
        prosdimViewModel.Bitinglist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 210)).ToList<MASCode>();
        prosdimViewModel.Chewinglist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 211)).ToList<MASCode>();
        prosdimViewModel.Verticallist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 212)).ToList<MASCode>();
        prosdimViewModel.Mucosalist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 213)).ToList<MASCode>();
        prosdimViewModel.SoftTissuelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 214)).ToList<MASCode>();
        prosdimViewModel.DepthOfVestibulelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 215)).ToList<MASCode>();
        prosdimViewModel.ToothExposurelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 216)).ToList<MASCode>();
        prosdimViewModel.MouthOpeninglist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 217)).ToList<MASCode>();
        prosdimViewModel.TemporizationList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 221)).ToList<MASCode>();
        prosdimViewModel.LoadingList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 222)).ToList<MASCode>();
        prosdimViewModel.AbutmentTypeList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 223)).ToList<MASCode>();
        prosdimViewModel.TrayTypeList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 224)).ToList<MASCode>();
        prosdimViewModel.ImpressionTypeList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 225)).ToList<MASCode>();
        prosdimViewModel.LipsIncompList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 121)).ToList<MASCode>();
        prosdimViewModel.GingivaList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 226)).ToList<MASCode>();
        prosdimViewModel.OpposingDentitionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 227)).ToList<MASCode>();
        prosdimViewModel.BoneDivisionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 228)).ToList<MASCode>();
        prosdimViewModel.ProstheticOptionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 229)).ToList<MASCode>();
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        prosdimViewModel.medicalalertviewmodel = medicalAlertViewModel2;
      }
      return prosdimViewModel;
    }

    public int PROSDIMSavePatient(PROSDIMViewModel model)
    {
      model.MandatoryDummy = "Y";
      PROSDIMCasesheet prosdimCasesheet = new PROSDIMCasesheet();
      PROSDIMCasesheet entity1 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PROSDIMViewModel, PROSDIMCasesheet>())).CreateMapper().Map<PROSDIMViewModel, PROSDIMCasesheet>(model);
      entity1.ProsthoDIMId = 0;
      entity1.ProsthoDIMDate = new DateTime?(DateTime.Now);
      entity1.CreatedDate = new DateTime?(DateTime.Now);
      entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      if (model.CHKTL4)
        model.TL4 = "Y";
      else
        model.TL3 = "N";
      model.TL3 = !model.CHKTL3 ? "N" : "Y";
      model.TL2 = !model.CHKTL2 ? "N" : "Y";
      model.TL1 = !model.CHKTL1 ? "N" : "Y";
      model.TR1 = !model.CHKTR1 ? "N" : "Y";
      model.TR2 = !model.CHKTR2 ? "N" : "Y";
      model.TR3 = !model.CHKTR3 ? "N" : "Y";
      model.TR4 = !model.CHKTR4 ? "N" : "Y";
      entity1.ProsthoDIMNo = this._CasesheetNoService.GetProsthoDIMNo();
      int num = this._uow.Repository<PROSDIMCasesheet>().Add(entity1, false);
      IEnumerable<PROSDIMCasesheetProperties> all = this._uow.Repository<PROSDIMCasesheetProperties>().GetAll();
      foreach (PropertyInfo property in model.GetType().GetProperties())
      {
        PropertyInfo prop = property;
        if (all.FirstOrDefault<PROSDIMCasesheetProperties>((Func<PROSDIMCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) model, (object[]) null);
          if (obj != null)
          {
            PROSDIMCasesheetPropertyValues entity2 = new PROSDIMCasesheetPropertyValues();
            entity2.ProsthoDIMId = num;
            PROSDIMCasesheetProperties casesheetProperties = all.FirstOrDefault<PROSDIMCasesheetProperties>((Func<PROSDIMCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            entity2.PropId = casesheetProperties.PropertyId;
            entity2.PropValues = obj.ToString();
            this._uow.Repository<PROSDIMCasesheetPropertyValues>().Add(entity2, false);
          }
        }
      }
      model.ProsthoDIMId = num;
      if (model.CreatedepartmentReferredStatus != null)
      {
        ReferralStatus entity2 = new ReferralStatus();
        foreach (ReferralStatusViewModel createdepartmentReferredStatu in model.CreatedepartmentReferredStatus)
        {
          if (createdepartmentReferredStatu.chkToDeptId)
          {
            entity2.PatientId = model.PatientId;
            entity2.FromdeptId = 7;
            entity2.ReferredReason = createdepartmentReferredStatu.ReferredReason;
            entity2.ToDeptId = createdepartmentReferredStatu.ToDeptId;
            entity2.Priority = createdepartmentReferredStatu.Priority;
            entity2.RoomNo = createdepartmentReferredStatu.RoomNo;
            entity2.TreatmentStatus = createdepartmentReferredStatu.TreatmentStatus;
            ReferralStatus referralStatus1 = entity2;
            DateTime now = DateTime.Now;
            DateTime? nullable1 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            referralStatus1.FromDate = nullable1;
            ReferralStatus referralStatus2 = entity2;
            now = DateTime.Now;
            DateTime? nullable2 = new DateTime?(Convert.ToDateTime(now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            referralStatus2.CreatedDate = nullable2;
            entity2.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
            this._Referralservice.Add(entity2);
          }
        }
      }
      if (model.BillingQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(model.BillingQueueDetails, new BillQueueDetails()
        {
          CaserecordId = num,
          ReferredTreatmentId = 5,
          DeptId = 7,
          PatientId = model.PatientId
        });
      if (model.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(model.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = num,
          ReferredTreatmentId = 5,
          DeptId = 7,
          PatientId = model.PatientId
        });
      if (model.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(model.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = model.PatientId,
          AllotId = model.studentAllotmentViewModel.AllotId,
          DoctorId = model.studentAllotmentViewModel.DoctorId,
          StudentId = model.studentAllotmentViewModel.StudentId,
          DeptId = 7,
          ReferredTreatmentId = 5
        });
      return num;
    }

    public int PROSDIMUpdatePatient(PROSDIMViewModel ViewModel)
    {
      ViewModel.ProsthoDIMId = ViewModel.ProsthoDIMId;
      PROSDIMCasesheet prosdimCasesheet = new PROSDIMCasesheet();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PROSDIMViewModel, PROSDIMCasesheet>()));
      PROSDIMCasesheet entity1 = Mapper.Map<PROSDIMViewModel, PROSDIMCasesheet>(ViewModel);
      entity1.ModifiedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      entity1.ModifiedSystem = this._Dropdownservice.GetIPAddress(false);
      if (ViewModel.CHKTL4)
        ViewModel.TL4 = "Y";
      else
        ViewModel.TL3 = "N";
      ViewModel.TL3 = !ViewModel.CHKTL3 ? "N" : "Y";
      ViewModel.TL2 = !ViewModel.CHKTL2 ? "N" : "Y";
      ViewModel.TL1 = !ViewModel.CHKTL1 ? "N" : "Y";
      ViewModel.TR1 = !ViewModel.CHKTR1 ? "N" : "Y";
      ViewModel.TR2 = !ViewModel.CHKTR2 ? "N" : "Y";
      ViewModel.TR3 = !ViewModel.CHKTR3 ? "N" : "Y";
      ViewModel.TR4 = !ViewModel.CHKTR4 ? "N" : "Y";
      this._uow.Repository<PROSDIMCasesheet>().Update(entity1, false);
      IEnumerable<PROSDIMCasesheetProperties> all1 = this._uow.Repository<PROSDIMCasesheetProperties>().GetAll();
      IEnumerable<PROSDIMCasesheetPropertyValues> all2 = this._uow.Repository<PROSDIMCasesheetPropertyValues>().GetAll("ProsthoDIMId=" + (object) ViewModel.ProsthoDIMId);
      foreach (PropertyInfo property1 in ViewModel.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<PROSDIMCasesheetProperties>((Func<PROSDIMCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) ViewModel, (object[]) null);
          if (obj != null)
          {
            PROSDIMCasesheetPropertyValues entity2 = new PROSDIMCasesheetPropertyValues();
            entity2.ProsthoDIMId = ViewModel.ProsthoDIMId;
            PROSDIMCasesheetProperties property = all1.FirstOrDefault<PROSDIMCasesheetProperties>((Func<PROSDIMCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            PROSDIMCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<PROSDIMCasesheetPropertyValues>((Func<PROSDIMCasesheetPropertyValues, bool>) (a =>
            {
              if (a.PropId == property.PropertyId)
                return a.ProsthoDIMId == ViewModel.ProsthoDIMId;
              return false;
            }));
            if (casesheetPropertyValues != null)
            {
              entity2.PropId = property.PropertyId;
              entity2.PropValues = obj.ToString();
              entity2.ValueId = casesheetPropertyValues.ValueId;
              this._uow.Repository<PROSDIMCasesheetPropertyValues>().Update(entity2, false);
            }
            else
            {
              entity2.PropId = property.PropertyId;
              entity2.PropValues = obj.ToString();
              this._uow.Repository<PROSDIMCasesheetPropertyValues>().Add(entity2, false);
            }
          }
        }
      }
      if (ViewModel.CreatedepartmentReferredStatus != null)
      {
        ReferralStatus entity2 = new ReferralStatus();
        foreach (ReferralStatusViewModel createdepartmentReferredStatu in ViewModel.CreatedepartmentReferredStatus)
        {
          if (createdepartmentReferredStatu.chkToDeptId)
          {
            entity2.PatientId = ViewModel.PatientId;
            entity2.FromdeptId = 7;
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
      if (ViewModel.BillingQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.ProsthoDIMId,
          DeptId = 7,
          PatientId = ViewModel.PatientId,
          ReferredTreatmentId = 5
        });
      if (ViewModel.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(ViewModel.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = ViewModel.PatientId,
          AllotId = ViewModel.studentAllotmentViewModel.AllotId,
          DoctorId = ViewModel.studentAllotmentViewModel.DoctorId,
          StudentId = ViewModel.studentAllotmentViewModel.StudentId,
          DeptId = 7,
          ReferredTreatmentId = 5
        });
      if (ViewModel.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.ProsthoDIMId,
          ReferredTreatmentId = 5,
          DeptId = 7,
          PatientId = ViewModel.PatientId
        });
      return entity1.ProsthoDIMId;
    }

    public PROSDIMViewModel PROSDIMBindEditPatientModel(int treatId, long allotId, int PROSDIMId)
    {
      IEnumerable<MASCode> allCodes = this._Dropdownservice.GetAllCodes();
      PROSDIMViewModel prosdimPatientDetails = this.GetPROSDIMPatientDetails(PROSDIMId);
      prosdimPatientDetails.CHKTL4 = prosdimPatientDetails.TL4 == "Y";
      prosdimPatientDetails.CHKTL3 = prosdimPatientDetails.TL3 == "Y";
      prosdimPatientDetails.CHKTL2 = prosdimPatientDetails.TL2 == "Y";
      prosdimPatientDetails.CHKTL1 = prosdimPatientDetails.TL1 == "Y";
      prosdimPatientDetails.CHKTR1 = prosdimPatientDetails.TR1 == "Y";
      prosdimPatientDetails.CHKTR2 = prosdimPatientDetails.TR2 == "Y";
      prosdimPatientDetails.CHKTR3 = prosdimPatientDetails.TR3 == "Y";
      prosdimPatientDetails.CHKTR4 = prosdimPatientDetails.TR4 == "Y";
      PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
      string whereClause = "DeptId=" + (object) 7;
      informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
      informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
      informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 7);
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      allotmentViewModel.AllotId = allotId;
      allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
      allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
      allotmentViewModel.DoctorId = informationViewModel.DoctorId;
      ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
      {
        PatientId = informationViewModel.PatientId,
        FromdeptId = 7
      };
      prosdimPatientDetails.patientInformationViewModel = informationViewModel;
      prosdimPatientDetails.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 7).ToList<ReferralStatusViewModel>();
      prosdimPatientDetails.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
      prosdimPatientDetails.ApprovedepartmentReferral = this._Referralservice.ReferralApprovalList(informationViewModel.PatientId, 7).ToList<ReferralStatusViewModel>();
      prosdimPatientDetails.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 7, 5);
      prosdimPatientDetails.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
      prosdimPatientDetails.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 7);
      prosdimPatientDetails.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 7, 5);
      prosdimPatientDetails.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 7);
      prosdimPatientDetails.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(7);
      prosdimPatientDetails.studentAllotmentViewModel = allotmentViewModel;
      prosdimPatientDetails.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 7, 5);
      prosdimPatientDetails.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
      prosdimPatientDetails.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
      prosdimPatientDetails.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
      prosdimPatientDetails.TreatmentReferredId = informationViewModel.ReferredId;
      prosdimPatientDetails.followupViewModal = new FollowupViewModal()
      {
        PatientId = informationViewModel.PatientId,
        DeptId = 7,
        FollowupDate = DateTime.Now,
        FollowupTime = DateTime.Now
      };
      prosdimPatientDetails.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 7, 5);
      prosdimPatientDetails.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
      prosdimPatientDetails.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
      prosdimPatientDetails.Clenchinglist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 207)).ToList<MASCode>();
      prosdimPatientDetails.GrindingBruxismlist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 208)).ToList<MASCode>();
      prosdimPatientDetails.MastiMusclelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 209)).ToList<MASCode>();
      prosdimPatientDetails.Bitinglist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 210)).ToList<MASCode>();
      prosdimPatientDetails.Chewinglist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 211)).ToList<MASCode>();
      prosdimPatientDetails.Verticallist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 212)).ToList<MASCode>();
      prosdimPatientDetails.Mucosalist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 213)).ToList<MASCode>();
      prosdimPatientDetails.SoftTissuelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 214)).ToList<MASCode>();
      prosdimPatientDetails.DepthOfVestibulelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 215)).ToList<MASCode>();
      prosdimPatientDetails.ToothExposurelist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 216)).ToList<MASCode>();
      prosdimPatientDetails.MouthOpeninglist = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 217)).ToList<MASCode>();
      prosdimPatientDetails.TemporizationList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 221)).ToList<MASCode>();
      prosdimPatientDetails.LoadingList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 222)).ToList<MASCode>();
      prosdimPatientDetails.AbutmentTypeList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 223)).ToList<MASCode>();
      prosdimPatientDetails.TrayTypeList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 224)).ToList<MASCode>();
      prosdimPatientDetails.ImpressionTypeList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 225)).ToList<MASCode>();
      prosdimPatientDetails.LipsIncompList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 121)).ToList<MASCode>();
      prosdimPatientDetails.GingivaList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 226)).ToList<MASCode>();
      prosdimPatientDetails.OpposingDentitionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 227)).ToList<MASCode>();
      prosdimPatientDetails.BoneDivisionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 228)).ToList<MASCode>();
      prosdimPatientDetails.ProstheticOptionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 229)).ToList<MASCode>();
      IEnumerable<PROSDIMCasesheetProperties> dimProperties = this.GetDIMProperties();
      prosdimPatientDetails.Proplist = dimProperties;
      MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
      MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
      prosdimPatientDetails.medicalalertviewmodel = medicalAlertViewModel2;
      prosdimPatientDetails.radioRegistrationviewmodel = new RadioRegistrationViewModel()
      {
        RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) prosdimPatientDetails.PatientId))
      };
      prosdimPatientDetails.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
      {
        LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) prosdimPatientDetails.PatientId))
      };
      prosdimPatientDetails.approvalViewModal = new ApprovalViewModal()
      {
        ApprovalTypeId = 1,
        CaserecordId = PROSDIMId,
        DeptId = 7,
        DoctorId = allotmentViewModel.DoctorId,
        PatientId = (long) prosdimPatientDetails.PatientId,
        ReferredTreatmentId = 5
      };
      if (prosdimPatientDetails.Approval1)
      {
        prosdimPatientDetails.DisplayApproval1 = "Approved";
        prosdimPatientDetails.ReadOnlyApproval1 = true;
      }
      else
      {
        prosdimPatientDetails.DisplayApproval1 = "Not Approved";
        prosdimPatientDetails.ReadOnlyApproval1 = false;
      }
      if (prosdimPatientDetails.Approval2)
      {
        prosdimPatientDetails.DisplayApproval2 = "Approved";
        prosdimPatientDetails.ReadOnlyApproval2 = true;
        prosdimPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = true;
      }
      else
      {
        prosdimPatientDetails.DisplayApproval2 = "Not Approved";
        prosdimPatientDetails.ReadOnlyApproval2 = false;
        prosdimPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = false;
      }
      if (prosdimPatientDetails.Approval3)
      {
        prosdimPatientDetails.DisplayApproval3 = "Casesheet Closed";
        prosdimPatientDetails.ReadOnlyApproval3 = true;
        prosdimPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = true;
        prosdimPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = true;
      }
      else
      {
        prosdimPatientDetails.DisplayApproval3 = "Not Approved";
        prosdimPatientDetails.ReadOnlyApproval3 = false;
        prosdimPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = false;
        prosdimPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = false;
      }
      PROSDIMViewModel prosdimViewModel = this._uow.Repository<PROSDIMViewModel>().GetEntitiesBySql(string.Format(Queries.DisplayProsDimApprovalStage, (object) 7, (object) prosdimPatientDetails.ProsthoDIMId)).FirstOrDefault<PROSDIMViewModel>();
      prosdimPatientDetails.ApprovalStage = prosdimViewModel == null ? "Not Initiated" : (!(prosdimViewModel.ApprovalStage == "") ? prosdimViewModel.ApprovalStage : "Not Initiated");
      prosdimPatientDetails.ReadOnlyApproval4 = true;
      return prosdimPatientDetails;
    }

    public void PROSDIMSavefollowUp(PROSDIMViewModel model)
    {
      this._FollowUpService.Add(new FollowUp()
      {
        PatientId = model.PatientId,
        FollowupDate = new DateTime?(model.followupViewModal.FollowupDate),
        FollowupTime = new DateTime?(model.followupViewModal.FollowupTime),
        DeptId = 7,
        FollowupReason = model.followupViewModal.FollowupReason,
        CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))),
        CreatedSystem = this._Dropdownservice.GetIPAddress(false),
        ReferredTreatmentId = 5
      });
    }

    public void PROSDIMUpdateAllotment(PROSDIMViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        ProcedureNotes = model.studentAllotmentViewModel.ProcedureNotes,
        ProcedureNotesDate = new DateTime?(DateTime.Now),
        DoctorId = model.studentAllotmentViewModel.DoctorId,
        AllotId = model.studentAllotmentViewModel.AllotId,
        CaserecordId = model.ProsthoDIMId,
        ReferredTreatmentId = 5
      });
    }

    public void PROSDIMUpdateReferralStatus(PROSDIMViewModel model)
    {
      int patientId = model.PatientId;
      int num = 7;
      this._Referralservice.UpdateReferralStatus(new ReferralStatus()
      {
        VisitType = "dbo.GetVisitType(" + (object) patientId + "," + (object) num + "," + (object) model.TreatmentReferredId + ")",
        ReferredId = model.TreatmentReferredId,
        ReferredTreatmentId = 5
      });
    }

    public PROSDIMViewModel BindPROSDIMPatientReport(int PROSDIMId)
    {
      PROSDIMViewModel prosdimViewModel1 = new PROSDIMViewModel();
      PROSDIMViewModel prosdimViewModel2 = this._uow.Repository<PROSDIMViewModel>().GetEntitiesBySql(string.Format("exec GetPROSDIMCasesheetReport {0}", (object) PROSDIMId)).FirstOrDefault<PROSDIMViewModel>();
      prosdimViewModel2.patientInformationViewModel = new PatientInformationViewModel()
      {
        PatientName = prosdimViewModel2.PatientName,
        AgeGender = prosdimViewModel2.Age.ToString() + "/" + (object) (Gender) prosdimViewModel2.GenderId,
        Phone = prosdimViewModel2.Phone,
        OpNo = prosdimViewModel2.OpNo,
        Area = prosdimViewModel2.Area
      };
      prosdimViewModel2.FileUploadlist = this._uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) prosdimViewModel2.PatientId, (object) 7));
      prosdimViewModel2.paidInvestigationList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.PaidBillsforReport, (object) prosdimViewModel2.PatientId, (object) 7, (object) PROSDIMId, (object) 5, (object) 20, (object) 16));
      prosdimViewModel2.deptTreatmentList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.DeptTreatmentforReport, (object) prosdimViewModel2.PatientId, (object) 7, (object) PROSDIMId, (object) 5));
      ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
      {
        PatientId = prosdimViewModel2.PatientId,
        FromdeptId = 7
      };
      prosdimViewModel2.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(prosdimViewModel2.PatientId).ToList<ReferralStatusViewModel>();
      prosdimViewModel2.followupViewModal = new FollowupViewModal()
      {
        PatientId = prosdimViewModel2.PatientId,
        DeptId = 7,
        FollowupDate = DateTime.Now,
        FollowupTime = DateTime.Now
      };
      prosdimViewModel2.followupList = this._FollowUpService.LoadFollowupList(prosdimViewModel2.PatientId, 7, 5);
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      prosdimViewModel2.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(prosdimViewModel2.PatientId, 7, 5);
      return prosdimViewModel2;
    }

    public List<PROSSearchDetails> prosthoSearchDetails(string From_Date, string To_Date, int TreatmentId, string url)
    {
      string str = "(OP.IsDummy ='N')";
      if (this._uow.Repository<IsDummyEnable>().GetAll("SearchForm ='Y'").Count<IsDummyEnable>() > 0)
        str = "(0=0)";
      List<PROSSearchDetails> prosSearchDetailsList = new List<PROSSearchDetails>();
      List<PROSSearchDetails> list;
      switch (TreatmentId)
      {
        case 1:
          list = this._uow.Repository<PROSSearchDetails>().GetEntitiesBySql(string.Format(Queries.PROSCDSearch, (object) From_Date, (object) To_Date, (object) 7, (object) url, (object) str)).ToList<PROSSearchDetails>();
          break;
        case 2:
          list = this._uow.Repository<PROSSearchDetails>().GetEntitiesBySql(string.Format(Queries.PROSRPDSearch, (object) From_Date, (object) To_Date, (object) 7, (object) url, (object) str)).ToList<PROSSearchDetails>();
          break;
        case 3:
          list = this._uow.Repository<PROSSearchDetails>().GetEntitiesBySql(string.Format(Queries.PROSFPDSearch, (object) From_Date, (object) To_Date, (object) 7, (object) url, (object) str)).ToList<PROSSearchDetails>();
          break;
        case 4:
          list = this._uow.Repository<PROSSearchDetails>().GetEntitiesBySql(string.Format(Queries.PROSMFPSearch, (object) From_Date, (object) To_Date, (object) 7, (object) url, (object) str)).ToList<PROSSearchDetails>();
          break;
        case 5:
          list = this._uow.Repository<PROSSearchDetails>().GetEntitiesBySql(string.Format(Queries.PROSDIMSearch, (object) From_Date, (object) To_Date, (object) 7, (object) url, (object) str)).ToList<PROSSearchDetails>();
          break;
        default:
          list = this._uow.Repository<PROSSearchDetails>().GetEntitiesBySql(string.Format(Queries.PROSAllSearch, (object) From_Date, (object) To_Date, (object) 7, (object) url, (object) str)).ToList<PROSSearchDetails>();
          break;
      }
      return list;
    }

    public void CDProcedureApproval(PROSCDViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        DoctorApproval = "Y",
        DoctorApprovalDate = new DateTime?(DateTime.Now),
        AllotId = model.studentAllotmentViewModel.AllotId
      });
    }

    public void RPDProcedureApproval(PROSRPDViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        DoctorApproval = "Y",
        DoctorApprovalDate = new DateTime?(DateTime.Now),
        AllotId = model.studentAllotmentViewModel.AllotId
      });
    }

    public void FPDProcedureApproval(PROSFPDViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        DoctorApproval = "Y",
        DoctorApprovalDate = new DateTime?(DateTime.Now),
        AllotId = model.studentAllotmentViewModel.AllotId
      });
    }

    public void MFPProcedureApproval(PROSMFPViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        DoctorApproval = "Y",
        DoctorApprovalDate = new DateTime?(DateTime.Now),
        AllotId = model.studentAllotmentViewModel.AllotId
      });
    }

    public void DIMProcedureApproval(PROSDIMViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        DoctorApproval = "Y",
        DoctorApprovalDate = new DateTime?(DateTime.Now),
        AllotId = model.studentAllotmentViewModel.AllotId
      });
    }

    public List<StudentAllotmentViewModel> ProsCDCasesheetApprovalList(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.PROSCDApproval, (object) From_Date, (object) To_Date, (object) 7, (object) url, (object) 1)).ToList<StudentAllotmentViewModel>();
    }

    public List<StudentAllotmentViewModel> ProsRPDCasesheetApprovalList(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.PROSRPDApproval, (object) From_Date, (object) To_Date, (object) 7, (object) url, (object) 2)).ToList<StudentAllotmentViewModel>();
    }

    public List<StudentAllotmentViewModel> ProsFPDCasesheetApprovalList(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.PROSFPDApproval, (object) From_Date, (object) To_Date, (object) 7, (object) url, (object) 3)).ToList<StudentAllotmentViewModel>();
    }

    public List<StudentAllotmentViewModel> ProsMFPCasesheetApprovalList(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.PROSMFPApproval, (object) From_Date, (object) To_Date, (object) 7, (object) url, (object) 4)).ToList<StudentAllotmentViewModel>();
    }

    public List<StudentAllotmentViewModel> ProsDIMCasesheetApprovalList(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.PROSDIMApproval, (object) From_Date, (object) To_Date, (object) 7, (object) url, (object) 5)).ToList<StudentAllotmentViewModel>();
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
