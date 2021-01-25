// Decompiled with JetBrains decompiler
// Type: DIMS.Services.Implementation.PEDOCasesheetService
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
  public class PEDOCasesheetService : ServiceBase<PEDOCasesheet>, IPEDOCasesheetService, IService<PEDOCasesheet>
  {
    private IUnitOfWork _uow;
    private IOPDPatientRegistrationService _OPDservice;
    private IMASCodeService _Dropdownservice;
    private IStudentAllotmentService _AllotmentService;
    private IReferralStatusService _Referralservice;
    private IBillQueueService _BillQueueservice;
    private IFollowUpService _FollowUpService;
    private ICasesheetNoService _CasesheetNoService;
    private IPrescriptionsService _PrescriptionsService;

    public PEDOCasesheetService(IUnitOfWork uow)
      : base(uow)
    {
      this._uow = uow;
      this._OPDservice = (IOPDPatientRegistrationService) new OPDPatientRegistrationService(this._uow);
      this._Dropdownservice = (IMASCodeService) new MASCodeService(this._uow);
      this._AllotmentService = (IStudentAllotmentService) new StudentAllotmentService(this._uow);
      this._Referralservice = (IReferralStatusService) new ReferralStatusService(this._uow);
      this._BillQueueservice = (IBillQueueService) new BillQueueService(this._uow);
      this._FollowUpService = (IFollowUpService) new FollowUpService(this._uow);
      this._CasesheetNoService = (ICasesheetNoService) new CasesheetNoService(this._uow);
      this._PrescriptionsService = (IPrescriptionsService) new PrescriptionsService(this._uow);
    }

    public PEDOViewModel GetPEDOPatientDetails(int Id)
    {
      return this._uow.Repository<PEDOViewModel>().GetEntitiesBySql(string.Format("exec GetPEDOCasesheet {0}", (object) Id)).FirstOrDefault<PEDOViewModel>();
    }

    public IEnumerable<PEDOCasesheetProperties> GetProperties()
    {
      return this._uow.Repository<PEDOCasesheetProperties>().GetAll();
    }

    public PEDOViewModel BindPEDOPatientModel(long allotId)
    {
      PEDOViewModel pedoViewModel = new PEDOViewModel();
      if (!string.IsNullOrEmpty(allotId.ToString()))
      {
        IEnumerable<MASCode> allCodes = this._Dropdownservice.GetAllCodes();
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + (object) 6;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 6);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        pedoViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        pedoViewModel.patientInformationViewModel = informationViewModel;
        pedoViewModel.studentAllotmentViewModel = allotmentViewModel;
        pedoViewModel.PatientId = informationViewModel.PatientId;
        pedoViewModel.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        pedoViewModel.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 6, 0);
        pedoViewModel.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 6);
        pedoViewModel.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(6);
        pedoViewModel.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 6, 0);
        pedoViewModel.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 6);
        pedoViewModel.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        pedoViewModel.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 6).ToList<ReferralStatusViewModel>();
        pedoViewModel.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 6, 0);
        pedoViewModel.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        pedoViewModel.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        pedoViewModel.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        pedoViewModel.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 6,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        pedoViewModel.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 6, 0);
        pedoViewModel.EruptedToothYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 58)).ToList<MASCode>();
        pedoViewModel.PhysicianCareYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 64)).ToList<MASCode>();
        pedoViewModel.FirstDentalVisitYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 94)).ToList<MASCode>();
        pedoViewModel.ToothBrushMethodList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 95)).ToList<MASCode>();
        pedoViewModel.AntibioticsBefoYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 96)).ToList<MASCode>();
        pedoViewModel.FrankelzRatingList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 97)).ToList<MASCode>();
        pedoViewModel.NormalCsectionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 98)).ToList<MASCode>();
        pedoViewModel.MedicationYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 65)).ToList<MASCode>();
        pedoViewModel.MotherConditionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 68)).ToList<MASCode>();
        pedoViewModel.AllergyHistoryYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 67)).OrderByDescending<MASCode, int>((Func<MASCode, int>) (a => a.CodeId)).ToList<MASCode>();
        pedoViewModel.ChildConditionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 66)).ToList<MASCode>();
        pedoViewModel.HospitalizedYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 69)).ToList<MASCode>();
        pedoViewModel.InformationSourceList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 70)).ToList<MASCode>();
        pedoViewModel.BrushingFrequencyList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 71)).ToList<MASCode>();
        pedoViewModel.SupervisedYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 72)).ToList<MASCode>();
        pedoViewModel.SleepNatureList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 99)).ToList<MASCode>();
        pedoViewModel.SleepDurationList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 100)).ToList<MASCode>();
        pedoViewModel.DaySleepList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 101)).ToList<MASCode>();
        pedoViewModel.MouthDrynessList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 102)).ToList<MASCode>();
        pedoViewModel.SnoringYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 103)).ToList<MASCode>();
        pedoViewModel.FullTermPrematureList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 104)).ToList<MASCode>();
        pedoViewModel.ConsanguineousMarriageYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 105)).ToList<MASCode>();
        pedoViewModel.DentalProblemsYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 106)).ToList<MASCode>();
        pedoViewModel.HospitalizationYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 73)).ToList<MASCode>();
        pedoViewModel.TraumaYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 74)).ToList<MASCode>();
        pedoViewModel.SignificantHistoryYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 75)).ToList<MASCode>();
        pedoViewModel.IllnessYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 76)).ToList<MASCode>();
        pedoViewModel.MomMedicationYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 77)).ToList<MASCode>();
        pedoViewModel.BluebabyYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 78)).ToList<MASCode>();
        pedoViewModel.BottleSleepYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 79)).ToList<MASCode>();
        pedoViewModel.PacifierYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 80)).ToList<MASCode>();
        pedoViewModel.VisitReactionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 107)).ToList<MASCode>();
        pedoViewModel.CommuLearnDifficultyYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 108)).ToList<MASCode>();
        pedoViewModel.BodyTypeList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 109)).ToList<MASCode>();
        pedoViewModel.FacialFormExtraOralList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 117)).ToList<MASCode>();
        pedoViewModel.FacialProfileList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 81)).ToList<MASCode>();
        pedoViewModel.FacialSymmetryList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 82)).ToList<MASCode>();
        pedoViewModel.TmjList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 83)).ToList<MASCode>();
        pedoViewModel.LipsList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 84)).ToList<MASCode>();
        pedoViewModel.IncissorRelationOverbiteList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 85)).ToList<MASCode>();
        pedoViewModel.CrossbiteList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 86)).ToList<MASCode>();
        pedoViewModel.OpenbiteList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 87)).ToList<MASCode>();
        pedoViewModel.MolarClassRightList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 88)).ToList<MASCode>();
        pedoViewModel.MolarClassLeftList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 89)).ToList<MASCode>();
        pedoViewModel.CanineClassRightList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 90)).ToList<MASCode>();
        pedoViewModel.CanineClassLeftList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 91)).ToList<MASCode>();
        pedoViewModel.MidlineList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 92)).ToList<MASCode>();
        pedoViewModel.MandibularShiftRightList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 93)).ToList<MASCode>();
        pedoViewModel.MandibularShiftLeftList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 118)).ToList<MASCode>();
        pedoViewModel.BloodTransfusionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 230)).ToList<MASCode>();
        pedoViewModel.FriendsMakingDifficultyList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 231)).ToList<MASCode>();
        pedoViewModel.GetAlongFailList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 232)).ToList<MASCode>();
        pedoViewModel.BrotherSisterList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 233)).ToList<MASCode>();
        pedoViewModel.SchoolWorkDiffList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 234)).ToList<MASCode>();
        pedoViewModel.DentistFearList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 235)).ToList<MASCode>();
        pedoViewModel.LipTypeList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 236)).ToList<MASCode>();
        pedoViewModel.FacialDivergenceList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 189)).ToList<MASCode>();
        pedoViewModel.HeadShapeList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 186)).ToList<MASCode>();
        pedoViewModel.StainsList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 237)).ToList<MASCode>();
        pedoViewModel.CalculusList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 238)).ToList<MASCode>();
        pedoViewModel.PlaqueList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 239)).ToList<MASCode>();
        pedoViewModel.RespiratoryList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 240)).ToList<MASCode>();
        pedoViewModel.CvsList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 241)).ToList<MASCode>();
        pedoViewModel.CnsList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 242)).ToList<MASCode>();
        pedoViewModel.ImmuneSystemList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 243)).ToList<MASCode>();
        pedoViewModel.RenalDiseaseList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 244)).ToList<MASCode>();
        pedoViewModel.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) pedoViewModel.PatientId))
        };
        pedoViewModel.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) pedoViewModel.PatientId))
        };
        IEnumerable<PEDOCasesheetProperties> properties = this.GetProperties();
        pedoViewModel.Proplist = properties;
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        pedoViewModel.medicalalertviewmodel = medicalAlertViewModel2;
      }
      return pedoViewModel;
    }

    public int SavePEDOPatient(PEDOViewModel model)
    {
      try
      {
        model.MandatoryDummy = "Y";
        PEDOCasesheet pedoCasesheet = new PEDOCasesheet();
        PEDOCasesheet entity1 = new MapperConfiguration((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PEDOViewModel, PEDOCasesheet>())).CreateMapper().Map<PEDOViewModel, PEDOCasesheet>(model);
        entity1.PEDODate = DateTime.Now;
        entity1.CreatedDate = new DateTime?(DateTime.Now);
        entity1.LastVisitedDate = new DateTime?(DateTime.Now);
        entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
        model.EruptedToothAtBirthDesc = !model.chkEruptedToothAtBirthDesc ? "N" : "Y";
        model.EruptedToothThirtyDaysDesc = !model.chkEruptedToothThirtyDaysDesc ? "N" : "Y";
        entity1.PEDONo = this._CasesheetNoService.GetPEDONo();
        int num = this._uow.Repository<PEDOCasesheet>().Add(entity1, false);
        IEnumerable<PEDOCasesheetProperties> all = this._uow.Repository<PEDOCasesheetProperties>().GetAll();
        foreach (PropertyInfo property in model.GetType().GetProperties())
        {
          PropertyInfo prop = property;
          if (all.FirstOrDefault<PEDOCasesheetProperties>((Func<PEDOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
          {
            string name = prop.Name;
            object obj = prop.GetValue((object) model, (object[]) null);
            if (obj != null)
            {
              PEDOCasesheetPropertyValues entity2 = new PEDOCasesheetPropertyValues();
              entity2.PEDOId = num;
              PEDOCasesheetProperties casesheetProperties = all.FirstOrDefault<PEDOCasesheetProperties>((Func<PEDOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
              if ((!(casesheetProperties.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(casesheetProperties.PropertyDataType == "int") || (int) obj != 0))
              {
                entity2.PropId = casesheetProperties.PropertyId;
                entity2.PropValues = obj.ToString();
                this._uow.Repository<PEDOCasesheetPropertyValues>().Add(entity2, false);
              }
            }
          }
        }
        model.PEDOId = num;
        if (model.CreatedepartmentReferredStatus != null)
        {
          ReferralStatus entity2 = new ReferralStatus();
          foreach (ReferralStatusViewModel createdepartmentReferredStatu in model.CreatedepartmentReferredStatus)
          {
            if (createdepartmentReferredStatu.chkToDeptId)
            {
              entity2.PatientId = model.PatientId;
              entity2.FromdeptId = 6;
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
        model.PEDOId = num;
        if (model.BillingQueueDetails != null)
          this._BillQueueservice.SaveInvestigation(model.BillingQueueDetails, new BillQueueDetails()
          {
            CaserecordId = num,
            ReferredTreatmentId = 0,
            DeptId = 6,
            PatientId = model.PatientId
          });
        if (model.BillingLabRadQueueDetails != null)
          this._BillQueueservice.SaveInvestigation(model.BillingLabRadQueueDetails, new BillQueueDetails()
          {
            CaserecordId = num,
            ReferredTreatmentId = 0,
            DeptId = 6,
            PatientId = model.PatientId
          });
        if (model.PrescriptionsDetails != null)
          this._PrescriptionsService.SavePrescriptions(model.PrescriptionsDetails, new Prescriptions()
          {
            PatientId = model.PatientId,
            AllotId = model.studentAllotmentViewModel.AllotId,
            DoctorId = model.studentAllotmentViewModel.DoctorId,
            StudentId = model.studentAllotmentViewModel.StudentId,
            DeptId = 6,
            ReferredTreatmentId = 0
          });
        return num;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public PEDOViewModel BindEditPEDOPatientModel(long allotId, int PEDOId)
    {
      try
      {
        IEnumerable<MASCode> allCodes = this._Dropdownservice.GetAllCodes();
        PEDOViewModel pedoPatientDetails = this.GetPEDOPatientDetails(PEDOId);
        pedoPatientDetails.chkEruptedToothAtBirthDesc = pedoPatientDetails.EruptedToothAtBirthDesc == "Y";
        pedoPatientDetails.chkEruptedToothThirtyDaysDesc = pedoPatientDetails.EruptedToothThirtyDaysDesc == "Y";
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + (object) 6;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        informationViewModel.DueAmount = this._AllotmentService.ShowPatientBalance(informationViewModel.PatientId, 6);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        allotmentViewModel.DoctorId = informationViewModel.DoctorId;
        ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
        {
          PatientId = informationViewModel.PatientId,
          FromdeptId = 6
        };
        pedoPatientDetails.patientInformationViewModel = informationViewModel;
        pedoPatientDetails.billingLabRadQueueViewModal = this._BillQueueservice.GetServicesListByLabRad();
        pedoPatientDetails.paidLabRadInvestigationList = this._BillQueueservice.BillpaidInvestigationList(informationViewModel.PatientId, 6);
        pedoPatientDetails.BillingLabRadQueueDetails = this._BillQueueservice.BillLabRadServicesList(informationViewModel.PatientId, 6, 0);
        pedoPatientDetails.ApprovedepartmentReferral = this._Referralservice.ReferralApprovalList(informationViewModel.PatientId, 6).ToList<ReferralStatusViewModel>();
        pedoPatientDetails.CreatedepartmentReferredStatus = this._Referralservice.DefaultReferralList(informationViewModel.PatientId, 6).ToList<ReferralStatusViewModel>();
        pedoPatientDetails.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(informationViewModel.PatientId).ToList<ReferralStatusViewModel>();
        pedoPatientDetails.BillingQueueDetails = this._BillQueueservice.BillServicesList(informationViewModel.PatientId, 6, 0);
        pedoPatientDetails.paidInvestigationList = this._BillQueueservice.BillpaidtreatemList(informationViewModel.PatientId, 6);
        pedoPatientDetails.billingQueueViewModal = this._BillQueueservice.GetServicesListByDeptId(6);
        pedoPatientDetails.studentAllotmentViewModel = allotmentViewModel;
        pedoPatientDetails.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(informationViewModel.PatientId, 6, 0);
        pedoPatientDetails.PrescriptionsList = this._PrescriptionsService.LoadPrescriptionsList();
        pedoPatientDetails.PrescriptionsDetails = Enumerable.Empty<PrescriptionsViewModel>();
        pedoPatientDetails.PreviousPrescriptionList = this._PrescriptionsService.PreviousPrescriptionsList(informationViewModel.PatientId);
        pedoPatientDetails.TreatmentReferredId = informationViewModel.ReferredId;
        pedoPatientDetails.followupViewModal = new FollowupViewModal()
        {
          PatientId = informationViewModel.PatientId,
          DeptId = 6,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        pedoPatientDetails.followupList = this._FollowUpService.LoadFollowupList(informationViewModel.PatientId, 6, 0);
        pedoPatientDetails.EruptedToothYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 58)).ToList<MASCode>();
        pedoPatientDetails.PhysicianCareYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 64)).ToList<MASCode>();
        pedoPatientDetails.FirstDentalVisitYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 94)).ToList<MASCode>();
        pedoPatientDetails.ToothBrushMethodList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 95)).ToList<MASCode>();
        pedoPatientDetails.AntibioticsBefoYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 96)).ToList<MASCode>();
        pedoPatientDetails.FrankelzRatingList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 97)).ToList<MASCode>();
        pedoPatientDetails.NormalCsectionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 98)).ToList<MASCode>();
        pedoPatientDetails.MedicationYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 65)).ToList<MASCode>();
        pedoPatientDetails.MotherConditionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 68)).ToList<MASCode>();
        pedoPatientDetails.AllergyHistoryYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 67)).ToList<MASCode>();
        pedoPatientDetails.ChildConditionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 66)).ToList<MASCode>();
        pedoPatientDetails.HospitalizedYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 69)).ToList<MASCode>();
        pedoPatientDetails.InformationSourceList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 70)).ToList<MASCode>();
        pedoPatientDetails.BrushingFrequencyList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 71)).ToList<MASCode>();
        pedoPatientDetails.SupervisedYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 72)).ToList<MASCode>();
        pedoPatientDetails.SleepNatureList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 99)).ToList<MASCode>();
        pedoPatientDetails.SleepDurationList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 100)).ToList<MASCode>();
        pedoPatientDetails.DaySleepList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 101)).ToList<MASCode>();
        pedoPatientDetails.MouthDrynessList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 102)).ToList<MASCode>();
        pedoPatientDetails.SnoringYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 103)).ToList<MASCode>();
        pedoPatientDetails.FullTermPrematureList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 104)).ToList<MASCode>();
        pedoPatientDetails.ConsanguineousMarriageYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 105)).ToList<MASCode>();
        pedoPatientDetails.DentalProblemsYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 106)).ToList<MASCode>();
        pedoPatientDetails.HospitalizationYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 73)).ToList<MASCode>();
        pedoPatientDetails.TraumaYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 74)).ToList<MASCode>();
        pedoPatientDetails.SignificantHistoryYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 75)).ToList<MASCode>();
        pedoPatientDetails.IllnessYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 76)).ToList<MASCode>();
        pedoPatientDetails.MomMedicationYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 77)).ToList<MASCode>();
        pedoPatientDetails.BluebabyYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 78)).ToList<MASCode>();
        pedoPatientDetails.BottleSleepYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 79)).ToList<MASCode>();
        pedoPatientDetails.PacifierYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 80)).ToList<MASCode>();
        pedoPatientDetails.VisitReactionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 107)).ToList<MASCode>();
        pedoPatientDetails.CommuLearnDifficultyYesNoList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 108)).ToList<MASCode>();
        pedoPatientDetails.BodyTypeList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 109)).ToList<MASCode>();
        pedoPatientDetails.FacialFormExtraOralList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 117)).ToList<MASCode>();
        pedoPatientDetails.FacialProfileList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 81)).ToList<MASCode>();
        pedoPatientDetails.FacialSymmetryList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 82)).ToList<MASCode>();
        pedoPatientDetails.TmjList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 83)).ToList<MASCode>();
        pedoPatientDetails.LipsList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 84)).ToList<MASCode>();
        pedoPatientDetails.IncissorRelationOverbiteList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 85)).ToList<MASCode>();
        pedoPatientDetails.CrossbiteList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 86)).ToList<MASCode>();
        pedoPatientDetails.OpenbiteList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 87)).ToList<MASCode>();
        pedoPatientDetails.MolarClassRightList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 88)).ToList<MASCode>();
        pedoPatientDetails.MolarClassLeftList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 89)).ToList<MASCode>();
        pedoPatientDetails.CanineClassRightList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 90)).ToList<MASCode>();
        pedoPatientDetails.CanineClassLeftList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 91)).ToList<MASCode>();
        pedoPatientDetails.MidlineList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 92)).ToList<MASCode>();
        pedoPatientDetails.MandibularShiftRightList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 93)).ToList<MASCode>();
        pedoPatientDetails.MandibularShiftLeftList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 118)).ToList<MASCode>();
        pedoPatientDetails.BloodTransfusionList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 230)).ToList<MASCode>();
        pedoPatientDetails.FriendsMakingDifficultyList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 231)).ToList<MASCode>();
        pedoPatientDetails.GetAlongFailList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 232)).ToList<MASCode>();
        pedoPatientDetails.BrotherSisterList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 233)).ToList<MASCode>();
        pedoPatientDetails.SchoolWorkDiffList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 234)).ToList<MASCode>();
        pedoPatientDetails.DentistFearList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 235)).ToList<MASCode>();
        pedoPatientDetails.LipTypeList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 236)).ToList<MASCode>();
        pedoPatientDetails.FacialDivergenceList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 189)).ToList<MASCode>();
        pedoPatientDetails.HeadShapeList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 186)).ToList<MASCode>();
        pedoPatientDetails.StainsList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 237)).ToList<MASCode>();
        pedoPatientDetails.CalculusList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 238)).ToList<MASCode>();
        pedoPatientDetails.PlaqueList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 239)).ToList<MASCode>();
        pedoPatientDetails.RespiratoryList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 240)).ToList<MASCode>();
        pedoPatientDetails.CvsList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 241)).ToList<MASCode>();
        pedoPatientDetails.CnsList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 242)).ToList<MASCode>();
        pedoPatientDetails.ImmuneSystemList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 243)).ToList<MASCode>();
        pedoPatientDetails.RenalDiseaseList = (IEnumerable<MASCode>) allCodes.Where<MASCode>((Func<MASCode, bool>) (x => x.CodeTypeId == 244)).ToList<MASCode>();
        IEnumerable<PEDOCasesheetProperties> properties = this.GetProperties();
        pedoPatientDetails.Proplist = properties;
        pedoPatientDetails.radioRegistrationviewmodel = new RadioRegistrationViewModel()
        {
          RadioList = this._uow.Repository<RADIORegistration>().GetEntitiesBySql(string.Format(Queries.GetRadioNoforAllDept, (object) pedoPatientDetails.PatientId))
        };
        pedoPatientDetails.laboratoryRegistrationviewmodel = new LaboratoryRegistrationViewModel()
        {
          LaboratoryList = this._uow.Repository<LaboratoryRegistration>().GetEntitiesBySql(string.Format(Queries.GetLaboratoryNoforAllDept, (object) pedoPatientDetails.PatientId))
        };
        MedicalAlertViewModel medicalAlertViewModel1 = new MedicalAlertViewModel();
        MedicalAlertViewModel medicalAlertViewModel2 = this._PrescriptionsService.BindMedicalAlert(informationViewModel.PatientId);
        pedoPatientDetails.medicalalertviewmodel = medicalAlertViewModel2;
        pedoPatientDetails.approvalViewModal = new ApprovalViewModal()
        {
          ApprovalTypeId = 1,
          CaserecordId = PEDOId,
          DeptId = 6,
          DoctorId = allotmentViewModel.DoctorId,
          PatientId = (long) pedoPatientDetails.PatientId,
          ReferredTreatmentId = 0
        };
        if (pedoPatientDetails.Approval1)
        {
          pedoPatientDetails.DisplayApproval1 = "Approved";
          pedoPatientDetails.ReadOnlyApproval1 = true;
        }
        else
        {
          pedoPatientDetails.DisplayApproval1 = "Not Approved";
          pedoPatientDetails.ReadOnlyApproval1 = false;
        }
        if (pedoPatientDetails.Approval2)
        {
          pedoPatientDetails.DisplayApproval2 = "Approved";
          pedoPatientDetails.ReadOnlyApproval2 = true;
          pedoPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = true;
        }
        else
        {
          pedoPatientDetails.DisplayApproval2 = "Not Approved";
          pedoPatientDetails.ReadOnlyApproval2 = false;
          pedoPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval2 = false;
        }
        if (pedoPatientDetails.Approval3)
        {
          pedoPatientDetails.DisplayApproval3 = "Casesheet Closed";
          pedoPatientDetails.ReadOnlyApproval3 = true;
          pedoPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = true;
          pedoPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = true;
        }
        else
        {
          pedoPatientDetails.DisplayApproval3 = "Not Approved";
          pedoPatientDetails.ReadOnlyApproval3 = false;
          pedoPatientDetails.billingLabRadQueueViewModal.ReadOnlyApproval3 = false;
          pedoPatientDetails.billingQueueViewModal.ReadOnlyApproval3 = false;
        }
        PEDOViewModel pedoViewModel = this._uow.Repository<PEDOViewModel>().GetEntitiesBySql(string.Format(Queries.DisplayPedoApprovalStage, (object) 6, (object) pedoPatientDetails.PEDOId)).FirstOrDefault<PEDOViewModel>();
        pedoPatientDetails.ApprovalStage = pedoViewModel == null ? "Not Initiated" : (!(pedoViewModel.ApprovalStage == "") ? pedoViewModel.ApprovalStage : "Not Initiated");
        pedoPatientDetails.ReadOnlyApproval4 = true;
        return pedoPatientDetails;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public int UpdatePEDOPatient(PEDOViewModel ViewModel)
    {
      ViewModel.PEDODate = Convert.ToDateTime(ViewModel.PEDODate.ToString("yyyy-MM-dd HH:mm:ss.fff"));
      ViewModel.PEDOId = ViewModel.PEDOId;
      PEDOCasesheet pedoCasesheet = new PEDOCasesheet();
      Mapper.Initialize((Action<IMapperConfiguration>) (cfg => cfg.CreateMap<PEDOViewModel, PEDOCasesheet>()));
      PEDOCasesheet entity1 = Mapper.Map<PEDOViewModel, PEDOCasesheet>(ViewModel);
      entity1.ModifiedDate = new DateTime?(DateTime.Now);
      entity1.LastVisitedDate = new DateTime?(DateTime.Now);
      entity1.CreatedSystem = this._Dropdownservice.GetIPAddress(false);
      ViewModel.EruptedToothAtBirthDesc = !ViewModel.chkEruptedToothAtBirthDesc ? "N" : "Y";
      ViewModel.EruptedToothThirtyDaysDesc = !ViewModel.chkEruptedToothThirtyDaysDesc ? "N" : "Y";
      this._uow.Repository<PEDOCasesheet>().Update(entity1, false);
      IEnumerable<PEDOCasesheetProperties> all1 = this._uow.Repository<PEDOCasesheetProperties>().GetAll();
      IEnumerable<PEDOCasesheetPropertyValues> all2 = this._uow.Repository<PEDOCasesheetPropertyValues>().GetAll("PEDOId=" + (object) ViewModel.PEDOId);
      foreach (PropertyInfo property1 in ViewModel.GetType().GetProperties())
      {
        PropertyInfo prop = property1;
        if (all1.FirstOrDefault<PEDOCasesheetProperties>((Func<PEDOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name)) != null)
        {
          string name = prop.Name;
          object obj = prop.GetValue((object) ViewModel, (object[]) null);
          if (obj != null)
          {
            PEDOCasesheetPropertyValues entity2 = new PEDOCasesheetPropertyValues();
            entity2.PEDOId = ViewModel.PEDOId;
            PEDOCasesheetProperties property = all1.FirstOrDefault<PEDOCasesheetProperties>((Func<PEDOCasesheetProperties, bool>) (a => a.PropertyName == prop.Name));
            if ((!(property.PropertyDataType == "decimal") || !((Decimal) obj == Decimal.Zero)) && (!(property.PropertyDataType == "int") || (int) obj != 0))
            {
              PEDOCasesheetPropertyValues casesheetPropertyValues = all2.FirstOrDefault<PEDOCasesheetPropertyValues>((Func<PEDOCasesheetPropertyValues, bool>) (a =>
              {
                if (a.PropId == property.PropertyId)
                  return a.PEDOId == ViewModel.PEDOId;
                return false;
              }));
              if (casesheetPropertyValues != null)
              {
                entity2.PropId = property.PropertyId;
                entity2.PropValues = obj.ToString();
                entity2.ValueId = casesheetPropertyValues.ValueId;
                this._uow.Repository<PEDOCasesheetPropertyValues>().Update(entity2, false);
              }
              else
              {
                entity2.PropId = property.PropertyId;
                entity2.PropValues = obj.ToString();
                this._uow.Repository<PEDOCasesheetPropertyValues>().Add(entity2, false);
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
            entity2.FromdeptId = 6;
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
          CaserecordId = ViewModel.PEDOId,
          ReferredTreatmentId = 0,
          DeptId = 6,
          PatientId = ViewModel.PatientId
        });
      if (ViewModel.BillingLabRadQueueDetails != null)
        this._BillQueueservice.SaveInvestigation(ViewModel.BillingLabRadQueueDetails, new BillQueueDetails()
        {
          CaserecordId = ViewModel.PEDOId,
          ReferredTreatmentId = 0,
          DeptId = 6,
          PatientId = ViewModel.PatientId
        });
      if (ViewModel.PrescriptionsDetails != null)
        this._PrescriptionsService.SavePrescriptions(ViewModel.PrescriptionsDetails, new Prescriptions()
        {
          PatientId = ViewModel.PatientId,
          AllotId = ViewModel.studentAllotmentViewModel.AllotId,
          DoctorId = ViewModel.studentAllotmentViewModel.DoctorId,
          StudentId = ViewModel.studentAllotmentViewModel.StudentId,
          DeptId = 6,
          ReferredTreatmentId = 0
        });
      return entity1.PEDOId;
    }

    public StudentAllotmentViewModel DisplayAllotment(int PatientId, long ReferredId, int CourseType)
    {
      StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
      int DeptId = 6;
      string DeptCode = Department.PEDO.ToString();
      return this._AllotmentService.DisplayAllotment(PatientId, ReferredId, CourseType, DeptId, DeptCode);
    }

    public void UpdateAllotment(PEDOViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        ProcedureNotes = model.studentAllotmentViewModel.ProcedureNotes,
        ProcedureNotesDate = new DateTime?(DateTime.Now),
        DoctorId = model.studentAllotmentViewModel.DoctorId,
        AllotId = model.studentAllotmentViewModel.AllotId,
        CaserecordId = model.PEDOId,
        ReferredTreatmentId = 0
      });
    }

    public void SavefollowUp(PEDOViewModel model)
    {
      this._FollowUpService.Add(new FollowUp()
      {
        PatientId = model.PatientId,
        FollowupDate = new DateTime?(model.followupViewModal.FollowupDate),
        FollowupTime = new DateTime?(model.followupViewModal.FollowupTime),
        DeptId = 6,
        FollowupReason = model.followupViewModal.FollowupReason,
        CreatedDate = new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))),
        CreatedSystem = this._Dropdownservice.GetIPAddress(false),
        ReferredTreatmentId = 0
      });
    }

    public void UpdateReferralStatus(PEDOViewModel model)
    {
      int patientId = model.PatientId;
      int num = 6;
      this._Referralservice.UpdateReferralStatus(new ReferralStatus()
      {
        VisitType = "dbo.GetVisitType(" + (object) patientId + "," + (object) num + "," + (object) model.TreatmentReferredId + ")",
        ReferredId = model.TreatmentReferredId,
        ReferredTreatmentId = 0
      });
    }

    public void ProcedureApproval(PEDOViewModel model)
    {
      this._AllotmentService.Update(new StudentAllotment()
      {
        DoctorApproval = "Y",
        DoctorApprovalDate = new DateTime?(DateTime.Now),
        AllotId = model.studentAllotmentViewModel.AllotId
      });
    }

    public PEDOViewModel DisplayPEDOPatient(int Id)
    {
      return new PEDOViewModel();
    }

    public PEDOViewModel BindPEDOPatientReport(int PEDOId)
    {
      try
      {
        PEDOViewModel pedoViewModel1 = new PEDOViewModel();
        PEDOViewModel pedoViewModel2 = this._uow.Repository<PEDOViewModel>().GetEntitiesBySql(string.Format("exec GetPEDOCasesheetReport {0}", (object) PEDOId)).FirstOrDefault<PEDOViewModel>();
        pedoViewModel2.patientInformationViewModel = new PatientInformationViewModel()
        {
          PatientName = pedoViewModel2.PatientName,
          AgeGender = pedoViewModel2.Age.ToString() + "/" + (object) (Gender) pedoViewModel2.GenderId.Value,
          Phone = pedoViewModel2.Phone,
          OpNo = pedoViewModel2.OpNo,
          Area = pedoViewModel2.Area
        };
        pedoViewModel2.FileUploadlist = this._uow.Repository<FileUploadViewModel>().GetEntitiesBySql(string.Format(Queries.FileUploadList, (object) pedoViewModel2.PatientId, (object) 6));
        pedoViewModel2.paidInvestigationList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.PaidBillsforReport, (object) pedoViewModel2.PatientId, (object) 6, (object) PEDOId, (object) 0, (object) 20, (object) 16));
        pedoViewModel2.deptTreatmentList = this._uow.Repository<BillingViewModal>().GetEntitiesBySql(string.Format(Queries.DeptTreatmentforReport, (object) pedoViewModel2.PatientId, (object) 6, (object) PEDOId, (object) 0));
        ReferralStatusViewModel referralStatusViewModel = new ReferralStatusViewModel()
        {
          PatientId = pedoViewModel2.PatientId,
          FromdeptId = 6
        };
        pedoViewModel2.ViewdepartmentReferredStatus = this._Referralservice.ReferralList(pedoViewModel2.PatientId).ToList<ReferralStatusViewModel>();
        pedoViewModel2.followupViewModal = new FollowupViewModal()
        {
          PatientId = pedoViewModel2.PatientId,
          DeptId = 6,
          FollowupDate = DateTime.Now,
          FollowupTime = DateTime.Now
        };
        pedoViewModel2.followupList = this._FollowUpService.LoadFollowupList(pedoViewModel2.PatientId, 6, 0);
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        pedoViewModel2.studentProcedureNotesViewModel = this._AllotmentService.LoadProcedureNotesList(pedoViewModel2.PatientId, 6, 0);
        return pedoViewModel2;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public List<PEDOSearchDetails> pedoSearchDetails(string From_Date, string To_Date, string url)
    {
      string str = "(OP.IsDummy ='N')";
      if (this._uow.Repository<IsDummyEnable>().GetAll("SearchForm ='Y'").Count<IsDummyEnable>() > 0)
        str = "(0=0)";
      return this._uow.Repository<PEDOSearchDetails>().GetEntitiesBySql(string.Format(Queries.PEDOSearch, (object) From_Date, (object) To_Date, (object) 6, (object) url, (object) str)).ToList<PEDOSearchDetails>();
    }

    public List<StudentAllotmentViewModel> pedoCasesheetApprovalList(string From_Date, string To_Date, string url)
    {
      return this._uow.Repository<StudentAllotmentViewModel>().GetEntitiesBySql(string.Format(Queries.PEDOApproval, (object) From_Date, (object) To_Date, (object) 6, (object) url)).ToList<StudentAllotmentViewModel>();
    }

    public PEDOViewModel BindTreatmentList(long allotId)
    {
      PEDOViewModel pedoViewModel = new PEDOViewModel();
      if (!string.IsNullOrEmpty(allotId.ToString()))
      {
        PatientInformationViewModel informationViewModel = this._uow.Repository<PatientInformationViewModel>().GetEntitiesBySql(string.Format(Queries.AllotedPatientDetails, (object) allotId)).SingleOrDefault<PatientInformationViewModel>();
        string whereClause = "DeptId=" + (object) 6;
        informationViewModel.Doctorlist = this._uow.Repository<MASDoctor>().GetAll(whereClause);
        informationViewModel.AgeGender = informationViewModel.Age.ToString() + "/" + (object) (Gender) informationViewModel.GenderId;
        StudentAllotmentViewModel allotmentViewModel = new StudentAllotmentViewModel();
        allotmentViewModel.AllotId = allotId;
        allotmentViewModel.ProcedureNotes = informationViewModel.ProcedureNotes;
        allotmentViewModel.DoctorApproval = informationViewModel.DoctorApproval;
        pedoViewModel.TreatmentReferredId = informationViewModel.ReferredId;
        pedoViewModel.patientInformationViewModel = informationViewModel;
        pedoViewModel.studentAllotmentViewModel = allotmentViewModel;
        pedoViewModel.PatientId = informationViewModel.PatientId;
        pedoViewModel.Treatmentlist = (IEnumerable<PEDOViewModel>) this._uow.Repository<PEDOViewModel>().GetEntitiesBySql(string.Format(Queries.PEDOPatientTreatmentList, (object) informationViewModel.PatientId)).ToList<PEDOViewModel>();
      }
      return pedoViewModel;
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
